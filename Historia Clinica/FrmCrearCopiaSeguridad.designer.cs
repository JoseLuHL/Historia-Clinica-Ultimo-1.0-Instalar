namespace Micopia
{
    partial class FrmCrearCopiaSeguridad
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
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
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCrearCopiaSeguridad));
            this.BtnGenerar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.LblCreanado = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.PnlMSM = new System.Windows.Forms.Panel();
            this.BtnCerrar = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.historiaClinica_Activar = new Historia_Clinica.HistoriaClinica_Activar();
            this.historiaClinicaActivarBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PnlMSM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.historiaClinica_Activar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.historiaClinicaActivarBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnGenerar
            // 
            this.BtnGenerar.BackColor = System.Drawing.Color.Transparent;
            this.BtnGenerar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnGenerar.BackgroundImage")));
            this.BtnGenerar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnGenerar.Location = new System.Drawing.Point(432, 219);
            this.BtnGenerar.Name = "BtnGenerar";
            this.BtnGenerar.Size = new System.Drawing.Size(156, 80);
            this.BtnGenerar.TabIndex = 0;
            this.toolTip1.SetToolTip(this.BtnGenerar, "Ubicación de copia de seguridad");
            this.BtnGenerar.UseVisualStyleBackColor = false;
            this.BtnGenerar.Click += new System.EventHandler(this.BtnGenerar_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(989, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Creando Copia de Seguridad";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar1.ForeColor = System.Drawing.SystemColors.Control;
            this.progressBar1.Location = new System.Drawing.Point(16, 52);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(989, 29);
            this.progressBar1.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // LblCreanado
            // 
            this.LblCreanado.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCreanado.ForeColor = System.Drawing.Color.Black;
            this.LblCreanado.Location = new System.Drawing.Point(30, 99);
            this.LblCreanado.Name = "LblCreanado";
            this.LblCreanado.Size = new System.Drawing.Size(960, 54);
            this.LblCreanado.TabIndex = 3;
            this.LblCreanado.Text = "Creando copia de seguridad, espere mientras se completa esta operación ";
            this.LblCreanado.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PnlMSM
            // 
            this.PnlMSM.Controls.Add(this.progressBar1);
            this.PnlMSM.Controls.Add(this.label1);
            this.PnlMSM.Location = new System.Drawing.Point(3, 396);
            this.PnlMSM.Name = "PnlMSM";
            this.PnlMSM.Size = new System.Drawing.Size(1013, 111);
            this.PnlMSM.TabIndex = 4;
            // 
            // BtnCerrar
            // 
            this.BtnCerrar.BackColor = System.Drawing.Color.Transparent;
            this.BtnCerrar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnCerrar.BackgroundImage")));
            this.BtnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCerrar.Location = new System.Drawing.Point(998, 3);
            this.BtnCerrar.Name = "BtnCerrar";
            this.BtnCerrar.Size = new System.Drawing.Size(27, 29);
            this.BtnCerrar.TabIndex = 25;
            this.BtnCerrar.TabStop = false;
            this.BtnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.BackColor = System.Drawing.Color.SteelBlue;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(-8, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1036, 86);
            this.label13.TabIndex = 26;
            this.label13.Text = "COPIA DE SEGURIDAD";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // historiaClinica_Activar
            // 
            this.historiaClinica_Activar.DataSetName = "HistoriaClinica_Activar";
            this.historiaClinica_Activar.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // historiaClinicaActivarBindingSource
            // 
            this.historiaClinicaActivarBindingSource.DataSource = this.historiaClinica_Activar;
            this.historiaClinicaActivarBindingSource.Position = 0;
            // 
            // FrmCrearCopiaSeguridad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 519);
            this.Controls.Add(this.BtnCerrar);
            this.Controls.Add(this.PnlMSM);
            this.Controls.Add(this.LblCreanado);
            this.Controls.Add(this.BtnGenerar);
            this.Controls.Add(this.label13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCrearCopiaSeguridad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmCrearCopiaSeguridad_Load);
            this.PnlMSM.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BtnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.historiaClinica_Activar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.historiaClinicaActivarBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.Button BtnGenerar;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Panel PnlMSM;
        public System.Windows.Forms.Label LblCreanado;
        private System.Windows.Forms.PictureBox BtnCerrar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Timer timer2;
        private Historia_Clinica.HistoriaClinica_Activar historiaClinica_Activar;
        private System.Windows.Forms.BindingSource historiaClinicaActivarBindingSource;
    }
}

