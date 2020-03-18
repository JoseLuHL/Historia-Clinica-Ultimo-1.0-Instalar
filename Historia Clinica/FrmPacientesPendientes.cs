using Historia_Clinica.Conexion;
using Historia_Clinica.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Historia_Clinica
{
    public partial class FrmPacientesPendientes : Form
    {
        public FrmPacientesPendientes()
        {
            InitializeComponent();
        }
        //ESTILOS DEL DGV
        public void EstilosDgv2(DataGridView DGV)
        {

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
        ClsSqlServer NuevoSql = new ClsSqlServer();
        DataTable TablaPaciente = new DataTable();
        public async Task<string>  BuscarHistoriaSinAtender()
        {
                TablaPaciente = new DataTable();
                DgvDatos.Rows.Clear();
                NuevoSql.CadenaCnn = Historia_Clinica.Conexion.CadenaConexion.cadena();
                NuevoSql.Conectar();
                string Criterio_Empresa = "";
                if (RdbTodas.Checked == false)
                {
                    Criterio_Empresa = " WHERE dbo.InformacionOcupacionalProvi.InfOcu_CodEmpresa= " + CboEmpresa.SelectedValue;
                }
            
                string Query = "SELECT DISTINCT	CONCAT(dbo.Paciente.Pac_Nombre1,' ',dbo.Paciente.Pac_Nombre2,' ',        "+
                                 "dbo.Paciente.Pac_Apellido1,' ',dbo.Paciente.Pac_Apellido2) AS nombres,                 "+
                                 " dbo.Paciente.Pac_TipoIdentificacion, dbo.EntradaProvisional.Ent_Codigo,               "+
                                 "dbo.Paciente.Pac_Identificacion, dbo.Paciente.Pac_FechaNacimiento,                     "+
                                 "dbo.EntradaProvisional.Entr_FechaEntrada,                                               " +
                                 "dbo.TipoExamen.TipoExam_Descripcion, dbo.InformacionOcupacionalProvi.InfOcu_CodEmpresa, " +
                                 "dbo.Enfasis.Enfa_Descripcion, dbo.Paciente.Pac_CodGenero,                          "+
                                 "dbo.EntradaProvisional.Ent_Estado, dbo.Genero.Gen_Codigo, dbo.Genero.Gen_Descripcion   "+
                                 "FROM  dbo.TipoExamen INNER JOIN                                                        "+
                                 "dbo.EntradaProvisional ON dbo.TipoExamen.TipoExam_Codigo =                             "+
                                 "dbo.EntradaProvisional.Entr_TipoExamenCodigo INNER JOIN                                "+
                                 "dbo.Enfasis ON dbo.EntradaProvisional.Ent_Enfasis = dbo.Enfasis.Enfa_Codigo INNER JOIN "+
                                 "dbo.Paciente INNER JOIN                                                                "+
                                 "dbo.Genero ON dbo.Paciente.Pac_CodGenero = dbo.Genero.Gen_Codigo ON                "+
                                 "dbo.EntradaProvisional.Entr_IdPaciente = dbo.Paciente.Pac_Identificacion   INNER JOIN "+
                                 "dbo.InformacionOcupacionalProvi ON dbo.EntradaProvisional.Entr_IdPaciente = dbo.InformacionOcupacionalProvi.InfOcu_paciente        "+ Criterio_Empresa +
                                 "  ORDER BY dbo.EntradaProvisional.Ent_Codigo, dbo.InformacionOcupacionalProvi.InfOcu_CodEmpresa   DESC";
            //"WHERE  CONCAT(dbo.Paciente.Pac_Nombre1,dbo.Paciente.Pac_Nombre2,dbo.Paciente.Pac_Apellido1,dbo.Paciente.Pac_Apellido2,Pac_Identificacion,Entr_Numero) LIKE" + "'%" + TxtCriterio.Text + "%'  And Entr_FechaEntrada between  '" + DtDesde.Text + "'and'" + DtHasta.Text + "'";
                await Task.Run(() => {TablaPaciente = NuevoSql.LlenarTabla(Query); });
            //await Task.Run(() => { tablaPaciente = ObjServer.LlenarTabla(Query); });
            //MessageBox.Show(TablaPaciente.Rows.Count.ToString());
            if (TablaPaciente.Rows.Count > 0)
                {
                    for (int i = 0; i < TablaPaciente.Rows.Count; i++)
                    {
                        DgvDatos.Rows.Add((i + 1).ToString(), "", TablaPaciente.Rows[i]["Pac_Identificacion"].ToString(), TablaPaciente.Rows[i]["nombres"].ToString(), DateTime.Now.Year - Convert.ToDateTime(TablaPaciente.Rows[i]["Pac_FechaNacimiento"]).Year + " años", TablaPaciente.Rows[i]["Gen_Descripcion"].ToString(), TablaPaciente.Rows[i]["Entr_FechaEntrada"], TablaPaciente.Rows[i]["TipoExam_Descripcion"].ToString(), TablaPaciente.Rows[i]["Enfa_Descripcion"].ToString());
                    }
                    DgvDatosColatendido.Visible = false;
                    DgvDatosColImprimir.Visible = false;
                    DgvDatosColNumeroHistoria.Visible = false;

                    TablaPaciente.Dispose();
                }
                EstilosDgv2(DgvDatos);

            return string.Empty;
        }
        public async Task<string> BuscarHistoriaAtendida()
        {
            try
            {
                DgvDatos.Rows.Clear();
                NuevoSql.CadenaCnn = Historia_Clinica.Conexion.CadenaConexion.cadena();
                NuevoSql.Conectar();
                string criterio = "";
                if (RdbAtendido.Checked == true && TxtCriterio.Text != "")
                {
                    criterio = "AND  CONCAT(dbo.Paciente.Pac_Nombre1,dbo.Paciente.Pac_Nombre2,dbo.Paciente.Pac_Apellido1,dbo.Paciente.Pac_Apellido2,Pac_Identificacion,Entr_Numero) LIKE" + "'%" + TxtCriterio.Text.Replace("'","") + "%'";
                }
                string Criterio_Empresa = "";
                if (RdbTodas.Checked == false)
                {
                    Criterio_Empresa = " AND dbo.InformacionOcupacional.InfOcu_CodEmpresa= "+ CboEmpresa.SelectedValue;
                }
                string Query = "SELECT DISTINCT	CONCAT(dbo.Paciente.Pac_Nombre1,' ',dbo.Paciente.Pac_Nombre2,' ',        				            " +
                                "dbo.Paciente.Pac_Apellido1,' ',dbo.Paciente.Pac_Apellido2) AS nombres,                                             " +
                                "dbo.Paciente.Pac_TipoIdentificacion, dbo.Paciente.Pac_Identificacion,dbo.InformacionOcupacional.InfOcu_CodEmpresa,  " +
                                "dbo.Paciente.Pac_FechaNacimiento, dbo.TipoExamen.TipoExam_Descripcion,Ent_conceptoAptitud, 		                                    " +
                                "dbo.Enfasis.Enfa_Descripcion, dbo.Genero.Gen_Codigo, dbo.Genero.Gen_Descripcion, dbo.EntradaHistoria.Ent_Estado, 	" +
                                "dbo.EntradaHistoria.Entr_Numero, dbo.EntradaHistoria.Entr_FechaEntrada			                                    " +
                                "FROM	dbo.TipoExamen INNER JOIN	dbo.EntradaHistoria ON dbo.TipoExamen.TipoExam_Codigo = 						" +
                                "dbo.EntradaHistoria.Entr_TipoExamenCodigo AND 									                                    " +
                                "dbo.TipoExamen.TipoExam_Codigo = dbo.EntradaHistoria.Entr_TipoExamenCodigo 	                                    " +
                                "INNER JOIN  dbo.Enfasis ON dbo.EntradaHistoria.Ent_Enfasis = dbo.Enfasis.Enfa_Codigo 		                        " +
                                "INNER JOIN	dbo.Paciente INNER JOIN  dbo.Genero ON dbo.Paciente.Pac_CodGenero = dbo.Genero.Gen_Codigo ON 		" +
                                "dbo.EntradaHistoria.Entr_IdPaciente = dbo.Paciente.Pac_Identificacion AND 	                                    " +
                                "dbo.EntradaHistoria.Entr_IdPaciente = dbo.Paciente.Pac_Identificacion		INNER JOIN "+
                                "dbo.InformacionOcupacional ON dbo.EntradaHistoria.Entr_Numero = dbo.InformacionOcupacional.InfOcu_Entrada_Numero                                    " +
                                "where	 Entr_FechaEntrada BETWEEN '" + DtDesde.Text + "'" + " AND '" + DtHasta.Text + "' " + criterio + " " + Criterio_Empresa + " ORDER BY Entr_Numero DESC";

                DataTable Tabla = new DataTable();
                await Task.Run(() => { Tabla = NuevoSql.LlenarTabla(Query); });

                EstilosDgv2(DgvDatos);   
                
                if (Tabla.Rows.Count > 0)
                {
                    for (int i = 0; i < Tabla.Rows.Count; i++)
                    {
                        DgvDatos.Rows.Add((i + 1).ToString(), Tabla.Rows[i]["Entr_Numero"].ToString(), Tabla.Rows[i]["Pac_Identificacion"].ToString(), Tabla.Rows[i]["nombres"].ToString(), DateTime.Now.Year - Convert.ToDateTime(Tabla.Rows[i]["Pac_FechaNacimiento"]).Year + " años", Tabla.Rows[i]["Gen_Descripcion"].ToString(), Tabla.Rows[i]["Entr_FechaEntrada"].ToString().Substring(0, 10), Tabla.Rows[i]["TipoExam_Descripcion"].ToString(), Tabla.Rows[i]["Enfa_Descripcion"].ToString(), Tabla.Rows[i]["Entr_FechaEntrada"].ToString(), Tabla.Rows[i]["Ent_conceptoAptitud"].ToString());

                        DgvDatos.Rows[i].Cells["Column1"].Value = Tabla.Rows[i]["Ent_Estado"].ToString();
                        if (Tabla.Rows[i]["Ent_Estado"].ToString() == "True")
                            DgvDatos.Rows[i].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                        else
                        {
                            DgvDatos.Rows[i].DefaultCellStyle.BackColor = Color.SteelBlue;
                            DgvDatos.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        }
                    }
                    DgvDatosColatendido.Visible = true;
                    DgvDatosColImprimir.Visible = true;
                    DgvDatosColNumeroHistoria.Visible = true;
                    Tabla = null;
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());                
                throw;
            }
            return string.Empty;
        }
        public async void  CargarLoad()
        {
            try
            {
                if (RdbSinAtender.Checked == true)
                {
                 await BuscarHistoriaSinAtender();
                }
                   
                else
                {
                  await  BuscarHistoriaAtendida();
                }                  
            }
            catch (Exception)
            {

            }
        }
        private async void FrmPacientesPendientes_Load(object sender, EventArgs e)
        {
            //Cargo los datos que tendra el combobox de las empresas
            NuevoSql.CadenaCnn = await Historia_Clinica.Conexion.CadenaConexion.cadenaAsync();
            NuevoSql.Conectar();

            CboEmpresa.DataSource =  NuevoSql.LlenarTabla("SELECT [Empr_Codigo] As Codigo,[Empre_Nit] As Nit,[Empre_RazonSocial] As Descripcion  FROM [dbo].[Empresa] ORDER BY Descripcion");
            CboEmpresa.DisplayMember = "Descripcion";
            CboEmpresa.ValueMember = "Codigo";

            if (RdbAtendido.Checked)
            {
                await BuscarHistoriaAtendida();
                GruBuscar.Visible = true;
            }
            else
            {
                await BuscarHistoriaSinAtender();
                GruBuscar.Visible = false;
            }
        }
        public async void CARGAR_TODO()
        {
            if (RdbAtendido.Checked)
            {
               await BuscarHistoriaAtendida();
            }
            else
            {
              await  BuscarHistoriaSinAtender();
            }
            if (DgvDatos.Rows.Count == 0)
            {
                MessageBox.Show("No se han encontrado resultados \n Ingrese otra fecha o empresa", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Btn_LupaBuscar_Click(object sender, EventArgs e)
        {
            CARGAR_TODO();
        }
        private async void RdbSinAtender_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (RdbAtendido.Checked)
            //    {
            //        await BuscarHistoriaAtendida();
            //        GruBuscar.Visible = true;
            //    }
            //    else
            //    {
            //        await BuscarHistoriaSinAtender();
            //        GruBuscar.Visible = false;
            //    }
            //}
            //catch (Exception)
            //{
            //}

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
        string IdPacientenAtencion;
        public Boolean ConsultarPaciente()
        {
            CARGAR_TODO();
            AutoClosingMessageBox.Show("Cargando...", "", 1500);
            Boolean retornar =false;
            int x = 0;
            for (int i = 0; i < DgvDatos.Rows.Count; i++)
            {
                string valor = DgvDatos.Rows[i].Cells["DgvDatosColDocumento"].Value.ToString();
                if (valor==IdPacientenAtencion)
                {
                    x++;
                    retornar = true;
                    break;
                }
            }

            if (x==0)
            {
                retornar = false;
            }
            return retornar;
        }
        private void DgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (RdbSinAtender.Checked)
            {
                //ABIRI FORMULADO DE HISTORIA CLINICA
                if (e.RowIndex > -1 && e.ColumnIndex==3)
                {
                    FrnHistoriaClinica f = new FrnHistoriaClinica();
                    #region VARIABLES 
                    string documento = DgvDatos.CurrentRow.Cells["DgvDatosColDocumento"].Value.ToString();
                    string edad = DgvDatos.CurrentRow.Cells["DgvDatosColedad"].Value.ToString();
                    string sexo = DgvDatos.CurrentRow.Cells["DgvDatosColSexo"].Value.ToString();
                    string fecha = DgvDatos.CurrentRow.Cells["DgvDatosColFecha"].Value.ToString();
                    string entrada = DgvDatos.CurrentRow.Cells["DgvDatosColEntrada"].Value.ToString();
                    string efasis = DgvDatos.CurrentRow.Cells["DgvDatosColEnfasis"].Value.ToString();
                    string Nombres = DgvDatos.CurrentRow.Cells["DgvDatosColNombres"].Value.ToString();
                    #endregion 
                    IdPacientenAtencion = documento;
                    if (ConsultarPaciente())
                    {
                    #region PASAMOS LOS VALORES A LOS OBJETOS DEL FORMULARIO 
                    //ConsultarPaciente();
                    f.LblDatos.Text = "Tipo de Examen: " + entrada;
                    f.LblDatos1.Text = "Enfasis: " + efasis;
                    f.LblDatos2.Text = "Fecha : " + fecha.Substring(0, 10);
                    f.LblDatos3.Text = "Sexo: " + sexo;
                    f.LblDatos4.Text = "Edad: " + edad;
                    f.TxtNombre.Text = Nombres;
                    f.TxtDocumento.Text = documento;
                    f.SooloVer = false;
                    #endregion 
                    f.ShowDialog();
                    }
                    else
                        MessageBox.Show("El paciente ya fue atendido", "", MessageBoxButtons.OK);
                }
            }
            else
            {
                #region PARA ABRIR EL REPORTE Y EL FORMULARIO PARA VISUALIZAR LA INFORMACIÓN
                if (e.RowIndex > -1)
                {
                    if (e.ColumnIndex == DgvDatosColImprimir.Index)
                    {
                            string Documento = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColDocumento"].Value.ToString();
                            //SE REALIZAN LAS RESPECTIVAS COLUMNAS Y SE LLENAN LAS TABLAS CON LA INFORMACIÓN QUE SE REQUIERE PARA EL REPORTE
                            string Query = "SELECT	dbo.Paciente.Pac_Nombre1+ ' ' + isnull(dbo.Paciente.Pac_Nombre2,'')                         " +
                                                "		+' '+dbo.Paciente.Pac_Apellido1+ ' '+isnull(dbo.Paciente.Pac_Apellido2,'') As Nombres,      " +
                                                "		dbo.Paciente.Pac_TipoIdentificacion, dbo.Paciente.Pac_TipoIdentificacion+' '+dbo.Paciente.Pac_Identificacion As Pac_Identificacion,                       " +
                                                "		dbo.Paciente.Pac_FechaNacimiento, dbo.Genero.Gen_Descripcion,                               " +
                                                "		dbo.Paciente.Pac_CodCiudad, dbo.Paciente.Pac_Direccion,                               " +
                                                "		dbo.Paciente.Pac_CodNivelEducativo, dbo.Paciente.Pac_CodProfesion,Profesion.Prof_Descripcion,                             " +
                                                "		dbo.TipoSangre.TipSan_Descripcion, dbo.Paciente.Pac_EstadoCivil,                            " +
                                                "		dbo.EstadoCivil.EstCivil_Descripcion,Pac_Telefono                                           " +
                                                "FROM	dbo.Paciente LEFT OUTER JOIN                                                                " +
                                                "		dbo.EstadoCivil ON dbo.Paciente.Pac_EstadoCivil =                                           " +
                                                "		dbo.EstadoCivil.EstCivil_Codigo LEFT OUTER JOIN                                             " +
                                                "		dbo.TipoSangre ON dbo.Paciente.Pac_TipoSangre =                                             " +
                                                "		dbo.TipoSangre.TipSan_Codigo LEFT OUTER JOIN                                                " +
                                                "		dbo.Genero ON dbo.Paciente.Pac_CodGenero = dbo.Genero.Gen_Codigo inner join Profesion on Profesion.Prof_Codigo =    dbo.Paciente.Pac_CodProfesion                        " +
                                                "WHERE	Pac_Identificacion='" + Documento + "'";
                            TablaPaciente = NuevoSql.LlenarTabla(Query);
                            //FIN PACIENTE

                            string NumeroEntrada = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColNumeroHistoria"].Value.ToString();
                            string Enfasis = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColEnfasis"].Value.ToString();
                            string Fecha = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColFecha1"].Value.ToString();
                            string ConceptoTarea = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosconceptoAptitud"].Value.ToString();

                            string examenes = "";
                            string query = "SELECT  dbo.Examen.Exam_Descripcion, dbo.EntradaHistoria.Entr_Numero, dbo.TipResultado.TipRes_Descripcion               " +
                                                "FROM  dbo.ExamenPracticado INNER JOIN                                                                              " +
                                                "dbo.Examen ON dbo.ExamenPracticado.ExaPrac_Examen_Codigo = dbo.Examen.Exam_Codigo INNER JOIN                       " +
                                                "dbo.EntradaHistoria ON dbo.ExamenPracticado.ExaPrac_Entrada_Numero = dbo.EntradaHistoria.Entr_Numero INNER JOIN    " +
                                                "dbo.TipResultado ON dbo.ExamenPracticado.ExaPrac_Resultado = dbo.TipResultado.TipRes_Codigo INNER JOIN             " +
                                                "dbo.TipResultado AS TipResultado_1 ON dbo.ExamenPracticado.ExaPrac_Resultado = TipResultado_1.TipRes_Codigo WHERE dbo.EntradaHistoria.Entr_Numero=" + NumeroEntrada;
                            DataTable TablaExamenes = new DataTable();
                            TablaExamenes = NuevoSql.LlenarTabla(query);
                            for (int i = 0; i < TablaExamenes.Rows.Count; i++)
                            {
                                string x = TablaExamenes.Rows[i]["Exam_Descripcion"].ToString() + " (" + TablaExamenes.Rows[i]["TipRes_Descripcion"].ToString() + ")";
                                if (i == 0) examenes = x;
                                else examenes = examenes + "      " + x;
                            }
                            //ENVIÓ DE PARAMETROS AL REPORTE 
                            string edad = Convert.ToString(CalcularEdad(Convert.ToDateTime(TablaPaciente.Rows[0]["Pac_FechaNacimiento"]))) + " " + "Años";
                            FrmVisualizarReporte f = new FrmVisualizarReporte();
                            CrystalReport1 cry = new CrystalReport1();
                            string documento = TablaPaciente.Rows[0]["Pac_Identificacion"].ToString();
                            cry.SetParameterValue("NombreCompleto", TablaPaciente.Rows[0]["Nombres"]);
                            cry.SetParameterValue("Documento", TablaPaciente.Rows[0]["Pac_Identificacion"]);
                            cry.SetParameterValue("Edad", edad);
                            cry.SetParameterValue("Genero", TablaPaciente.Rows[0]["Gen_Descripcion"]);
                            cry.SetParameterValue("Telefono", TablaPaciente.Rows[0]["Pac_Telefono"].ToString());
                            cry.SetParameterValue("Ciudad", Configuracion.LugarExamen);
                            cry.SetParameterValue("nit", Configuracion.Nit);
                            cry.SetParameterValue("piepagina", Configuracion.PiePagina);
                            if (examenes.Trim() == "")
                            {
                                cry.SetParameterValue("ExamenesPracticados", "NO APLICA");

                            }
                            else
                                cry.SetParameterValue("ExamenesPracticados", examenes);

                            cry.SetParameterValue("@NumeroEntrada", Convert.ToInt32(NumeroEntrada));
                            cry.SetParameterValue("Enfasis", Enfasis);
                            cry.SetParameterValue("FechaExamen", Fecha);
                            if (ConceptoTarea == "")
                                cry.SetParameterValue("DesAptitud", "");
                            else
                                cry.SetParameterValue("DesAptitud", "CONCEPTO DE APTITUD PARA LA TAREA:");
                            cry.SetParameterValue("ConceptoTarea", ConceptoTarea);

                            //PARA EL TIPO DE EXAMEN
                            Query = "SELECT dbo.TipoExamen.TipoExam_Descripcion,                " +
                                    "dbo.Concepto.Conc_Descripcion                              " +
                                    "FROM   dbo.TipoExamen INNER JOIN                           " +
                                    "dbo.EntradaHistoria ON dbo.TipoExamen.TipoExam_Codigo =    " +
                                    "dbo.EntradaHistoria.Entr_TipoExamenCodigo INNER JOIN       " +
                                    "dbo.Concepto ON dbo.EntradaHistoria.Entr_Concepto_Codigo = " +
                                    "dbo.Concepto.Conc_Codigo where dbo.EntradaHistoria.Entr_Numero= " + NumeroEntrada;
                            TablaPaciente = null;
                            TablaPaciente = NuevoSql.LlenarTabla(Query);
                            cry.SetParameterValue("TipoExamen", TablaPaciente.Rows[0]["TipoExam_Descripcion"].ToString());
                            cry.SetParameterValue("Concepto", TablaPaciente.Rows[0]["Conc_Descripcion"].ToString());
                            TablaPaciente = null;
                            //PARA EL NOMBRE DE LA EMPRESA
                            Query = "SELECT Empresa.Empre_RazonSocial,Empresa.Empre_Nit, cargo.Carg_Descripcion as Ocup_Descripcion, InfOcu_Entrada_Numero FROM dbo.InformacionOcupacional inner join Empresa on InfOcu_CodEmpresa=Empresa.Empr_Codigo inner join cargo  on cargo.Carg_Codigo = InformacionOcupacional.InfOcu_CodOcupacion WHERE InfOcu_Entrada_Numero=" + NumeroEntrada;
                            TablaPaciente = NuevoSql.LlenarTabla(Query);
                            cry.SetParameterValue("NombreEmpresa", TablaPaciente.Rows[0]["Empre_RazonSocial"].ToString());
                            cry.SetParameterValue("nitempresa", TablaPaciente.Rows[0]["Empre_Nit"].ToString());
                            cry.SetParameterValue("Ciudad", Configuracion.LugarExamen);
                            cry.SetParameterValue("nit", Configuracion.Nit);
                            cry.SetParameterValue("tel", Configuracion.Telefono);
                            cry.SetParameterValue("direccion", Configuracion.Direccion);
                            cry.SetParameterValue("piepagina", Configuracion.PiePagina);
                            cry.SetParameterValue("Cargo", TablaPaciente.Rows[0]["Ocup_Descripcion"]);

                            Query = "SELECT	dbo.ARL.Arl_Codigo, dbo.ARL.Arl_Descripcion, " +
                               "dbo.EPS.Eps_Codigo, dbo.EPS.Eps_Descripcion,                              " +
                               "dbo.Paciente.Pac_Identificacion, dbo.Paciente.Pac_Nombre1,                " +
                               "dbo.Paciente.Pac_Nombre2, dbo.Paciente.Pac_Apellido1,                     " +
                               "dbo.Paciente.Pac_Apellido2, dbo.EntradaHistoria.Entr_IdPaciente,          " +
                               "dbo.EntradaHistoria.Entr_FechaEntrada, dbo.EntradaHistoria.Entr_Numero    " +
                               "FROM	dbo.EntradaHistoria RIGHT OUTER JOIN                               " +
                               "dbo.EPS INNER JOIN                                                        " +
                               "dbo.Paciente ON dbo.EPS.Eps_Codigo = dbo.Paciente.Pac_CodEPS INNER JOIN   " +
                               "dbo.ARL ON dbo.Paciente.Pac_CodARL = dbo.ARL.Arl_Codigo ON                " +
                               "dbo.EntradaHistoria.Entr_IdPaciente = dbo.Paciente.Pac_Identificacion     " +
                               "AND dbo.EntradaHistoria.Entr_IdPaciente = dbo.Paciente.Pac_Identificacion "+
                               "WHERE Pac_Identificacion='" + Documento + "'";
                            TablaPaciente = null;
                            TablaPaciente = NuevoSql.LlenarTabla(Query);
                            if (TablaPaciente.Rows.Count>0)
                            {
                                cry.SetParameterValue("EPS", TablaPaciente.Rows[0]["Eps_Descripcion"]);
                                cry.SetParameterValue("ARL", TablaPaciente.Rows[0]["Arl_Descripcion"]);
                            }
                            else
                            {
                                cry.SetParameterValue("EPS", "");
                                cry.SetParameterValue("ARL", "");
                            }
                            

                            TablaPaciente = null;
                            TablaExamenes = null;
                            //SE VISUALIZA EL REPORTE
                            f.crystalReportViewer1.ReportSource = cry;
                            f.Show();
                    }
                }
                #endregion

                #region PARA ARIR FORMULARIO DE HISTORIA PARA VER LA HISTORIA DEL PACIENTE
                if (e.RowIndex>-1)
                {
                    if (e.ColumnIndex == DgvDatosColatendido.Index)
                    {
                        string documento = DgvDatos.CurrentRow.Cells["DgvDatosColDocumento"].Value.ToString();
                        string NumeroEntrada = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColNumeroHistoria"].Value.ToString();
                        string Enfasis = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColEnfasis"].Value.ToString();
                        string Fecha = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColFecha1"].Value.ToString();
                        string ConceptoTarea = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosconceptoAptitud"].Value.ToString();
                        FrmOpcionesImprimir f = new FrmOpcionesImprimir();

                        f.NumeroAtencion = Convert.ToInt32(DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColNumeroHistoria"].Value.ToString());
                            string Nombres = DgvDatos.CurrentRow.Cells["DgvDatosColNombres"].Value.ToString();
                            f.LblNombre.Text = Nombres;
                            f.DocumentoP = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColDocumento"].Value.ToString();
                            f.NumEntradaP = NumeroEntrada;
                            f.EnfasisP = Enfasis;
                            f.FechaP = Fecha;
                            f.documento = documento;

                            f.ConceptoP = ConceptoTarea;

                            f.ShowDialog();
                    }

                    if ( e.ColumnIndex == DgvDatosColDocumento.Index || e.ColumnIndex == DgvDatosColNombres.Index)
                    {
                        if (LblMensaje.Text == "HISTORIA MÉDICA")
                        {
                                string paciente = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColDocumento"].Value.ToString();
                                FrnHistoriaClinica f = new FrnHistoriaClinica();
                                f.LblEntrada.Visible = true;
                                f.LblEntrada.Text = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColNumeroHistoria"].Value.ToString();
                                f.TxtDocumento.Text = paciente;
                                if (DgvDatos.Rows[e.RowIndex].Cells["Column1"].Value.ToString() == "False")
                                {
                                    f.SooloVer = true;
                                    //f.BuscarTodo();
                                    f.BtnCerrarHistoria.Visible = true;
                                    f.BtnGuardarHistoria.Visible = true;
                                    f.BtnCerrarHistoria.Visible = true;
                                    f.LblGuardarCerra.Visible = true;
                                    f.LblGuardar.Visible = true;
                                    f.TxtDocumento.BackColor = Color.SteelBlue;
                                }
                                else
                                {
                                    f.SooloVer = true;
                                    //f.BuscarTodo();
                                    f.TxtDocumento.ReadOnly = true;
                                    f.BtnGuardarHistoria.Visible = false;
                                    f.BtnCerrarHistoria.Visible = false;
                                    f.LblGuardarCerra.Visible = false;
                                    f.LblGuardar.Visible = false;
                                }
                                if (LblMensaje.Text == "CETIFICADOS Y HISTORIAS")
                                {
                                    f.TxtDocumento.ReadOnly = true;
                                    f.BtnGuardarHistoria.Visible = false;
                                    f.BtnCerrarHistoria.Visible = false;
                                    f.LblGuardarCerra.Visible = false;
                                    f.LblGuardar.Visible = false;
                                }
                                string documento = DgvDatos.CurrentRow.Cells["DgvDatosColDocumento"].Value.ToString();
                                string edad = DgvDatos.CurrentRow.Cells["DgvDatosColedad"].Value.ToString();
                                string sexo = DgvDatos.CurrentRow.Cells["DgvDatosColSexo"].Value.ToString();
                                string fecha = DgvDatos.CurrentRow.Cells["DgvDatosColFecha"].Value.ToString();
                                string entrada = DgvDatos.CurrentRow.Cells["DgvDatosColEntrada"].Value.ToString();
                                string efasis = DgvDatos.CurrentRow.Cells["DgvDatosColEnfasis"].Value.ToString();

                                f.LblDatos.Text = "Tipo de Examen: " + entrada;
                                f.LblDatos1.Text = "Enfasis: " + efasis;
                                f.LblDatos2.Text = "Fecha : " + fecha.Substring(0, 10);
                                f.LblDatos3.Text = "Sexo: " + sexo;
                                f.LblDatos4.Text = "Edad: " + edad;

                                f.ShowDialog();
                        }
                        else
                        {
                            if (DgvDatos.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.SteelBlue)
                            {
                                    if (MessageBox.Show("¿Desea ver y adjuntar examenes?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        string documento = DgvDatos.CurrentRow.Cells["DgvDatosColDocumento"].Value.ToString();
                                        string Nombres = DgvDatos.CurrentRow.Cells["DgvDatosColNombres"].Value.ToString();
                                        string NumeroAtencion = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColNumeroHistoria"].Value.ToString();
                                        FrmVerPacientes f = new FrmVerPacientes();
                                        f.LblNombrePaciente.Text = Nombres;
                                        f.LblNumeroAtencion.Text = NumeroAtencion;
                                        f.txtidentificacion.Text = documento;
                                        f.ShowDialog();
                                    }
                            }
                        }
                    }
                }
                #endregion
            }
        }
        private async void TxtCriterio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                if (RdbAtendido.Checked)
                {
                  await  BuscarHistoriaAtendida();
                }
                else
                {
                   await BuscarHistoriaSinAtender();
                }                   
            }
        }
        private void BtnHistoria_Click(object sender, EventArgs e)
        {
            FrnHistoriaClinica f = new FrnHistoriaClinica();
            f.Show();
        }
        private async  void button1_Click(object sender, EventArgs e)
        {
            if (RdbAtendido.Checked)
            {
                await BuscarHistoriaAtendida();
            }
            else
            {
                await BuscarHistoriaSinAtender();
            }
        }
        private void RdbAtendido_CheckedChanged(object sender, EventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FrmGenerarInforme fr = new FrmGenerarInforme();
            fr.ShowDialog();
        }
        private void ChkActivar_CheckedChanged(object sender, EventArgs e)
        {
        }
        private  void CboEmpresa_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }
        private void DtDesde_Validating(object sender, CancelEventArgs e)
        {
            CARGAR_TODO();
        }
        private void CboEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void RdbTodas_CheckedChanged(object sender, EventArgs e)
        {
            if (RdbTodas.Checked == true)
            {
                CboEmpresa.Enabled = false;
                CARGAR_TODO();
            }
            else
            {
                CboEmpresa.Enabled = true;
                CARGAR_TODO();
            }   
        }
        private void DgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 )
            {
                if (DgvDatos.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.SteelBlue)
                {
                    if (LblMensaje.Text == "HISTORIA MÉDICA")
                    {
                        if (MessageBox.Show("¿Desea abrir la atención?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string Numero = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColNumeroHistoria"].Value.ToString();
                            string sql = "update EntradaHistoria set Ent_Estado=0 where Entr_Numero=" + Numero;
                            NuevoSql.CadnaSentencia = sql;
                            NuevoSql.Sentencia();
                            //MessageBox.Show(sql);
                            DgvDatos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.SteelBlue;
                            DgvDatos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                            DgvDatos.Rows[e.RowIndex].Cells["Column1"].Value = "False";
                            MessageBox.Show("Atención abrierta", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
        }

        private async void CboEmpresa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (RdbAtendido.Checked)
                {
                    await BuscarHistoriaAtendida();
                }
                else
                {
                    await BuscarHistoriaSinAtender();
                }
            }
            catch (Exception)
            {
            }
        }

        private async void RdbSinAtender_Click(object sender, EventArgs e)
        {
            try
            {
                //if (RdbAtendido.Checked)
                //{
                await BuscarHistoriaSinAtender();
                GruBuscar.Visible = false;
                //}
                //else
                //{


                //}
            }
            catch (Exception)
            {
            }
        }

        private async void RdbAtendido_Click(object sender, EventArgs e)
        {
            await BuscarHistoriaAtendida();
            GruBuscar.Visible = true;
            
        }
    }
}
public class AutoClosingMessageBox
{
    System.Threading.Timer _timeoutTimer;
    string _caption;
    AutoClosingMessageBox(string text, string caption, int timeout)
    {
        _caption = caption;
        _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
            null, timeout, System.Threading.Timeout.Infinite);
        using (_timeoutTimer)
            MessageBox.Show(text, caption);
    }
    public static void Show(string text, string caption, int timeout)
    {
        new AutoClosingMessageBox(text, caption, timeout);
    }
    void OnTimerElapsed(object state)
    {
        IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
        if (mbWnd != IntPtr.Zero)
            SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        _timeoutTimer.Dispose();
    }
    const int WM_CLOSE = 0x0010;
    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
}