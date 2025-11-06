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

        //Traduzir ou interpretar o valor de outra propriedade chamada NivelAcesso (char string com 'A' e 'F'), retornando uma descrição mais facil de ler.
        public string NivelAcessoTratado
        {
            get
            {
                return (NivelAcesso == 'A') ? "Administrador" : "Funcionário";
            }
        }
    }

    //Classe de coleção com a lista de usuarios
    public class UsuarioCollection : List<Usuario> { }
}