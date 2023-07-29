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

        //Clients
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

        //Projects
        public Project Insert(Project p)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var sql = $"InsertProject";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("longname", p.LongName));
                        cmd.Parameters.Add(new SqlParameter("clientid", p.ClientId));
                        conn.Open();
                        var Id = (int)cmd.ExecuteScalar();
                        p.Id = Id;
                    }
                }
            }
            catch (Exception)
            {
                return p;
            }
            return p;
        }

        public List<Project> GetProject()
        {
            var results = new List<Project>();
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = "select id, longname, clientid from Projects";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        results.Add(new Project
                        {
                            Id = (int)reader[0],
                            LongName = reader[1].ToString() ?? string.Empty,
                            ClientId = (int)reader[2]
                        });

                    }
                }
            }
            return results;
        }

        public void DeleteProject(int c)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var sql = "DeleteProject";
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
        public Project UpdateProject(Project p)
        {
            //int active = p.IsActive ? 0 : 1;
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    var sql = $"UpdateProject";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("longname", p.LongName));
                        cmd.Parameters.Add(new SqlParameter("id", p.Id));
              //          cmd.Parameters.Add(new SqlParameter("isactive", active));
                        conn.Open();
                        cmd.ExecuteReader();
                    }

                }
            }
            catch (Exception)
            {
                return p;
            }
            return p;
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