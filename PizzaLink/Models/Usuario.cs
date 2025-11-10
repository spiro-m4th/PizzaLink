using System.Collections.Generic;

namespace PizzaLink.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; } 
        public char NivelAcesso { get; set; }

        //traduzir o valor da propriedade NivelAcesso (char string com 'A' e 'F'), retornando uma descrição legível.
        public string NivelAcessoTratado
        {
            get
            {
                return (NivelAcesso == 'A') ? "Administrador" : "Funcionário";
            }
        }
    }

    //classe collection com a lista de usuarios
    public class UsuarioCollection : List<Usuario> 
    { 
    
    }
}