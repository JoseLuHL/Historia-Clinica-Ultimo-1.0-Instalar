namespace Historia_Clinica
{
    partial class FrmVerExamenes
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.SP_ExamenPracticado_ImagenBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.HistoriaClinica_New = new Historia_Clinica.HistoriaClinica_New();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblDocumento = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtTitulo = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.HistoriaClinicaDataSet = new Historia_Clinica.HistoriaClinicaDataSet();
            this.AccidenteLaboralBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AccidenteLaboralTableAdapter = new Historia_Clinica.HistoriaClinicaDataSetTableAdapters.AccidenteLaboralTableAdapter();
            this.SP_ExamenPracticado_ImagenTableAdapter = new Historia_Clinica.HistoriaClinica_NewTableAdapters.SP_ExamenPracticado_ImagenTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.SP_ExamenPracticado_ImagenBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistoriaClinica_New)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistoriaClinicaDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccidenteLaboralBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // SP_ExamenPracticado_ImagenBindingSource
            // 
            this.SP_ExamenPracticado_ImagenBindingSource.DataMember = "SP_ExamenPracticado_Imagen";
            this.SP_ExamenPracticado_ImagenBindingSource.DataSource = this.HistoriaClinica_New;
            // 
            // HistoriaClinica_New
            // 
            this.HistoriaClinica_New.DataSetName = "HistoriaClinica_New";
            this.HistoriaClinica_New.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "New_Imagen";
            reportDataSource1.Value = this.SP_ExamenPracticado_ImagenBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Historia_Clinica.Report_Ver_Examen.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(3, 106);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1166, 584);
            this.reportViewer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.reportViewer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1172, 693);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LblDocumento);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.TxtTitulo);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1166, 97);
            this.panel1.TabIndex = 1;
            // 
            // LblDocumento
            // 
            this.LblDocumento.AutoSize = true;
            this.LblDocumento.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDocumento.Location = new System.Drawing.Point(201, 6);
            this.LblDocumento.Name = "LblDocumento";
            this.LblDocumento.Size = new System.Drawing.Size(98, 18);
            this.LblDocumento.TabIndex = 7;
            this.LblDocumento.Text = "documento";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Título:";
            // 
            // TxtTitulo
            // 
            this.TxtTitulo.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTitulo.Location = new System.Drawing.Point(77, 61);
            this.TxtTitulo.Name = "TxtTitulo";
            this.TxtTitulo.Size = new System.Drawing.Size(285, 27);
            this.TxtTitulo.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(375, 23);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(782, 47);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(288, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Paciente: Juanito Perez Rodriguez";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Examenes Practicados";
            // 
            // HistoriaClinicaDataSet
            // 
            this.HistoriaClinicaDataSet.DataSetName = "HistoriaClinicaDataSet";
            this.HistoriaClinicaDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // AccidenteLaboralBindingSource
            // 
            this.AccidenteLaboralBindingSource.DataMember = "AccidenteLaboral";
            this.AccidenteLaboralBindingSource.DataSource = this.HistoriaClinicaDataSet;
            // 
            // AccidenteLaboralTableAdapter
            // 
            this.AccidenteLaboralTableAdapter.ClearBeforeFill = true;
            // 
            // SP_ExamenPracticado_ImagenTableAdapter
            // 
            this.SP_ExamenPracticado_ImagenTableAdapter.ClearBeforeFill = true;
            // 
            // FrmVerExamenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 696);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmVerExamenes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmVerExamenes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SP_ExamenPracticado_ImagenBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistoriaClinica_New)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistoriaClinicaDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccidenteLaboralBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource AccidenteLaboralBindingSource;
        private HistoriaClinicaDataSet HistoriaClinicaDataSet;
        private HistoriaClinicaDataSetTableAdapters.AccidenteLaboralTableAdapter AccidenteLaboralTableAdapter;
        private System.Windows.Forms.BindingSource SP_ExamenPracticado_ImagenBindingSource;
        private HistoriaClinica_New HistoriaClinica_New;
        private HistoriaClinica_NewTableAdapters.SP_ExamenPracticado_ImagenTableAdapter SP_ExamenPracticado_ImagenTableAdapter;
        private System.Windows.Forms.TextBox TxtTitulo;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label LblDocumento;

    }
}