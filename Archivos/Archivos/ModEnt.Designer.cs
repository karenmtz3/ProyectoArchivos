namespace Archivos
{
    partial class ModEnt
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
            this.cbNomEnt = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNuevNombre = new System.Windows.Forms.TextBox();
            this.btnGuarda = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbNomEnt
            // 
            this.cbNomEnt.FormattingEnabled = true;
            this.cbNomEnt.Location = new System.Drawing.Point(89, 35);
            this.cbNomEnt.Name = "cbNomEnt";
            this.cbNomEnt.Size = new System.Drawing.Size(121, 21);
            this.cbNomEnt.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nuevo Nombre:";
            // 
            // txtNuevNombre
            // 
            this.txtNuevNombre.Location = new System.Drawing.Point(110, 75);
            this.txtNuevNombre.Name = "txtNuevNombre";
            this.txtNuevNombre.Size = new System.Drawing.Size(100, 20);
            this.txtNuevNombre.TabIndex = 2;
            // 
            // btnGuarda
            // 
            this.btnGuarda.Location = new System.Drawing.Point(244, 47);
            this.btnGuarda.Name = "btnGuarda";
            this.btnGuarda.Size = new System.Drawing.Size(67, 35);
            this.btnGuarda.TabIndex = 3;
            this.btnGuarda.Text = "Guardar Cambio";
            this.btnGuarda.UseVisualStyleBackColor = true;
            this.btnGuarda.Click += new System.EventHandler(this.btnGuarda_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(240, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Selecciona la entidad a modificar";
            // 
            // ModEnt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 110);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGuarda);
            this.Controls.Add(this.txtNuevNombre);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbNomEnt);
            this.Name = "ModEnt";
            this.Text = "Modifca Entidad";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbNomEnt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNuevNombre;
        private System.Windows.Forms.Button btnGuarda;
        private System.Windows.Forms.Label label2;
    }
}