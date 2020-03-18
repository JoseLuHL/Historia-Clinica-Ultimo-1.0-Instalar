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
    public partial class FrmImprimirReporte : Form
    {
        public FrmImprimirReporte()
        {
            InitializeComponent();
        }
        //ESTILOS DEL DGV
        public void EstilosDgv2(DataGridView DGV)
        {
            DGV.DefaultCellStyle.Font = new Font("Verdana", 11);
            Font prFont = new Font("Verdana", 11, FontStyle.Bold);
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                if (i % 2 == 0)
                    DGV.Rows[i].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                else
                    DGV.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
            for (int i = 0; i < DGV.Columns.Count; i++)
            {
                DGV.Columns[i].HeaderCell.Style.Font = prFont;
                DGV.Columns[i].HeaderCell.Style.ForeColor = Color.White;
                DGV.Columns[i].HeaderCell.Style.BackColor = Color.DimGray;
                //DGV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //DGV.AutoResizeColumns();
            DGV.EnableHeadersVisualStyles = false;
        }
        //ESTILOS DEL DGV
        public void EstilosDgv(DataGridView DGV)
        {
            DGV.DefaultCellStyle.Font = new Font("Verdana", 11);
            Font prFont = new Font("Verdana", 11, FontStyle.Bold);
            for (int i = 0; i < DGV.Columns.Count; i++)
            {
                DGV.Columns[i].HeaderCell.Style.Font = prFont;
                DGV.Columns[i].HeaderCell.Style.ForeColor = Color.White;
                DGV.Columns[i].HeaderCell.Style.BackColor = Color.DimGray;
            }
            DGV.EnableHeadersVisualStyles = false;
        }

        ClsSqlServer NuevoSql = new ClsSqlServer();
        public void BuscarHistoria()
        {
            if (TxtCriterio.Text != "")
            {
                DgvDatos.Rows.Clear();
                NuevoSql.CadenaCnn = Historia_Clinica.Conexion.CadenaConexion.cadena();
                NuevoSql.Conectar();
                string Query = "SELECT  CONCAT(dbo.Paciente.Pac_Nombre1,+' '+dbo.Paciente.Pac_Nombre2,+' '+dbo.Paciente.Pac_Apellido1,+' '+dbo.Paciente.Pac_Apellido2) As Nombres,                                " +
                                "		dbo.Paciente.Pac_Identificacion, dbo.EntradaHistoria.Entr_Numero,Entr_FechaEntrada, 'Ver' As Ver, 'Imprimir' As Imprimir,(SELECT Enfasis.Enfa_Descripcion from Enfasis where Enfasis.Enfa_Codigo= dbo.EntradaHistoria.Ent_Enfasis) as Enfasis                                                                       " +
                                "FROM   dbo.EntradaHistoria INNER JOIN                                                                                                                                 " +
                                "       dbo.Paciente ON dbo.EntradaHistoria.Entr_IdPaciente = dbo.Paciente.Pac_Identificacion                                                                      " +
                                "WHERE  CONCAT(dbo.Paciente.Pac_Nombre1,dbo.Paciente.Pac_Nombre2,dbo.Paciente.Pac_Apellido1,dbo.Paciente.Pac_Apellido2,Pac_Identificacion,Entr_Numero) LIKE" + "'%" + TxtCriterio.Text + "%'  And Entr_FechaEntrada between  '" + DtDesde.Text + "'and'" + DtHasta.Text + "'";
                DataTable Tabla = new DataTable();
                Tabla = NuevoSql.LlenarTabla(Query);
                if (Tabla.Rows.Count > 0)
                {
                    for (int i = 0; i < Tabla.Rows.Count; i++)
                    {
                        DgvDatos.Rows.Add(Tabla.Rows[i]["Nombres"].ToString(), Tabla.Rows[i]["Pac_Identificacion"].ToString(), Tabla.Rows[i]["Entr_Numero"].ToString(), Tabla.Rows[i]["Entr_FechaEntrada"],Tabla.Rows[i]["Enfasis"], Tabla.Rows[i]["Ver"].ToString(), Tabla.Rows[i]["Imprimir"].ToString());
                        DgvDatos.Rows[i].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    }
                    Tabla = null;
                }
                else
                    MessageBox.Show("No se han encontrado resultados", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EstilosDgv2(DgvDatos);

            }
            else
                MessageBox.Show("No ha ingresado ningun caracter", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
        }
        private void Btn_LupaBuscar_Click(object sender, EventArgs e)
        {
            BuscarHistoria();
        }

        private void FrmImprimirReporte_Load(object sender, EventArgs e)
        {
            EstilosDgv2(DgvDatos);
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
        private void DgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            DataTable TablaPaciente = new DataTable();
                //SE VERIFIVA QUE SE HAYA SELECCIONADO UNA COLUMNA
            if (e.ColumnIndex>0)
            {
                //PREGUNTAMOS QUE SERA LA COLUMNA DE IMPRIMIR
                if (DgvDatos.Columns[e.ColumnIndex].HeaderText=="********")
                {
                    string Documento = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColDocumento"].Value.ToString();
                    //SE REALIZAN LAS RESPECTIVAS COLUMNAS Y SE LLENAN LAS TABLAS CON LA INFORMACIÓN QUE SE REQUIERE PARA EL REPORTE
                    string Query = "SELECT	dbo.Paciente.Pac_Nombre1+ ' ' + isnull(dbo.Paciente.Pac_Nombre2,'')                         " +
                                        "		+' '+dbo.Paciente.Pac_Apellido1+ ' '+isnull(dbo.Paciente.Pac_Apellido2,'') As Nombres,      " +
                                        "		dbo.Paciente.Pac_TipoIdentificacion, dbo.Paciente.Pac_TipoIdentificacion+' '+dbo.Paciente.Pac_Identificacion As Pac_Identificacion,                       " +
                                        "		dbo.Paciente.Pac_FechaNacimiento, dbo.Genero.Gen_Descripcion,                               " +
                                        "		dbo.Paciente.Pac_LugarNacimiento, dbo.Paciente.Pac_Direccion,                               " +
                                        "		dbo.Paciente.Pac_NivelEducacion, dbo.Paciente.Pac_Profesion_Codigo,                         " +
                                        "		dbo.TipoSangre.TipSan_Descripcion, dbo.Paciente.Pac_EstadoCivil,                            " +
                                        "		dbo.EstadoCivil.EstCivil_Descripcion,Pac_Telefono                                           " +
                                        "FROM	dbo.Paciente LEFT OUTER JOIN                                                                " +
                                        "		dbo.EstadoCivil ON dbo.Paciente.Pac_EstadoCivil =                                           " +
                                        "		dbo.EstadoCivil.EstCivil_Codigo LEFT OUTER JOIN                                             " +
                                        "		dbo.TipoSangre ON dbo.Paciente.Pac_TipoSangre =                                             " +
                                        "		dbo.TipoSangre.TipSan_Codigo LEFT OUTER JOIN                                                " +
                                        "		dbo.Genero ON dbo.Paciente.Pac_Genero_Codigo = dbo.Genero.Gen_Codigo                        " +
                                        "WHERE	Pac_Identificacion='" + Documento + "'";
                    TablaPaciente = NuevoSql.LlenarTabla(Query);                        
                    //FIN PACIENTE

                    string NumeroEntrada = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColNumeroHistoria"].Value.ToString();
                    string Enfasis = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColEnfasis"].Value.ToString();
                    string Fecha = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColFecha"].Value.ToString(); 
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
            cry.SetParameterValue("NombreCompleto", TablaPaciente.Rows[0]["Nombres"]);
            cry.SetParameterValue("Cargo", TablaPaciente.Rows[0]["Pac_Profesion_Codigo"]);
            cry.SetParameterValue("Documento", TablaPaciente.Rows[0]["Pac_Identificacion"]);
            cry.SetParameterValue("Edad", edad);
            cry.SetParameterValue("Genero", TablaPaciente.Rows[0]["Gen_Descripcion"]);
            cry.SetParameterValue("Telefono", TablaPaciente.Rows[0]["Pac_Telefono"].ToString());
            cry.SetParameterValue("EPS", "NOMBRE DE LA EPS");
            cry.SetParameterValue("ARL", "NOMBRE DE LA ARL");
            cry.SetParameterValue("Ciudad", Configuracion.LugarExamen);
            cry.SetParameterValue("nit", Configuracion.Nit);
            cry.SetParameterValue("piepagina", Configuracion.PiePagina);
            if (examenes.Trim()=="")
            {
                cry.SetParameterValue("ExamenesPracticados", "NO APLICA");
   
            }
                    else                
                cry.SetParameterValue("ExamenesPracticados", examenes);

            cry.SetParameterValue("@NumeroEntrada", Convert.ToInt32(NumeroEntrada));
            cry.SetParameterValue("Enfasis", Enfasis);
            cry.SetParameterValue("FechaExamen", Fecha);
            //cry.DataSourceConnections = NuevoSql.conexion;

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
            Query = "SELECT InfOcu_Empresa, InfOcu_Entrada_Numero FROM dbo.InformacionOcupacional WHERE InfOcu_Entrada_Numero=" + NumeroEntrada;
                    TablaPaciente = NuevoSql.LlenarTabla(Query);
                    cry.SetParameterValue("NombreEmpresa", TablaPaciente.Rows[0]["InfOcu_Empresa"].ToString());
                    //cry.SetParameterValue("nitempresa", TablaPaciente.Rows[0]["InfOcu_Nit"].ToString());
                    cry.SetParameterValue("Ciudad", Configuracion.LugarExamen);
                    cry.SetParameterValue("nit", Configuracion.Nit);
                    cry.SetParameterValue("tel", Configuracion.Telefono);
                    cry.SetParameterValue("direccion", Configuracion.Direccion);
                    cry.SetParameterValue("piepagina", Configuracion.PiePagina);

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

                    //cry.DataSourceConnections[0].SetConnection("192.168.1.10", "2019";
                    //cry.Load()
                    TablaPaciente = null;
                    TablaExamenes = null;
                    //SE VISUALIZA EL REPORTE
            f.crystalReportViewer1.ReportSource = cry;
            f.Show();
                }
            }

            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("No se puede generar el reporte","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            //}
            try
            {
                if (DgvDatos.Columns[e.ColumnIndex].HeaderText == "-----")
                {
                    HistoriaPaciente f = new HistoriaPaciente();
                    f.Nhistoria = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColNumeroHistoria"].Value.ToString();
                    f.TxtDocumento.Text = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColDocumento"].Value.ToString();
                    f.Show();
                }
            }
            catch (Exception)
            { }
            
        }

        private void TxtCriterio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                BuscarHistoria();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmGenerarInforme fr = new FrmGenerarInforme();
            fr.ShowDialog();
        }
    }
}
