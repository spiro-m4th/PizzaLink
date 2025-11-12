using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using PizzaLink.Controllers;
using PizzaLink.Models;

namespace PizzaLink.Views
{
    public partial class frmSelecionaProduto : Form
    {
        public Produto produtoSelecao;
        private bool emSelecao = false;
        ProdutoController produtoController = new ProdutoController();

        public frmSelecionaProduto(bool emSelecao = false)
        {
            InitializeComponent();
            this.emSelecao = emSelecao;
            dgvProdutos.AutoGenerateColumns = true;
        }

        private void frmSelecionaProduto_Load(object sender, EventArgs e)
        {
            if (this.emSelecao)
            {
                btnNovo.Visible = false;
                btnAlterar.Visible = false;
                btnExcluir.Visible = false;
                btnSelecionar.Visible = true;
                this.Text = "Selecionar Produto";
            }
            else
            {
                btnNovo.Visible = true;
                btnAlterar.Visible = true;
                btnExcluir.Visible = true;
                btnSelecionar.Visible = false;
                this.Text = "Gerenciar Produtos";
            }
            CarregarGrid();
        }

        private void CarregarGrid()
        {
            dgvProdutos.DataSource = null;
            dgvProdutos.DataSource = produtoController.GetByFilter();
            dgvProdutos.Update();
            dgvProdutos.Refresh();
        }

        private Produto GetSelecionado()
        {
            if (dgvProdutos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum produto selecionado.");
                return null;
            }
            return dgvProdutos.SelectedRows[0].DataBoundItem as Produto;
        }

        //manipulacao de dados
        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmCadastroProduto frm = new frmCadastroProduto(0);
            frm.ShowDialog();
            CarregarGrid();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Produto selecionado = GetSelecionado();
            if (selecionado == null) return;

            frmCadastroProduto frm = new frmCadastroProduto(selecionado.ProdutoId);
            frm.ShowDialog();
            CarregarGrid();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Produto selecionado = GetSelecionado();
            if (selecionado == null) return;

            if (MessageBox.Show("Deseja realmente excluir este produto?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    //tenta excluir
                    if (produtoController.Excluir(selecionado.ProdutoId) > 0)
                    {
                        MessageBox.Show("Produto excluído com sucesso!");
                        CarregarGrid();
                    }
                }
                catch (SqlException ex) when (ex.Number == 547) //547 = Erro de FK
                {
                    MessageBox.Show(
                        "Não é possível excluir este produto, pois ele já foi utilizado em um ou mais pedidos.",
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
        //botao selecionar
        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Produto selecionado = GetSelecionado();
            if (selecionado == null) 
                return;
            this.produtoSelecao = selecionado;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            dgvProdutos.DataSource = produtoController.GetByFilter("Nome LIKE '%" + txtPesquisa.Text + "%'");
        }
    }
}