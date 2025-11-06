using System;
using System.Collections.Generic;

namespace PizzaLink.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public DateTime DataHora { get; set; }
        public Usuario Usuario { get; set; } // Relacionamento com Usuário
        public Cliente Cliente { get; set; } // Relacionamento com Cliente
        public decimal ValorTotal { get; set; }
        public char Status { get; set; }

        // Lista essencial para o Controller
        // Vai guardar os itens antes de salvar no banco
        public List<ItemPedido> ItensDoPedido { get; set; }

        //Inicializar a lista
        public Pedido()
        {
            this.ItensDoPedido = new List<ItemPedido>();
        }

        // Propriedade tratada
        public string StatusTratado
        {
            get
            {
                switch (Status)
                {
                    case 'P': return "Pendente";
                    case 'F': return "Finalizado";
                    case 'C': return "Cancelado";
                    default: return "Desconhecido";
                }
            }
        }
    }
    public class PedidoCollection : List<Pedido> { }
}