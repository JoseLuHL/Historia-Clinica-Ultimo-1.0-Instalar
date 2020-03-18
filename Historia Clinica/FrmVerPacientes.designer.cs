namespace Historia_Clinica
{
    partial class FrmVerPacientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVerPacientes));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.BtnCargarExamenes = new System.Windows.Forms.Button();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.DgvExamenPacticado = new System.Windows.Forms.DataGridView();
            this.LblMensaje = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.LblNombrePaciente = new System.Windows.Forms.Label();
            this.LblGuardar = new System.Windows.Forms.Label();
            this.BtnGuardarHistoria = new System.Windows.Forms.Button();
            this.LblNumeroAtencion = new System.Windows.Forms.Label();
            this.PctFoto = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtidentificacion = new System.Windows.Forms.Label();
            this.DgvExamenPacticadoColCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvExamenPacticadoColExamen = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DgvExamenPacticadoColResultado = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DgvExamenPacticadoColAjuntar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvExamenPacticadoColAjuntar2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvExamenPacticadoColFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvExamenPacticadoColFechaver = new System.Windows.Forms.DataGridViewImageColumn();
            this.DgvExamenPacticadoColEliminar = new System.Windows.Forms.DataGridViewImageColumn();
            this.DgvExamenPacticadoColDireccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvExamenPacticado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctFoto)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnCargarExamenes
            // 
            this.BtnCargarExamenes.BackColor = System.Drawing.Color.SteelBlue;
            this.BtnCargarExamenes.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCargarExamenes.ForeColor = System.Drawing.Color.White;
            this.BtnCargarExamenes.Location = new System.Drawing.Point(373, 140);
            this.BtnCargarExamenes.Name = "BtnCargarExamenes";
            this.BtnCargarExamenes.Size = new System.Drawing.Size(172, 36);
            this.BtnCargarExamenes.TabIndex = 66;
            this.BtnCargarExamenes.Text = "Cargar Examenes";
            this.BtnCargarExamenes.UseVisualStyleBackColor = false;
            this.BtnCargarExamenes.Click += new System.EventHandler(this.BtnCargarExamenes_Click);
            // 
            // pictureBox13
            // 
            this.pictureBox13.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox13.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox13.BackgroundImage")));
            this.pictureBox13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox13.Location = new System.Drawing.Point(304, 146);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(42, 33);
            this.pictureBox13.TabIndex = 65;
            this.pictureBox13.TabStop = false;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.SteelBlue;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label14.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(12, 145);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(292, 36);
            this.label14.TabIndex = 64;
            this.label14.Text = "Examenes Practicados";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label14.UseCompatibleTextRendering = true;
            // 
            // groupBox12
            // 
            this.groupBox12.BackColor = System.Drawing.Color.Transparent;
            this.groupBox12.Controls.Add(this.DgvExamenPacticado);
            this.groupBox12.ForeColor = System.Drawing.Color.Black;
            this.groupBox12.Location = new System.Drawing.Point(12, 173);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(765, 232);
            this.groupBox12.TabIndex = 63;
            this.groupBox12.TabStop = false;
            // 
            // DgvExamenPacticado
            // 
            this.DgvExamenPacticado.AllowUserToResizeColumns = false;
            this.DgvExamenPacticado.AllowUserToResizeRows = false;
            this.DgvExamenPacticado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DgvExamenPacticado.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvExamenPacticado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DgvExamenPacticado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvExamenPacticado.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DgvExamenPacticadoColCodigo,
            this.DgvExamenPacticadoColExamen,
            this.DgvExamenPacticadoColResultado,
            this.DgvExamenPacticadoColAjuntar,
            this.DgvExamenPacticadoColAjuntar2,
            this.DgvExamenPacticadoColFecha,
            this.DgvExamenPacticadoColFechaver,
            this.DgvExamenPacticadoColEliminar,
            this.DgvExamenPacticadoColDireccion});
            this.DgvExamenPacticado.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DgvExamenPacticado.Location = new System.Drawing.Point(7, 12);
            this.DgvExamenPacticado.Name = "DgvExamenPacticado";
            this.DgvExamenPacticado.RowHeadersVisible = false;
            this.DgvExamenPacticado.Size = new System.Drawing.Size(752, 214);
            this.DgvExamenPacticado.TabIndex = 2;
            this.DgvExamenPacticado.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvExamenPacticado_CellClick_1);
            this.DgvExamenPacticado.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvExamenPacticado_CellEndEdit);
            this.DgvExamenPacticado.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DgvExamenPacticado_UserAddedRow_1);
            // 
            // LblMensaje
            // 
            this.LblMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblMensaje.BackColor = System.Drawing.Color.SteelBlue;
            this.LblMensaje.Font = new System.Drawing.Font("Verdana", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMensaje.ForeColor = System.Drawing.Color.White;
            this.LblMensaje.Location = new System.Drawing.Point(-10, -10);
            this.LblMensaje.Name = "LblMensaje";
            this.LblMensaje.Size = new System.Drawing.Size(804, 68);
            this.LblMensaje.TabIndex = 68;
            this.LblMensaje.Text = "AJUNTAR EXAMENES";
            this.LblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox8.BackColor = System.Drawing.Color.Gray;
            this.pictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox8.Location = new System.Drawing.Point(0, 55);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(794, 23);
            this.pictureBox8.TabIndex = 67;
            this.pictureBox8.TabStop = false;
            // 
            // LblNombrePaciente
            // 
            this.LblNombrePaciente.AutoSize = true;
            this.LblNombrePaciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNombrePaciente.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.LblNombrePaciente.Location = new System.Drawing.Point(12, 89);
            this.LblNombrePaciente.Name = "LblNombrePaciente";
            this.LblNombrePaciente.Size = new System.Drawing.Size(467, 37);
            this.LblNombrePaciente.TabIndex = 69;
            this.LblNombrePaciente.Text = "JUANITO PEREZ RODRIGUEZ";
            // 
            // LblGuardar
            // 
            this.LblGuardar.AutoSize = true;
            this.LblGuardar.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblGuardar.ForeColor = System.Drawing.Color.Blue;
            this.LblGuardar.Location = new System.Drawing.Point(338, 506);
            this.LblGuardar.Name = "LblGuardar";
            this.LblGuardar.Size = new System.Drawing.Size(72, 18);
            this.LblGuardar.TabIndex = 71;
            this.LblGuardar.Text = "Guardar";
            // 
            // BtnGuardarHistoria
            // 
            this.BtnGuardarHistoria.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnGuardarHistoria.BackgroundImage")));
            this.BtnGuardarHistoria.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnGuardarHistoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGuardarHistoria.Location = new System.Drawing.Point(324, 411);
            this.BtnGuardarHistoria.Name = "BtnGuardarHistoria";
            this.BtnGuardarHistoria.Size = new System.Drawing.Size(102, 92);
            this.BtnGuardarHistoria.TabIndex = 70;
            this.BtnGuardarHistoria.UseVisualStyleBackColor = true;
            this.BtnGuardarHistoria.Click += new System.EventHandler(this.BtnGuardarHistoria_Click);
            // 
            // LblNumeroAtencion
            // 
            this.LblNumeroAtencion.AutoSize = true;
            this.LblNumeroAtencion.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumeroAtencion.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.LblNumeroAtencion.Location = new System.Drawing.Point(735, 89);
            this.LblNumeroAtencion.Name = "LblNumeroAtencion";
            this.LblNumeroAtencion.Size = new System.Drawing.Size(35, 37);
            this.LblNumeroAtencion.TabIndex = 72;
            this.LblNumeroAtencion.Text = "#";
            this.LblNumeroAtencion.Visible = false;
            // 
            // PctFoto
            // 
            this.PctFoto.Image = ((System.Drawing.Image)(resources.GetObject("PctFoto.Image")));
            this.PctFoto.Location = new System.Drawing.Point(656, 111);
            this.PctFoto.Name = "PctFoto";
            this.PctFoto.Size = new System.Drawing.Size(73, 56);
            this.PctFoto.TabIndex = 73;
            this.PctFoto.TabStop = false;
            this.PctFoto.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtidentificacion
            // 
            this.txtidentificacion.AutoSize = true;
            this.txtidentificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtidentificacion.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.txtidentificacion.Location = new System.Drawing.Point(573, 89);
            this.txtidentificacion.Name = "txtidentificacion";
            this.txtidentificacion.Size = new System.Drawing.Size(35, 37);
            this.txtidentificacion.TabIndex = 74;
            this.txtidentificacion.Text = "#";
            this.txtidentificacion.Visible = false;
            // 
            // DgvExamenPacticadoColCodigo
            // 
            this.DgvExamenPacticadoColCodigo.HeaderText = "Codigo";
            this.DgvExamenPacticadoColCodigo.Name = "DgvExamenPacticadoColCodigo";
            this.DgvExamenPacticadoColCodigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvExamenPacticadoColCodigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvExamenPacticadoColCodigo.Visible = false;
            // 
            // DgvExamenPacticadoColExamen
            // 
            this.DgvExamenPacticadoColExamen.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.DgvExamenPacticadoColExamen.HeaderText = "Examen";
            this.DgvExamenPacticadoColExamen.Name = "DgvExamenPacticadoColExamen";
            this.DgvExamenPacticadoColExamen.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvExamenPacticadoColExamen.Width = 150;
            // 
            // DgvExamenPacticadoColResultado
            // 
            this.DgvExamenPacticadoColResultado.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.DgvExamenPacticadoColResultado.HeaderText = "Resultado";
            this.DgvExamenPacticadoColResultado.Name = "DgvExamenPacticadoColResultado";
            this.DgvExamenPacticadoColResultado.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvExamenPacticadoColResultado.Visible = false;
            this.DgvExamenPacticadoColResultado.Width = 150;
            // 
            // DgvExamenPacticadoColAjuntar
            // 
            this.DgvExamenPacticadoColAjuntar.HeaderText = "Imagen1";
            this.DgvExamenPacticadoColAjuntar.Name = "DgvExamenPacticadoColAjuntar";
            this.DgvExamenPacticadoColAjuntar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvExamenPacticadoColAjuntar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvExamenPacticadoColAjuntar.Visible = false;
            // 
            // DgvExamenPacticadoColAjuntar2
            // 
            this.DgvExamenPacticadoColAjuntar2.HeaderText = "Adjuntar";
            this.DgvExamenPacticadoColAjuntar2.Name = "DgvExamenPacticadoColAjuntar2";
            this.DgvExamenPacticadoColAjuntar2.ReadOnly = true;
            // 
            // DgvExamenPacticadoColFecha
            // 
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.DgvExamenPacticadoColFecha.DefaultCellStyle = dataGridViewCellStyle1;
            this.DgvExamenPacticadoColFecha.HeaderText = "Fecha del Examen";
            this.DgvExamenPacticadoColFecha.MaxInputLength = 10;
            this.DgvExamenPacticadoColFecha.Name = "DgvExamenPacticadoColFecha";
            this.DgvExamenPacticadoColFecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvExamenPacticadoColFecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvExamenPacticadoColFecha.Visible = false;
            this.DgvExamenPacticadoColFecha.Width = 130;
            // 
            // DgvExamenPacticadoColFechaver
            // 
            this.DgvExamenPacticadoColFechaver.HeaderText = "Ver";
            this.DgvExamenPacticadoColFechaver.Image = ((System.Drawing.Image)(resources.GetObject("DgvExamenPacticadoColFechaver.Image")));
            this.DgvExamenPacticadoColFechaver.Name = "DgvExamenPacticadoColFechaver";
            this.DgvExamenPacticadoColFechaver.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // DgvExamenPacticadoColEliminar
            // 
            this.DgvExamenPacticadoColEliminar.HeaderText = "";
            this.DgvExamenPacticadoColEliminar.Name = "DgvExamenPacticadoColEliminar";
            this.DgvExamenPacticadoColEliminar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // DgvExamenPacticadoColDireccion
            // 
            this.DgvExamenPacticadoColDireccion.HeaderText = "direccion";
            this.DgvExamenPacticadoColDireccion.Name = "DgvExamenPacticadoColDireccion";
            this.DgvExamenPacticadoColDireccion.ReadOnly = true;
            this.DgvExamenPacticadoColDireccion.Visible = false;
            // 
            // FrmVerPacientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(790, 532);
            this.Controls.Add(this.txtidentificacion);
            this.Controls.Add(this.PctFoto);
            this.Controls.Add(this.LblNumeroAtencion);
            this.Controls.Add(this.LblGuardar);
            this.Controls.Add(this.BtnGuardarHistoria);
            this.Controls.Add(this.LblNombrePaciente);
            this.Controls.Add(this.LblMensaje);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.BtnCargarExamenes);
            this.Controls.Add(this.pictureBox13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.groupBox12);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmVerPacientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmVerPacientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            this.groupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvExamenPacticado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctFoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private HistoriaClinicaDataSet HistoriaClinicaDataSet;
        private System.Windows.Forms.Button BtnCargarExamenes;
        private System.Windows.Forms.PictureBox pictureBox13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox12;
        public System.Windows.Forms.Label LblMensaje;
        private System.Windows.Forms.PictureBox pictureBox8;
        public System.Windows.Forms.Label LblNombrePaciente;
        public System.Windows.Forms.Label LblGuardar;
        public System.Windows.Forms.Button BtnGuardarHistoria;
        public System.Windows.Forms.Label LblNumeroAtencion;
        private System.Windows.Forms.PictureBox PctFoto;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView DgvExamenPacticado;
        public System.Windows.Forms.Label txtidentificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvExamenPacticadoColCodigo;
        private System.Windows.Forms.DataGridViewComboBoxColumn DgvExamenPacticadoColExamen;
        private System.Windows.Forms.DataGridViewComboBoxColumn DgvExamenPacticadoColResultado;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvExamenPacticadoColAjuntar;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvExamenPacticadoColAjuntar2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvExamenPacticadoColFecha;
        private System.Windows.Forms.DataGridViewImageColumn DgvExamenPacticadoColFechaver;
        private System.Windows.Forms.DataGridViewImageColumn DgvExamenPacticadoColEliminar;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvExamenPacticadoColDireccion;
        //private HistoriaClinicaDataSetTableAdapters.DepartamentoTableAdapter DepartamentoTableAdapter;
        //private HistoriaClinicaDataSetTableAdapters.AccidenteLaboralTableAdapter AccidenteLaboralTableAdapter;
        //private HistoriaClinicaDataSetTableAdapters.NUMERO_DE_PACIENTES_DIAGNOSTICOTableAdapter NUMERO_DE_PACIENTES_DIAGNOSTICOTableAdapter;
        //private HistoriaClinicaDataSetTableAdapters.OcupacionTableAdapter OcupacionTableAdapter;

    }
}