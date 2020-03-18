using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Historia_Clinica.Reportes;
namespace Historia_Clinica
{
    public partial class FrmGenerarInforme : Form
    {
        public FrmGenerarInforme()
        {
            InitializeComponent();
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
                    DGV.Rows[i].DefaultCellStyle.BackColor = Color.LightSteelBlue;
            }
            //DGV.AutoResizeColumns();
            DGV.EnableHeadersVisualStyles = false;
        }
        ClsSqlServer ObjServer = new ClsSqlServer();
        private void FrmEnsayo_Load(object sender, EventArgs e)
        {
            DgvDatosColDocumento.HeaderText = "";
            DgvDatosColNombres.HeaderText = "";
            //Establecer conexión  con el Gestor.
            ObjServer.CadenaCnn = Conexion.CadenaConexion.cadena();
            ObjServer.Conectar();
            //----Fin Establecer conexión-----

            //Cargo los datos que tendra el combobox de las empresas
            CboEmpresa.DataSource = ObjServer.LlenarTabla("SELECT [Empr_Codigo] As Codigo,[Empre_Nit] As Nit,[Empre_RazonSocial] As Descripcion  FROM [dbo].[Empresa] ORDER BY Descripcion");
            CboEmpresa.DisplayMember = "Descripcion";
            CboEmpresa.ValueMember = "Codigo";
            //
            EstilosDgv(DgvDatos);
        }
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

