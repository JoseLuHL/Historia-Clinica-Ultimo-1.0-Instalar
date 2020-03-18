namespace Historia_Clinica
{
    partial class FrmGenerarInforme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGenerarInforme));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RdbDiagnostico = new System.Windows.Forms.RadioButton();
            this.RdbOcupacionales = new System.Windows.Forms.RadioButton();
            this.TxtOcupacion = new System.Windows.Forms.Button();
            this.DtHasta = new System.Windows.Forms.DateTimePicker();
            this.DtDesde = new System.Windows.Forms.DateTimePicker();
            this.TxtAceptar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.CboEmpresa = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.DgvDatos = new System.Windows.Forms.DataGridView();
            this.DgvDatosColNumero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColNombres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnGenerar = new System.Windows.Forms.Button();
            this.BrnGraficas = new System.Windows.Forms.Button();
            this.DgvPacientes = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColNumeroHistoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColedad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColSexo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColEntrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColEnfasis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColFecha1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColatendido = new System.Windows.Forms.DataGridViewImageColumn();
            this.DgvDatosColImprimir = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvPacientes)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RdbDiagnostico);
            this.panel1.Controls.Add(this.RdbOcupacionales);
            this.panel1.Controls.Add(this.TxtOcupacion);
            this.panel1.Controls.Add(this.DtHasta);
            this.panel1.Controls.Add(this.DtDesde);
            this.panel1.Controls.Add(this.TxtAceptar);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.CboEmpresa);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Location = new System.Drawing.Point(6, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(536, 236);
            this.panel1.TabIndex = 0;
            // 
            // RdbDiagnostico
            // 
            this.RdbDiagnostico.AutoSize = true;
            this.RdbDiagnostico.Checked = true;
            this.RdbDiagnostico.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RdbDiagnostico.Location = new System.Drawing.Point(231, 117);
            this.RdbDiagnostico.Name = "RdbDiagnostico";
            this.RdbDiagnostico.Size = new System.Drawing.Size(216, 23);
            this.RdbDiagnostico.TabIndex = 117;
            this.RdbDiagnostico.TabStop = true;
            this.RdbDiagnostico.Text = "Número de Diagnostico";
            this.RdbDiagnostico.UseVisualStyleBackColor = true;
            // 
            // RdbOcupacionales
            // 
            this.RdbOcupacionales.AutoSize = true;
            this.RdbOcupacionales.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RdbOcupacionales.Location = new System.Drawing.Point(231, 146);
            this.RdbOcupacionales.Name = "RdbOcupacionales";
            this.RdbOcupacionales.Size = new System.Drawing.Size(216, 23);
            this.RdbOcupacionales.TabIndex = 116;
            this.RdbOcupacionales.Text = "Número de Ocupaciones";
            this.RdbOcupacionales.UseVisualStyleBackColor = true;
            // 
            // TxtOcupacion
            // 
            this.TxtOcupacion.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOcupacion.Location = new System.Drawing.Point(104, 20);
            this.TxtOcupacion.Name = "TxtOcupacion";
            this.TxtOcupacion.Size = new System.Drawing.Size(90, 17);
            this.TxtOcupacion.TabIndex = 115;
            this.TxtOcupacion.Text = "Ocupación";
            this.TxtOcupacion.UseVisualStyleBackColor = true;
            this.TxtOcupacion.Visible = false;
            this.TxtOcupacion.Click += new System.EventHandler(this.TxtOcupacion_Click);
            // 
            // DtHasta
            // 
            this.DtHasta.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtHasta.Location = new System.Drawing.Point(24, 178);
            this.DtHasta.Name = "DtHasta";
            this.DtHasta.Size = new System.Drawing.Size(155, 26);
            this.DtHasta.TabIndex = 114;
            // 
            // DtDesde
            // 
            this.DtDesde.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtDesde.Location = new System.Drawing.Point(24, 111);
            this.DtDesde.Name = "DtDesde";
            this.DtDesde.Size = new System.Drawing.Size(155, 26);
            this.DtDesde.TabIndex = 113;
            // 
            // TxtAceptar
            // 
            this.TxtAceptar.BackColor = System.Drawing.SystemColors.Control;
            this.TxtAceptar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TxtAceptar.BackgroundImage")));
            this.TxtAceptar.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAceptar.Location = new System.Drawing.Point(231, 176);
            this.TxtAceptar.Name = "TxtAceptar";
            this.TxtAceptar.Size = new System.Drawing.Size(286, 55);
            this.TxtAceptar.TabIndex = 111;
            this.TxtAceptar.Text = "Aceptar";
            this.TxtAceptar.UseVisualStyleBackColor = false;
            this.TxtAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Menu;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 18);
            this.label1.TabIndex = 110;
            this.label1.Text = "Hasta";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.SystemColors.Menu;
            this.label16.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(20, 84);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 18);
            this.label16.TabIndex = 108;
            this.label16.Text = "Desde";
            // 
            // CboEmpresa
            // 
            this.CboEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboEmpresa.FormattingEnabled = true;
            this.CboEmpresa.Location = new System.Drawing.Point(23, 43);
            this.CboEmpresa.Name = "CboEmpresa";
            this.CboEmpresa.Size = new System.Drawing.Size(494, 28);
            this.CboEmpresa.TabIndex = 106;
            this.CboEmpresa.SelectedValueChanged += new System.EventHandler(this.CboEmpresa_SelectedValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(20, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 18);
            this.label15.TabIndex = 105;
            this.label15.Text = "Empresa";
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
            this.DgvDatosColDocumento,
            this.DgvDatosColNombres});
            this.DgvDatos.Location = new System.Drawing.Point(12, 262);
            this.DgvDatos.MultiSelect = false;
            this.DgvDatos.Name = "DgvDatos";
            this.DgvDatos.ReadOnly = true;
            this.DgvDatos.RowHeadersVisible = false;
            this.DgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvDatos.Size = new System.Drawing.Size(1075, 356);
            this.DgvDatos.TabIndex = 24;
            // 
            // DgvDatosColNumero
            // 
            this.DgvDatosColNumero.HeaderText = "#";
            this.DgvDatosColNumero.Name = "DgvDatosColNumero";
            this.DgvDatosColNumero.ReadOnly = true;
            this.DgvDatosColNumero.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvDatosColNumero.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColNumero.Width = 30;
            // 
            // DgvDatosColDocumento
            // 
            this.DgvDatosColDocumento.HeaderText = "Documento";
            this.DgvDatosColDocumento.Name = "DgvDatosColDocumento";
            this.DgvDatosColDocumento.ReadOnly = true;
            this.DgvDatosColDocumento.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvDatosColDocumento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColDocumento.Width = 340;
            // 
            // DgvDatosColNombres
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DgvDatosColNombres.DefaultCellStyle = dataGridViewCellStyle1;
            this.DgvDatosColNombres.HeaderText = "Nombre Completo";
            this.DgvDatosColNombres.Name = "DgvDatosColNombres";
            this.DgvDatosColNombres.ReadOnly = true;
            this.DgvDatosColNombres.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColNombres.Width = 125;
            // 
            // BtnGenerar
            // 
            this.BtnGenerar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnGenerar.BackColor = System.Drawing.SystemColors.Control;
            this.BtnGenerar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnGenerar.BackgroundImage")));
            this.BtnGenerar.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGenerar.Location = new System.Drawing.Point(0, 640);
            this.BtnGenerar.Name = "BtnGenerar";
            this.BtnGenerar.Size = new System.Drawing.Size(560, 55);
            this.BtnGenerar.TabIndex = 118;
            this.BtnGenerar.Text = "Generar Reporte";
            this.BtnGenerar.UseVisualStyleBackColor = false;
            this.BtnGenerar.Click += new System.EventHandler(this.BtnGenerar_Click);
            // 
            // BrnGraficas
            // 
            this.BrnGraficas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BrnGraficas.BackColor = System.Drawing.SystemColors.Control;
            this.BrnGraficas.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BrnGraficas.BackgroundImage")));
            this.BrnGraficas.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrnGraficas.Location = new System.Drawing.Point(566, 640);
            this.BrnGraficas.Name = "BrnGraficas";
            this.BrnGraficas.Size = new System.Drawing.Size(537, 55);
            this.BrnGraficas.TabIndex = 119;
            this.BrnGraficas.Text = "Generar Graficas";
            this.BrnGraficas.UseVisualStyleBackColor = false;
            this.BrnGraficas.Click += new System.EventHandler(this.BrnGraficas_Click);
            // 
            // DgvPacientes
            // 
            this.DgvPacientes.AllowUserToAddRows = false;
            this.DgvPacientes.AllowUserToDeleteRows = false;
            this.DgvPacientes.AllowUserToResizeColumns = false;
            this.DgvPacientes.AllowUserToResizeRows = false;
            this.DgvPacientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvPacientes.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvPacientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DgvPacientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvPacientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.DgvDatosColNumeroHistoria,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.DgvDatosColedad,
            this.DgvDatosColSexo,
            this.DgvDatosColFecha,
            this.DgvDatosColEntrada,
            this.DgvDatosColEnfasis,
            this.DgvDatosColFecha1,
            this.Column1,
            this.DgvDatosColatendido,
            this.DgvDatosColImprimir});
            this.DgvPacientes.Location = new System.Drawing.Point(566, 69);
            this.DgvPacientes.MultiSelect = false;
            this.DgvPacientes.Name = "DgvPacientes";
            this.DgvPacientes.ReadOnly = true;
            this.DgvPacientes.Size = new System.Drawing.Size(525, 549);
            this.DgvPacientes.TabIndex = 120;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Menu;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(563, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 18);
            this.label2.TabIndex = 118;
            this.label2.Text = "PACIENTES ATENDIDOS";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "#";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // DgvDatosColNumeroHistoria
            // 
            this.DgvDatosColNumeroHistoria.HeaderText = "N° Atención";
            this.DgvDatosColNumeroHistoria.Name = "DgvDatosColNumeroHistoria";
            this.DgvDatosColNumeroHistoria.ReadOnly = true;
            this.DgvDatosColNumeroHistoria.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Documento";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Nombre Completo";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 300;
            // 
            // DgvDatosColedad
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DgvDatosColedad.DefaultCellStyle = dataGridViewCellStyle2;
            this.DgvDatosColedad.HeaderText = "Edad";
            this.DgvDatosColedad.Name = "DgvDatosColedad";
            this.DgvDatosColedad.ReadOnly = true;
            this.DgvDatosColedad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColedad.Visible = false;
            this.DgvDatosColedad.Width = 80;
            // 
            // DgvDatosColSexo
            // 
            this.DgvDatosColSexo.HeaderText = "Genero";
            this.DgvDatosColSexo.Name = "DgvDatosColSexo";
            this.DgvDatosColSexo.ReadOnly = true;
            this.DgvDatosColSexo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColSexo.Visible = false;
            this.DgvDatosColSexo.Width = 70;
            // 
            // DgvDatosColFecha
            // 
            dataGridViewCellStyle3.NullValue = null;
            this.DgvDatosColFecha.DefaultCellStyle = dataGridViewCellStyle3;
            this.DgvDatosColFecha.HeaderText = "Fecha Ingreso";
            this.DgvDatosColFecha.MaxInputLength = 10;
            this.DgvDatosColFecha.Name = "DgvDatosColFecha";
            this.DgvDatosColFecha.ReadOnly = true;
            this.DgvDatosColFecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColFecha.Visible = false;
            this.DgvDatosColFecha.Width = 300;
            // 
            // DgvDatosColEntrada
            // 
            this.DgvDatosColEntrada.HeaderText = "Entrada";
            this.DgvDatosColEntrada.Name = "DgvDatosColEntrada";
            this.DgvDatosColEntrada.ReadOnly = true;
            this.DgvDatosColEntrada.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColEntrada.Visible = false;
            this.DgvDatosColEntrada.Width = 180;
            // 
            // DgvDatosColEnfasis
            // 
            this.DgvDatosColEnfasis.HeaderText = "enfasis";
            this.DgvDatosColEnfasis.Name = "DgvDatosColEnfasis";
            this.DgvDatosColEnfasis.ReadOnly = true;
            this.DgvDatosColEnfasis.Visible = false;
            // 
            // DgvDatosColFecha1
            // 
            this.DgvDatosColFecha1.HeaderText = "fecha";
            this.DgvDatosColFecha1.Name = "DgvDatosColFecha1";
            this.DgvDatosColFecha1.ReadOnly = true;
            this.DgvDatosColFecha1.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Estado";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // DgvDatosColatendido
            // 
            this.DgvDatosColatendido.HeaderText = "";
            this.DgvDatosColatendido.Image = ((System.Drawing.Image)(resources.GetObject("DgvDatosColatendido.Image")));
            this.DgvDatosColatendido.Name = "DgvDatosColatendido";
            this.DgvDatosColatendido.ReadOnly = true;
            this.DgvDatosColatendido.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvDatosColatendido.Visible = false;
            this.DgvDatosColatendido.Width = 50;
            // 
            // DgvDatosColImprimir
            // 
            this.DgvDatosColImprimir.HeaderText = "";
            this.DgvDatosColImprimir.Image = ((System.Drawing.Image)(resources.GetObject("DgvDatosColImprimir.Image")));
            this.DgvDatosColImprimir.Name = "DgvDatosColImprimir";
            this.DgvDatosColImprimir.ReadOnly = true;
            this.DgvDatosColImprimir.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvDatosColImprimir.Visible = false;
            this.DgvDatosColImprimir.Width = 50;
            // 
            // FrmGenerarInforme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 695);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DgvPacientes);
            this.Controls.Add(this.BrnGraficas);
            this.Controls.Add(this.BtnGenerar);
            this.Controls.Add(this.DgvDatos);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmGenerarInforme";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmEnsayo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvPacientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox CboEmpresa;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button TxtAceptar;
        private System.Windows.Forms.DateTimePicker DtHasta;
        private System.Windows.Forms.DateTimePicker DtDesde;
        private System.Windows.Forms.Button TxtOcupacion;
        private System.Windows.Forms.DataGridView DgvDatos;
        private System.Windows.Forms.RadioButton RdbDiagnostico;
        private System.Windows.Forms.RadioButton RdbOcupacionales;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColNumero;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColNombres;
        private System.Windows.Forms.Button BtnGenerar;
        private System.Windows.Forms.Button BrnGraficas;
        private System.Windows.Forms.DataGridView DgvPacientes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColNumeroHistoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColedad;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColSexo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColEntrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColEnfasis;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColFecha1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewImageColumn DgvDatosColatendido;
        private System.Windows.Forms.DataGridViewImageColumn DgvDatosColImprimir;
    }
}