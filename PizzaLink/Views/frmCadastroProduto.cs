using PizzaLink.Controllers;
using PizzaLink.Models;
using System;
using System.Windows.Forms;

namespace PizzaLink.Views
{
    public partial class frmCadastroProduto : Form
    {
        private int produtoId = 0;
        ProdutoController produtoController = new ProdutoController();

        public frmCadastroProduto(int id)
        {
            InitializeComponent();
            this.produtoId = id;
        }

        //configurar no evento load os nomes dos formularios
        //em caso de alteracao do objeto
        //ou para adicionar um novo objeto no database
        private void frmCadastroProduto_Load(object sender, EventArgs e)
        {
            if (this.produtoId != 0)
            {
                this.Text = "Alterar Produto";
                Produto produto = produtoController.GetById(this.produtoId);
                txtNome.Text = produto.Nome;
                txtPreco.Text = produto.Preco.ToString("F2");
                txtEstoque.Text = produto.Estoque.ToString();

                if (produto.Tipo == 'P') cbxTipo.SelectedIndex = 0;
                else if (produto.Tipo == 'B') cbxTipo.SelectedIndex = 1;
                else cbxTipo.SelectedIndex = 2;
            }
            else
            {
                this.Text = "Novo Produto";
                cbxTipo.SelectedIndex = 0;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtPreco.Text))
            {
                MessageBox.Show("Os campos Nome e Preço são obrigatórios.");
                return;
            }

            Produto produto = new Produto();
            produto.Nome = txtNome.Text.Trim();
            produto.Preco = Convert.ToDecimal(txtPreco.Text);
            produto.Estoque = Convert.ToInt32(txtEstoque.Text);

            if (cbxTipo.SelectedIndex == 0)
            {
                MessageBox.Show("Preencha corretamente o tipo de produto","ERRO", MessageBoxButtons.OK);
                return;
            }
            else if (cbxTipo.SelectedIndex == 1) produto.Tipo = 'P';
            else if (cbxTipo.SelectedIndex == 2) produto.Tipo = 'B';
            else produto.Tipo = 'L';

            try
            {
                if (this.produtoId == 0)
                {
                    produtoController.Inserir(produto);
                }
                else
                {
                    produto.ProdutoId = this.produtoId;
                    produtoController.Alterar(produto);
                }
                MessageBox.Show("Produto salvo com sucesso!", "SUCESSO");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "ERRO");
            }
        }
    }
}