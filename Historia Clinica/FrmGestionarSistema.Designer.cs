namespace Historia_Clinica
{
    partial class FrmGestionarSistema
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGestionarSistema));
            this.DgvDetalleTablas = new System.Windows.Forms.DataGridView();
            this.DgvTablaDetalleColGuardar = new System.Windows.Forms.DataGridViewImageColumn();
            this.DgvTablaDetalleColEditar = new System.Windows.Forms.DataGridViewImageColumn();
            this.DgvTablaDetalleColEliminar = new System.Windows.Forms.DataGridViewImageColumn();
            this.DgvTablas = new System.Windows.Forms.DataGridView();
            this.DgvTablasColNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvClavePrimaria = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDetalleTablas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTablas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvClavePrimaria)).BeginInit();
            this.SuspendLayout();
            // 
            // DgvDetalleTablas
            // 
            this.DgvDetalleTablas.AllowUserToDeleteRows = false;
            this.DgvDetalleTablas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvDetalleTablas.BackgroundColor = System.Drawing.Color.DarkGray;
            this.DgvDetalleTablas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvDetalleTablas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DgvTablaDetalleColGuardar,
            this.DgvTablaDetalleColEditar,
            this.DgvTablaDetalleColEliminar});
            this.DgvDetalleTablas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DgvDetalleTablas.Location = new System.Drawing.Point(323, 0);
            this.DgvDetalleTablas.Name = "DgvDetalleTablas";
            this.DgvDetalleTablas.Size = new System.Drawing.Size(705, 504);
            this.DgvDetalleTablas.TabIndex = 3;
            this.DgvDetalleTablas.EditModeChanged += new System.EventHandler(this.DgvDetalleTablas_EditModeChanged);
            this.DgvDetalleTablas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDetalleTablas_CellClick);
            this.DgvDetalleTablas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDetalleTablas_CellContentClick);
            this.DgvDetalleTablas.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DgvDetalleTablas_UserAddedRow);
            // 
            // DgvTablaDetalleColGuardar
            // 
            this.DgvTablaDetalleColGuardar.HeaderText = "";
            this.DgvTablaDetalleColGuardar.Image = ((System.Drawing.Image)(resources.GetObject("DgvTablaDetalleColGuardar.Image")));
            this.DgvTablaDetalleColGuardar.Name = "DgvTablaDetalleColGuardar";
            this.DgvTablaDetalleColGuardar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvTablaDetalleColGuardar.ToolTipText = "Guardar";
            this.DgvTablaDetalleColGuardar.Width = 30;
            // 
            // DgvTablaDetalleColEditar
            // 
            this.DgvTablaDetalleColEditar.HeaderText = "";
            this.DgvTablaDetalleColEditar.Image = ((System.Drawing.Image)(resources.GetObject("DgvTablaDetalleColEditar.Image")));
            this.DgvTablaDetalleColEditar.Name = "DgvTablaDetalleColEditar";
            this.DgvTablaDetalleColEditar.ReadOnly = true;
            this.DgvTablaDetalleColEditar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvTablaDetalleColEditar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DgvTablaDetalleColEditar.Visible = false;
            this.DgvTablaDetalleColEditar.Width = 30;
            // 
            // DgvTablaDetalleColEliminar
            // 
            this.DgvTablaDetalleColEliminar.HeaderText = "";
            this.DgvTablaDetalleColEliminar.Image = ((System.Drawing.Image)(resources.GetObject("DgvTablaDetalleColEliminar.Image")));
            this.DgvTablaDetalleColEliminar.Name = "DgvTablaDetalleColEliminar";
            this.DgvTablaDetalleColEliminar.ReadOnly = true;
            this.DgvTablaDetalleColEliminar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvTablaDetalleColEliminar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DgvTablaDetalleColEliminar.ToolTipText = "Eliminar";
            this.DgvTablaDetalleColEliminar.Width = 30;
            // 
            // DgvTablas
            // 
            this.DgvTablas.AllowUserToAddRows = false;
            this.DgvTablas.AllowUserToDeleteRows = false;
            this.DgvTablas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DgvTablas.BackgroundColor = System.Drawing.Color.DarkGray;
            this.DgvTablas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvTablas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DgvTablasColNombre});
            this.DgvTablas.Location = new System.Drawing.Point(-1, 0);
            this.DgvTablas.Name = "DgvTablas";
            this.DgvTablas.ReadOnly = true;
            this.DgvTablas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvTablas.Size = new System.Drawing.Size(318, 504);
            this.DgvTablas.TabIndex = 2;
            this.DgvTablas.SelectionChanged += new System.EventHandler(this.DgvTablas_SelectionChanged_1);
            // 
            // DgvTablasColNombre
            // 
            this.DgvTablasColNombre.HeaderText = "Nombre";
            this.DgvTablasColNombre.Name = "DgvTablasColNombre";
            this.DgvTablasColNombre.ReadOnly = true;
            // 
            // DgvClavePrimaria
            // 
            this.DgvClavePrimaria.AllowUserToAddRows = false;
            this.DgvClavePrimaria.AllowUserToDeleteRows = false;
            this.DgvClavePrimaria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DgvClavePrimaria.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.DgvClavePrimaria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvClavePrimaria.Location = new System.Drawing.Point(568, 434);
            this.DgvClavePrimaria.Name = "DgvClavePrimaria";
            this.DgvClavePrimaria.ReadOnly = true;
            this.DgvClavePrimaria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvClavePrimaria.Size = new System.Drawing.Size(460, 62);
            this.DgvClavePrimaria.TabIndex = 4;
            // 
            // FrmGestionarSistema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1031, 508);
            this.Controls.Add(this.DgvDetalleTablas);
            this.Controls.Add(this.DgvClavePrimaria);
            this.Controls.Add(this.DgvTablas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmGestionarSistema";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmGestionarSistema_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvDetalleTablas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTablas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvClavePrimaria)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView DgvDetalleTablas;
        public System.Windows.Forms.DataGridView DgvTablas;
        public System.Windows.Forms.DataGridView DgvClavePrimaria;
        private System.Windows.Forms.DataGridViewImageColumn DgvTablaDetalleColGuardar;
        private System.Windows.Forms.DataGridViewImageColumn DgvTablaDetalleColEditar;
        private System.Windows.Forms.DataGridViewImageColumn DgvTablaDetalleColEliminar;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvTablasColNombre;
    }
}