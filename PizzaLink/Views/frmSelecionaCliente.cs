using PizzaLink.Controllers;
using PizzaLink.Models;
using System;
using System.Windows.Forms;

namespace PizzaLink.Views
{
    public partial class frmSelecionaCliente : Form
    {
        public Cliente clienteSelecao;
        private bool emSelecao = false;
        ClienteController clienteController = new ClienteController();

        public frmSelecionaCliente(bool emSelecao = false)
        {
            InitializeComponent();
            this.emSelecao = emSelecao;
            dgvClientes.AutoGenerateColumns = true;
        }

        private void frmSelecionaCliente_Load(object sender, EventArgs e)
        {
            if (this.emSelecao)
            {
                btnNovo.Visible = false;
                btnAlterar.Visible = false;
                btnExcluir.Visible = false;
                btnSelecionar.Visible = true;
                this.Text = "Selecionar Cliente";
            }
            else
            {
                btnNovo.Visible = true;
                btnAlterar.Visible = true;
                btnExcluir.Visible = true;
                btnSelecionar.Visible = false;
                this.Text = "Gerenciar Clientes";
            }
            CarregarGrid();
        }

        private void CarregarGrid()
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = clienteController.GetByFilter();
            dgvClientes.Update();
            dgvClientes.Refresh();
        }

        private Cliente GetSelecionado()
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente selecionado.");
                return null;
            }
            return dgvClientes.SelectedRows[0].DataBoundItem as Cliente;
        }

        //MANIPULAÇÃO DE DADOS
        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmCadastroCliente frm = new frmCadastroCliente(0);
            frm.ShowDialog();
            CarregarGrid();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Cliente selecionado = GetSelecionado();
            if (selecionado == null) return;

            frmCadastroCliente frm = new frmCadastroCliente(selecionado.ClienteId);
            frm.ShowDialog();
            CarregarGrid();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Cliente selecionado = GetSelecionado();
            if (selecionado == null) return;

            if (MessageBox.Show("Deseja realmente excluir?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                clienteController.Excluir(selecionado.ClienteId);
                CarregarGrid();
            }
        }

        //BOTAO SELECIONAR
        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Cliente selecionado = GetSelecionado();
            if (selecionado == null)
                return;
            this.clienteSelecao = selecionado;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            dgvClientes.DataSource = clienteController.GetByFilter("Nome LIKE '%" + txtPesquisa.Text + "%'");
        }
    }
}