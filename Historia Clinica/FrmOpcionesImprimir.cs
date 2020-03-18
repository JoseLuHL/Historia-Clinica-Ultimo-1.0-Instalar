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
    public partial class FrmOpcionesImprimir : Form
    {
        public FrmOpcionesImprimir()
        {
            InitializeComponent();
        }
      public  int NumeroAtencion;
      public string documento;

        private void FrmOpcionesImprimir_Load(object sender, EventArgs e)
        {

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void PnlCertificado_Paint(object sender, PaintEventArgs e)
        {
                //Convert.ToInt32(DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColNumeroHistoria"].Value.ToString());
        }

        private void PnlAtencion_Paint(object sender, PaintEventArgs e)
        {
        }

        private void PnlAtencion_Click(object sender, EventArgs e)
        {
            FrmItenInforme f = new FrmItenInforme();
            f.NumeroAten = NumeroAtencion;
            f.ShowDialog();
        }

        private void PnlExamenes_Click(object sender, EventArgs e)
        {

            FrmVerExamenes f = new FrmVerExamenes();
            f.NumeroAtencion = NumeroAtencion;
            f.NombreCompleto = LblNombre.Text;
            f.LblDocumento.Text = DocumentoP;
            f.ShowDialog();
        }
        public void EventoBoton(EventArgs e, Panel btn, Color color)
        {
            if (btn.Enabled == true)
            {
                btn.BackColor = color;
            }
        }
        private void PnlExamenes_MouseHover(object sender, EventArgs e)
        {
            EventoBoton(e, PnlExamenes, Color.SteelBlue);
        }

        private void PnlExamenes_MouseLeave(object sender, EventArgs e)
        {
            EventoBoton(e, PnlExamenes, Color.Transparent);
        }

        private void PnlAtencion_MouseHover(object sender, EventArgs e)
        {
            EventoBoton(e, PnlAtencion, Color.SteelBlue);
        }

        private void PnlAtencion_MouseLeave(object sender, EventArgs e)
        {
            EventoBoton(e, PnlAtencion, Color.Transparent);

        }

        private void PnlCertificado_MouseHover(object sender, EventArgs e)
        {
            EventoBoton(e, PnlCertificado, Color.SteelBlue);
        }

        private void PnlCertificado_MouseLeave(object sender, EventArgs e)
        {
            EventoBoton(e, PnlCertificado, Color.Transparent);
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
        public string DocumentoP;
        public string NumEntradaP;
        public string EnfasisP;
        public string FechaP;
        public string ConceptoP;

        private void PnlCertificado_Click(object sender, EventArgs e)
        {
            DataTable TablaPaciente = new DataTable();
            ClsSqlServer NuevoSql = new ClsSqlServer();
            NuevoSql.CadenaCnn = Historia_Clinica.Conexion.CadenaConexion.cadena();
            NuevoSql.Conectar();

            string Documento = DocumentoP;
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

                string NumeroEntrada = NumEntradaP;
                string Enfasis = EnfasisP;
                string Fecha = FechaP;
                string ConceptoTarea = ConceptoP;

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
                if (RdbCarta.Checked)
                {
                    CrystalReport1 cry = new CrystalReport1();
                    cry.SetParameterValue("NombreCompleto", TablaPaciente.Rows[0]["Nombres"]);
                    cry.SetParameterValue("Documento", TablaPaciente.Rows[0]["Pac_Identificacion"]);
                    cry.SetParameterValue("Edad", edad);
                    cry.SetParameterValue("Genero", TablaPaciente.Rows[0]["Gen_Descripcion"]);
                    cry.SetParameterValue("Telefono", TablaPaciente.Rows[0]["Pac_Telefono"].ToString());
                    cry.SetParameterValue("Ciudad", Configuracion.LugarExamen);
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
                    cry.SetParameterValue("Ciudad", Configuracion.LugarExamen);
                    cry.SetParameterValue("nit", Configuracion.Nit);
                    cry.SetParameterValue("tel", Configuracion.Telefono);
                    cry.SetParameterValue("direccion", Configuracion.Direccion);
                    cry.SetParameterValue("piepagina", Configuracion.PiePagina);
                    cry.SetParameterValue("nitempresa", TablaPaciente.Rows[0]["Empre_Nit"].ToString());
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
                            "WHERE Pac_Identificacion='" + Documento +"'";
                    TablaPaciente = null;
                    TablaPaciente = NuevoSql.LlenarTabla(Query);

                    if (TablaPaciente.Rows.Count > 0)
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
            else
                {
                    CrystalReport1Oficio cry = new CrystalReport1Oficio();
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
                    Query = "SELECT Empresa.Empre_RazonSocial, Empresa.Empre_Nit, cargo.Carg_Descripcion as Ocup_Descripcion, InfOcu_Entrada_Numero FROM dbo.InformacionOcupacional inner join Empresa on InfOcu_CodEmpresa=Empresa.Empr_Codigo inner join cargo  on cargo.Carg_Codigo = InformacionOcupacional.InfOcu_CodOcupacion WHERE InfOcu_Entrada_Numero=" + NumeroEntrada;
                    TablaPaciente = NuevoSql.LlenarTabla(Query);
                    cry.SetParameterValue("NombreEmpresa", TablaPaciente.Rows[0]["Empre_RazonSocial"].ToString());
                    cry.SetParameterValue("Ciudad", Configuracion.LugarExamen);
                    cry.SetParameterValue("nit", Configuracion.Nit);
                    cry.SetParameterValue("tel", Configuracion.Telefono);
                    cry.SetParameterValue("direccion", Configuracion.Direccion);
                    cry.SetParameterValue("piepagina", Configuracion.PiePagina);
                    cry.SetParameterValue("nitempresa", TablaPaciente.Rows[0]["Empre_Nit"].ToString());
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
                
                //}
            //}
        }

        private void PnlExamenes_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
