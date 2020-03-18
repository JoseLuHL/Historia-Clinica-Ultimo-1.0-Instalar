using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Historia_Clinica.Conexion
{
   public class ClsGuardarImagen
    {
       public List<string> obtenerArchivosDirectorio(string rutaArchivo)
       {

           List<string> listaRuta = new List<string>();

           listaRuta = Directory.GetFiles(Path.GetDirectoryName(rutaArchivo), Path.GetFileName(rutaArchivo)).ToList();

           return listaRuta;
       }

       public void AbrirIMG(string ruta)
       {
           string ubicacion = ruta;
           String EXTENCION = "";
           string[] archivo = Path.GetFileName(ruta).Split('.');

                   EXTENCION = archivo[1];

           if (EXTENCION == "pdf")
           {
               System.Diagnostics.Process proc = new System.Diagnostics.Process();
               proc.StartInfo.FileName = ruta;
               proc.Start();
               proc.Close();
           }
           else
           {
               string UbicacionExamen = ruta;
               System.Diagnostics.Process process = new System.Diagnostics.Process();
               System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
               startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
               startInfo.FileName = "cmd.exe";
               startInfo.Arguments = @"/c rundll32 ""C:\Program Files\Windows Photo Viewer\PhotoViewer.dll"", ImageView_Fullscreen " + UbicacionExamen;
               process.StartInfo = startInfo;
               process.Start();
           }
       }

        public void AbrirIMG (string documento,string numeroatencion,string nombreexamenExtencion)
        {
            string[] files = Directory.GetFiles(Application.StartupPath + @"\Examenes Practicados\" + documento + @"\" + numeroatencion, "*.*");
            string[] ubicacion = Directory.GetFiles(Application.StartupPath + @"\Examenes Practicados\" + documento + @"\" + numeroatencion, "*.*");
            String EXTENCION = "";
            for (int i = 0; i < ubicacion.Length; i++)
            {
                string[] archivo = Path.GetFileName(ubicacion[i]).Split('.');

                if (archivo[0]==nombreexamenExtencion)
                {
                    EXTENCION = archivo[1];
                    nombreexamenExtencion = nombreexamenExtencion + "." + archivo[1];
                }                
            }
            if (EXTENCION == "pdf")
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = Application.StartupPath + @"\Examenes Practicados\" + documento + @"\" + numeroatencion + @"\" + nombreexamenExtencion;
                proc.Start();
                proc.Close();
            }
            else
            {
                string UbicacionExamen = Application.StartupPath + @"\Examenes Practicados\" + documento + @"\" + numeroatencion + @"\" + nombreexamenExtencion;
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = @"/c rundll32 ""C:\Program Files\Windows Photo Viewer\PhotoViewer.dll"", ImageView_Fullscreen " + UbicacionExamen;
                process.StartInfo = startInfo;
                process.Start();
            }
        }

        public string RutaImagen(string documento, string numeroatencion, string nombreexamenExtencion)
        {
            string UbicacionExamen;
            try
            {
                string[] files = Directory.GetFiles(Application.StartupPath + @"\Examenes Practicados\" + documento + @"\" + numeroatencion, "*.*");
                string[] ubicacion = Directory.GetFiles(Application.StartupPath + @"\Examenes Practicados\" + documento + @"\" + numeroatencion, "*.*");
                String EXTENCION = "";
                for (int i = 0; i < ubicacion.Length; i++)
                {
                    string[] archivo = Path.GetFileName(ubicacion[i]).Split('.');
                    if (archivo[0] == nombreexamenExtencion)
                    {
                        EXTENCION = archivo[1];
                        nombreexamenExtencion = nombreexamenExtencion + "." + archivo[1];
                    }
                }

                UbicacionExamen = Application.StartupPath + @"\Examenes Practicados\" + documento + @"\" + numeroatencion + @"\" + nombreexamenExtencion;
                
                if (EXTENCION == "pdf")
                {
                    UbicacionExamen = "";
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = Application.StartupPath + @"\Examenes Practicados\" + documento + @"\" + numeroatencion + @"\" + nombreexamenExtencion;
                    proc.Start();
                    proc.Close();
                }
            }
            catch (Exception)
            {
                UbicacionExamen = "";
            }
            return UbicacionExamen;
        }
        public void CrearCarpeta(string documento, string numero, string nombreExamenExtencion, string direcionIMG)
        {
            //Direcion que contiene la carpeta del paciente, se crea con el numero de identificacion
            string CarpetaPaciente = Application.StartupPath + @"\Examenes Practicados\" + documento;
            //Se utiliza para almacenar la ruta donde se guardara el examen
            string UbicacionExamen;
            //preguntamos si la carpeta existe
            if (Directory.Exists(CarpetaPaciente))
            {
                //Ruta que contiene la carpeta con el numero de la atencion
                string CarpetaAtencion = Application.StartupPath + @"\Examenes Practicados\" + documento + @"\" + numero;

                if (Directory.Exists(CarpetaAtencion))
                {
                    UbicacionExamen = Application.StartupPath + @"\Examenes Practicados\" + documento + @"\" + numero + @"\" + nombreExamenExtencion;
                }
                else
                {
                    Directory.CreateDirectory(CarpetaAtencion);
                    UbicacionExamen = Application.StartupPath + @"\Examenes Practicados\" + documento + @"\" + numero + @"\" + nombreExamenExtencion;
                }

                File.Copy(direcionIMG, UbicacionExamen, true);
            }
            else
            {
                Directory.CreateDirectory(CarpetaPaciente);
                string CarpetaAtencion = Application.StartupPath + @"\Examenes Practicados\" + documento + @"\" + numero;
                Directory.CreateDirectory(CarpetaAtencion);
                UbicacionExamen = Application.StartupPath + @"\Examenes Practicados\" + documento + @"\" + numero + @"\" + nombreExamenExtencion;
                File.Copy(direcionIMG, UbicacionExamen, true);
            }
        }
    }
}
