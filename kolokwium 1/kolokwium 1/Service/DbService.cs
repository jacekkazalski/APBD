using kolokwium_1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium_1.Service
{
    public class DbService : IDbService
    {
        String connString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=teams;Integrated Security=True";

        async Task IDbService.AddPlayerToTeam(PlayerTeam pt)
        {
            await using SqlConnection connection = new(connString);
            String sql = "SELECT Count(1) FROM Player WHERE IdPlayer = @IDP";
            await using SqlCommand comm = new(sql, connection);
            comm.Parameters.AddWithValue("IDP", pt.IdPlayer);
            await connection.OpenAsync();
            if((int) await comm.ExecuteScalarAsync() < 1)
            {
                throw new Exception("gracz nie istnieje");
            }
            sql = "SELECT Count(1) FROM Team WHERE IdTeam = @IDT";
            await using SqlCommand comm2 = new(sql, connection);
            comm2.Parameters.AddWithValue("IDT", pt.IdTeam);
            if((int) await comm2.ExecuteScalarAsync() < 1)
            {
                throw new Exception("druzyna nie istnieje");
            }

            sql = "SELECT Count(1) FROM Team WHERE IdTeam = @IDT" +
                " AND MaxAge >(SELECT DATEDIFF(hour,dateofbirth,GETDATE())/8766 AS AgeYearsIntTrunc FROM Player WHERE IdPlayer = @IDP)";
            await using SqlCommand comm3 = new(sql, connection);
            comm3.Parameters.AddWithValue("IDP", pt.IdPlayer);
            comm3.Parameters.AddWithValue("IDT", pt.IdTeam);
            if((int) await comm3.ExecuteScalarAsync() < 1)
            {
                throw new Exception("gracz jest za stary");
            }


            sql = "INSERT INTO Player_Team VALUES (@IDP,@IDT,@NUM,@DESCR)";
            await using SqlCommand comm4 = new(sql, connection);
            comm4.Parameters.AddWithValue("IDP", pt.IdPlayer);
            comm4.Parameters.AddWithValue("IDT", pt.IdTeam);
            comm4.Parameters.AddWithValue("NUM", pt.NumOnShirt);
            comm4.Parameters.AddWithValue("DESCR", pt.Comment);

            await comm4.ExecuteNonQueryAsync();
        }

        async Task<List<Team>> IDbService.GetTeamInChampioshipAsync(int idChampionship)
        {
            await using SqlConnection connection = new(connString);
            String sql = "SELECT Count(1) FROM Championship WHERE IdChampionship = @IDCH";
           
            await using SqlCommand comm = new(sql, connection);
            comm.Parameters.AddWithValue("IDCH", idChampionship);
            await connection.OpenAsync();
            if ((int) await comm.ExecuteScalarAsync() < 1)
            {
                throw new Exception("nie ma takich rozgrywek");
                
            }
            sql = "SELECT t.IdTeam, TeamName, MaxAge, Score FROM Team t JOIN Championship_Team ct ON t.IdTeam = ct.IdTeam WHERE ct.IdChampionship = @IDCH  ";
            await using SqlCommand comm2 = new(sql, connection);
            comm2.Parameters.AddWithValue("IDCH", idChampionship);
            

            await using SqlDataReader reader = await comm2.ExecuteReaderAsync();
            List<Team> result = new List<Team>();

            if (!reader.HasRows)
            {
                throw new Exception("brak druzyn w rozgrywkach");
            }
            while(await reader.ReadAsync())
            {
                result.Add(new Team
                {
                    IdTeam = int.Parse(reader["IdTeam"].ToString()),
                    TeamName = reader["TeamName"].ToString(),
                    MaxAge = int.Parse(reader["MaxAge"].ToString()),
                    Score = double.Parse(reader["Score"].ToString())
                });
            }

            await connection.CloseAsync();
            



            return result;
            
        }

    }
}
