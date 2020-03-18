using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;
using Historia_Clinica.Conexion;
namespace Historia_Clinica
{
    public partial class Paciente : Form
    {
        public Paciente()
        {
            InitializeComponent();
        }
        ClsSqlServer ObjServer = new ClsSqlServer();

        //Para cambiar la apariencia de los DGV
        public void EstilosDgv(DataGridView DGV)
        {
            DGV.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            DGV.DefaultCellStyle.Font = new Font("Verdana", 10);
            Font prFont = new Font("Verdana", 11, FontStyle.Bold);
            for (int i = 0; i < DGV.Columns.Count; i++)
            {
                DGV.Columns[i].HeaderCell.Style.Font = prFont;
                DGV.Columns[i].HeaderCell.Style.ForeColor = Color.Black;
                //DGV.Columns[i].HeaderCell.Style.BackColor = Color.Transparent;
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

        private void selecctionarAgregado(string documento)
        {
            DgvDatos.ClearSelection();
            for (int i = 0; i < DgvDatos.Rows.Count; i++)
            {
                if (DgvDatos.Rows[i].Cells["DgvDatosColDocumento"].Value.ToString() == documento)
                {
                    DgvDatos.CurrentCell = DgvDatos.Rows[i].Cells["DgvDatosColDocumento"];
                    break;
                }
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
                        string SQL = "INSERT INTO [dbo].[Paciente] " +
                                              "([Pac_TipoIdentificacion] " +
                                              ",[Pac_Identificacion]     " +
                                              ",[Pac_Nombre1]            " +
                                              ",[Pac_Nombre2]            " +
                                              ",[Pac_Apellido1]          " +
                                              ",[Pac_Apellido2]          " +
                                              ",[Pac_FechaNacimiento]    " +
                                              ",[Pac_CodGenero]      " +
                                              ",Pac_CodDepto " +
                                              ",Pac_CodCiudad " +
                                              ",[Pac_Direccion]          " +
                                              ",[Pac_CodNivelEducativo]     " +
                                              ",[Pac_CodProfesion]   " +
                                              ",[Pac_TipoSangre]         " +
                                              ",[Pac_EstadoCivil]        " +
                                              ",Pac_Telefono,Pac_Foto,Pac_Huella,Pac_Firma,Pac_Dominancia_Codigo,Pac_Fecha,Pac_CodEPS,          Pac_CodARL) " +
                                        " VALUES (@TipoI,@ide,@n1,@n2,@a1,@a2,@fecha,@genero,@CodDepartamento,@CodCiudad,@dire,@nivel,@pro,@tipos,@estado,@tel,@foto1,@foto2,@firma,@dominancia,@fechaIngreso, @EPS, @ARL)";
                      //  Pac_CodEPS = @EPS, Pac_CodARL = @ARL
                        comman.CommandText = SQL;
                        comman.Parameters.Add("@TipoI", SqlDbType.NVarChar);
                        comman.Parameters.Add("@ide", SqlDbType.NVarChar);
                        comman.Parameters.Add("@n1", SqlDbType.NVarChar);
                        comman.Parameters.Add("@n2", SqlDbType.NVarChar);
                        comman.Parameters.Add("@a1", SqlDbType.NVarChar);
                        comman.Parameters.Add("@a2", SqlDbType.NVarChar);
                        comman.Parameters.Add("@fecha", SqlDbType.Date);
                        comman.Parameters.Add("@genero", SqlDbType.NVarChar);
                        comman.Parameters.Add("@CodDepartamento", SqlDbType.NVarChar);
                        comman.Parameters.Add("@CodCiudad", SqlDbType.NVarChar);
                        comman.Parameters.Add("@dire", SqlDbType.NVarChar);
                        comman.Parameters.Add("@nivel", SqlDbType.Int);
                        comman.Parameters.Add("@pro", SqlDbType.NVarChar);
                        comman.Parameters.Add("@tipos", SqlDbType.Int);
                        comman.Parameters.Add("@estado", SqlDbType.Int);

                        comman.Parameters.Add("@EPS", SqlDbType.Int);
                        comman.Parameters.Add("@ARL", SqlDbType.Int);

                        comman.Parameters.Add("@tel", SqlDbType.NVarChar);
                        comman.Parameters.Add("@foto1", SqlDbType.Image);
                        comman.Parameters.Add("@foto2", SqlDbType.Image);
                        comman.Parameters.Add("@firma", SqlDbType.Image);
                        comman.Parameters.Add("@dominancia", SqlDbType.Int);
                        comman.Parameters.Add("@fechaIngreso", SqlDbType.Date);

                        comman.Parameters["@TipoI"].Value = CboTipoDocumento.SelectedValue.ToString();
                        comman.Parameters["@ide"].Value = TxtDocumento.Text;
                        comman.Parameters["@n1"].Value = TxtNombre1.Text;
                        comman.Parameters["@n2"].Value = TxtNombre2.Text;
                        comman.Parameters["@a1"].Value = TxtApellido1.Text;
                        comman.Parameters["@a2"].Value = TxtApellido2.Text;
                        comman.Parameters["@fecha"].Value = DtFechaNacimiento.Value.ToShortDateString();
                        comman.Parameters["@genero"].Value = CboGenero.SelectedValue.ToString();
                        comman.Parameters["@CodDepartamento"].Value = Cbo_Departamento.SelectedValue.ToString();
                        comman.Parameters["@CodCiudad"].Value = Cbo_Municipio.SelectedValue.ToString();
                        //comman.Parameters["@lugar"].Value = TxtLugarNacimiento.Text;
                        comman.Parameters["@dire"].Value = TxtDireccion.Text;
                        comman.Parameters["@nivel"].Value = Cbo_NiverEducativo.SelectedValue.ToString();
                        comman.Parameters["@pro"].Value = Cbo_Profesion.SelectedValue.ToString();
                        comman.Parameters["@tipos"].Value = CboTipoSangre.SelectedValue;
                        comman.Parameters["@estado"].Value = CboEstadoCivil.SelectedValue;
                        comman.Parameters["@tel"].Value = TxtTelefono.Text;
                        comman.Parameters["@dominancia"].Value = CboDominancia.SelectedValue;

                        comman.Parameters["@EPS"].Value = CboEPS.SelectedValue;
                        comman.Parameters["@ARL"].Value = CboARL.SelectedValue;

                        string[] fecha = DateTime.Now.ToString().Split(' ');
                        comman.Parameters["@fechaIngreso"].Value = fecha[0];
                        if (PctHuella.Image != null)
                        {
                            System.IO.MemoryStream ms1 = new System.IO.MemoryStream();
                            PctHuella.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
                            comman.Parameters["@foto2"].Value = ms1.GetBuffer();
                        }
                        else
                            comman.Parameters["@foto2"].Value = DBNull.Value;
                        if (PctFoto.Image != null)
                        {
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            PctFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            comman.Parameters["@foto1"].Value = ms.GetBuffer();
                        }
                        else
                            comman.Parameters["@foto1"].Value = DBNull.Value;

                        if (Pt_Firma.Image != null)
                        {
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            Pt_Firma.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            comman.Parameters["@firma"].Value = ms.GetBuffer();
                        }

                        else
                            comman.Parameters["@firma"].Value = DBNull.Value;

                        comman.ExecuteNonQuery();
                        SQLtrans.Commit();
                        documentoP = TxtDocumento.Text;
                        TxtDocumento.Clear();
                        Limpiar();
                        BuscarPaciente("");
                        selecctionarAgregado(documentoP);
                        Limpiar();
                        MessageBox.Show("Registro guardado", "Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            else
                MessageBox.Show("La operación no puedo completarse debido a: \n 1 - falta de datos obligatorio \n 2 - Ya se ha registrado este documento  \n 3 - La información ingresada no corresponde a la requerida  \n Vuelva a intentarlo!!! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void Limpiar()
        {
            TxtNombre1.Clear();
            TxtNombre2.Clear();
            TxtApellido1.Clear();
            TxtApellido2.Clear();
            TxtLugarNacimiento.Clear();
            TxtDireccion.Clear();
            TxtNiverEducativo.Clear();
            TxtProfesion.Clear();
            TxtTelefono.Clear();
            PctHuella.Image = null;
            PctFoto.Image = null;
            Pt_Firma.Image = null;
            //CboDominancia.SelectedValue = 1;
            TxtEdad.Text = "";
            DtFechaNacimiento.Value = DateTime.Now.Date;

        }
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

        public void BuscarPaciente_(string Documento_)
        {
            DgvDatos.ClearSelection();

            string Query = "SELECT [Pac_TipoIdentificacion] " +
                    " ,[Pac_Identificacion]          " +
                    " ,[Pac_Nombre1]                 " +
                    " ,[Pac_Nombre2]                 " +
                    " ,[Pac_Apellido1]               " +
                    " ,[Pac_Apellido2]               " +
                    " ,[Pac_FechaNacimiento]         " +
                    " ,[Pac_CodGenero]               " +
                    " ,Pac_CodCiudad                 " +
                    " ,Pac_CodDepto                  " +
                    " ,[Pac_Direccion]               " +
                    " ,[Pac_CodNivelEducativo]       " +
                    " ,[Pac_CodProfesion]            " +
                    " ,[Pac_TipoSangre]              " +
                    " ,[Pac_EstadoCivil]             " +
                    ",Pac_Telefono,Pac_Foto,Pac_Huella, Pac_firma,Pac_Dominancia_Codigo,Pac_Fecha,Pac_CodEPS, Pac_CodARL             " +
                    " FROM [dbo].[Paciente] WHERE Pac_Identificacion='" + Documento_ + "'";
            //WHERE Pac_Identificacion=" + Documento;
            DataTable tablaPaciente2 = new DataTable();
            tablaPaciente2 = ObjServer.LlenarTabla(Query);
            if (tablaPaciente2.Rows.Count > 0)
            {
                IdPacienteTra = TxtDocumento.Text;
                Guardar_o_Modificar = false;
                CboTipoDocumento.SelectedValue = tablaPaciente2.Rows[0]["Pac_TipoIdentificacion"];
                Cbo_Departamento.SelectedValue = tablaPaciente2.Rows[0]["Pac_CodDepto"];
                Cbo_Municipio.SelectedValue = tablaPaciente2.Rows[0]["Pac_CodCiudad"];
                Cbo_NiverEducativo.SelectedValue = tablaPaciente2.Rows[0]["Pac_CodNivelEducativo"];
                Cbo_Profesion.SelectedValue = tablaPaciente2.Rows[0]["Pac_CodProfesion"];

                CboEPS.SelectedValue = tablaPaciente2.Rows[0]["Pac_CodEPS"];
                CboARL.SelectedValue = tablaPaciente2.Rows[0]["Pac_CodARL"];

                numeroDocumentoPaciete = Documento_;
                TxtNombre1.Text = tablaPaciente2.Rows[0]["Pac_Nombre1"].ToString();
                TxtNombre2.Text = tablaPaciente2.Rows[0]["Pac_Nombre2"].ToString();
                TxtApellido1.Text = tablaPaciente2.Rows[0]["Pac_Apellido1"].ToString();
                TxtApellido2.Text = tablaPaciente2.Rows[0]["Pac_Apellido2"].ToString();
                DtFechaNacimiento.Text = tablaPaciente2.Rows[0]["Pac_FechaNacimiento"].ToString();
                TxtEdad.Text = CalcularEdad(Convert.ToDateTime(DtFechaNacimiento.Text)).ToString();
                CboGenero.SelectedValue = tablaPaciente2.Rows[0]["Pac_CodGenero"];
                TxtDireccion.Text = tablaPaciente2.Rows[0]["Pac_Direccion"].ToString();
                CboTipoSangre.SelectedValue = tablaPaciente2.Rows[0]["Pac_TipoSangre"];
                CboEstadoCivil.SelectedValue = tablaPaciente2.Rows[0]["Pac_EstadoCivil"];
                TxtTelefono.Text = tablaPaciente2.Rows[0]["Pac_Telefono"].ToString();
                CboDominancia.SelectedValue = tablaPaciente2.Rows[0]["Pac_Dominancia_Codigo"].ToString();
                // El campo productImage primero se almacena en un buffer
                if (tablaPaciente2.Rows[0]["Pac_Foto"].ToString() != "")
                {
                    byte[] imageBuffer = (byte[])tablaPaciente2.Rows[0]["Pac_Foto"];
                    // Se crea un MemoryStream a partir de ese buffer
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);
                    // Se utiliza el MemoryStream para extraer la imagen
                    this.PctFoto.Image = Image.FromStream(ms);
                }
                if (tablaPaciente2.Rows[0]["Pac_Huella"].ToString() != "")
                {
                    byte[] imageBuffer1 = (byte[])tablaPaciente2.Rows[0]["Pac_Huella"];
                    // Se crea un MemoryStream a partir de ese buffer
                    System.IO.MemoryStream ms1 = new System.IO.MemoryStream(imageBuffer1);
                    // Se utiliza el MemoryStream para extraer la imagen
                    this.PctHuella.Image = Image.FromStream(ms1);
                }
                if (tablaPaciente2.Rows[0]["Pac_firma"].ToString() != "")
                {
                    byte[] imageBuffer1 = (byte[])tablaPaciente2.Rows[0]["Pac_firma"];
                    // Se crea un MemoryStream a partir de ese buffer
                    System.IO.MemoryStream ms1 = new System.IO.MemoryStream(imageBuffer1);
                    // Se utiliza el MemoryStream para extraer la imagen
                    this.Pt_Firma.Image = Image.FromStream(ms1);
                }
            }
            else
            {
                Guardar_o_Modificar = true;
                Limpiar();
                if (MessageBox.Show("El paciente no se encuentra ¿Desea crearlo?", "Paciente", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    TxtNombre1.Focus();
                else
                {
                    TxtDocumento.Focus();
                    TxtDocumento.SelectAll();
                }
            }
            //tablaPaciente2.Dispose();
        }


        //BUSCAR UN PACIENTE
        DataTable tablaPaciente = new DataTable();
        public async Task<string> BuscarPaciente(string val)
        {
            DgvDatos.Rows.Clear();
            //if (Documento!="") 
            //{
            string Query = "SELECT TOP 20 [Pac_TipoIdentificacion] " +
                    " ,[Pac_Identificacion]          " +
                    " ,[Pac_Nombre1]                 " +
                    " ,[Pac_Nombre2]                 " +
                    " ,[Pac_Apellido1]               " +
                    " ,[Pac_Apellido2]               " +
                    " ,[Pac_FechaNacimiento]         " +
                    " ,[Pac_CodGenero]               " +
                    " ,Pac_CodCiudad                 " +
                    " ,Pac_CodDepto                  " +
                    " ,[Pac_Direccion]               " +
                    " ,[Pac_CodProfesion]       " +
                    " ,Pac_CodNivelEducativo " +
                    " ,[Pac_TipoSangre]              " +
                    " ,[Pac_EstadoCivil]             " +
                    ",Pac_Telefono,Pac_Foto,Pac_Huella, Pac_firma,Pac_Dominancia_Codigo,Pac_Fecha,Pac_CodEPS, Pac_CodARL                 " +
                    " FROM [dbo].[Paciente] ORDER BY Pac_Fecha desc";
            //WHERE Pac_Identificacion=" + Documento;
             await Task.Run(() => { tablaPaciente = ObjServer.LlenarTabla(Query); }); 
            if (tablaPaciente.Rows.Count > 0)
            {
                for (int i = 0; i < tablaPaciente.Rows.Count; i++)
                {
                    string nombre1 = tablaPaciente.Rows[i]["Pac_Nombre1"].ToString();
                    string nombre2 = tablaPaciente.Rows[i]["Pac_Nombre2"].ToString();
                    string apellido1 = tablaPaciente.Rows[i]["Pac_Apellido1"].ToString();
                    string apellido2 = tablaPaciente.Rows[i]["Pac_Apellido2"].ToString();
                    DgvDatos.Rows.Add((i + 1).ToString(), tablaPaciente.Rows[i]["Pac_Identificacion"], nombre1 + " " + nombre2 + " " + apellido1 + " " + apellido2);
                }

                if (tablaPaciente.Rows[0]["Pac_Foto"].ToString() != "")
                {
                    byte[] imageBuffer = (byte[])tablaPaciente.Rows[0]["Pac_Foto"];
                    // Se crea un MemoryStream a partir de ese buffer
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);
                    // Se utiliza el MemoryStream para extraer la imagen
                    this.PctFoto.Image = Image.FromStream(ms);
                }
                if (tablaPaciente.Rows[0]["Pac_Huella"].ToString() != "")
                {
                    byte[] imageBuffer1 = (byte[])tablaPaciente.Rows[0]["Pac_Huella"];
                    // Se crea un MemoryStream a partir de ese buffer
                    System.IO.MemoryStream ms1 = new System.IO.MemoryStream(imageBuffer1);
                    // Se utiliza el MemoryStream para extraer la imagen
                    this.PctHuella.Image = Image.FromStream(ms1);
                }
                if (tablaPaciente.Rows[0]["Pac_firma"].ToString() != "")
                {
                    byte[] imageBuffer1 = (byte[])tablaPaciente.Rows[0]["Pac_firma"];
                    // Se crea un MemoryStream a partir de ese buffer
                    System.IO.MemoryStream ms1 = new System.IO.MemoryStream(imageBuffer1);
                    // Se utiliza el MemoryStream para extraer la imagen
                    this.Pt_Firma.Image = Image.FromStream(ms1);
                }
            }
            else
            {
                Limpiar();
            }
            return string.Empty;
        }
        public void CargarDatosEmpresa()
        {
            SqlConnection conexion = new SqlConnection(Historia_Clinica.Conexion.CadenaConexion.cadena());
            conexion.Open();
            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            string consulta = @"SELECT [Empr_Codigo] ,[Empre_Nit] as nit ,[Empre_RazonSocial] as nombre FROM [dbo].[Empresa]";
            SqlCommand cmd = new SqlCommand(consulta, conexion);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    namesCollection.Add(dr["nombre"].ToString());
                    namesCollection.Add(dr["nit"].ToString() + "-" + dr["nombre"].ToString());
                }
            }

            dr.Close();
            conexion.Close();

            TxtApellido1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TxtApellido1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TxtApellido1.AutoCompleteCustomSource = namesCollection;
        }
        public static int CalcularEdad(DateTime fechaNacimiento)
        {
            // Obtiene la fecha actual:
            DateTime fechaActual = DateTime.Today;


            // Comprueba que la se haya introducido una fecha válida; si
            // la fecha de nacimiento es mayor a la fecha actual se muestra mensaje
            // de advertencia:
            if (fechaNacimiento > fechaActual)
            {
                Console.WriteLine("La fecha de nacimiento es mayor que la actual.");
                return -1;
            }
            else
            {
                int edad = fechaActual.Year - fechaNacimiento.Year;
                // Comprueba que el mes de la fecha de nacimiento es mayor
                // que el mes de la fecha actual:
                if (fechaNacimiento.Month > fechaActual.Month)
                {
                    --edad;
                }

                return edad;
            }
        }
         async Task CARGAR_COMBOS()
        {
            //CARGAR COMBO DE TIPO DOCUMENTO
            await Task.Run(() => { 
            CboTipoDocumento.DataSource = ObjServer.LlenarTabla("SELECT [TipoIde_Codigo] As Codigo ,[TipoIde_Descripcion] As Descripcion FROM [dbo].[TipoDocumento]");;
            });
            CboTipoDocumento.DisplayMember = "Descripcion";
            CboTipoDocumento.ValueMember = "Codigo";

            //CARGAR COMBO DE TIPO GENERO
            await Task.Run(() =>
            {
                CboGenero.DataSource = ObjServer.LlenarTabla(" SELECT [Gen_Codigo] As Codigo, [Gen_Descripcion] As Descripcion  FROM [dbo].[Genero] ORDER BY Descripcion");
            });
            CboGenero.DisplayMember = "Descripcion";
            CboGenero.ValueMember = "Codigo";

            //CARGAR COMBO DE ESTADO CIVIL
            await Task.Run(() =>
            {
                CboEstadoCivil.DataSource = ObjServer.LlenarTabla("SELECT [EstCivil_Codigo] As Codigo ,[EstCivil_Descripcion] As Descripcion  FROM [dbo].[EstadoCivil] ORDER BY Descripcion");
            });
                CboEstadoCivil.DisplayMember = "Descripcion";
            CboEstadoCivil.ValueMember = "Codigo";

            //CARGAR COMBO DE TIPOS DE SANGRE
            await Task.Run(() =>
            {
                CboTipoSangre.DataSource = ObjServer.LlenarTabla("SELECT [TipSan_Codigo] As Codigo ,[TipSan_Descripcion] As Descripcion  FROM [dbo].[TipoSangre] ORDER BY Descripcion");
            });
                CboTipoSangre.DisplayMember = "Descripcion";
            CboTipoSangre.ValueMember = "Codigo";

            //CARGAR COMBO DE TIPOS DE DOMINANCIA
            await Task.Run(() =>
            {
                CboDominancia.DataSource = ObjServer.LlenarTabla("SELECT [Dom_Codigo] As Codigo ,[Dom_Descripcion] As Descripcion FROM [dbo].[Dominancia] ORDER BY Descripcion");
            });
                CboDominancia.DisplayMember = "Descripcion";
            CboDominancia.ValueMember = "Codigo";

            //CARGAR COMBO DE TIPOS DE NIVEL EDUCATIVO
            await Task.Run(() =>
            {
                Cbo_NiverEducativo.DataSource = ObjServer.LlenarTabla("SELECT [NivEdu_Codigo] AS Codigo,[NivEdu_Descripcion] AS Descripcion FROM [dbo].[NivelEducativo] ORDER BY Codigo");
            });
                Cbo_NiverEducativo.DisplayMember = "Descripcion";
            Cbo_NiverEducativo.ValueMember = "Codigo";

            //CARGAR COMBO DE TIPOS DE NIVEL DEPARTAMENTO
            await Task.Run(() =>
            {
                Cbo_Departamento.DataSource = ObjServer.LlenarTabla("SELECT [Dept_Codigo] AS Codigo,[Dept_Nombre] AS Descripcion FROM [dbo].[Departamento] ORDER BY Descripcion");
            });
                Cbo_Departamento.DisplayMember = "Descripcion";
            Cbo_Departamento.ValueMember = "Codigo";
            //Cbo_Departamento.s

            //CARGAR COMBO DE TIPOS DE NIVEL PRFESIÓN
            await Task.Run(() =>
            {
                Cbo_Profesion.DataSource = ObjServer.LlenarTabla("SELECT [Prof_Codigo] AS Codigo,[Prof_Descripcion] AS Descripcion FROM [dbo].[Profesion] ORDER BY Descripcion");
            });
                Cbo_Profesion.DisplayMember = "Descripcion";
                Cbo_Profesion.ValueMember = "Codigo";

            //CARGAR COMBO DE TIPOS DE EPS
            await Task.Run(() =>
            {
                CboEPS.DataSource = ObjServer.LlenarTabla("SELECT [Eps_Codigo] ,[Eps_Descripcion] FROM [dbo].[EPS]");
            });
            CboEPS.DisplayMember = "Eps_Descripcion";
            CboEPS.ValueMember = "Eps_Codigo";

            //CARGAR COMBO DE TIPOS DE ARL
            await Task.Run(() =>
            {
                CboARL.DataSource = ObjServer.LlenarTabla("SELECT [Arl_Codigo] ,[Arl_Descripcion]  FROM [dbo].[ARL]");
            });
            CboARL.DisplayMember = "Arl_Descripcion";
            CboARL.ValueMember = "Arl_Codigo";

        }
        private async void Paciente_Load(object sender, EventArgs e)
        {
            ObjServer.CadenaCnn = Historia_Clinica.Conexion.CadenaConexion.cadena();
            ObjServer.Conectar();

            await CARGAR_COMBOS();

            //CAMBIAR ASPECTO DEL DGV

            Limpiar();
            DgvDatos.ClearSelection();
            
            await BuscarPaciente("");
            EstilosDgv(DgvDatos);
        }

        //[Pac_CodEPS],[Pac_CodARL]
   
        private void Btn_LupaBuscar_Click(object sender, EventArgs e)
        {
            if (TxtDocumento.Text != "")
            {
                BuscarPaciente_(TxtDocumento.Text);
                DgvDatos.ClearSelection();
            }
            else
            {
                TxtDocumento.Focus();
                MessageBox.Show("Ingrese un número de documento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        string IdPacienteTra = "";
        private void TxtDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BuscarPaciente_(TxtDocumento.Text);
            }
        }
        string numeroDocumentoPaciete;
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
                        string Query = "UPDATE [dbo].[Paciente] " +
                            "SET [Pac_Identificacion]= @identi, [Pac_TipoIdentificacion] = @TipoI" +
                            ",[Pac_Nombre1] = @n1" +
                            ",[Pac_Nombre2] = @n2" +
                            ",[Pac_Apellido1] = @a1" +
                            ",[Pac_Apellido2] = @a2" +
                            ",[Pac_FechaNacimiento] = @fecha" +
                            ",[Pac_CodGenero] =   @genero" +
                            ",Pac_CodDepto = @CodDepartamento " +
                            ",Pac_CodCiudad = @CodCiudad " +
                            ",[Pac_Direccion] = @dire" +
                            ",[Pac_CodNivelEducativo] = @nivel" +
                            ",[Pac_CodProfesion] = @pro" +
                            ",[Pac_TipoSangre] = @tipos" +
                            ",[Pac_EstadoCivil] = @estado" +
                            ",[Pac_Telefono] = @tel, Pac_Foto=@foto1, Pac_Huella=@foto2, "+
                            " Pac_Firma=@firma, Pac_Dominancia_Codigo=@dominancia, Pac_CodEPS=@EPS, Pac_CodARL=@ARL " +
                            " WHERE [Pac_Identificacion]= @ide";

                        comman.CommandText = Query;
                        comman.Parameters.AddWithValue("@identi", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@TipoI", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@ide", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@n1", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@n2", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@a1", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@a2", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@fecha", SqlDbType.Date);
                        comman.Parameters.AddWithValue("@genero", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@dire", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@nivel", SqlDbType.NChar);
                        comman.Parameters.AddWithValue("@pro", SqlDbType.Int);
                        comman.Parameters.AddWithValue("@tipos", SqlDbType.Int);
                        comman.Parameters.AddWithValue("@EPS", SqlDbType.Int);
                        comman.Parameters.AddWithValue("@ARL", SqlDbType.Int);
                        comman.Parameters.AddWithValue("@estado", SqlDbType.Int);
                        comman.Parameters.AddWithValue("@tel", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@dominancia", SqlDbType.Int);
                        comman.Parameters.AddWithValue("@CodDepartamento", SqlDbType.NVarChar);
                        comman.Parameters.AddWithValue("@CodCiudad", SqlDbType.NVarChar);

                        comman.Parameters["@identi"].Value = TxtDocumento.Text;
                        comman.Parameters["@TipoI"].Value = CboTipoDocumento.SelectedValue.ToString();
                        comman.Parameters["@ide"].Value = numeroDocumentoPaciete ;
                        comman.Parameters["@n1"].Value = TxtNombre1.Text;
                        comman.Parameters["@n2"].Value = TxtNombre2.Text;
                        comman.Parameters["@a1"].Value = TxtApellido1.Text;
                        comman.Parameters["@fecha"].Value = DtFechaNacimiento.Value.Date.Date;
                        comman.Parameters["@a2"].Value = TxtApellido2.Text;
                        comman.Parameters["@genero"].Value = CboGenero.SelectedValue.ToString();

                        comman.Parameters["@CodDepartamento"].Value = Cbo_Departamento.SelectedValue.ToString();
                        comman.Parameters["@CodCiudad"].Value = Cbo_Municipio.SelectedValue.ToString();

                        comman.Parameters["@dire"].Value = TxtDireccion.Text;
                        comman.Parameters["@nivel"].Value = Cbo_NiverEducativo.SelectedValue;
                        comman.Parameters["@pro"].Value = Cbo_Profesion.SelectedValue;
                        comman.Parameters["@tipos"].Value = CboTipoSangre.SelectedValue;
                        comman.Parameters["@estado"].Value = CboEstadoCivil.SelectedValue;
                        comman.Parameters["@tel"].Value = TxtTelefono.Text;
                        comman.Parameters["@dominancia"].Value = CboDominancia.SelectedValue;
                        comman.Parameters["@EPS"].Value = CboEPS.SelectedValue;
                        comman.Parameters["@ARL"].Value = CboARL.SelectedValue;


                        if (PctHuella.Image != null)
                        {
                            System.IO.MemoryStream ms1 = new System.IO.MemoryStream();
                            PctHuella.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
                            comman.Parameters.AddWithValue("@foto2", ms1.GetBuffer());
                        }
                        else
                        {
                            SqlParameter imageParameter = new SqlParameter("@foto2", SqlDbType.Image);
                            imageParameter.Value = DBNull.Value;
                            comman.Parameters.Add(imageParameter);
                        }
                        //comman.Parameters["@foto2"].Value = DBNull.Value;

                        if (PctFoto.Image != null)
                        {
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            PctFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            comman.Parameters.AddWithValue("@foto1", ms.GetBuffer());
                        }
                        else
                        {
                            SqlParameter imageParameter = new SqlParameter("@foto1", SqlDbType.Image);
                            imageParameter.Value = DBNull.Value;
                            comman.Parameters.Add(imageParameter);
                        }

                        if (Pt_Firma.Image != null)
                        {
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            Pt_Firma.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            comman.Parameters.AddWithValue("@firma", ms.GetBuffer());
                        }
                        else
                        {
                            SqlParameter imageParameter = new SqlParameter("@firma", SqlDbType.Image);
                            imageParameter.Value = DBNull.Value;
                            comman.Parameters.Add(imageParameter);
                        }

                        comman.ExecuteNonQuery();
                        SQLtrans.Commit();

                        documentoP = TxtDocumento.Text;
                        Limpiar();
                        BuscarPaciente("");
                        selecctionarAgregado(documentoP);
                        DgvDatos.ClearSelection();
                        Limpiar();
                        TxtDocumento.Clear();
                        TxtDocumento.Enabled = true;
                        MessageBox.Show("La Operación se ha completado!", "Finalizado", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("La operación no puedo completarse debido a: \n 1 - No dispone de una conexión  \n 2 - Ya se ha registrado este documento  \n 3 - La información ingresada no corresponde a la requerida  \n Vuelva a intentarlo!!! " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            TxtDocumento.Enabled = true;
            TxtDocumento.Clear();
            Limpiar();
            TxtDocumento.Focus();
            DgvDatos.ClearSelection();
            Btn_LupaBuscar.Enabled = true;
            Guardar_o_Modificar = true;
        }
        private void LblFoto_Click(object sender, EventArgs e)
        {
            // Se crea el OpenFileDialog
            OpenFileDialog dialog = new OpenFileDialog();
            // Se muestra al usuario esperando una acción
            DialogResult result = dialog.ShowDialog();

            // Si seleccionó un archivo (asumiendo que es una imagen lo que seleccionó)
            // la mostramos en el PictureBox de la inferfaz
            if (result == DialogResult.OK)
            {
                this.PctFoto.Image = Image.FromFile(dialog.FileName);
                //MessageBox.Show(System.Drawing.Imaging.ImageFormat.Jpeg.ToString());
                //this.PctFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
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
                this.PctHuella.Image = Image.FromFile(dialog.FileName);
            }
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
                //MessageBox.Show(System.Drawing.Imaging.ImageFormat.Jpeg.ToString());
                //this.PctFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
        private void Lbl_Guardar_MouseLeave(object sender, EventArgs e)
        {
            Lbl_Guardar.BackColor = Color.Gray;
        }

        private void Lbl_Guardar_MouseHover(object sender, EventArgs e)
        {
            Lbl_Guardar.BackColor = Color.SteelBlue;
        }
        private void BtnActualizar_MouseLeave(object sender, EventArgs e)
        {
            BtnActualizar.BackColor = Color.Gray;
        }

        private void BtnActualizar_MouseHover(object sender, EventArgs e)
        {
            BtnActualizar.BackColor = Color.SteelBlue;
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            TxtDocumento.Clear();
            Limpiar();
            TxtDocumento.Focus();
            DgvDatos.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pt_Firma.Image = null;
        }

        private void BtnLimpHuella_Click(object sender, EventArgs e)
        {
            PctHuella.Image = null;
        }

        private void BtnLimpFoto_Click(object sender, EventArgs e)
        {
            PctFoto.Image = null;
        }

        Boolean Guardar_o_Modificar = true; //si es true es para guardar
        private void DgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                Limpiar();
                if (tablaPaciente.Rows.Count > 0)
                {                 // DgvDatosColNumero
                    Guardar_o_Modificar = false;
                    TxtDocumento.Enabled = true;
                    Btn_LupaBuscar.Enabled = false;
                    CboTipoDocumento.SelectedValue = tablaPaciente.Rows[e.RowIndex]["Pac_TipoIdentificacion"];
                    TxtDocumento.Text = tablaPaciente.Rows[e.RowIndex]["Pac_Identificacion"].ToString();
                    numeroDocumentoPaciete = tablaPaciente.Rows[e.RowIndex]["Pac_Identificacion"].ToString();
                    TxtNombre1.Text = tablaPaciente.Rows[e.RowIndex]["Pac_Nombre1"].ToString();
                    TxtNombre2.Text = tablaPaciente.Rows[e.RowIndex]["Pac_Nombre2"].ToString();
                    TxtApellido1.Text = tablaPaciente.Rows[e.RowIndex]["Pac_Apellido1"].ToString();
                    TxtApellido2.Text = tablaPaciente.Rows[e.RowIndex]["Pac_Apellido2"].ToString();
                    DtFechaNacimiento.Text = tablaPaciente.Rows[e.RowIndex]["Pac_FechaNacimiento"].ToString();
                    TxtEdad.Text = CalcularEdad(Convert.ToDateTime(DtFechaNacimiento.Text)).ToString();
                    CboGenero.SelectedValue = tablaPaciente.Rows[e.RowIndex]["Pac_CodGenero"];
                    //TxtLugarNacimiento.Text = tablaPaciente.Rows[e.RowIndex]["Pac_LugarNacimiento"].ToString();
                    TxtDireccion.Text = tablaPaciente.Rows[e.RowIndex]["Pac_Direccion"].ToString();
                    //TxtNiverEducativo.Text = tablaPaciente.Rows[e.RowIndex]["Pac_NivelEducacion"].ToString();
                    //TxtProfesion.Text = tablaPaciente.Rows[e.RowIndex]["Pac_Profesion_Codigo"].ToString();
                    CboTipoSangre.SelectedValue = tablaPaciente.Rows[e.RowIndex]["Pac_TipoSangre"];
                    CboEstadoCivil.SelectedValue = tablaPaciente.Rows[e.RowIndex]["Pac_EstadoCivil"];
                    TxtTelefono.Text = tablaPaciente.Rows[e.RowIndex]["Pac_Telefono"].ToString();
                    CboDominancia.SelectedValue = tablaPaciente.Rows[e.RowIndex]["Pac_Dominancia_Codigo"].ToString();
                    Cbo_Departamento.SelectedValue = tablaPaciente.Rows[e.RowIndex]["Pac_CodDepto"];
                    Cbo_Municipio.SelectedValue = tablaPaciente.Rows[e.RowIndex]["Pac_CodCiudad"];
                    Cbo_NiverEducativo.SelectedValue = tablaPaciente.Rows[e.RowIndex]["Pac_CodNivelEducativo"];
                    Cbo_Profesion.SelectedValue = tablaPaciente.Rows[e.RowIndex]["Pac_CodProfesion"];

                    CboEPS.SelectedValue = tablaPaciente.Rows[e.RowIndex]["Pac_CodEPS"];
                    CboARL.SelectedValue = tablaPaciente.Rows[e.RowIndex]["Pac_CodARL"];
                    // El campo productImage primero se almacena en un buffer  Pac_CodEPS=@EPS, Pac_CodARL=@ARL
                    if (tablaPaciente.Rows[e.RowIndex]["Pac_Foto"].ToString() != "")
                    {
                        byte[] imageBuffer = (byte[])tablaPaciente.Rows[e.RowIndex]["Pac_Foto"];
                        // Se crea un MemoryStream a partir de ese buffer
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);
                        // Se utiliza el MemoryStream para extraer la imagen
                        this.PctFoto.Image = Image.FromStream(ms);
                    }
                    if (tablaPaciente.Rows[e.RowIndex]["Pac_Huella"].ToString() != "")
                    {
                        byte[] imageBuffer1 = (byte[])tablaPaciente.Rows[e.RowIndex]["Pac_Huella"];
                        // Se crea un MemoryStream a partir de ese buffer
                        System.IO.MemoryStream ms1 = new System.IO.MemoryStream(imageBuffer1);
                        // Se utiliza el MemoryStream para extraer la imagen
                        this.PctHuella.Image = Image.FromStream(ms1);
                    }
                    if (tablaPaciente.Rows[e.RowIndex]["Pac_firma"].ToString() != "")
                    {
                        byte[] imageBuffer1 = (byte[])tablaPaciente.Rows[e.RowIndex]["Pac_firma"];
                        // Se crea un MemoryStream a partir de ese buffer
                        System.IO.MemoryStream ms1 = new System.IO.MemoryStream(imageBuffer1);
                        // Se utiliza el MemoryStream para extraer la imagen
                        this.Pt_Firma.Image = Image.FromStream(ms1);
                    }
                }
            }
            else
            {
            }
        }

        private void TxtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjServer.Solo_Numeros(e);
        }

        private void TxtNombre1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjServer.Solo_Letras(e);
        }

        private void TxtLugarNacimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjServer.Solo_Letras(e);
        }

        private void Cbo_Departamento_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                //CARGAR COMBO DE TIPOS DE NIVEL MUNICIPIO
                Cbo_Municipio.DataSource = ObjServer.LlenarTabla("SELECT [Ciud_CodDepto] AS CODDEPARTAMENTO,[Ciud_Codigo] As Codigo,[Ciud_Nombre] AS Descripcion FROM [dbo].[Ciudad] WHERE [Ciud_CodDepto] = '" + Cbo_Departamento.SelectedValue + "'");
                Cbo_Municipio.DisplayMember = "Descripcion";
                Cbo_Municipio.ValueMember = "Codigo";
            }
            catch (Exception)
            {
            }
        }

        private void TxtDocumento_TextChanged(object sender, EventArgs e)
        {
            if (IdPacienteTra != TxtDocumento.Text)
            {
                //Limpiar();
                //IdPacienteTra = "";
            }
        }

        private void LnkAbiriGestionSistema_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmGestionarSistema f = new FrmGestionarSistema();
            f.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Esta operacion actualizará todos los controles que muestra informacion desplegable ¿Desea continuar?", "Aceptar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                //CARGAR COMBO DE TIPO DOCUMENTO
                CboTipoDocumento.DataSource = ObjServer.LlenarTabla("SELECT [TipoIde_Codigo] As Codigo ,[TipoIde_Descripcion] As Descripcion FROM [dbo].[TipoDocumento]");
                CboTipoDocumento.DisplayMember = "Descripcion";
                CboTipoDocumento.ValueMember = "Codigo";

                //CARGAR COMBO DE TIPO GENERO
                CboGenero.DataSource = ObjServer.LlenarTabla(" SELECT [Gen_Codigo] As Codigo, [Gen_Descripcion] As Descripcion  FROM [dbo].[Genero] ORDER BY Descripcion");
                CboGenero.DisplayMember = "Descripcion";
                CboGenero.ValueMember = "Codigo";

                //CARGAR COMBO DE ESTADO CIVIL
                CboEstadoCivil.DataSource = ObjServer.LlenarTabla("SELECT [EstCivil_Codigo] As Codigo ,[EstCivil_Descripcion] As Descripcion  FROM [dbo].[EstadoCivil] ORDER BY Descripcion");
                CboEstadoCivil.DisplayMember = "Descripcion";
                CboEstadoCivil.ValueMember = "Codigo";

                //CARGAR COMBO DE TIPOS DE SANGRE
                CboTipoSangre.DataSource = ObjServer.LlenarTabla("SELECT [TipSan_Codigo] As Codigo ,[TipSan_Descripcion] As Descripcion  FROM [dbo].[TipoSangre] ORDER BY Descripcion");
                CboTipoSangre.DisplayMember = "Descripcion";
                CboTipoSangre.ValueMember = "Codigo";

                //CARGAR COMBO DE TIPOS DE DOMINANCIA
                CboDominancia.DataSource = ObjServer.LlenarTabla("SELECT [Dom_Codigo] As Codigo ,[Dom_Descripcion] As Descripcion FROM [dbo].[Dominancia] ORDER BY Descripcion");
                CboDominancia.DisplayMember = "Descripcion";
                CboDominancia.ValueMember = "Codigo";

                //CARGAR COMBO DE TIPOS DE NIVEL EDUCATIVO
                Cbo_NiverEducativo.DataSource = ObjServer.LlenarTabla("SELECT [NivEdu_Codigo] AS Codigo,[NivEdu_Descripcion] AS Descripcion FROM [dbo].[NivelEducativo] ORDER BY Codigo");
                Cbo_NiverEducativo.DisplayMember = "Descripcion";
                Cbo_NiverEducativo.ValueMember = "Codigo";

                //CARGAR COMBO DE TIPOS DE NIVEL DEPARTAMENTO
                Cbo_Departamento.DataSource = ObjServer.LlenarTabla("SELECT [Dept_Codigo] AS Codigo,[Dept_Nombre] AS Descripcion FROM [dbo].[Departamento] ORDER BY Descripcion");
                Cbo_Departamento.DisplayMember = "Descripcion";
                Cbo_Departamento.ValueMember = "Codigo";
                //Cbo_Departamento.s
                //CARGAR COMBO DE TIPOS DE NIVEL PRFESIÓN
                Cbo_Profesion.DataSource = ObjServer.LlenarTabla("SELECT [Prof_Codigo] AS Codigo,[Prof_Descripcion] AS Descripcion FROM [dbo].[Profesion] ORDER BY Descripcion");
                Cbo_Profesion.DisplayMember = "Descripcion";
                Cbo_Profesion.ValueMember = "Codigo";

            }

        }

        private void DtFechaNacimiento_Validating(object sender, CancelEventArgs e)
        {
        }

        private void DtFechaNacimiento_ValueChanged(object sender, EventArgs e)
        {
            try
            {
             TxtEdad.Text = CalcularEdad(DtFechaNacimiento.Value).ToString();
            }
            catch (Exception)
            {}
        }
    }
}
