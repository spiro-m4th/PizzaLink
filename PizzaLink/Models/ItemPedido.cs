using System.Collections.Generic;

namespace PizzaLink.Models
{
    public class ItemPedido
    {
        public int ItemPedidoId { get; set; }
        public int PedidoId { get; set; } 
        public Produto Produto { get; set; } //relacionamento com Produto
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; } //preço na hora da venda

        //cálculo do total (igual do banco de dados)
        public decimal Subtotal
        {
            get { return Quantidade * PrecoUnitario; }
        }
    }
    public class ItemPedidoCollection : List<ItemPedido>
    { 
    
    }
}