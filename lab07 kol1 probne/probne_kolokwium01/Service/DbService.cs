using probne_kolokwium01.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace probne_kolokwium01.Service
{
    public class DbService : IDbService
    {
        string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog = prescription; Integrated Security = True";
        async Task IDbService.AddMedicamentToPrescription(List<Prescription_Medicament> list)
        {
            await using SqlConnection connection = new(connString);
            await connection.OpenAsync();
            foreach(Prescription_Medicament p in list)
            {
                string sql = "SELECT Count(1) FROM Prescription WHERE IdPrescription = @IDP";
                await using SqlCommand command = new(sql,connection);
                command.Parameters.AddWithValue("IDP", p.IdPrescription);
                if((int) await command.ExecuteScalarAsync() < 1)
                {
                    //404 nie ma recepty
                    throw new Exception("nie ma takiej recepty");
                }
                sql = "SELECT Count(1) FROM Medicament WHERE IdMedicament = @IDM";
                await using SqlCommand command2 = new(sql,connection);
                command2.Parameters.AddWithValue("IDM", p.IdMedicament);
                if((int) await command2.ExecuteScalarAsync() < 1)
                {
                    //404 nie ma leku
                    throw new Exception("nie ma takiego leku");
                }
                sql = "SELECT Count(1) FROM Prescription_Medicament WHERE IdMedicament = @IDM AND IdPrescription = @IDP";
                await using SqlCommand command3 = new(sql,connection);
                command3.Parameters.AddWithValue("IDM", p.IdPrescription);
                command3.Parameters.AddWithValue("IDP", p.IdMedicament);
                if((int) await command3.ExecuteScalarAsync() > 0)
                {
                    continue;
                }
                sql = "INSERT INTO Prescription_Medicament (IdMedicament, IdPrescription, Dose, Details) VALUES (@IDM, @IDP, @DOSE, @DETS)";
                await using SqlCommand command4 = new(sql,connection);
                command4.Parameters.AddWithValue("IDM", p.IdPrescription);
                command4.Parameters.AddWithValue("IDP", p.IdMedicament);
                command4.Parameters.AddWithValue("DOSE", p.Dose);
                command4.Parameters.AddWithValue("DETS", p.Details);

                await command4.ExecuteNonQueryAsync();
            }
            await connection.CloseAsync();
        }

        async Task<List<Prescription>> IDbService.GetAllPerscriptions()
        {
            string sql = "SELECT * FROM Prescription ORDER BY Date DESC";
            await using SqlConnection connection = new(connString);
            await using SqlCommand command = new(sql, connection);
            await connection.OpenAsync();

            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            List<Prescription> result = new List<Prescription>();

            while (await reader.ReadAsync())
            {
                result.Add(new Prescription
                {
                    IdPrescription = int.Parse(reader["IdPrescription"].ToString()),
                    Date = reader["Date"].ToString(),
                    DueDate = reader["DueDate"].ToString(),
                    IdPatient = int.Parse(reader["IdPatient"].ToString()),
                    IdDoctor = int.Parse(reader["IdDoctor"].ToString())

                });
            }

            await connection.CloseAsync();

            return result;
        }

        async Task<List<Prescription>> IDbService.GetPerscriptions(string name)
        {
            string sql = "SELECT * FROM Prescription WHERE IdPatient =" +
                " (SELECT IdPatient FROM Patient WHERE LastName = @name) ORDER BY Date DESC ";
            await using SqlConnection connection = new(connString);
            await using SqlCommand command = new(sql,connection);
            command.Parameters.AddWithValue("name", name);
            await connection.OpenAsync();

            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            List<Prescription> result = new List<Prescription>();

            while(await reader.ReadAsync())
            {
                result.Add(new Prescription
                {
                    IdPrescription = int.Parse(reader["IdPrescription"].ToString()),
                    Date = reader["Date"].ToString(),
                    DueDate = reader["DueDate"].ToString(),
                    IdPatient = int.Parse(reader["IdPatient"].ToString()),
                    IdDoctor = int.Parse(reader["IdDoctor"].ToString())

                });
            }

            await connection.CloseAsync();

            return result;
        }
    }
}
