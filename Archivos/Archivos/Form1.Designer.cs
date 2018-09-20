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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Proyecto));
            this.AgregaEnt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NomNuevo = new System.Windows.Forms.TextBox();
            this.EntNueva = new System.Windows.Forms.TextBox();
            this.EliminaEnt = new System.Windows.Forms.Button();
            this.ModEnt = new System.Windows.Forms.Button();
            this.ListNombres = new System.Windows.Forms.ComboBox();
            this.cab = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.NA = new System.Windows.Forms.Label();
            this.txtTDato = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CBIndice = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CBDatos = new System.Windows.Forms.ComboBox();
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
            this.DGAtributos = new System.Windows.Forms.DataGridView();
            this.NomA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoIndice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirInd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirSA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ListaAtributos = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGEntidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGAtributos)).BeginInit();
            this.menuStrip1.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.NomNuevo);
            this.groupBox1.Controls.Add(this.EntNueva);
            this.groupBox1.Controls.Add(this.EliminaEnt);
            this.groupBox1.Controls.Add(this.ModEnt);
            this.groupBox1.Controls.Add(this.AgregaEnt);
            this.groupBox1.Location = new System.Drawing.Point(12, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(391, 76);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entidad";
            // 
            // NomNuevo
            // 
            this.NomNuevo.Enabled = false;
            this.NomNuevo.Location = new System.Drawing.Point(208, 17);
            this.NomNuevo.Name = "NomNuevo";
            this.NomNuevo.Size = new System.Drawing.Size(100, 20);
            this.NomNuevo.TabIndex = 8;
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
            this.EliminaEnt.Location = new System.Drawing.Point(115, 47);
            this.EliminaEnt.Name = "EliminaEnt";
            this.EliminaEnt.Size = new System.Drawing.Size(75, 23);
            this.EliminaEnt.TabIndex = 6;
            this.EliminaEnt.Text = "Eliminar";
            this.EliminaEnt.UseVisualStyleBackColor = true;
            this.EliminaEnt.Click += new System.EventHandler(this.EliminaEnt_Click);
            // 
            // ModEnt
            // 
            this.ModEnt.Enabled = false;
            this.ModEnt.Location = new System.Drawing.Point(314, 14);
            this.ModEnt.Name = "ModEnt";
            this.ModEnt.Size = new System.Drawing.Size(75, 23);
            this.ModEnt.TabIndex = 5;
            this.ModEnt.Text = "Modificar";
            this.ModEnt.UseVisualStyleBackColor = true;
            this.ModEnt.Click += new System.EventHandler(this.ModEnt_Click);
            // 
            // ListNombres
            // 
            this.ListNombres.Enabled = false;
            this.ListNombres.FormattingEnabled = true;
            this.ListNombres.Location = new System.Drawing.Point(468, 36);
            this.ListNombres.Name = "ListNombres";
            this.ListNombres.Size = new System.Drawing.Size(92, 21);
            this.ListNombres.TabIndex = 9;
            this.ListNombres.SelectedIndexChanged += new System.EventHandler(this.ListNombres_SelectedIndexChanged);
            // 
            // cab
            // 
            this.cab.AutoSize = true;
            this.cab.Location = new System.Drawing.Point(139, 9);
            this.cab.Name = "cab";
            this.cab.Size = new System.Drawing.Size(16, 13);
            this.cab.TabIndex = 9;
            this.cab.Text = "-1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Cabecera: ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.NA);
            this.groupBox3.Controls.Add(this.txtTDato);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.CBIndice);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.CBDatos);
            this.groupBox3.Controls.Add(this.NuevoAtrib);
            this.groupBox3.Controls.Add(this.ElimAtrib);
            this.groupBox3.Controls.Add(this.ModifAtrib);
            this.groupBox3.Controls.Add(this.AgregarAtrib);
            this.groupBox3.Location = new System.Drawing.Point(577, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(444, 76);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Atributos";
            // 
            // NA
            // 
            this.NA.AutoSize = true;
            this.NA.Location = new System.Drawing.Point(6, 20);
            this.NA.Name = "NA";
            this.NA.Size = new System.Drawing.Size(47, 13);
            this.NA.TabIndex = 15;
            this.NA.Text = "Nombre:";
            // 
            // txtTDato
            // 
            this.txtTDato.Enabled = false;
            this.txtTDato.Location = new System.Drawing.Point(328, 45);
            this.txtTDato.Name = "txtTDato";
            this.txtTDato.Size = new System.Drawing.Size(72, 20);
            this.txtTDato.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Long de dato:";
            // 
            // CBIndice
            // 
            this.CBIndice.Enabled = false;
            this.CBIndice.FormattingEnabled = true;
            this.CBIndice.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.CBIndice.Location = new System.Drawing.Point(354, 14);
            this.CBIndice.Name = "CBIndice";
            this.CBIndice.Size = new System.Drawing.Size(35, 21);
            this.CBIndice.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(269, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Tipo de índice:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Tipo de Dato:";
            // 
            // CBDatos
            // 
            this.CBDatos.Enabled = false;
            this.CBDatos.FormattingEnabled = true;
            this.CBDatos.Items.AddRange(new object[] {
            "E",
            "C"});
            this.CBDatos.Location = new System.Drawing.Point(229, 14);
            this.CBDatos.Name = "CBDatos";
            this.CBDatos.Size = new System.Drawing.Size(34, 21);
            this.CBDatos.TabIndex = 9;
            // 
            // NuevoAtrib
            // 
            this.NuevoAtrib.Enabled = false;
            this.NuevoAtrib.Location = new System.Drawing.Point(59, 17);
            this.NuevoAtrib.Name = "NuevoAtrib";
            this.NuevoAtrib.Size = new System.Drawing.Size(86, 20);
            this.NuevoAtrib.TabIndex = 8;
            // 
            // ElimAtrib
            // 
            this.ElimAtrib.Enabled = false;
            this.ElimAtrib.Location = new System.Drawing.Point(168, 43);
            this.ElimAtrib.Name = "ElimAtrib";
            this.ElimAtrib.Size = new System.Drawing.Size(75, 23);
            this.ElimAtrib.TabIndex = 7;
            this.ElimAtrib.Text = "Eliminar";
            this.ElimAtrib.UseVisualStyleBackColor = true;
            this.ElimAtrib.Click += new System.EventHandler(this.ElimAtrib_Click);
            // 
            // ModifAtrib
            // 
            this.ModifAtrib.Enabled = false;
            this.ModifAtrib.Location = new System.Drawing.Point(87, 43);
            this.ModifAtrib.Name = "ModifAtrib";
            this.ModifAtrib.Size = new System.Drawing.Size(75, 23);
            this.ModifAtrib.TabIndex = 1;
            this.ModifAtrib.Text = "Modificar";
            this.ModifAtrib.UseVisualStyleBackColor = true;
            this.ModifAtrib.Click += new System.EventHandler(this.ModifAtrib_Click);
            // 
            // AgregarAtrib
            // 
            this.AgregarAtrib.Enabled = false;
            this.AgregarAtrib.Location = new System.Drawing.Point(6, 43);
            this.AgregarAtrib.Name = "AgregarAtrib";
            this.AgregarAtrib.Size = new System.Drawing.Size(75, 23);
            this.AgregarAtrib.TabIndex = 0;
            this.AgregarAtrib.Text = "Agregar";
            this.AgregarAtrib.UseVisualStyleBackColor = true;
            this.AgregarAtrib.Click += new System.EventHandler(this.AgregarAtrib_Click);
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
            this.DGEntidad.Location = new System.Drawing.Point(12, 107);
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
            // DGAtributos
            // 
            this.DGAtributos.AllowUserToAddRows = false;
            this.DGAtributos.AllowUserToDeleteRows = false;
            this.DGAtributos.BackgroundColor = System.Drawing.Color.Silver;
            this.DGAtributos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGAtributos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NomA,
            this.DirA,
            this.TipoDato,
            this.LongA,
            this.TipoIndice,
            this.DirInd,
            this.DirSA});
            this.DGAtributos.Location = new System.Drawing.Point(486, 107);
            this.DGAtributos.Name = "DGAtributos";
            this.DGAtributos.Size = new System.Drawing.Size(603, 392);
            this.DGAtributos.TabIndex = 6;
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1103, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.toolStripSeparator2,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "&Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("nuevoToolStripMenuItem.Image")));
            this.nuevoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.nuevoToolStripMenuItem.Text = "&Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click_1);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("abrirToolStripMenuItem.Image")));
            this.abrirToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.abrirToolStripMenuItem.Text = "&Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("guardarToolStripMenuItem.Image")));
            this.guardarToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.guardarToolStripMenuItem.Text = "&Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(153, 6);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.salirToolStripMenuItem.Text = "&Cerrar Archivo";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(421, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Entidad:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(421, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Atributo:";
            // 
            // ListaAtributos
            // 
            this.ListaAtributos.Enabled = false;
            this.ListaAtributos.FormattingEnabled = true;
            this.ListaAtributos.Location = new System.Drawing.Point(468, 70);
            this.ListaAtributos.Name = "ListaAtributos";
            this.ListaAtributos.Size = new System.Drawing.Size(92, 21);
            this.ListaAtributos.TabIndex = 12;
            // 
            // Proyecto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 525);
            this.Controls.Add(this.ListaAtributos);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ListNombres);
            this.Controls.Add(this.DGAtributos);
            this.Controls.Add(this.cab);
            this.Controls.Add(this.DGEntidad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Proyecto";
            this.Text = "Proyecto";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGEntidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGAtributos)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AgregaEnt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button EliminaEnt;
        private System.Windows.Forms.Button ModEnt;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button ElimAtrib;
        private System.Windows.Forms.Button ModifAtrib;
        private System.Windows.Forms.Button AgregarAtrib;
        private System.Windows.Forms.DataGridView DGEntidad;
        private System.Windows.Forms.DataGridView DGAtributos;
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
        private System.Windows.Forms.Label cab;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ListNombres;
        private System.Windows.Forms.TextBox NomNuevo;
        private System.Windows.Forms.Label NA;
        private System.Windows.Forms.TextBox txtTDato;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CBIndice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CBDatos;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox ListaAtributos;
    }
}

