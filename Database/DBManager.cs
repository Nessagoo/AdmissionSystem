using Npgsql;
using System;
using System.Configuration;

namespace AdmissionSystem.Database
{
    public static class DBManager
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQL"].ConnectionString;

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }

        public static void TestConnection()
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Successfully connected to PostgreSQL database");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to database: {ex.Message}");
                    throw;
                }
            }
        }
    }
}