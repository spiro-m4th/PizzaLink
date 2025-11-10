using PizzaLink.Models;
using PizzaLink.Services;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PizzaLink.Controllers
{
    public class PedidoController
    {
        DataBaseSqlServer dataBase = new DataBaseSqlServer();

        UsuarioController usuarioController = new UsuarioController();
        ClienteController clienteController = new ClienteController();
        ItemPedidoController itemPedidoController = new ItemPedidoController();

        public int Inserir(Pedido pedido)
        {
            string query =
                "INSERT INTO Pedido (UsuarioId, ClienteId, DataHora, ValorTotal, Status) " +
                "VALUES (@UsuarioId, @ClienteId, @DataHora, @ValorTotal, @Status);" +
                "SELECT SCOPE_IDENTITY();"; //pega o ID do pedido inserido

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@UsuarioId", pedido.Usuario.UsuarioId);
            command.Parameters.AddWithValue("@ClienteId", pedido.Cliente.ClienteId);
            command.Parameters.AddWithValue("@DataHora", pedido.DataHora);
            command.Parameters.AddWithValue("@ValorTotal", pedido.ValorTotal);
            command.Parameters.AddWithValue("@Status", pedido.Status);

            object retorno = dataBase.ExecuteScalar(command);

            int pedidoId = 0;
            if (retorno != null)
                pedidoId = Convert.ToInt32(retorno);
            else
                return 0; //se não retornar o ID é pq falhou

            //inserir os itens do pedido (detalhes)
            if (pedidoId > 0)
            {
                foreach (ItemPedido item in pedido.ItensDoPedido)
                {
                    item.PedidoId = pedidoId; //associa o item ao pedido principal
                    itemPedidoController.Inserir(item); //salva o item
                }
            }

            return pedidoId; //retorna o ID do peidido
        }

        public int Alterar(Pedido pedido)
        { 
            string query =
                "UPDATE Pedido SET " +
                "UsuarioId = @UsuarioId, " +
                "ClienteId = @ClienteId, " +
                "DataHora = @DataHora, " +
                "ValorTotal = @ValorTotal, " +
                "Status = @Status " +
                "WHERE PedidoId = @PedidoId";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@PedidoId", pedido.PedidoId);
            command.Parameters.AddWithValue("@UsuarioId", pedido.Usuario.UsuarioId);
            command.Parameters.AddWithValue("@ClienteId", pedido.Cliente.ClienteId);
            command.Parameters.AddWithValue("@DataHora", pedido.DataHora);
            command.Parameters.AddWithValue("@ValorTotal", pedido.ValorTotal);
            command.Parameters.AddWithValue("@Status", pedido.Status);

            return dataBase.ExecuteSQL(command);
        }
        public int AlterarStatus(int pedidoId, char status)
        {
            string query =
                "UPDATE Pedido SET " +
                "Status = @Status " +
                "WHERE PedidoId = @PedidoId";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@PedidoId", pedidoId);
            command.Parameters.AddWithValue("@Status", status);
            return dataBase.ExecuteSQL(command);
        }
        public int Excluir(int pedidoId)
        {
            itemPedidoController.ExcluirPorPedidoId(pedidoId);

            string query = "DELETE FROM Pedido WHERE PedidoId = @PedidoId";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@PedidoId", pedidoId);

            return dataBase.ExecuteSQL(command);
        }
        public Pedido GetById(int pedidoId)
        {
            string query = "SELECT * FROM Pedido WHERE PedidoId = @PedidoId";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@PedidoId", pedidoId);

            DataTable dataTable = dataBase.GetDataTable(command);

            if (dataTable.Rows.Count > 0)
            {
                Pedido pedido = new Pedido();

                pedido.PedidoId = (int)dataTable.Rows[0]["PedidoId"];
                pedido.DataHora = (DateTime)dataTable.Rows[0]["DataHora"];
                pedido.ValorTotal = (decimal)dataTable.Rows[0]["ValorTotal"];
                pedido.Status = Convert.ToChar(dataTable.Rows[0]["Status"]);

                int usuarioId = (int)dataTable.Rows[0]["UsuarioId"];
                pedido.Usuario = usuarioController.GetById(usuarioId);

                int clienteId = (int)dataTable.Rows[0]["ClienteId"];
                pedido.Cliente = clienteController.GetById(clienteId);

                pedido.ItensDoPedido = itemPedidoController.GetByPedidoId(pedidoId);

                return pedido;
            }
            else
                return null;
        }
        private PedidoCollection GetByFilter(string filtro = "")
        {
            string query = "SELECT * FROM Pedido ";

            if (filtro != "")
                query += " WHERE " + filtro;

            query += " ORDER BY DataHora DESC"; //Em ordem decrescente, ou seja, do mais recente pro mais antigo

            SqlCommand command = new SqlCommand(query);

            DataTable dataTable = dataBase.GetDataTable(command);

            PedidoCollection pedidos = new PedidoCollection();

            //Percorrer todas as linhas
            foreach (DataRow row in dataTable.Rows)
            {
                Pedido pedido = new Pedido();

                pedido.PedidoId = (int)row["PedidoId"];
                pedido.DataHora = (DateTime)row["DataHora"];
                pedido.ValorTotal = (decimal)row["ValorTotal"];
                pedido.Status = Convert.ToChar(row["Status"]);

                int usuarioId = (int)row["UsuarioId"];
                pedido.Usuario = usuarioController.GetById(usuarioId);

                int clienteId = (int)row["ClienteId"];
                pedido.Cliente = clienteController.GetById(clienteId);

                pedidos.Add(pedido);
            }
            return pedidos;
        }
        public PedidoCollection GetAll()
        {
            return GetByFilter();
        }
        public PedidoCollection GetByPeriodo(DateTime dtInicial, DateTime dtFinal)
        {
            DateTime dtInicialZeroHoras = dtInicial.Date;
            DateTime dtFinalUltimaHora = new DateTime(dtFinal.Year, dtFinal.Month, dtFinal.Day, 23, 59, 59);

            string dtIniStr = dtInicialZeroHoras.ToString("yyyy-MM-dd HH:mm:ss");
            string dtFinStr = dtFinalUltimaHora.ToString("yyyy-MM-dd HH:mm:ss");

            string where =
                "DataHora " +
                "BETWEEN '" + dtIniStr + "' " +
                "AND '" + dtFinStr + "' ";

            return GetByFilter(where);
        }

        //Outros filtros
        public PedidoCollection GetByCliente(int clienteId)
        {
            return GetByFilter("ClienteId = " + clienteId);
        }

        public PedidoCollection GetByStatus(char status)
        {
            return GetByFilter("Status = '" + status + "'");
        }
    }
}