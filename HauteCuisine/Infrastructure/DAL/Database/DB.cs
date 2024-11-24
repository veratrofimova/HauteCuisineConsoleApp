using HauteCuisine.BLL.Observer;
using Npgsql;
using System.Data;

namespace HauteCuisine.Infrastructure.DAL.Database
{
    public class DB
    {
        const string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123";

        public DataTable ExecuteScript(string script)
        {
            DataTable dt = new DataTable();
            var connection = new NpgsqlConnection(connectionString);
            try
            {
                connection.Open();

                string sql = string.Format(script);
                using var cmd = new NpgsqlCommand(sql, connection);
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);

                return dt;
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable SelectData(string fileName)
        {
            string script = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, $@"..\..\..\DAL\Script\{fileName}.sql"));
            return ExecuteScript(script);
        }

        public void InsertData(object data)
        {
            QueryOperation queryOperation = new QueryOperation();
            EventSubscriber eventSubscriber = new EventSubscriber();
            eventSubscriber.SubcriberTo(queryOperation);

            string insertSqlQuery = queryOperation.InsertSqlQuery(data);

            InsertRow(insertSqlQuery);
        }

        private void InsertRow(string insertIntoData)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                connection.Open();

                using var cmd = new NpgsqlCommand(insertIntoData, connection);
                {
                    var cnt = cmd.ExecuteNonQuery().ToString();
                }

                Console.WriteLine("Данные успешно добавлены");
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
