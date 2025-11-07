using PizzaLink.Controllers;
using PizzaLink.Models;
using System;
using System.Windows.Forms;

namespace PizzaLink.Views
{
    public partial class frmCadastroUsuario : Form
    {
        // Variável para guardar o ID (0 = Novo, >0 = Alterar)
        private int usuarioId = 0;
        UsuarioController usuarioController = new UsuarioController();

        // Construtor que aceita o ID, como exigido pelo frmLogin
        public frmCadastroUsuario(int id)
        {
            InitializeComponent();
            this.usuarioId = id;
        }

        private void frmCadastroUsuario_Load(object sender, EventArgs e)
        {
            if (this.usuarioId != 0)
            {
                // Modo Alteração
                this.Text = "Alterar Usuário";

                // Carrega os dados do banco
                Usuario usuario = usuarioController.GetById(this.usuarioId);
                txtNome.Text = usuario.Nome;
                txtLogin.Text = usuario.Login;
                txtSenha.Text = ""; // Senha não deve ser carregada

                // Em um sistema real, o NivelAcesso seria um ComboBox
                // Aqui, apenas travamos o 'F' para cadastro
            }
            else
            {
                // Modo Novo (veio do "Cadastre-se" ou "Novo")
                this.Text = "Novo Cadastro de Usuário";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // 1. Validar
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Preencha todos os campos (Nome, Login, Senha).");
                return;
            }

            // 2. Preencher o Modelo
            Usuario usuario = new Usuario();
            usuario.Nome = txtNome.Text;
            usuario.Login = txtLogin.Text;
            usuario.Senha = txtSenha.Text; // (Em app real, usar Hash)

            try
            {
                // 3. Chamar Controller (Inserir ou Alterar)
                if (this.usuarioId == 0)
                {
                    // INSERIR
                    // Usuário que se cadastra é 'F' (Funcionário) por padrão
                    usuario.NivelAcesso = 'F';
                    usuarioController.Inserir(usuario);
                }
                else
                {
                    // ALTERAR
                    usuario.UsuarioId = this.usuarioId;
                    // (Aqui pegaria o NivelAcesso de um ComboBox)
                    usuario.NivelAcesso = 'F'; // Simples
                    usuarioController.Alterar(usuario);
                }

                MessageBox.Show("Usuário salvo com sucesso!", "Sucesso");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro");
            }
        }
    }
}