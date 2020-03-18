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
using Historia_Clinica.Conexion;
using Historia_Clinica;
namespace Micopia
{
    public partial class FrmCrearCopiaSeguridad : Form
    {
        public FrmCrearCopiaSeguridad()
        {
            InitializeComponent();
        }
        //"Data Source=.;Initial Catalog=HistoriaClinica;Integrated Security=True"
         SqlConnection conexion = new SqlConnection(CadenaConexion.cadena());
         public string Restaurar = "";

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            if (Restaurar=="")
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "BAK|";
                saveFileDialog1.Title = "Guardar copia de seguridad";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    RutaGuardar = saveFileDialog1.FileName;
                    LblCreanado.Visible = true;
                    BtnGenerar.Enabled = false;
                    LblCreanado.Text = "Creando copia de seguridad, espere mientras se completa esta operación";
                    PnlMSM.Visible = true;
                    timer1.Start();
                }    
            }
            else
            {
                // Se crea el OpenFileDialog
                OpenFileDialog dialog = new OpenFileDialog();
                // Se muestra al usuario esperando una acción
                DialogResult result = dialog.ShowDialog();

                // Si seleccionó un archivo (asumiendo que es una imagen lo que seleccionó)
                // la mostramos en el PictureBox de la inferfaz
                if (result == DialogResult.OK)
                {
                    if (MessageBox.Show("Esta seguro que desea restaurar la base de datos", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                    ubicacion_copia = dialog.FileName;
                    LblCreanado.Text = "Restaurando copia de seguridad, espere mientras se completa esta operación";
                    LblCreanado.Visible = true;
                    BtnGenerar.Enabled = false;
                    PnlMSM.Visible = true;
                    timer2.Start();

                    //this.Pt_Firma.Image = Image.FromFile(dialog.FileName);
                    //MessageBox.Show(System.Drawing.Imaging.ImageFormat.Jpeg.ToString());
                    //this.PctFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
               }
            }            
        }

        public void RESTAURAR(string ubicacion)
        {
                SqlConnection Cnnn = new System.Data.SqlClient.SqlConnection("Data Source=DESKTOP-V1D9AHP;Initial Catalog=MANTENIMIENTO;Integrated Security=True");
                Cnnn.Open();
                SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SP_RESTAURAR_COPIA_SEGURIDAD");
                try
                {
                    cmd.Connection = Cnnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UBICACION", ubicacion);
                    //MessageBox.Show(dialog.FileName);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    timer2.Stop();
                    MessageBox.Show(ex.ToString());
                    timer2.Stop();
                    MessageBox.Show("Algo salio mal, la operación no pudo completarse", "", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                    LblCreanado.Text = "Clic sobre el botón para continuar...";
                    BtnGenerar.Enabled = true;
                    PnlMSM.Visible = false;
                }
                Cnnn.Close();
        }
        public string RutaGuardar = "";
        public void CREAR_COPIA_SEGURIDAD( )
        {
            string ruta = Application.StartupPath + "\\Copia_de_seguridad\\";
            string nombre_copia = (System.DateTime.Today.Day.ToString() + "-" + System.DateTime.Today.Month.ToString() + "-" + System.DateTime.Today.Year.ToString() + "-" + System.DateTime.Now.Hour.ToString() + "-" + System.DateTime.Now.Minute.ToString() + "-" + System.DateTime.Now.Second.ToString() + " Copia");
            string x = "BACKUP DATABASE [HistoriaClinica] TO  DISK = N'" + ruta + nombre_copia + "' WITH NOFORMAT, NOINIT,  NAME = N'HistoriaClinica-Completa Base de datos Copia de seguridad', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            
            SqlCommand cmd = new SqlCommand(x, conexion);
            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                if (RutaGuardar != "")
                {
                    string rutaOr = Application.StartupPath + "\\Copia_de_seguridad\\" + nombre_copia;
                    string rutaDes = RutaGuardar;
                    System.IO.File.Copy(rutaOr, rutaDes, true);
                    System.IO.File.Delete(rutaOr);
                }
            }
            catch (Exception )
            {
                timer1.Stop();
                MessageBox.Show("Algo salio mal, la operación no pudo completarse","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                //this.Dispose();
                if (RutaGuardar == "")
                {
                    this.Dispose();
                }
                LblCreanado.Text = "Clic sobre el botón para continuar...";
                BtnGenerar.Enabled = true;
                PnlMSM.Visible = false;

            }
            finally
            {
                conexion.Close();
                conexion.Dispose();
            }
            //Recuerda Compartir con tus amigos , eso hace que yo siga aportando lo poquito que yo sé :)
            //Facebook: https://www.facebook.com/TodakarhdGames
            //Twitter: https://twitter.com/TodakarHDPer
            //Google+: https://plus.google.com/u/0/112493613987084700183
            //Instragram: http://instagram.com/todakarhd
            //Google http://Google.com
        }
        private void FrmCrearCopiaSeguridad_Load(object sender, EventArgs e)
        {
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            if (progressBar1.Value==35)
            {
                CREAR_COPIA_SEGURIDAD();
            }
            if (progressBar1.Value==100)
            {
                timer1.Stop();
                
                MessageBox.Show("La operación ha finalizado correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (RutaGuardar=="")
                {
                    this.Dispose();
                }
                LblCreanado.Text = "Clic sobre el botón para continuar...";
                BtnGenerar.Enabled = true;
                PnlMSM.Visible = false;
            }
            label1.Text = (progressBar1.Value +1)+ " %";
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            //FrmPresentacion f = new FrmPresentacion();
            this.Dispose();
        }
        string ubicacion_copia = "";
        private void timer2_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            if (progressBar1.Value == 35)
            {
                RESTAURAR(ubicacion_copia);
            }
            if (progressBar1.Value == 100)
            {
                timer2.Stop();
                MessageBox.Show("La restauración de la base de dados ha finalizado correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LblCreanado.Text = "Clic sobre el botón para continuar...";
                BtnGenerar.Enabled = true;
                PnlMSM.Visible = false;
            }
            label1.Text = (progressBar1.Value+1) + " %";
        }
    }
}
