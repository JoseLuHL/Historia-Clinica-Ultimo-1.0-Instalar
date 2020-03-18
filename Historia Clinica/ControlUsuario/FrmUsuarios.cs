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
    public partial class FrmUsuarios : Form
    {
        public FrmUsuarios()
        {
            InitializeComponent();
        }

        ClsSqlServer ObjServer = new ClsSqlServer();

        public bool agregar;
        public bool editar;
        public bool inactivar;

        //public void CARGAR_TIPO_IDENTIFICACION()
        //{
        //    //CARGAR COMBO DE TIPO DOCUMENTO
        //    ObjServer.Conectar();
        //    CboTipoDocumento.DataSource = ObjServer.LlenarTabla("SELECT TipoIde_Codigo   ,TipoIde_Descripcion FROM dbo.TipoDocumento ") ;

        //    CboTipoDocumento.DisplayMember = "TipoIde_Descripcion";
        //    CboTipoDocumento.ValueMember = "TipoIde_Codigo";

        //    if (editar==true || inactivar==true)
        //    {
        //        CboTipoDocumento.Visible = false;
        //        TxtContraseña.Visible = false;
        //        TxtNombre.Visible = false;
        //    }

        //}

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            //CARGAR_TIPO_IDENTIFICACION();
        }

        public void GUARDAR_DATOS()
        {
            if (TxtDocumento.Text.Trim()=="")
            {
                MessageBox.Show("Ingresar un numero de documento para continuar","",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
            if (TxtNombre.Text.Trim() == "")
            {
                MessageBox.Show("Ingresar un nombre para continuar", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (TxtContraseña.Text.Trim() == "")
            {
                MessageBox.Show("Ingresar una contraseña para continuar", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("¿Esta seguro de guardar la Información? ", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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
                        string SQL;

                        SQL = "INSERT INTO [dbo].[Usuario]        " +
                                     "([Usu_Nombre]                   " +
                                     ",[Usu_NombreCompleto]                      " +
                                     ",[Usu_Contraseña],Us_Estado )                 " +
                                     "VALUES                            " +
                                     "(@Usu_Nombre   " +
                                     ",@Usu_NombreCompleto     " +
                                     ",@contraseña, 1)";

                        comman.CommandText = SQL;
                        comman.Parameters.Add("@Usu_Nombre", SqlDbType.NVarChar);
                        comman.Parameters.Add("@Usu_NombreCompleto", SqlDbType.NVarChar);
                        comman.Parameters.Add("@contraseña", SqlDbType.NVarChar);

                        comman.Parameters["@Usu_Nombre"].Value = TxtDocumento.Text.Trim();
                        comman.Parameters["@Usu_NombreCompleto"].Value = TxtNombre.Text;
                        comman.Parameters["@contraseña"].Value = TxtContraseña.Text;

                    comman.ExecuteNonQuery();
                    SQLtrans.Commit();
                    MessageBox.Show("Registro guardado", "Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                    catch (Exception ex)
                    {
                        MessageBox.Show("La operación no puedo completarse debido a: \n 1 - No dispone de una conexión  \n 2 - Ya se ha registrado este documento  \n 3 - La información ingresada no corresponde a la requerida  \n Vuelva a intentarlo!!! " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageBox.Show(ex.ToString());
                        try
                        { SQLtrans.Rollback(); }
                        catch (Exception exRollback)
                        {
                            //Console.WriteLine(exRollback.Message); 
                        }
                    }
                }
            
        }

        public void EDITAR_DATOS()
        {
            if (TxtDocumento.Text.Trim() == "")
            {
                MessageBox.Show("Ingresar un numero de documento para continuar", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (TxtNombre.Text.Trim() == "")
            {
                MessageBox.Show("Ingresar un nombre para continuar", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (TxtContraseña.Text.Trim() == "")
            {
                MessageBox.Show("Ingresar una contraseña para continuar", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("¿Esta seguro de editar la Información? ", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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
                    string SQL;

                    //SQL = "DELETE FROM [dbo].[Empresa]";
                    //comman.CommandText = SQL;
                    //comman.ExecuteNonQuery();


                    SQL = "UPDATE [dbo].[Usuario]  SET  ,[Usu_NombreCompleto] = @Usu_NombreCompleto ,[Usu_Contraseña] = @contraseña WHERE [Usu_Nombre] = @Usu_Nombre";

                    comman.CommandText = SQL;
                    comman.Parameters.Add("@Usu_Nombre", SqlDbType.NVarChar);
                    comman.Parameters.Add("@Usu_NombreCompleto", SqlDbType.NVarChar);
                    comman.Parameters.Add("@contraseña", SqlDbType.NVarChar);

                    comman.Parameters["@Usu_Nombre"].Value = TxtDocumento.Text.Trim();
                    //comman.Parameters["@tipodocumento"].Value = CboTipoDocumento.SelectedValue.ToString();
                    comman.Parameters["@Usu_NombreCompleto"].Value = TxtNombre.Text;
                    comman.Parameters["@contraseña"].Value = TxtContraseña.Text;

                    comman.ExecuteNonQuery();
                    SQLtrans.Commit();

                    MessageBox.Show("Registro actualizado", "Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OcultarLimpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("La operación no puedo completarse debido a: \n 1 - No dispone de una conexión  \n 2 - Ya se ha registrado este documento  \n 3 - La información ingresada no corresponde a la requerida  \n Vuelva a intentarlo!!! " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show(ex.ToString());
                    try
                    { SQLtrans.Rollback(); }
                    catch (Exception exRollback)
                    {
                        //Console.WriteLine(exRollback.Message); 
                    }
                }
            }

        }

        public void INACTIVAR(int estado)
        {
            if (MessageBox.Show("¿Esta seguro de inactivar el usuario? ", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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
                    string SQL;

                    SQL = "UPDATE [dbo].[Usuario]  SET [Usu_Estado]=" + estado + " WHERE [Usu_Nombre] = @Usu_Nombre";

                   comman.CommandText = SQL;
                   comman.Parameters.Add("@Usu_Nombre", SqlDbType.NVarChar);

                   comman.Parameters["@Usu_Nombre"].Value = TxtDocumento.Text.Trim();
                    comman.ExecuteNonQuery();
                    SQLtrans.Commit();
                    MessageBox.Show("Usario inactivo", "Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OcultarLimpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("La operación no puedo completarse debido a: \n 1 - No dispone de una conexión  \n 2 - Ya se ha registrado este documento  \n 3 - La información ingresada no corresponde a la requerida  \n Vuelva a intentarlo!!! " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show(ex.ToString());
                    try
                    { SQLtrans.Rollback(); }
                    catch (Exception exRollback)
                    {
                        //Console.WriteLine(exRollback.Message); 
                    }
                }
            }

        }

        public void Limpiar()
        {
            TxtContraseña.Clear();            
            TxtNombre.Clear();
            TxtDocumento.Clear();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (agregar==true)
            {
                GUARDAR_DATOS();
            }
            if (editar==true)
            {
                EDITAR_DATOS();
            }
            if (inactivar==true)
            {
                INACTIVAR(2);
            }

        }

        private void TxtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjServer.Solo_Numeros(e);
        }

        private void TxtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjServer.Solo_Letras(e);
        }

        public void BuscarUsuario()
        {
            DataTable tabla = new DataTable();
            string sql = "SELECT [Usu_Nombre]       "+
                          ",[Usu_Contraseña]        "+
                          ",[Usu_NombreCompleto]    "+
                          ",[Usu_FechaCaduca]       "+
                          ",[Usu_Estado]            "+
                          ",[Usu_Tipo]              "+
                          "FROM [dbo].[Usuario]     "+
                            "FROM[dbo].[Usuario] WHERE Usu_Nombre='" + TxtDocumento.Text.Trim() + "'";
            tabla = ObjServer.LlenarTabla(sql);
            //MessageBox.Show(tabla.Rows.Count.ToString());
            if (tabla.Rows.Count > 0)
            {
                if (tabla.Rows[0]["Usu_Estado"].ToString() == "2")
                {
                    if (MessageBox.Show("El usuario esta inactivo ¿Desea activarlo? ", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        INACTIVAR(1);
                    }
                    else
                        return;
                }
                //CboTipoDocumento.SelectedValue = tabla.Rows[0]["Us_Tipo"];
                TxtNombre.Text = tabla.Rows[0]["Usu_NombreCompleto"].ToString();
                TxtContraseña.Text = tabla.Rows[0]["Usu_Contraseña"].ToString();
                //TxtDocumento.Text= tabla.Rows[0]["Us_Documento"].ToString();
                //CboTipoDocumento.Visible = true;
                TxtContraseña.Visible = true;
                TxtNombre.Visible = true;   
            }
            else
            {
                MessageBox.Show("No hay resultados para el numero de documento ingresado", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OcultarLimpiar();
            }
        }

        public void OcultarLimpiar()
        {
            TxtContraseña.Clear();
            TxtNombre.Clear();
            TxtContraseña.Visible = false;
            TxtNombre.Visible = false;
        }

        private void TxtDocumento_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TxtDocumento_TextChanged(object sender, EventArgs e)
        {
            if (inactivar || editar)
            {
                if (TxtNombre.Text!="")
                {
                    OcultarLimpiar();
                }                
            }
        }

        private void FrmUsuarios_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void FrmUsuarios_KeyUp(object sender, KeyEventArgs e)
        {
            if ((Keys)e.KeyValue == Keys.Escape)
                this.Close();

            if (e.KeyValue == 27)
            {
                this.Dispose();
            }
        }

        private void CboTipoDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void BtnGuardar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void TxtDocumento_KeyDown_1(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            if (editar || inactivar)
            {
                if (TxtDocumento.Text!="")
                {
                    BuscarUsuario();
                }
            }
        }
    }
}
