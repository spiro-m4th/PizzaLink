using System;
using System.Windows.Forms;

namespace PizzaLink.Views
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        //CADASTROS
        private void menuUsuarios_Click(object sender, EventArgs e)
        {
            frmSelecionaUsuario frm = new frmSelecionaUsuario();
            frm.ShowDialog();
        }

        private void menuClientes_Click(object sender, EventArgs e)
        {
            frmSelecionaCliente frm = new frmSelecionaCliente();
            frm.ShowDialog();
        }

        private void menuProdutos_Click(object sender, EventArgs e)
        {
            frmSelecionaProduto frm = new frmSelecionaProduto();
            frm.ShowDialog();
        }

        //MOVIMENTACAO
        private void menuNovoPedido_Click(object sender, EventArgs e)
        {
            frmNovoPedido frm = new frmNovoPedido();
            frm.ShowDialog();
        }

        //CONSULTAS
        private void menuConsultarPedidos_Click(object sender, EventArgs e)
        {
            frmSelecionaPedido frm = new frmSelecionaPedido();
            frm.ShowDialog();
        }

        //SAIR
        private void menuSair_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Deseja realmente finalizar o app?", "CONFIRMAÇÃO", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
                this.Close();
            else
            return;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {

        }
    }

}