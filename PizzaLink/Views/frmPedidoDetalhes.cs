using PizzaLink.Controllers;
using PizzaLink.Models;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace PizzaLink.Views
{
    public partial class frmPedidoDetalhes : Form
    {
        Pedido pedidoSelecionado;
        ItemPedidoController itemPedidoController = new ItemPedidoController();

        //construtor que ira receber o pedido da outra tela
        public frmPedidoDetalhes(Pedido pedido)
        {
            InitializeComponent();

            this.pedidoSelecionado = pedido;

            dgvItensPedido.AutoGenerateColumns = false;           
        }
        #region Carregar Propriedade
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
        private void dgvItensPedido_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                // Padrão 2: Verifica se a linha não é nula e se a coluna é aninhada
                if ((dgvItensPedido.Rows[e.RowIndex].DataBoundItem != null) &&
                    (dgvItensPedido.Columns[e.ColumnIndex].DataPropertyName.Contains(".")))
                {
                    // Se sim, chama o Reflection para buscar o valor
                    e.Value = CarregarPropriedade(dgvItensPedido.Rows[e.RowIndex].DataBoundItem,
                        dgvItensPedido.Columns[e.ColumnIndex].DataPropertyName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
        private void frmPedidoDetalhes_Load(object sender, EventArgs e)
        {
            //iniciar com o metodo de carregar os dados na tela
            CarregarDadosPedido();
            CarregarItens();
        }

        private void CarregarDadosPedido()
        {
            //carregar dados do objeto
            txtPedidoId.Text = pedidoSelecionado.PedidoId.ToString();
            txtDataHora.Text = pedidoSelecionado.DataHora.ToString();
            txtStatus.Text = pedidoSelecionado.StatusTratado;
            txtTotal.Text = pedidoSelecionado.ValorTotal.ToString("C2");
            txtClienteNome.Text = pedidoSelecionado.Cliente.Nome;
            txtUsuarioNome.Text = pedidoSelecionado.Usuario.Nome;
        }

        private void CarregarItens()
        {
            //carregar a grid
            dgvItensPedido.DataSource = null;
            //buscar no BD
            dgvItensPedido.DataSource = itemPedidoController.GetByPedidoId(pedidoSelecionado.PedidoId);
            dgvItensPedido.Update();
            dgvItensPedido.Refresh();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Deseja fechar?", "CONFIRMAÇÃO", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            this.Close();
        }

    }
}