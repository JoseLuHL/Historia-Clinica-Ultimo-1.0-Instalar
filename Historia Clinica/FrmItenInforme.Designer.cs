namespace Historia_Clinica
{
    partial class FrmItenInforme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmItenInforme));
            this.DgvItems = new System.Windows.Forms.DataGridView();
            this.label13 = new System.Windows.Forms.Label();
            this.Lbl_Guardar = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DgvItemsColCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvItemsColDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvItemsColSi = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // DgvItems
            // 
            this.DgvItems.AllowUserToAddRows = false;
            this.DgvItems.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.DgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DgvItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DgvItemsColCodigo,
            this.DgvItemsColDescripcion,
            this.DgvItemsColSi});
            this.DgvItems.Location = new System.Drawing.Point(1, 63);
            this.DgvItems.Name = "DgvItems";
            this.DgvItems.Size = new System.Drawing.Size(504, 424);
            this.DgvItems.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.BackColor = System.Drawing.Color.SteelBlue;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(0, -2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(504, 65);
            this.label13.TabIndex = 9;
            this.label13.Text = "SELECCIONE ITEMS A VISUALIZAR EN EL INFORME";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Lbl_Guardar
            // 
            this.Lbl_Guardar.BackColor = System.Drawing.Color.Gray;
            this.Lbl_Guardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Lbl_Guardar.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Guardar.ForeColor = System.Drawing.Color.White;
            this.Lbl_Guardar.Image = ((System.Drawing.Image)(resources.GetObject("Lbl_Guardar.Image")));
            this.Lbl_Guardar.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.Lbl_Guardar.Location = new System.Drawing.Point(-5, 489);
            this.Lbl_Guardar.Name = "Lbl_Guardar";
            this.Lbl_Guardar.Size = new System.Drawing.Size(509, 51);
            this.Lbl_Guardar.TabIndex = 22;
            this.Lbl_Guardar.Text = "C O N T I N U A R ";
            this.Lbl_Guardar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_Guardar.Click += new System.EventHandler(this.Lbl_Guardar_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.SystemColors.Control;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(482, 45);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 23;
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(423, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.TabIndex = 24;
            this.label1.Text = "Todos";
            // 
            // DgvItemsColCodigo
            // 
            this.DgvItemsColCodigo.HeaderText = "Item";
            this.DgvItemsColCodigo.Name = "DgvItemsColCodigo";
            this.DgvItemsColCodigo.ReadOnly = true;
            this.DgvItemsColCodigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvItemsColCodigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvItemsColCodigo.Width = 40;
            // 
            // DgvItemsColDescripcion
            // 
            this.DgvItemsColDescripcion.HeaderText = "Descripción";
            this.DgvItemsColDescripcion.Name = "DgvItemsColDescripcion";
            this.DgvItemsColDescripcion.ReadOnly = true;
            this.DgvItemsColDescripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvItemsColDescripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DgvItemsColDescripcion.Width = 390;
            // 
            // DgvItemsColSi
            // 
            this.DgvItemsColSi.HeaderText = "SI";
            this.DgvItemsColSi.Name = "DgvItemsColSi";
            this.DgvItemsColSi.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvItemsColSi.Width = 30;
            // 
            // FrmItenInforme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 541);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Lbl_Guardar);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.DgvItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmItenInforme";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmItenInforme_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DgvItems;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label Lbl_Guardar;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvItemsColCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvItemsColDescripcion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DgvItemsColSi;
    }
}