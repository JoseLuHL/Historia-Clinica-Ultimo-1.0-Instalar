using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micopia;
using Conexion;
using Historia_Clinica.Conexion;
using System.Threading;
using Control_de_Tecnicos;


namespace Historia_Clinica
{
    public partial class FrmPresentacion : Form
    {
        public FrmPresentacion()
        {
            InitializeComponent();
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmPresentacion_MouseMove);
        }
        // Declaraciones del API de Windows (y constantes usadas para mover el form)
        //
        const int WM_SYSCOMMAND = 0x112;
        const int MOUSE_MOVE = 0xF012;
        //
        // Declaraciones del API
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        //
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        //
        // función privada usada para mover el formulario actual
        private void moverForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }
        public void Cargando()
        {
            //circularProgressBar1.Visible = true;
            //circularProgressBar1.Value = 0;
            //circularProgressBar1.Minimum = 0;
            //circularProgressBar1.Maximum = 100;

            for (int i = 1; i <= 100; i++)
            {
                Thread.Sleep(5);
                //circularProgressBar1.Value = i;
                //circularProgressBar1.Update();
            }
        }
        private void FrmPresentacion_Load(object sender, EventArgs e)
        {
            Pct_.Focus();
            TxtUsuario.ForeColor = Color.Gray;
            TxtContraseña.ForeColor = Color.Gray;
            TxtUsuario.Text = "Ingrese Usuario...";
            TxtContraseña.Text = "Ingrese Contraseña...";
            BtnIniciarSesion.Focus();
            TxtContraseña.UseSystemPasswordChar = true;
        }

        private void TxtUsuario_Leave(object sender, EventArgs e)
        {
            if (TxtUsuario.Text == "")
            {
                TxtUsuario.ForeColor = Color.Gray;
                TxtUsuario.Text = "Ingrese Usuario...";
            }
        }

        private void TxtUsuario_Enter(object sender, EventArgs e)
        {
            if (TxtUsuario.Text == "Ingrese Usuario...")
            {
                TxtUsuario.ForeColor = Color.Black;
                TxtUsuario.Text = "";
            }
        }

        private void TxtContraseña_Leave(object sender, EventArgs e)
        {
            if (TxtContraseña.Text == "")
            {
                TxtContraseña.ForeColor = Color.Gray;
                TxtContraseña.Text = "Ingrese Contraseña...";
            }

        }

        private void TxtContraseña_Enter(object sender, EventArgs e)
        {
            if (TxtContraseña.Text == "Ingrese Contraseña...")
            {
                TxtContraseña.ForeColor = Color.Black;
                TxtContraseña.Text = "";
            }
        }

        private void BtnHistoria_Click(object sender, EventArgs e)
        {

            FrmPacientesPendientes f = new FrmPacientesPendientes();
            //FrnHistoriaClinica f = new FrnHistoriaClinica();
            f.ShowDialog();
            //this.WindowState = FormWindowState.Minimized;
        }

        private void BtnAspirante_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Minimized;
            Paciente f = new Paciente();
            f.Show();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            if (CadenaConexion.SaberHacerCopiaSeguridad())
            {
                if (BtnAspirante.BackColor != Color.Silver)
                {

                    if (MessageBox.Show("¿Crear copia de seguridad?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        FrmCrearCopiaSeguridad f = new FrmCrearCopiaSeguridad();
                        this.Hide();
                        f.BtnGenerar.Visible = false;
                        f.timer1.Start();
                        f.ShowDialog();
                    }
                    else
                    {
                        this.Dispose();
                    }
                }
                else
                    this.Dispose();
            }
            else
                this.Dispose();
        }

        private void PctSistema_Click(object sender, EventArgs e)
        {
            //FrmContenido f = new FrmContenido();
            FrmGestionarSistema f = new FrmGestionarSistema();
            f.ShowDialog();
            //this.WindowState = FormWindowState.Minimized;
        }

        public async Task<string> IniciarSesion()
        {
            string res = "";
            if (BtnAspirante.BackColor != Color.Transparent)
            {
                try
                {
                    //SE PASA LA CADENA DE CONEXIÓN A LA PRIPIEDAD DE LA CLASE ClsSqlServer()
                    NuevoSql.CadenaCnn = await Historia_Clinica.Conexion.CadenaConexion.cadenaAsync();
                    //MessageBox.Show(Historia_Clinica.Conexion.CadenaConexion.cadena());
                    //INICIAMOS LA CONEXIÓN()
                    await NuevoSql.ConectarAsync();
                    //PREPARAMOS LA CONSULTA
                    string Query = "SELECT  dbo.Usuario.Usu_Nombre,                         " +
                                    "dbo.Usuario.Usu_Contraseña,                           " +
                                    "dbo.ModulosUsuario.Mod_Codigo,                        " +
                                    "dbo.ModulosUsuario.Mod_Descripcion,                   " +
                                    "dbo.Usuario.Usu_NombreCompleto                        " +
                                    "FROM  dbo.UsuarioModulo INNER JOIN                    " +
                                    "dbo.Usuario ON dbo.UsuarioModulo.UsuMod_Usuario       " +
                                    "= dbo.Usuario.Usu_Nombre INNER JOIN                   " +
                                    "dbo.ModulosUsuario ON dbo.UsuarioModulo.UsuMod_Modulo " +
                                    "= dbo.ModulosUsuario.Mod_Codigo Where Usu_Nombre=" + "'" + TxtUsuario.Text + "'";
                    DataTable TablaUsuario = new DataTable();
                    //CARGAMOS LA TABLA CON LA CONSULTA  HECHA()
                    await Task.Run(() => { TablaUsuario = NuevoSql.LlenarTabla(Query); });
                    //SE EMPIEZA A VALIDAR QUE EL USUARIO EXISTA, QUE LA CONTRASEÑA COINSIDA CON LA INGRESADA 
                    //UNA VEZ CONFIRMADO QUE EL USUARIO EXISTE SE EMPIEZA A ACTIVAR LOS MUDULOS CORRESPONDIENTES AL USUARIO
                    //MessageBox.Show(TablaUsuario.Rows.Count.ToString());

                    if (TablaUsuario.Rows.Count > 0)
                    {
                        if (TxtContraseña.Text == TablaUsuario.Rows[0]["Usu_Contraseña"].ToString())
                        {
                            ClsUsuarioLogin.ConsultarMedico(TxtUsuario.Text);
                            LblBienvenido.Text = "Hola " + TablaUsuario.Rows[0]["Usu_NombreCompleto"].ToString();
                            CadenaConexion.config();
                            //MessageBox.Show(Configuracion.Nombre);
                            LblCerrar.Visible = true;
                            PnlUsuario.Visible = false;
                            foreach (Control item in GrpOpc.Controls)
                            {
                                if (item is PictureBox)
                                {
                                    for (int i = 0; i < TablaUsuario.Rows.Count; i++)
                                    {
                                        if (TablaUsuario.Rows[i]["Mod_Codigo"].ToString() == item.Tag.ToString())
                                        {
                                            item.Enabled = true;
                                            item.BackColor = Color.Transparent;
                                        }
                                    }
                                }
                            }
                            TxtContraseña.Text = "";
                            TxtUsuario.Text = "";
                            timer1.Enabled = false;
                            //circularProgressBar1.Visible = false;
                        }
                        else
                        {
                            timer1.Enabled = false;
                            //circularProgressBar1.Visible = false;
                            MessageBox.Show("Usuario o contraseña incorrecto!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                    }
                    else
                    {
                        timer1.Enabled = false;
                        //circularProgressBar1.Visible = false;
                        MessageBox.Show("Usuario o contraseña incorrecto!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    timer1.Enabled = false;
                    //circularProgressBar1.Visible = false;
                    //res = ex.Message;
                    MessageBox.Show("Sin conexión ", "", MessageBoxButtons.OK);
                }
            }
            return res;
        }
        ClsSqlServer NuevoSql = new ClsSqlServer();
        private async void BtnIniciarSesion_Click(object sender, EventArgs e)
        {
            //circularProgressBar1.Visible = true;
            //timer1.Enabled = true;
            await Deshabilitar();
            BtnIniciarSesion.Enabled = false;
            await IniciarSesion();
            BtnIniciarSesion.Enabled = true;
            //timer1.Enabled = false;
            //circularProgressBar1.Visible = false;
        }
        public async Task Deshabilitar()
        {
            LblBienvenido.Text = "";
            //await Task.Run(() =>
            //{
                foreach (Control item in GrpOpc.Controls)
                {
                    if (item is PictureBox)
                    {
                        item.Enabled = false;
                        item.BackColor = Color.Silver;
                    }
                }

            //});
        }
        //CERRAR SESIÓN 
        private async void LblCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Cerrar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                await Deshabilitar();
                PnlUsuario.Visible = true;
                LblCerrar.Visible = false;
            }

        }
        //ABRE EL FORMULARIO DE REPORTES
        private void BtnReporte_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Minimized;
            FrmPacientesPendientes f = new FrmPacientesPendientes();
            f.RdbSinAtender.Checked = false;
            f.RdbSinAtender.Visible = false;
            f.RdbAtendido.Checked = true;
            f.BtnHistoria.Visible = false;
            f.RdbAtendido.Visible = false;
            f.LblMensaje.Text = "CERTIFICADOS E HISTORIAS";
            f.Show();
        }

        private void FrmPresentacion_MouseMove(object sender, MouseEventArgs e)
        {
            //moverForm();
        }
        //MINIMIZAR EL FORMULARIO()
        private void PctMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Minimized;
            FrmExamenesPrcticados f = new FrmExamenesPrcticados();
            f.Show();
        }
        public void EventoBoton(EventArgs e, PictureBox btn, Color color)
        {
            if (btn.Enabled == true)
            {
                btn.BackColor = color;
            }
        }
        private void BtnHistoria_MouseHover(object sender, EventArgs e)
        {
            EventoBoton(e, BtnHistoria, Color.Lavender);
        }

        private void BtnHistoria_MouseLeave(object sender, EventArgs e)
        {
            EventoBoton(e, BtnHistoria, Color.Transparent);
        }

        private void BtnAspirante_MouseHover(object sender, EventArgs e)
        {
            EventoBoton(e, BtnAspirante, Color.Lavender);
        }

        private void BtnAspirante_MouseLeave(object sender, EventArgs e)
        {
            EventoBoton(e, BtnAspirante, Color.Transparent);
        }

        private void BtnReporte_MouseHover(object sender, EventArgs e)
        {
            EventoBoton(e, BtnReporte, Color.Lavender);
        }

        private void BtnReporte_MouseLeave(object sender, EventArgs e)
        {
            EventoBoton(e, BtnReporte, Color.Transparent);
        }

        private void PctSistema_MouseHover(object sender, EventArgs e)
        {
            EventoBoton(e, PctSistema, Color.Lavender);
        }

        private void PctSistema_MouseLeave(object sender, EventArgs e)
        {
            EventoBoton(e, PctSistema, Color.Transparent);
        }

        private void pictureBox10_MouseHover(object sender, EventArgs e)
        {
            EventoBoton(e, pictureBox10, Color.Lavender);
        }

        private void pictureBox10_MouseLeave(object sender, EventArgs e)
        {
            EventoBoton(e, pictureBox10, Color.Transparent);
        }

        private async void TxtContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //circularProgressBar1.Visible = true;
                timer1.Enabled = true;
                await Deshabilitar();
                BtnIniciarSesion.Enabled = false;
                await IniciarSesion();
                BtnIniciarSesion.Enabled = true;

            }
        }
        public string SepararCadena(string cadena)
        {
            string NuevaCadena = "";
            //string cadena = "holaMundoCruel";
            for (int i = 0; i < cadena.Length; i++)
            {
                char caracter = Convert.ToChar(cadena.Substring(i, 1));

                if (char.IsUpper(caracter))
                    NuevaCadena = NuevaCadena + " " + caracter;
                else
                    NuevaCadena = NuevaCadena + caracter;
            }
            return NuevaCadena;
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            FrmVerPacientes f2 = new FrmVerPacientes();
            f2.ShowDialog();
            //FrmVisualizarReporte f = new FrmVisualizarReporte();
            //f.crystalReportViewer1.ReportSource = HistoriaClinicaDataSet;
        }

        private void BtnAyuda_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = Application.StartupPath + "\\manual\\manual.pdf";
                proc.Start();
                proc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Archivo no encontrado", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (BtnHistoria.BackColor != Color.Silver)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "BAK Copia|";
                saveFileDialog1.Title = "Save an Sql File";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    FrmCrearCopiaSeguridad f = new FrmCrearCopiaSeguridad();
                    f.RutaGuardar = saveFileDialog1.FileName;
                    f.ShowDialog();
                }
            }
            else
                MessageBox.Show("No tiene permiso para realizar esta operación", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            if (CadenaConexion.SaberHacerCopiaSeguridad())
            {
                //if (BtnHistoria.BackColor != Color.Silver)
                //{
                    //this.WindowState = FormWindowState.Minimized;
                    FrmCrearCopiaSeguridad f = new FrmCrearCopiaSeguridad();
                    //f.LblCreanado.Visible = false;
                    f.PnlMSM.Visible = false;
                    f.LblCreanado.Text = "Clic sobre el botón para continuar...";
                    //f.RutaGuardar = ""; //saveFileDialog1.FileName;
                    //this.Hide();

                    f.ShowDialog();

                    //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    //saveFileDialog1.Filter = "BAK|";
                    //saveFileDialog1.Title = "Guardar copia de seguridad";
                    //saveFileDialog1.ShowDialog();
                    //if (saveFileDialog1.FileName != "")
                    //{
                    //    //this.Hide();
                    //    FrmCrearCopiaSeguridad f = new FrmCrearCopiaSeguridad();
                    //    f.RutaGuardar = saveFileDialog1.FileName;
                    //    f.ShowDialog();

                    //}
                //}
                //else
                //    MessageBox.Show("No tiene permiso para realizar esta operación", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Esta opción no esta habilitada en este equipo", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void BtnExportar_MouseHover(object sender, EventArgs e)
        {
            EventoBoton(e, BtnExportar, Color.Lavender);
        }

        private void BtnExportar_MouseLeave(object sender, EventArgs e)
        {
            EventoBoton(e, BtnExportar, Color.Transparent);
        }

        private void BtnAyuda_MouseHover(object sender, EventArgs e)
        {
            EventoBoton(e, BtnAyuda, Color.Lavender);
        }

        private void BtnAyuda_MouseLeave(object sender, EventArgs e)
        {
            EventoBoton(e, BtnAyuda, Color.Transparent);
        }

        private void BtnRestaurar_Click(object sender, EventArgs e)
        {
            if (CadenaConexion.SaberHacerCopiaSeguridad())
            {
                //if (BtnHistoria.BackColor != Color.Silver)
                //{
                    //this.WindowState = FormWindowState.Minimized;
                    FrmCrearCopiaSeguridad f = new FrmCrearCopiaSeguridad();
                    //f.LblCreanado.Visible = false;
                    f.PnlMSM.Visible = false;
                    f.LblCreanado.Text = "Clic sobre el botón para continuar...";
                    f.Restaurar = "Restaurar";

                    f.ShowDialog();
                //}
                //else
                //    MessageBox.Show("No tiene permiso para realizar esta operación", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Esta opción no esta habilitada en este equipo", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {

                //circularProgressBar1.Value = 0;
                //circularProgressBar1.Minimum = 0;
                //circularProgressBar1.Maximum = 100;
                for (int i = 1; i <= 100; i++)
                {
                    Thread.Sleep(3);
                    //circularProgressBar1.Value = i;
                    //circularProgressBar1.Update();
                }
            }
            else
            {
                //circularProgressBar1.Visible = false;
            }
        }

        private void Pic_Medico_Click(object sender, EventArgs e)
        {
            if (BtnHistoria.BackColor != Color.Silver)
            {
                //this.WindowState = FormWindowState.Minimized;
                FrmMedicos f = new FrmMedicos();

                f.ShowDialog();
            }
            else
                MessageBox.Show("No tiene permiso para realizar esta operación", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void BtnUsuario_Click(object sender, EventArgs e)
        {
            FrmGestion_Usuario f = new FrmGestion_Usuario();
            f.ShowDialog();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
