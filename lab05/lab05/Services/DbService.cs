using lab05.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace lab05.Services
{
    public class DbService : IDbService
    {
        private const string ConnString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=warehouse;Integrated Security=True";

        public async Task<int> RegisterProduct(ProductWarehouseRegister productWarehouseRegister)
        {
            string sql = "SELECT Count(1) FROM Product WHERE IdProduct = @idProduct";
            await using SqlConnection connection = new(ConnString);
            await using SqlCommand comm = new(sql, connection);
            comm.Parameters.AddWithValue("idProduct", productWarehouseRegister.IdProduct);

            sql = "SELECT Count(1) FROM Warehouse WHERE IdWarehouse = @idWarehouse";
            await using SqlCommand comm2 = new(sql, connection);
            comm2.Parameters.AddWithValue("idWarehouse", productWarehouseRegister.IdWarehouse);

            await connection.OpenAsync();
            if ((int)  await comm.ExecuteScalarAsync() < 1)
            {
                //nie ma produktu
                return -1;
            }
            if ((int) await comm2.ExecuteScalarAsync() < 1)
            {
                //nie ma magazynu
                return -4;
            }
 

            sql = "SELECT IdOrder FROM [Order] WHERE IdProduct = @idProduct AND Amount = @amount AND CreatedAt < @createdAt";
            await using SqlCommand comm3 = new(sql, connection);
            comm3.Parameters.AddWithValue("idProduct", productWarehouseRegister.IdProduct);
            comm3.Parameters.AddWithValue("amount", productWarehouseRegister.Amount);
            comm3.Parameters.AddWithValue("createdAt", productWarehouseRegister.CreatedAt);
            await using SqlDataReader dr = await comm3.ExecuteReaderAsync();
            string idOrder = "";

            if (!dr.HasRows)
            {
                //tu 404 nie ma takiego zamowienia
                return -2;
            }
            else
            {
                await dr.ReadAsync();
                idOrder = dr["IdOrder"].ToString();

            }
            dr.Close();

            sql = "SELECT Count(1) FROM Product_Warehouse WHERE IdOrder = @idOrder";
            await using SqlCommand comm4 = new(sql, connection);
            comm4.Parameters.AddWithValue("idOrder", idOrder);

            if ((int)await comm4.ExecuteScalarAsync() > 0)
            {
                //zamowienie  juz zrealizowane
                return -3;
            }

            sql = "UPDATE [Order] SET FulfilledAt = @createdAt WHERE IdOrder = @idOrder";
            await using SqlCommand comm5 = new(sql, connection);
            comm5.Parameters.AddWithValue("createdAt",productWarehouseRegister.CreatedAt);
            comm5.Parameters.AddWithValue("idOrder", idOrder);
            await comm5.ExecuteNonQueryAsync();

            sql = "SELECT Price FROM Product WHERE IdProduct = @idProduct";
            await using SqlCommand comm6 = new(sql, connection);
            comm6.Parameters.AddWithValue("idProduct",productWarehouseRegister.IdProduct);
            decimal price =  (decimal) await comm6.ExecuteScalarAsync()*productWarehouseRegister.Amount;

            sql = "INSERT INTO Product_Warehouse(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)" +
                " VALUES(@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt)";
            await using SqlCommand comm7 = new(sql, connection);
            comm7.Parameters.AddWithValue("IdWarehouse",productWarehouseRegister.IdWarehouse);
            comm7.Parameters.AddWithValue("IdProduct",productWarehouseRegister.IdProduct);
            comm7.Parameters.AddWithValue("IdOrder", idOrder);
            comm7.Parameters.AddWithValue("Amount", productWarehouseRegister.Amount);
            comm7.Parameters.AddWithValue("Price",price);
            comm7.Parameters.AddWithValue("CreatedAt", productWarehouseRegister.CreatedAt);
            await comm7.ExecuteNonQueryAsync();

            sql = "SELECT IdProductWarehouse FROM Product_Warehouse WHERE CreatedAt = @createdAt ";
            await using SqlCommand comm8 = new(sql, connection);
            comm8.Parameters.AddWithValue("createdAt", productWarehouseRegister.CreatedAt);
            int result = (int) await comm8.ExecuteScalarAsync();

            return result;
        }
        public async Task<int> RegisterProductProcedure(ProductWarehouseRegister productWarehouseRegister)
        {
            await using SqlConnection con = new (ConnString);
            await using SqlCommand com = new ("AddProductToWarehouse", con);
            
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("IdWarehouse", productWarehouseRegister.IdWarehouse);
            com.Parameters.AddWithValue("IdProduct", productWarehouseRegister.IdProduct);
            com.Parameters.AddWithValue("Amount", productWarehouseRegister.Amount);
            com.Parameters.AddWithValue("CreatedAt", productWarehouseRegister.CreatedAt);

            await con.OpenAsync();
            int result=-1;
            using (var dr = await com.ExecuteReaderAsync())
            {
                while (await dr.ReadAsync())
                {
                    result = (int)dr.GetDecimal(0);
                }
            }
            return result;
        }
    }
}
