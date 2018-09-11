namespace Archivos
{
    partial class Proyecto
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.AgregaEnt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EntNueva = new System.Windows.Forms.TextBox();
            this.EliminaEnt = new System.Windows.Forms.Button();
            this.ModEnt = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.BGuardar = new System.Windows.Forms.Button();
            this.BNuevo = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.NuevoAtrib = new System.Windows.Forms.TextBox();
            this.ElimAtrib = new System.Windows.Forms.Button();
            this.ModifAtrib = new System.Windows.Forms.Button();
            this.AgregarAtrib = new System.Windows.Forms.Button();
            this.DGEntidad = new System.Windows.Forms.DataGridView();
            this.NombEnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirEnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirAtrib = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirDatos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirEntS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridAtributos = new System.Windows.Forms.DataGridView();
            this.NomA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoIndice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirInd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirSA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGEntidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAtributos)).BeginInit();
            this.SuspendLayout();
            // 
            // AgregaEnt
            // 
            this.AgregaEnt.Enabled = false;
            this.AgregaEnt.Location = new System.Drawing.Point(115, 17);
            this.AgregaEnt.Name = "AgregaEnt";
            this.AgregaEnt.Size = new System.Drawing.Size(75, 23);
            this.AgregaEnt.TabIndex = 1;
            this.AgregaEnt.Text = "Agregar";
            this.AgregaEnt.UseVisualStyleBackColor = true;
            this.AgregaEnt.Click += new System.EventHandler(this.AgregaEnt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EntNueva);
            this.groupBox1.Controls.Add(this.EliminaEnt);
            this.groupBox1.Controls.Add(this.ModEnt);
            this.groupBox1.Controls.Add(this.AgregaEnt);
            this.groupBox1.Location = new System.Drawing.Point(346, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 50);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entidad";
            // 
            // EntNueva
            // 
            this.EntNueva.Enabled = false;
            this.EntNueva.Location = new System.Drawing.Point(9, 19);
            this.EntNueva.Name = "EntNueva";
            this.EntNueva.Size = new System.Drawing.Size(100, 20);
            this.EntNueva.TabIndex = 7;
            // 
            // EliminaEnt
            // 
            this.EliminaEnt.Enabled = false;
            this.EliminaEnt.Location = new System.Drawing.Point(277, 17);
            this.EliminaEnt.Name = "EliminaEnt";
            this.EliminaEnt.Size = new System.Drawing.Size(75, 23);
            this.EliminaEnt.TabIndex = 6;
            this.EliminaEnt.Text = "Eliminar";
            this.EliminaEnt.UseVisualStyleBackColor = true;
            // 
            // ModEnt
            // 
            this.ModEnt.Enabled = false;
            this.ModEnt.Location = new System.Drawing.Point(196, 17);
            this.ModEnt.Name = "ModEnt";
            this.ModEnt.Size = new System.Drawing.Size(75, 23);
            this.ModEnt.TabIndex = 5;
            this.ModEnt.Text = "Modificar";
            this.ModEnt.UseVisualStyleBackColor = true;
            this.ModEnt.Click += new System.EventHandler(this.ModEnt_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.BGuardar);
            this.groupBox2.Controls.Add(this.BNuevo);
            this.groupBox2.Location = new System.Drawing.Point(18, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(255, 50);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Archivo";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(169, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // BGuardar
            // 
            this.BGuardar.Location = new System.Drawing.Point(87, 17);
            this.BGuardar.Name = "BGuardar";
            this.BGuardar.Size = new System.Drawing.Size(75, 23);
            this.BGuardar.TabIndex = 6;
            this.BGuardar.Text = "Guardar";
            this.BGuardar.UseVisualStyleBackColor = true;
            this.BGuardar.Click += new System.EventHandler(this.BGuardar_Click);
            // 
            // BNuevo
            // 
            this.BNuevo.Location = new System.Drawing.Point(6, 17);
            this.BNuevo.Name = "BNuevo";
            this.BNuevo.Size = new System.Drawing.Size(75, 23);
            this.BNuevo.TabIndex = 5;
            this.BNuevo.Text = "Nuevo";
            this.BNuevo.UseVisualStyleBackColor = true;
            this.BNuevo.Click += new System.EventHandler(this.BNuevo_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.NuevoAtrib);
            this.groupBox3.Controls.Add(this.ElimAtrib);
            this.groupBox3.Controls.Add(this.ModifAtrib);
            this.groupBox3.Controls.Add(this.AgregarAtrib);
            this.groupBox3.Location = new System.Drawing.Point(755, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(360, 50);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Atributos";
            // 
            // NuevoAtrib
            // 
            this.NuevoAtrib.Enabled = false;
            this.NuevoAtrib.Location = new System.Drawing.Point(6, 19);
            this.NuevoAtrib.Name = "NuevoAtrib";
            this.NuevoAtrib.Size = new System.Drawing.Size(100, 20);
            this.NuevoAtrib.TabIndex = 8;
            // 
            // ElimAtrib
            // 
            this.ElimAtrib.Enabled = false;
            this.ElimAtrib.Location = new System.Drawing.Point(279, 17);
            this.ElimAtrib.Name = "ElimAtrib";
            this.ElimAtrib.Size = new System.Drawing.Size(75, 23);
            this.ElimAtrib.TabIndex = 7;
            this.ElimAtrib.Text = "Eliminar";
            this.ElimAtrib.UseVisualStyleBackColor = true;
            // 
            // ModifAtrib
            // 
            this.ModifAtrib.Enabled = false;
            this.ModifAtrib.Location = new System.Drawing.Point(198, 17);
            this.ModifAtrib.Name = "ModifAtrib";
            this.ModifAtrib.Size = new System.Drawing.Size(75, 23);
            this.ModifAtrib.TabIndex = 1;
            this.ModifAtrib.Text = "Modificar";
            this.ModifAtrib.UseVisualStyleBackColor = true;
            // 
            // AgregarAtrib
            // 
            this.AgregarAtrib.Enabled = false;
            this.AgregarAtrib.Location = new System.Drawing.Point(117, 17);
            this.AgregarAtrib.Name = "AgregarAtrib";
            this.AgregarAtrib.Size = new System.Drawing.Size(75, 23);
            this.AgregarAtrib.TabIndex = 0;
            this.AgregarAtrib.Text = "Agregar";
            this.AgregarAtrib.UseVisualStyleBackColor = true;
            // 
            // DGEntidad
            // 
            this.DGEntidad.AllowUserToAddRows = false;
            this.DGEntidad.AllowUserToDeleteRows = false;
            this.DGEntidad.AllowUserToOrderColumns = true;
            this.DGEntidad.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGEntidad.BackgroundColor = System.Drawing.Color.Silver;
            this.DGEntidad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGEntidad.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NombEnt,
            this.DirEnt,
            this.DirAtrib,
            this.DirDatos,
            this.DirEntS});
            this.DGEntidad.Location = new System.Drawing.Point(12, 59);
            this.DGEntidad.Name = "DGEntidad";
            this.DGEntidad.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGEntidad.Size = new System.Drawing.Size(443, 392);
            this.DGEntidad.TabIndex = 5;
            // 
            // NombEnt
            // 
            this.NombEnt.HeaderText = "Nombre de la Entidad";
            this.NombEnt.Name = "NombEnt";
            // 
            // DirEnt
            // 
            this.DirEnt.HeaderText = "Dirección de Entidad";
            this.DirEnt.Name = "DirEnt";
            // 
            // DirAtrib
            // 
            this.DirAtrib.HeaderText = "Dirección de Atributos";
            this.DirAtrib.Name = "DirAtrib";
            // 
            // DirDatos
            // 
            this.DirDatos.HeaderText = "Dirección de Datos";
            this.DirDatos.Name = "DirDatos";
            // 
            // DirEntS
            // 
            this.DirEntS.HeaderText = "Dirección de Siguiente Entidad";
            this.DirEntS.Name = "DirEntS";
            // 
            // dataGridAtributos
            // 
            this.dataGridAtributos.BackgroundColor = System.Drawing.Color.Silver;
            this.dataGridAtributos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAtributos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NomA,
            this.DirA,
            this.TipoDato,
            this.LongA,
            this.TipoIndice,
            this.DirInd,
            this.DirSA});
            this.dataGridAtributos.Location = new System.Drawing.Point(512, 59);
            this.dataGridAtributos.Name = "dataGridAtributos";
            this.dataGridAtributos.Size = new System.Drawing.Size(603, 392);
            this.dataGridAtributos.TabIndex = 6;
            // 
            // NomA
            // 
            this.NomA.HeaderText = "Nombre del Atributo";
            this.NomA.Name = "NomA";
            this.NomA.Width = 80;
            // 
            // DirA
            // 
            this.DirA.HeaderText = "Dirección del Atributo";
            this.DirA.Name = "DirA";
            this.DirA.Width = 80;
            // 
            // TipoDato
            // 
            this.TipoDato.HeaderText = "Tipo de Dato";
            this.TipoDato.Name = "TipoDato";
            this.TipoDato.Width = 80;
            // 
            // LongA
            // 
            this.LongA.HeaderText = "Longitud";
            this.LongA.Name = "LongA";
            this.LongA.Width = 70;
            // 
            // TipoIndice
            // 
            this.TipoIndice.HeaderText = "Tipo de Indice";
            this.TipoIndice.Name = "TipoIndice";
            this.TipoIndice.Width = 80;
            // 
            // DirInd
            // 
            this.DirInd.HeaderText = "Dirección de Indice";
            this.DirInd.Name = "DirInd";
            this.DirInd.Width = 80;
            // 
            // DirSA
            // 
            this.DirSA.HeaderText = "Dirección de Siguiente Atributo";
            this.DirSA.Name = "DirSA";
            this.DirSA.Width = 90;
            // 
            // Proyecto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1127, 456);
            this.Controls.Add(this.dataGridAtributos);
            this.Controls.Add(this.DGEntidad);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Proyecto";
            this.Text = "Proyecto";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGEntidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAtributos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AgregaEnt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button EliminaEnt;
        private System.Windows.Forms.Button ModEnt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BNuevo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button ElimAtrib;
        private System.Windows.Forms.Button ModifAtrib;
        private System.Windows.Forms.Button AgregarAtrib;
        private System.Windows.Forms.DataGridView DGEntidad;
        private System.Windows.Forms.DataGridView dataGridAtributos;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BGuardar;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombEnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn DirEnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn DirAtrib;
        private System.Windows.Forms.DataGridViewTextBoxColumn DirDatos;
        private System.Windows.Forms.DataGridViewTextBoxColumn DirEntS;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomA;
        private System.Windows.Forms.DataGridViewTextBoxColumn DirA;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDato;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongA;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoIndice;
        private System.Windows.Forms.DataGridViewTextBoxColumn DirInd;
        private System.Windows.Forms.DataGridViewTextBoxColumn DirSA;
        private System.Windows.Forms.TextBox EntNueva;
        private System.Windows.Forms.TextBox NuevoAtrib;
    }
}

