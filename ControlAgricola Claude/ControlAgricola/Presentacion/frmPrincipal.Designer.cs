namespace ControlAgricola.Presentacion
{
    partial class frmPrincipal
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
            this.pnlPrincipal = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMantenimientos = new System.Windows.Forms.Button();
            this.pnlSubMantenimientos = new System.Windows.Forms.Panel();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnCultivos = new System.Windows.Forms.Button();
            this.btnLotes = new System.Windows.Forms.Button();
            this.btnAgroquimicos = new System.Windows.Forms.Button();
            this.btnAplicaciones = new System.Windows.Forms.Button();
            this.btnPaquetes = new System.Windows.Forms.Button();
            this.btnCalendarizacion = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.pnlSubReportes = new System.Windows.Forms.Panel();
            this.btnRepCalendario = new System.Windows.Forms.Button();
            this.btnRepAgroquimicos = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTipoUsuario = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlPrincipal.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlSubMantenimientos.SuspendLayout();
            this.pnlSubReportes.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPrincipal
            // 
            this.pnlPrincipal.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pnlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrincipal.Location = new System.Drawing.Point(227, 140);
            this.pnlPrincipal.Name = "pnlPrincipal";
            this.pnlPrincipal.Size = new System.Drawing.Size(1226, 760);
            this.pnlPrincipal.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnMantenimientos);
            this.panel1.Controls.Add(this.pnlSubMantenimientos);
            this.panel1.Controls.Add(this.btnCalendarizacion);
            this.panel1.Controls.Add(this.btnReportes);
            this.panel1.Controls.Add(this.pnlSubReportes);
            this.panel1.Controls.Add(this.btnCerrarSesion);
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 140);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(227, 760);
            this.panel1.TabIndex = 1;
            // 
            // btnMantenimientos
            // 
            this.btnMantenimientos.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnMantenimientos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMantenimientos.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMantenimientos.Image = global::ControlAgricola.Properties.Resources.gtk_preferences;
            this.btnMantenimientos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMantenimientos.Location = new System.Drawing.Point(12, 20);
            this.btnMantenimientos.Name = "btnMantenimientos";
            this.btnMantenimientos.Size = new System.Drawing.Size(200, 44);
            this.btnMantenimientos.TabIndex = 2;
            this.btnMantenimientos.Text = "Mantenimientos";
            this.btnMantenimientos.UseVisualStyleBackColor = false;
            this.btnMantenimientos.Click += new System.EventHandler(this.btnMantenimientos_Click);
            // 
            // pnlSubMantenimientos
            // 
            this.pnlSubMantenimientos.Controls.Add(this.btnUsuarios);
            this.pnlSubMantenimientos.Controls.Add(this.btnCultivos);
            this.pnlSubMantenimientos.Controls.Add(this.btnLotes);
            this.pnlSubMantenimientos.Controls.Add(this.btnAgroquimicos);
            this.pnlSubMantenimientos.Controls.Add(this.btnAplicaciones);
            this.pnlSubMantenimientos.Controls.Add(this.btnPaquetes);
            this.pnlSubMantenimientos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlSubMantenimientos.Location = new System.Drawing.Point(0, 70);
            this.pnlSubMantenimientos.Name = "pnlSubMantenimientos";
            this.pnlSubMantenimientos.Size = new System.Drawing.Size(227, 270);
            this.pnlSubMantenimientos.TabIndex = 3;
            this.pnlSubMantenimientos.Visible = false;
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.Image = global::ControlAgricola.Properties.Resources.system_users;
            this.btnUsuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuarios.Location = new System.Drawing.Point(26, 0);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(175, 40);
            this.btnUsuarios.TabIndex = 4;
            this.btnUsuarios.Text = "Usuarios";
            this.btnUsuarios.UseVisualStyleBackColor = true;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnCultivos
            // 
            this.btnCultivos.Image = global::ControlAgricola.Properties.Resources.stock_data_sources;
            this.btnCultivos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCultivos.Location = new System.Drawing.Point(26, 45);
            this.btnCultivos.Name = "btnCultivos";
            this.btnCultivos.Size = new System.Drawing.Size(175, 40);
            this.btnCultivos.TabIndex = 5;
            this.btnCultivos.Text = "Cultivos";
            this.btnCultivos.UseVisualStyleBackColor = true;
            this.btnCultivos.Click += new System.EventHandler(this.btnCultivos_Click);
            // 
            // btnLotes
            // 
            this.btnLotes.Image = global::ControlAgricola.Properties.Resources.gtk_index;
            this.btnLotes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLotes.Location = new System.Drawing.Point(26, 90);
            this.btnLotes.Name = "btnLotes";
            this.btnLotes.Size = new System.Drawing.Size(175, 40);
            this.btnLotes.TabIndex = 6;
            this.btnLotes.Text = "Lotes";
            this.btnLotes.UseVisualStyleBackColor = true;
            this.btnLotes.Click += new System.EventHandler(this.btnLotes_Click);
            // 
            // btnAgroquimicos
            // 
            this.btnAgroquimicos.Image = global::ControlAgricola.Properties.Resources.accessories_calculator;
            this.btnAgroquimicos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgroquimicos.Location = new System.Drawing.Point(26, 135);
            this.btnAgroquimicos.Name = "btnAgroquimicos";
            this.btnAgroquimicos.Size = new System.Drawing.Size(175, 40);
            this.btnAgroquimicos.TabIndex = 7;
            this.btnAgroquimicos.Text = "Agroquímicos";
            this.btnAgroquimicos.UseVisualStyleBackColor = true;
            this.btnAgroquimicos.Click += new System.EventHandler(this.btnAgroquimicos_Click);
            // 
            // btnAplicaciones
            // 
            this.btnAplicaciones.Image = global::ControlAgricola.Properties.Resources.gtk_execute;
            this.btnAplicaciones.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAplicaciones.Location = new System.Drawing.Point(26, 180);
            this.btnAplicaciones.Name = "btnAplicaciones";
            this.btnAplicaciones.Size = new System.Drawing.Size(175, 40);
            this.btnAplicaciones.TabIndex = 8;
            this.btnAplicaciones.Text = "Aplicaciones";
            this.btnAplicaciones.UseVisualStyleBackColor = true;
            this.btnAplicaciones.Click += new System.EventHandler(this.btnAplicaciones_Click);
            // 
            // btnPaquetes
            // 
            this.btnPaquetes.Image = global::ControlAgricola.Properties.Resources.contacts;
            this.btnPaquetes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPaquetes.Location = new System.Drawing.Point(26, 225);
            this.btnPaquetes.Name = "btnPaquetes";
            this.btnPaquetes.Size = new System.Drawing.Size(175, 40);
            this.btnPaquetes.TabIndex = 9;
            this.btnPaquetes.Text = "Paquetes";
            this.btnPaquetes.UseVisualStyleBackColor = true;
            this.btnPaquetes.Click += new System.EventHandler(this.btnPaquetes_Click);
            // 
            // btnCalendarizacion
            // 
            this.btnCalendarizacion.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCalendarizacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalendarizacion.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalendarizacion.Image = global::ControlAgricola.Properties.Resources.date;
            this.btnCalendarizacion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCalendarizacion.Location = new System.Drawing.Point(12, 355);
            this.btnCalendarizacion.Name = "btnCalendarizacion";
            this.btnCalendarizacion.Size = new System.Drawing.Size(200, 44);
            this.btnCalendarizacion.TabIndex = 10;
            this.btnCalendarizacion.Text = "Calendarización";
            this.btnCalendarizacion.UseVisualStyleBackColor = false;
            this.btnCalendarizacion.Click += new System.EventHandler(this.btnCalendarizacion_Click);
            // 
            // btnReportes
            // 
            this.btnReportes.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportes.Image = global::ControlAgricola.Properties.Resources.gpm_statistics;
            this.btnReportes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportes.Location = new System.Drawing.Point(12, 415);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(200, 44);
            this.btnReportes.TabIndex = 11;
            this.btnReportes.Text = "Reportes";
            this.btnReportes.UseVisualStyleBackColor = false;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // pnlSubReportes
            // 
            this.pnlSubReportes.Controls.Add(this.btnRepCalendario);
            this.pnlSubReportes.Controls.Add(this.btnRepAgroquimicos);
            this.pnlSubReportes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlSubReportes.Location = new System.Drawing.Point(0, 465);
            this.pnlSubReportes.Name = "pnlSubReportes";
            this.pnlSubReportes.Size = new System.Drawing.Size(227, 90);
            this.pnlSubReportes.TabIndex = 12;
            this.pnlSubReportes.Visible = false;
            // 
            // btnRepCalendario
            // 
            this.btnRepCalendario.Image = global::ControlAgricola.Properties.Resources.date;
            this.btnRepCalendario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepCalendario.Location = new System.Drawing.Point(26, 0);
            this.btnRepCalendario.Name = "btnRepCalendario";
            this.btnRepCalendario.Size = new System.Drawing.Size(175, 40);
            this.btnRepCalendario.TabIndex = 13;
            this.btnRepCalendario.Text = "Calendario por día";
            this.btnRepCalendario.UseVisualStyleBackColor = true;
            this.btnRepCalendario.Click += new System.EventHandler(this.btnRepCalendario_Click);
            // 
            // btnRepAgroquimicos
            // 
            this.btnRepAgroquimicos.Image = global::ControlAgricola.Properties.Resources.gtk_index;
            this.btnRepAgroquimicos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepAgroquimicos.Location = new System.Drawing.Point(26, 45);
            this.btnRepAgroquimicos.Name = "btnRepAgroquimicos";
            this.btnRepAgroquimicos.Size = new System.Drawing.Size(175, 40);
            this.btnRepAgroquimicos.TabIndex = 14;
            this.btnRepAgroquimicos.Text = "Agroquímicos";
            this.btnRepAgroquimicos.UseVisualStyleBackColor = true;
            this.btnRepAgroquimicos.Click += new System.EventHandler(this.btnRepAgroquimicos_Click);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarSesion.Image = global::ControlAgricola.Properties.Resources.gnome_shutdown;
            this.btnCerrarSesion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrarSesion.Location = new System.Drawing.Point(12, 575);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(200, 44);
            this.btnCerrarSesion.TabIndex = 15;
            this.btnCerrarSesion.Text = "Cerrar sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblTipoUsuario);
            this.panel2.Controls.Add(this.lblUsuario);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1453, 140);
            this.panel2.TabIndex = 16;
            // 
            // lblTipoUsuario
            // 
            this.lblTipoUsuario.AutoSize = true;
            this.lblTipoUsuario.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoUsuario.Location = new System.Drawing.Point(1100, 80);
            this.lblTipoUsuario.Name = "lblTipoUsuario";
            this.lblTipoUsuario.Size = new System.Drawing.Size(100, 19);
            this.lblTipoUsuario.TabIndex = 17;
            this.lblTipoUsuario.Text = "";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(1100, 50);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(100, 19);
            this.lblUsuario.TabIndex = 18;
            this.lblUsuario.Text = "";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(60, 50);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(100, 19);
            this.lblTitle.TabIndex = 19;
            this.lblTitle.Text = "Control Agrícola";
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1453, 900);
            this.Controls.Add(this.pnlPrincipal);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Name = "frmPrincipal";
            this.Text = "Control Agrícola";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.pnlPrincipal.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlSubMantenimientos.ResumeLayout(false);
            this.pnlSubReportes.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlPrincipal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnMantenimientos;
        private System.Windows.Forms.Panel pnlSubMantenimientos;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnCultivos;
        private System.Windows.Forms.Button btnLotes;
        private System.Windows.Forms.Button btnAgroquimicos;
        private System.Windows.Forms.Button btnAplicaciones;
        private System.Windows.Forms.Button btnPaquetes;
        private System.Windows.Forms.Button btnCalendarizacion;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Panel pnlSubReportes;
        private System.Windows.Forms.Button btnRepCalendario;
        private System.Windows.Forms.Button btnRepAgroquimicos;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTipoUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblTitle;
    }
}
