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
using Conexion;
using Historia_Clinica.Conexion;
namespace Historia_Clinica
{
    public partial class FrmVerPacientes : Form
    {
        public FrmVerPacientes()
        {
            InitializeComponent();
        }
        public void EstilosDgv(DataGridView DGV)
        {
            DGV.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            DGV.DefaultCellStyle.Font = new Font("Verdana", 11);
            Font prFont = new Font("Verdana", 11, FontStyle.Bold);
            for (int i = 0; i < DGV.Columns.Count; i++)
            {
                DGV.Columns[i].HeaderCell.Style.Font = prFont;
                DGV.Columns[i].HeaderCell.Style.ForeColor = Color.Black;
                DGV.Columns[i].HeaderCell.Style.BackColor = Color.Gainsboro;
                DGV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            DGV.AutoResizeColumns();
            DGV.EnableHeadersVisualStyles = false;
        }
        ClsSqlServer ObjServer = new ClsSqlServer();
        string identificacion = "";
        public void CARGAR_EXAMENES()
        {
            //CARGAR LOS DATOS EN EXAMENES  PRACTICADOS:::
            DgvExamenPacticado.Rows.Clear();
            identificacion = "";
            DataTable Datos = new DataTable();
            Datos = ObjServer.LlenarTabla("SELECT dbo.ExamenPracticado.ExaPrac_Examen_Codigo, dbo.ExamenPracticado.ExaPrac_Entrada_Numero, "+
                                                "dbo.ExamenPracticado.ExaPrac_Resultado, dbo.ExamenPracticado.ExaPrac_Ajuntar, "+ 
                                                "dbo.ExamenPracticado.ExaPrac_FechaExamen, dbo.EntradaHistoria.Entr_IdPaciente "+
                                                "FROM            dbo.ExamenPracticado INNER JOIN "+
                                                "dbo.EntradaHistoria ON dbo.ExamenPracticado.ExaPrac_Entrada_Numero = dbo.EntradaHistoria.Entr_Numero "+ 
                                                "WHERE ExaPrac_Entrada_Numero=" + Convert.ToInt32(LblNumeroAtencion.Text));
            if (Datos.Rows.Count > 0)
            {
                identificacion = Datos.Rows[0]["Entr_IdPaciente"].ToString();
                DgvExamenPacticado.RowCount = Datos.Rows.Count + 1;
                for (int i = 0; i < Datos.Rows.Count; i++)
                {
                    DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value = Datos.Rows[i]["ExaPrac_Examen_Codigo"];
                    DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value = Datos.Rows[i]["ExaPrac_Ajuntar"];
                    DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value = Datos.Rows[i]["ExaPrac_FechaExamen"].ToString().Substring(0,10);
                    DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value = Datos.Rows[i]["ExaPrac_Resultado"];
                    DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar2"].Value = "Imagen #" + (i+1);
                }
            }
            else
            {
                MessageBox.Show("No hay examenes ajuntados", "", MessageBoxButtons.OK);
                DgvExamenPacticado.RowCount = 1;
                DgvExamenPacticado.Rows[0].Cells["DgvExamenPacticadoColAjuntar"].Value = "......";
                DgvExamenPacticado.Rows[0].Cells["DgvExamenPacticadoColFecha"].Value = "01/01/1990";
            }
        }
        public void CargarFecha()
        {
            DgvExamenPacticado.Rows[DgvExamenPacticado.Rows.Count - 1].Cells["DgvExamenPacticadoColFecha"].Value = "01/01/1999";
        }
        public void tamañoDGV()
        {
            DgvExamenPacticadoColAjuntar2.Width = 180;
            DgvExamenPacticadoColExamen.Width = 250;
            DgvExamenPacticadoColFecha.Width = 200;
            DgvExamenPacticadoColResultado.Width = 250;
        }
        private void FrmVerPacientes_Load(object sender, EventArgs e)
        {
            ObjServer.CadenaCnn = Historia_Clinica.Conexion.CadenaConexion.cadena();
            ObjServer.Conectar();

            // Cargo los datos que tendra el combobox DgvExamenPacticadoColResultado
            DgvExamenPacticadoColResultado.DataSource = ObjServer.LlenarTabla("SELECT [TipRes_Codigo] As Codigo  ,[TipRes_Descripcion] As Descripcion  FROM [dbo].[TipResultado]  order by TipRes_Descripcion");
            DgvExamenPacticadoColResultado.DisplayMember = "Descripcion";
            DgvExamenPacticadoColResultado.ValueMember = "Codigo";

            //DgvExamenPacticadoColResultado
            DgvExamenPacticado.Rows.Clear();
            string query = "";
            string ColCod = "";
            string ColDes = "";
            DataTable tabla = new DataTable();
            ColCod = "DgvExamenPacticadoColCodigo";
            ColDes = "DgvExamenPacticadoColExamen";
            query = "SELECT [Exam_Codigo] As [Codigo] ,[Exam_Descripcion] As [Descripcion] FROM [dbo].[Examen] order by Exam_Descripcion";
            tabla = ObjServer.LlenarTabla(query);
            DgvExamenPacticadoColExamen.DataSource = tabla;
            DgvExamenPacticadoColExamen.DisplayMember = "Descripcion";
            DgvExamenPacticadoColExamen.ValueMember = "Codigo";

            CARGAR_EXAMENES();
            DgvExamenPacticadoColExamen.Width = 180;
            DgvExamenPacticadoColAjuntar.Width = 110;
            DgvExamenPacticadoColResultado.Width = 130;
            EstilosDgv(DgvExamenPacticado);
            CargarIconosDGV();
            tamañoDGV();
            //Creamos los parametros para el reporte
            //ReportParameter FechaAtencion = new ReportParameter();
            //ReportParameter Concepto = new ReportParameter();
            //ReportParameter TipoExamen = new ReportParameter();
            //ReportParameter Enfasis = new ReportParameter();
            //ReportParameter ConceptoAptitud = new ReportParameter();
            //ReportParameter Recomendacion = new ReportParameter();
            ////
            ////Establecemos el valor de los parámetros
            //FechaAtencion = new ReportParameter("FechaAtencion", "0000value_par0");
            //Concepto = new ReportParameter("Concepto", "0000value_par0");
            //TipoExamen = new ReportParameter("TipoExamen", "0000value_par0");
            //Enfasis = new ReportParameter("Enfasis", "0000value_par0");
            //ConceptoAptitud = new ReportParameter("ConceptoActitud", "0000value_par0");
            //Recomendacion = new ReportParameter("Recomendacion", "0000value_par0");

            // TODO: esta línea de código carga datos en la tabla 'conexionHistoriaC.SP_EntradaHistoria' Puede moverla o quitarla según sea necesario.          
            // this.reportViewer1.LocalReport.SetParameters(FechaAtencion);
            //this.reportViewer1.LocalReport.SetParameters(Concepto);
            //this.reportViewer1.LocalReport.SetParameters(TipoExamen);
            //this.reportViewer1.LocalReport.SetParameters(Enfasis);
            //this.reportViewer1.LocalReport.SetParameters(ConceptoAptitud);
            //this.reportViewer1.LocalReport.SetParameters(Recomendacion);
            //this.reportViewer1.RefreshReport();
        }
        private void DgvExamenPacticado_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
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
                        DgvExamenPacticado.CurrentRow.Cells[3].Value = (dialog.SafeFileName);
                        DgvExamenPacticado.CurrentRow.Cells["DgvExamenPacticadoColDireccion"].Value = dialog.FileName;
                        DgvExamenPacticado.CurrentCell.Value = "Imagen #" + (e.RowIndex + 1).ToString();
                        CargarFecha();
                    }
                }
                if (e.ColumnIndex == 6)
                {
                    try
                    {
                        string Imagen = "";
                        Imagen = DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentCell.RowIndex].Cells[3].Value.ToString();
                        //MessageBox.Show(Imagen);
                        if (Imagen == "System.Byte[]")
                        {
                            byte[] imageBuffer = (byte[])DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentCell.RowIndex].Cells[3].Value;
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

                        startInfo.Arguments = @"/c rundll32 ""C:\Program Files\Windows Photo Viewer\PhotoViewer.dll"", ImageView_Fullscreen " + RutaArchivoDestino;
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                    catch (Exception)
                    {
                        try
                        {
                            ClsGuardarImagen ObjGuardarImagen = new ClsGuardarImagen();

                            if ( DgvExamenPacticado.Rows[e.RowIndex].Cells["DgvExamenPacticadoColDireccion"].Value == null)
                            {
                                string nombreexamen = DgvExamenPacticado.Rows[e.RowIndex].Cells["DgvExamenPacticadoColExamen"].Value.ToString();
                                ObjGuardarImagen.AbrirIMG(txtidentificacion.Text, LblNumeroAtencion.Text, nombreexamen );  

                            }
                            else
                            {
                                MessageBox.Show("Inicio");
                                ObjGuardarImagen.AbrirIMG(DgvExamenPacticado.Rows[e.RowIndex].Cells["DgvExamenPacticadoColDireccion"].Value.ToString());  
                            }                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("No hay imagen " + ex.ToString(), "", MessageBoxButtons.OK);
                        }                       
                    }
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

       public void CargarIconosDGV()
        {
            string Imagen = Application.StartupPath + @"\imagenes\ver.png";
            DgvExamenPacticado.Rows[DgvExamenPacticado.Rows.Count - 1].Cells["DgvExamenPacticadoColFechaver"].Value = Image.FromFile(Imagen);
            DgvExamenPacticado.Rows[DgvExamenPacticado.Rows.Count - 1].Cells["DgvExamenPacticadoColFecha"].Value = "01/01/1999";
        }
        private void BtnGuardarHistoria_Click(object sender, EventArgs e)
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
                        string QueryI = "DELETE FROM [dbo].[ExamenPracticado]  WHERE ExaPrac_Entrada_Numero=" + Convert.ToInt32(LblNumeroAtencion.Text) ;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        string[] fecha = DateTime.Now.ToString().Split(' ');
                        //PRIME SE INSERTA LA ENTRADA - INSERTAR EN ENTRADA - HISTORIA
                        string Query;

                        //Insertar EXAMEN PRACTICADOS
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        for (int i = 0; i < DgvExamenPacticado.Rows.Count-1; i++)
                        {
                            string Imagen = "";
                            Imagen = DgvExamenPacticado.Rows[i].Cells[3].Value.ToString();
                            //MessageBox.Show(Imagen);
                            if (Imagen != "System.Byte[]")
                            {
                                if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value != null)
                                {
                                    //if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value.ToString().Length > 7 && DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value != null)
                                    //{
                                        Boolean confirmar = false; //Para saber si hay datos para agregar
                                        int CodExamen = 0;
                                        string FechaExa = "";
                                        int resultado = 0;

                                        if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value != null)
                                            resultado = Convert.ToInt32(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value);
                                        else
                                            resultado = 1;

                                        FechaExa = DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value.ToString();
                                        CodExamen = Convert.ToInt32(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value);
                                        confirmar = true;
                                        Query = "INSERT INTO [dbo].[ExamenPracticado] VALUES (@examen" + i + ",@NumAtencion" + i + ",@resultado" + i + ",@foto" + i + ",@fecha" + i + ")";

                                        comman.CommandText = Query;
                                        comman.Parameters.Add("@NumAtencion" + i, SqlDbType.Int);
                                        comman.Parameters.Add("@examen" + i, SqlDbType.Int);
                                        comman.Parameters.Add("@resultado" + i, SqlDbType.Int);
                                        comman.Parameters.Add("@fecha" + i, SqlDbType.Date);
                                        comman.Parameters["@NumAtencion" + i].Value = Convert.ToInt32(LblNumeroAtencion.Text);
                                        comman.Parameters["@examen" + i].Value = CodExamen;
                                        comman.Parameters["@resultado" + i].Value = resultado;
                                        comman.Parameters["@fecha" + i].Value = FechaExa;

                                        if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColDireccion"].Value!=null)
                                        {
                                            ClsGuardarImagen ObjGuardarImagen = new ClsGuardarImagen();
                                            string direcion = DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColDireccion"].Value.ToString();
                                            string[] extencion = DgvExamenPacticado.Rows[i].Cells[3].Value.ToString().Split('.');
                                            string nombreexamen = DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value.ToString();
                                            //MessageBox.Show(txtidentificacion.Text + " - " + LblNumeroAtencion.Text + " - " + nombreexamen + "." + extencion[1] + " - " + direcion);
                                            ObjGuardarImagen.CrearCarpeta(txtidentificacion.Text, LblNumeroAtencion.Text, nombreexamen + "." + extencion[1], direcion);
                                        }

                                        SqlParameter imageParameter = new SqlParameter("@foto" + i, SqlDbType.Image);
                                        imageParameter.Value = DBNull.Value;
                                        comman.Parameters.Add(imageParameter);
                                        comman.ExecuteNonQuery();
                                    }
                            }
                            else
                            {
                                        int CodExamen = 0;
                                        string FechaExa = "";
                                        int resultado = 0;

                                        if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value != null)
                                            resultado = Convert.ToInt32(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value);
                                        else
                                            resultado = 1;

