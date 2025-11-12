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

        //variavel para armazenar que o usuario esta em edicao
        private Usuario usuarioEdicao;

        UsuarioController usuarioController = new UsuarioController();

        //iniciar um construtor que aceita id como parametro
        public frmCadastroUsuario(int id)
        {
            InitializeComponent();
            this.usuarioId = id;
        }

        private void frmCadastroUsuario_Load(object sender, EventArgs e)
        {
            //tela para alterar
            if (this.usuarioId != 0) 
            {
                this.Text = "Alterar Usuário";

                this.usuarioEdicao = usuarioController.GetById(this.usuarioId);

                if (this.usuarioEdicao == null)
                {
                    MessageBox.Show("Usuário não encontrado.", "Erro");
                    this.Close();
                    return;
                }
                txtNome.Text = usuarioEdicao.Nome;
                txtLogin.Text = usuarioEdicao.Login;
                txtSenha.Enabled = false;
                txtRepetirSenha.Enabled = false;
            }
            //novo usuario
            else            
            {
                this.Text = "Novo Cadastro de Usuário";
                this.usuarioEdicao = new Usuario();
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja Cancelar?", "CONFIRMAÇÃO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //validacoes
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Nome e Login são obrigatórios.");
                return;
            }

            usuarioEdicao.Nome = txtNome.Text.Trim();
            usuarioEdicao.Login = txtLogin.Text.Trim();

            //modo novo - ha validacao de senha
            if (this.usuarioId == 0)
            {
                if (txtSenha.TextLength < 6)
                {
                    MessageBox.Show("A senha deve ter no minímo 6 caracteres.", "ERRO");
                    return;
                }
                if (txtSenha.Text != txtRepetirSenha.Text)
                {
                    MessageBox.Show("As senhas não coincidem.", "ERRO");
                    return;
                }

                //define a senha 
                usuarioEdicao.Senha = txtSenha.Text;
                usuarioEdicao.NivelAcesso = 'F'; //nivel default de usuarios
            }

            try
            {
                if (this.usuarioId == 0)
                {
                    usuarioController.Inserir(usuarioEdicao);
                }
                //modo alterar
                else
                {
                    usuarioController.Alterar(usuarioEdicao);
                }

                MessageBox.Show("Usuário salvo com sucesso!", "SUCESSO");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "ERRO");
            }
        }
    }
}