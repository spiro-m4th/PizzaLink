// Adicione estes 'usings'
using PizzaLink.Controllers;
using System;
using System.Windows.Forms;

namespace PizzaLink.Views
{
    public partial class frmSelecionaCliente : Form
    {
        // Instancia o controller
        ClienteController clienteController = new ClienteController();

        public frmSelecionaCliente()
        {
            InitializeComponent();
        }

        // Evento Load (ao carregar o formulário)
        private void frmSelecionaCliente_Load(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        // Método privado para carregar/atualizar o grid
        private void CarregarGrid()
        {
            // Chama o Controller para buscar todos
            dgvClientes.DataSource = clienteController.GetAll();
            // Configurar colunas (opcional, mas recomendado)
            dgvClientes.Columns["ClienteId"].HeaderText = "ID";
            dgvClientes.Columns["Nome"].Width = 200;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            // Chama o Controller para buscar por nome
            dgvClientes.DataSource = clienteController.GetByNome(txtPesquisa.Text);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // 1. Verificar se algo está selecionado
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um cliente para excluir.");
                return;
            }

            // 2. Pedir confirmação
            var resultado = MessageBox.Show("Tem certeza que deseja excluir?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                // 3. Pegar o ID
                int id = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["ClienteId"].Value);

                // 4. Chamar o Controller
                clienteController.Excluir(id);

                // 5. Atualizar o grid
                CarregarGrid();
            }
        }

        // --- Botões que abrem a tela de Cadastro ---

        private void btnNovo_Click(object sender, EventArgs e)
        {
            // Abre o formulário de cadastro passando "0" (Novo)
            frmCadastroCliente form = new frmCadastroCliente(0);
            form.ShowDialog(); // ShowDialog (Modal)

            // Atualiza o grid após o cadastro
            CarregarGrid();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um cliente para alterar.");
                return;
            }

            // Pega o ID da linha selecionada
            int id = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["ClienteId"].Value);

            // Abre o formulário de cadastro passando o ID
            frmCadastroCliente form = new frmCadastroCliente(id);
            form.ShowDialog();

            // Atualiza o grid
            CarregarGrid();
        }
    }
}