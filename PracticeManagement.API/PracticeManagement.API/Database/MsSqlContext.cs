using Microsoft.Data.SqlClient;
using PracticeManagement.CLI.Models;

namespace PracticeManagement.API.Database
{
    public class MsSqlContext
    {
        private MsSqlContext()
        {
            connectionString = "Server=DESKTOP-CNH6L6U;Database=LegalEase_DB;Trusted_Connection=True;TrustServerCertificate=True";
        }
        private string connectionString;

        public Client Insert(Client c)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var sql = $"InsertClient";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("name", c.Name));
                        conn.Open();
                        var Id = (int)cmd.ExecuteScalar();
                        c.Id = Id;
                    }
                }
            }
            catch (Exception)
            {
                return c;
            }
            return c;
        }

        public void Delete(int c)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var sql = "DeleteClient";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@id", c));
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public Client Update(Client c)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var sql = $"UpdateClient";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("name", c.Name));
                        cmd.Parameters.Add(new SqlParameter("id", c.Id));
                        conn.Open();
                        cmd.ExecuteReader();
                    }

                }
            }
            catch (Exception)
            {
                return c;
            }
            return c;
        }

        public List<Client> GetClient()
        {
            var results = new List<Client>();
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = "select id, name from Clients";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        results.Add(new Client
                        {
                            Id = (int)reader[0],
                            Name = reader[1].ToString() ?? string.Empty
                        });

                    }
                }
            }
            return results;
        }


        private static MsSqlContext? Instance;
        public static MsSqlContext Current
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new MsSqlContext();
                }
                return Instance;
            }
        }
    }
}