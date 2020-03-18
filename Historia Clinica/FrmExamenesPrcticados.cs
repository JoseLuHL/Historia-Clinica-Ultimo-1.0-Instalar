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
using System.IO;
using System.Drawing.Imaging;
using Historia_Clinica.Conexion;
namespace Historia_Clinica
{
    public partial class FrmExamenesPrcticados : Form
    {
        public FrmExamenesPrcticados()
        {
            InitializeComponent();
        }

        #region PARA EL PACIENTE
        DataTable tablaPaciente = new DataTable();
        #endregion

        #region METODO::: CON ESTE METODO SE CARGAR LOS PACIENTES AL DGV 
        public async Task  BuscarPaciente(string Documento)
        {
            string res = "";
            DgvDatos.Rows.Clear();
            IdPacienteTra = TxtDocumento.Text;
            string Query = "SELECT TOP 20 [Pac_TipoIdentificacion] " +
                    " ,[Pac_Identificacion]          " +
                    " ,[Pac_Nombre1]                 " +
                    " ,[Pac_Nombre2]                 " +
                    " ,[Pac_Apellido1]               " +
                    " ,[Pac_Apellido2]               " +
                    " ,[Pac_FechaNacimiento]         " +
                    " ,[Pac_CodGenero]           " +
                    " ,[Pac_CodCiudad]         " +
                    " ,[Pac_Direccion]               " +
                    " ,[Pac_CodNivelEducativo]          " +
                    " ,[Pac_CodProfesion]        " +
                    " ,[Pac_TipoSangre]              " +
                    " ,[Pac_EstadoCivil]             " +
                    ",Pac_Telefono,Pac_Foto,Pac_Huella, Pac_firma,Pac_Dominancia_Codigo, Pac_Fecha                 " +
                    " FROM [dbo].[Paciente] ORDER BY Pac_Fecha desc";
            //WHERE Pac_Identificacion=" + Documento;
            await Task.Run(() =>
            {
                tablaPaciente = ObjServer.LlenarTabla(Query);
            });
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
                }
            }
        #endregion

