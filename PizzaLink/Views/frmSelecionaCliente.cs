using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using PizzaLink.Controllers;
using PizzaLink.Models;

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
            //vou deixar o GenerateColumns como true pois é uma grid de um objeto com atributos simples
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

            if (MessageBox.Show("Deseja realmente excluir este registro?", "CONFIRMAÇÃO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    //Tenta excluir
                    if (clienteController.Excluir(selecionado.ClienteId) > 0)
                    {
                        MessageBox.Show("Cliente excluído com sucesso!");
                        CarregarGrid(); //Atualiza a grid
                    }
                }
                catch (SqlException ex) when (ex.Number == 547) // 547 = Erro de FK
                {
                    //mensagem de erro decorrente de um teste que deu errado.
                    //deu errado excluir um cliente que possui um pedido ja cadastrado
                    MessageBox.Show(
                        "Não é possível excluir este cliente pois ele já possui pedidos cadastrados no histórico.",
                        "Exclusão Falhou",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro inesperado ao excluir: " + ex.Message, "ERRO");
                }
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