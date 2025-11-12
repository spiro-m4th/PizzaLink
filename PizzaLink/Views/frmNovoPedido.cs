using PizzaLink.Controllers;
using PizzaLink.Models;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace PizzaLink.Views
{
    public partial class frmNovoPedido : Form
    {
        //atributos para armazenar as selecoes
        Cliente clientePedido;
        Usuario usuarioPedido;
        Produto produtoPedido;

        //atributo de pedido
        Pedido pedido;

        //instanciar controllers
        PedidoController pedidoController = new PedidoController();
        ItemPedidoController itemPedidoController = new ItemPedidoController();

        public frmNovoPedido()
        {
            InitializeComponent();
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
                MessageBox.Show(ex.Message, "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private void dgvItensPedido_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //verificar se a coluna nao é nula e se contem ponto
                if ((dgvItensPedido.Rows[e.RowIndex].DataBoundItem != null) &&
                    (dgvItensPedido.Columns[e.ColumnIndex].DataPropertyName.Contains(".")))
                {
                    //se sim, chama o reflection para carregar a propriedade
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

        //método de pesquisa
        private void PesquisarCliente()
        {
            frmSelecionaCliente frm = new frmSelecionaCliente(true); //true = Modo Seleção
            if (frm.ShowDialog() == DialogResult.OK)
            {
                clientePedido = frm.clienteSelecao;
                txtClienteId.Text = clientePedido.ClienteId.ToString();
                txtClienteNome.Text = clientePedido.Nome;
            }
        }

        private void PesquisarUsuario()
        {
            frmSelecionaUsuario frm = new frmSelecionaUsuario(true);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                usuarioPedido = frm.usuarioSelecao;
                txtUsuarioId.Text = usuarioPedido.UsuarioId.ToString();
                txtUsuarioNome.Text = usuarioPedido.Nome;
            }
        }

        private void PesquisarProduto()
        {
            frmSelecionaProduto frm = new frmSelecionaProduto(true);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                produtoPedido = frm.produtoSelecao;
                txtProdutoNome.Text = produtoPedido.Nome;
                txtValorUnitario.Text = produtoPedido.Preco.ToString("C2"); //c2 para formato de moeda
                txtQuantidade.Focus();
            }
        }

        //mais métodos, dessa vez de manipulacao
        private void AdicionarItem()
        {
            //validar
            if (usuarioPedido == null)
            {
                MessageBox.Show("Selecione o usuário (vendedor)", "ATENÇÃO", MessageBoxButtons.OK);
                btnPesquisarUsuario.Focus();
                return;
            }
            if (clientePedido == null)
            {
                MessageBox.Show("Selecione o cliente", "ATENÇÃO", MessageBoxButtons.OK);
                btnPesquisarCliente.Focus();
                return;
            }
            if (produtoPedido == null)
            {
                MessageBox.Show("Selecione o produto", "ATENÇÃO", MessageBoxButtons.OK);
                btnPesquisarProduto.Focus();
                return;
            }

            int quantidade = 0;
            if (!int.TryParse(txtQuantidade.Text, out quantidade) || quantidade <= 0)
            {
                MessageBox.Show("A quantidade deve ser maior que zero", "ATENÇÃO");
                txtQuantidade.Focus();
                return;
            }

            //criar o pedido se for o primeiro item
            // para funcionar, PedidoController.Inserir preicsa retornar o id
            if (pedido == null)
            {
                pedido = new Pedido();
                pedido.Usuario = usuarioPedido;
                pedido.Cliente = clientePedido;
                pedido.DataHora = DateTime.Now;
                pedido.Status = 'P'; //a propriedade tratada se refere a pendente
                pedido.ValorTotal = 0; //atualiza o valor

                pedido.PedidoId = pedidoController.Inserir(pedido);

                txtPedidoId.Text = pedido.PedidoId.ToString();
                txtDataHora.Text = pedido.DataHora.ToString();
            }

            //criar e salvar o item
            ItemPedido item = new ItemPedido();
            item.PedidoId = pedido.PedidoId;
            item.Produto = produtoPedido;
            item.Quantidade = quantidade;
            item.PrecoUnitario = produtoPedido.Preco; //salva o preço no momento da venda

            if (itemPedidoController.Inserir(item) > 0)
            {
                AtualizarTotalPedido();
                AtualizarGrade(pedido.PedidoId);
                LimparCamposItem();
            }
            else
            {
                MessageBox.Show("Falha ao inserir item", "ERRO");
            }
        }

        private ItemPedido GetItemSelecionado()
        {
            if (dgvItensPedido.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum item selecionado");
                return null;
            }
            return dgvItensPedido.SelectedRows[0].DataBoundItem as ItemPedido;
        }

        private void RemoverItem()
        {
            ItemPedido itemSelecionado = GetItemSelecionado();
            if (itemSelecionado == null) return;

            if (MessageBox.Show("Deseja realmente remover este item?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                itemPedidoController.ExcluirPorItemId(itemSelecionado.ItemPedidoId);

                AtualizarTotalPedido();
                AtualizarGrade(pedido.PedidoId);
            }
        }

        private void FinalizarPedido()
        {
            if (pedido == null)
            {
                MessageBox.Show("Nenhum Pedido criado para ser Finalizado.", "Atenção...");
                return;
            }

            if (MessageBox.Show("Deseja realmente Finalizar este pedido?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (pedidoController.AlterarStatus(pedido.PedidoId, 'F') > 0)
                {
                    MessageBox.Show("Pedido finalizado com sucesso.");
                    LimparTelaToda();
                }
            }
        }

        private void CancelarPedido()
        {
            if (pedido == null)
            {
                MessageBox.Show("Nenhum Pedido criado para ser Cancelado.", "Atenção...");
                return;
            }

            if (MessageBox.Show("Deseja realmente Cancelar este pedido?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (pedidoController.AlterarStatus(pedido.PedidoId, 'C') > 0)
                {
                    MessageBox.Show("Pedido cancelado com sucesso.");
                    LimparTelaToda();
                }
            }
        }

        #region Métodos
        private void AtualizarGrade(int pedidoId)
        {
            dgvItensPedido.DataSource = null;
            dgvItensPedido.DataSource = itemPedidoController.GetByPedidoId(pedidoId);
            dgvItensPedido.Update();
            dgvItensPedido.Refresh();
        }

        private void AtualizarTotalPedido()
        {
            ItemPedidoCollection itens = itemPedidoController.GetByPedidoId(pedido.PedidoId);
            decimal novoTotal = 0;
            foreach (ItemPedido item in itens)
            {
                novoTotal += item.Subtotal;
            }

            pedido.ValorTotal = novoTotal;
            pedidoController.Alterar(pedido);
            lblValorTotal.Text = novoTotal.ToString("C2");
        }

        private void LimparCamposItem()
        {
            produtoPedido = null;
            txtProdutoNome.Clear();
            txtValorUnitario.Clear();
            txtQuantidade.Text = "";
            btnPesquisarProduto.Focus();
        }

        private void LimparTelaToda()
        {
            clientePedido = null;
            usuarioPedido = null;
            produtoPedido = null;
            pedido = null;

            txtClienteId.Clear();
            txtClienteNome.Clear();
            txtUsuarioId.Clear();
            txtUsuarioNome.Clear();
            txtPedidoId.Clear();
            txtDataHora.Clear();
            lblValorTotal.Text = "R$ 0,00";
            LimparCamposItem();

            dgvItensPedido.DataSource = null;
            btnPesquisarCliente.Focus();
        }
        #endregion
        #region Eventos de Click
        private void btnPesquisarCliente_Click(object sender, EventArgs e)
        {
            PesquisarCliente();
        }

        private void btnPesquisarUsuario_Click(object sender, EventArgs e)
        {
            PesquisarUsuario();
        }

        private void btnPesquisarProduto_Click(object sender, EventArgs e)
        {
            PesquisarProduto();
        }

        private void btnAdicionarItem_Click(object sender, EventArgs e)
        {
            AdicionarItem();
        }

        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            RemoverItem();
        }

        private void btnFinalizarPedido_Click(object sender, EventArgs e)
        {
            FinalizarPedido();
        }

        private void btnCancelarPedido_Click(object sender, EventArgs e)
        {
            CancelarPedido();
        }

        private void frmNovoPedido_Load(object sender, EventArgs e)
        {
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Deseja sair da tela de pedidos?", "CONFIRMAÇÃO", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            this.Close();
        }
        #endregion
    }
}