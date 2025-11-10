using System.Collections.Generic;

namespace PizzaLink.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public char Tipo { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }

        //traduzir o que significa cada char
        public string TipoTratado
        {
            get
            {
                switch (Tipo)
                {
                    case 'P': return "Pizza";
                    case 'B': return "Bebida";
                    case 'L': return "Lanche";
                    default: return "Outro";
                }
            }
        }
    }
    public class ProdutoCollection : List<Produto> 
    { 
    
    }
}