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
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.btnCultivos = new System.Windows.Forms.Button();
            this.btnAgroquimicos = new System.Windows.Forms.Button();
            this.btnAplicaciones = new System.Windows.Forms.Button();
            this.btnPaquetes = new System.Windows.Forms.Button();
            this.btnCalendario = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.pnlSubReportes = new System.Windows.Forms.Panel();
            this.btnRepAplicaciones = new System.Windows.Forms.Button();
            this.btnRepAgroquimicos = new System.Windows.Forms.Button();
            this.btnVerCalendario = new System.Windows.Forms.Button();
            this.lblAdmin = new System.Windows.Forms.Label();
            this.btnLotes = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.pnlTitulo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlContenedor = new System.Windows.Forms.Panel();
            this.pnlMenu.SuspendLayout();
            this.pnlSubReportes.SuspendLayout();
            this.pnlTitulo.SuspendLayout();
            this.pnlContenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.lblUsuario);
            this.pnlMenu.Controls.Add(this.btnCultivos);
            this.pnlMenu.Controls.Add(this.btnAgroquimicos);
            this.pnlMenu.Controls.Add(this.btnAplicaciones);
            this.pnlMenu.Controls.Add(this.btnPaquetes);
            this.pnlMenu.Controls.Add(this.btnCalendario);
            this.pnlMenu.Controls.Add(this.btnReportes);
            this.pnlMenu.Controls.Add(this.pnlSubReportes);
            this.pnlMenu.Controls.Add(this.lblAdmin);
            this.pnlMenu.Controls.Add(this.btnLotes);
            this.pnlMenu.Controls.Add(this.btnUsuarios);
            this.pnlMenu.Controls.Add(this.btnSalir);
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(200, 700);
            this.pnlMenu.TabIndex = 0;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(10, 10);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(180, 45);
            this.lblUsuario.Text = "Usuario";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUsuario.TabIndex = 1;
            // 
            // btnCultivos
            // 
            this.btnCultivos.BackColor = System.Drawing.Color.White;
            this.btnCultivos.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnCultivos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCultivos.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCultivos.Location = new System.Drawing.Point(15, 65);
            this.btnCultivos.Name = "btnCultivos";
            this.btnCultivos.Size = new System.Drawing.Size(170, 34);
            this.btnCultivos.Text = "Cultivos";
            this.btnCultivos.UseVisualStyleBackColor = false;
            this.btnCultivos.TabIndex = 2;
            this.btnCultivos.Click += new System.EventHandler(this.btnCultivos_Click);
            // 
            // btnAgroquimicos
            // 
            this.btnAgroquimicos.BackColor = System.Drawing.Color.White;
            this.btnAgroquimicos.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnAgroquimicos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgroquimicos.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgroquimicos.Location = new System.Drawing.Point(15, 107);
            this.btnAgroquimicos.Name = "btnAgroquimicos";
            this.btnAgroquimicos.Size = new System.Drawing.Size(170, 34);
            this.btnAgroquimicos.Text = "Agroquímicos";
            this.btnAgroquimicos.UseVisualStyleBackColor = false;
            this.btnAgroquimicos.TabIndex = 3;
            this.btnAgroquimicos.Click += new System.EventHandler(this.btnAgroquimicos_Click);
            // 
            // btnAplicaciones
            // 
            this.btnAplicaciones.BackColor = System.Drawing.Color.White;
            this.btnAplicaciones.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnAplicaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicaciones.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicaciones.Location = new System.Drawing.Point(15, 149);
            this.btnAplicaciones.Name = "btnAplicaciones";
            this.btnAplicaciones.Size = new System.Drawing.Size(170, 34);
            this.btnAplicaciones.Text = "Aplicaciones";
            this.btnAplicaciones.UseVisualStyleBackColor = false;
            this.btnAplicaciones.TabIndex = 4;
            this.btnAplicaciones.Click += new System.EventHandler(this.btnAplicaciones_Click);
            // 
            // btnPaquetes
            // 
            this.btnPaquetes.BackColor = System.Drawing.Color.White;
            this.btnPaquetes.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnPaquetes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPaquetes.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPaquetes.Location = new System.Drawing.Point(15, 191);
            this.btnPaquetes.Name = "btnPaquetes";
            this.btnPaquetes.Size = new System.Drawing.Size(170, 34);
            this.btnPaquetes.Text = "Paquetes";
            this.btnPaquetes.UseVisualStyleBackColor = false;
            this.btnPaquetes.TabIndex = 5;
            this.btnPaquetes.Click += new System.EventHandler(this.btnPaquetes_Click);
            // 
            // btnCalendario
            // 
            this.btnCalendario.BackColor = System.Drawing.Color.White;
            this.btnCalendario.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnCalendario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalendario.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalendario.Location = new System.Drawing.Point(15, 233);
            this.btnCalendario.Name = "btnCalendario";
            this.btnCalendario.Size = new System.Drawing.Size(170, 34);
            this.btnCalendario.Text = "Calendarización";
            this.btnCalendario.UseVisualStyleBackColor = false;
            this.btnCalendario.TabIndex = 6;
            this.btnCalendario.Click += new System.EventHandler(this.btnCalendario_Click);
            // 
            // btnReportes
            // 
            this.btnReportes.BackColor = System.Drawing.Color.White;
            this.btnReportes.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportes.Location = new System.Drawing.Point(15, 275);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(170, 34);
            this.btnReportes.Text = "Reportes";
            this.btnReportes.UseVisualStyleBackColor = false;
            this.btnReportes.TabIndex = 7;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // pnlSubReportes
            // 
            this.pnlSubReportes.Controls.Add(this.btnRepAplicaciones);
            this.pnlSubReportes.Controls.Add(this.btnRepAgroquimicos);
            this.pnlSubReportes.Controls.Add(this.btnVerCalendario);
            this.pnlSubReportes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.pnlSubReportes.Location = new System.Drawing.Point(0, 313);
            this.pnlSubReportes.Name = "pnlSubReportes";
            this.pnlSubReportes.Size = new System.Drawing.Size(200, 118);
            this.pnlSubReportes.Visible = false;
            this.pnlSubReportes.TabIndex = 8;
            // 
            // btnRepAplicaciones
            // 
            this.btnRepAplicaciones.BackColor = System.Drawing.Color.White;
            this.btnRepAplicaciones.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnRepAplicaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepAplicaciones.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepAplicaciones.Location = new System.Drawing.Point(25, 6);
            this.btnRepAplicaciones.Name = "btnRepAplicaciones";
            this.btnRepAplicaciones.Size = new System.Drawing.Size(160, 30);
            this.btnRepAplicaciones.Text = "Aplicaciones x Día";
            this.btnRepAplicaciones.UseVisualStyleBackColor = false;
            this.btnRepAplicaciones.TabIndex = 9;
            this.btnRepAplicaciones.Click += new System.EventHandler(this.btnRepAplicaciones_Click);
            // 
            // btnRepAgroquimicos
            // 
            this.btnRepAgroquimicos.BackColor = System.Drawing.Color.White;
            this.btnRepAgroquimicos.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnRepAgroquimicos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepAgroquimicos.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepAgroquimicos.Location = new System.Drawing.Point(25, 44);
            this.btnRepAgroquimicos.Name = "btnRepAgroquimicos";
            this.btnRepAgroquimicos.Size = new System.Drawing.Size(160, 30);
            this.btnRepAgroquimicos.Text = "Agroquímicos";
            this.btnRepAgroquimicos.UseVisualStyleBackColor = false;
            this.btnRepAgroquimicos.TabIndex = 10;
            this.btnRepAgroquimicos.Click += new System.EventHandler(this.btnRepAgroquimicos_Click);
            // 
            // btnVerCalendario
            // 
            this.btnVerCalendario.BackColor = System.Drawing.Color.White;
            this.btnVerCalendario.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnVerCalendario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerCalendario.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerCalendario.Location = new System.Drawing.Point(25, 82);
            this.btnVerCalendario.Name = "btnVerCalendario";
            this.btnVerCalendario.Size = new System.Drawing.Size(160, 30);
            this.btnVerCalendario.Text = "Ver Calendario";
            this.btnVerCalendario.UseVisualStyleBackColor = false;
            this.btnVerCalendario.TabIndex = 11;
            this.btnVerCalendario.Click += new System.EventHandler(this.btnVerCalendario_Click);
            // 
            // lblAdmin
            // 
            this.lblAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAdmin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdmin.ForeColor = System.Drawing.Color.White;
            this.lblAdmin.Location = new System.Drawing.Point(15, 535);
            this.lblAdmin.Name = "lblAdmin";
            this.lblAdmin.Size = new System.Drawing.Size(170, 18);
            this.lblAdmin.Text = "Solo administrador";
            this.lblAdmin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAdmin.TabIndex = 12;
            // 
            // btnLotes
            // 
            this.btnLotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLotes.BackColor = System.Drawing.Color.White;
            this.btnLotes.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnLotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLotes.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLotes.Location = new System.Drawing.Point(15, 556);
            this.btnLotes.Name = "btnLotes";
            this.btnLotes.Size = new System.Drawing.Size(170, 34);
            this.btnLotes.Text = "Lotes";
            this.btnLotes.UseVisualStyleBackColor = false;
            this.btnLotes.TabIndex = 13;
            this.btnLotes.Click += new System.EventHandler(this.btnLotes_Click);
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUsuarios.BackColor = System.Drawing.Color.White;
            this.btnUsuarios.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsuarios.Location = new System.Drawing.Point(15, 598);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(170, 34);
            this.btnUsuarios.Text = "Usuarios";
            this.btnUsuarios.UseVisualStyleBackColor = false;
            this.btnUsuarios.TabIndex = 14;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSalir.BackColor = System.Drawing.Color.White;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(15, 650);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(170, 34);
            this.btnSalir.Text = "Cerrar sesión";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.TabIndex = 15;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // pnlTitulo
            // 
            this.pnlTitulo.Controls.Add(this.lblTitulo);
            this.pnlTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(160)))), ((int)(((byte)(71)))));
            this.pnlTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitulo.Location = new System.Drawing.Point(200, 0);
            this.pnlTitulo.Name = "pnlTitulo";
            this.pnlTitulo.Size = new System.Drawing.Size(1050, 50);
            this.pnlTitulo.TabIndex = 16;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1050, 50);
            this.lblTitulo.Text = "Control Agrícola - Agrícola Los 3 Patitos S.A.";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitulo.TabIndex = 17;
            // 
            // pnlContenedor
            // 
            this.pnlContenedor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenedor.Location = new System.Drawing.Point(200, 50);
            this.pnlContenedor.Name = "pnlContenedor";
            this.pnlContenedor.Size = new System.Drawing.Size(1050, 650);
            this.pnlContenedor.TabIndex = 18;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1250, 700);
            this.Controls.Add(this.pnlContenedor);
            this.Controls.Add(this.pnlTitulo);
            this.Controls.Add(this.pnlMenu);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "frmPrincipal";
            this.Text = "Control Agrícola";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            this.pnlSubReportes.ResumeLayout(false);
            this.pnlSubReportes.PerformLayout();
            this.pnlTitulo.ResumeLayout(false);
            this.pnlTitulo.PerformLayout();
            this.pnlContenedor.ResumeLayout(false);
            this.pnlContenedor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Button btnCultivos;
        private System.Windows.Forms.Button btnAgroquimicos;
        private System.Windows.Forms.Button btnAplicaciones;
        private System.Windows.Forms.Button btnPaquetes;
        private System.Windows.Forms.Button btnCalendario;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Panel pnlSubReportes;
        private System.Windows.Forms.Button btnRepAplicaciones;
        private System.Windows.Forms.Button btnRepAgroquimicos;
        private System.Windows.Forms.Button btnVerCalendario;
        private System.Windows.Forms.Label lblAdmin;
        private System.Windows.Forms.Button btnLotes;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Panel pnlTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlContenedor;
    }
}
