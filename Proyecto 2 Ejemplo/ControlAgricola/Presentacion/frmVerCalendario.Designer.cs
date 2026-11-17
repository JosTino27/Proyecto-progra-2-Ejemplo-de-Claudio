namespace ControlAgricola.Presentacion
{
    partial class frmVerCalendario
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
            this.calAplicaciones = new System.Windows.Forms.MonthCalendar();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.lblDia = new System.Windows.Forms.Label();
            this.grdDia = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdDia)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(291, 23);
            this.lblTitulo.Text = "Calendario de Aplicaciones";
            this.lblTitulo.TabIndex = 0;
            // 
            // calAplicaciones
            // 
            this.calAplicaciones.CalendarDimensions = new System.Drawing.Size(2, 2);
            this.calAplicaciones.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calAplicaciones.Location = new System.Drawing.Point(20, 60);
            this.calAplicaciones.MaxSelectionCount = 1;
            this.calAplicaciones.Name = "calAplicaciones";
            this.calAplicaciones.TabIndex = 1;
            this.calAplicaciones.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calAplicaciones_DateSelected);
            // 
            // lblAyuda
            // 
            this.lblAyuda.AutoSize = true;
            this.lblAyuda.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyuda.Location = new System.Drawing.Point(20, 400);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(405, 16);
            this.lblAyuda.Text = "Los días en negrita tienen aplicaciones programadas.";
            this.lblAyuda.TabIndex = 2;
            // 
            // lblDia
            // 
            this.lblDia.AutoSize = true;
            this.lblDia.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDia.Location = new System.Drawing.Point(540, 63);
            this.lblDia.Name = "lblDia";
            this.lblDia.Size = new System.Drawing.Size(163, 16);
            this.lblDia.Text = "Aplicaciones del día:";
            this.lblDia.TabIndex = 3;
            // 
            // grdDia
            // 
            this.grdDia.AllowUserToAddRows = false;
            this.grdDia.AllowUserToDeleteRows = false;
            this.grdDia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdDia.BackgroundColor = System.Drawing.Color.White;
            this.grdDia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDia.Location = new System.Drawing.Point(540, 90);
            this.grdDia.MultiSelect = false;
            this.grdDia.Name = "grdDia";
            this.grdDia.ReadOnly = true;
            this.grdDia.RowHeadersVisible = false;
            this.grdDia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDia.Size = new System.Drawing.Size(440, 460);
            this.grdDia.TabIndex = 4;
            // 
            // frmVerCalendario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.grdDia);
            this.Controls.Add(this.lblDia);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.calAplicaciones);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmVerCalendario";
            this.Text = "Calendario de Aplicaciones";
            this.Load += new System.EventHandler(this.frmVerCalendario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdDia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.MonthCalendar calAplicaciones;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.Label lblDia;
        private System.Windows.Forms.DataGridView grdDia;
    }
}
