using PizzaLink.Models;
using PizzaLink.Services;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PizzaLink.Controllers
{
    public class ClienteController
    {
        DataBaseSqlServer dataBase = new DataBaseSqlServer();
        public int Inserir(Cliente cliente)
        {
            string query =
                "INSERT INTO Cliente (Nome, Telefone, Cpf, Endereco) " +
                "VALUES (@Nome, @Telefone, @Cpf, @Endereco)";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@Nome", cliente.Nome);
            command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
            command.Parameters.AddWithValue("@Cpf", (object)cliente.Cpf ?? DBNull.Value);
            command.Parameters.AddWithValue("@Endereco", (object)cliente.Endereco ?? DBNull.Value);

            return dataBase.ExecuteSQL(command);
        }
        public int Alterar(Cliente cliente)
        {
            string query =
                "UPDATE Cliente SET " +
                "Nome = @Nome, " +
                "Telefone = @Telefone, " +
                "Cpf = @Cpf, " +
                "Endereco = @Endereco " +
                "WHERE ClienteId = @ClienteId";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@Nome", cliente.Nome);
            command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
            command.Parameters.AddWithValue("@Cpf", (object)cliente.Cpf ?? DBNull.Value);
            command.Parameters.AddWithValue("@Endereco", (object)cliente.Endereco ?? DBNull.Value);
            command.Parameters.AddWithValue("@ClienteId", cliente.ClienteId);

            return dataBase.ExecuteSQL(command);
        }
        public int Excluir(int clienteId)
        {
            string query =
                "DELETE FROM Cliente " +
                "WHERE ClienteId = @ClienteId";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@ClienteId", clienteId);

            return dataBase.ExecuteSQL(command);
        }
        public Cliente GetById(int clienteId)
        {
            string query =
                "SELECT * " +
                "FROM Cliente " +
                "WHERE ClienteId = @ClienteId";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@ClienteId", clienteId);

            DataTable dataTable = dataBase.GetDataTable(command);

            if (dataTable.Rows.Count > 0)
            {
                Cliente cliente = new Cliente();

                cliente.ClienteId = (int)dataTable.Rows[0]["ClienteId"];
                cliente.Nome = (string)dataTable.Rows[0]["Nome"];
                cliente.Telefone = (string)dataTable.Rows[0]["Telefone"];
                cliente.Cpf = dataTable.Rows[0]["Cpf"] == DBNull.Value ? null : (string)dataTable.Rows[0]["Cpf"];
                cliente.Endereco = dataTable.Rows[0]["Endereco"] == DBNull.Value ? null : (string)dataTable.Rows[0]["Endereco"];
                return cliente;
            }
            else
                return null;
        }
        public ClienteCollection GetByFilter(string filtro = "")
        {
            string query = "SELECT * FROM Cliente ";

            if (filtro != "")
                query += " WHERE " + filtro;

            query += " ORDER BY Nome";

            SqlCommand command = new SqlCommand(query);

            DataTable dataTable = dataBase.GetDataTable(command);

            ClienteCollection clientes = new ClienteCollection();

            foreach (DataRow row in dataTable.Rows)
            {
                Cliente cliente = new Cliente();

                cliente.ClienteId = (int)row["ClienteId"];
                cliente.Nome = (string)row["Nome"];
                cliente.Telefone = (string)row["Telefone"];
                cliente.Cpf = row["Cpf"] == DBNull.Value ? null : (string)row["Cpf"];
                cliente.Endereco = row["Endereco"] == DBNull.Value ? null : (string)row["Endereco"];

                clientes.Add(cliente);
            }
            return clientes;
        }
        public ClienteCollection GetAll()
        {
            return GetByFilter();
        }
        public ClienteCollection GetByNome(string value)
        {
            return GetByFilter("Nome LIKE '%" + value + "%'");
        }
        public Cliente GetByTelefone(string telefone)
        {
            var colecao = GetByFilter("Telefone = '" + telefone + "'");
            if (colecao.Count > 0)
                return colecao[0];
            else
                return null;
        }
    }
}