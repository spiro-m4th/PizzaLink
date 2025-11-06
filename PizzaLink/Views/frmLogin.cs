using PizzaLink.Controllers;
using PizzaLink.Models;
using PizzaLink.Services;
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
        // Métodod para btnEntrar e btnSair
        private void btnSair_Click(object sender, EventArgs e)
        {
            // Fecha a aplicação inteira
            Application.Exit();
        }
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            // Validação dos campos
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos corretamente.", "Campos Vazios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chamar o controller
            UsuarioController controller = new UsuarioController();
            Usuario usuario = controller.ValidarLogin(txtLogin.Text, txtSenha.Text);

            // Verificar resultado
            if (usuario != null)
            {
                // se DEU CERTO
                // salva o usuário na sessão global
                SessaoUsuario.Login(usuario);

                //Esconde a tela de login
                this.Hide();

                // Criar e mostrar o form principal
                // ShowDialog() para que o código pare aqui
                // até o FormPrincipal ser fechado.

                frmPrincipal frmPrincipal = new frmPrincipal();
                frmPrincipal.ShowDialog();

                // Quando o usuário fechar o frmPrincipal,
                // o código continua daqui e fecha a tela de Login.
                this.Close();
            }
            else
            {
                // DEU RUIM
                MessageBox.Show("Usuário/ senha inválidos.", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Clear();
                txtLogin.Focus();
            }
        }

        private void lnkCadastreSe_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Criar o formulário de cadastro de usuário
            // passar '0' no parametro para indicar que é um cadastro "novo".
            frmCadastroUsuario frmCadastro = new frmCadastroUsuario(0);

            // Esconder, temporariamente, a tela de login
            this.Hide();

            // Exibir tela de Cadastro
            frmCadastro.ShowDialog();

            // Após fechar o cadastro (salvando ou cancelando),
            //a tela de login é mostrada novamente.
            this.Show();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
