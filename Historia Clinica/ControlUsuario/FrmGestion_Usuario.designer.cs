namespace Control_de_Tecnicos
{
    partial class FrmGestion_Usuario
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
            this.BtnAceptar = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.BtnNuevo = new System.Windows.Forms.Button();
            this.CboUsuario = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.DgvPermisos = new System.Windows.Forms.DataGridView();
            this.DgvModuloColID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvModuloColDescripciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvModuloColPermiso = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvPermisos)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnAceptar
            // 
            this.BtnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BtnAceptar.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAceptar.ForeColor = System.Drawing.Color.White;
            this.BtnAceptar.Location = new System.Drawing.Point(161, 593);
            this.BtnAceptar.Name = "BtnAceptar";
            this.BtnAceptar.Size = new System.Drawing.Size(263, 40);
            this.BtnAceptar.TabIndex = 39;
            this.BtnAceptar.Text = "Aceptar";
            this.BtnAceptar.UseVisualStyleBackColor = false;
            this.BtnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.Panel1.Controls.Add(this.BtnNuevo);
            this.Panel1.Controls.Add(this.CboUsuario);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(593, 61);
            this.Panel1.TabIndex = 38;
            // 
            // BtnNuevo
            // 
            this.BtnNuevo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnNuevo.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNuevo.Location = new System.Drawing.Point(487, 18);
            this.BtnNuevo.Name = "BtnNuevo";
            this.BtnNuevo.Size = new System.Drawing.Size(89, 29);
            this.BtnNuevo.TabIndex = 37;
            this.BtnNuevo.Text = "Nuevo";
            this.BtnNuevo.UseVisualStyleBackColor = false;
            this.BtnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);
            this.BtnNuevo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvPermisos_KeyDown);
            // 
            // CboUsuario
            // 
            this.CboUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboUsuario.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboUsuario.FormattingEnabled = true;
            this.CboUsuario.Location = new System.Drawing.Point(102, 18);
            this.CboUsuario.Name = "CboUsuario";
            this.CboUsuario.Size = new System.Drawing.Size(370, 29);
            this.CboUsuario.TabIndex = 17;
            this.CboUsuario.SelectionChangeCommitted += new System.EventHandler(this.CboUsuario_SelectionChangeCommitted);
            this.CboUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvPermisos_KeyDown);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(6, 21);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(98, 21);
            this.Label4.TabIndex = 18;
            this.Label4.Text = "Usuario:";
            // 
            // DgvPermisos
            // 
            this.DgvPermisos.AllowUserToAddRows = false;
            this.DgvPermisos.AllowUserToDeleteRows = false;
            this.DgvPermisos.AllowUserToResizeColumns = false;
            this.DgvPermisos.AllowUserToResizeRows = false;
            this.DgvPermisos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvPermisos.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.DgvPermisos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DgvPermisos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DgvPermisos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvPermisos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DgvModuloColID,
            this.DgvModuloColDescripciones,
            this.DgvModuloColPermiso});
            this.DgvPermisos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DgvPermisos.EnableHeadersVisualStyles = false;
            this.DgvPermisos.Location = new System.Drawing.Point(22, 78);
            this.DgvPermisos.MultiSelect = false;
            this.DgvPermisos.Name = "DgvPermisos";
            this.DgvPermisos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DgvPermisos.RowHeadersVisible = false;
            this.DgvPermisos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvPermisos.Size = new System.Drawing.Size(547, 501);
            this.DgvPermisos.TabIndex = 107;
            this.DgvPermisos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvPermisos_KeyDown);
            // 
            // DgvModuloColID
            // 
            this.DgvModuloColID.HeaderText = "Codigo";
            this.DgvModuloColID.Name = "DgvModuloColID";
            this.DgvModuloColID.Visible = false;
            // 
            // DgvModuloColDescripciones
            // 
            this.DgvModuloColDescripciones.HeaderText = "Descripción";
            this.DgvModuloColDescripciones.MaxInputLength = 10;
            this.DgvModuloColDescripciones.Name = "DgvModuloColDescripciones";
            this.DgvModuloColDescripciones.ReadOnly = true;
            this.DgvModuloColDescripciones.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvModuloColDescripciones.Width = 470;
            // 
            // DgvModuloColPermiso
            // 
            this.DgvModuloColPermiso.HeaderText = "";
            this.DgvModuloColPermiso.Name = "DgvModuloColPermiso";
            this.DgvModuloColPermiso.Width = 26;
            // 
            // FrmGestion_Usuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 639);
            this.Controls.Add(this.DgvPermisos);
            this.Controls.Add(this.BtnAceptar);
            this.Controls.Add(this.Panel1);
            this.Name = "FrmGestion_Usuario";
            this.Load += new System.EventHandler(this.FrmGestion_Usuario_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvPermisos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button BtnAceptar;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button BtnNuevo;
        internal System.Windows.Forms.ComboBox CboUsuario;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.DataGridView DgvPermisos;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvModuloColID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvModuloColDescripciones;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DgvModuloColPermiso;
    }
}