namespace Archivos
{
    partial class Registros
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Registro = new System.Windows.Forms.TabControl();
            this.TabRegistro = new System.Windows.Forms.TabPage();
            this.DGRegistros = new System.Windows.Forms.DataGridView();
            this.DGCaptura = new System.Windows.Forms.DataGridView();
            this.BtnEliminar = new System.Windows.Forms.Button();
            this.BtnModificar = new System.Windows.Forms.Button();
            this.BtnAgregar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DGPrimario = new System.Windows.Forms.DataGridView();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.AbrirIndice = new System.Windows.Forms.Button();
            this.DGIndices = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGSecundario = new System.Windows.Forms.DataGridView();
            this.DirInd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.Registro.SuspendLayout();
            this.TabRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGRegistros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGCaptura)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGPrimario)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGIndices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGSecundario)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.importarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1079, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // importarToolStripMenuItem
            // 
            this.importarToolStripMenuItem.Name = "importarToolStripMenuItem";
            this.importarToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.importarToolStripMenuItem.Text = "Importar";
            // 
            // Registro
            // 
            this.Registro.Controls.Add(this.TabRegistro);
            this.Registro.Controls.Add(this.tabPage1);
            this.Registro.Controls.Add(this.tabPage3);
            this.Registro.Location = new System.Drawing.Point(12, 27);
            this.Registro.Name = "Registro";
            this.Registro.SelectedIndex = 0;
            this.Registro.Size = new System.Drawing.Size(1067, 454);
            this.Registro.TabIndex = 17;
            // 
            // TabRegistro
            // 
            this.TabRegistro.Controls.Add(this.DGRegistros);
            this.TabRegistro.Controls.Add(this.DGCaptura);
            this.TabRegistro.Controls.Add(this.BtnEliminar);
            this.TabRegistro.Controls.Add(this.BtnModificar);
            this.TabRegistro.Controls.Add(this.BtnAgregar);
            this.TabRegistro.Controls.Add(this.label2);
            this.TabRegistro.Controls.Add(this.label1);
            this.TabRegistro.Location = new System.Drawing.Point(4, 22);
            this.TabRegistro.Name = "TabRegistro";
            this.TabRegistro.Padding = new System.Windows.Forms.Padding(3);
            this.TabRegistro.Size = new System.Drawing.Size(1059, 428);
            this.TabRegistro.TabIndex = 1;
            this.TabRegistro.Text = "Registros";
            this.TabRegistro.UseVisualStyleBackColor = true;
            // 
            // DGRegistros
            // 
            this.DGRegistros.AllowUserToAddRows = false;
            this.DGRegistros.AllowUserToDeleteRows = false;
            this.DGRegistros.AllowUserToOrderColumns = true;
            this.DGRegistros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGRegistros.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGRegistros.BackgroundColor = System.Drawing.Color.Silver;
            this.DGRegistros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGRegistros.Location = new System.Drawing.Point(22, 158);
            this.DGRegistros.Name = "DGRegistros";
            this.DGRegistros.Size = new System.Drawing.Size(648, 259);
            this.DGRegistros.TabIndex = 14;
            this.DGRegistros.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGRegistros_CellClick);
            // 
            // DGCaptura
            // 
            this.DGCaptura.AllowUserToDeleteRows = false;
            this.DGCaptura.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGCaptura.BackgroundColor = System.Drawing.Color.Silver;
            this.DGCaptura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGCaptura.Location = new System.Drawing.Point(22, 69);
            this.DGCaptura.Name = "DGCaptura";
            this.DGCaptura.Size = new System.Drawing.Size(365, 69);
            this.DGCaptura.TabIndex = 13;
            // 
            // BtnEliminar
            // 
            this.BtnEliminar.Location = new System.Drawing.Point(210, 24);
            this.BtnEliminar.Name = "BtnEliminar";
            this.BtnEliminar.Size = new System.Drawing.Size(75, 23);
            this.BtnEliminar.TabIndex = 12;
            this.BtnEliminar.Text = "Eliminar";
            this.BtnEliminar.UseVisualStyleBackColor = true;
            this.BtnEliminar.Click += new System.EventHandler(this.BtnEliminar_Click);
            // 
            // BtnModificar
            // 
            this.BtnModificar.Location = new System.Drawing.Point(114, 24);
            this.BtnModificar.Name = "BtnModificar";
            this.BtnModificar.Size = new System.Drawing.Size(75, 23);
            this.BtnModificar.TabIndex = 11;
            this.BtnModificar.Text = "Modificar";
            this.BtnModificar.UseVisualStyleBackColor = true;
            this.BtnModificar.Click += new System.EventHandler(this.BtnModificar_Click);
            // 
            // BtnAgregar
            // 
            this.BtnAgregar.Location = new System.Drawing.Point(22, 24);
            this.BtnAgregar.Name = "BtnAgregar";
            this.BtnAgregar.Size = new System.Drawing.Size(75, 23);
            this.BtnAgregar.TabIndex = 10;
            this.BtnAgregar.Text = "Agregar";
            this.BtnAgregar.UseVisualStyleBackColor = true;
            this.BtnAgregar.Click += new System.EventHandler(this.BtnAgregar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(387, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 16);
            this.label2.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(304, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Entidad:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DGPrimario);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1059, 428);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Índice Primario";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DGPrimario
            // 
            this.DGPrimario.AllowUserToAddRows = false;
            this.DGPrimario.AllowUserToDeleteRows = false;
            this.DGPrimario.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGPrimario.BackgroundColor = System.Drawing.Color.Silver;
            this.DGPrimario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGPrimario.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Valor,
            this.Direccion});
            this.DGPrimario.Location = new System.Drawing.Point(46, 61);
            this.DGPrimario.Name = "DGPrimario";
            this.DGPrimario.ReadOnly = true;
            this.DGPrimario.Size = new System.Drawing.Size(187, 184);
            this.DGPrimario.TabIndex = 22;
            // 
            // Valor
            // 
            this.Valor.HeaderText = "Valor";
            this.Valor.Name = "Valor";
            this.Valor.ReadOnly = true;
            // 
            // Direccion
            // 
            this.Direccion.HeaderText = "Dirección";
            this.Direccion.Name = "Direccion";
            this.Direccion.ReadOnly = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.AbrirIndice);
            this.tabPage3.Controls.Add(this.DGIndices);
            this.tabPage3.Controls.Add(this.DGSecundario);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1059, 428);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Índice Secundario";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // AbrirIndice
            // 
            this.AbrirIndice.Location = new System.Drawing.Point(107, 17);
            this.AbrirIndice.Name = "AbrirIndice";
            this.AbrirIndice.Size = new System.Drawing.Size(75, 23);
            this.AbrirIndice.TabIndex = 26;
            this.AbrirIndice.Text = "Abrir Indice";
            this.AbrirIndice.UseVisualStyleBackColor = true;
            this.AbrirIndice.Click += new System.EventHandler(this.AbrirIndice_Click);
            // 
            // DGIndices
            // 
            this.DGIndices.AllowUserToAddRows = false;
            this.DGIndices.AllowUserToDeleteRows = false;
            this.DGIndices.BackgroundColor = System.Drawing.Color.Silver;
            this.DGIndices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGIndices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.Column5});
            this.DGIndices.Location = new System.Drawing.Point(386, 42);
            this.DGIndices.Name = "DGIndices";
            this.DGIndices.ReadOnly = true;
            this.DGIndices.Size = new System.Drawing.Size(467, 318);
            this.DGIndices.TabIndex = 25;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column1";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column2";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column3";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // DGSecundario
            // 
            this.DGSecundario.AllowUserToAddRows = false;
            this.DGSecundario.AllowUserToDeleteRows = false;
            this.DGSecundario.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGSecundario.BackgroundColor = System.Drawing.Color.Silver;
            this.DGSecundario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGSecundario.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DirInd,
            this.Column1,
            this.Column2,
            this.Column6});
            this.DGSecundario.Location = new System.Drawing.Point(24, 70);
            this.DGSecundario.Name = "DGSecundario";
            this.DGSecundario.ReadOnly = true;
            this.DGSecundario.Size = new System.Drawing.Size(311, 301);
            this.DGSecundario.TabIndex = 24;
            // 
            // DirInd
            // 
            this.DirInd.FillWeight = 80F;
            this.DirInd.HeaderText = "Dir Indice";
            this.DirInd.Name = "DirInd";
            this.DirInd.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Valor";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 80F;
            this.Column2.HeaderText = "Dirección";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.FillWeight = 80F;
            this.Column6.HeaderText = "Dir Sig";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Registros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 478);
            this.Controls.Add(this.Registro);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Registros";
            this.Text = "Registros";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Registro.ResumeLayout(false);
            this.TabRegistro.ResumeLayout(false);
            this.TabRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGRegistros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGCaptura)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGPrimario)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGIndices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGSecundario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importarToolStripMenuItem;
        private System.Windows.Forms.TabControl Registro;
        private System.Windows.Forms.TabPage TabRegistro;
        private System.Windows.Forms.DataGridView DGRegistros;
        private System.Windows.Forms.DataGridView DGCaptura;
        private System.Windows.Forms.Button BtnEliminar;
        private System.Windows.Forms.Button BtnModificar;
        private System.Windows.Forms.Button BtnAgregar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView DGPrimario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Direccion;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button AbrirIndice;
        private System.Windows.Forms.DataGridView DGIndices;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridView DGSecundario;
        private System.Windows.Forms.DataGridViewTextBoxColumn DirInd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}