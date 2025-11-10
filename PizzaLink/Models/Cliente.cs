using System.Collections.Generic;

namespace PizzaLink.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        public string Endereco { get; set; }
    }
    public class ClienteCollection : List<Cliente> 
    { 
    
    }
}