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
    public partial class FrmCancelarCita : Form
    {
        public FrmCancelarCita()
        {
            InitializeComponent();
        }
        ClsSqlServer NuevoSql = new ClsSqlServer();
        DataTable TablaPaciente = new DataTable();
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

        public async Task<string> BuscarHistoriaSinAtender()
        {
            DgvDatos.Rows.Clear();
            NuevoSql.CadenaCnn = Historia_Clinica.Conexion.CadenaConexion.cadena();
            NuevoSql.Conectar();
            string Criterio_Empresa = "";

            string Query = "SELECT DISTINCT	CONCAT(dbo.Paciente.Pac_Nombre1,' ',dbo.Paciente.Pac_Nombre2,' ',        " +
                             "dbo.Paciente.Pac_Apellido1,' ',dbo.Paciente.Pac_Apellido2) AS nombres,                 " +
                             " dbo.Paciente.Pac_TipoIdentificacion, dbo.EntradaProvisional.Ent_Codigo,               " +
                             "dbo.Paciente.Pac_Identificacion, dbo.Paciente.Pac_FechaNacimiento,                     " +
                             "dbo.EntradaProvisional.Entr_FechaEntrada,                                               " +
                             "dbo.TipoExamen.TipoExam_Descripcion, dbo.InformacionOcupacionalProvi.InfOcu_CodEmpresa, " +
                             "dbo.Enfasis.Enfa_Descripcion, dbo.Paciente.Pac_CodGenero,                          " +
                             "dbo.EntradaProvisional.Ent_Estado, dbo.Genero.Gen_Codigo, dbo.Genero.Gen_Descripcion   " +
                             "FROM  dbo.TipoExamen INNER JOIN                                                        " +
                             "dbo.EntradaProvisional ON dbo.TipoExamen.TipoExam_Codigo =                             " +
                             "dbo.EntradaProvisional.Entr_TipoExamenCodigo INNER JOIN                                " +
                             "dbo.Enfasis ON dbo.EntradaProvisional.Ent_Enfasis = dbo.Enfasis.Enfa_Codigo INNER JOIN " +
                             "dbo.Paciente INNER JOIN                                                                " +
                             "dbo.Genero ON dbo.Paciente.Pac_CodGenero = dbo.Genero.Gen_Codigo ON                " +
                             "dbo.EntradaProvisional.Entr_IdPaciente = dbo.Paciente.Pac_Identificacion   INNER JOIN " +
                             "dbo.InformacionOcupacionalProvi ON dbo.EntradaProvisional.Entr_IdPaciente = dbo.InformacionOcupacionalProvi.InfOcu_paciente        " + Criterio_Empresa +
                             "  ORDER BY dbo.EntradaProvisional.Ent_Codigo, dbo.InformacionOcupacionalProvi.InfOcu_CodEmpresa   DESC";
            //"WHERE  CONCAT(dbo.Paciente.Pac_Nombre1,dbo.Paciente.Pac_Nombre2,dbo.Paciente.Pac_Apellido1,dbo.Paciente.Pac_Apellido2,Pac_Identificacion,Entr_Numero) LIKE" + "'%" + TxtCriterio.Text + "%'  And Entr_FechaEntrada between  '" + DtDesde.Text + "'and'" + DtHasta.Text + "'";
            await Task.Run(() => { TablaPaciente = NuevoSql.LlenarTabla(Query);});
            if (TablaPaciente.Rows.Count > 0)
            {
                for (int i = 0; i < TablaPaciente.Rows.Count; i++)
                {
                    DgvDatos.Rows.Add((i + 1).ToString(), TablaPaciente.Rows[i]["Pac_Identificacion"].ToString(), TablaPaciente.Rows[i]["nombres"].ToString());
                }
                //DgvDatos.Visible = false;
                //DgvDatosColImprimir.Visible = false;
                //DgvDatosColNumeroHistoria.Visible = false;
                
                TablaPaciente = null;
            }
            EstilosDgv2(DgvDatos);
            return string.Empty;
        }

        private async void FrmCancelarCita_Load(object sender, EventArgs e)
        {
            //Cargo los datos que tendra el combobox de las empresas
            NuevoSql.CadenaCnn = Historia_Clinica.Conexion.CadenaConexion.cadena();
            NuevoSql.Conectar();
            await BuscarHistoriaSinAtender();
        }

        private async void DgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>-1)
            {
                if (MessageBox.Show("¿Desea quitar el paciente de la agenda?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    SqlConnection cnn = new SqlConnection(CadenaConexion.cadena());
                    string documento = DgvDatos.Rows[e.RowIndex].Cells["DgvDatosColDocumento"].Value.ToString();
                    //ELIMINAR LOS EXAMENES PRACTICADOS
                    SqlCommand comman = new SqlCommand();
                    cnn.Open();
                   
                    string QueryI = "DELETE FROM [dbo].[ExamenPracticadoProvi]  WHERE ExaPac_Paciente='" +documento + "'";
                    comman.CommandText = QueryI;
                    comman.Connection = cnn;
                    comman.ExecuteNonQuery();

                    //ELIMINAR 
                    QueryI = "DELETE FROM [dbo].[EntradaProvisional] WHERE Entr_IdPaciente='" + documento + "'";
                    comman.CommandText = QueryI;
                    comman.Connection = cnn;
                    comman.ExecuteNonQuery();


                    cnn.Close();
                    await  BuscarHistoriaSinAtender();
                }
            }
        }
    }
}
