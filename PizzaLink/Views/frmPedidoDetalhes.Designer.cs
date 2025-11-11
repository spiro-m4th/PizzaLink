namespace PizzaLink.Views
{
    partial class frmPedidoDetalhes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPedidoId = new System.Windows.Forms.TextBox();
            this.txtDataHora = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtClienteNome = new System.Windows.Forms.TextBox();
            this.txtUsuarioNome = new System.Windows.Forms.TextBox();
            this.dgvItensPedido = new System.Windows.Forms.DataGridView();
            this.btnFechar = new System.Windows.Forms.Button();
            this.colProdutoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProdutoNome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecoUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItensPedido)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtTotal);
            this.panel1.Controls.Add(this.txtStatus);
            this.panel1.Controls.Add(this.txtDataHora);
            this.panel1.Controls.Add(this.txtPedidoId);
            this.panel1.Location = new System.Drawing.Point(12, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 407);
            this.panel1.TabIndex = 0;
            // 
            // txtPedidoId
            // 
            this.txtPedidoId.Location = new System.Drawing.Point(3, 27);
            this.txtPedidoId.Name = "txtPedidoId";
            this.txtPedidoId.ReadOnly = true;
            this.txtPedidoId.Size = new System.Drawing.Size(100, 20);
            this.txtPedidoId.TabIndex = 0;
            // 
            // txtDataHora
            // 
            this.txtDataHora.Location = new System.Drawing.Point(3, 87);
            this.txtDataHora.Name = "txtDataHora";
            this.txtDataHora.ReadOnly = true;
            this.txtDataHora.Size = new System.Drawing.Size(100, 20);
            this.txtDataHora.TabIndex = 1;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(3, 150);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(100, 20);
            this.txtStatus.TabIndex = 2;
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(3, 221);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Dados do Pedido";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtUsuarioNome);
            this.panel2.Controls.Add(this.txtClienteNome);
            this.panel2.Location = new System.Drawing.Point(241, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 407);
            this.panel2.TabIndex = 5;
            // 
            // txtClienteNome
            // 
            this.txtClienteNome.Location = new System.Drawing.Point(20, 27);
            this.txtClienteNome.Name = "txtClienteNome";
            this.txtClienteNome.ReadOnly = true;
            this.txtClienteNome.Size = new System.Drawing.Size(100, 20);
            this.txtClienteNome.TabIndex = 4;
            // 
            // txtUsuarioNome
            // 
            this.txtUsuarioNome.Location = new System.Drawing.Point(20, 106);
            this.txtUsuarioNome.Name = "txtUsuarioNome";
            this.txtUsuarioNome.ReadOnly = true;
            this.txtUsuarioNome.Size = new System.Drawing.Size(100, 20);
            this.txtUsuarioNome.TabIndex = 5;
            // 
            // dgvItensPedido
            // 
            this.dgvItensPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItensPedido.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProdutoId,
            this.colProdutoNome,
            this.colQuantidade,
            this.colPrecoUnitario,
            this.colSubtotal});
            this.dgvItensPedido.Location = new System.Drawing.Point(518, 31);
            this.dgvItensPedido.Name = "dgvItensPedido";
            this.dgvItensPedido.Size = new System.Drawing.Size(240, 150);
            this.dgvItensPedido.TabIndex = 6;
            this.dgvItensPedido.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvItensPedido_CellFormatting);
            // 
            // btnFechar
            // 
            this.btnFechar.Location = new System.Drawing.Point(683, 415);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 23);
            this.btnFechar.TabIndex = 7;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            // 
            // colProdutoId
            // 
            this.colProdutoId.HeaderText = "ID Produto";
            this.colProdutoId.Name = "colProdutoId";
            this.colProdutoId.ReadOnly = true;
            // 
            // colProdutoNome
            // 
            this.colProdutoNome.HeaderText = "Nome do Produto";
            this.colProdutoNome.Name = "colProdutoNome";
            this.colProdutoNome.ReadOnly = true;
            // 
            // colQuantidade
            // 
            this.colQuantidade.HeaderText = "Quantidade";
            this.colQuantidade.Name = "colQuantidade";
            this.colQuantidade.ReadOnly = true;
            // 
            // colPrecoUnitario
            // 
            this.colPrecoUnitario.HeaderText = "Preço Unitario";
            this.colPrecoUnitario.Name = "colPrecoUnitario";
            this.colPrecoUnitario.ReadOnly = true;
            // 
            // colSubtotal
            // 
            this.colSubtotal.HeaderText = "Subtotal";
            this.colSubtotal.Name = "colSubtotal";
            this.colSubtotal.ReadOnly = true;
            // 
            // frmPedidoDetalhes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.dgvItensPedido);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "frmPedidoDetalhes";
            this.Text = "frmPedidoDetalhes";
            this.Load += new System.EventHandler(this.frmPedidoDetalhes_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItensPedido)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtDataHora;
        private System.Windows.Forms.TextBox txtPedidoId;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtUsuarioNome;
        private System.Windows.Forms.TextBox txtClienteNome;
        private System.Windows.Forms.DataGridView dgvItensPedido;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProdutoId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProdutoNome;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecoUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubtotal;
    }
}