namespace Historia_Clinica
{
    partial class FrmCancelarCita
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
            this.label74 = new System.Windows.Forms.Label();
            this.DgvDatos = new System.Windows.Forms.DataGridView();
            this.label17 = new System.Windows.Forms.Label();
            this.DgvDatosColNumero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColNombres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvDatosColEliminar = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.BackColor = System.Drawing.Color.SteelBlue;
            this.label74.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.ForeColor = System.Drawing.Color.Black;
            this.label74.Location = new System.Drawing.Point(150, 9);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(313, 29);
            this.label74.TabIndex = 82;
            this.label74.Text = "PACIENTES AGENDADOS";
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
            this.DgvDatos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DgvDatos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvDatos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DgvDatosColNumero,
            this.DgvDatosColDocumento,
            this.DgvDatosColNombres,
            this.dataGridViewTextBoxColumn1,
            this.DgvDatosColEliminar});
            this.DgvDatos.Location = new System.Drawing.Point(1, 57);
            this.DgvDatos.MultiSelect = false;
            this.DgvDatos.Name = "DgvDatos";
            this.DgvDatos.ReadOnly = true;
            this.DgvDatos.RowHeadersVisible = false;
            this.DgvDatos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvDatos.Size = new System.Drawing.Size(534, 465);
            this.DgvDatos.TabIndex = 81;
            this.DgvDatos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDatos_CellClick);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.BackColor = System.Drawing.Color.SteelBlue;
            this.label17.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Blue;
            this.label17.Location = new System.Drawing.Point(-2, -4);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(554, 58);
            this.label17.TabIndex = 83;
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            // DgvDatosColDocumento
            // 
            this.DgvDatosColDocumento.HeaderText = "Documento";
            this.DgvDatosColDocumento.Name = "DgvDatosColDocumento";
            this.DgvDatosColDocumento.ReadOnly = true;
            this.DgvDatosColDocumento.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvDatosColDocumento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColDocumento.Width = 118;
            // 
            // DgvDatosColNombres
            // 
            this.DgvDatosColNombres.HeaderText = "Nombre Completo";
            this.DgvDatosColNombres.Name = "DgvDatosColNombres";
            this.DgvDatosColNombres.ReadOnly = true;
            this.DgvDatosColNombres.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvDatosColNombres.Width = 357;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "index";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // DgvDatosColEliminar
            // 
            this.DgvDatosColEliminar.HeaderText = "Eliminar";
            this.DgvDatosColEliminar.Name = "DgvDatosColEliminar";
            this.DgvDatosColEliminar.ReadOnly = true;
            this.DgvDatosColEliminar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvDatosColEliminar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DgvDatosColEliminar.Visible = false;
            this.DgvDatosColEliminar.Width = 68;
            // 
            // FrmCancelarCita
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 534);
            this.Controls.Add(this.label74);
            this.Controls.Add(this.DgvDatos);
            this.Controls.Add(this.label17);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmCancelarCita";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmCancelarCita_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.DataGridView DgvDatos;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColNumero;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvDatosColNombres;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewImageColumn DgvDatosColEliminar;

    }
}