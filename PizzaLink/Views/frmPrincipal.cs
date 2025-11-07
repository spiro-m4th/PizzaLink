using System;
using System.Windows.Forms;

namespace PizzaLink.Views
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            // Garanta que 'IsMdiContainer' esteja FALSO nas propriedades
        }

        // --- CADASTROS ---
        private void menuClientes_Click(object sender, EventArgs e)
        {
            // O padrão é abrir a tela de seleção, não a de cadastro
            frmSelecionaCliente frm = new frmSelecionaCliente();
            frm.ShowDialog();
        }

        private void menuProdutos_Click(object sender, EventArgs e)
        {
            frmSelecionaProduto frm = new frmSelecionaProduto();
            frm.ShowDialog();
        }

        private void menuUsuarios_Click(object sender, EventArgs e)
        {
            frmSelecaoUsuario frm = new frmSelecaoUsuario();
            frm.ShowDialog();
        }

        // --- MOVIMENTAÇÃO ---
        private void menuNovoPedido_Click(object sender, EventArgs e)
        {
            frmNovoPedido frm = new frmNovoPedido();
            frm.ShowDialog();
        }

        // --- CONSULTA ---
        private void menuConsultarPedidos_Click(object sender, EventArgs e)
        {
            // O Padrão 3 (frmPedidoHistorico) é a tela de consulta
            frmSelecaoPedido frm = new frmSelecaoPedido();
            frm.ShowDialog();
        }

        // --- SAIR ---
        private void menuSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}