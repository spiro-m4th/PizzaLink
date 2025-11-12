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
            dgvUsuarios.AutoGenerateColumns = true;
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
            //filtro baseado no que esta digitado no txtPesquisa
            string filtro = "Nome LIKE '%" + txtPesquisa.Text + "%'";
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = usuarioController.GetByFilter(filtro);
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

            if (MessageBox.Show("Deseja realmente excluir?", "CONFIRMAÇÃO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    usuarioController.Excluir(selecionado.UsuarioId);
                    CarregarGrid();
                    MessageBox.Show("Usuário excluído com sucesso!");
                }
                catch (System.Data.SqlClient.SqlException ex) when (ex.Number == 547) //547 = Erro de FKe
                {
                    MessageBox.Show(
                        "Não é possível excluir este usuário, pois ele já está vinculado a um ou mais pedidos.",
                        "Exclusão Falhou",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro inesperado ao excluir: " + ex.Message, "Erro");
                }
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
            CarregarGrid();
        }
    }
}