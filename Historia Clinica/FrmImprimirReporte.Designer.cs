namespace Historia_Clinica
{
    partial class FrmImprimirReporte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImprimirReporte));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DtHasta = new System.Windows.Forms.DateTimePicker();
            this.DtDesde = new System.Windows.Forms.DateTimePicker();
            this.Btn_LupaBuscar = new System.Windows.Forms.Button();
            this.TxtCriterio = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DgvDatos = new System.Windows.Forms.DataGridView();
            this.DgvDatosColNombres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColNumeroHistoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColEnfasis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColVer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColImprimir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.BackColor = System.Drawing.Color.SteelBlue;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(-2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(951, 86);
            this.label13.TabIndex = 9;
            this.label13.Text = "HISTORIAS Y CERTIFICADOS";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.DtHasta);
            this.groupBox1.Controls.Add(this.DtDesde);
            this.groupBox1.Controls.Add(this.Btn_LupaBuscar);
            this.groupBox1.Controls.Add(this.TxtCriterio);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(846, 117);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(699, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 21);
            this.label3.TabIndex = 108;
            this.label3.Text = "Hasta:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(572, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 21);
            this.label2.TabIndex = 107;
            this.label2.Text = "Desde:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DtHasta
            // 
            this.DtHasta.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtHasta.Location = new System.Drawing.Point(702, 68);
            this.DtHasta.Name = "DtHasta";
            this.DtHasta.Size = new System.Drawing.Size(119, 27);
            this.DtHasta.TabIndex = 106;
            // 
            // DtDesde
            // 
            this.DtDesde.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtDesde.Location = new System.Drawing.Point(573, 68);
            this.DtDesde.Name = "DtDesde";
            this.DtDesde.Size = new System.Drawing.Size(123, 27);
            this.DtDesde.TabIndex = 105;
            this.DtDesde.Value = new System.DateTime(2000, 2, 23, 0, 0, 0, 0);
            // 
            // Btn_LupaBuscar
            // 
            this.Btn_LupaBuscar.BackColor = System.Drawing.Color.Ivory;
            this.Btn_LupaBuscar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_LupaBuscar.BackgroundImage")));
            this.Btn_LupaBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_LupaBuscar.Location = new System.Drawing.Point(488, 56);
            this.Btn_LupaBuscar.Name = "Btn_LupaBuscar";
            this.Btn_LupaBuscar.Size = new System.Drawing.Size(49, 39);
            this.Btn_LupaBuscar.TabIndex = 104;
            this.Btn_LupaBuscar.UseVisualStyleBackColor = false;
            this.Btn_LupaBuscar.Click += new System.EventHandler(this.Btn_LupaBuscar_Click);
            // 
            // TxtCriterio
            // 
            this.TxtCriterio.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.TxtCriterio.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCriterio.Location = new System.Drawing.Point(15, 61);
            this.TxtCriterio.Name = "TxtCriterio";
            this.TxtCriterio.Size = new System.Drawing.Size(467, 27);
            this.TxtCriterio.TabIndex = 103;
            this.TxtCriterio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtCriterio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCriterio_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(599, 18);
            this.label5.TabIndex = 100;
            this.label5.Text = "Número de la Historia, Número de Documento o Nombre del Trabajador.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 18);
            this.label4.TabIndex = 100;
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 18);
            this.label1.TabIndex = 100;
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DgvDatos
            // 
            this.DgvDatos.AllowUserToAddRows = false;
            this.DgvDatos.AllowUserToDeleteRows = false;
            this.DgvDatos.AllowUserToResizeColumns = false;
            this.DgvDatos.AllowUserToResizeRows = false;
            this.DgvDatos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DgvDatosColNombres,
            this.DgvDatosColDocumento,
            this.DgvDatosColNumeroHistoria,
            this.DgvDatosColFecha,
            this.DgvDatosColEnfasis,
            this.DgvDatosColVer,
            this.DgvDatosColImprimir});
            this.DgvDatos.Location = new System.Drawing.Point(13, 211);
            this.DgvDatos.Name = "DgvDatos";
            this.DgvDatos.ReadOnly = true;
            this.DgvDatos.Size = new System.Drawing.Size(932, 327);
            this.DgvDatos.TabIndex = 11;
            this.DgvDatos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDatos_CellClick);
            // 
            // DgvDatosColNombres
            // 
            this.DgvDatosColNombres.HeaderText = "Nombre Completo";
            this.DgvDatosColNombres.Name = "DgvDatosColNombres";
            this.DgvDatosColNombres.ReadOnly = true;
            this.DgvDatosColNombres.Width = 300;
            // 
            // DgvDatosColDocumento
            // 
            this.DgvDatosColDocumento.HeaderText = "Documento";
            this.DgvDatosColDocumento.Name = "DgvDatosColDocumento";
            this.DgvDatosColDocumento.ReadOnly = true;
            this.DgvDatosColDocumento.Width = 120;
            // 
            // DgvDatosColNumeroHistoria
            // 
            this.DgvDatosColNumeroHistoria.HeaderText = "NumeroHistoria";
            this.DgvDatosColNumeroHistoria.Name = "DgvDatosColNumeroHistoria";
            this.DgvDatosColNumeroHistoria.ReadOnly = true;
            this.DgvDatosColNumeroHistoria.Width = 140;
            // 
            // DgvDatosColFecha
            // 
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.DgvDatosColFecha.DefaultCellStyle = dataGridViewCellStyle1;
            this.DgvDatosColFecha.HeaderText = "Fecha";
            this.DgvDatosColFecha.MaxInputLength = 10;
            this.DgvDatosColFecha.Name = "DgvDatosColFecha";
            this.DgvDatosColFecha.ReadOnly = true;
            this.DgvDatosColFecha.Width = 120;
            // 
            // DgvDatosColEnfasis
            // 
            this.DgvDatosColEnfasis.HeaderText = "Enfasis";
            this.DgvDatosColEnfasis.Name = "DgvDatosColEnfasis";
            this.DgvDatosColEnfasis.ReadOnly = true;
            this.DgvDatosColEnfasis.Visible = false;
            // 
            // DgvDatosColVer
            // 
            this.DgvDatosColVer.HeaderText = "-----";
            this.DgvDatosColVer.Name = "DgvDatosColVer";
            this.DgvDatosColVer.ReadOnly = true;
            this.DgvDatosColVer.Width = 80;
            // 
            // DgvDatosColImprimir
            // 
            this.DgvDatosColImprimir.HeaderText = "********";
            this.DgvDatosColImprimir.Name = "DgvDatosColImprimir";
            this.DgvDatosColImprimir.ReadOnly = true;
            this.DgvDatosColImprimir.Width = 110;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Ivory;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(836, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 55);
            this.button1.TabIndex = 109;
            this.button1.Text = "Generar Informe";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmImprimirReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 550);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DgvDatos);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmImprimirReporte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmImprimirReporte_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDatos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button Btn_LupaBuscar;
        private System.Windows.Forms.TextBox TxtCriterio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DtHasta;
        private System.Windows.Forms.DateTimePicker DtDesde;
        private System.Windows.Forms.DataGridView DgvDatos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColNombres;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColNumeroHistoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColEnfasis;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColVer;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColImprimir;
        internal System.Windows.Forms.Button button1;
    }
}