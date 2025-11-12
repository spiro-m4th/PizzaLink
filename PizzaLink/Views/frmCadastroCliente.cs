using PizzaLink.Controllers;
using PizzaLink.Models;
using System;
using System.Windows.Forms;

namespace PizzaLink.Views
{
    public partial class frmCadastroCliente : Form
    {
        private int clienteId = 0;
        ClienteController clienteController = new ClienteController();

        public frmCadastroCliente(int id)
        {
            InitializeComponent();
            this.clienteId = id;
        }

        private void frmCadastroCliente_Load(object sender, EventArgs e)
        {
            if (this.clienteId != 0)
            {
                this.Text = "Alterar Cliente";
                Cliente cliente = clienteController.GetById(this.clienteId);
                txtNome.Text = cliente.Nome;
                txtTelefone.Text = cliente.Telefone;
                txtCpf.Text = cliente.Cpf;
                txtEndereco.Text = cliente.Endereco;
            }
            else
            {
                this.Text = "Novo Cliente";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Deseja Cancelar?", "CONFIRMAÇÃO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                MessageBox.Show("Os Campos Nome e Telefone são obrigatórios.");
                return;
            }

            Cliente cliente = new Cliente();        //usar trim para evitar espaços desnecessarios
            cliente.Nome = txtNome.Text;
            cliente.Telefone = txtTelefone.Text.Trim();
            cliente.Cpf = txtCpf.Text.Trim();
            cliente.Endereco = txtEndereco.Text;

            try
            {
                if (this.clienteId == 0)
                {
                    clienteController.Inserir(cliente);
                }
                else
                {
                    cliente.ClienteId = this.clienteId;
                    clienteController.Alterar(cliente);
                }
                MessageBox.Show("Cliente salvo com sucesso!", "SUCESSO");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "ERRO");
            }
        }
    }
}