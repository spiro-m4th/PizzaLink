using System.Data;
using System.Data.SqlClient;


namespace PizzaLink.Services
{
    public class DataBaseSqlServer
    {
        //string de conexão com o banco de dados SQL Server

        //tive que fazer a conexao com o banco de outra forma, pois estava
        //tendo erros na hora de ler no dataGridView, nada estava sendo exibido.
        //com base em pesquisas, descobri que o SqlDataAdapter.Fill(dataTable) falha pelo
        //motivo de que o GetConnection sendo chamado usa uma conexao que se mantem aberta,
        //desta forma nao retorna nenhum erro na hora de testar
        //para isso sera necessario so criar uma conexao (e nao abri-la)
        //e posteriormente abrir e fechar a conexao a cada comando que for dado no programa
        //utilizar using garante que .Dispose e .Close sejam chamados

        private SqlConnection GetConnection()
        {
              string connectionString =
                @"Data Source=localhost\SQLEXPRESS;" +
                "Initial Catalog=PizzaLinkMVC;" +
                "Integrated Security=SSPI;";

            /* string connectionString =
                 @"Data Source=DESKTOP-MATH;" +
                 "Initial Catalog=PizzaLinkMVC;" +
                 "Integrated Security=SSPI;"; */

            /* string connectionString =
                @"Data Source=MATHEUS;" +
                "Initial Catalog=PizzaLinkMVC;" +
                "Integrated Security=SSPI;"; */

            return new SqlConnection(connectionString);
        }

        //executa comandos SQL que não retornam dados - INSERT DELETE e UPDATE
        public int ExecuteSQL(SqlCommand command)
        {
            using (SqlConnection connection = GetConnection())
            {
                command.Connection = connection;
                connection.Open();
                return command.ExecuteNonQuery();
                //aqui a conexao sera fechada
            }
        }

        //executa comandos SQL que retornam um único valor - SELECT
        public object ExecuteScalar (SqlCommand command)
        {
            using (SqlConnection connection = GetConnection())
            {
                command.Connection = connection;
                connection.Open();
                return command.ExecuteScalar();
            }
        }

        //executa comandos SQL que retornam um conjunto de dados - SELECT
        public DataTable GetDataTable (SqlCommand command)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = GetConnection())
            {                
                command.Connection = connection;
                connection.Open();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
        }
    }
}
