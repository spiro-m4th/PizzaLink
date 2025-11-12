using PizzaLink.Models;
using PizzaLink.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PizzaLink.Controllers
{
    public class UsuarioController
    {
        //instanciar a classe de conexão com o BD
        DataBaseSqlServer dataBase = new DataBaseSqlServer();

        //método que insere na tabela Usuario
        public int Inserir(Usuario usuario)
        {
            //comando no SQLServer para inserir
            string query =
                "INSERT INTO Usuario (Nome, Login, Senha, NivelAcesso) " +
                "VALUES (@Nome, @Login, @Senha, @NivelAcesso)";

            SqlCommand command = new SqlCommand(query);

            //definir os valores dos parametros
            command.Parameters.AddWithValue("@Nome", usuario.Nome);
            command.Parameters.AddWithValue("@Login", usuario.Login);
            command.Parameters.AddWithValue("@Senha", usuario.Senha);
            command.Parameters.AddWithValue("@NivelAcesso", usuario.NivelAcesso);

            //executar o comando SQL e retornar
            //a quantidade de affected rows
            return dataBase.ExecuteSQL(command);
        }

        //método para alterar o registro
        //segue o mesmo esquema do método Inserir
        //define o comando, os parametros e executa retornando as linhas afetadas
        public int Alterar(Usuario usuario)
        {
            //modo alterar nao deve mexer na senha
            //a nao ser que seja implementado um sistema de administracao
            //mais robusto, com permissoes de tipos de usuario (isto ja foi implementado
            //neste programa, so faltou implementar mais funcionalidades para que apenas
            //administradores possam alterar senhas e usuarios (funcionario) comuns nao possam alterar
            //os usuarios)
            string query =
                "UPDATE Usuario SET " +
                "Nome = @Nome, " +
                "Login = @Login, " +
                "NivelAcesso = @NivelAcesso " +
                "WHERE UsuarioId = @UsuarioId";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@Nome", usuario.Nome);
            command.Parameters.AddWithValue("@Login", usuario.Login);
            command.Parameters.AddWithValue("@NivelAcesso", usuario.NivelAcesso);
            command.Parameters.AddWithValue("@UsuarioId", usuario.UsuarioId);

            return dataBase.ExecuteSQL(command);
        }

        //método para excluir um registro
        //segue o mesmo esquema dos outros métodos
        public int Excluir(int usuarioId)
        {
            string query =
                "DELETE FROM Usuario " +
                "WHERE UsuarioId = @UsuarioId";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@UsuarioId", usuarioId);

            return dataBase.ExecuteSQL(command);
        }

        //método publico que retorna um objeto do tipo Usuario
        public Usuario GetById(int usuarioId)
        {
            string query =
                "SELECT * " +
                "FROM Usuario " +
                "WHERE UsuarioId = @UsuarioId " +
                "ORDER BY Nome";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@UsuarioId", usuarioId);

            //executar o comando SQL e armazenar o resultado em um DataTable

            DataTable dataTable = dataBase.GetDataTable(command);

            if (dataTable.Rows.Count > 0)
            {
                //criar um novo objeto Usuario
                Usuario usuario = new Usuario();

                //conveter dados do SQL Server para C# usando Casting Direto
                usuario.UsuarioId = (int)dataTable.Rows[0]["UsuarioId"];
                usuario.Nome = (string)dataTable.Rows[0]["Nome"];
                usuario.Login = (string)dataTable.Rows[0]["Login"];
                //por boas práticas e segurança, não retornar a senha              
                usuario.NivelAcesso = Convert.ToChar(dataTable.Rows[0]["NivelAcesso"]);

                return usuario;
            }
            else
                return null;
        }

        //método que retorna a coleção de Usuarios com filtro
        public UsuarioCollection GetByFilter(string filtro = "")
        {
            //comando SQL para selecionar
            string query = "SELECT * FROM Usuario ";

            //validar se o filtro foi passado no parametro
            if (filtro != "")
                query += " WHERE " + filtro;

            query += " ORDER BY Nome";

            SqlCommand command = new SqlCommand(query);

            //executar o comando SQL e armazenar o resultado
            DataTable dataTable = dataBase.GetDataTable(command);

            //criar um novo objeto UsuarioColletion
            UsuarioCollection usuarios = new UsuarioCollection();

            //percorrer todas as linhas retornadas no DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                Usuario usuario = new Usuario();

                usuario.UsuarioId = (int)row["UsuarioId"];
                usuario.Nome = (string)row["Nome"];
                usuario.Login = (string)row["Login"];
                usuario.NivelAcesso = Convert.ToChar(row["NivelAcesso"]);

                //adicionar o objeto usuario na coleção
                usuarios.Add(usuario);
            }
            return usuarios;
        }

        //método que retorna uma coleção de Usuarios
        public UsuarioCollection GetAll()
        {
            return GetByFilter();
        }

    }
}