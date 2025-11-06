//Para poder implementar uma tela de Login, será necessário armazenar o usuário logado em algum lugar acessível.
//Para isso criei uma classe estática chamada SessaoUsuario em Services
//De modo que possa ser acessado em qualquer parte do projeto e armazene o usuário logado
using PizzaLink.Models;

namespace PizzaLink.Services
{
    public static class SessaoUsuario
    {
        public static Usuario UsuarioLogado { get; private set; }
        public static void Login(Usuario usuario)
        {
            UsuarioLogado = usuario;
        }
        public static void Logout()
        {
            UsuarioLogado = null;
        }
    }
}