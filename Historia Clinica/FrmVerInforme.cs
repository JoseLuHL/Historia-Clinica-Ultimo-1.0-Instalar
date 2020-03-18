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
using Microsoft.Reporting.WinForms;
//using Microsoft.Reporting.WinForms.Internal.Soap.ReportingServices2005.Execution;
using Microsoft.ReportingServices;
namespace Historia_Clinica
{
    public partial class FrmVerInforme : Form
    {
        public FrmVerInforme()
        {
            InitializeComponent();
        }
        ClsSqlServer ObjConexion = new ClsSqlServer();

        public int NumeroAtencion = 8;
        public DataTable TablaItem = new DataTable();

        private void FrmVerInforme_Load(object sender, EventArgs e)
        {
            
            ObjConexion.CadenaCnn = Conexion.CadenaConexion.cadena();
            ObjConexion.Conectar();

            DataTable tablaItemDB = new DataTable();
            string Query1 = "SELECT [Intem_Codigo] " +
                            ",[Intem_Descripcion] " +
                            ",[Intem_Activo]      " +
                            "FROM [dbo].[ItemActivar]";
            //WHERE Pac_Identificacion=" + Documento;
            tablaItemDB = ObjConexion.LlenarTabla(Query1);

            // TODO: esta línea de código carga datos en la tabla 'HistoriaClinicaDataSet.SP_AntecedentesPersonales' Puede moverla o quitarla según sea necesario.
             //this.SP_AntecedentesPersonalesTableAdapter.
            this.HistoriaClinicaDataSet.EnforceConstraints = false;
            this.HistoriaClinicaData_Actualizado.EnforceConstraints=false;
            HistoriaClinicaDataSet_FirmaMedico.EnforceConstraints = false;
            //this.historiaClinicaDataSet_FirmaMedico1
            SP_RecomendacionesTableAdapter.Connection = ObjConexion.conexion;
            //var s = 
            DataTable tabla = new DataTable();
            string sql ="SELECT	dbo.EntradaHistoria.Entr_Numero, dbo.Recomendacion.Reco_Descripcion,                                                             "+
		                    "dbo.RecomendacionDescripcion.RecDes_Descripcion, dbo.Paciente.Pac_Identificacion, dbo.Paciente.Pac_Foto,                            "+
		                    "dbo.Paciente.Pac_Huella, dbo.Paciente.Pac_Firma,dbo.Medico.Medic_Huella, dbo.Medico.Medic_Firma                                     "+
                            "FROM	dbo.Paciente INNER JOIN                                                                                                      "+
		                    "dbo.EntradaHistoria ON dbo.Paciente.Pac_Identificacion = dbo.EntradaHistoria.Entr_IdPaciente LEFT OUTER JOIN                        "+
		                    "dbo.Medico ON dbo.EntradaHistoria.Ent_Medico = dbo.Medico.Medic_Identificacion LEFT OUTER JOIN                                      "+
		                    "dbo.RecomendacionDescripcion INNER JOIN                                                                                             "+
		                    "dbo.Recomendacion ON dbo.RecomendacionDescripcion.RecDes_Recomendacios_Codigo = dbo.Recomendacion.Reco_Codigo INNER JOIN            "+
		                    "dbo.RecomendacionPaciente ON dbo.RecomendacionDescripcion.RecDes_Codigo = dbo.RecomendacionPaciente.RecoPac_Recomendacion_Codigo ON "+
		                    "dbo.EntradaHistoria.Entr_Numero = dbo.RecomendacionPaciente.RecoPac_Entrada_Numero "+
		                    "WHERE Entr_Numero= "+NumeroAtencion;
            tabla = ObjConexion.LlenarTabla(sql);
            ReportDataSource r = new ReportDataSource("FirmaMedico", tabla);
            //this.SP_RecomendacionesTableAdapter.Fill(HistoriaClinicaDataSet_FirmaMedico.SP_Recomendaciones,NumeroAtencion);
            this.SP_AntecedentesPersonalesTableAdapter.Fill(this.HistoriaClinicaDataSet.SP_AntecedentesPersonales, NumeroAtencion);
            this.SP_AntecedenteFamiliarTableAdapter.Fill(this.HistoriaClinicaData_Actualizado.SP_AntecedenteFamiliar, NumeroAtencion);
            this.SP_HabitosTableAdapter.Fill(this.HistoriaClinicaData_Actualizado.SP_Habitos, NumeroAtencion);
            this.SP_InmunizarTableAdapter.Fill(this.HistoriaClinicaData_Actualizado.SP_Inmunizar, NumeroAtencion);
            this.SP_AccidenteLaboralTableAdapter.Fill(this.HistoriaClinicaData_Actualizado.SP_AccidenteLaboral, NumeroAtencion);
            this.SP_EnfermedadProfesionalTableAdapter.Fill(this.HistoriaClinicaData_Actualizado.SP_EnfermedadProfesional, NumeroAtencion);
            this.SP_RiesgosOcupacionalesTableAdapter.Fill(this.HistoriaClinicaData_Actualizado.SP_RiesgosOcupacionales, NumeroAtencion);
            this.SP_ExamenPracticadosTableAdapter.Fill(this.HistoriaClinicaData_Actualizado.SP_ExamenPracticados, NumeroAtencion);
            this.SP_ExamenFisicoTableAdapter.Fill(this.HistoriaClinicaData_Actualizado.SP_ExamenFisico, NumeroAtencion);
            SP_PruebaEquilibrioTableAdapter.Connection = ObjConexion.conexion;
            this.SP_PruebaEquilibrioTableAdapter.Fill(this.HistoriaClinicaData_Actualizado.SP_PruebaEquilibrio, NumeroAtencion);
            this.SP_RevisionSistemaTableAdapter.Fill(this.HistoriaClinicaData_Actualizado.SP_RevisionSistema, NumeroAtencion);
            this.SP_ReporteRecomendacionesTableAdapter.Fill(this.HistoriaClinicaData_Actualizado.SP_ReporteRecomendaciones, NumeroAtencion);
            this.SP_ActivarItemsTableAdapter.Fill(this.HistoriaClinica_Activar.SP_ActivarItems);

            //Creamos los parametros para la atenciòn
            ReportParameter FechaAtencion = new ReportParameter();
            ReportParameter TipoExamen = new ReportParameter();
            ReportParameter Enfasis = new ReportParameter();
            ReportParameter Concepto = new ReportParameter();
            ReportParameter Reubicacion = new ReportParameter();
            ReportParameter ConceptoAptitud = new ReportParameter();
            ReportParameter Diagnostico = new ReportParameter();
            ReportParameter Descripcion = new ReportParameter();


            //Creamos los parametros para la informacion del paciente
            ReportParameter NombreCompleto = new ReportParameter();
            ReportParameter NumeroDocumento = new ReportParameter();
            ReportParameter FechaNacimiento = new ReportParameter();
            ReportParameter Sexo = new ReportParameter();

            //Creamos los parametros para la informacion Ocupacional
            ReportParameter Empresa = new ReportParameter();
            ReportParameter Cargo = new ReportParameter();
            ReportParameter Jornada = new ReportParameter();
            ReportParameter Area = new ReportParameter();
            ReportParameter Elementos = new ReportParameter();
            ReportParameter Funciones = new ReportParameter();
            ReportParameter Materia = new ReportParameter();
            ReportParameter Herramientas = new ReportParameter();
            ReportParameter Maquinaria = new ReportParameter();
            ReportParameter FechaCargo = new ReportParameter();

            //Creamos los parametros para los Antecedentes Ginecologícos
            ReportParameter FechaRegla      = new ReportParameter();
            ReportParameter Hijos           = new ReportParameter();
            ReportParameter Partos          = new ReportParameter();
            ReportParameter Abortos         = new ReportParameter();
            ReportParameter Sanos           = new ReportParameter();
            ReportParameter Gestaciones     = new ReportParameter();
            ReportParameter Menopausia      = new ReportParameter();
            ReportParameter Menarca         = new ReportParameter();
            ReportParameter Citologia       = new ReportParameter();
            ReportParameter Planificacion = new ReportParameter();

            //Creamos los parametros para los Signos Vitales
            ReportParameter Talla           = new ReportParameter();
            ReportParameter Peso            = new ReportParameter();
            ReportParameter IMC             = new ReportParameter();
            ReportParameter Lateralidad     = new ReportParameter();
            ReportParameter Cardiaca        = new ReportParameter();
            ReportParameter Arterial        = new ReportParameter();
            ReportParameter Cintura         = new ReportParameter();
            ReportParameter Interpretacion  = new ReportParameter();

            //Creamos los parametros para prueba de equilibrio
            ReportParameter Marcha  = new ReportParameter();
            ReportParameter Reflejo = new ReportParameter();
            ReportParameter Piel    = new ReportParameter();

            //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
            //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
            //                          VISUALIZAR INFORMACIÓN                           //
            //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
            //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
            ReportParameter INFORMACION_DEL_ASPIRANTE_O_TRABAJADOR                  = new ReportParameter();
            ReportParameter INFORMACION_DE_LA_ATENCION_DEL_ASPIRANTE_O_TRABAJADOR   = new ReportParameter();
            ReportParameter ANTECEDENTES_PERSONALES                                 = new ReportParameter();
            ReportParameter ANTECEDENTES_FAMILIARES                                 = new ReportParameter();
            ReportParameter HABITOS                                                 = new ReportParameter();
            ReportParameter INMUNIZACION                                            = new ReportParameter();
            ReportParameter ACCIDENTE_LABORAL                                       = new ReportParameter();
            ReportParameter ENFERMEDAD_PROFESIONAL                                  = new ReportParameter();
            ReportParameter RIESGOS_OCUPACIONALES                                   = new ReportParameter();
            ReportParameter INFORMACION_OCUPACIONAL                                 = new ReportParameter();
            ReportParameter EXAMENES_PRACTICADOS                                    = new ReportParameter();
            ReportParameter ANTECEDENTES_GINECOLOGICOS                              = new ReportParameter();
            ReportParameter EXAMEN_FISICO                                           = new ReportParameter();
            ReportParameter SIGNOS_VITALES                                          = new ReportParameter();
            ReportParameter PRUEBA_DE_EQUILIBRIO                                    = new ReportParameter();
            ReportParameter REVISION_POR_SISTEMA                                    = new ReportParameter();
            ReportParameter RECOMENDACIONES                                         = new ReportParameter();

            INFORMACION_DEL_ASPIRANTE_O_TRABAJADOR = new ReportParameter("INFORMACION_DEL_ASPIRANTE_O_TRABAJADOR", TablaItem.Rows[0]["valor"].ToString());
            INFORMACION_DE_LA_ATENCION_DEL_ASPIRANTE_O_TRABAJADOR = new ReportParameter("INFORMACION_DE_LA_ATENCION_DEL_ASPIRANTE_O_TRABAJADOR", TablaItem.Rows[1]["valor"].ToString());
            ANTECEDENTES_PERSONALES = new ReportParameter("ANTECEDENTES_PERSONALES", TablaItem.Rows[2]["valor"].ToString());
            ANTECEDENTES_FAMILIARES = new ReportParameter("ANTECEDENTES_FAMILIARES", TablaItem.Rows[3]["valor"].ToString());
            HABITOS = new ReportParameter("HABITOS", TablaItem.Rows[4]["valor"].ToString());
            INMUNIZACION = new ReportParameter("INMUNIZACION", TablaItem.Rows[5]["valor"].ToString());
            ACCIDENTE_LABORAL = new ReportParameter("ACCIDENTE_LABORAL", TablaItem.Rows[6]["valor"].ToString());
            ENFERMEDAD_PROFESIONAL = new ReportParameter("ENFERMEDAD_PROFESIONAL", TablaItem.Rows[7]["valor"].ToString());
            RIESGOS_OCUPACIONALES = new ReportParameter("RIESGOS_OCUPACIONALES", TablaItem.Rows[8]["valor"].ToString());
            INFORMACION_OCUPACIONAL = new ReportParameter("INFORMACION_OCUPACIONAL", TablaItem.Rows[9]["valor"].ToString());
            EXAMENES_PRACTICADOS = new ReportParameter("EXAMENES_PRACTICADOS", TablaItem.Rows[10]["valor"].ToString());
            ANTECEDENTES_GINECOLOGICOS = new ReportParameter("ANTECEDENTES_GINECOLOGICOS", TablaItem.Rows[11]["valor"].ToString());
            SIGNOS_VITALES = new ReportParameter("SIGNOS_VITALES", TablaItem.Rows[12]["valor"].ToString());
            EXAMEN_FISICO = new ReportParameter("EXAMEN_FISICO", TablaItem.Rows[13]["valor"].ToString());
            PRUEBA_DE_EQUILIBRIO = new ReportParameter("PRUEBA_DE_EQUILIBRIO", TablaItem.Rows[14]["valor"].ToString());
            REVISION_POR_SISTEMA = new ReportParameter("REVISION_POR_SISTEMA", TablaItem.Rows[15]["valor"].ToString());
            RECOMENDACIONES = new ReportParameter("RECOMENDACIONES", TablaItem.Rows[16]["valor"].ToString());

            DataTable nuevaTabla = new DataTable();
            nuevaTabla.Columns.Add("descripcion", typeof(ReportParameter));

            //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
            //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
            //                          LA ATENCIÓN DEL PACIENTE                         //
            //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
            //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
            //Consultamos los datos de la atención del paciente
            string Query = "SELECT dbo.EntradaHistoria.Entr_FechaEntrada, " +
                            "dbo.Concepto.Conc_Descripcion,              " +
                            "dbo.EntradaHistoria.Entr_Recomendacion,     " +
                            "dbo.EntradaHistoria.Entr_Reubicacion,       " +
                            "dbo.TipoExamen.TipoExam_Descripcion,        " +
                            "dbo.Enfasis.Enfa_Descripcion,               " +
                            "EntradaHistoria.Entr_IdPaciente,            " +
                            "dbo.EntradaHistoria.Ent_Estado,             " +
                            "dbo.EntradaHistoria.Ent_conceptoAptitud     " +
                            "FROM  dbo.EntradaHistoria INNER JOIN        " +
                            "dbo.TipoExamen ON dbo.EntradaHistoria." +
                            "Entr_TipoExamenCodigo =                     " +
                            "dbo.TipoExamen.TipoExam_Codigo AND          " +
                            "dbo.EntradaHistoria.Entr_TipoExamenCodigo = " +
                            "dbo.TipoExamen.TipoExam_Codigo INNER JOIN   " +
                            "dbo.Concepto ON dbo.EntradaHistoria.Entr_Concepto_Codigo " +
                            "= dbo.Concepto.Conc_Codigo INNER JOIN          " +
                            "dbo.Enfasis ON dbo.EntradaHistoria.Ent_Enfasis " +
                            "= dbo.Enfasis.Enfa_Codigo " +
                            "where [Entr_Numero] = " + NumeroAtencion;

            DataTable TablaDatos = new DataTable();
            TablaDatos = ObjConexion.LlenarTabla(Query);
            if (TablaDatos.Rows != null & TablaDatos.Rows.Count>0)
            {
                string IdPaciente = TablaDatos.Rows[0]["Entr_IdPaciente"].ToString();

                string Variable_FechaAtencion = TablaDatos.Rows[0]["Entr_FechaEntrada"].ToString().Substring(0,10);
                string Variable_TipoExamen = TablaDatos.Rows[0]["TipoExam_Descripcion"].ToString();
                string Variable_Enfasis = TablaDatos.Rows[0]["Enfa_Descripcion"].ToString();
                string Variable_Concepto = TablaDatos.Rows[0]["Conc_Descripcion"].ToString();
                string Variable_Reubicacion = TablaDatos.Rows[0]["Entr_Reubicacion"].ToString();
                string Variable_ConceptoAptitud = TablaDatos.Rows[0]["Ent_conceptoAptitud"].ToString();
                if (Variable_ConceptoAptitud == "")
                    Variable_ConceptoAptitud = "No Aplica";
                string Variable_Descripcion = TablaDatos.Rows[0]["Entr_Recomendacion"].ToString();
                if (Variable_Reubicacion == "True") Variable_Reubicacion = "SI";
                else Variable_Reubicacion = "NO";

                //Consultar Diagnosticos 
                Query = "SELECT	dbo.Diagnostico.Diag_Descripcion,        " +
                        "dbo.DiagnosticoPaciente.DiagPaci_NumeroHistoria " +
                        "FROM	dbo.DiagnosticoPaciente INNER JOIN       " +
                        "dbo.Diagnostico ON dbo.DiagnosticoPaciente." +
                        "DiagPaci_CodDiagnostico = dbo.Diagnostico.Diag_Codigo "+
                        "where DiagPaci_NumeroHistoria = " + NumeroAtencion;
                TablaDatos = new DataTable();
                TablaDatos = ObjConexion.LlenarTabla(Query);
                string Variable_Diagnostico = "Pendiente por definir";
                
                for (int i = 0; i < TablaDatos.Rows.Count; i++)
                {
                    string Valor = TablaDatos.Rows[i]["Diag_Descripcion"].ToString();
                    if (i == 0)
                        Variable_Diagnostico = Valor;
                    else
                        Variable_Diagnostico = Variable_Diagnostico + " - " + Valor;
                }

                if (TablaDatos.Rows.Count == 0)
                    Variable_Diagnostico = "SIN DIAGNOSTICO";
                
                ////Establecemos el valor de los parámetros de atenciòn
                FechaAtencion = new ReportParameter("FechaAtencion", Variable_FechaAtencion);
                TipoExamen = new ReportParameter("TipoExamen", Variable_TipoExamen);
                Enfasis = new ReportParameter("Enfasis", Variable_Enfasis);
                Reubicacion = new ReportParameter("Reubicacion", Variable_Reubicacion);
                Concepto = new ReportParameter("Concepto", Variable_Concepto);
                Diagnostico = new ReportParameter("Diagnostico", Variable_Diagnostico);
                ConceptoAptitud = new ReportParameter("ConceptoAptitud", Variable_ConceptoAptitud);
                Descripcion = new ReportParameter("Descripcion", Variable_Descripcion);

                //Consultamos los datos personales del paciente
                Query = "SELECT	dbo.Paciente.Pac_Nombre1+ ' ' + isnull(dbo.Paciente.Pac_Nombre2,'')                         " +
                    "		+' '+dbo.Paciente.Pac_Apellido1+ ' '+isnull(dbo.Paciente.Pac_Apellido2,'') As Nombres,      " +
                    "		dbo.Paciente.Pac_TipoIdentificacion, dbo.Paciente.Pac_Identificacion,                       " +
                    "		dbo.Paciente.Pac_FechaNacimiento, dbo.Genero.Gen_Descripcion,Pac_FechaNacimiento,                               " +
                    "		dbo.Paciente.Pac_CodCiudad, dbo.Paciente.Pac_Direccion,                               " +
                    "		dbo.Paciente.Pac_CodNivelEducativo, dbo.Paciente.Pac_CodProfesion,                         " +
                    "		dbo.TipoSangre.TipSan_Descripcion, dbo.Paciente.Pac_EstadoCivil,                            " +
                    "		dbo.EstadoCivil.EstCivil_Descripcion,Pac_Telefono,Pac_Foto                                           " +
                    "FROM	dbo.Paciente LEFT OUTER JOIN                                                                " +
                    "		dbo.EstadoCivil ON dbo.Paciente.Pac_EstadoCivil =                                           " +
                    "		dbo.EstadoCivil.EstCivil_Codigo LEFT OUTER JOIN                                             " +
                    "		dbo.TipoSangre ON dbo.Paciente.Pac_TipoSangre =                                             " +
                    "		dbo.TipoSangre.TipSan_Codigo LEFT OUTER JOIN                                                " +
                    "		dbo.Genero ON dbo.Paciente.Pac_CodGenero = dbo.Genero.Gen_Codigo                        " +
                    "WHERE	Pac_Identificacion='" + IdPaciente + "'";
                TablaDatos = new DataTable();
                TablaDatos = ObjConexion.LlenarTabla(Query);

                string Variable_NombreCompleto = TablaDatos.Rows[0]["Nombres"].ToString();

                string Variable_NumeroDocumento = 
                    TablaDatos.Rows[0]["Pac_TipoIdentificacion"].ToString() + ". " + 
                    TablaDatos.Rows[0]["Pac_Identificacion"].ToString();

                string Variable_FechaNacimiento = "Fecha de Nacimiento: "+ TablaDatos.Rows[0]["Pac_FechaNacimiento"].ToString().Substring(0,10);
                string Variable_Sexo = "Sexo: " + TablaDatos.Rows[0]["Gen_Descripcion"].ToString();
                //Cargar Imagen
                //HistoriaClinicaDataSet2.nul
                this.SP_DatosPacienteTableAdapter.Fill(this.HistoriaClinicaDataSet2.SP_DatosPaciente,  IdPaciente);
                //Establecemos el valor de los parámetros de la informacion personal
                NombreCompleto = new ReportParameter("NombreCompleto", Variable_NombreCompleto);
                NumeroDocumento = new ReportParameter("NumeroDocumento", Variable_NumeroDocumento);
                FechaNacimiento = new ReportParameter("FechaNacimiento", Variable_FechaNacimiento);
                Sexo = new ReportParameter("Sexo", Variable_Sexo);

                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
                //                          INFORMACIÓN OCUPACIONAL                          //
                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
                //Se consultan la información ocupacional 
                Query = "SELECT  dbo.Empresa.Empre_RazonSocial,                      "+
		                "dbo.Cargo.Carg_Descripcion,                                 "+
		                "dbo.InformacionOcupacional.InfOcu_Jornada,                  "+
		                "dbo.InformacionOcupacional.InfOcu_Area,                     "+
		                "dbo.InformacionOcupacional.InfOcu_ElementoProte,            "+
		                "dbo.InformacionOcupacional.InfOcu_DescripcionFunciones,     "+
		                "dbo.InformacionOcupacional.InfOcu_MateriaPrima,             "+
		                "dbo.InformacionOcupacional.InfOcu_Herramienta,              "+
		                "dbo.InformacionOcupacional.InfOcu_Maquinaria,               "+
		                "dbo.InformacionOcupacional.InfOcu_FechaIngreso,             "+
		                "dbo.InformacionOcupacional.InfOcu_FechaCargoActual          "+
                        "FROM    dbo.InformacionOcupacional INNER JOIN               "+
		                "dbo.Empresa ON dbo.InformacionOcupacional.InfOcu_CodEmpresa "+
		                "= dbo.Empresa.Empr_Codigo INNER JOIN                        "+
		                "dbo.Cargo ON dbo.InformacionOcupacional.InfOcu_CodOcupacion "+
		                "= dbo.Cargo.Carg_Codigo                                     "+
                        "where dbo.InformacionOcupacional.InfOcu_Entrada_Numero = " + NumeroAtencion;
                TablaDatos = new DataTable();
                TablaDatos = ObjConexion.LlenarTabla(Query);
                //Se asigna el valor a las variables 
                string Variable_Empresa     = TablaDatos.Rows[0]["Empre_RazonSocial"            ].ToString();
                string Variable_Cargo       = TablaDatos.Rows[0]["Carg_Descripcion"             ].ToString();
                string Variable_Jornada     = TablaDatos.Rows[0]["InfOcu_Jornada"               ].ToString();
                string Variable_Area        = TablaDatos.Rows[0]["InfOcu_Area"                  ].ToString();
                string Variable_Elementos   = TablaDatos.Rows[0]["InfOcu_ElementoProte"         ].ToString();
                string Variable_Funciones   = TablaDatos.Rows[0]["InfOcu_DescripcionFunciones"  ].ToString();
                string Variable_Materia     = TablaDatos.Rows[0]["InfOcu_MateriaPrima"          ].ToString();
                string Variable_Herramientas = TablaDatos.Rows[0]["InfOcu_Herramienta"          ].ToString();
                string Variable_Maquinaria  = TablaDatos.Rows[0]["InfOcu_Maquinaria"            ].ToString();
                string Variable_FechaCargo  = TablaDatos.Rows[0]["InfOcu_FechaCargoActual"      ].ToString().Substring(0,10);

                //Establecemos el valor de los parámetros de la informacion personal
                Empresa     = new ReportParameter("Empresa", Variable_Empresa);
                Cargo       = new ReportParameter("Cargo", Variable_Cargo);
                Jornada     = new ReportParameter("Jornada", Variable_Jornada);
                Area        = new ReportParameter("Area", Variable_Area);
                Elementos   = new ReportParameter("Elementos", Variable_Elementos);
                Funciones    = new ReportParameter("Funciones", Variable_Funciones);
                Materia      = new ReportParameter("Materia", Variable_Materia);
                Herramientas = new ReportParameter("Herramientas", Variable_Herramientas);
                Maquinaria   = new ReportParameter("Maquinaria", Variable_Maquinaria);
                FechaCargo   = new ReportParameter("FechaCargo", Variable_FechaCargo);

                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
                //                          ANTECEDENTES GINECOLOGICOS                       //
                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://

                //Se consultan la información Ginecologica 
                Query = "SELECT [CicMens_Entrada_Numero] "+
                        ",[CicMens_FechaUltimaRegla]     "+
                        ",[CicMens_HijosSanos]           "+
                        ",[CicMens_Gestaciones]          "+
                        ",[CicMens_Partos]               "+
                        ",[CicMens_Abortos]              "+
                        ",[CicMens_Hijos]                "+
                        ",[CicMens_ResultadoCitologia]   "+
                        ",[CicMens_Planificacion]        "+
                        ",[CicMens_Edadmenopausia]       "+
                        ",[CicMens_Edadmenarca]          "+
                        ",[CicMens_metodo]               "+
                        "FROM [dbo].[CicloMenstrual]     "+
                        "where CicMens_Entrada_Numero = " + NumeroAtencion;
                TablaDatos = new DataTable();
                TablaDatos = ObjConexion.LlenarTabla(Query);

                //Variables
                if (TablaDatos.Rows.Count>0)
                {
                    string Variable_FechaRegla =        TablaDatos.Rows[0]["CicMens_FechaUltimaRegla"].ToString().Substring(0, 10);
                    string Variable_Hijos =             TablaDatos.Rows[0]["CicMens_Hijos"].ToString();
                    string Variable_Partos =            TablaDatos.Rows[0]["CicMens_Partos"].ToString();
                    string Variable_Abortos =           TablaDatos.Rows[0]["CicMens_Abortos"].ToString();
                    string Variable_Sanos =             TablaDatos.Rows[0]["CicMens_HijosSanos"].ToString();
                    string Variable_Gestaciones =       TablaDatos.Rows[0]["CicMens_Gestaciones"].ToString();
                    string Variable_Menopausia =        TablaDatos.Rows[0]["CicMens_Edadmenopausia"].ToString();
                    string Variable_Menarca =           TablaDatos.Rows[0]["CicMens_Edadmenarca"].ToString();
                    string Variable_Citologia =         TablaDatos.Rows[0]["CicMens_ResultadoCitologia"].ToString();
                    string Variable_Planificacion =     TablaDatos.Rows[0]["CicMens_Planificacion"].ToString();
                    if (Variable_Planificacion == "False")
                        Variable_Planificacion = "NO";
                    else
                        Variable_Planificacion = TablaDatos.Rows[0]["CicMens_metodo"].ToString();
                    //Establecemos el valor de los parámetros de Gestaciones
                    if (Variable_Sexo=="Sexo: Hombre")
                    {
                        Variable_FechaRegla = "NO APLICA";
                        //Variable_Hijos =        "NO APLICA";
                        Variable_Partos = "NO APLICA";
                        Variable_Abortos =      "NO APLICA";
                        //Variable_Sanos =        "NO APLICA";
                        Variable_Gestaciones =  "NO APLICA";
                        Variable_Menopausia =   "NO APLICA";
                        Variable_Menarca =      "NO APLICA";
                        Variable_Citologia =    "NO APLICA";
                        //Variable_Planificacion ="NO APLICA";
                    }
                    FechaRegla = new ReportParameter("FechaRegla", Variable_FechaRegla);
                    Hijos = new ReportParameter("Hijos", Variable_Hijos);
                    Partos = new ReportParameter("Partos", Variable_Partos);
                    Abortos = new ReportParameter("Abortos", Variable_Abortos);
                    Sanos = new ReportParameter("Sanos", Variable_Sanos);
                    Gestaciones = new ReportParameter("Gestaciones", Variable_Gestaciones);
                    Menopausia = new ReportParameter("Menopausia", Variable_Menopausia);
                    Menarca = new ReportParameter("Menarca", Variable_Menarca);
                    Citologia = new ReportParameter("Citologia", Variable_Citologia);
                    Planificacion = new ReportParameter("Planificacion", Variable_Planificacion);
                }
                
                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
                //                          SIGNOS VITALES                                   //
                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://

                Query="SELECT [ExaFisi_Entrada_Numero] "+
                        ",[ExaFisi_PresionArterial]    "+
                        ",[ExaFisi_FrecuenciaCardiaca] "+
                        ",[ExaFisi_Lateracidad]        "+
                        ",[ExaFisi_Peso]               "+
                        ",[ExaFisi_Talla]              "+
                        ",[ExaFisi_PerimetroCintura]   "+
                        ",[ExaFisi_IMC]                "+
                        ",[ExaFisi_Interpretacion]     "+
                        "FROM [dbo].[ExamenFisico]     "+
                        "where ExaFisi_Entrada_Numero = " + NumeroAtencion;

                TablaDatos = new DataTable();
                TablaDatos = ObjConexion.LlenarTabla(Query);

                string Variable_Talla           = TablaDatos.Rows[0]["ExaFisi_Talla"].ToString();
                string Variable_Peso            = TablaDatos.Rows[0]["ExaFisi_Peso"].ToString();
                string Variable_IMC             = TablaDatos.Rows[0]["ExaFisi_IMC"].ToString();
                string Variable_Lateralidad     = TablaDatos.Rows[0]["ExaFisi_Lateracidad"].ToString();
                string Variable_Cardiaca        = TablaDatos.Rows[0]["ExaFisi_FrecuenciaCardiaca"].ToString();
                string Variable_Arterial        = TablaDatos.Rows[0]["ExaFisi_PresionArterial"].ToString();
                string Variable_Cintura         = TablaDatos.Rows[0]["ExaFisi_PerimetroCintura"].ToString();
                string Variable_Interpretacion = TablaDatos.Rows[0]["ExaFisi_Interpretacion"].ToString();

                //Establecemos el valor de los parámetros de Gestaciones
                Talla = new ReportParameter("Talla", Variable_Talla);
                Peso = new ReportParameter("Peso", Variable_Peso);
                IMC = new ReportParameter("IMC", Variable_IMC);
                Lateralidad = new ReportParameter("Lateralidad", Variable_Lateralidad);
                Cardiaca = new ReportParameter("Cardiaca", Variable_Cardiaca);
                Arterial = new ReportParameter("Arterial", Variable_Arterial);
                Cintura = new ReportParameter("Cintura", Variable_Cintura);
                Interpretacion = new ReportParameter("Interpretacion", Variable_Interpretacion);

                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
                //            PRUEBBA DE EQUILIBRIO (MARCHA, REFLEJO Y PIEL)                 //
                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
                //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
                Query="SELECT [EqiPa_Cidigo]            "+
                        ",[EqiPa_Equilibro]             "+
                        ",[EqiPa_HistoriaNumero]        "+
                        ",[EqiPa_Estado]                "+
                        ",[EqiPa_Reflejos]              "+
                        ",[EqiPa_Marcha]                "+
                        ",[EqiPa_Piel]                  "+
                        "FROM [dbo].[EquilibroPaciente] "+
                        "where EqiPa_HistoriaNumero = " + NumeroAtencion;
                TablaDatos = new DataTable();
                TablaDatos = ObjConexion.LlenarTabla(Query);

                string Variable_Marcha = TablaDatos.Rows[0]["EqiPa_Marcha"].ToString();
                string Variable_Reflejo = TablaDatos.Rows[0]["EqiPa_Reflejos"].ToString();
                string Variable_Piel = TablaDatos.Rows[0]["EqiPa_Piel"].ToString();

                //Establecemos el valor de los parámetros de Gestaciones
                Marcha  = new ReportParameter("Marcha", Variable_Marcha);
                Reflejo = new ReportParameter("Reflejo", Variable_Reflejo);
                Piel    = new ReportParameter("Piel", Variable_Piel);

                //Se envian los parametros al reporte 
                //--(DATOS DE LA ATENCIÓN);             
                this.reportViewer1.LocalReport.SetParameters(FechaAtencion);
                this.reportViewer1.LocalReport.SetParameters(Concepto);
                this.reportViewer1.LocalReport.SetParameters(TipoExamen);
                this.reportViewer1.LocalReport.SetParameters(Enfasis);
                this.reportViewer1.LocalReport.SetParameters(ConceptoAptitud);
                this.reportViewer1.LocalReport.SetParameters(Reubicacion);
                this.reportViewer1.LocalReport.SetParameters(Diagnostico);
                this.reportViewer1.LocalReport.SetParameters(Descripcion);

                //--(DATOS PERSONALES)
                this.reportViewer1.LocalReport.SetParameters(NombreCompleto);
                this.reportViewer1.LocalReport.SetParameters(NumeroDocumento);
                this.reportViewer1.LocalReport.SetParameters(FechaNacimiento);
                this.reportViewer1.LocalReport.SetParameters(Sexo);

                //--(DATOS INFORMACION OCUPACIONAL)
                this.reportViewer1.LocalReport.SetParameters(Empresa);
                this.reportViewer1.LocalReport.SetParameters(Cargo);
                this.reportViewer1.LocalReport.SetParameters(Jornada);
                this.reportViewer1.LocalReport.SetParameters(Area);
                this.reportViewer1.LocalReport.SetParameters(Elementos);
                this.reportViewer1.LocalReport.SetParameters(Funciones);
                this.reportViewer1.LocalReport.SetParameters(Materia);
                this.reportViewer1.LocalReport.SetParameters(Herramientas);
                this.reportViewer1.LocalReport.SetParameters(Maquinaria);
                this.reportViewer1.LocalReport.SetParameters(FechaCargo);

                //--(DATOS ANTECEDENTES GINECOLOGICOS)
                this.reportViewer1.LocalReport.SetParameters(FechaRegla);
                this.reportViewer1.LocalReport.SetParameters(Hijos);
                this.reportViewer1.LocalReport.SetParameters(Partos);
                this.reportViewer1.LocalReport.SetParameters(Abortos);
                this.reportViewer1.LocalReport.SetParameters(Sanos);
                this.reportViewer1.LocalReport.SetParameters(Gestaciones);
                this.reportViewer1.LocalReport.SetParameters(Menopausia);
                this.reportViewer1.LocalReport.SetParameters(Menarca);
                this.reportViewer1.LocalReport.SetParameters(Citologia);
                this.reportViewer1.LocalReport.SetParameters(Planificacion);

                //--(DATOS SIGNOS VITALES)
                this.reportViewer1.LocalReport.SetParameters(Talla);
                this.reportViewer1.LocalReport.SetParameters(Peso);
                this.reportViewer1.LocalReport.SetParameters(IMC);
                this.reportViewer1.LocalReport.SetParameters(Lateralidad);
                this.reportViewer1.LocalReport.SetParameters(Cardiaca);
                this.reportViewer1.LocalReport.SetParameters(Arterial);
                this.reportViewer1.LocalReport.SetParameters(Cintura);
                this.reportViewer1.LocalReport.SetParameters(Interpretacion);

                //--(DATOS PRUEBA DE EQUILIBRIO)
                this.reportViewer1.LocalReport.SetParameters(Marcha);
                this.reportViewer1.LocalReport.SetParameters(Reflejo);
                this.reportViewer1.LocalReport.SetParameters(Piel);

                //--(DATOS PRUEBA DE EQUILIBRIO)
                this.reportViewer1.LocalReport.SetParameters(INFORMACION_DEL_ASPIRANTE_O_TRABAJADOR);
                this.reportViewer1.LocalReport.SetParameters(INFORMACION_DE_LA_ATENCION_DEL_ASPIRANTE_O_TRABAJADOR);
                this.reportViewer1.LocalReport.SetParameters(ANTECEDENTES_PERSONALES);
                this.reportViewer1.LocalReport.SetParameters(ANTECEDENTES_FAMILIARES);
                this.reportViewer1.LocalReport.SetParameters(HABITOS);
                this.reportViewer1.LocalReport.SetParameters(INMUNIZACION);
                this.reportViewer1.LocalReport.SetParameters(ACCIDENTE_LABORAL);
                this.reportViewer1.LocalReport.SetParameters(ENFERMEDAD_PROFESIONAL);
                this.reportViewer1.LocalReport.SetParameters(RIESGOS_OCUPACIONALES);
                this.reportViewer1.LocalReport.SetParameters(INFORMACION_OCUPACIONAL);
                this.reportViewer1.LocalReport.SetParameters(EXAMENES_PRACTICADOS);
                this.reportViewer1.LocalReport.SetParameters(ANTECEDENTES_GINECOLOGICOS);
                this.reportViewer1.LocalReport.SetParameters(EXAMEN_FISICO);
                this.reportViewer1.LocalReport.SetParameters(SIGNOS_VITALES);
                this.reportViewer1.LocalReport.SetParameters(PRUEBA_DE_EQUILIBRIO);
                this.reportViewer1.LocalReport.SetParameters(REVISION_POR_SISTEMA);
                this.reportViewer1.LocalReport.SetParameters(RECOMENDACIONES);
                this.reportViewer1.LocalReport.DataSources.Add(r);
                this.reportViewer1.RefreshReport();

                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                //Seleccionamos el zoom que deseamos utilizar. En este caso un 100%
                reportViewer1.ZoomPercent = 100;

            }
            else
            MessageBox.Show("No hay resoltados para la informacion de la atencion");
        }
    }
}
