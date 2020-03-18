using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Historia_Clinica.Conexion;

namespace Historia_Clinica
{
    public partial class FrmVerExamenes : Form
    {
        public FrmVerExamenes()
        {
            InitializeComponent();
        }
        public void EstilosDgv(DataGridView DGV)
        {
            DGV.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            //DGV.DefaultCellStyle.Font = new Font("Verdana", 12);
            Font prFont = new Font("Verdana", 12, FontStyle.Bold);
            for (int i = 0; i < DGV.Columns.Count; i++)
            {
                DGV.Columns[i].HeaderCell.Style.Font = prFont;
                DGV.Columns[i].HeaderCell.Style.ForeColor = Color.Black;
                DGV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            DGV.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DGV.AutoResizeColumns();

            DGV.EnableHeadersVisualStyles = false;
        }
        ClsSqlServer ObjConexion = new ClsSqlServer();
        public int NumeroAtencion;
        public string NombreCompleto;
        private void FrmVerExamenes_Load(object sender, EventArgs e)
        {
            TxtTitulo.Text = "TÍTULO POR DEFECTO";
            ObjConexion.CadenaCnn = Conexion.CadenaConexion.cadena();
            ObjConexion.Conectar();
                        DataTable tabla = new DataTable();
            //WHERE Pac_Identificacion=" + Documento;
            string query = "SELECT	dbo.ExamenPracticado.ExaPrac_Examen_Codigo,           "+
		                    "dbo.ExamenPracticado.ExaPrac_Entrada_Numero,                 "+
		                    "dbo.ExamenPracticado.ExaPrac_Resultado,                      "+
		                    "dbo.ExamenPracticado.ExaPrac_Ajuntar,                        "+
		                    "dbo.ExamenPracticado.ExaPrac_FechaExamen,                    "+
		                    "dbo.ExamenPracticado.ExaPrac_Codigo,                         "+
		                    "dbo.Examen.Exam_Descripcion,                                 "+
		                    "dbo.TipResultado.TipRes_Descripcion                          "+
                            "FROM	dbo.ExamenPracticado INNER JOIN                       "+
		                    "dbo.Examen ON dbo.ExamenPracticado.ExaPrac_Examen_Codigo =   "+ 
		                    "dbo.Examen.Exam_Codigo INNER JOIN                            "+
		                    "dbo.TipResultado ON dbo.ExamenPracticado.ExaPrac_Resultado = "+
		                    "dbo.TipResultado.TipRes_Codigo "+
                            "WHERE [ExaPrac_Entrada_Numero]="+NumeroAtencion;
           tabla = ObjConexion.LlenarTabla(query);
           if (tabla.Rows.Count > 0)
           {
               for (int i = 0; i < tabla.Rows.Count; i++)
               {
                   string des = tabla.Rows[i]["Exam_Descripcion"].ToString();
                   string Cod = tabla.Rows[i]["ExaPrac_Examen_Codigo"].ToString();
                   dataGridView1.Columns.Add(Cod, des);
               }
           }
           else
           {
               MessageBox.Show("No hay examenes para mostrar", "", MessageBoxButtons.OK);
               this.Dispose();
               this.Close();
               return;
           }
           EstilosDgv(dataGridView1);
           //Creamos los parametros para la atenciòn
           ReportParameter Titulo = new ReportParameter();
           ReportParameter nombre = new ReportParameter();

           //Establecemos el valor de los parámetros de la informacion personal
           Titulo = new ReportParameter("Titulo", TxtTitulo.Text);
           nombre = new ReportParameter("Nombre", NombreCompleto);

           //--(DATOS ANTECEDENTES GINECOLOGICOS)

           reportViewer1.LocalReport.EnableExternalImages = true;
           this.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
ReportParameter paramImagen = new ReportParameter("rutaImagen", "");
            reportViewer1.LocalReport.SetParameters(paramImagen);
            this.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;

           this.reportViewer1.LocalReport.SetParameters(Titulo);
           this.reportViewer1.LocalReport.SetParameters(nombre);
           this.reportViewer1.RefreshReport();
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            //Seleccionamos el zoom que deseamos utilizar. En este caso un 100%

            
            //            Dim reportViewer1 As New List(Of ReportParameter)
//            parametros.Add(New ReportParameter("rutaImagen", "file:C:\Nueva carpeta\eneagrama.JPG"))
//Me.ReportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local
//        ReportViewer1.LocalReport.ReportPath = "..\\..\\Report1.rdlc"
//        Me.ReportViewer1.LocalReport.SetParameters(parametros)

            reportViewer1.ZoomPercent = 100;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGridView1.Columns[e.ColumnIndex].Name);
            if (e.ColumnIndex>-1)
            {
                int examen = Convert.ToInt32(dataGridView1.Columns[e.ColumnIndex].Name);
                //SP_ExamenPracticado_ImagenTableAdapter.Connection
                this.SP_ExamenPracticado_ImagenTableAdapter.Fill(this.HistoriaClinica_New.SP_ExamenPracticado_Imagen, NumeroAtencion, examen);
                //Creamos los parametros para la atenciòn
                ReportParameter Titulo = new ReportParameter();
                ReportParameter nombre = new ReportParameter();

                //Establecemos el valor de los parámetros de la informacion personal
                Titulo = new ReportParameter("Titulo", TxtTitulo.Text);
                nombre = new ReportParameter("Nombre", NombreCompleto);

                //--(DATOS ANTECEDENTES GINECOLOGICOS)
                reportViewer1.LocalReport.EnableExternalImages = true;
                this.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;

                this.reportViewer1.LocalReport.SetParameters(Titulo);
                this.reportViewer1.LocalReport.SetParameters(nombre);
                
                ClsGuardarImagen ObjImagen = new ClsGuardarImagen();
                string ruta = ObjImagen.RutaImagen(LblDocumento.Text, NumeroAtencion.ToString(), examen.ToString());
                //MessageBox.Show(ruta);
                if (ruta == "")
                {
                    ReportParameter paramImagen = new ReportParameter("rutaImagen", "", true);
                    reportViewer1.LocalReport.SetParameters(paramImagen);
                }
                else
                {
                    ReportParameter paramImagen = new ReportParameter("rutaImagen", "file:" + ruta, true);
                    reportViewer1.LocalReport.SetParameters(paramImagen);
                }

                this.reportViewer1.RefreshReport();
                //reportViewer1.DataBindings=Repor
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                //Seleccionamos el zoom que deseamos utilizar. En este caso un 100%
                reportViewer1.ZoomPercent = 100;
            }
        }
    }
}
