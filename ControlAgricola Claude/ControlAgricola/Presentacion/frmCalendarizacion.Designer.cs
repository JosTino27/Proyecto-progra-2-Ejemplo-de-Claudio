namespace ControlAgricola.Presentacion
{
    partial class frmCalendarizacion
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.grpCultivo = new System.Windows.Forms.GroupBox();
            this.lblCultivo = new System.Windows.Forms.Label();
            this.cboCultivo = new System.Windows.Forms.ComboBox();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.btnVer = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.dgvCalendario = new System.Windows.Forms.DataGridView();
            this.grpCultivo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendario)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "CALENDARIZACIÓN DE APLICACIONES";
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Image = global::ControlAgricola.Properties.Resources.gnome_shutdown;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(898, 32);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(158, 41);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // grpCultivo
            // 
            this.grpCultivo.Controls.Add(this.lblCultivo);
            this.grpCultivo.Controls.Add(this.cboCultivo);
            this.grpCultivo.Controls.Add(this.btnCalcular);
            this.grpCultivo.Controls.Add(this.btnVer);
            this.grpCultivo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCultivo.Location = new System.Drawing.Point(34, 100);
            this.grpCultivo.Name = "grpCultivo";
            this.grpCultivo.Size = new System.Drawing.Size(774, 79);
            this.grpCultivo.TabIndex = 2;
            this.grpCultivo.TabStop = false;
            this.grpCultivo.Text = "Seleccione el cultivo";
            // 
            // lblCultivo
            // 
            this.lblCultivo.AutoSize = true;
            this.lblCultivo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCultivo.Location = new System.Drawing.Point(6, 33);
            this.lblCultivo.Name = "lblCultivo";
            this.lblCultivo.Size = new System.Drawing.Size(100, 19);
            this.lblCultivo.TabIndex = 3;
            this.lblCultivo.Text = "Cultivo:";
            // 
            // cboCultivo
            // 
            this.cboCultivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCultivo.FormattingEnabled = true;
            this.cboCultivo.Location = new System.Drawing.Point(100, 33);
            this.cboCultivo.Name = "cboCultivo";
            this.cboCultivo.Size = new System.Drawing.Size(220, 24);
            this.cboCultivo.TabIndex = 4;
            // 
            // btnCalcular
            // 
            this.btnCalcular.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalcular.Image = global::ControlAgricola.Properties.Resources.gtk_execute;
            this.btnCalcular.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCalcular.Location = new System.Drawing.Point(345, 25);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(180, 38);
            this.btnCalcular.TabIndex = 5;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // btnVer
            // 
            this.btnVer.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVer.Image = global::ControlAgricola.Properties.Resources.search;
            this.btnVer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVer.Location = new System.Drawing.Point(540, 25);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(180, 38);
            this.btnVer.TabIndex = 6;
            this.btnVer.Text = "Ver calendario";
            this.btnVer.UseVisualStyleBackColor = true;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(34, 198);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(100, 19);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "Total de aplicaciones:";
            // 
            // txtTotal
            // 
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Location = new System.Drawing.Point(240, 197);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(80, 23);
            this.txtTotal.TabIndex = 8;
            // 
            // dgvCalendario
            // 
            this.dgvCalendario.AllowUserToAddRows = false;
            this.dgvCalendario.AllowUserToDeleteRows = false;
            this.dgvCalendario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalendario.Location = new System.Drawing.Point(34, 235);
            this.dgvCalendario.Name = "dgvCalendario";
            this.dgvCalendario.ReadOnly = true;
            this.dgvCalendario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCalendario.Size = new System.Drawing.Size(992, 495);
            this.dgvCalendario.TabIndex = 9;
            // 
            // frmCalendarizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 760);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.grpCultivo);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.dgvCalendario);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "frmCalendarizacion";
            this.Text = "Calendarización";
            this.Load += new System.EventHandler(this.frmCalendarizacion_Load);
            this.grpCultivo.ResumeLayout(false);
            this.grpCultivo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox grpCultivo;
        private System.Windows.Forms.Label lblCultivo;
        private System.Windows.Forms.ComboBox cboCultivo;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.DataGridView dgvCalendario;
    }
}