        public void BuscarHistoriaAtendida()
        {
            try
            {
                DgvPacientes.Rows.Clear();
                ObjServer.CadenaCnn = Historia_Clinica.Conexion.CadenaConexion.cadena();
                ObjServer.Conectar();

                string Query = "SELECT DISTINCT dbo.Paciente.Pac_Identificacion, CONCAT(dbo.Paciente.Pac_Nombre1,' ',dbo.Paciente.Pac_Nombre2,' ',        				            " +
                                "dbo.Paciente.Pac_Apellido1,' ',dbo.Paciente.Pac_Apellido2) AS nombres,                                             " +
                                "dbo.Paciente.Pac_TipoIdentificacion,  			                                    " +
                                "dbo.Paciente.Pac_FechaNacimiento, dbo.TipoExamen.TipoExam_Descripcion, 		                                    " +
                                "dbo.Enfasis.Enfa_Descripcion, dbo.Genero.Gen_Codigo, dbo.Genero.Gen_Descripcion, 	" +
                                "dbo.EntradaHistoria.Entr_FechaEntrada			                                    " +
                                "FROM	dbo.TipoExamen INNER JOIN	dbo.EntradaHistoria ON dbo.TipoExamen.TipoExam_Codigo = 						" +
                                "dbo.EntradaHistoria.Entr_TipoExamenCodigo AND 									                                    " +
                                "dbo.TipoExamen.TipoExam_Codigo = dbo.EntradaHistoria.Entr_TipoExamenCodigo 	                                    " +
                                "INNER JOIN  dbo.Enfasis ON dbo.EntradaHistoria.Ent_Enfasis = dbo.Enfasis.Enfa_Codigo 		                        " +
                                "INNER JOIN	dbo.Paciente INNER JOIN  dbo.Genero ON dbo.Paciente.Pac_CodGenero = dbo.Genero.Gen_Codigo ON 		" +
                                "dbo.EntradaHistoria.Entr_IdPaciente = dbo.Paciente.Pac_Identificacion AND 	                                    " +
                                "dbo.EntradaHistoria.Entr_IdPaciente = dbo.Paciente.Pac_Identificacion		                                    " +
                                "INNER JOIN 	dbo.InformacionOcupacional ON dbo.EntradaHistoria.Entr_Numero = dbo.InformacionOcupacional.InfOcu_Entrada_Numero INNER JOIN "+
								"dbo.Empresa ON dbo.InformacionOcupacional.InfOcu_CodEmpresa = dbo.Empresa.Empr_Codigo "+
                                "where	 Entr_FechaEntrada BETWEEN '" + DtDesde.Text + "'" + " AND '" + DtHasta.Text + "' AND  dbo.InformacionOcupacional.InfOcu_CodEmpresa=" + CboEmpresa.SelectedValue + "  ORDER BY nombres";
                DataTable Tabla = new DataTable();

                Tabla = ObjServer.LlenarTabla(Query);
                //ObjServer.(DgvDatos);
                EstilosDgv2(DgvPacientes);
                if (Tabla.Rows.Count > 0)
                {
                    for (int i = 0; i < Tabla.Rows.Count; i++)
                    {
                        DgvPacientes.Rows.Add((i + 1).ToString(), Tabla.Rows[i]["Pac_Identificacion"].ToString(), Tabla.Rows[i]["Pac_Identificacion"].ToString(), Tabla.Rows[i]["nombres"].ToString(), DateTime.Now.Year - Convert.ToDateTime(Tabla.Rows[i]["Pac_FechaNacimiento"]).Year + " años", Tabla.Rows[i]["Gen_Descripcion"].ToString(), Tabla.Rows[i]["Entr_FechaEntrada"].ToString().Substring(0, 10), Tabla.Rows[i]["TipoExam_Descripcion"].ToString(), Tabla.Rows[i]["Enfa_Descripcion"].ToString(), Tabla.Rows[i]["Entr_FechaEntrada"].ToString());
                        //if (Tabla.Rows[i]["Ent_Estado"].ToString() == "True")
                        //    DgvDatos.Rows[i].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                        //else
                        //{
                        //    DgvDatos.Rows[i].DefaultCellStyle.BackColor = Color.SteelBlue;
                        //    DgvDatos.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        //}
                    }
                    //DgvDatosColatendido.Visible = true;
                    //DgvDatosColImprimir.Visible = true;
                    //DgvDatosColNumeroHistoria.Visible = true;
                    //Tabla = null;
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
                
            }
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            string CodEmpresa= CboEmpresa.SelectedValue.ToString();
            string FechaDesde=       DtDesde.Value.ToShortDateString();
            string FechaHasta=   DtHasta.Value.ToShortDateString();
            DgvDatos.Rows.Clear();

            if (RdbDiagnostico.Checked)
            {
                string Sql = "		SELECT dbo.Diagnostico.Diag_Descripcion,                                                  " +
                                "	   COUNT(dbo.DiagnosticoPaciente.DiagPaci_CodDiagnostico) AS Expr2,                       " +
                                "	   dbo.Diagnostico.Diag_Codigo, dbo.Empresa.Empre_Nit, dbo.Empresa.Empre_RazonSocial      " +
                                " FROM   dbo.Diagnostico INNER JOIN                                                            " +
                                "	   dbo.DiagnosticoPaciente ON dbo.Diagnostico.Diag_Codigo =                               " +
                                "	   dbo.DiagnosticoPaciente.DiagPaci_CodDiagnostico INNER JOIN                             " +
                                "	   dbo.EntradaHistoria ON dbo.DiagnosticoPaciente.DiagPaci_NumeroHistoria =               " +
                                "	   dbo.EntradaHistoria.Entr_Numero INNER JOIN                                             " +
                                "	   dbo.InformacionOcupacional ON dbo.EntradaHistoria.Entr_Numero =                        " +
                                "	   dbo.InformacionOcupacional.InfOcu_Entrada_Numero INNER JOIN                            " +
                                "	   dbo.Empresa ON dbo.InformacionOcupacional.InfOcu_CodEmpresa = dbo.Empresa.Empr_Codigo  " +
                                "		WHERE EntradaHistoria.Entr_FechaEntrada between '" + FechaDesde + "' and  '" + FechaHasta + "'" +
                                " and dbo.Empresa.Empr_Codigo="+CodEmpresa +" " +                                                      
                                " GROUP BY dbo.Diagnostico.Diag_Descripcion, dbo.Diagnostico.Diag_Codigo,                      " +
                                "	   dbo.Empresa.Empre_Nit, dbo.Empresa.Empre_RazonSocial";
                
                DataTable TablaDiagnistico = new DataTable();
                TablaDiagnistico = ObjServer.LlenarTabla(Sql);

                DgvDatosColDocumento.HeaderText = "Diagnóstico";
                DgvDatosColNombres.HeaderText = "N° de Pacientes";
                if (TablaDiagnistico.Rows.Count>0)
                {
                    for (int i = 0; i < TablaDiagnistico.Rows.Count; i++)
			            {
                            DgvDatos.Rows.Add((i+1).ToString(), TablaDiagnistico.Rows[i]["Diag_Descripcion"], TablaDiagnistico.Rows[i]["Expr2"]);
                        }
                }
                EstilosDgv(DgvDatos);

            }
            else
            {
                string Sql = "SELECT COUNT(dbo.InformacionOcupacional.InfOcu_CodOcupacion) AS [NÚMERO DE PACIENTES], " +
                                "		dbo.Cargo.Carg_Descripcion AS CARGO, dbo.Empresa.Empre_RazonSocial AS EMPRESA " +
                                "FROM	dbo.EntradaHistoria INNER JOIN                                                " +
                                "		dbo.InformacionOcupacional ON                                                 " +
                                "		dbo.EntradaHistoria.Entr_Numero =                                             " +
                                "		dbo.InformacionOcupacional.InfOcu_Entrada_Numero                              " +
                                "		INNER JOIN dbo.Empresa ON                                                     " +
                                "		dbo.InformacionOcupacional.InfOcu_CodEmpresa =                                " +
                                "		dbo.Empresa.Empr_Codigo INNER JOIN                                            " +
                                "		dbo.Cargo ON dbo.InformacionOcupacional.InfOcu_CodOcupacion =                 " +
                                "		dbo.Cargo.Carg_Codigo                                                         " +
                                "		WHERE EntradaHistoria.Entr_FechaEntrada between '" + FechaDesde + "' and  '" + FechaHasta + "'" +
                                "		and dbo.Empresa.Empr_Codigo="+CodEmpresa +" " +    
                                " GROUP BY dbo.Cargo.Carg_Descripcion, dbo.Empresa.Empre_Nit, dbo.Empresa.Empre_RazonSocial";
                DgvDatosColDocumento.HeaderText = "Ocupación";
                DgvDatosColNombres.HeaderText = "N° de Pacientes";
                DataTable TablaDiagnistico = new DataTable();
                TablaDiagnistico = ObjServer.LlenarTabla(Sql);
                if (TablaDiagnistico.Rows.Count > 0)
                {
                    for (int i = 0; i < TablaDiagnistico.Rows.Count; i++)
                    {
                        DgvDatos.Rows.Add((i + 1).ToString(), TablaDiagnistico.Rows[i]["CARGO"], TablaDiagnistico.Rows[i]["NÚMERO DE PACIENTES"]);
                    }

                }      
            }
            BuscarHistoriaAtendida();

            if (DgvDatos.Rows.Count<=0)
            {
                MessageBox.Show("Sin resultados", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void TxtOcupacion_Click(object sender, EventArgs e)
        {
            //CrpInforme Crp = new CrpInforme();
            ////Crp.Subreports.ToString() = "";
            ////Crp.SetParameterValue("CrpSubInformePacienteDiagnosticorpt_CodEmpresa", CboEmpresa.SelectedValue);
            //Crp.SetParameterValue("@CodEmpresa", CboEmpresa.SelectedValue);
            //Crp.SetParameterValue("@FechaDesde", DtDesde.Value.ToShortDateString());
            //Crp.SetParameterValue("@FechaHasta", DtHasta.Value.ToShortDateString());
            ////Crp.SetParameterValue("@FechaHasta", DtHasta.Value.ToShortDateString());
            ////Crp.Parameter_CrpSubInformePacienteDiagnosticorpt_CodEmpresa = "";
            //FrmVisualizarReporte f = new FrmVisualizarReporte();

            ////SE VISUALIZA EL REPORTE
            //f.crystalReportViewer1.ReportSource = Crp;
            //f.Show();


            //CrpSubInformePacienteDiagnostico Crp = new CrpSubInformePacienteDiagnostico();

            //Crp.SetParameterValue("@CodEmpresa", CboEmpresa.SelectedValue);
            //Crp.SetParameterValue("@FechaDesde", DtDesde.Value.ToShortDateString());
            //Crp.SetParameterValue("@FechaHasta", DtHasta.Value.ToShortDateString());

            //FrmVisualizarReporte f = new FrmVisualizarReporte();

            //f.crystalReportViewer1.ReportSource = Crp;
            //f.Show();
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            if (RdbDiagnostico.Checked)
            {
                CrpSubInformePacienteDiagnostico Crp = new CrpSubInformePacienteDiagnostico();

                Crp.SetParameterValue("@CodEmpresa", CboEmpresa.SelectedValue);
                Crp.SetParameterValue("@FechaDesde", DtDesde.Value.ToShortDateString());
                Crp.SetParameterValue("@FechaHasta", DtHasta.Value.ToShortDateString());

                FrmVisualizarReporte f = new FrmVisualizarReporte();

                f.crystalReportViewer1.ReportSource = Crp;
                f.Show();
            }
            else
            {
                CrpInforme Crp = new CrpInforme();
                Crp.SetParameterValue("@CodEmpresa", CboEmpresa.SelectedValue);
                Crp.SetParameterValue("@FechaDesde", DtDesde.Value.ToShortDateString());
                Crp.SetParameterValue("@FechaHasta", DtHasta.Value.ToShortDateString());
                FrmVisualizarReporte f = new FrmVisualizarReporte();
                //SE VISUALIZA EL REPORTE
                f.crystalReportViewer1.ReportSource = Crp;
                f.Show();
            }
        }

        private void BrnGraficas_Click(object sender, EventArgs e)
        {
            CrpGraficaxSexo Crp = new CrpGraficaxSexo();
            Crp.SetParameterValue("@CodEmpresa", CboEmpresa.SelectedValue);
            Crp.SetParameterValue("@FechaDesde", DtDesde.Value.ToShortDateString());
            Crp.SetParameterValue("@FechaHasta", DtHasta.Value.ToShortDateString());
            FrmVisualizarReporte f = new FrmVisualizarReporte();
            //SE VISUALIZA EL REPORTE
            f.crystalReportViewer1.ReportSource = Crp;
            f.Show();
        }

        private void CboEmpresa_SelectedValueChanged(object sender, EventArgs e)
        {
            DgvDatos.Rows.Clear();
            DgvPacientes.Rows.Clear();
        }
    }
}
