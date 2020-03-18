using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
namespace Conexion
{
   public  class ClsSqlServer : ClsGestores
    {
       public static string ValorConfig;

       //Asignar la conexión establecida()
      public SqlConnection conexion = new SqlConnection();
       //private string Servidor = "";
       //private string BaseDatos = "";
      private string Query = "";
       private string cadenaC = "";

       //Propiedad para la sentencia a ejecutar()
       public override string CadnaSentencia { set { Query=value; } }

       //Propiedad para la cadena de Conección()
       //public string NombreServidor { set {Servidor=value ;} }
       //public string NombreBaseDatos { set {BaseDatos=value ;} }
       //public string NombreUsuario { set {Usuario=value ;} }
       //public string Password { set {Contraseña=value ;} }

       //Para asignarle la cadena de conexión()
       
       public override string CadenaCnn {set { cadenaC=value; } }

 
       public override void  Conectar()
       {
           conexion.ConnectionString = cadenaC;
           conexion.Open();
       }
       //Permite ejecutar una sentencia a la base de datos()
       public override void Sentencia()
       {
           SqlCommand Cmd = new SqlCommand(Query,conexion);
           Cmd.ExecuteNonQuery();
       }
       //Permite cargar los datos al DGV()
       public override void CargarDatos( DataGridView Dgv, string Consulta)
       {
           DataTable Tabla = new DataTable();
           SqlDataAdapter DataAd = new SqlDataAdapter(Consulta,conexion);
           DataAd.Fill(Tabla);
           Dgv.DataSource = Tabla;
       }
    }
}
