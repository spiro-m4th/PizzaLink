using PizzaLink.Controllers;
using PizzaLink.Models;
using System;
using System.Windows.Forms;

namespace PizzaLink.Views
{
    public partial class frmCadastroCliente : Form
    {
        // Variável privada para guardar o ID do cliente
        // 0 = Novo, > 0 = Alterar
        private int clienteId = 0;
        ClienteController clienteController = new ClienteController();

        // Construtor modificado para receber o ID
        public frmCadastroCliente(int id)
        {
            InitializeComponent();
            this.clienteId = id;
        }

        // Evento Load
        private void frmCadastroCliente_Load(object sender, EventArgs e)
        {
            if (this.clienteId != 0)
            {
                // Modo Alteração: Carrega os dados
                this.Text = "Alterar Cliente";

                // 1. Chama o Controller
                Cliente cliente = clienteController.GetById(this.clienteId);

                // 2. Preenche os campos
                txtNome.Text = cliente.Nome;
                txtTelefone.Text = cliente.Telefone;
                txtCpf.Text = cliente.Cpf;
                txtEndereco.Text = cliente.Endereco;
            }
            else
            {
                // Modo Cadastro
                this.Text = "Novo Cliente";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close(); // Apenas fecha o formulário
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // 1. Validar dados (simples)
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                MessageBox.Show("Nome e Telefone são obrigatórios.");
                return;
            }

            // 2. Preencher o objeto Model
            Cliente cliente = new Cliente();
            cliente.Nome = txtNome.Text;
            cliente.Telefone = txtTelefone.Text;
            cliente.Cpf = txtCpf.Text;
            cliente.Endereco = txtEndereco.Text;

            try
            {
                // 3. Chamar o Controller para Salvar
                if (this.clienteId == 0)
                {
                    // Inserir
                    clienteController.Inserir(cliente);
                }
                else
                {
                    // Alterar
                    cliente.ClienteId = this.clienteId; // Não esquecer o ID
                    clienteController.Alterar(cliente);
                }

                MessageBox.Show("Cliente salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Fecha o formulário
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}