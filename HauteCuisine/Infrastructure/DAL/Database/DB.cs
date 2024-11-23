using HauteCuisine.DAL.OM;
using Npgsql;
using System.Data;

namespace HauteCuisine.Infrastructure.DAL.Database
{
    public class DB
    {
        const string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123";

        public DataTable ExecuteScript(string script)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string sql = string.Format(script);
            using var cmd = new NpgsqlCommand(sql, connection);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(rdr);

            connection.Close();

            return dt;
        }

        public DataTable SelectData(string fileName)
        {
            string script = File.ReadAllText($@"C:\Users\kuzne\source\repos\HauteCuisine\HauteCuisine\DAL\Script\{fileName}.sql");
            return ExecuteScript(script);
        }

        public void InsertData(object data)
        {
            var script = new QueryOperation();
            string insertSqlQuery = script.InsertSqlQuery(data);

            InsertRow(insertSqlQuery);
        }

        private void InsertRow(string insertIntoData)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                connection.Open();

                using var cmd = new NpgsqlCommand(insertIntoData, connection);
                var cnt = cmd.ExecuteNonQuery().ToString();

                Console.WriteLine("Данные успешно добавлены");

            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
