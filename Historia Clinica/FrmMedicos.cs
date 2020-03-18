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

namespace Historia_Clinica
{
    public partial class FrmMedicos : Form
    {
        public FrmMedicos()
        {
            InitializeComponent();
        }

        private void LblFirma_Click(object sender, EventArgs e)
        {
            // Se crea el OpenFileDialog
            OpenFileDialog dialog = new OpenFileDialog();
            // Se muestra al usuario esperando una acción
            DialogResult result = dialog.ShowDialog();

            // Si seleccionó un archivo (asumiendo que es una imagen lo que seleccionó)
            // la mostramos en el PictureBox de la inferfaz
            if (result == DialogResult.OK)
            {
                this.Pt_Firma.Image = Image.FromFile(dialog.FileName);
            }
        }

        private void LblHuella_Click(object sender, EventArgs e)
        {
            // Se crea el OpenFileDialog
            OpenFileDialog dialog = new OpenFileDialog();
            // Se muestra al usuario esperando una acción
            DialogResult result = dialog.ShowDialog();

            // Si seleccionó un archivo (asumiendo que es una imagen lo que seleccionó)
            // la mostramos en el PictureBox de la inferfaz
            if (result == DialogResult.OK)
            {
                this.PctSello.Image = Image.FromFile(dialog.FileName);

            }
        }
        Boolean Guardar_o_Modificar = true; //si es true es para guardar
        string documentoP = "";
        private void Lbl_Guardar_Click(object sender, EventArgs e)
        {
            if (TxtDocumento.Text == "")
                TxtDocumento.Focus();
            else
            {
                //MessageBox.Show(Guardar_o_Modificar.ToString()); Pac_CodEPS=@EPS, Pac_CodARL=@ARL
                if (Guardar_o_Modificar == true)
                    GUARDAR_DATOS();
                else
                    ACTUALIZAR_DATOS();
            }
        }

 
        public void GUARDAR_DATOS()
        {
            if (TxtDocumento.Text != "" && TxtNombre1.Text != "" && TxtApellido1.Text != "")
            {
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
                        string SQL = "INSERT INTO [dbo].[Medico]     "+
                                        "([Medic_TipoIdentificacion] "+
                                        ",[Medic_Identificacion]     "+
                                        ",[Medic_Nombre1]            "+
                                        ",[Medic_Nombre2]            "+
                                        ",[Medic_Apellido1]          "+
                                        ",[Medic_Apellido2]          "+
                                        ",[Medic_Foto]               "+
                                        ",[Medic_Firma],[Medic_NumeroTarjeta] ,[Medic_RegistroMedico])             " +
                                        " VALUES                     "+
                                        "(@TipoI,@ide,@n1,@n2,@a1,@a2,@foto1,@foto2,@a3,@a4)";

                        comman.CommandText = SQL;
                        comman.Parameters.AddWithValue("@identi", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@TipoI", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@ide", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@n1", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@n2", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@a1", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@a2", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@a3", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@a4", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@foto1", SqlDbType.Image);
                        comman.Parameters.AddWithValue("@foto2", SqlDbType.Image);

                        MessageBox.Show(TxtDocumento.Text);

                        comman.Parameters["@ide"].Value = TxtDocumento.Text;
                        comman.Parameters["@TipoI"].Value = CboTipoDocumento.SelectedValue.ToString();
                        comman.Parameters["@n1"].Value = TxtNombre1.Text;
                        comman.Parameters["@n2"].Value = TxtNombre2.Text;
                        comman.Parameters["@a1"].Value = TxtApellido1.Text;
                        comman.Parameters["@a2"].Value = TxtApellido2.Text;
                        comman.Parameters["@a3"].Value = TxtNumeroTarjeta;
                        comman.Parameters["@a4"].Value = TxtRegistroMedico;
 
                        if (PctSello.Image != null)
                        {
                            System.IO.MemoryStream ms1 = new System.IO.MemoryStream();
                            PctSello.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
                            comman.Parameters["@foto1"].Value = ms1.GetBuffer();
                        }
                        else
                            comman.Parameters["@foto1"].Value = DBNull.Value;
                       
                        if (Pt_Firma.Image != null)
                        {
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            Pt_Firma.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            comman.Parameters["@foto2"].Value = ms.GetBuffer();
                        }
                        else
                            comman.Parameters["@foto2"].Value = DBNull.Value;

                        comman.ExecuteNonQuery();
                        SQLtrans.Commit();
                        documentoP = TxtDocumento.Text;
                        TxtDocumento.Clear();
                        Limpiar();
                        MessageBox.Show("Registro guardado", "Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("La operación no puedo completarse debido a: \n 1 - No dispone de una conexión  \n 2 - Ya se ha registrado este documento  \n 3 - La información ingresada no corresponde a la requerida  \n Vuelva a intentarlo!!! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else
                MessageBox.Show("La operación no puedo completarse debido a: \n 1 - falta de datos obligatorio \n 2 - Ya se ha registrado este documento  \n 3 - La información ingresada no corresponde a la requerida  \n Vuelva a intentarlo!!! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ACTUALIZAR_DATOS()
        {
            if (TxtDocumento.Text != "" && TxtNombre1.Text != "" && TxtApellido1.Text != "")
            {
                SqlConnection cnn = new SqlConnection(CadenaConexion.cadena());
                cnn.Open();
                SqlTransaction SQLtrans = cnn.BeginTransaction();

                if (MessageBox.Show("¿Esta seguro de actualizar la Información? ", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        //[Pac_CodEPS],[Pac_CodARL]
                        Guardar_o_Modificar = false;
                        SqlCommand comman = cnn.CreateCommand();
                        comman.Transaction = SQLtrans;
                        string Query = "";

                        Query = "UPDATE [dbo].[Medico]                   " +
                               "SET [Medic_TipoIdentificacion] = @TipoI  " +
                               ",[Medic_Nombre1] = @n1                 " +
                               ",[Medic_Nombre2] = @n2                 " +
                               ",[Medic_Apellido1] = @a1               " +
                               ",[Medic_Apellido2] = @a2               " +
                               ",[Medic_Foto] = @foto1                 " +
                               ",[Medic_Firma] = @foto2                " +
                               ",[Medic_NumeroTarjeta] = @a3        " +
                               ",[Medic_RegistroMedico] = @a4       " +
                               "WHERE [Medic_Identificacion]=@identi";

                        comman.CommandText = Query;
                        comman.Parameters.AddWithValue("@identi", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@TipoI", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@ide", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@n1", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@n2", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@a1", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@a2", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@a3", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@a4", SqlDbType.NVarChar);
                        //comman.Parameters.AddWithValue("@foto1", SqlDbType.Image);
                        //comman.Parameters.AddWithValue("@foto2", SqlDbType.Image);

                        comman.Parameters["@identi"].Value = TxtDocumento.Text;
                        comman.Parameters["@TipoI"].Value = CboTipoDocumento.SelectedValue.ToString();
                        comman.Parameters["@n1"].Value = TxtNombre1.Text;
                        comman.Parameters["@n2"].Value = TxtNombre2.Text;
                        comman.Parameters["@a1"].Value = TxtApellido1.Text;
                        comman.Parameters["@a2"].Value = TxtApellido2.Text;
                        comman.Parameters["@a3"].Value = TxtNumeroTarjeta.Text;
                        comman.Parameters["@a4"].Value = TxtRegistroMedico.Text;

                        if (PctSello.Image != null)
                        {
                            System.IO.MemoryStream ms1 = new System.IO.MemoryStream();
                            PctSello.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
                            comman.Parameters.AddWithValue("@foto2", ms1.GetBuffer());
                        }
                        else
                        {
                            SqlParameter imageParameter = new SqlParameter("@foto2", SqlDbType.Image);
                            imageParameter.Value = DBNull.Value;
                            comman.Parameters.Add(imageParameter);
                        }
                        //comman.Parameters["@foto2"].Value = DBNull.Value;
                        
                        if (Pt_Firma.Image != null)
                        {
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            Pt_Firma.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            comman.Parameters.AddWithValue("@foto1", ms.GetBuffer());
                        }
                        else
                        {
                            SqlParameter imageParameter = new SqlParameter("@foto1", SqlDbType.Image);
                            imageParameter.Value = DBNull.Value;
                            comman.Parameters.Add(imageParameter);
                        }

                        comman.ExecuteNonQuery();
                        SQLtrans.Commit();

                        documentoP = TxtDocumento.Text;
                        Limpiar();
                        TxtDocumento.Clear();
                        TxtDocumento.Enabled = true;
                        MessageBox.Show("La Operación se ha completado!", "Finalizado", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("La operación no puedo completarse debido a: \n 1 - No dispone de una conexión  \n 2 - Ya se ha registrado este documento  \n 3 - La información ingresada no corresponde a la requerida  \n Vuelva a intentarlo!!! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        try
                        { SQLtrans.Rollback(); }
                        catch (Exception exRollback)
                        { Console.WriteLine(exRollback.Message); }
                    }
                }
            }
            else
                MessageBox.Show("No es posible continuar, se requiere información", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtDocumento.Focus();
        }

        public void Limpiar()
        {
            TxtNombre1.Clear();
            TxtNombre2.Clear();
            TxtApellido1.Clear();
            TxtApellido2.Clear();
            TxtNumeroTarjeta.Clear();
            TxtRegistroMedico.Clear();
            PctSello.Image = null;
            Pt_Firma.Image = null;
        }
        ClsSqlServer ObjServer = new ClsSqlServer();
        private  void FrmMedicos_Load(object sender, EventArgs e)
        {
            //CARGAR COMBO DE TIPO DOCUMENTO
            ObjServer.CadenaCnn = Historia_Clinica.Conexion.CadenaConexion.cadena();
            ObjServer.Conectar();

            CboTipoDocumento.DataSource = ObjServer.LlenarTabla("SELECT [TipoIde_Codigo] As Codigo ,[TipoIde_Descripcion] As Descripcion FROM [dbo].[TipoDocumento]"); ;
            CboTipoDocumento.DisplayMember = "Descripcion";
            CboTipoDocumento.ValueMember = "Codigo";
        }

        private void Btn_LupaBuscar_Click(object sender, EventArgs e)
        {
            if (TxtDocumento.Text != "")
            {
                BuscarPaciente_(TxtDocumento.Text);
            }
            else
            {
                TxtDocumento.Focus();
                MessageBox.Show("Ingrese un número de documento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public void BuscarPaciente_(string Documento_)
        {
            string Query = "SELECT [Medic_TipoIdentificacion] " +
                          ",[Medic_Identificacion] "+
                          ",[Medic_Nombre1]        "+
                          ",[Medic_Nombre2]        "+
                          ",[Medic_Apellido1]      "+
                          ",[Medic_Apellido2]      "+
                          ",[Medic_Foto]           "+
                          ",[Medic_Firma],[Medic_NumeroTarjeta] ,[Medic_RegistroMedico]          " +
                          "FROM [dbo].[Medico]     "+
                    "WHERE Medic_Identificacion='" + Documento_ + "'";
            //WHERE Pac_Identificacion=" + Documento;
            DataTable tablaPaciente2 = new DataTable();
            tablaPaciente2 = ObjServer.LlenarTabla(Query);
            if (tablaPaciente2.Rows.Count > 0)
            {
                Guardar_o_Modificar = false;
                CboTipoDocumento.SelectedValue = tablaPaciente2.Rows[0]["Medic_TipoIdentificacion"];

                TxtNombre1.Text = tablaPaciente2.Rows[0]["Medic_Nombre1"].ToString();
                TxtNombre2.Text = tablaPaciente2.Rows[0]["Medic_Nombre2"].ToString();
                TxtApellido1.Text = tablaPaciente2.Rows[0]["Medic_Apellido1"].ToString();
                TxtApellido2.Text = tablaPaciente2.Rows[0]["Medic_Apellido2"].ToString();
                TxtNumeroTarjeta.Text = tablaPaciente2.Rows[0]["Medic_NumeroTarjeta"].ToString();
                TxtRegistroMedico.Text = tablaPaciente2.Rows[0]["Medic_RegistroMedico"].ToString();                
                    
                // El campo productImage primero se almacena en un buffer
                if (tablaPaciente2.Rows[0]["Medic_Foto"].ToString() != "")
                {
                    byte[] imageBuffer = (byte[])tablaPaciente2.Rows[0]["Medic_Foto"];
                    // Se crea un MemoryStream a partir de ese buffer
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);
                    // Se utiliza el MemoryStream para extraer la imagen
                    this.Pt_Firma.Image = Image.FromStream(ms);
                }
                if (tablaPaciente2.Rows[0]["Medic_Firma"].ToString() != "")
                {
                    byte[] imageBuffer1 = (byte[])tablaPaciente2.Rows[0]["Medic_Firma"];
                    // Se crea un MemoryStream a partir de ese buffer
                    System.IO.MemoryStream ms1 = new System.IO.MemoryStream(imageBuffer1);
                    // Se utiliza el MemoryStream para extraer la imagen
                    this.PctSello.Image = Image.FromStream(ms1);
                }
            }
            else
            {
                Guardar_o_Modificar = true;
                Limpiar();
                if (MessageBox.Show("El Medico no se encuentra ¿Desea crearlo?", "Paciente", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    TxtNombre1.Focus();
                else
                {
                    TxtDocumento.Focus();
                    TxtDocumento.SelectAll();
                }
            }
            //tablaPaciente2.Dispose();
        }

        private void TxtDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                BuscarPaciente_(TxtDocumento.Text);
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void BtnLimpHuella_Click(object sender, EventArgs e)
        {
            PctSello.Image = null;
            
        }

        private void BtnLimpFirma_Click(object sender, EventArgs e)
        {
            Pt_Firma.Image = null;
        }
    }
}
