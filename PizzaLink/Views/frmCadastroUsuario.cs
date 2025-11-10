using PizzaLink.Controllers;
using PizzaLink.Models;
using System;
using System.Windows.Forms;

namespace PizzaLink.Views
{
    public partial class frmCadastroUsuario : Form
    {
        //variavel para guardar o ID do usuario
        private int usuarioId = 0;
        UsuarioController usuarioController = new UsuarioController();

        //iniciar um construtor que aceita id como parametro
        public frmCadastroUsuario(int id)
        {
            InitializeComponent();
            this.usuarioId = id;
        }

        private void frmCadastroUsuario_Load(object sender, EventArgs e)
        {
            if (this.usuarioId != 0)
            {
                //tela de alteração de usuario
                this.Text = "Alterar Usuário";

                //carrega os dados do banco
                Usuario usuario = usuarioController.GetById(this.usuarioId);
                txtNome.Text = usuario.Nome;
                txtLogin.Text = usuario.Login;
                txtSenha.Text = "";
                txtSenha.Enabled = false; //desabilitar senha na alteração por boas praticas
            }
            else
            {
                //modo cadastrar usuario
                this.Text = "Novo Cadastro de Usuário";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //validacao de dados
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Nome e Login são obrigatórios.");
                return;
            }

            if (this.usuarioId == 0 && string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Senha é obrigatória para novos usuários.");
                return;
            }

            //preencher os models
            Usuario usuario = new Usuario();
            usuario.Nome = txtNome.Text;
            usuario.Login = txtLogin.Text;
            usuario.Senha = txtSenha.Text;

            try
            {
                //chamar o controller para modificar os dados
                if (this.usuarioId == 0)
                {
                    //insert
                    usuario.NivelAcesso = 'F';
                    usuarioController.Inserir(usuario);
                }
                else
                {
                    //update
                    usuario.UsuarioId = this.usuarioId;

                    Usuario usuarioExistente = usuarioController.GetById(this.usuarioId);
                    usuario.NivelAcesso = usuarioExistente.NivelAcesso;
                }

                MessageBox.Show("Usuário salvo com sucesso!", "SUCESSO", MessageBoxButtons.OK);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "ERRO", MessageBoxButtons.OK);
            }
        }
    }
}