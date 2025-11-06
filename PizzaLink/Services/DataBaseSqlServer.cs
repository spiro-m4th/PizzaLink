using System.Data;
using System.Data.SqlClient;


namespace PizzaLink.Services
{
    public class DataBaseSqlServer
    {
        //String de conexão com o banco de dados SQL Server
        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection();

            connection.ConnectionString =
                "Data Source=.\\SQLEXPRESS;" +
                "Initial Catalog=PizzaLinkMVC;" +
                "Integrated Security=SSPI;";

            connection.Open();
            return connection;
        }

        //Executa comandos SQL que não retornam dados - INSERT DELETE e UPDATE
        public int ExecuteSQL(SqlCommand command)
        {
            command.Connection = GetConnection();
            //command.CommandType = System.Data.CommandType.Text;
            return command.ExecuteNonQuery();
        }

        //Executa comandos SQL que retornam um único valor - SELECT
        public object ExecuteScalar (SqlCommand command)
        {
            command.Connection = GetConnection();
            return command.ExecuteScalar();
        }

        //Executa comandos SQL que retornam um conjunto de dados - SELECT
        public DataTable GetDataTable (SqlCommand command)
        {
            DataTable dataTable = new DataTable();

            command.Connection = GetConnection();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);

            return dataTable;
        }
    }
}