        #region METODO::: SE UTILIZA PARA CAMBIAR EN ESTILO DEL DGV
        public void EstilosDgv2(DataGridView DGV)
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
            DGV.EnableHeadersVisualStyles = false;
        }
        #endregion

        #region METODO::: SE UTILIZA PARA CAMBIAR EL TAMAÑO DEL DGV
        public void tamañoDGV()
        {   
            DgvExamenPacticadoColAjuntar2.Width = 180;
            DgvExamenPacticadoColExamen.Width = 250;
            DgvExamenPacticadoColFecha.Width = 200;
            DgvExamenPacticadoColResultado.Width = 250;
        }
        #endregion

        #region METODO::: SE UTILIZA PARA CAMBIAR EN ESTILO DEL DGV
        public void EstilosDgv(DataGridView DGV)
        {
            DGV.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            DGV.DefaultCellStyle.Font = new Font("Verdana", 10);
            Font prFont = new Font("Verdana", 11, FontStyle.Bold);
                for (int i = 0; i < DGV.Columns.Count; i++)
                {
                //DGV.Columns[i].HeaderCell.Style.Font = prFont;
                DGV.Columns[i].HeaderCell.Style.ForeColor = Color.Black;
                DGV.Columns[i].HeaderCell.Style.BackColor = Color.Transparent;
                DGV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            DGV.AutoResizeColumns();
            DGV.EnableHeadersVisualStyles = false;
            
        }
        #endregion

        #region OBJETOS::: SE CREA EL OBJETO DE LA CLASE Y UNA TABLA QUE CONTIENE LOS EXAMENES PRACTICADOS DEL PACIENTE
        ClsSqlServer ObjServer = new ClsSqlServer();
        DataTable tabla = new DataTable();
        #endregion

        #region METODO::: EVENTO LOAD DEL FORMULARIO
        private async void FrmExamenesPrcticados_Load(object sender, EventArgs e)
        {
            EstilosDgv(DgvExamenPacticado);
            tamañoDGV();
            //Establecer conexión  con el Gestor.
            ObjServer.CadenaCnn = Conexion.CadenaConexion.cadena();
            ObjServer.Conectar();
            //----Fin Establecer conexión-----

            //Cargar combo de tipo examen
            CboTipoExamen.DataSource = ObjServer.LlenarTabla("SELECT [TipoExam_Codigo] As Codigo ,[TipoExam_Descripcion] As Descripcion FROM [dbo].[TipoExamen]");
            CboTipoExamen.DisplayMember = "Descripcion";
            CboTipoExamen.ValueMember = "Codigo";
            //
            //Cargar combo de tipo examen
            string Query = "SELECT [Enfa_Codigo] as Codigo ,[Enfa_Descripcion] as Descripcion FROM [dbo].[Enfasis]";//Where TipoExam_Codigo =" + tipoEaxamen;
            CboEnfasis.DataSource = ObjServer.LlenarTabla(Query);
            CboEnfasis.DisplayMember = "Descripcion";
            CboEnfasis.ValueMember = "Codigo";


            // Cargo los datos que tendra el combobox Concepto
            DataTable tableConcepto = new DataTable();
            CboConcepto.DataSource = ObjServer.LlenarTabla("SELECT [Conc_Codigo] As Codigo,[Conc_Descripcion] As Descripcion FROM [dbo].[Concepto]");
            CboConcepto.DisplayMember = "Descripcion";
            CboConcepto.ValueMember = "Codigo";

            // Cargo los datos que tendra el combobox DgvExamenPacticadoColResultado
            DgvExamenPacticadoColResultado.DataSource = ObjServer.LlenarTabla("SELECT [TipRes_Codigo] As Codigo  ,[TipRes_Descripcion] As Descripcion  FROM [dbo].[TipResultado]");
            DgvExamenPacticadoColResultado.DisplayMember = "Descripcion";
            DgvExamenPacticadoColResultado.ValueMember = "Codigo";

            //Cargo los datos que tendra el combobox de las empresas
            CboEmpresa.DataSource = ObjServer.LlenarTabla("SELECT [Empr_Codigo] As Codigo,[Empre_Nit] As Nit,[Empre_RazonSocial] As Descripcion  FROM [dbo].[Empresa] ORDER BY Descripcion");
            CboEmpresa.DisplayMember = "Descripcion";
            CboEmpresa.ValueMember = "Codigo";

            //Cargar combo de Medicos
            CboProfecional.DataSource = ObjServer.LlenarTabla("SELECT [Medic_TipoIdentificacion]  ,[Medic_Identificacion] , concat([Medic_Nombre1],+' '+[Medic_Nombre2],+' '+[Medic_Apellido1],+' '+[Medic_Apellido2]) as Nombre  FROM [dbo].[Medico]");
            CboProfecional.DisplayMember = "Nombre";
            CboProfecional.ValueMember = "Medic_Identificacion";

            //Cargar combo de Lugar Examen
            CboLugarExamen.DataSource = ObjServer.LlenarTabla("SELECT [Lugar_Codigo] as Codigo  ,[Lugar_Nombre] as Descripcion  FROM [dbo].[LugarRealizacionExamen]");
            CboLugarExamen.DisplayMember = "Descripcion";
            CboLugarExamen.ValueMember = "Codigo";

            #region CARGAR COMBO DE OCUPACIONES 
            CboCargoOcupacion.DataSource = ObjServer.LlenarTabla("SELECT [Carg_Codigo]  as Codigo ,[Carg_Descripcion] AS Descripcion FROM [dbo].[Cargo]");
            CboCargoOcupacion.DisplayMember = "Descripcion";
            CboCargoOcupacion.ValueMember = "Codigo";
            #endregion
            //

            //DgvExamenPacticadoColResultado
            DgvExamenPacticado.Rows.Clear();
            string query = "";
            query = "SELECT [Exam_Codigo] As [Codigo] ,[Exam_Descripcion] As [Descripcion] FROM [dbo].[Examen]";
            tabla = ObjServer.LlenarTabla(query);
            DgvExamenPacticadoColExamen.DataSource = tabla;
            DgvExamenPacticadoColExamen.DisplayMember = "Descripcion";
            DgvExamenPacticadoColExamen.ValueMember = "Codigo";

            string ColCod = "";
            string ColDes = "";
            ColCod = "DgvExamenPacticadoColCodigo";
            ColDes = "DgvExamenPacticadoColExamen";

            DgvExamenPacticado.RowCount = 1;
            for (int i = 0; i < 1; i++)
            {
                DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value = tabla.Rows[i]["Codigo"];
                DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value = 1;
                DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar2"].Value = "......";
                DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value = "01/01/1990";
            }
            //<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>

            CargarIconosDGV();
            CargarFecha();
            DgvExamenPacticado.Columns["DgvExamenPacticadoColAjuntar2"].ReadOnly = true;
            DgvExamenPacticado.Rows.Clear();
            CargarIconosDGV();
            EstilosDgv2(DgvDatos);
            await BuscarPaciente("");
        }
        #endregion

        #region METODO::: SE UTILIZA PARA CARGAR LOS EXAMENES DEL PACIENTE
        public void CargarDgvExamenes()
        {
            DgvExamenPacticado.RowCount = 1;
            for (int i = 0; i < 1; i++)
            {
                DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value = tabla.Rows[i]["Codigo"];
                DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value = 1;
                DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar2"].Value = "......";
                DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value = "01/01/1990";
            }
            DgvExamenPacticado.Rows.Clear();
            CargarIconosDGV();
            //<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>
            //MessageBox.Show(tabla.Rows.Count.ToString());
        }
        #endregion

        #region METODO::: SE UTILIZA PARA CARGAR LA INFORMACION OCUPACIONAL QUE SE LE REGISTRO
        public void CARGAR_INFORMACION_OCUPACIONAL()
        {
            try
            {
                DataTable TablaInfoOcupacional = new DataTable();
                
                TablaInfoOcupacional = ObjServer.LlenarTabla("SELECT [InfOcu_Numero]  " +
                                                              ",[InfOcu_paciente]     " +
                                                              ",[InfOcu_CodEmpresa]      " +
                                                              ",[InfOcu_FechaIngreso] " +
                                                              ",[InfOcu_Jornada]      " +
                                                              ",[InfOcu_CodOcupacion]    " +
                                                              ",[InfOcu_Area]         " +
                                                              "FROM [dbo].[InformacionOcupacionalProvi] WHERE InfOcu_paciente='"+TxtDocumento.Text+"'");
                if (TablaInfoOcupacional.Rows.Count>0)
                {
                   CboCargoOcupacion.SelectedValue = TablaInfoOcupacional.Rows[0]["InfOcu_CodOcupacion"].ToString();
                    TxtJornada.Text = TablaInfoOcupacional.Rows[0]["InfOcu_Jornada"].ToString();
                    CboEmpresa.SelectedValue = TablaInfoOcupacional.Rows[0]["InfOcu_CodEmpresa"].ToString();
                    TxtSesion.Text = TablaInfoOcupacional.Rows[0]["InfOcu_Area"].ToString();
                    DtFechaIngreso.Text = TablaInfoOcupacional.Rows[0]["InfOcu_FechaIngreso"].ToString();
                }
                else
                {
                    LIMPIAR_CONTROLES();
                    //DtFechaIngreso.Clear();
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        public async Task<string> CargarLoadAsync()
        {
            await Task.Run(() => { CargarLoad(); });
            return string.Empty;
        }

        public void CargarLoad()
        {
            
        }

        public void LIMPIAR_CONTROLES()
            {
                 TxtCargo.Clear();
                 TxtJornada.Clear();
                 CboEmpresa.Text = "";
                 TxtSesion.Clear();
                 TxtNombre.Clear();
                 DgvExamenPacticado.Rows.Clear();
            }
        public void BuscarPaciente_P(string IdPaciente)
        {
            //try
            //{
            DataTable TablaPaciente = new DataTable();
            //<<<<<<<<<<<<para preciones la tecla Enter en el control de Buscar>>>>>>>>>>>>>>
            if (IdPaciente != string.Empty)
            {
                //<<<<<<<<<<<<<<<<<<<<<<
                string Query = "SELECT	dbo.Paciente.Pac_Nombre1+ ' ' + isnull(dbo.Paciente.Pac_Nombre2,'')                         " +
                                "		+' '+dbo.Paciente.Pac_Apellido1+ ' '+isnull(dbo.Paciente.Pac_Apellido2,'') As Nombres,      " +
                                "		dbo.Paciente.Pac_TipoIdentificacion, dbo.Paciente.Pac_Identificacion,                       " +
                                "		dbo.Paciente.Pac_FechaNacimiento, dbo.Genero.Gen_Descripcion,                               " +
                                "		dbo.Paciente.Pac_Direccion,                         " +
                                "		dbo.Paciente.Pac_CodNivelEducativo, dbo.Paciente.Pac_CodProfesion,                         " +
                                "		dbo.TipoSangre.TipSan_Descripcion, dbo.Paciente.Pac_EstadoCivil,                            " +
                                "		dbo.EstadoCivil.EstCivil_Descripcion,Pac_Telefono                                           " +
                                "FROM	dbo.Paciente LEFT OUTER JOIN                                                                " +
                                "		dbo.EstadoCivil ON dbo.Paciente.Pac_EstadoCivil =                                           " +
                                "		dbo.EstadoCivil.EstCivil_Codigo LEFT OUTER JOIN                                             " +
                                "		dbo.TipoSangre ON dbo.Paciente.Pac_TipoSangre =                                             " +
                                "		dbo.TipoSangre.TipSan_Codigo LEFT OUTER JOIN                                                " +
                                "		dbo.Genero ON dbo.Paciente.Pac_CodGenero = dbo.Genero.Gen_Codigo                        " +
                                "WHERE	Pac_Identificacion='" + IdPaciente + "'";
                TablaPaciente = ObjServer.LlenarTabla(Query);
                //LblEntrada.Text = "";
                if (TablaPaciente.Rows.Count > 0)
                {
                    TxtNombre.Text = TablaPaciente.Rows[0]["Nombres"].ToString();
                    CargarExamenes();
                    CargarIconosDGV();
                }
                else
                {
                    //LblEntrada.Text = "";
                    TxtNombre.SelectAll();
                    CargarDgvExamenes();
                    DgvExamenPacticado.Rows.Clear();

                    MessageBox.Show("Verificar el documento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //>>>>>>>>>>>>>>>>>>>>>>
            }
            else
            {
                //LblEntrada.Text = "";
                TxtNombre.Clear();
                CargarDgvExamenes();
                DgvExamenPacticado.Rows.Clear();
                MessageBox.Show("Ingrese un número de documento", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //<<<<<<<<<<<<Fin precionar tecla enter>>>>>>>>>>>>>>>>>>>>>>>
            //}
            //catch (Exception)
            //{
            //}
        }
        private void TxtDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                BuscarPaciente_P(TxtDocumento.Text);
                CARGAR_INFORMACION_OCUPACIONAL();

            }
            
        }
        
        public void CargarExamenes()
        {
            if (TxtDocumento.Text != "")
            {
                try
                {
                    CARGAR_INFORMACION_OCUPACIONAL();
                    //CARGAR LOS DATOS EN EXAMENES  PRACTICADOS:::
                    DgvExamenPacticado.Rows.Clear();
                    DataTable Datos = new DataTable();
                    Datos = ObjServer.LlenarTabla("SELECT [ExaPac_Paciente]  ,[ExaPrac_Examen_Codigo] ,[ExaPrac_Resultado], [ExaPrac_Ajuntar] ,[ExaPrac_FechaExamen]  FROM [dbo].[ExamenPracticadoProvi] where [ExaPac_Paciente] = " + TxtDocumento.Text);
                    if (Datos.Rows.Count > 0)
                    {
                        DgvExamenPacticado.RowCount = Datos.Rows.Count + 1;
                        for (int i = 0; i < Datos.Rows.Count; i++)
                        {
                            DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value = Datos.Rows[i]["ExaPrac_Examen_Codigo"];
                            DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value = Datos.Rows[i]["ExaPrac_Ajuntar"];
                            DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar2"].Value = "Imagen #"+(i+1).ToString();
                            DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value = Datos.Rows[i]["ExaPrac_FechaExamen"];
                            //DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value = Datos.Rows[i]["ExaPrac_Resultado"];
                        }
                    }
                    else
                    {
                        //MessageBox.Show("No se han cargados los datos", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        CargarDgvExamenes();
                        //DgvExamenPacticado.Rows.Clear();
                    }
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                //CARGAR DATOS DE ENTRADAHISTORIA
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT [Entr_IdPaciente] ,[Entr_FechaEntrada] ,[Entr_Concepto_Codigo] ,[Entr_TipoExamenCodigo],  Ent_LugarExamen,Ent_Medico " +
                                                ",[Ent_Enfasis]  FROM [dbo].[EntradaProvisional] WHERE [Entr_IdPaciente]=" + TxtDocumento.Text);
                if (Datos.Rows.Count > 0)
                {
                    CboConcepto.SelectedValue = Datos.Rows[0]["Entr_Concepto_Codigo"];
                    CboTipoExamen.SelectedValue = Datos.Rows[0]["Entr_TipoExamenCodigo"];
                    CboEnfasis.SelectedValue = Datos.Rows[0]["Ent_Enfasis"];
                    CboProfecional.SelectedValue = Datos.Rows[0]["Ent_Medico"];
                    CboLugarExamen.SelectedValue = Datos.Rows[0]["Ent_LugarExamen"];                    
                    MessageBox.Show("Hay una cita pendiete para este paciente", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //DgvExamenPacticado.Rows.Clear();
                    CargarDgvExamenes();
                    //MessageBox.Show("No se hay datos para mostrar", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                }
                catch (Exception ex )
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Ingrese un numero de documento", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void TxtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjServer.Solo_Numeros(e);
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
          try
           {
                string RutaArchivoDestino = @"C:\Users\JOSEPH\Pictures\Saved Pictures\NombreArch.jpg";
                File.Delete(RutaArchivoDestino);
                string Imagen = Application.StartupPath + @"\imagenes\doctor.png";
                string ruta = Path.Combine(@"C:\Users\JOSEPH\Pictures\Saved Pictures", "NombreArch.jpg");
                PctFoto.Image.Save(ruta, ImageFormat.Jpeg);

                //string ruta_imagen = @"C:\User\foto.jpg";
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                //PctFoto.Image = Image.FromFile(Imagen);

                startInfo.Arguments = @"/c rundll32 ""C:\Program Files\Windows Photo Viewer\PhotoViewer.dll"", ImageView_Fullscreen " + RutaArchivoDestino;
                 process.StartInfo = startInfo;
                process.Start();

                
          }
                catch (Exception)
                {
                MessageBox.Show("Ha ocurrido un error. Verifique.", "ERROR",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void BtnGuardarHistoria_Click(object sender, EventArgs e)
        {
            if (TxtDocumento.Text != "")
            {
                if (MessageBox.Show("¿Esta seguro de guardar la Informacón? ", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    ObjServer.CadenaCnn = Conexion.CadenaConexion.cadena();
                    ObjServer.Conectar();
                    //:::::::::Comienzo de la transacción:::::::::::::
                    //Establecemos el Objeto que nos va a permitir conectarnos a la base de Datos()
                    SqlConnection cnn = new SqlConnection(Conexion.CadenaConexion.cadena());
                    //Abrimos la conexión()
                    cnn.Open();
                    //Comenzamos la transacción ()
                    SqlTransaction SQLtrans = cnn.BeginTransaction();
                    try
                    {
                        SqlCommand comman = cnn.CreateCommand();
                        comman.Transaction = SQLtrans;

                        //ELIMINAR LOS EXAMENES PRACTICADOS
                        string QueryI = "DELETE FROM [dbo].[ExamenPracticadoProvi]  WHERE ExaPac_Paciente='" + TxtDocumento.Text + "'";
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        //ELIMINAR 
                        QueryI = "DELETE FROM [dbo].[EntradaProvisional] WHERE Entr_IdPaciente='" + TxtDocumento.Text + "'";
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        string[] fecha = DateTime.Now.ToString().Split(' ');
                        //PRIME SE INSERTA LA ENTRADA - INSERTAR EN ENTRADA - HISTORIA

                        string Query = "INSERT INTO [dbo].[EntradaProvisional]([Entr_IdPaciente],[Entr_FechaEntrada], [Entr_Concepto_Codigo],Entr_TipoExamenCodigo,[Ent_Enfasis],Ent_Medico,Ent_LugarExamen) " +
                                        "VALUES (" + "'" + TxtDocumento.Text + "', '" + DateTime.Now.ToString() + "'," + CboConcepto.SelectedValue + "," + CboTipoExamen.SelectedValue + "," + CboEnfasis.SelectedValue + ",'"+CboProfecional.SelectedValue.ToString()+"',"+CboLugarExamen.SelectedValue+")";
                        //OBTENEMOS EL NUMERO DE LA HISTORIA INSERTADA
                        comman.CommandText = Query;
                        comman.ExecuteNonQuery();

                        Query = "DELETE FROM [dbo].[InformacionOcupacionalProvi] WHERE InfOcu_paciente='" + TxtDocumento.Text + "'";
                        comman.CommandText = Query;
                        comman.ExecuteNonQuery();

                        Query = "INSERT INTO [dbo].[InformacionOcupacionalProvi] " +
                                    "   ([InfOcu_CodEmpresa]                      " +
                                    "   ,[InfOcu_FechaIngreso]                 " +
                                    "   ,[InfOcu_Jornada]                      " +
                                    "   ,[InfOcu_CodOcupacion]                    " +
                                    "   ,[InfOcu_Area],InfOcu_paciente)                        " +
                                    "VALUES " +
                                    "   (" + "" + CboEmpresa.SelectedValue + ",'" + DtFechaIngreso.Text + "','" + TxtJornada.Text + "'," + CboCargoOcupacion.SelectedValue + ",'" + TxtSesion.Text + "','" + TxtDocumento.Text + "')";

                        comman.CommandText = Query;
                        comman.ExecuteNonQuery();
                        //Insertar EXAMEN PRACTICADOS
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        for (int i = 0; i < DgvExamenPacticado.Rows.Count - 1; i++)
                        {
                            string examen = DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value.ToString();
                            
                            //if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value != null)
                            //{
                                Boolean confirmar = false; //Para saber si hay datos para agregar
                                int CodExamen = 0;
                                string FechaExa = "";
                                int resultado = 0;

                                if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value != null)
                                    resultado = Convert.ToInt32(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value);
                                else
                                    resultado = 1;

                                if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value != null)
                                    FechaExa = DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value.ToString();
                                else
                                    FechaExa = "";

                                CodExamen = Convert.ToInt32(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value);
                                confirmar = true;

                                Query = "INSERT INTO [dbo].[ExamenPracticadoProvi] VALUES (@documento" + i + ",@examen" + i + ",@resultado" + i + ",@foto" + i + ",@fecha" + i + ")";

                                comman.CommandText = Query;
                                comman.Parameters.Add("@documento" + i, SqlDbType.NVarChar);
                                comman.Parameters.Add("@examen" + i, SqlDbType.Int);
                                comman.Parameters.Add("@resultado" + i, SqlDbType.Int);
                                //comman.Parameters.Add("@foto" + i, SqlDbType.Image);
                                comman.Parameters.Add("@fecha" + i, SqlDbType.Date);

                                comman.Parameters["@documento" + i].Value = TxtDocumento.Text;
                                comman.Parameters["@examen" + i].Value = CodExamen;
                                comman.Parameters["@resultado" + i].Value = resultado;
                                comman.Parameters["@fecha" + i].Value = FechaExa;

                                
                                    SqlParameter imageParameter = new SqlParameter("@foto"+i, SqlDbType.Image);
                                    imageParameter.Value = DBNull.Value;
                                    comman.Parameters.Add(imageParameter);
                                //}

                                comman.ExecuteNonQuery();
                            //}
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                        SQLtrans.Commit();
                        TxtDocumento.Clear();
                        TxtNombre.Clear();
                        DgvExamenPacticado.Rows.Clear();
                        CargarIconosDGV();
                        LIMPIAR_CONTROLES();
                        MessageBox.Show("Datos guardados correctamente", "finalizar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception ex)
                    {
                        //ObjServer.CadnaSentencia = "DELETE FROM [dbo].[EntradaHistoria] WHERE Entr_Numero=" + NumeroEntrada;
                        //ObjServer.Sentencia();
                        MessageBox.Show("La operación no puedo completarse debido a: \n 1 - No dispone de una conexión  \n 2 - El registro esta duplicado  \n 3 - La información ingrasado no corresponde a la requerida  \n 4 - No se ha cargado imagen  \n Vuelva a intentarlo!!! " + ex.ToString() , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageBox.Show(ex.ToString());
                        SQLtrans.Rollback();

                    }
                }
            }
            else
            {
                MessageBox.Show("Ingrese un numero de documento", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ////MessageBox.Show(DateTime.Now.ToString());

            //if (TxtDocumento.Text != "")
            //{

            //    if (MessageBox.Show("¿Esta seguro de guardar la Informacón? ", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //    {
            //        ObjServer.CadenaCnn = Conexion.CadenaConexion.cadena();
            //        ObjServer.Conectar();

            //        //:::::::::Comienzo de la transacción:::::::::::::
            //        //Establecemos el Objeto que nos va a permitir conectarnos a la base de Datos()
            //        SqlConnection cnn = new SqlConnection(Conexion.CadenaConexion.cadena());
            //        //Abrimos la conexión()
            //        cnn.Open();
            //        //Comenzamos la transacción ()
            //        SqlTransaction SQLtrans = cnn.BeginTransaction();
            //        try
            //        {
            //            SqlCommand comman = cnn.CreateCommand();
            //            comman.Transaction = SQLtrans;

            //            //ELIMINAR LOS EXAMENES PRACTICADOS
            //            string QueryI = "DELETE FROM [dbo].[ExamenPracticadoProvi]  WHERE ExaPac_Paciente='" + TxtDocumento.Text + "'";
            //            comman.CommandText = QueryI;
            //            comman.ExecuteNonQuery();

            //            //ELIMINAR 
            //            QueryI = "DELETE FROM [dbo].[EntradaProvisional] WHERE Entr_IdPaciente='" + TxtDocumento.Text + "'";
            //            comman.CommandText = QueryI;
            //            comman.ExecuteNonQuery();

            //            string[] fecha = DateTime.Now.ToString().Split(' ');
            //            //PRIME SE INSERTA LA ENTRADA - INSERTAR EN ENTRADA - HISTORIA

            //            string Query = "INSERT INTO [dbo].[EntradaProvisional]([Entr_IdPaciente],[Entr_FechaEntrada], [Entr_Concepto_Codigo],Entr_TipoExamenCodigo,[Ent_Enfasis]) " +
            //                            "VALUES (" + "'" + TxtDocumento.Text + "', '" + DateTime.Now.ToString() + "'," + CboConcepto.SelectedValue + "," + CboTipoExamen.SelectedValue + "," + CboEnfasis.SelectedValue + ")";
            //            //OBTENEMOS EL NUMERO DE LA HISTORIA INSERTADA
            //            comman.CommandText = Query;
            //            comman.ExecuteNonQuery();

            //            Query = "DELETE FROM [dbo].[InformacionOcupacionalProvi] WHERE InfOcu_paciente='" + TxtDocumento.Text + "'";
            //            comman.CommandText = Query;
            //            comman.ExecuteNonQuery();

            //            Query = "INSERT INTO [dbo].[InformacionOcupacionalProvi] " +
            //                        "   ([InfOcu_CodEmpresa]                      " +
            //                        "   ,[InfOcu_FechaIngreso]                 " +
            //                        "   ,[InfOcu_Jornada]                      " +
            //                        "   ,[InfOcu_CodOcupacion]                    " +
            //                        "   ,[InfOcu_Area],InfOcu_paciente)                        " +
            //                        "VALUES " +
            //                        "   (" + "" + CboEmpresa.SelectedValue + ",'" + DtFechaIngreso.Text + "','"  + TxtJornada.Text + "'," + CboCargoOcupacion.SelectedValue + ",'" + TxtSesion.Text + "','"+TxtDocumento.Text+"')";
                        
            //            comman.CommandText = Query;
            //            comman.ExecuteNonQuery();
            //            //Insertar EXAMEN PRACTICADOS
            //            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //            for (int i = 0; i < DgvExamenPacticado.Rows.Count - 1; i++)
            //            {
            //                    if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value.ToString().Length > 7 && DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value != null)
            //                    {
            //                        Boolean confirmar = false; //Para saber si hay datos para agregar
            //                        int CodExamen = 0;
            //                        string FechaExa = "";
            //                        int resultado = 0;

            //                        if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value != null)
            //                            resultado = Convert.ToInt32(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value);
            //                        else
            //                            resultado = 1;

            //                        FechaExa = DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value.ToString();
            //                        CodExamen = Convert.ToInt32(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value);
            //                        confirmar = true;
            //                        Query = "INSERT INTO [dbo].[ExamenPracticado] VALUES (@documento" + i + ",@examen" + i + ",@resultado" + i + ",@foto" + i + ",@fecha" + i + ")";

            //                        comman.CommandText = Query;
            //                        comman.Parameters.Add("@documento" + i, SqlDbType.NVarChar);
            //                        comman.Parameters.Add("@examen" + i, SqlDbType.Int);
            //                        comman.Parameters.Add("@resultado" + i, SqlDbType.Int);
            //                        //comman.Parameters.Add("@foto" + i, SqlDbType.Image);
            //                        comman.Parameters.Add("@fecha" + i, SqlDbType.Date);

            //                        comman.Parameters["@documento" + i].Value = TxtDocumento.Text;
            //                        comman.Parameters["@examen" + i].Value = CodExamen;
            //                        comman.Parameters["@resultado" + i].Value = resultado;
            //                        comman.Parameters["@fecha" + i].Value = FechaExa;
                                    
            //                        if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value!= null)
            //                        {
            //                            comman.Parameters.Add("@foto" + i, SqlDbType.Image);
            //                            string Imagen = DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value.ToString();
            //                            MessageBox.Show(Imagen);
            //                            PctFoto.Image = null;
            //                            ms = new MemoryStream();
            //                            if (Imagen == "System.Byte[]")
            //                            {
            //                                byte[] imageBuffer = (byte[])DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value;
            //                                // Se crea un MemoryStream a partir de ese buffer
            //                                System.IO.MemoryStream ms1 = new System.IO.MemoryStream(imageBuffer);
            //                                // Se utiliza el MemoryStream para extraer la imagen
            //                                this.PctFoto.Image = Image.FromStream(ms1);
            //                                PctFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //                            }
            //                            else
            //                            {
            //                                this.PctFoto.Image = Image.FromFile(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value.ToString());
            //                                PctFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //                            }
            //                            comman.Parameters["@foto" + i].Value = ms.GetBuffer();

            //                        }
            //                        else
            //                        {
            //                            SqlParameter imageParameter = new SqlParameter("@foto", SqlDbType.Image);
            //                            imageParameter.Value = DBNull.Value;
            //                            comman.Parameters.Add(imageParameter);
            //                        }

            //                        comman.ExecuteNonQuery();

            //                    }
            //                }                            
            //            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            //            SQLtrans.Commit();
            //            TxtDocumento.Clear();
            //            TxtNombre.Clear();
            //            DgvExamenPacticado.Rows.Clear();
            //            CargarIconosDGV();
            //            LIMPIAR_CONTROLES();
            //            MessageBox.Show("Datos guardados correctamente", "finalizar", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        }
            //        catch (Exception ex)
            //        {

            //            MessageBox.Show("La operación no puedo completarse debido a: \n 1 - No dispone de una conexión  \n 2 - El registro esta duplicado  \n 3 - La información ingrasado no corresponde a la requerida  \n 4 - No se ha cargado imagen  \n Vuelva a intentarlo!!!" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            //MessageBox.Show(ex.ToString());
            //            SQLtrans.Rollback();

            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Ingrese un numero de documento", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}


        }

        private void DgvExamenPacticado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>-1)
            {
                if (e.ColumnIndex == 4)
                {

                    // Se crea el OpenFileDialog
                    OpenFileDialog dialog = new OpenFileDialog();
                    // Se muestra al usuario esperando una acción
                    DialogResult result = dialog.ShowDialog();

                    // Si seleccionó un archivo (asumiendo que es una imagen lo que seleccionó)
                    // la mostramos en el PictureBox de la inferfaz
                    if (result == DialogResult.OK)
                    {
                        DgvExamenPacticado.CurrentRow.Cells[3].Value = (dialog.FileName);
                        DgvExamenPacticado.CurrentCell.Value="Imagen #" + ( e.RowIndex + 1).ToString() ;
                    }
                }
                if (e.ColumnIndex == 6)
                {
                    try
                    {
                        //string Imagen = DgvExamenPacticado.CurrentCell.RowIndex].Cells["DgvExamenPacticadoColAjuntar2"].Value;
                        string Imagen = "";
                        Imagen = DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentCell.RowIndex].Cells["DgvExamenPacticadoColAjuntar"].Value.ToString();
                        if (Imagen == "System.Byte[]")
                        {
                            byte[] imageBuffer = (byte[])DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentCell.RowIndex].Cells["DgvExamenPacticadoColAjuntar"].Value;
                            // Se crea un MemoryStream a partir de ese buffer
                            System.IO.MemoryStream ms1 = new System.IO.MemoryStream(imageBuffer);
                            // Se utiliza el MemoryStream para extraer la imagen
                            this.PctFoto.Image = Image.FromStream(ms1);
                        }
                        else
                        {
                            this.PctFoto.Image = Image.FromFile(Imagen);
                            //PctFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);                            
                        }
                        string RutaArchivoDestino = Application.StartupPath + @"\imagenes\NombreArch.jpg";
                        File.Delete(RutaArchivoDestino);
                        //string Imagen = Application.StartupPath + @"\imagenes\doctor.png";
                        string ruta = Path.Combine(Application.StartupPath + @"\imagenes", "NombreArch.jpg");
                        PctFoto.Image.Save(ruta, ImageFormat.Jpeg);

                        //string ruta_imagen = @"C:\User\foto.jpg";
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo.FileName = "cmd.exe";
                        //PctFoto.Image = Image.FromFile(Imagen);

                        startInfo.Arguments = @"/c rundll32 ""C:\Program Files\Windows Photo Viewer\PhotoViewer.dll"", ImageView_Fullscreen " + RutaArchivoDestino;
                        process.StartInfo = startInfo;
                        process.Start();

                    }
                    catch (Exception EX)
                    {
                        MessageBox.Show(EX.ToString());
                    }

                }
                if (e.ColumnIndex == DgvExamenPacticadoColEliminar.Index)
                {
                    if (e.RowIndex > -1)
                    {
                        try
                        {
                            if (DgvExamenPacticado.Rows.Count > 1)
                            {
                                DgvExamenPacticado.Rows.RemoveAt(DgvExamenPacticado.CurrentRow.Index);
                            }
                            else
                            {
                                DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentRow.Index].Cells["DgvExamenPacticadoColAjuntar2"].Value = ".....";
                                DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentRow.Index].Cells["DgvExamenPacticadoColResultado"].Value = 1;
                                CargarFecha();
                            }
                        }
                        catch (Exception)
                        {
                            
                        }
                        
                    }
                }                
            }
           
        }
        public void GuardarImg()
        {
            string RutaArchivoDestino = @"C:\Users\JOSEPH\Pictures\Saved Pictures\NombreArch.jpg";
            string ruta = Path.Combine(@"C:\Users\JOSEPH\Pictures\Saved Pictures", "NombreArch.jpg");
            PctFoto.Image.Save(ruta, ImageFormat.Jpeg);
            //File.Delete(RutaArchivoDestino);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string RutaArchivoDestino = @"C:\Users\JOSEPH\Pictures\Saved Pictures\NombreArch.jpg";
            File.Delete(RutaArchivoDestino);
        }
        public void CargarIconosDGV()
        {
            string Imagen = Application.StartupPath + @"\imagenes\ver.png";
            DgvExamenPacticado.Rows[DgvExamenPacticado.Rows.Count - 1].Cells["DgvExamenPacticadoColFechaver"].Value = Image.FromFile(Imagen);
            DgvExamenPacticado.Rows[DgvExamenPacticado.Rows.Count - 1].Cells["DgvExamenPacticadoColFecha"].Value = "01/01/1999";
        }
        public void CargarFecha()
        {
            DgvExamenPacticado.Rows[DgvExamenPacticado.Rows.Count - 1].Cells["DgvExamenPacticadoColFecha"].Value = "01/01/1999";
            //MessageBox.Show("gh");
        }
        private void DgvExamenPacticado_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            CargarIconosDGV();
            CargarFecha();
        }

        private void DgvExamenPacticado_KeyDown(object sender, KeyEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyCode);
            if (codigo == 8 || e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        if (DgvExamenPacticado.Rows.Count > 1)
                        {
                            DgvExamenPacticado.Rows.RemoveAt(DgvExamenPacticado.CurrentRow.Index);
                            //DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentRow.Index].Cells["DgvExamenPacticadoColAjuntar2"].Value = ".....";
                            //DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentRow.Index].Cells["DgvExamenPacticadoColResultado"].Value = 1;
                        }
                        else
                        {
                            DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentRow.Index].Cells["DgvExamenPacticadoColAjuntar2"].Value = ".....";
                            DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentRow.Index].Cells["DgvExamenPacticadoColResultado"].Value = 1;
                            CargarFecha();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
        string IdPacienteSele = "";
        private void DgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        string IdPacienteTra = "";
        private void TxtDocumento_TextChanged(object sender, EventArgs e)
        {
                if (IdPacienteSele != IdPacienteTra)
                {
                    //LIMPIAR_CONTROLES();
                    //DgvExamenPacticado.Rows.Clear();
                    //TxtNombre.Clear();
                    //CargarDgvExamenes();
                    //IdPacienteTra = "";
                    //DgvDatos.ClearSelection();
                }         
        }

        private void TxtNombreEmpresa_TextChanged(object sender, EventArgs e)
        {

        }

        private void DgvDatos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Delete || e.KeyCode==Keys.End)
            {
                 if (TxtDocumento.Text != IdPacienteTra)
                {
                    LIMPIAR_CONTROLES();
                    CargarDgvExamenes();
                    TxtNombre.Clear();
                    //IdPacienteTra = "";
                    //DgvDatos.ClearSelection();
                }         
            }
        }

        private void DgvDatos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > -1)
            //{
            //    LIMPIAR_CONTROLES();
            //    TxtDocumento.Clear();
            //    TxtDocumento.Text = tablaPaciente.Rows[e.RowIndex]["Pac_Identificacion"].ToString();
            //    TxtDocumento.Focus();
            //    TxtDocumento.SelectAll();
            //    CargarDgvExamenes();
            //    BuscarPaciente_P(TxtDocumento.Text); 

            //}
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
            //Cargar combo de tipo examen
            CboTipoExamen.DataSource = ObjServer.LlenarTabla("SELECT [TipoExam_Codigo] As Codigo ,[TipoExam_Descripcion] As Descripcion FROM [dbo].[TipoExamen]");
            CboTipoExamen.DisplayMember = "Descripcion";
            CboTipoExamen.ValueMember = "Codigo";
            //
            //Cargar combo de tipo examen
            string Query = "SELECT [Enfa_Codigo] as Codigo ,[Enfa_Descripcion] as Descripcion FROM [dbo].[Enfasis]";//Where TipoExam_Codigo =" + tipoEaxamen;
            CboEnfasis.DataSource = ObjServer.LlenarTabla(Query);
            CboEnfasis.DisplayMember = "Descripcion";
            CboEnfasis.ValueMember = "Codigo";


            // Cargo los datos que tendra el combobox Concepto
            DataTable tableConcepto = new DataTable();
            CboConcepto.DataSource = ObjServer.LlenarTabla("SELECT [Conc_Codigo] As Codigo,[Conc_Descripcion] As Descripcion FROM [dbo].[Concepto]");
            CboConcepto.DisplayMember = "Descripcion";
            CboConcepto.ValueMember = "Codigo";
            //
            // Cargo los datos que tendra el combobox DgvExamenPacticadoColResultado
            DgvExamenPacticadoColResultado.DataSource = ObjServer.LlenarTabla("SELECT [TipRes_Codigo] As Codigo  ,[TipRes_Descripcion] As Descripcion  FROM [dbo].[TipResultado]");
            DgvExamenPacticadoColResultado.DisplayMember = "Descripcion";
            DgvExamenPacticadoColResultado.ValueMember = "Codigo";
            //

            //Cargo los datos que tendra el combobox de las empresas
            CboEmpresa.DataSource = ObjServer.LlenarTabla("SELECT [Empr_Codigo] As Codigo,[Empre_Nit] As Nit,[Empre_RazonSocial] As Descripcion  FROM [dbo].[Empresa] ORDER BY Descripcion");
            CboEmpresa.DisplayMember = "Descripcion";
            CboEmpresa.ValueMember = "Codigo";
            //

            #region CARGAR COMBO DE OCUPACIONES
            CboCargoOcupacion.DataSource = ObjServer.LlenarTabla("SELECT [Ocup_Codigo] as Codigo,[Ocup_Descripcion] AS Descripcion FROM [dbo].[Ocupacion]");
            CboCargoOcupacion.DisplayMember = "Descripcion";
            CboCargoOcupacion.ValueMember = "Codigo";
            #endregion
            //

            //DgvExamenPacticadoColResultado
            //DgvExamenPacticado.Rows.Clear();
            string query = "";
            query = "SELECT [Exam_Codigo] As [Codigo] ,[Exam_Descripcion] As [Descripcion] FROM [dbo].[Examen]";
            tabla = ObjServer.LlenarTabla(query);
            DgvExamenPacticadoColExamen.DataSource = tabla;
            DgvExamenPacticadoColExamen.DisplayMember = "Descripcion";
            DgvExamenPacticadoColExamen.ValueMember = "Codigo";
        }
      }

        private void DgvDatos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > -1)
            //{
            //    LIMPIAR_CONTROLES();
            //    TxtDocumento.Clear();
            //    TxtDocumento.Text = tablaPaciente.Rows[e.RowIndex]["Pac_Identificacion"].ToString();
            //    TxtDocumento.Focus();
            //    TxtDocumento.SelectAll();
            //    CargarDgvExamenes();
            //    BuscarPaciente_P(TxtDocumento.Text);

            //}
        }

        private void DgvDatos_SelectionChanged(object sender, EventArgs e)
        {
            if (DgvDatos.CurrentRow.Index > -1)
            {
                LIMPIAR_CONTROLES();
                TxtDocumento.Clear();
                TxtDocumento.Text = tablaPaciente.Rows[DgvDatos.CurrentRow.Index]["Pac_Identificacion"].ToString();
                TxtDocumento.Focus();
                TxtDocumento.SelectAll();
                CargarDgvExamenes();
                BuscarPaciente_P(TxtDocumento.Text);
                CARGAR_INFORMACION_OCUPACIONAL();

            }
        }
        public Boolean ValidarFecha(string fecha)
        {
            Boolean valor = false;
            try
            {
                if (fecha.Length == 10)
                {
                    DateTime.Parse(fecha);
                    valor = true;
                }
            }
            catch
            {
                valor = false;
            }
            return valor;
        }

        private void DgvExamenPacticado_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvExamenPacticadoColFecha.Index)
            {
                if (e.RowIndex > -1)
                {
                    int cell = e.RowIndex;
                    string fecha = DgvExamenPacticado.Rows[e.RowIndex].Cells["DgvExamenPacticadoColFecha"].Value.ToString();
                    if (ValidarFecha(fecha) == false)
                    {
                        MessageBox.Show("Fecha incorrecta");
                        DgvExamenPacticado.Rows[cell].Cells["DgvExamenPacticadoColFecha"].Value = "01/01/1990";
                    }
                }
            }
        }

        private void DgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void CboCargoOcupacion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnPacientesAgendados_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de cancelar la cita?", "Aceptar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                SqlConnection cnn = new SqlConnection(CadenaConexion.cadena());
                //Abrimos la conexión()
                cnn.Open();
                //Comenzamos la transacción ()
                try
                {
                    SqlCommand comman = new SqlCommand();

                    //ELIMINAR LOS EXAMENES PRACTICADOS
                    string QueryI = "DELETE FROM [dbo].[ExamenPracticadoProvi]  WHERE ExaPac_Paciente='" + TxtDocumento.Text + "'";
                    comman.CommandText = QueryI;
                    comman.Connection = cnn;
                    comman.ExecuteNonQuery();

                    comman = new SqlCommand();
                    //ELIMINAR 
                    QueryI = "DELETE FROM [dbo].[EntradaProvisional] WHERE Entr_IdPaciente='" + TxtDocumento.Text + "'";
                    comman.CommandText = QueryI;
                    comman.Connection = cnn;
                    comman.ExecuteNonQuery();
                    MessageBox.Show("Operación finalizada de manera exitosa", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {

                }
            }            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FrmCancelarCita f = new FrmCancelarCita();
            f.ShowDialog();
        }
    }
}
