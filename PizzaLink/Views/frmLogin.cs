using PizzaLink.Controllers;
using PizzaLink.Models;
using System;
using System.Windows.Forms;

namespace PizzaLink.Views
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Por favor, preencha o login e a senha.", "Campos invalidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UsuarioController controller = new UsuarioController();
            Usuario usuario = controller.ValidarLogin(txtLogin.Text, txtSenha.Text);

            if (usuario != null)
            {
                // DEU BOM
                this.Hide();

                frmPrincipal principal = new frmPrincipal();
                principal.ShowDialog();

                // quando o frmPrincipal fechar, o programa continua daqui
                this.Close();
            }
            else
            {
                // DEU RUIM
                MessageBox.Show("Usuário ou senha inválidos.", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Clear();
                txtLogin.Focus();
            }
        }

        private void lnkCadastreSe_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // passar 0 como parametro para indicar que é um novo cadastro
            frmCadastroUsuario frmCadastro = new frmCadastroUsuario(0);

            this.Hide();
            frmCadastro.ShowDialog();
            this.Show(); 
        }
    }
}