namespace Control_de_Tecnicos.Controles_Usuario
{
    partial class Control_Usuario
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnNuevo = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.TxtContraseña = new System.Windows.Forms.TextBox();
            this.DgvUsuario = new System.Windows.Forms.DataGridView();
            this.DgvClientesColID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvUsuarioColNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvUsuarioColUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvUsuarioColContraseña = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.TxtNombre = new System.Windows.Forms.TextBox();
            this.TxtUsuario = new System.Windows.Forms.TextBox();
            this.BtnAceptar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvUsuario)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnNuevo
            // 
            this.BtnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(172)))), ((int)(((byte)(0)))));
            this.BtnNuevo.FlatAppearance.BorderSize = 0;
            this.BtnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNuevo.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNuevo.ForeColor = System.Drawing.Color.White;
            this.BtnNuevo.Location = new System.Drawing.Point(264, 109);
            this.BtnNuevo.Name = "BtnNuevo";
            this.BtnNuevo.Size = new System.Drawing.Size(33, 34);
            this.BtnNuevo.TabIndex = 65;
            this.BtnNuevo.Text = "+";
            this.BtnNuevo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BtnNuevo.UseVisualStyleBackColor = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(63, 258);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(99, 20);
            this.Label1.TabIndex = 64;
            this.Label1.Text = "Contraseña:";
            // 
            // TxtContraseña
            // 
            this.TxtContraseña.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtContraseña.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtContraseña.Location = new System.Drawing.Point(67, 281);
            this.TxtContraseña.Name = "TxtContraseña";
            this.TxtContraseña.Size = new System.Drawing.Size(230, 26);
            this.TxtContraseña.TabIndex = 60;
            // 
            // DgvUsuario
            // 
            this.DgvUsuario.AllowUserToAddRows = false;
            this.DgvUsuario.AllowUserToDeleteRows = false;
            this.DgvUsuario.AllowUserToOrderColumns = true;
            this.DgvUsuario.AllowUserToResizeColumns = false;
            this.DgvUsuario.AllowUserToResizeRows = false;
            this.DgvUsuario.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.DgvUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DgvUsuario.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            this.DgvUsuario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvUsuario.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DgvClientesColID,
            this.DgvUsuarioColNombre,
            this.DgvUsuarioColUsuario,
            this.DgvUsuarioColContraseña});
            this.DgvUsuario.Location = new System.Drawing.Point(316, 109);
            this.DgvUsuario.Name = "DgvUsuario";
            this.DgvUsuario.ReadOnly = true;
            this.DgvUsuario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvUsuario.Size = new System.Drawing.Size(458, 75);
            this.DgvUsuario.TabIndex = 61;
            // 
            // DgvClientesColID
            // 
            this.DgvClientesColID.HeaderText = "#";
            this.DgvClientesColID.Name = "DgvClientesColID";
            this.DgvClientesColID.ReadOnly = true;
            // 
            // DgvUsuarioColNombre
            // 
            this.DgvUsuarioColNombre.HeaderText = "Nombre Completo";
            this.DgvUsuarioColNombre.Name = "DgvUsuarioColNombre";
            this.DgvUsuarioColNombre.ReadOnly = true;
            this.DgvUsuarioColNombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DgvUsuarioColUsuario
            // 
            this.DgvUsuarioColUsuario.HeaderText = "Usuario";
            this.DgvUsuarioColUsuario.MaxInputLength = 10;
            this.DgvUsuarioColUsuario.Name = "DgvUsuarioColUsuario";
            this.DgvUsuarioColUsuario.ReadOnly = true;
            this.DgvUsuarioColUsuario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DgvUsuarioColContraseña
            // 
            this.DgvUsuarioColContraseña.HeaderText = "Contraseña";
            this.DgvUsuarioColContraseña.Name = "DgvUsuarioColContraseña";
            this.DgvUsuarioColContraseña.ReadOnly = true;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(63, 194);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(67, 20);
            this.Label7.TabIndex = 63;
            this.Label7.Text = "Usuario:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(63, 135);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(68, 20);
            this.Label6.TabIndex = 62;
            this.Label6.Text = "Nombre";
            // 
            // TxtNombre
            // 
            this.TxtNombre.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtNombre.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNombre.Location = new System.Drawing.Point(67, 158);
            this.TxtNombre.Name = "TxtNombre";
            this.TxtNombre.Size = new System.Drawing.Size(230, 26);
            this.TxtNombre.TabIndex = 58;
            // 
            // TxtUsuario
            // 
            this.TxtUsuario.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TxtUsuario.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUsuario.Location = new System.Drawing.Point(67, 217);
            this.TxtUsuario.Name = "TxtUsuario";
            this.TxtUsuario.Size = new System.Drawing.Size(230, 26);
            this.TxtUsuario.TabIndex = 59;
            // 
            // BtnAceptar
            // 
            this.BtnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(172)))), ((int)(((byte)(0)))));
            this.BtnAceptar.FlatAppearance.BorderSize = 0;
            this.BtnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAceptar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAceptar.ForeColor = System.Drawing.Color.White;
            this.BtnAceptar.Location = new System.Drawing.Point(163, 332);
            this.BtnAceptar.Name = "BtnAceptar";
            this.BtnAceptar.Size = new System.Drawing.Size(134, 27);
            this.BtnAceptar.TabIndex = 57;
            this.BtnAceptar.Text = "Aceptar";
            this.BtnAceptar.UseVisualStyleBackColor = false;
            // 
            // Control_Usuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnNuevo);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.TxtContraseña);
            this.Controls.Add(this.DgvUsuario);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.TxtNombre);
            this.Controls.Add(this.TxtUsuario);
            this.Controls.Add(this.BtnAceptar);
            this.MinimumSize = new System.Drawing.Size(126, 39);
            this.Name = "Control_Usuario";
            this.Size = new System.Drawing.Size(837, 505);
            ((System.ComponentModel.ISupportInitialize)(this.DgvUsuario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnNuevo;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox TxtContraseña;
        internal System.Windows.Forms.DataGridView DgvUsuario;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DgvClientesColID;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DgvUsuarioColNombre;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DgvUsuarioColUsuario;
        internal System.Windows.Forms.DataGridViewTextBoxColumn DgvUsuarioColContraseña;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox TxtNombre;
        internal System.Windows.Forms.TextBox TxtUsuario;
        private System.Windows.Forms.Button BtnAceptar;
    }
}
