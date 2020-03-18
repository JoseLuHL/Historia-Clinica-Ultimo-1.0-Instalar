using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using Historia_Clinica.Conexion;
namespace Historia_Clinica
{
    public partial class FrmItenInforme : Form
    {
        public FrmItenInforme()
        {
            InitializeComponent();
        }
        public void Cargar()
        {
            DgvItems.Rows.Clear();
            DataTable tabla = new DataTable();
            string Query = "SELECT [Intem_Codigo] "+
                            ",[Intem_Descripcion] "+
                            ",[Intem_Activo]      "+
                            "FROM [dbo].[ItemActivar]";
            //WHERE Pac_Identificacion=" + Documento;
            tabla = ObjServer.LlenarTabla(Query);
            if (tabla.Rows.Count > 0)
            {
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    string Cod = tabla.Rows[i]["Intem_Codigo"].ToString();
                    string Des = tabla.Rows[i]["Intem_Descripcion"].ToString();
                    Boolean activar = Convert.ToBoolean( tabla.Rows[i]["Intem_Activo"]);
                    DgvItems.Rows.Add(Cod, Des , activar);
                    if (i == 1)
                        DgvItems.Rows[i].Cells["DgvItemsColSi"].ReadOnly = true;
                }
            }
        }
        public void EstilosDgv(DataGridView DGV)
        {
            DGV.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            DGV.DefaultCellStyle.Font = new Font("Verdana", 10);
            Font prFont = new Font("Verdana", 11, FontStyle.Bold);
            for (int i = 0; i < DGV.Columns.Count; i++)
            {
                DGV.Columns[i].HeaderCell.Style.Font = prFont;
                DGV.Columns[i].HeaderCell.Style.ForeColor = Color.Black;
                DGV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                if (i % 2 == 0)
                    DGV.Rows[i].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else
                    DGV.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
            //DGV.AutoResizeColumns();
            DGV.EnableHeadersVisualStyles = false;
        }

        ClsSqlServer ObjServer = new ClsSqlServer();
        DataTable tablaItem = new DataTable();

        private void FrmItenInforme_Load(object sender, EventArgs e)
        {
            ObjServer.CadenaCnn = CadenaConexion.cadena();
            ObjServer.Conectar();
            EstilosDgv(DgvItems);
            Cargar();
        }
        public void GUARDAR_DATOS()
        {

                //Establecemos el Objeto que nos va a permitir conectarnos a la base de Datos()
                SqlConnection cnn = new SqlConnection(Conexion.CadenaConexion.cadena());
                //Abrimos la conexión()
                cnn.Open();
                //Comenzamos la transacción ()
                SqlTransaction SQLtrans = cnn.BeginTransaction();
                try
                {
                    tablaItem = new DataTable();
                    tablaItem.Columns.Add("codigo", typeof(int));
                    tablaItem.Columns.Add("descripcion", typeof(string));
                    tablaItem.Columns.Add("valor", typeof(bool));
                    SqlCommand comman = cnn.CreateCommand();
                    comman.Transaction = SQLtrans;
                    for (int i = 0; i < DgvItems.Rows.Count; i++)
                    {
                            Boolean activar = Convert.ToBoolean(DgvItems.Rows[i].Cells["DgvItemsColSi"].Value);
                            int codigo = Convert.ToInt32(DgvItems.Rows[i].Cells["DgvItemsColCodigo"].Value.ToString());
                            string des = (DgvItems.Rows[i].Cells["DgvItemsColDescripcion"].Value.ToString().Replace(" ", "_"));
                        
                            //Creo la tabla
                            //Agrego las dos columnas


                            //if (!activar)
                            //{
                            tablaItem.Rows.Add(codigo,des,activar);
                            //}

                            string SQL = "UPDATE [dbo].[ItemActivar]  SET [Intem_Activo] = @activar" + i + " WHERE Intem_Codigo=@codigo" + i;
                            comman.CommandText = SQL;
                            comman.Parameters.Add("@activar" + i, SqlDbType.Bit);
                            comman.Parameters.Add("@codigo" + i, SqlDbType.Int);
                            //MessageBox.Show(SQL + " "+ codigo.ToString() + " " + activar.ToString());
                            comman.Parameters["@activar" + i].Value = activar;
                            comman.Parameters["@codigo" + i].Value = codigo;
                            comman.ExecuteNonQuery();
                    }
                    SQLtrans.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("La operación no puedo completarse debido a: \n 1 - No dispone de una conexión  \n 2 - Ya se ha registrado este documento  \n 3 - La información ingresada no corresponde a la requerida  \n Vuelva a intentarlo!!! " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show(ex.ToString());
                    try
                    { SQLtrans.Rollback(); }
                    catch (Exception exRollback)
                    {
                    }
            }
        }
        public int NumeroAten;
        private void Lbl_Guardar_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("¿Esta seguro de guardar la Información? ", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //{
                Lbl_Guardar.Focus();
                GUARDAR_DATOS();
                FrmVerInforme f = new FrmVerInforme();
                f.NumeroAtencion = NumeroAten;
                f.TablaItem = tablaItem;
                f.ShowDialog();
            //}
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < DgvItems.Rows.Count; i++)
                {
                    DgvItems.Rows[i].Cells["DgvItemsColSi"].Value = 1;
                }                
            }
            else
            {
                for (int i = 0; i < DgvItems.Rows.Count; i++)
                {
                    DgvItems.Rows[i].Cells["DgvItemsColSi"].Value = 0;
                }
            }
        }
    }
}
