using Historia_Clinica;
using Historia_Clinica.Conexion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control_de_Tecnicos
{
    public partial class FrmGestion_Usuario : Form
    {
        public FrmGestion_Usuario()
        {
            InitializeComponent();
        }
        ClsSqlServer ObjServer = new ClsSqlServer();
        public void CargarDgv()
        {
            DgvPermisos.Rows.Clear();
            string Sql = "SELECT [Mod_Codigo] ,[Mod_Descripcion] FROM [dbo].[ModulosUsuario]  order by Mod_Descripcion desc";

            DataTable Tabla = new DataTable();
            Tabla = ObjServer.LlenarTabla(Sql);
            int x = 0;
            if (Tabla.Rows.Count > 0) {
                foreach (DataRow item in Tabla.Rows)
                {
                    x++;
                    DgvPermisos.Rows.Add(item["Mod_Codigo"].ToString(), item["Mod_Descripcion"].ToString());
                }
            }
            //ObjServer.es(DgvPermisos);
        }

        public void CargarDgvDelUsuario()
        {
            CargarDgv();
            String Sql = "SELECT	   dbo.Usuario.Usu_Tipo,                "+           
		                    "dbo.Usuario.Usu_Nombre,                        "+ 
		                    "dbo.Usuario.Usu_Contraseña,                    "+ 
		                    "dbo.Usuario.Usu_Estado,                        "+ 
		                    "dbo.UsuarioModulo.UsuMod_Usuario,              "+
		                    "dbo.UsuarioModulo.UsuMod_Modulo,               "+
		                    "dbo.ModulosUsuario.Mod_Codigo,                 "+
		                    "dbo.ModulosUsuario.Mod_Descripcion             "+
                            "FROM    dbo.Usuario INNER JOIN                 "+
                            "dbo.UsuarioModulo ON dbo.Usuario.Usu_Nombre =  "+
                            "dbo.UsuarioModulo.UsuMod_Usuario INNER JOIN    "+
                            "dbo.ModulosUsuario ON                          "+
                            "dbo.UsuarioModulo.UsuMod_Modulo =              "+
                            "dbo.ModulosUsuario.Mod_Codigo                   "+
                        "WHERE dbo.Usuario.Usu_Nombre = '" + CboUsuario.SelectedValue + "'";
            DataTable Tabla = new DataTable();
            Tabla = ObjServer.LlenarTabla(Sql);

            if (Tabla.Rows.Count > 0) {
                for (int index = 0; index<DgvPermisos.Rows.Count; index++)
                {
                    //MessageBox.Show(" Primero "+ DgvPermisos.Rows[index].Cells["DgvModuloColID"].Value.ToString());
                    for (int index2 = 0; index2< Tabla.Rows.Count; index2++)
                    {
                        //MessageBox.Show(" segundo " +  Tabla.Rows[index2]["Mod_Codigo"].ToString());
                        if (DgvPermisos.Rows[index].Cells["DgvModuloColID"].Value.ToString() == Tabla.Rows[index2]["Mod_Codigo"].ToString())
                        {
                            DgvPermisos.Rows[index].Cells["DgvModuloColPermiso"].Value = 1;
                            break;
                        }
                    }
            }                   
            }
        }
        void CARGAR_COMBOS()
        {
            try
            {

                ObjServer.Conectar();
                CboUsuario.DataSource = ObjServer.LlenarTabla("SELECT [Usu_Nombre],[Usu_Contraseña],[Usu_NombreCompleto],[Usu_FechaCaduca],[Usu_Estado],[Usu_Tipo]  FROM [dbo].[Usuario]"); ;
                CboUsuario.DisplayMember = "Usu_NombreCompleto";
                CboUsuario.ValueMember = "Usu_Nombre";


            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            FrmUsuarios f = new FrmUsuarios();
            f.ShowDialog();
        }

        private  void FrmGestion_Usuario_Load(object sender, EventArgs e)
        {
            //ObjServer.EstilosDgv(DgvPermisos);
             CARGAR_COMBOS();
             CboUsuario.Text = "";
        }

        private void CboUsuario_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarDgvDelUsuario();
        }

        public void GUARDAR_MODULO_USUARIO()
        {
            if (MessageBox.Show("¿Seguro que desea continuar?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Establecemos el Objeto que nos va a permitir conectarnos a la base de Datos()
                SqlConnection cnn = new SqlConnection(CadenaConexion.cadena());
                //Abrimos la conexión()
                cnn.Open();
                //Comenzamos la transacción ()
                SqlTransaction SQLtrans = cnn.BeginTransaction();
                try
                {
                    SqlCommand comman = cnn.CreateCommand();
                    comman.Transaction = SQLtrans;
                    string sql;
                    sql = "DELETE FROM [dbo].[UsuarioModulo] WHERE UsuMod_Usuario = '" + CboUsuario.SelectedValue + "'";
                    comman.CommandText = sql;
                    comman.ExecuteNonQuery();
                    string Usuario = CboUsuario.SelectedValue.ToString();
                    for (int index = 0; index < DgvPermisos.Rows.Count; index++)
                    {
                        if (Convert.ToBoolean(DgvPermisos.Rows[index].Cells["DgvModuloColPermiso"].Value) == true)
                        {
                            int CodModulo = Convert.ToInt32(DgvPermisos.Rows[index].Cells["DgvModuloColID"].Value);

                            sql = "INSERT INTO [dbo].[UsuarioModulo] ([UsuMod_Usuario] ,[UsuMod_Modulo]) VALUES ('" + Usuario + "'," + CodModulo + ")";
                            comman.CommandText = sql;
                            comman.ExecuteNonQuery();
                        }
                    }
                    SQLtrans.Commit();
                    MessageBox.Show("Operación Completada", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception)
                {
                    SQLtrans.Rollback();
                    throw;
                }
            }
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            GUARDAR_MODULO_USUARIO();
        }

        private void DgvPermisos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