                                        FechaExa = DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value.ToString();

                                        CodExamen = Convert.ToInt32(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value);
                                        Query = "INSERT INTO [dbo].[ExamenPracticado] VALUES (@examen" + i + ",@NumAtencion" + i + ",@resultado" + i + ",@foto" + i + ",@fecha" + i + ")";

                                        comman.CommandText = Query;
                                        comman.Parameters.Add("@NumAtencion" + i, SqlDbType.Int);
                                        comman.Parameters.Add("@examen" + i, SqlDbType.Int);
                                        comman.Parameters.Add("@resultado" + i, SqlDbType.Int);
                                        comman.Parameters.Add("@fecha" + i, SqlDbType.Date);

                                        comman.Parameters["@NumAtencion" + i].Value = Convert.ToInt32(LblNumeroAtencion.Text);
                                        comman.Parameters["@examen" + i].Value = CodExamen;
                                        comman.Parameters["@resultado" + i].Value = resultado;
                                        comman.Parameters["@fecha" + i].Value = FechaExa;

                                        if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value != null)
                                        {
                                            comman.Parameters.Add("@foto" + i, SqlDbType.Image);
                                            string Imagen2 = DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value.ToString();
                                            PctFoto.Image = null;
                                            ms = new MemoryStream();
                                            if (Imagen2 == "System.Byte[]")
                                            {
                                                byte[] imageBuffer = (byte[])DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value;
                                                // Se crea un MemoryStream a partir de ese buffer
                                                System.IO.MemoryStream ms1 = new System.IO.MemoryStream(imageBuffer);
                                                // Se utiliza el MemoryStream para extraer la imagen
                                                this.PctFoto.Image = Image.FromStream(ms1);
                                                PctFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                            }
                                            else
                                            {
                                                this.PctFoto.Image = Image.FromFile(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value.ToString());
                                                PctFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                            }
                                            comman.Parameters["@foto" + i].Value = ms.GetBuffer();
                                        }
                                        else
                                        {
                                            SqlParameter imageParameter = new SqlParameter("@foto", SqlDbType.Image);
                                            imageParameter.Value = DBNull.Value;
                                            comman.Parameters.Add(imageParameter);
                                        }
                                        comman.ExecuteNonQuery();
                            }
                            
                            }                            
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                        SQLtrans.Commit();
                        //DgvExamenPacticado.Rows.Clear();
                        CargarIconosDGV();
                        MessageBox.Show("Datos guardados correctamente", "finalizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("La operación no puedo completarse debido a: \n 1 - No dispone de una conexión  \n 2 - El registro esta duplicado  \n 3 - La información ingrasado no corresponde a la requerida  \n 4 - No se ha cargado imagen  \n Vuelva a intentarlo!!! " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageBox.Show(ex.ToString());
                        SQLtrans.Rollback();
                    }
              }
        }
        private void DgvExamenPacticado_UserAddedRow_1(object sender, DataGridViewRowEventArgs e)
        {
            CargarIconosDGV();
            CargarFecha();
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
        private void BtnCargarExamenes_Click(object sender, EventArgs e)
        {
            CARGAR_EXAMENES();
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
                    else
                    {
                        if (Convert.ToDateTime(fecha) > DateTime.Now.Date)
                        {
                            MessageBox.Show("Fecha mayor a la actual");
                            DgvExamenPacticado.Rows[cell].Cells["DgvExamenPacticadoColFecha"].Value = DateTime.Now.Date.ToString().Substring(0, 10);
                        }
                    }
                }
            }
        }       
    }
}
