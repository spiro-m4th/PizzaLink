using PizzaLink.Models;
using PizzaLink.Services;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PizzaLink.Controllers
{
    public class UsuarioController
    {
        //Instanciar a classe de conexão com o BD
        DataBaseSqlServer dataBase = new DataBaseSqlServer();

        //Método que insere na tabela Usuario
        public int Inserir(Usuario usuario)
        {
            //Comando no SQLServer para inserir
            string query =
                "INSERT INTO Usuario (Nome, Login, Senha, NivelAcesso) " +
                "VALUES (@Nome, @Login, @Senha, @NivelAcesso)";

            SqlCommand command = new SqlCommand(query);

            //Definir os valores dos parametros
            command.Parameters.AddWithValue("@Nome", usuario.Nome);
            command.Parameters.AddWithValue("@Login", usuario.Login);
            command.Parameters.AddWithValue("@Senha", usuario.Senha);
            command.Parameters.AddWithValue("@NivelAcesso", usuario.NivelAcesso);

            //Executar o comando SQL e retornar
            //a quantidade de affected rows
            return dataBase.ExecuteSQL(command);
        }

        //Método para alterar o registro
        //Segue o mesmo esquema do método Inserir
        //Define o comando, os parametros e executa retornando as linhas afetadas
        public int Alterar(Usuario usuario)
        {
            string query =
                "UPDATE Usuario SET " +
                "Nome = @Nome, " +
                "Login = @Login, " +
                "Senha = @Senha, " +
                "NivelAcesso = @NivelAcesso " +
                "WHERE UsuarioId = @UsuarioId";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@Nome", usuario.Nome);
            command.Parameters.AddWithValue("@Login", usuario.Login);
            command.Parameters.AddWithValue("@Senha", usuario.Senha);
            command.Parameters.AddWithValue("@NivelAcesso", usuario.NivelAcesso);
            command.Parameters.AddWithValue("@UsuarioId", usuario.UsuarioId);

            return dataBase.ExecuteSQL(command);
        }

        //Método para excluir um registro
        //Segue o mesmo esquema dos outros métodos
        public int Excluir(int usuarioId)
        {
            string query =
                "DELETE FROM Usuario " +
                "WHERE UsuarioId = @UsuarioId";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@UsuarioId", usuarioId);

            return dataBase.ExecuteSQL(command);
        }

        //Método publico que retorna um objeto do tipo Usuario
        public Usuario GetById(int usuarioId)
        {
            string query =
                "SELECT * " +
                "FROM Usuario " +
                "WHERE UsuarioId = @UsuarioId " +
                "ORDER BY Nome";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@UsuarioId", usuarioId);

            //Executar o comando SQL e armazenar o resultado em um DataTable

            DataTable dataTable = dataBase.GetDataTable(command);

            if (dataTable.Rows.Count > 0)
            {
                //Criar um novo objeto Usuario
                Usuario usuario = new Usuario();

                //Conveter dados do SQL Server para C# usando Casting Direto
                usuario.UsuarioId = (int)dataTable.Rows[0]["UsuarioId"];
                usuario.Nome = (string)dataTable.Rows[0]["Nome"];
                usuario.Login = (string)dataTable.Rows[0]["Login"];
                //Por boas práticas e segurança, não retornar a senha
                usuario.NivelAcesso = Convert.ToChar(dataTable.Rows[0]["NivelAcesso"]);

                return usuario;
            }
            else
                return null;
        }

        //Método que retorna a coleção de Usuarios com filtro
        public UsuarioCollection GetByFilter(string filtro = "")
        {
            //Comando SQL para selecionar
            string query = "SELECT * FROM Usuario ";

            //Validar se o filtro foi passado no parametro
            if (filtro != "")
                query += " WHERE " + filtro;

            query += " ORDER BY Nome";

            SqlCommand command = new SqlCommand(query);

            //Executar o comando SQL e armazenar o resultado
            DataTable dataTable = dataBase.GetDataTable(command);

            //Criar um novo objeto UsuarioColletion
            UsuarioCollection usuarios = new UsuarioCollection();

            //Percorrer todas as linhas retornadas no DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                Usuario usuario = new Usuario();

                usuario.UsuarioId = (int)row["UsuarioId"];
                usuario.Nome = (string)row["Nome"];
                usuario.Login = (string)row["Login"];
                usuario.NivelAcesso = Convert.ToChar(row["NivelAcesso"]);

                //Adicionar o objeto usuario na coleção
                usuarios.Add(usuario);
            }
            return usuarios;
        }

        //Método que retorna uma coleção de Usuarios
        public UsuarioCollection GetAll()
        {
            return GetByFilter();
        }

        // Método para validar o login
        public Usuario ValidarLogin(string login, string senha)
        {
            //Comando para Selecionar o usuario com login e senha que foram informados
            string query =
                "SELECT * " +
                "FROM Usuario " +
                "WHERE Login = @Login AND Senha = @Senha";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Login", login);
            command.Parameters.AddWithValue("@Senha", senha);

            DataTable dataTable = dataBase.GetDataTable(command);

            if (dataTable.Rows.Count > 0)
            {
                //Se encontrou, retorna o objeto Usuario completo
                int usuarioId = (int)dataTable.Rows[0]["UsuarioId"];
                return GetById(usuarioId);
            }
            else
                return null;
        }
    }
}