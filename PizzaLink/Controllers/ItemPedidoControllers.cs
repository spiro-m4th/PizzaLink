using PizzaLink.Models;
using PizzaLink.Services;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PizzaLink.Controllers
{
    public class ItemPedidoController
    {
        DataBaseSqlServer dataBase = new DataBaseSqlServer();

        ProdutoController produtoController = new ProdutoController();

        public int Inserir(ItemPedido itemPedido)
        {
            string query =
                "INSERT INTO ItemPedido (PedidoId, ProdutoId, Quantidade, PrecoUnitario) " +
                "VALUES (@PedidoId, @ProdutoId, @Quantidade, @PrecoUnitario)";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@PedidoId", itemPedido.PedidoId);
            command.Parameters.AddWithValue("@ProdutoId", itemPedido.Produto.ProdutoId);
            command.Parameters.AddWithValue("@Quantidade", itemPedido.Quantidade);
            command.Parameters.AddWithValue("@PrecoUnitario", itemPedido.PrecoUnitario);

            return dataBase.ExecuteSQL(command);
        }
        public int ExcluirPorPedidoId(int pedidoId)
        {
            string query =
                "DELETE FROM ItemPedido " +
                "WHERE PedidoId = @PedidoId";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@PedidoId", pedidoId);

            return dataBase.ExecuteSQL(command);
        }
        private ItemPedidoCollection GetByFilter(string filtro = "")
        {
            string query = "SELECT * FROM ItemPedido ";

            if (filtro != "")
                query += " WHERE " + filtro;

            SqlCommand command = new SqlCommand(query);

            DataTable dataTable = dataBase.GetDataTable(command);

            ItemPedidoCollection itensPedido = new ItemPedidoCollection();

            foreach (DataRow row in dataTable.Rows)
            {
                ItemPedido item = new ItemPedido();

                item.ItemPedidoId = (int)row["ItemPedidoId"];
                item.PedidoId = (int)row["PedidoId"]; 
                item.Quantidade = (int)row["Quantidade"];
                item.PrecoUnitario = (decimal)row["PrecoUnitario"];

                int produtoId = (int)row["ProdutoId"];
                item.Produto = produtoController.GetById(produtoId);

                itensPedido.Add(item);
            }
            return itensPedido;
        }
        public ItemPedidoCollection GetByPedidoId(int pedidoId)
        {
            return GetByFilter("PedidoId = " + pedidoId);
        }

        //metodo necessario na logica de frmNovoPedido
        public int ExcluirPorItemId(int itemPedidoId)
        {
            string query = "DELETE FROM ItemPedido WHERE ItemPedidoId = @ItemPedidoId";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@ItemPedidoId", itemPedidoId);
            return dataBase.ExecuteSQL(command);
        }
    }
}