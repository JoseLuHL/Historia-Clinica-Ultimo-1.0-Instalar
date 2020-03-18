namespace Historia_Clinica
{
    partial class FrmPacientesPendientes
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPacientesPendientes));
            this.DgvDatos = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ChkActivar = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.RdbSinAtender = new System.Windows.Forms.RadioButton();
            this.RdbAtendido = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.RdbTodas = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.CboEmpresa = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Btn_LupaBuscar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DtHasta = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.DtDesde = new System.Windows.Forms.DateTimePicker();
            this.GruBuscar = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtCriterio = new System.Windows.Forms.TextBox();
            this.BtnGenerarInforme = new System.Windows.Forms.Button();
            this.BtnHistoria = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.LblMensaje = new System.Windows.Forms.Label();
            this.DgvDatosColNumero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColNumeroHistoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColNombres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColedad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColSexo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColEntrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColEnfasis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColFecha1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosconceptoAptitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColatendido = new System.Windows.Forms.DataGridViewImageColumn();
            this.DgvDatosColImprimir = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDatos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.GruBuscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.SuspendLayout();
            // 
            // DgvDatos
            // 
            this.DgvDatos.AllowUserToAddRows = false;
            this.DgvDatos.AllowUserToDeleteRows = false;
            this.DgvDatos.AllowUserToResizeColumns = false;
            this.DgvDatos.AllowUserToResizeRows = false;
            this.DgvDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvDatos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvDatos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DgvDatosColNumero,
            this.DgvDatosColNumeroHistoria,
            this.DgvDatosColDocumento,
            this.DgvDatosColNombres,
            this.DgvDatosColedad,
            this.DgvDatosColSexo,
            this.DgvDatosColFecha,
            this.DgvDatosColEntrada,
            this.DgvDatosColEnfasis,
            this.DgvDatosColFecha1,
            this.DgvDatosconceptoAptitud,
            this.Column1,
            this.DgvDatosColatendido,
            this.DgvDatosColImprimir});
            this.DgvDatos.Location = new System.Drawing.Point(12, 275);
            this.DgvDatos.MultiSelect = false;
            this.DgvDatos.Name = "DgvDatos";
            this.DgvDatos.ReadOnly = true;
            this.DgvDatos.Size = new System.Drawing.Size(1291, 238);
            this.DgvDatos.TabIndex = 13;
            this.DgvDatos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDatos_CellClick);
            this.DgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDatos_CellDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.ChkActivar);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.GruBuscar);
            this.groupBox1.Controls.Add(this.BtnGenerarInforme);
            this.groupBox1.Controls.Add(this.BtnHistoria);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1291, 178);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.panel2);
            this.groupBox6.Controls.Add(this.panel1);
            this.groupBox6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(0, 103);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(421, 69);
            this.groupBox6.TabIndex = 123;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Atenciones";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(209, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 19);
            this.label9.TabIndex = 117;
            this.label9.Text = "Cerradas";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(209, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 19);
            this.label8.TabIndex = 116;
            this.label8.Text = "Abiertas";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(128, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(80, 14);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.ForeColor = System.Drawing.Color.SteelBlue;
            this.panel1.Location = new System.Drawing.Point(127, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(81, 12);
            this.panel1.TabIndex = 0;
            // 
            // ChkActivar
            // 
            this.ChkActivar.AutoSize = true;
            this.ChkActivar.BackColor = System.Drawing.Color.Olive;
            this.ChkActivar.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActivar.ForeColor = System.Drawing.Color.Cornsilk;
            this.ChkActivar.Location = new System.Drawing.Point(1087, 119);
            this.ChkActivar.Name = "ChkActivar";
            this.ChkActivar.Size = new System.Drawing.Size(78, 22);
            this.ChkActivar.TabIndex = 121;
            this.ChkActivar.Text = "Activar";
            this.ChkActivar.UseVisualStyleBackColor = false;
            this.ChkActivar.Visible = false;
            this.ChkActivar.CheckedChanged += new System.EventHandler(this.ChkActivar_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.RdbSinAtender);
            this.groupBox5.Controls.Add(this.RdbAtendido);
            this.groupBox5.Location = new System.Drawing.Point(862, 16);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(171, 80);
            this.groupBox5.TabIndex = 122;
            this.groupBox5.TabStop = false;
            // 
            // RdbSinAtender
            // 
            this.RdbSinAtender.AutoSize = true;
            this.RdbSinAtender.Checked = true;
            this.RdbSinAtender.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RdbSinAtender.ForeColor = System.Drawing.Color.Red;
            this.RdbSinAtender.Location = new System.Drawing.Point(17, 15);
            this.RdbSinAtender.Name = "RdbSinAtender";
            this.RdbSinAtender.Size = new System.Drawing.Size(140, 27);
            this.RdbSinAtender.TabIndex = 109;
            this.RdbSinAtender.TabStop = true;
            this.RdbSinAtender.Text = "Sin Atender";
            this.RdbSinAtender.UseVisualStyleBackColor = true;
            this.RdbSinAtender.CheckedChanged += new System.EventHandler(this.RdbSinAtender_CheckedChanged);
            this.RdbSinAtender.Click += new System.EventHandler(this.RdbSinAtender_Click);
            // 
            // RdbAtendido
            // 
            this.RdbAtendido.AutoSize = true;
            this.RdbAtendido.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RdbAtendido.ForeColor = System.Drawing.Color.Green;
            this.RdbAtendido.Location = new System.Drawing.Point(17, 48);
            this.RdbAtendido.Name = "RdbAtendido";
            this.RdbAtendido.Size = new System.Drawing.Size(123, 27);
            this.RdbAtendido.TabIndex = 110;
            this.RdbAtendido.Text = "Atendidos";
            this.RdbAtendido.UseVisualStyleBackColor = true;
            this.RdbAtendido.CheckedChanged += new System.EventHandler(this.RdbAtendido_CheckedChanged);
            this.RdbAtendido.Click += new System.EventHandler(this.RdbAtendido_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.RdbTodas);
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Controls.Add(this.CboEmpresa);
            this.groupBox4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(549, 102);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(484, 62);
            this.groupBox4.TabIndex = 121;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Empresa";
            // 
            // RdbTodas
            // 
            this.RdbTodas.AutoSize = true;
            this.RdbTodas.Checked = true;
            this.RdbTodas.Location = new System.Drawing.Point(9, 25);
            this.RdbTodas.Name = "RdbTodas";
            this.RdbTodas.Size = new System.Drawing.Size(73, 22);
            this.RdbTodas.TabIndex = 122;
            this.RdbTodas.TabStop = true;
            this.RdbTodas.Text = "Todas";
            this.RdbTodas.UseVisualStyleBackColor = true;
            this.RdbTodas.CheckedChanged += new System.EventHandler(this.RdbTodas_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(91, 25);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(58, 22);
            this.radioButton1.TabIndex = 121;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Una";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // CboEmpresa
            // 
            this.CboEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboEmpresa.Enabled = false;
            this.CboEmpresa.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboEmpresa.FormattingEnabled = true;
            this.CboEmpresa.Location = new System.Drawing.Point(155, 24);
            this.CboEmpresa.Name = "CboEmpresa";
            this.CboEmpresa.Size = new System.Drawing.Size(320, 26);
            this.CboEmpresa.TabIndex = 120;
            this.CboEmpresa.SelectedIndexChanged += new System.EventHandler(this.CboEmpresa_SelectedIndexChanged);
            this.CboEmpresa.SelectionChangeCommitted += new System.EventHandler(this.CboEmpresa_SelectionChangeCommitted);
            this.CboEmpresa.SelectedValueChanged += new System.EventHandler(this.CboEmpresa_SelectedValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Btn_LupaBuscar);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(427, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(116, 148);
            this.groupBox3.TabIndex = 119;
            this.groupBox3.TabStop = false;
            // 
            // Btn_LupaBuscar
            // 
            this.Btn_LupaBuscar.BackColor = System.Drawing.Color.Ivory;
            this.Btn_LupaBuscar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_LupaBuscar.BackgroundImage")));
            this.Btn_LupaBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_LupaBuscar.Location = new System.Drawing.Point(34, 12);
            this.Btn_LupaBuscar.Name = "Btn_LupaBuscar";
            this.Btn_LupaBuscar.Size = new System.Drawing.Size(48, 36);
            this.Btn_LupaBuscar.TabIndex = 104;
            this.toolTip1.SetToolTip(this.Btn_LupaBuscar, "Buscar");
            this.Btn_LupaBuscar.UseVisualStyleBackColor = false;
            this.Btn_LupaBuscar.Click += new System.EventHandler(this.Btn_LupaBuscar_Click);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 19);
            this.label7.TabIndex = 116;
            this.label7.Text = "Buscar";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Ivory;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(32, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 47);
            this.button1.TabIndex = 113;
            this.toolTip1.SetToolTip(this.button1, "Refrescar");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(11, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 19);
            this.label6.TabIndex = 115;
            this.label6.Text = "Actualizar";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.DtHasta);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.DtDesde);
            this.groupBox2.Location = new System.Drawing.Point(549, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 80);
            this.groupBox2.TabIndex = 118;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 16);
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
            this.DtHasta.Location = new System.Drawing.Point(155, 40);
            this.DtHasta.Name = "DtHasta";
            this.DtHasta.Size = new System.Drawing.Size(129, 27);
            this.DtHasta.TabIndex = 106;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(152, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 21);
            this.label3.TabIndex = 108;
            this.label3.Text = "Hasta:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DtDesde
            // 
            this.DtDesde.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtDesde.Location = new System.Drawing.Point(16, 40);
            this.DtDesde.Name = "DtDesde";
            this.DtDesde.Size = new System.Drawing.Size(133, 27);
            this.DtDesde.TabIndex = 111;
            this.DtDesde.Validating += new System.ComponentModel.CancelEventHandler(this.DtDesde_Validating);
            // 
            // GruBuscar
            // 
            this.GruBuscar.Controls.Add(this.label5);
            this.GruBuscar.Controls.Add(this.TxtCriterio);
            this.GruBuscar.Location = new System.Drawing.Point(3, 8);
            this.GruBuscar.Name = "GruBuscar";
            this.GruBuscar.Size = new System.Drawing.Size(418, 89);
            this.GruBuscar.TabIndex = 117;
            this.GruBuscar.TabStop = false;
            this.GruBuscar.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(42, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(312, 32);
            this.label5.TabIndex = 100;
            this.label5.Text = "Número de la Historia, Número de Documento \r\no Nombre del Trabajador.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TxtCriterio
            // 
            this.TxtCriterio.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtCriterio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtCriterio.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCriterio.Location = new System.Drawing.Point(6, 54);
            this.TxtCriterio.Name = "TxtCriterio";
            this.TxtCriterio.Size = new System.Drawing.Size(398, 27);
            this.TxtCriterio.TabIndex = 103;
            this.TxtCriterio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.TxtCriterio, "Buscar historia");
            this.TxtCriterio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCriterio_KeyDown);
            // 
            // BtnGenerarInforme
            // 
            this.BtnGenerarInforme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnGenerarInforme.BackColor = System.Drawing.Color.Ivory;
            this.BtnGenerarInforme.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnGenerarInforme.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGenerarInforme.Location = new System.Drawing.Point(1087, 28);
            this.BtnGenerarInforme.Name = "BtnGenerarInforme";
            this.BtnGenerarInforme.Size = new System.Drawing.Size(100, 80);
            this.BtnGenerarInforme.TabIndex = 114;
            this.BtnGenerarInforme.Text = "Generar Informe";
            this.BtnGenerarInforme.UseVisualStyleBackColor = false;
            this.BtnGenerarInforme.Click += new System.EventHandler(this.button2_Click);
            // 
            // BtnHistoria
            // 
            this.BtnHistoria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnHistoria.BackColor = System.Drawing.Color.Ivory;
            this.BtnHistoria.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnHistoria.BackgroundImage")));
            this.BtnHistoria.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnHistoria.Location = new System.Drawing.Point(1193, 31);
            this.BtnHistoria.Name = "BtnHistoria";
            this.BtnHistoria.Size = new System.Drawing.Size(81, 74);
            this.BtnHistoria.TabIndex = 112;
            this.toolTip1.SetToolTip(this.BtnHistoria, "Nueva Historia");
            this.BtnHistoria.UseVisualStyleBackColor = false;
            this.BtnHistoria.Click += new System.EventHandler(this.BtnHistoria_Click);
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
            // pictureBox8
            // 
            this.pictureBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox8.BackColor = System.Drawing.Color.Gray;
            this.pictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox8.Location = new System.Drawing.Point(-2, 66);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(1322, 27);
            this.pictureBox8.TabIndex = 59;
            this.pictureBox8.TabStop = false;
            // 
            // LblMensaje
            // 
            this.LblMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblMensaje.BackColor = System.Drawing.Color.SteelBlue;
            this.LblMensaje.Font = new System.Drawing.Font("Verdana", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMensaje.ForeColor = System.Drawing.Color.White;
            this.LblMensaje.Location = new System.Drawing.Point(0, -1);
            this.LblMensaje.Name = "LblMensaje";
            this.LblMensaje.Size = new System.Drawing.Size(1320, 77);
            this.LblMensaje.TabIndex = 60;
            this.LblMensaje.Text = "HISTORIA MÉDICA";
            this.LblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DgvDatosColNumero
            // 
            this.DgvDatosColNumero.HeaderText = "#";
            this.DgvDatosColNumero.Name = "DgvDatosColNumero";
            this.DgvDatosColNumero.ReadOnly = true;
            this.DgvDatosColNumero.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvDatosColNumero.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColNumero.Width = 50;
            // 
            // DgvDatosColNumeroHistoria
            // 
            this.DgvDatosColNumeroHistoria.HeaderText = "Atención";
            this.DgvDatosColNumeroHistoria.Name = "DgvDatosColNumeroHistoria";
            this.DgvDatosColNumeroHistoria.ReadOnly = true;
            this.DgvDatosColNumeroHistoria.Visible = false;
            this.DgvDatosColNumeroHistoria.Width = 89;
            // 
            // DgvDatosColDocumento
            // 
            this.DgvDatosColDocumento.HeaderText = "Documento";
            this.DgvDatosColDocumento.Name = "DgvDatosColDocumento";
            this.DgvDatosColDocumento.ReadOnly = true;
            this.DgvDatosColDocumento.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvDatosColDocumento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColDocumento.Width = 130;
            // 
            // DgvDatosColNombres
            // 
            this.DgvDatosColNombres.HeaderText = "Nombre Completo";
            this.DgvDatosColNombres.Name = "DgvDatosColNombres";
            this.DgvDatosColNombres.ReadOnly = true;
            this.DgvDatosColNombres.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColNombres.Width = 350;
            // 
            // DgvDatosColedad
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DgvDatosColedad.DefaultCellStyle = dataGridViewCellStyle1;
            this.DgvDatosColedad.HeaderText = "Edad";
            this.DgvDatosColedad.Name = "DgvDatosColedad";
            this.DgvDatosColedad.ReadOnly = true;
            this.DgvDatosColedad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColedad.Width = 80;
            // 
            // DgvDatosColSexo
            // 
            this.DgvDatosColSexo.HeaderText = "Genero";
            this.DgvDatosColSexo.Name = "DgvDatosColSexo";
            this.DgvDatosColSexo.ReadOnly = true;
            this.DgvDatosColSexo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColSexo.Width = 80;
            // 
            // DgvDatosColFecha
            // 
            dataGridViewCellStyle2.NullValue = null;
            this.DgvDatosColFecha.DefaultCellStyle = dataGridViewCellStyle2;
            this.DgvDatosColFecha.HeaderText = "Fecha Ingreso";
            this.DgvDatosColFecha.MaxInputLength = 10;
            this.DgvDatosColFecha.Name = "DgvDatosColFecha";
            this.DgvDatosColFecha.ReadOnly = true;
            this.DgvDatosColFecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColFecha.Width = 250;
            // 
            // DgvDatosColEntrada
            // 
            this.DgvDatosColEntrada.HeaderText = "Tipo de Atención";
            this.DgvDatosColEntrada.Name = "DgvDatosColEntrada";
            this.DgvDatosColEntrada.ReadOnly = true;
            this.DgvDatosColEntrada.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColEntrada.Width = 160;
            // 
            // DgvDatosColEnfasis
            // 
            this.DgvDatosColEnfasis.HeaderText = "enfasis";
            this.DgvDatosColEnfasis.Name = "DgvDatosColEnfasis";
            this.DgvDatosColEnfasis.ReadOnly = true;
            this.DgvDatosColEnfasis.Visible = false;
            this.DgvDatosColEnfasis.Width = 65;
            // 
            // DgvDatosColFecha1
            // 
            this.DgvDatosColFecha1.HeaderText = "fecha";
            this.DgvDatosColFecha1.Name = "DgvDatosColFecha1";
            this.DgvDatosColFecha1.ReadOnly = true;
            this.DgvDatosColFecha1.Visible = false;
            this.DgvDatosColFecha1.Width = 59;
            // 
            // DgvDatosconceptoAptitud
            // 
            this.DgvDatosconceptoAptitud.HeaderText = "concepto Aptitud";
            this.DgvDatosconceptoAptitud.Name = "DgvDatosconceptoAptitud";
            this.DgvDatosconceptoAptitud.ReadOnly = true;
            this.DgvDatosconceptoAptitud.Visible = false;
            this.DgvDatosconceptoAptitud.Width = 113;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Estado";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            this.Column1.Width = 65;
            // 
            // DgvDatosColatendido
            // 
            this.DgvDatosColatendido.HeaderText = "";
            this.DgvDatosColatendido.Image = ((System.Drawing.Image)(resources.GetObject("DgvDatosColatendido.Image")));
            this.DgvDatosColatendido.Name = "DgvDatosColatendido";
            this.DgvDatosColatendido.ReadOnly = true;
            this.DgvDatosColatendido.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvDatosColatendido.Visible = false;
            this.DgvDatosColatendido.Width = 45;
            // 
            // DgvDatosColImprimir
            // 
            this.DgvDatosColImprimir.HeaderText = "";
            this.DgvDatosColImprimir.Image = ((System.Drawing.Image)(resources.GetObject("DgvDatosColImprimir.Image")));
            this.DgvDatosColImprimir.Name = "DgvDatosColImprimir";
            this.DgvDatosColImprimir.ReadOnly = true;
            this.DgvDatosColImprimir.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvDatosColImprimir.Width = 45;
            // 
            // FrmPacientesPendientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1315, 514);
            this.Controls.Add(this.LblMensaje);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.DgvDatos);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmPacientesPendientes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPacientesPendientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvDatos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.GruBuscar.ResumeLayout(false);
            this.GruBuscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DgvDatos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DtHasta;
        internal System.Windows.Forms.Button Btn_LupaBuscar;
        private System.Windows.Forms.TextBox TxtCriterio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DtDesde;
        private System.Windows.Forms.PictureBox pictureBox8;
        public System.Windows.Forms.RadioButton RdbAtendido;
        public System.Windows.Forms.RadioButton RdbSinAtender;
        internal System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button BtnHistoria;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Button BtnGenerarInforme;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox GruBuscar;
        public System.Windows.Forms.Label LblMensaje;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox ChkActivar;
        private System.Windows.Forms.ComboBox CboEmpresa;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton RdbTodas;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColNumero;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColNumeroHistoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColNombres;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColedad;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColSexo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColEntrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColEnfasis;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColFecha1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosconceptoAptitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewImageColumn DgvDatosColatendido;
        private System.Windows.Forms.DataGridViewImageColumn DgvDatosColImprimir;
    }
}