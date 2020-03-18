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
    public partial class FrmGestionarSistema : Form
    {
        public FrmGestionarSistema()
        {
            InitializeComponent();
        }

        string OrigenesDatos;
        ClsSqlServer NuevoSql = new ClsSqlServer();

        //Para cambiar la apariencia de los DGV
        public void EstilosDgv(DataGridView DGV)
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
            //DGV.DefaultCellStyle.BackColor = Color.White;
            //DGV.AutoResizeColumns();

            //DGV.DefaultCellStyle.Font = new Font("Verdana", 11);
            //Font prFont = new Font("Verdana", 11, FontStyle.Bold);
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                if (i % 2 == 0)
                    DGV.Rows[i].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                else
                    DGV.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
            DGV.EnableHeadersVisualStyles = false;
        }
        public string SepararCadena(string cadena)
        {
            string NuevaCadena = "";
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
        ClsSqlServer ObjServer = new ClsSqlServer();
        public void BuscarHistoriaSinAtender()
        {
            
            
        }
  
        private void FrmGestionarSistema_Load(object sender, EventArgs e)
        {
            //DgvDatos.Rows.Clear();
            NuevoSql.CadenaCnn = Historia_Clinica.Conexion.CadenaConexion.cadena();
            NuevoSql.Conectar();
            string Query = "select name As [TABLA] from sys.tables Where name <> 'sysdiagrams'" +
                                            " and name <> 'AntecednteFamiliar' and " +
                                            "name <> 'AccidenteLaboral' and name <> 'AntecedentePersonal'" +
                                            " and name <> 'CicloMenstrual' and name <> 'EnfermedadProfesional' " +
                                            "and name <> 'EntradaHistoria' and name <> 'ExamenFisico'" +
                                            " and name <> 'ExamenLaboratorio' and name <> 'ExamenPracticado' and name <> 'HabitoPaciente'" +
                                            " and name <> 'InformacionOcupacional' and name <> 'Paciente' and name <> 'ModulosUsuario' and name <> 'Inmunizar'" +
                                            " and name <> 'ProbabilidadRiego' and name <> 'RiesgoOcupacional' and name <> 'RecomendacionPaciente'" +
                                            " and name <> 'RevisionSistema' and name <> 'ElementoProteccion' and name <> 'TipoExamenEnfasis'  and name <> 'Usuario' and name <> 'EquilibroPaciente'" +
                                            " and name <> 'UsuarioModulo' and name <> 'ExamenPracticadoProvi' and name <> 'EntradaProvisional' and name <> 'EquilibroPaciente' "+
                                            " and name <> 'Examen_Paciente' and name <> 'InformacionOcupacionalProvi' and name <> 'DiagnosticoPaciente' "+
                                            "and name <> 'Edad' and name <> 'SiNo' and name <> 'TEMPO_FUMADORES' and name <> 'ItemActivar'  and name <> 'Elemento'  and name <> 'Medico'  and name <> 'Cliente' order by TABLA";
            //"WHERE  CONCAT(dbo.Paciente.Pac_Nombre1,dbo.Paciente.Pac_Nombre2,dbo.Paciente.Pac_Apellido1,dbo.Paciente.Pac_Apellido2,Pac_Identificacion,Entr_Numero) LIKE" + "'%" + TxtCriterio.Text + "%'  And Entr_FechaEntrada between  '" + DtDesde.Text + "'and'" + DtHasta.Text + "'";
            DataTable Tablas = new DataTable();
            Tablas = NuevoSql.LlenarTabla(Query);
            //MessageBox.Show(TablaPaciente.Rows.Count.ToString());

            for (int i = 0; i < Tablas.Rows.Count; i++)
            {
                DgvTablas.Rows.Add(SepararCadena(Tablas.Rows[i]["TABLA"].ToString()));
            }
            //::
            //::
            //NuevoSql.CargarDatos(DgvTablas, );
            //::
            DgvTablas.Columns[0].Width = 210;
            //::
            DgvDetalleTablas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            DgvTablas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //::
            //MARCAR LAS CLAVES PRIMARIAS DE LA TABLA CARGADA
            MarcarClavePK();
            //::
            EstilosDgv(DgvDetalleTablas);
            EstilosDgv(DgvTablas);
        }

        public string where(string[] columna, string[] valor, string[] TipoCol)
        {
            string clausula = "";
            string valor2 = "";
            for (int i = 0; i < columna.Length; i++)
            {
                valor2 = "";

                if (TipoCol[i] == "System.String")
                    valor2 = "'" + valor[i] + "'";
                else
                    valor2 = valor[i];

                if (i > 0)
                    clausula = clausula + " AND " + columna[i] + "=" + valor2;
                else
                    clausula = columna[i] + "=" + valor2;
            }
            clausula = " WHERE " + clausula;
            return clausula;
        }

        public string sentencia(string[] columna, string[] valor, string tabla, string[] TipoCol)
        {
            string Consulta = " ";
            string valor2 = "";
            for (int i = 0; i < columna.Length; i++)
            {
                valor2 = "";

                if (TipoCol[i] == "System.String")
                    valor2 = "'" + valor[i] + "'";
                else
                    valor2 = valor[i];

                if (i > 0)
                    Consulta = Consulta + ", " + columna[i] + "=" + valor2;
                else
                    Consulta = columna[i] + "=" + valor2;
            }
            Consulta = "UPDATE " + tabla + " SET " + Consulta;
            return Consulta;
        }


        public string Insertar(string[] columna, string[] valor, string tabla, string[] TipoCol)
        {
            string Consulta = " ";
            string valor2 = "";
            for (int i = 0; i < columna.Length; i++)
            {
                valor2 = "";

                if (TipoCol[i] == "System.String")
                    valor2 = "'" + valor[i] + "'";
                else
                    valor2 = valor[i];

                if (i > 0)
                    Consulta = Consulta + ", " + "" + "" + valor2;
                else
                    Consulta = "" + "" + valor2;
            }
            Consulta = "INSERT " + tabla + " VALUES (" + Consulta + ")";
            return Consulta;
        }

        public void CargarIconosDGV()
        {
            string Imagen = Application.StartupPath + @"\imagenes\disquete.png";
            DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 1].Cells["DgvTablaDetalleColGuardar"].Value = Image.FromFile(Imagen);

            Imagen = Application.StartupPath + @"\imagenes\editar.ico";
            DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 1].Cells["DgvTablaDetalleColEditar"].Value = Image.FromFile(Imagen);

            Imagen = Application.StartupPath + @"\imagenes\eliminar.ico";
            DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 1].Cells["DgvTablaDetalleColEliminar"].Value = Image.FromFile(Imagen);

        }
        public void MarcarClavePK()
        {
            ColPK = 0;
            for (int i = 3; i < DgvDetalleTablas.Columns.Count; i++)
            {
                for (int x = 0; x < DgvClavePrimaria.Rows.Count; x++)
                {
                    if (DgvDetalleTablas.Columns[i].HeaderText == DgvClavePrimaria.Rows[x].Cells["ColumnName"].Value.ToString())
                    {
                        DgvDetalleTablas.Columns[i].DefaultCellStyle.BackColor = Color.Red;
                        DgvDetalleTablas.Columns[i].ReadOnly = true;
                        DgvDetalleTablas.Columns[i].Visible = false;
                        ColPK++;
                        break;
                    }
                }
            }
            try
            {
                CargarIconosDGV();
            }
            catch (System.IO.FileNotFoundException ex)
            {

                throw;
            }
            for (int i = 3; i < ColPK + 3; i++)
            {
                DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 1].Cells[i].Style.BackColor = Color.WhiteSmoke;
                DgvDetalleTablas.Columns[i].ReadOnly = false;
            }
        }

        //Para saber el numero de columnas que pertenecen a la clave primaria
        int ColPK = 0;
        public void EDITAR_REGISTRO()
        {

        }
        private void DgvDetalleTablas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                            string Sentencia = "";
                            //para que no tome la ultima fila cuando a editar
                            //MessageBox.Show(e.RowIndex.ToString() + " == " + (DgvDetalleTablas.Rows.Count - 2).ToString());
                            if (e.RowIndex == DgvDetalleTablas.Rows.Count - 2 & DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 2].DefaultCellStyle.BackColor == Color.SteelBlue)
                            {
                                //Se valida que ESTE MARCADA LA COLUMNA PARA EDITAR

                                    #region AGREGAR LA CIUDAD
                                    if (DgvTablas.CurrentCell.Value.ToString().Replace(" ", "").ToString() == "Ciudad")
                                    {
                                        if (MessageBox.Show("Desea  editar el registro?", "Editar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                           string CodigoCiudad = DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 2].Cells[3].Value.ToString();
                                            string CodigoDepar = DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 2].Cells[5].Value.ToString();
                                            string Descripcion = DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 2].Cells[4].Value.ToString();

                                            string Query = "INSERT INTO [dbo].[Ciudad]([Ciud_CodDepto],[Ciud_Codigo],[Ciud_Nombre]) VALUES ('" + CodigoDepar + "','" + CodigoCiudad + "','" + Descripcion + "')";
                                            //MessageBox.Show(Query);
                                            NuevoSql.CadnaSentencia = Query;
                                            NuevoSql.Sentencia();
                                            int fila = DgvDetalleTablas.CurrentCell.RowIndex;
                                            CargarDetalle();
                                            SeleccionarFila(fila);
                                            MessageBox.Show("Registro Agregado!", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //}  
                                        
                                        }
                                        
                                    }
                                    #endregion
                                    else
                                    {
                                    #region AGREGAR EN LAS DEMAS TABLAS 
                                        if (MessageBox.Show("Desea agregar el registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        string[] columnasA = { };
                                        string[] valoresA = { };
                                        string[] TipoColA = { };
                                        int y = 0;
                                        //llenar los vectores de las columnas y valos que seran modificados
                                        for (int x = 4; x < DgvDetalleTablas.Columns.Count; x++)
                                        {
                                            y++;
                                            Array.Resize(ref columnasA, y);
                                            Array.Resize(ref valoresA, y);
                                            Array.Resize(ref TipoColA, y);

                                            string nombrecolumna = DgvDetalleTablas.Columns[x].Name;
                                            TipoColA[y - 1] = DgvDetalleTablas.Columns[x].ValueType.ToString();
                                            columnasA[y - 1] = nombrecolumna;
                                            valoresA[y - 1] = DgvDetalleTablas.Rows[e.RowIndex].Cells[x].Value.ToString();
                                        }
                                        string Query = Insertar(columnasA, valoresA, DgvTablas.CurrentCell.Value.ToString().Replace(" ", ""), TipoColA);
                                        //MessageBox.Show(Query);
                                        NuevoSql.CadnaSentencia = Query;
                                        NuevoSql.Sentencia();
                                        int fila = DgvDetalleTablas.CurrentCell.RowIndex;
                                        CargarDetalle();
                                        SeleccionarFila(fila);
                                        MessageBox.Show("Registro Agregado!", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //}     
                                    }
                                        #endregion
                                    }
                                    #region CODIGO COMENTADO 
                                    //if (MessageBox.Show("Desea editar el registro?", "Editar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    //{

                                    //    string[] columnasA = { }; // ALMACENA LAS COLUMNAS QUE SE VAN A MODIFICAR
                                    //    string[] columnasPK = { }; //ALMACENA LAS COLUMNAS QUE SON CLAVE PRIMARIA

                                    //    string[] valoresA = { }; //ALMACENA LOS VALORES QUE SE VAN A EDITAR
                                    //    string[] valoresPK = { }; //ALMACENA LOS VALORES DE LAS CLAVE PRIMARIA

                                    //    string[] TipoColA = { }; //ALMACENA EL TIPO DE COLUMNA (INT, STRING, ETC.)
                                    //    string[] TipoColPK = { };//ALMACENA EL TIPO DE COLUMNA PRIMARIA (INT, STRING, ETC.)
                                    //    int y = 0;  //SE UTILIZA COMO CONTADOR

                                    //    //llenar los vectores de las columnas y valos que seran modificados
                                    //    for (int x = 3 + ColPK; x < DgvDetalleTablas.Columns.Count; x++)
                                    //    {
                                    //        y++;
                                    //        //REDIMENCIONAMOS EL TAMAÑO DE LOS VECTORES
                                    //        Array.Resize(ref columnasA, y);
                                    //        Array.Resize(ref valoresA, y);
                                    //        Array.Resize(ref TipoColA, y);

                                    //        string nombrecolumna = DgvDetalleTablas.Columns[x].Name;
                                    //        //ASIGNACIÓN DE LOS RESPECTIVOS VALORES A LOS VECTORES
                                    //        TipoColA[y - 1] = DgvDetalleTablas.Columns[x].ValueType.ToString();
                                    //        columnasA[y - 1] = nombrecolumna;
                                    //        valoresA[y - 1] = DgvDetalleTablas.Rows[e.RowIndex].Cells[x].Value.ToString();
                                    //    }

                                    //    y = 0;
                                    //    //ASIGNAR LAS COLUMNAS QUE CONTIENEN LAS CLAVES PRIMARIAS 
                                    //    for (int x = 3; x < (ColPK + 3); x++)
                                    //    {
                                    //        y++;
                                    //        //REDIMENCIONAR LOS VECTORES;
                                    //        Array.Resize(ref columnasPK, y);
                                    //        Array.Resize(ref valoresPK, y);
                                    //        Array.Resize(ref TipoColPK, y);
                                    //        //ASIGNAR VALORES Y DESCRIPCION DE COLUMNAS PK
                                    //        TipoColPK[y - 1] = DgvDetalleTablas.Columns[x].ValueType.ToString();
                                    //        string nombrecolumna = DgvDetalleTablas.Columns[x].Name;
                                    //        columnasPK[y - 1] = nombrecolumna;
                                    //        valoresPK[y - 1] = DgvDetalleTablas.Rows[e.RowIndex].Cells[x].Value.ToString();
                                    //    }
                                    //    //ENVIO DE PARAMETROS A LA FUNCIÓN QUE DEVUELVE LA SENTENCIA Y SE ASIGNA  A UNA VARIABLE
                                    //    string Query = sentencia(columnasA, valoresA, DgvTablas.CurrentCell.Value.ToString().Replace(" ", ""), TipoColA) + " " + where(columnasPK, valoresPK, TipoColPK);
                                    //    //EJECUTAMOS LA SENTENCIA
                                    //    NuevoSql.CadnaSentencia = Query;
                                    //    NuevoSql.Sentencia();
                                    //    int fila = DgvDetalleTablas.CurrentCell.RowIndex;
                                    //    CargarDetalle();
                                    //    SeleccionarFila(fila);
                                    //    MessageBox.Show("Registro Editado!", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //}

                                    //else
                                    //    MessageBox.Show("No es posible editar el registro");
                                    //else if (DgvDetalleTablas.CurrentRow.Cells["DgvTablaDetalleColEliminar"].Style.BackColor == Color.GreenYellow)
                                    //{
                                    //    ////MessageBox.Show("SI");
                                    //if (MessageBox.Show("Desea eliminar el registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    //{
                                    //    string[] columnasPK = { };
                                    //    string[] valoresPK = { };
                                    //    string[] TipoColPK = { };
                                    //    int y = 0;
                                    //    //llenar los vectores de las columnas y valos que seran Eliminados
                                    //    y = 0;

                                    //    for (int x = 3; x < (ColPK + 3); x++)
                                    //    {
                                    //        y++;
                                    //        Array.Resize(ref columnasPK, y);
                                    //        Array.Resize(ref valoresPK, y);
                                    //        Array.Resize(ref TipoColPK, y);

                                    //        TipoColPK[y - 1] = DgvDetalleTablas.Columns[x].ValueType.ToString();
                                    //        string nombrecolumna = DgvDetalleTablas.Columns[x].Name;
                                    //        columnasPK[y - 1] = nombrecolumna;
                                    //        valoresPK[y - 1] = DgvDetalleTablas.Rows[e.RowIndex].Cells[x].Value.ToString();
                                    //    }
                                    //    //DELETE 
                                    //    string Query = "DELETE FROM " + DgvTablas.CurrentCell.Value.ToString().Replace(" ", "") + " " + where(columnasPK, valoresPK, TipoColPK);
                                    //    NuevoSql.CadnaSentencia = Query;
                                    //    NuevoSql.Sentencia();
                                    //    DgvDetalleTablas.Rows.RemoveAt(e.RowIndex);
                                    //    int fila = DgvDetalleTablas.CurrentCell.RowIndex;
                                    //    CargarDetalle();
                                    //    SeleccionarFila(fila);
                                    //    MessageBox.Show("Registro Eliminado!", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //}
                                    //}
                                    #endregion                               
                            }
                            else
                            {
                                if (DgvTablas.CurrentCell.Value.ToString().Replace(" ", "").ToString() == "Ciudad")
                                #region MODIFICAR LA TABLA CIUDAD
                                {
                                    if (MessageBox.Show("Desea  editar el registro?", "Editar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        string CodigoCiudad = DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 2].Cells[3].Value.ToString();
                                        string CodigoDepar = DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 2].Cells[5].Value.ToString();
                                        string Descripcion = DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 2].Cells[4].Value.ToString();

                                        string Query = "UPDATE [dbo].[Ciudad]  SET  [Ciud_Nombre] = '" + Descripcion + "'" + " WHERE [Ciud_CodDepto]= '" + CodigoDepar + "'" + " and [Ciud_Codigo]='" + CodigoCiudad + "'";
                                        //MessageBox.Show(Query);
                                        NuevoSql.CadnaSentencia = Query;
                                        NuevoSql.Sentencia();
                                        int fila = DgvDetalleTablas.CurrentCell.RowIndex;
                                        CargarDetalle();
                                        SeleccionarFila(fila);
                                        MessageBox.Show("Registro Editado!", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }

                                #endregion
                                else
                                {
                                #region MODIFICAR LAS TABLAS MENOS CIUDAD
                                    if (MessageBox.Show("Desea  editar el registro?", "Editar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        string[] columnasA = { }; // ALMACENA LAS COLUMNAS QUE SE VAN A MODIFICAR
                                        string[] columnasPK = { }; //ALMACENA LAS COLUMNAS QUE SON CLAVE PRIMARIA

                                        string[] valoresA = { }; //ALMACENA LOS VALORES QUE SE VAN A EDITAR
                                        string[] valoresPK = { }; //ALMACENA LOS VALORES DE LAS CLAVE PRIMARIA

                                        string[] TipoColA = { }; //ALMACENA EL TIPO DE COLUMNA (INT, STRING, ETC.)
                                        string[] TipoColPK = { };//ALMACENA EL TIPO DE COLUMNA PRIMARIA (INT, STRING, ETC.)
                                        int y = 0;  //SE UTILIZA COMO CONTADOR

                                        //llenar los vectores de las columnas y valos que seran modificados
                                        for (int x = 3 + ColPK; x < DgvDetalleTablas.Columns.Count; x++)
                                        {
                                            y++;
                                            //REDIMENCIONAMOS EL TAMAÑO DE LOS VECTORES
                                            Array.Resize(ref columnasA, y);
                                            Array.Resize(ref valoresA, y);
                                            Array.Resize(ref TipoColA, y);

                                            string nombrecolumna = DgvDetalleTablas.Columns[x].Name;
                                            //ASIGNACIÓN DE LOS RESPECTIVOS VALORES A LOS VECTORES
                                            TipoColA[y - 1] = DgvDetalleTablas.Columns[x].ValueType.ToString();
                                            columnasA[y - 1] = nombrecolumna;
                                            valoresA[y - 1] = DgvDetalleTablas.Rows[e.RowIndex].Cells[x].Value.ToString();
                                        }

                                        y = 0;
                                        //ASIGNAR LAS COLUMNAS QUE CONTIENEN LAS CLAVES PRIMARIAS 
                                        for (int x = 3; x < (ColPK + 3); x++)
                                        {
                                            y++;
                                            //REDIMENCIONAR LOS VECTORES;
                                            Array.Resize(ref columnasPK, y);
                                            Array.Resize(ref valoresPK, y);
                                            Array.Resize(ref TipoColPK, y);
                                            //ASIGNAR VALORES Y DESCRIPCION DE COLUMNAS PK
                                            TipoColPK[y - 1] = DgvDetalleTablas.Columns[x].ValueType.ToString();
                                            string nombrecolumna = DgvDetalleTablas.Columns[x].Name;
                                            columnasPK[y - 1] = nombrecolumna;
                                            valoresPK[y - 1] = DgvDetalleTablas.Rows[e.RowIndex].Cells[x].Value.ToString();
                                        }
                                        //ENVIO DE PARAMETROS A LA FUNCIÓN QUE DEVUELVE LA SENTENCIA Y SE ASIGNA  A UNA VARIABLE
                                        string Query = sentencia(columnasA, valoresA, DgvTablas.CurrentCell.Value.ToString().Replace(" ", ""), TipoColA) + " " + where(columnasPK, valoresPK, TipoColPK);
                                        //EJECUTAMOS LA SENTENCIA
                                        NuevoSql.CadnaSentencia = Query;
                                        NuevoSql.Sentencia();
                                        int fila = DgvDetalleTablas.CurrentCell.RowIndex;
                                        CargarDetalle();
                                        SeleccionarFila(fila);
                                        MessageBox.Show("Registro Editado!", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    #endregion
                                }
                            }
                    //DgvDetalleTablas.CurrentRow.Cells["DgvTablaDetalleColEliminar"].Style.BackColor = Color.WhiteSmoke;
                    //DgvDetalleTablas.CurrentRow.Cells["DgvTablaDetalleColEditar"].Style.BackColor = Color.WhiteSmoke;
                }

                #region  //Para la columna de Editar
                if (e.ColumnIndex == DgvTablaDetalleColEditar.Index)
                {
                     if (MessageBox.Show("Desea 2 editar el registro?", "Editar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string[] columnasA = { }; // ALMACENA LAS COLUMNAS QUE SE VAN A MODIFICAR
                            string[] columnasPK = { }; //ALMACENA LAS COLUMNAS QUE SON CLAVE PRIMARIA

                            string[] valoresA = { }; //ALMACENA LOS VALORES QUE SE VAN A EDITAR
                            string[] valoresPK = { }; //ALMACENA LOS VALORES DE LAS CLAVE PRIMARIA

                            string[] TipoColA = { }; //ALMACENA EL TIPO DE COLUMNA (INT, STRING, ETC.)
                            string[] TipoColPK = { };//ALMACENA EL TIPO DE COLUMNA PRIMARIA (INT, STRING, ETC.)
                            int y = 0;  //SE UTILIZA COMO CONTADOR

                            //llenar los vectores de las columnas y valos que seran modificados
                            for (int x = 3 + ColPK; x < DgvDetalleTablas.Columns.Count; x++)
                            {
                                y++;
                                //REDIMENCIONAMOS EL TAMAÑO DE LOS VECTORES
                                Array.Resize(ref columnasA, y);
                                Array.Resize(ref valoresA, y);
                                Array.Resize(ref TipoColA, y);

                                string nombrecolumna = DgvDetalleTablas.Columns[x].Name;
                                //ASIGNACIÓN DE LOS RESPECTIVOS VALORES A LOS VECTORES
                                TipoColA[y - 1] = DgvDetalleTablas.Columns[x].ValueType.ToString();
                                columnasA[y - 1] = nombrecolumna;
                                valoresA[y - 1] = DgvDetalleTablas.Rows[e.RowIndex].Cells[x].Value.ToString();
                            }

                            y = 0;
                            //ASIGNAR LAS COLUMNAS QUE CONTIENEN LAS CLAVES PRIMARIAS 
                            for (int x = 3; x < (ColPK + 3); x++)
                            {
                                y++;
                                //REDIMENCIONAR LOS VECTORES;
                                Array.Resize(ref columnasPK, y);
                                Array.Resize(ref valoresPK, y);
                                Array.Resize(ref TipoColPK, y);
                                //ASIGNAR VALORES Y DESCRIPCION DE COLUMNAS PK
                                TipoColPK[y - 1] = DgvDetalleTablas.Columns[x].ValueType.ToString();
                                string nombrecolumna = DgvDetalleTablas.Columns[x].Name;
                                columnasPK[y - 1] = nombrecolumna;
                                valoresPK[y - 1] = DgvDetalleTablas.Rows[e.RowIndex].Cells[x].Value.ToString();
                            }
                            //ENVIO DE PARAMETROS A LA FUNCIÓN QUE DEVUELVE LA SENTENCIA Y SE ASIGNA  A UNA VARIABLE
                            string Query = sentencia(columnasA, valoresA, DgvTablas.CurrentCell.Value.ToString().Replace(" ", ""), TipoColA) + " " + where(columnasPK, valoresPK, TipoColPK);
                            //EJECUTAMOS LA SENTENCIA
                            NuevoSql.CadnaSentencia = Query;
                            NuevoSql.Sentencia();
                            int fila = DgvDetalleTablas.CurrentCell.RowIndex;
                            CargarDetalle();
                            SeleccionarFila(fila);
                            MessageBox.Show("Registro Editado!", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                #endregion

                //Para la columna de Eliminar
                if (e.ColumnIndex == 2)
                {
                    if (DgvTablas.CurrentCell.Value.ToString().Replace(" ", "").ToString() == "Ciudad")
                    {
                        #region ELIMINAR LA CIUDAD
                        if (MessageBox.Show("Desea eliminar el registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string CodigoCiudad = DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 2].Cells[3].Value.ToString();
                            string CodigoDepar = DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 2].Cells[5].Value.ToString();
                            string Descripcion = DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 2].Cells[4].Value.ToString();

                            string Query = "DELETE FROM [dbo].[Ciudad]  WHERE [Ciud_CodDepto]= '" + CodigoDepar + "'" + " and [Ciud_Codigo]='" + CodigoCiudad + "'";
                            //MessageBox.Show(Query);
                            NuevoSql.CadnaSentencia = Query;
                            NuevoSql.Sentencia();
                            int fila = DgvDetalleTablas.CurrentCell.RowIndex;
                            CargarDetalle();
                            SeleccionarFila(fila);
                            MessageBox.Show("Registro Eliminado!", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}  

                        }
                        #endregion
                    }
                    else
                    {
                        #region ELIMINAR EN LAS DEMAS TABLAS
                        if (MessageBox.Show("Desea eliminar el registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string[] columnasPK = { };
                        string[] valoresPK = { };
                        string[] TipoColPK = { };
                        int y = 0;
                        //llenar los vectores de las columnas y valos que seran Eliminados
                        y = 0;

                        for (int x = 3; x < (ColPK + 3); x++)
                        {
                            y++;
                            Array.Resize(ref columnasPK, y);
                            Array.Resize(ref valoresPK, y);
                            Array.Resize(ref TipoColPK, y);

                            TipoColPK[y - 1] = DgvDetalleTablas.Columns[x].ValueType.ToString();
                            string nombrecolumna = DgvDetalleTablas.Columns[x].Name;
                            columnasPK[y - 1] = nombrecolumna;
                            valoresPK[y - 1] = DgvDetalleTablas.Rows[e.RowIndex].Cells[x].Value.ToString();
                        }
                        //DELETE 
                        string Query = "DELETE FROM " + DgvTablas.CurrentCell.Value.ToString().Replace(" ", "") + " " + where(columnasPK, valoresPK, TipoColPK);
                        NuevoSql.CadnaSentencia = Query;
                        NuevoSql.Sentencia();
                        DgvDetalleTablas.Rows.RemoveAt(e.RowIndex);
                        int fila = DgvDetalleTablas.CurrentCell.RowIndex;
                        CargarDetalle();
                        SeleccionarFila(fila);
                        MessageBox.Show("Registro Eliminado!", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puede completar la operación, revise la información "+ "\r\n" + " Vuelva a intentarlo " ,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void SeleccionarFila(int fila)
        {
            for (int i = 0; i < DgvDetalleTablas.Rows.Count; i++)
            {
                if (DgvDetalleTablas.Rows[i].Cells[1].RowIndex==fila)
                {
                    //MessageBox.Show("Fila " + DgvDetalleTablas.Rows[i].Cells[1].RowIndex);
                    DgvDetalleTablas.CurrentCell = DgvDetalleTablas.Rows[i].Cells[0];
                    break;
                }
            }
        }

        Boolean NewCol = false; //Para saber si ha seleccionado alguna de tablas que se agregan columnas
        Boolean NewCol1 = false; //Para saber si ha seleccionado alguna de tablas que se agregan columnas
        Boolean NewCol2 = false; //Para saber si ha seleccionado alguna de tablas que se agregan columnas
        Boolean NewCol3 = false; //Para saber si ha seleccionado alguna de tablas que se agregan columnas

        public void CargarDetalle()
        {
            try
            {
                if (DgvTablas.CurrentRow.Index > -1) //PARA SABER SI SE HA SELECCIONADO UNA FILA DEL DGV
                {
                    DgvDetalleTablas.DataSource = null; //LIMPIAR EL LA FUENTE DE DATOS DEL DGV
                    //LA TABLA RecomendacionDescripcion A ADICIONAR COLUMNAS  
                    
                    if (DgvTablas.CurrentCell.Value.ToString().Replace(" ", "") == "RecomendacionDescripcion")
                    {
                        if (NewCol == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                        {
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["RecDes_Descripcion"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["RecDes_Codigo"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["RecDes_Recomendacios_Codigo"].Index);
                            NewCol = false;
                        }

                        if (NewCol1 == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                        {
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Revi_Codigo"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Revi_Descripcion"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Revi_Sistema_Codigo"].Index);
                            NewCol1 = false;
                        }
                        if (NewCol2 == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                        {
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["EqDes_Codigo"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["EqDes_Descripcion"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["EqDes_Eqilibrio"].Index);
                            NewCol2 = false;
                        }
                        if (NewCol3 == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                        {
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Ciud_Codigo"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Ciud_Nombre"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Ciud_CodDepto"].Index);
                            NewCol3 = false;
                        }
                        DataGridViewComboBoxColumn colCombo = new DataGridViewComboBoxColumn();//Columna ComboBox
                        DataGridViewTextBoxColumn colTex1 = new DataGridViewTextBoxColumn();//Columna ComboBox
                        DataGridViewTextBoxColumn colTex2 = new DataGridViewTextBoxColumn();//Columna ComboBox

                      //LLENAMOS EL COMBO
                        //colCombo.DataSource = NuevoSql.LlenarTabla("SELECT Sist_Codigo AS Codigo, Sist_Descripcion as Descripcion FROM dbo.Sistema");
                        colCombo.DataSource = NuevoSql.LlenarTabla("SELECT [Reco_Codigo] AS Codigo ,[Reco_Descripcion]  as Descripcion  FROM [dbo].[Recomendacion] ORDER BY Reco_Codigo");
                        colCombo.DisplayMember = "Descripcion";
                        colCombo.ValueMember = "Codigo";
                        //SE CREAN LAS COLUMNAS DEL DGV
                        DgvDetalleTablas.Columns.Add(colTex1);//Agregamos al grid la columna tipo Combo
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].HeaderText = "RecDes_Codigo"; //SE CAMBIA EL TEXTO DE LA COLUMNA
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].Name = "RecDes_Codigo";
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].ValueType = typeof(string);

                        DgvDetalleTablas.Columns.Add(colTex2);//Agregamos al grid la columna tipo Combo
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].HeaderText = "RecDes_Descripcion"; //SE CAMBIA EL TEXTO DE LA COLUMNA
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].Name = "RecDes_Descripcion";
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].ValueType = typeof(string);

                        DgvDetalleTablas.Columns.Add(colCombo);//Agregamos al grid la columna tipo Combo
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].HeaderText = "RecDes_Recomendacios_Codigo"; //SE CAMBIA EL TEXTO DE LA COLUMNA
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].Name = "RecDes_Recomendacios_Codigo";
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].ValueType = typeof(int);

                        NewCol= true;
                        DataTable Tabla = new DataTable();
                        string Query = "SELECT	RecDes_Codigo,                             " +
                                        "dbo.RecomendacionDescripcion.RecDes_Descripcion, RecDes_Recomendacios_Codigo, " +
                                        "dbo.Recomendacion.Reco_Descripcion,dbo.Recomendacion.Reco_Codigo                " +
                                        "FROM	dbo.Recomendacion INNER JOIN               " +
                                        "dbo.RecomendacionDescripcion ON                   " +
                                        "dbo.Recomendacion.Reco_Codigo =                   " +
                                        "dbo.RecomendacionDescripcion.RecDes_Recomendacios_Codigo ORDER BY dbo.Recomendacion.Reco_Codigo";
                        Tabla = NuevoSql.LlenarTabla(Query);
                        NuevoSql.CargarDatos(DgvClavePrimaria, "SELECT i.name AS IndexName,OBJECT_NAME(ic.OBJECT_ID) AS TableName,COL_NAME(ic.OBJECT_ID,ic.column_id) AS ColumnName FROM sys.indexes AS i INNER JOIN sys.index_columns AS ic ON i.OBJECT_ID = ic.OBJECT_ID AND i.index_id = ic.index_id and i.is_primary_key = 1 where OBJECT_NAME(ic.OBJECT_ID)='" + DgvTablas.CurrentCell.Value.ToString().Replace(" ","") + "'");

                        DgvDetalleTablas.RowCount = Tabla.Rows.Count+1;

                        for (int i = 0; i < Tabla.Rows.Count; i++)
                        {
                            DgvDetalleTablas.Rows[i].Cells[3].Value = Tabla.Rows[i]["RecDes_Codigo"];
                            DgvDetalleTablas.Rows[i].Cells[4].Value = Tabla.Rows[i]["RecDes_Descripcion"];
                            DgvDetalleTablas.Rows[i].Cells[5].Value = Tabla.Rows[i]["Reco_Codigo"];
                        }
                        MarcarClavePK();
                        EstilosDgv(DgvDetalleTablas);

                    }
                  //LA TABLA Revision A ADICIONAR COLUMNAS 
                  else if (DgvTablas.CurrentCell.Value.ToString().Replace(" ", "") == "Revision")
                    {
                        if (NewCol1 == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                        {
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Revi_Codigo"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Revi_Descripcion"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Revi_Sistema_Codigo"].Index);
                            NewCol1 = false;
                        }
                        if (NewCol == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                        {
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["RecDes_Descripcion"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["RecDes_Codigo"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["RecDes_Recomendacios_Codigo"].Index);
                            NewCol = false;
                        }
                        if (NewCol2 == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                        {
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["EqDes_Codigo"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["EqDes_Descripcion"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["EqDes_Eqilibrio"].Index);
                            NewCol2 = false;
                        }
                        if (NewCol3 == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                        {
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Ciud_Codigo"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Ciud_Nombre"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Ciud_CodDepto"].Index);
                            NewCol3 = false;
                        }
                        DataGridViewComboBoxColumn colCombo = new DataGridViewComboBoxColumn();//Columna ComboBox
                        DataGridViewTextBoxColumn colTex1 = new DataGridViewTextBoxColumn();//Columna ComboBox
                        DataGridViewTextBoxColumn colTex2 = new DataGridViewTextBoxColumn();//Columna ComboBox

                        //LLENAMOS EL COMBO
                        colCombo.DataSource = NuevoSql.LlenarTabla("SELECT Sist_Codigo AS Codigo, Sist_Descripcion as Descripcion FROM dbo.Sistema");
                        colCombo.DisplayMember = "Descripcion";
                        colCombo.ValueMember = "Codigo";
                        //SE CREAN LAS COLUMNAS DEL DGV
                        DgvDetalleTablas.Columns.Add(colTex1);//Agregamos al grid la columna tipo Combo
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].HeaderText = "Revi_Codigo"; //SE CAMBIA EL TEXTO DE LA COLUMNA
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].Name = "Revi_Codigo";
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].ValueType = typeof(string);

                        DgvDetalleTablas.Columns.Add(colTex2);//Agregamos al grid la columna tipo Combo
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].HeaderText = "Revi_Descripcion"; //SE CAMBIA EL TEXTO DE LA COLUMNA
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].Name = "Revi_Descripcion";
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].ValueType = typeof(string);

                        DgvDetalleTablas.Columns.Add(colCombo);//Agregamos al grid la columna tipo Combo
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].HeaderText = "Revi_Sistema_Codigo"; //SE CAMBIA EL TEXTO DE LA COLUMNA
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].Name = "Revi_Sistema_Codigo";
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].ValueType = typeof(int);

                        NewCol1 = true;
                        DataTable Tabla = new DataTable();
                        string Query = "SELECT dbo.Sistema.Sist_Codigo, dbo.Sistema.Sist_Descripcion, "+
                                         "dbo.Revision.Revi_Codigo, dbo.Revision.Revi_Descripcion,    "+
                                         "dbo.Revision.Revi_Sistema_Codigo                            "+
                                         "FROM dbo.Sistema INNER JOIN dbo.Revision ON                 "+
                                         "dbo.Sistema.Sist_Codigo = dbo.Revision.Revi_Sistema_Codigo ORDER BY Sist_Codigo";
                        Tabla = NuevoSql.LlenarTabla(Query);
                        NuevoSql.CargarDatos(DgvClavePrimaria, "SELECT i.name AS IndexName,OBJECT_NAME(ic.OBJECT_ID) AS TableName,COL_NAME(ic.OBJECT_ID,ic.column_id) AS ColumnName FROM sys.indexes AS i INNER JOIN sys.index_columns AS ic ON i.OBJECT_ID = ic.OBJECT_ID AND i.index_id = ic.index_id and i.is_primary_key = 1 where OBJECT_NAME(ic.OBJECT_ID)='" + DgvTablas.CurrentCell.Value.ToString().Replace(" ","") + "'");

                        DgvDetalleTablas.RowCount = Tabla.Rows.Count + 1;

                        for (int i = 0; i < Tabla.Rows.Count; i++)
                        {
                            DgvDetalleTablas.Rows[i].Cells[3].Value = Tabla.Rows[i]["Revi_Codigo"];
                            DgvDetalleTablas.Rows[i].Cells[4].Value = Tabla.Rows[i]["Revi_Descripcion"];
                            DgvDetalleTablas.Rows[i].Cells[5].Value = Tabla.Rows[i]["Revi_Sistema_Codigo"];
                        }
                        MarcarClavePK();
                        EstilosDgv(DgvDetalleTablas);

                     }

                    else if (DgvTablas.CurrentCell.Value.ToString().Replace(" ", "") == "EquilibrioDes")
                      {                         
                          if (NewCol1 == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                          {
                              DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Revi_Codigo"].Index);
                              DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Revi_Descripcion"].Index);
                              DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Revi_Sistema_Codigo"].Index);
                              NewCol1 = false;
                          }
                          if (NewCol == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                          {
                              DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["RecDes_Descripcion"].Index);
                              DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["RecDes_Codigo"].Index);
                              DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["RecDes_Recomendacios_Codigo"].Index);
                              NewCol = false;
                          }
                          if (NewCol2 == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                          {
                              DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["EqDes_Codigo"].Index);
                              DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["EqDes_Descripcion"].Index);
                              DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["EqDes_Eqilibrio"].Index);
                              NewCol2 = false;
                          }
                          if (NewCol3 == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                          {
                              DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Ciud_Codigo"].Index);
                              DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Ciud_Nombre"].Index);
                              DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Ciud_CodDepto"].Index);
                              NewCol3 = false;
                          }
                          DataGridViewComboBoxColumn colCombo = new DataGridViewComboBoxColumn();//Columna ComboBox
                          DataGridViewTextBoxColumn colTex1 = new DataGridViewTextBoxColumn();//Columna ComboBox
                          DataGridViewTextBoxColumn colTex2 = new DataGridViewTextBoxColumn();//Columna ComboBox

                          //LLENAMOS EL COMBO
                          colCombo.DataSource = NuevoSql.LlenarTabla("SELECT [Equi_Codigo] as Codigo ,[Equi_Descripcion] as Descripcion FROM [dbo].[Equilibro]");
                          colCombo.DisplayMember = "Descripcion";
                          colCombo.ValueMember = "Codigo";
                          //SE CREAN LAS COLUMNAS DEL DGV
                          DgvDetalleTablas.Columns.Add(colTex1);//Agregamos al grid la columna tipo Combo
                          DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].HeaderText = "EqDes_Codigo"; //SE CAMBIA EL TEXTO DE LA COLUMNA
                          DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].Name = "EqDes_Codigo";
                          DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].ValueType = typeof(string);

                          DgvDetalleTablas.Columns.Add(colTex2);//Agregamos al grid la columna tipo Combo
                          DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].HeaderText = "EqDes_Descripcion"; //SE CAMBIA EL TEXTO DE LA COLUMNA
                          DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].Name = "EqDes_Descripcion";
                          DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].ValueType = typeof(string);

                          DgvDetalleTablas.Columns.Add(colCombo);//Agregamos al grid la columna tipo Combo
                          DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].HeaderText = "EqDes_Eqilibrio"; //SE CAMBIA EL TEXTO DE LA COLUMNA
                          DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].Name = "EqDes_Eqilibrio";
                          DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].ValueType = typeof(int);

                          NewCol2 = true;
                          DataTable Tabla = new DataTable();
                          string Query = "SELECT dbo.EquilibrioDes.EqDes_Codigo, " +
                                                                     "dbo.EquilibrioDes.EqDes_Descripcion, " +
                                                                     "dbo.EquilibrioDes.EqDes_Eqilibrio,   " +
                                                                     "dbo.Equilibro.Equi_Descripcion		  " +
                                                                     "FROM  dbo.EquilibrioDes INNER JOIN	  " +
                                                                     "dbo.Equilibro ON dbo.EquilibrioDes." +
                                                                     "EqDes_Eqilibrio = dbo.Equilibro.Equi_Codigo"; 
                          Tabla = NuevoSql.LlenarTabla(Query);
                          NuevoSql.CargarDatos(DgvClavePrimaria, "SELECT i.name AS IndexName,OBJECT_NAME(ic.OBJECT_ID) AS TableName,COL_NAME(ic.OBJECT_ID,ic.column_id) AS ColumnName FROM sys.indexes AS i INNER JOIN sys.index_columns AS ic ON i.OBJECT_ID = ic.OBJECT_ID AND i.index_id = ic.index_id and i.is_primary_key = 1 where OBJECT_NAME(ic.OBJECT_ID)='" + DgvTablas.CurrentCell.Value.ToString().Replace(" ","") + "'");

                          DgvDetalleTablas.RowCount = Tabla.Rows.Count + 1;

                          for (int i = 0; i < Tabla.Rows.Count; i++)
                          {
                              DgvDetalleTablas.Rows[i].Cells[3].Value = Tabla.Rows[i]["EqDes_Codigo"];
                              DgvDetalleTablas.Rows[i].Cells[4].Value = Tabla.Rows[i]["EqDes_Descripcion"];
                              DgvDetalleTablas.Rows[i].Cells[5].Value = Tabla.Rows[i]["EqDes_Eqilibrio"];
                          }
                          MarcarClavePK();
                          EstilosDgv(DgvDetalleTablas);

                      }
                    else if (DgvTablas.CurrentCell.Value.ToString().Replace(" ", "") == "Ciudad")
                    {
                        if (NewCol1 == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                        {
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Revi_Codigo"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Revi_Descripcion"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Revi_Sistema_Codigo"].Index);
                            NewCol1 = false;
                        }
                        if (NewCol == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                        {
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["RecDes_Descripcion"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["RecDes_Codigo"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["RecDes_Recomendacios_Codigo"].Index);
                            NewCol = false;
                        }
                        if (NewCol2 == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                        {
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["EqDes_Codigo"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["EqDes_Descripcion"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["EqDes_Eqilibrio"].Index);
                            NewCol2 = false;
                        }
                        if (NewCol3 == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                        {
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Ciud_Codigo"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Ciud_Nombre"].Index);
                            DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Ciud_CodDepto"].Index);
                            NewCol3 = false;
                        }
                        DataGridViewComboBoxColumn colCombo = new DataGridViewComboBoxColumn();//Columna ComboBox
                        DataGridViewTextBoxColumn colTex1 = new DataGridViewTextBoxColumn();//Columna ComboBox
                        DataGridViewTextBoxColumn colTex2 = new DataGridViewTextBoxColumn();//Columna ComboBox
                        //DataGridViewTextBoxColumn colTex3 = new DataGridViewTextBoxColumn();//Columna ComboBox

                        //LLENAMOS EL COMBO
                        colCombo.DataSource = NuevoSql.LlenarTabla("SELECT [Dept_Codigo] as Codigo ,[Dept_Nombre] as Descripcion  FROM [dbo].[Departamento]");
                        colCombo.DisplayMember = "Descripcion";
                        colCombo.ValueMember = "Codigo";
                        //SE CREAN LAS COLUMNAS DEL DGV
                        //SELECT [Ciud_CodDepto] ,[Ciud_Codigo],[Ciud_Nombre] FROM [dbo].[Ciudad]
                        DgvDetalleTablas.Columns.Add(colTex1);//Agregamos al grid la columna tipo Combo
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].HeaderText = "Ciud_Codigo"; //SE CAMBIA EL TEXTO DE LA COLUMNA
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].Name = "Ciud_Codigo";
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].ValueType = typeof(string);

                        DgvDetalleTablas.Columns.Add(colTex2);//Agregamos al grid la columna tipo Combo
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].HeaderText = "Ciud_Nombre"; //SE CAMBIA EL TEXTO DE LA COLUMNA
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].Name = "Ciud_Nombre";
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].ValueType = typeof(string);

                        DgvDetalleTablas.Columns.Add(colCombo);//Agregamos al grid la columna tipo Combo
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].HeaderText = "Ciud_CodDepto"; //SE CAMBIA EL TEXTO DE LA COLUMNA
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].Name = "Ciud_CodDepto";
                        DgvDetalleTablas.Columns[DgvDetalleTablas.Columns.Count - 1].ValueType = typeof(int);

                        NewCol3 = true;
                        DataTable Tabla = new DataTable();
                        string Query = "SELECT dbo.Ciudad.Ciud_Codigo, "+
		                                "dbo.Ciudad.Ciud_Nombre,       "+
		                                "dbo.Departamento.Dept_Nombre, "+
		                                "dbo.Departamento.Dept_Codigo  "+
                                        "FROM	dbo.Ciudad INNER JOIN  "+
		                                "dbo.Departamento ON           "+
		                                "dbo.Ciudad.Ciud_CodDepto =    "+
		                                "dbo.Departamento.Dept_Codigo";
                        Tabla = NuevoSql.LlenarTabla(Query);
                        //MessageBox.Show(Tabla.Rows.Count.ToString());
                        DgvDetalleTablas.RowCount = Tabla.Rows.Count + 1;
                                                //MessageBox.Show(Tabla.Rows.Count.ToString());
                        //SDFSDF
                        for (int i = 0; i < Tabla.Rows.Count; i++)
                        {
                            DgvDetalleTablas.Rows[i].Cells["Ciud_Codigo"].Value = Tabla.Rows[i]["Ciud_Codigo"];
                            DgvDetalleTablas.Rows[i].Cells["Ciud_Nombre"].Value = Tabla.Rows[i]["Ciud_Nombre"];
                            DgvDetalleTablas.Rows[i].Cells["Ciud_CodDepto"].Value = Tabla.Rows[i]["Dept_Codigo"];
                        }
                        NuevoSql.CargarDatos(DgvClavePrimaria, "SELECT i.name AS IndexName,OBJECT_NAME(ic.OBJECT_ID) AS TableName,COL_NAME(ic.OBJECT_ID,ic.column_id) AS ColumnName FROM sys.indexes AS i INNER JOIN sys.index_columns AS ic ON i.OBJECT_ID = ic.OBJECT_ID AND i.index_id = ic.index_id and i.is_primary_key = 1 where OBJECT_NAME(ic.OBJECT_ID)='" + DgvTablas.CurrentCell.Value.ToString().Replace(" ", "") + "'");
                        //MarcarClavePK();
                        EstilosDgv(DgvDetalleTablas);
                        //MessageBox.Show(Tabla.Rows.Count.ToString());
                        //DFGBH
                    }

                    else
                     {
                         if (NewCol1 == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                         {
                             DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Revi_Codigo"].Index);
                             DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Revi_Descripcion"].Index);
                             DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Revi_Sistema_Codigo"].Index);
                             NewCol1 = false;
                         }
                         if (NewCol == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                         {
                             DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["RecDes_Descripcion"].Index);
                             DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["RecDes_Codigo"].Index);
                             DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["RecDes_Recomendacios_Codigo"].Index);
                             NewCol = false;
                         }
                         if (NewCol2 == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN REVISIÓN
                         {
                             DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["EqDes_Codigo"].Index);
                             DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["EqDes_Descripcion"].Index);
                             DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["EqDes_Eqilibrio"].Index);
                             NewCol2 = false;
                         }
                         //MessageBox.Show(NewCol3.ToString());
                         if (NewCol3 == true) //VERIFICAR SI HAY QUE ELIMINAR LAS COLUMNAS AGREGADAS EN CIUDAD
                         {
                             DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Ciud_Codigo"].Index);
                             DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Ciud_Nombre"].Index);
                             DgvDetalleTablas.Columns.RemoveAt(DgvDetalleTablas.Columns["Ciud_CodDepto"].Index);
                             NewCol3 = false;
                         }
                         NuevoSql.CargarDatos(DgvDetalleTablas, "Select * from " + DgvTablas.CurrentCell.Value.ToString().Replace(" ", ""));
                         NuevoSql.CargarDatos(DgvClavePrimaria, "SELECT i.name AS IndexName,OBJECT_NAME(ic.OBJECT_ID) AS TableName,COL_NAME(ic.OBJECT_ID,ic.column_id) AS ColumnName FROM sys.indexes AS i INNER JOIN sys.index_columns AS ic ON i.OBJECT_ID = ic.OBJECT_ID AND i.index_id = ic.index_id and i.is_primary_key = 1 where OBJECT_NAME(ic.OBJECT_ID)='" + DgvTablas.CurrentCell.Value.ToString().Replace(" ", "") + "'");
                         
                         MarcarClavePK();
                         EstilosDgv(DgvDetalleTablas);

                     }
                    }
                CargarIconosDGV();
            }                   
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void DgvTablas_SelectionChanged_1(object sender, EventArgs e)
        {
            
            CargarDetalle();  

        }

        private void DgvDetalleTablas_EditModeChanged(object sender, EventArgs e)
        {
            
            CargarIconosDGV();
            //DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count -1].Cells[3].Style.BackColor = Color.Red;
            //MessageBox.Show("HOLA MUNDO 1");

        }

        private void DgvDetalleTablas_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 2].DefaultCellStyle.BackColor = Color.SteelBlue;

            CargarIconosDGV();

            //MessageBox.Show("HOLA MUNDO 2");

        }

        private void DgvDetalleTablas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
