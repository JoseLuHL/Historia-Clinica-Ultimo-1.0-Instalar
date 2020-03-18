using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Historia_Clinica.Conexion
{
    public static class CadenaConexion
    {
        public static void config()
        {
            string conexionTxt;

            conexionTxt = Application.StartupPath + @"\configuracion\config.txt"; //ARCHIVO DE TEXTO PARA GUARDAR DATOS
            string[] lines = System.IO.File.ReadAllLines(conexionTxt);
            string[] conexion = System.IO.File.ReadAllLines(conexionTxt);
            Configuracion.Nombre = conexion[0];
            Configuracion.Nit = conexion[1];
            Configuracion.Telefono = conexion[2];
            Configuracion.Direccion = conexion[3];
            Configuracion.PiePagina = conexion[4];
            
            Configuracion.LugarExamen = conexion[7];           
            //return conexion[0];
        }
        public static string cadena()
        {
            string conexionTxt;

            conexionTxt = Application.StartupPath + @"\conexion\conexion.txt"; //ARCHIVO DE TEXTO PARA GUARDAR DATOS
            string[] lines = System.IO.File.ReadAllLines(conexionTxt);
            string[] conexion = System.IO.File.ReadAllLines(conexionTxt);
            return conexion[0];
        }

        public static Boolean SaberHacerCopiaSeguridad()
        {
            Boolean retornar = false;
            string conexionTxt;
            conexionTxt = Application.StartupPath + @"\conexion\conexion.txt"; //ARCHIVO DE TEXTO PARA GUARDAR DATOS
            string[] lines = System.IO.File.ReadAllLines(conexionTxt);
            string[] conexion = System.IO.File.ReadAllLines(conexionTxt);
            if (conexion[1] == "S")
                retornar = true;
            return retornar;
        }

        public async static Task<string> cadenaAsync()
        {
            string conexionTxt;
            string[] conexion = new string[50];
            await Task.Run(() =>
            {
                conexionTxt = Application.StartupPath + @"\conexion\conexion.txt"; //ARCHIVO DE TEXTO PARA GUARDAR DATOS
                string[] lines = System.IO.File.ReadAllLines(conexionTxt);
                conexion = System.IO.File.ReadAllLines(conexionTxt);
            });
            return conexion[0];
        }
    }
    public static class Configuracion
    {
        public static string Nombre { get; set; }
        public static string Nit { get; set; }
        public static string Telefono { get; set; }
        public static string Direccion { get; set; }
        public static string PiePagina { get; set; }
        public static string LugarExamen { get; set; }
    }
        
}
