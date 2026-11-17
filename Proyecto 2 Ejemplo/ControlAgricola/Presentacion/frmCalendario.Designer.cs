namespace ControlAgricola.Presentacion
{
    partial class frmCalendario
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblCultivo = new System.Windows.Forms.Label();
            this.cmbCultivo = new System.Windows.Forms.ComboBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.btnCalendarizar = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grdCalendario = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdCalendario)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(347, 23);
            this.lblTitulo.Text = "Calendarización de Aplicaciones";
            this.lblTitulo.TabIndex = 0;
            // 
            // lblCultivo
            // 
            this.lblCultivo.AutoSize = true;
            this.lblCultivo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCultivo.Location = new System.Drawing.Point(20, 63);
            this.lblCultivo.Name = "lblCultivo";
            this.lblCultivo.Size = new System.Drawing.Size(62, 16);
            this.lblCultivo.Text = "Cultivo:";
            this.lblCultivo.TabIndex = 1;
            // 
            // cmbCultivo
            // 
            this.cmbCultivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCultivo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCultivo.FormattingEnabled = true;
            this.cmbCultivo.Location = new System.Drawing.Point(90, 60);
            this.cmbCultivo.Name = "cmbCultivo";
            this.cmbCultivo.Size = new System.Drawing.Size(250, 24);
            this.cmbCultivo.TabIndex = 2;
            // 
            // btnConsultar
            // 
            this.btnConsultar.BackColor = System.Drawing.Color.White;
            this.btnConsultar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultar.Location = new System.Drawing.Point(355, 57);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(150, 30);
            this.btnConsultar.Text = "Ver calendario";
            this.btnConsultar.UseVisualStyleBackColor = false;
            this.btnConsultar.TabIndex = 3;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnCalendarizar
            // 
            this.btnCalendarizar.BackColor = System.Drawing.Color.White;
            this.btnCalendarizar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnCalendarizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalendarizar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalendarizar.Location = new System.Drawing.Point(515, 57);
            this.btnCalendarizar.Name = "btnCalendarizar";
            this.btnCalendarizar.Size = new System.Drawing.Size(150, 30);
            this.btnCalendarizar.Text = "Calendarizar";
            this.btnCalendarizar.UseVisualStyleBackColor = false;
            this.btnCalendarizar.TabIndex = 4;
            this.btnCalendarizar.Click += new System.EventHandler(this.btnCalendarizar_Click);
            // 
            // btnBorrar
            // 
            this.btnBorrar.BackColor = System.Drawing.Color.White;
            this.btnBorrar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnBorrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBorrar.Location = new System.Drawing.Point(675, 57);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(160, 30);
            this.btnBorrar.Text = "Borrar calendario";
            this.btnBorrar.UseVisualStyleBackColor = false;
            this.btnBorrar.TabIndex = 5;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(20, 100);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(639, 16);
            this.lblInfo.Text = "Fecha programada = Fecha de siembra del lote + Días después de siembra del paquete";
            this.lblInfo.TabIndex = 6;
            // 
            // grdCalendario
            // 
            this.grdCalendario.AllowUserToAddRows = false;
            this.grdCalendario.AllowUserToDeleteRows = false;
            this.grdCalendario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grdCalendario.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdCalendario.BackgroundColor = System.Drawing.Color.White;
            this.grdCalendario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCalendario.Location = new System.Drawing.Point(20, 130);
            this.grdCalendario.MultiSelect = false;
            this.grdCalendario.Name = "grdCalendario";
            this.grdCalendario.ReadOnly = true;
            this.grdCalendario.RowHeadersVisible = false;
            this.grdCalendario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdCalendario.Size = new System.Drawing.Size(960, 420);
            this.grdCalendario.TabIndex = 7;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(20, 565);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(210, 16);
            this.lblTotal.Text = "Aplicaciones programadas: 0";
            this.lblTotal.TabIndex = 8;
            // 
            // frmCalendario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.grdCalendario);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnBorrar);
            this.Controls.Add(this.btnCalendarizar);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.cmbCultivo);
            this.Controls.Add(this.lblCultivo);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCalendario";
            this.Text = "Calendarización";
            this.Load += new System.EventHandler(this.frmCalendario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdCalendario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblCultivo;
        private System.Windows.Forms.ComboBox cmbCultivo;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Button btnCalendarizar;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.DataGridView grdCalendario;
        private System.Windows.Forms.Label lblTotal;
    }
}
