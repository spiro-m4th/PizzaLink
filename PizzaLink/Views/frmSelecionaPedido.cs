using PizzaLink.Controllers;
using PizzaLink.Models;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace PizzaLink.Views
{
    public partial class frmSelecionaPedido : Form
    {
        PedidoController pedidoController = new PedidoController();

        public frmSelecionaPedido()
        {
            InitializeComponent();
            dgvPedidos.AutoGenerateColumns = false;

            //testando uma maneira diferente de adicionar grids
            colPedidoId.DataPropertyName = "PedidoId";
            colDataHora.DataPropertyName = "DataHora";
            colClienteNome.DataPropertyName = "Cliente.Nome";
            colUsuarioNome.DataPropertyName = "Usuario.Nome";
            colValorTotal.DataPropertyName = "ValorTotal";
            colStatus.DataPropertyName = "StatusTratado";
        }

        private object CarregarPropriedade(object propriedade, string nomeDaPropriedade)
        {
            try
            {
                object retorno = "";
                if (nomeDaPropriedade.Contains("."))
                {
                    PropertyInfo[] propertyInfoArray;
                    string propriedadeAntesDoPonto;
                    propriedadeAntesDoPonto = nomeDaPropriedade.Substring(0, nomeDaPropriedade.IndexOf("."));
                    if (propriedade != null)
                    {
                        propertyInfoArray = propriedade.GetType().GetProperties();
                        foreach (PropertyInfo propertyInfo in propertyInfoArray)
                        {
                            if (propertyInfo.Name == propriedadeAntesDoPonto)
                            {
                                retorno = CarregarPropriedade(propertyInfo.GetValue(propriedade, null),
                                    nomeDaPropriedade.Substring(nomeDaPropriedade.IndexOf(".") + 1));
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Type typeProperty;
                    PropertyInfo propertyInfo;
                    if (propriedade != null)
                    {
                        typeProperty = propriedade.GetType();
                        propertyInfo = typeProperty.GetProperty(nomeDaPropriedade);
                        retorno = propertyInfo.GetValue(propriedade, null);
                    }
                }
                return retorno;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        //evento cell formating
        private void dgvPedidos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if ((dgvPedidos.Rows[e.RowIndex].DataBoundItem != null) &&
                    (dgvPedidos.Columns[e.ColumnIndex].DataPropertyName.Contains(".")))
                {
                    e.Value = CarregarPropriedade(dgvPedidos.Rows[e.RowIndex].DataBoundItem,
                        dgvPedidos.Columns[e.ColumnIndex].DataPropertyName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmSelecaoPedido_Load(object sender, EventArgs e)
        {
            //garantir que os filtros estejam visiveis
            txtFiltrar.Visible = true;
            cbxStatus.Visible = true;

            //padroes
            cbxFiltro.SelectedIndex = 4; // "todos"
            cbxStatus.SelectedIndex = 0; // "pendente"

            //desabilitar filtos que nao sejam "todos"
            AjustarFiltros();

            //pesquisa inicial
            Pesquisar();
        }

        //desabilitar tudo
        private void DesabilitarTodosFiltros()
        {
            txtFiltrar.Enabled = false;
            cbxStatus.Enabled = false;
        }

        //habilitar o filtro selecionado
        private void AjustarFiltros()
        {
            DesabilitarTodosFiltros();

            switch (cbxFiltro.SelectedIndex)
            {
                case 0: //periodo
                    dtpInicio.Enabled = true;
                    dtpFim.Enabled = true;
                    dtpInicio.Focus();
                    dtpFim.Focus();
                    break;
                case 1: //cliente ID
                case 2: //usuario ID
                    txtFiltrar.Enabled = true;
                    txtFiltrar.Focus();
                    break;
                case 3: //status
                    cbxStatus.Enabled = true;
                    cbxStatus.Focus();
                    break;
                case 4: //todos
                    btnPesquisar.Focus();
                    break;
            }
        }

        private void cbxFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            AjustarFiltros();
        }

        private void Pesquisar()
        {
            int id = 0;
            PedidoCollection pedidoCollection = new PedidoCollection();
            dgvPedidos.DataSource = null;

            //logica do filtro
            switch (cbxFiltro.SelectedIndex)
            {
                case 0:
                    pedidoCollection = pedidoController.GetByPeriodo(dtpInicio.Value, dtpFim.Value);
                    break;
                case 1:
                    if (int.TryParse(txtFiltrar.Text, out id))
                        pedidoCollection = pedidoController.GetByCliente(id);
                    break;
                case 2: 
                    if (int.TryParse(txtFiltrar.Text, out id))
                        pedidoCollection = pedidoController.GetByUsuario(id);
                    break;
                case 3: 
                    char status = 'P';
                    switch (cbxStatus.SelectedIndex)
                    {
                        case 0: status = 'P'; break;
                        case 1: status = 'F'; break;
                        case 2: status = 'C'; break; 
                    }
                    pedidoCollection = pedidoController.GetByStatus(status);
                    break;
                case 4: // Todos
                    pedidoCollection = pedidoController.GetByFilter();
                    break;
            }

            dgvPedidos.DataSource = pedidoCollection;
            dgvPedidos.Update();
            dgvPedidos.Refresh();
        }

        private Pedido GetRegistro()
        {
            if (dgvPedidos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um registro", "INFO");
                return null;
            }
            return dgvPedidos.SelectedRows[0].DataBoundItem as Pedido;
        }

        private void ChamarTelaVisualizacao()
        {
            Pedido pedidoSelecionado = GetRegistro();
            if (pedidoSelecionado != null)
            {
                // O GetById() no controller TAMBÉM precisa
                // preencher os dados aninhados para esta tela funcionar
                frmPedidoDetalhes frm = new frmPedidoDetalhes(pedidoSelecionado);
                frm.ShowDialog();
            }
        }

        private void AlterarStatus(bool IsFinalizar)
            //"status tratado"
        {
            Pedido pedidoSelecionado = GetRegistro();
            if (pedidoSelecionado == null) return;

            string statusTratado = IsFinalizar ? "Finalizar" : "Cancelar";
            char status = IsFinalizar ? 'F' : 'C';

            if (pedidoSelecionado.Status != 'P')
            {
                MessageBox.Show("Não é possivel " + statusTratado + " um pedido que já está " + pedidoSelecionado.StatusTratado, "ATENÇÃO");
                return;
            }

            if (MessageBox.Show("Deseja realmente " + statusTratado + " este pedido?", "CONFIRMAÇÃO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (pedidoController.AlterarStatus(pedidoSelecionado.PedidoId, status) > 0)
                {
                    MessageBox.Show("Pedido " + statusTratado.ToLower() + "o com sucesso.");
                    Pesquisar(); //atualizar grid
                }
            }
        }

        //eventos de click
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            ChamarTelaVisualizacao();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            AlterarStatus(true);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            AlterarStatus(false);
        }
    }
}