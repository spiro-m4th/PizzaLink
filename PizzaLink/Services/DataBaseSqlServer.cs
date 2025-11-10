using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace PizzaLink.Services
{
    public class DataBaseSqlServer
    {
        //string de conexão com o banco de dados SQL Server
        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection();

            connection.ConnectionString =
                @"Data Source=localhost\SQLEXPRESS;" +
                "Initial Catalog=PizzaLinkMVC;" +
                "Integrated Security=SSPI;";

            connection.Open();
            return connection;
        }

        //executa comandos SQL que não retornam dados - INSERT DELETE e UPDATE
        public int ExecuteSQL(SqlCommand command)
        {
            command.Connection = GetConnection();
            //command.CommandType = System.Data.CommandType.Text;
            return command.ExecuteNonQuery();
        }

        //executa comandos SQL que retornam um único valor - SELECT
        public object ExecuteScalar (SqlCommand command)
        {
            command.Connection = GetConnection();
            return command.ExecuteScalar();
        }

        //executa comandos SQL que retornam um conjunto de dados - SELECT
        public DataTable GetDataTable (SqlCommand command)
        {
            try                     //fui obrigado a colocar um try catch aqui, pq estava IMPOSSIVEL acessar o banco
            {
                DataTable dataTable = new DataTable();

                command.Connection = GetConnection();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);

                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no banco de dados: \n\n" + ex.Message, "ERRO", MessageBoxButtons.OK);
                return null;
            }
        }
    }
}
