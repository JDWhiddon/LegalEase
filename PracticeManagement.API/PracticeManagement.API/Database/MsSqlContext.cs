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

        public List<Client> Get()
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
