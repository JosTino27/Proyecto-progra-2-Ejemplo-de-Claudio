namespace ControlAgricola.Presentacion
{
    partial class frmReporteAgroquimicos
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
            this.grpFiltros = new System.Windows.Forms.GroupBox();
            this.lblFechaInicial = new System.Windows.Forms.Label();
            this.cboDiaInicial = new System.Windows.Forms.ComboBox();
            this.cboMesInicial = new System.Windows.Forms.ComboBox();
            this.cboAnioInicial = new System.Windows.Forms.ComboBox();
            this.lblFechaFinal = new System.Windows.Forms.Label();
            this.cboDiaFinal = new System.Windows.Forms.ComboBox();
            this.cboMesFinal = new System.Windows.Forms.ComboBox();
            this.cboAnioFinal = new System.Windows.Forms.ComboBox();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.dgvReporte = new System.Windows.Forms.DataGridView();
            this.grpFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).BeginInit();
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
            this.label1.Text = "LISTADO DE AGROQUÍMICOS POR FECHAS";
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
            // grpFiltros
            // 
            this.grpFiltros.Controls.Add(this.lblFechaInicial);
            this.grpFiltros.Controls.Add(this.cboDiaInicial);
            this.grpFiltros.Controls.Add(this.cboMesInicial);
            this.grpFiltros.Controls.Add(this.cboAnioInicial);
            this.grpFiltros.Controls.Add(this.lblFechaFinal);
            this.grpFiltros.Controls.Add(this.cboDiaFinal);
            this.grpFiltros.Controls.Add(this.cboMesFinal);
            this.grpFiltros.Controls.Add(this.cboAnioFinal);
            this.grpFiltros.Controls.Add(this.btnGenerar);
            this.grpFiltros.Controls.Add(this.btnLimpiar);
            this.grpFiltros.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFiltros.Location = new System.Drawing.Point(34, 100);
            this.grpFiltros.Name = "grpFiltros";
            this.grpFiltros.Size = new System.Drawing.Size(774, 110);
            this.grpFiltros.TabIndex = 2;
            this.grpFiltros.TabStop = false;
            this.grpFiltros.Text = "Rango de fechas";
            // 
            // lblFechaInicial
            // 
            this.lblFechaInicial.AutoSize = true;
            this.lblFechaInicial.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaInicial.Location = new System.Drawing.Point(6, 33);
            this.lblFechaInicial.Name = "lblFechaInicial";
            this.lblFechaInicial.Size = new System.Drawing.Size(100, 19);
            this.lblFechaInicial.TabIndex = 3;
            this.lblFechaInicial.Text = "Fecha inicial:";
            // 
            // cboDiaInicial
            // 
            this.cboDiaInicial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiaInicial.FormattingEnabled = true;
            this.cboDiaInicial.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.cboDiaInicial.Location = new System.Drawing.Point(181, 33);
            this.cboDiaInicial.Name = "cboDiaInicial";
            this.cboDiaInicial.Size = new System.Drawing.Size(50, 24);
            this.cboDiaInicial.TabIndex = 4;
            // 
            // cboMesInicial
            // 
            this.cboMesInicial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMesInicial.FormattingEnabled = true;
            this.cboMesInicial.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.cboMesInicial.Location = new System.Drawing.Point(236, 33);
            this.cboMesInicial.Name = "cboMesInicial";
            this.cboMesInicial.Size = new System.Drawing.Size(50, 24);
            this.cboMesInicial.TabIndex = 5;
            // 
            // cboAnioInicial
            // 
            this.cboAnioInicial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAnioInicial.FormattingEnabled = true;
            this.cboAnioInicial.Items.AddRange(new object[] {
            "2020",
            "2021",
            "2022",
            "2023",
            "2024",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030",
            "2031",
            "2032",
            "2033",
            "2034",
            "2035"});
            this.cboAnioInicial.Location = new System.Drawing.Point(291, 33);
            this.cboAnioInicial.Name = "cboAnioInicial";
            this.cboAnioInicial.Size = new System.Drawing.Size(66, 24);
            this.cboAnioInicial.TabIndex = 6;
            // 
            // lblFechaFinal
            // 
            this.lblFechaFinal.AutoSize = true;
            this.lblFechaFinal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaFinal.Location = new System.Drawing.Point(6, 70);
            this.lblFechaFinal.Name = "lblFechaFinal";
            this.lblFechaFinal.Size = new System.Drawing.Size(100, 19);
            this.lblFechaFinal.TabIndex = 7;
            this.lblFechaFinal.Text = "Fecha final:";
            // 
            // cboDiaFinal
            // 
            this.cboDiaFinal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiaFinal.FormattingEnabled = true;
            this.cboDiaFinal.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.cboDiaFinal.Location = new System.Drawing.Point(181, 70);
            this.cboDiaFinal.Name = "cboDiaFinal";
            this.cboDiaFinal.Size = new System.Drawing.Size(50, 24);
            this.cboDiaFinal.TabIndex = 8;
            // 
            // cboMesFinal
            // 
            this.cboMesFinal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMesFinal.FormattingEnabled = true;
            this.cboMesFinal.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.cboMesFinal.Location = new System.Drawing.Point(236, 70);
            this.cboMesFinal.Name = "cboMesFinal";
            this.cboMesFinal.Size = new System.Drawing.Size(50, 24);
            this.cboMesFinal.TabIndex = 9;
            // 
            // cboAnioFinal
            // 
            this.cboAnioFinal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAnioFinal.FormattingEnabled = true;
            this.cboAnioFinal.Items.AddRange(new object[] {
            "2020",
            "2021",
            "2022",
            "2023",
            "2024",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030",
            "2031",
            "2032",
            "2033",
            "2034",
            "2035"});
            this.cboAnioFinal.Location = new System.Drawing.Point(291, 70);
            this.cboAnioFinal.Name = "cboAnioFinal";
            this.cboAnioFinal.Size = new System.Drawing.Size(66, 24);
            this.cboAnioFinal.TabIndex = 10;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerar.Image = global::ControlAgricola.Properties.Resources.gtk_index;
            this.btnGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerar.Location = new System.Drawing.Point(420, 25);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(150, 38);
            this.btnGenerar.TabIndex = 11;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Image = global::ControlAgricola.Properties.Resources.edit_clear;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(420, 65);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(150, 38);
            this.btnLimpiar.TabIndex = 12;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // dgvReporte
            // 
            this.dgvReporte.AllowUserToAddRows = false;
            this.dgvReporte.AllowUserToDeleteRows = false;
            this.dgvReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReporte.Location = new System.Drawing.Point(34, 230);
            this.dgvReporte.Name = "dgvReporte";
            this.dgvReporte.ReadOnly = true;
            this.dgvReporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReporte.Size = new System.Drawing.Size(992, 500);
            this.dgvReporte.TabIndex = 13;
            // 
            // frmReporteAgroquimicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 760);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.grpFiltros);
            this.Controls.Add(this.dgvReporte);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "frmReporteAgroquimicos";
            this.Text = "Reporte de agroquímicos";
            this.Load += new System.EventHandler(this.frmReporteAgroquimicos_Load);
            this.grpFiltros.ResumeLayout(false);
            this.grpFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox grpFiltros;
        private System.Windows.Forms.Label lblFechaInicial;
        private System.Windows.Forms.ComboBox cboDiaInicial;
        private System.Windows.Forms.ComboBox cboMesInicial;
        private System.Windows.Forms.ComboBox cboAnioInicial;
        private System.Windows.Forms.Label lblFechaFinal;
        private System.Windows.Forms.ComboBox cboDiaFinal;
        private System.Windows.Forms.ComboBox cboMesFinal;
        private System.Windows.Forms.ComboBox cboAnioFinal;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvReporte;
    }
}
