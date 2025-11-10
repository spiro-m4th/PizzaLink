using PizzaLink.Controllers;
using PizzaLink.Models;
using System;
using System.Windows.Forms;

namespace PizzaLink.Views
{
    public partial class frmSelecionaUsuario : Form
    {
        //variável pública para o form de pedido pegar
        public Usuario usuarioSelecao;

        private bool emSelecao = false;
        UsuarioController usuarioController = new UsuarioController();

        //iniciar construtor definindo o modo
        public frmSelecionaUsuario(bool emSelecao = false)
        {
            InitializeComponent();
            this.emSelecao = emSelecao;
        }

        private void frmSelecionaUsuario_Load(object sender, EventArgs e)
        {
            //ajusta a tela para o modo de gerenciamento ou seleção
            if (this.emSelecao)
            {
                //modo de seleção (chamado pelo frmNovoPedido)
                btnNovo.Visible = false;
                btnAlterar.Visible = false;
                btnExcluir.Visible = false;
                btnSelecionar.Visible = true;
                this.Text = "Selecionar Usuário";
            }
            else
            {
                //modo de gerenciamento (chamado pelo frmPrincipal)
                btnNovo.Visible = true;
                btnAlterar.Visible = true;
                btnExcluir.Visible = true;
                btnSelecionar.Visible = false;
                this.Text = "Gerenciar Usuários";
            }
            CarregarGrid();
        }

        private void CarregarGrid()
        {
            dgvUsuarios.DataSource = null;
            //GetByFilter() sem filtro busca todos, igual GetAll()
            dgvUsuarios.DataSource = usuarioController.GetByFilter();
            dgvUsuarios.Update();
            dgvUsuarios.Refresh();
        }

        private Usuario GetSelecionado()
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum usuário selecionado.");
                return null;
            }
            //pega o objeto da linha
            return dgvUsuarios.SelectedRows[0].DataBoundItem as Usuario;
        }

        //botoes de gerenciamento
        private void btnNovo_Click(object sender, EventArgs e)
        {
            //abre tela de cadastro
            frmCadastroUsuario frm = new frmCadastroUsuario(0);
            frm.ShowDialog();
            CarregarGrid(); //atualiza a grid após fechar
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Usuario selecionado = GetSelecionado();
            if (selecionado == null) return;

            frmCadastroUsuario frm = new frmCadastroUsuario(selecionado.UsuarioId);
            frm.ShowDialog();
            CarregarGrid();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Usuario selecionado = GetSelecionado();
            if (selecionado == null) return;

            if (MessageBox.Show("Deseja realmente excluir?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                usuarioController.Excluir(selecionado.UsuarioId);
                CarregarGrid();
            }
        }

        //botão de seleção
        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Usuario selecionado = GetSelecionado();
            if (selecionado == null) return;

            //coloca o objeto na variável pública definida la em cima
            this.usuarioSelecao = selecionado;

            //definir resultado como OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //filtro na mão
            dgvUsuarios.DataSource = usuarioController.GetByFilter("Nome LIKE '%" + txtPesquisa.Text + "%'");
        }
    }
}