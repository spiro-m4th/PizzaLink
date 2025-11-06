//Seguir o mesmo esquema de UsuarioController.cs
//Onde tenho métodos para Inserir, Alterar, Excluir, GetById, GetByFilter, GetAll e GetByNome
//Além de usar métodos parecidos que incluem DataBaseSqlServer e SqlCommand
//E também usar ProdutoCollection similar a UsuarioCollection
using PizzaLink.Models;
using PizzaLink.Services;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PizzaLink.Controllers
{
    public class ProdutoController
    {
        DataBaseSqlServer dataBase = new DataBaseSqlServer();
        //Método para inserir na tabela Produtos
        public int Inserir(Produto produto)
        {
            //Comando SQL para inserir
            string query =
                "INSERT INTO Produto (Nome, Tipo, Preco, Estoque) " +
                "VALUES (@Nome, @Tipo, @Preco, @Estoque)";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@Nome", produto.Nome);
            command.Parameters.AddWithValue("@Tipo", produto.Tipo);
            command.Parameters.AddWithValue("@Preco", produto.Preco);
            command.Parameters.AddWithValue("@Estoque", produto.Estoque);

            return dataBase.ExecuteSQL(command);
        }

        public int Alterar(Produto produto)
        { 
            string query =
                "UPDATE Produto SET " +
                "Nome = @Nome, " +
                "Tipo = @Tipo, " +
                "Preco = @Preco, " +
                "Estoque = @Estoque " +
                "WHERE ProdutoId = @ProdutoId";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@Nome", produto.Nome);
            command.Parameters.AddWithValue("@Tipo", produto.Tipo);
            command.Parameters.AddWithValue("@Preco", produto.Preco);
            command.Parameters.AddWithValue("@Estoque", produto.Estoque);
            command.Parameters.AddWithValue("@ProdutoId", produto.ProdutoId);

            return dataBase.ExecuteSQL(command);
        }
        public int Excluir(int produtoId)
        { 
            string query =
                "DELETE FROM Produto " +
                "WHERE ProdutoId = @ProdutoId";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@ProdutoId", produtoId);

            return dataBase.ExecuteSQL(command);
        }
        public Produto GetById(int produtoId)
        {
            string query =
                "SELECT * " +
                "FROM Produto " +
                "WHERE ProdutoId = @ProdutoId";

            SqlCommand command = new SqlCommand(query);
            
            command.Parameters.AddWithValue("@ProdutoId", produtoId);

            DataTable dataTable = dataBase.GetDataTable(command);

            if (dataTable.Rows.Count > 0)
            {
                Produto produto = new Produto();
                
                produto.ProdutoId = (int)dataTable.Rows[0]["ProdutoId"];
                produto.Nome = (string)dataTable.Rows[0]["Nome"];
                produto.Tipo = Convert.ToChar(dataTable.Rows[0]["Tipo"]);
                produto.Preco = (decimal)dataTable.Rows[0]["Preco"];
                produto.Estoque = (int)dataTable.Rows[0]["Estoque"];

                return produto;
            }
            else
                return null;
        }
        public ProdutoCollection GetByFilter(string filtro = "")
        {
            string query = "SELECT * FROM Produto ";

            if (filtro != "")
                query += " WHERE " + filtro;

            query += " ORDER BY Nome";

            SqlCommand command = new SqlCommand(query);

            DataTable dataTable = dataBase.GetDataTable(command);

            ProdutoCollection produtos = new ProdutoCollection();

            foreach (DataRow row in dataTable.Rows)
            { 
                Produto produto = new Produto();

                produto.ProdutoId = (int)row["ProdutoId"];
                produto.Nome = (string)row["Nome"];
                produto.Tipo = Convert.ToChar(row["Tipo"]);
                produto.Preco = (decimal)row["Preco"];
                produto.Estoque = (int)row["Estoque"];

                produtos.Add(produto);
            }

            return produtos;
        }
        public ProdutoCollection GetAll()
        {
            return GetByFilter();
        }
        public ProdutoCollection GetByNome(string value)
        {
            return GetByFilter("Nome LIKE '%" + value + "%'");
        }
    }
}