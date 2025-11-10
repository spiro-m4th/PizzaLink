using System;
using PizzaLink.Views;
using System.Windows.Forms;

namespace PizzaLink
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmPrincipal());    //iniciar o app na tela de Login
        }
    }
}
