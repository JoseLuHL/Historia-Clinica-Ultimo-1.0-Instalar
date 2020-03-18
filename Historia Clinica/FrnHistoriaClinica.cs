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
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;
using System.Net.Sockets;
using Historia_Clinica.Reportes;

namespace Historia_Clinica
{
    public partial class FrnHistoriaClinica : Form
    {
        public FrnHistoriaClinica()
        {
            InitializeComponent();
        }
        //Objeto de la clase que se utiliza donde estan los metodos y demas 
        ClsSqlServer ObjServer = new ClsSqlServer();

        //Procedimiento para cargar la descripcion y codigo a los DGV que lo requieren
        public void CargarColumnas(DataGridView dgv, string query, string ColCode, string ColDes)
        {
            DataTable Tabla = new DataTable();
            Tabla = ObjServer.LlenarTabla(query);
            dgv.RowCount = Tabla.Rows.Count;
            for (int i = 0; i < Tabla.Rows.Count; i++)
            {
                dgv.Rows[i].Cells[ColCode].Value = Tabla.Rows[i]["Codigo"];
                dgv.Rows[i].Cells[ColDes].Value = Tabla.Rows[i]["Descripcion"];
            }
            Tabla = null;
        }
        //-----Fin Procedimiento para cargar-----

        public void tamañoDGV()
        {
            DgvInmunizacionColDosis.Width = 300;
            DgvInmunizacionColFecha.Width = 150;
            DgvInmunizacionColTipoImn.Width = 200;

            DgvAntecedenteFamiliarColEnfermedad.Width = 300;
            DgvAntecedenteFamiliarColMortalidad.Width = 100;
            DgvAntecedenteFamiliarColParentesco.Width = 210;

            DgvAntecedentePersonalColDiagnostico.Width = 180;

            DgvAccidenteLaboralColDias.Width = 100;
            DgvAccidenteLaboralColEmpresa.Width = 210;
            DgvAccidenteLaboralColFecha.Width = 110;
            DgvAccidenteLaboralColParteAfectada.Width = 200;
            DgvAccidenteLaboralNaturaleza.Width = 230;

            DgvRiesgoOcupacionalColCargo.Width = 200;
            DgvRiesgoOcupacionalColEmpresa.Width = 190;
            DgvRiesgoOcupacionalColMeses.Width = 74;

            DgvProbabilidadRiesgoColEstimacion.Width = 100;
            DgvProbabilidadRiesgoColProbabilidad.Width = 150;
            DgvProbabilidadRiesgoColRiesgo.Width = 150;
            DgvProbabilidadRiesgoColTipoRiesgo.Width = 180;

            DgvEnfermedadProfesionalColEmpresa.Width = 250;
            DgvEnfermedadProfesionalColEnfermedad.Width = 220;
            //DgvEnfermedadProfesionalColFecha.Width =2000;
            DgvEnfermedadProfesionalColFechaDiagnostigo.Width = 100;

            DgvExamenPacticadoColAjuntar.Width = 80;
            DgvExamenPacticadoColExamen.Width = 200;
            DgvExamenPacticadoColFecha.Width = 150;
            DgvExamenPacticadoColResultado.Width = 200;

            DgvHabitosColCaracteristica.Width = 220;
            DgvHabitosColFrecuencia.Width = 180;
            //DgvHabitosColHabito.Width = 0;
            DgvHabitosColObservacion.Width = 250;
            DgvAccidenteLaboralColDias.Width = 110;
            DgvExamenLaboratorioColExamenLab.Width = 360;
            //DgvHabitosColTiempoConsumo.Width = 98;
        }

        //Para cambiar la apariencia de los DGV
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
                DGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                DGV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            DGV.AutoResizeColumns();
            DGV.EnableHeadersVisualStyles = false;
        }

        //metodo para cargar los datos de la bd
        public static DataTable Datos()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conexion = new SqlConnection(Historia_Clinica.Conexion.CadenaConexion.cadena());//cadena conexion
                string consulta = "SELECT [Diag_Codigo] as Codigo ,[Diag_Descripcion] as Descripcion FROM [dbo].[Diagnostico]"; //consulta a la tabla paises
                SqlCommand comando = new SqlCommand(consulta, conexion);
                SqlDataAdapter adap = new SqlDataAdapter(comando);
                adap.Fill(dt);
            }
            catch (Exception)
            {
                dt = null;
                //MessageBox.Show("no se puede conectar con la Base de Datos");
            }
            return dt;
        }

        //metodo para cargar la coleccion de datos para el autocomplete
        public static AutoCompleteStringCollection Autocomplete()
        {
            DataTable dt = Datos();
            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            //CboDiagnostico.AutoCompleteCustomSource = Autocomplete();
            //CboDiagnostico.AutoCompleteMode = AutoCompleteMode.Suggest;
            //CboDiagnostico.AutoCompleteSource = AutoCompleteSource.CustomSource;
            try
            {
                //recorrer y cargar los items para el autocompletado
                foreach (DataRow row in dt.Rows)
                {
                    coleccion.Add(Convert.ToString(row["Descripcion"]));
                    coleccion.Add(Convert.ToString(row["Codigo"]));

                }
            }
            catch (Exception)
            {
                coleccion = null;
            }
            return coleccion;
        }

        public void CargarIinicio()
        {
            try
            {
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                TxtMarcha.Clear();
                TxtReflejos.Clear();
                TxtPiel.Clear();
                DgvRecomendaciones.Rows.Clear();
                DgvDiagnostico.Rows.Clear();
                DgvRecomendaciones.Columns.Clear();
                DgvAccidenteLaboral.Rows.Clear();
                DgvAntecedenteFamiliar.Rows.Clear();
                TxtElementosProte.Clear();
                DgvEnfermedadProfesional.Rows.Clear();
                TxtCoceptoTarea.Clear();

                DgvAccidenteLaboral.Rows[DgvAccidenteLaboral.Rows.Count - 1].Cells["DgvAccidenteLaboralColFecha"].Value = "01/01/1990";
                DgvEnfermedadProfesional.Rows[DgvEnfermedadProfesional.Rows.Count - 1].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value = "01/01/1990";
                DgvInmunizacion.Rows[DgvInmunizacion.Rows.Count - 1].Cells["DgvInmunizacionColFecha"].Value = "01/01/1990";
                //DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value

                // Cargo los datos que tendra el combobox DgvExamenPacticadoColResultado

                DgvExamenPacticadoColResultado.DataSource = ObjServer.LlenarTabla("SELECT [TipRes_Codigo] As Codigo  ,[TipRes_Descripcion] As Descripcion  FROM [dbo].[TipResultado]  order by TipRes_Descripcion");
                DgvExamenPacticadoColResultado.DisplayMember = "Descripcion";
                DgvExamenPacticadoColResultado.ValueMember = "Codigo";
                //
                // Cargo los datos que tendra el combobox Concepto
                DataTable tableConcepto = new DataTable();

                CboConcepto.DataSource = ObjServer.LlenarTabla("SELECT [Conc_Codigo] As Codigo,[Conc_Descripcion] As Descripcion FROM [dbo].[Concepto] order by  Conc_Descripcion");
                CboConcepto.DisplayMember = "Descripcion";
                CboConcepto.ValueMember = "Codigo";
                //

                //Cargar combo de tipo examen

                CboTipoExamen.DataSource = ObjServer.LlenarTabla("SELECT [TipoExam_Codigo] As Codigo ,[TipoExam_Descripcion] As Descripcion FROM [dbo].[TipoExamen] order by TipoExam_Descripcion");
                CboTipoExamen.DisplayMember = "Descripcion";
                CboTipoExamen.ValueMember = "Codigo";
                //
                //Examenes de laboratorio - Combo

                DgvExamenLaboratorioColExamenLab.DataSource = ObjServer.LlenarTabla("SELECT [TipExaLabo_Codigo] As Codigo ,[TipExaLabo_Descripcion] As Descripcion FROM [dbo].[TipoExamenLaboratorio] order by TipExaLabo_Descripcion");
                DgvExamenLaboratorioColExamenLab.DisplayMember = "Descripcion";
                DgvExamenLaboratorioColExamenLab.ValueMember = "Codigo";
                DgvExamenLaboratorio.RowCount = 2;
                DgvExamenLaboratorio.Rows[0].Cells["DgvExamenLaboratorioColExamenLab"].Value = 1;

                //

                //Cargar combo de Medicos
                CboProfecional.DataSource = ObjServer.LlenarTabla("SELECT [Medic_TipoIdentificacion]  ,[Medic_Identificacion] , concat([Medic_Nombre1],+' '+[Medic_Nombre2],+' '+[Medic_Apellido1],+' '+[Medic_Apellido2]) as Nombre  FROM [dbo].[Medico]");
                CboProfecional.DisplayMember = "Nombre";
                CboProfecional.ValueMember = "Medic_Identificacion";

                //Cargar combo de Lugar Examen
                CboLugarExamen.DataSource = ObjServer.LlenarTabla("SELECT [Lugar_Codigo] as Codigo  ,[Lugar_Nombre] as Descripcion  FROM [dbo].[LugarRealizacionExamen]");
                CboLugarExamen.DisplayMember = "Descripcion";
                CboLugarExamen.ValueMember = "Codigo";

                DgvAccidenteLaboralColParteAfectada.DataSource = ObjServer.LlenarTabla("SELECT [PartA_Codigo] As Codigo,[PartA_Descripcion] As [Descripcion] FROM [dbo].[ParteAfectada] Order by Descripcion");

                DgvAccidenteLaboralColParteAfectada.DisplayMember = "Descripcion";
                DgvAccidenteLaboralColParteAfectada.ValueMember = "Codigo";
                DgvAccidenteLaboral.Rows[0].Cells[2].Value = 1;
                //MessageBox.Show(ObjServer.x.ToString());

                //Cargar los riesgos al DgvRiesgos
                DgvRiesgoOcupacional.Rows.Clear();
                string query = "SELECT [TipoRiesg_Codigo] As [Codigo], [TipoRiesg_Descripcion] As [Descripcion] FROM [dbo].[TipoRiesgo] order by TipoRiesg_Descripcion";
                string ColCod = "DgvRiesgoOcupacionalColRiesgoCodio";
                string ColDes = "DgvRiesgoOcupacionalColRiesgo";
                DataTable tabla_riesgo = new DataTable();

                tabla_riesgo = ObjServer.LlenarTabla(query);
                DgvRiesgoOcupacionalColRiesgo.DataSource = tabla_riesgo;
                DgvRiesgoOcupacionalColRiesgo.DisplayMember = "Descripcion";
                DgvRiesgoOcupacionalColRiesgo.ValueMember = "Codigo";
                //CargarColumnas(DgvRiesgoOcupacional, query, ColCod, ColDes);
                DgvRiesgoOcupacional.RowCount = tabla_riesgo.Rows.Count + 1;
                for (int i = 0; i < DgvRiesgoOcupacional.Rows.Count - 1; i++)
                {
                    DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiesgo"].Value = tabla_riesgo.Rows[i]["Codigo"];
                    //DgvRiesgoOcupacional.Rows[i].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                }
                //----Fin cargar riesgos----

                //cargar las enfermedades al DgvEnfermedadProfesional

                DgvEnfermedadProfesionalColEnfermedad.DataSource = ObjServer.LlenarTabla("SELECT [Enf_Codigo] as Codigo ,[Enf_Descipcion] as Descripcion FROM [dbo].[Enfermedad] order by Enf_Descipcion ");
                DgvEnfermedadProfesionalColEnfermedad.DisplayMember = "Descripcion";
                DgvEnfermedadProfesionalColEnfermedad.ValueMember = "Codigo";
                //----Fin cargar enfermedades DgvEnfermedadProfesional

                //cargar las enfermedades al DgvInmunizacion
                DgvInmunizacion.Rows.Clear();
                DgvInmunizacionColTipoImn.DataSource = ObjServer.LlenarTabla("SELECT [TipInm_Codigo] as Codigo,[TipInm_Descripcion] as Descripcion  FROM [dbo].[TipoInmunizacion] order by TipInm_Descripcion");
                DgvInmunizacionColTipoImn.DisplayMember = "Descripcion";
                DgvInmunizacionColTipoImn.ValueMember = "Codigo";
                //----Fin cargar los tipos DgvInmunizacion

                query = "";
                ColCod = "";
                ColDes = "";

                //Cargar los antecedentes  al DgvAntecedentes
                DgvAntecedentePersonal.Rows.Clear();
                query = "SELECT [Ant_Codigo] As [Codigo], [Ant_Descripcion] As [Descripcion] FROM [dbo].[Antecedente]";
                ColCod = "DgvAntecedentePersonalColCodigo";
                ColDes = "DgvAntecedentePersonalColAntecedente";
                CargarColumnas(DgvAntecedentePersonal, query, ColCod, ColDes);
                //----Fin cargar Antecedentes----
                //DgvHabitos.Rows.Clear();
                query = "";
                ColCod = "";
                ColDes = "";

                //Cargar Los Habitos al DGVhabitos
                query = "SELECT [Hab_Codigo] As [Codigo] ,[Hab_Descripcion] As [Descripcion] FROM [dbo].[Habito]";
                ColCod = "DgvHabitosColCodigo";
                ColDes = "DgvHabitosColHabito";
                CargarColumnas(DgvHabitos, query, ColCod, ColDes);
                for (int i = 0; i < DgvHabitos.Rows.Count; i++)
                    DgvHabitos.Rows[i].Cells["DgvHabitosColNO"].Value = 1;
                //-----Fin cargar Habitos-----


                //DGVProbabilidad
                //<<<<<<Combo de probabilidad>>>>>>>>>
                DgvProbabilidadRiesgo.Rows.Clear();
                query = "";
                query = "";
                DgvProbabilidadRiesgoColProbabilidad.DataSource = ObjServer.LlenarTabla("SELECT [Prob_Codigo] As [Codigo] ,[Prob_Descripcion] As [Descripcion] FROM [dbo].[Probabilidad] order by Prob_Descripcion");
                DgvProbabilidadRiesgoColProbabilidad.DisplayMember = "Descripcion";
                DgvProbabilidadRiesgoColProbabilidad.ValueMember = "Codigo";

                //Combo de Tipo de Riesgo
                query = "";
                query = "SELECT [TipoRiesg_Codigo] As [Codigo], [TipoRiesg_Descripcion] As [Descripcion] FROM [dbo].[TipoRiesgo] order by TipoRiesg_Descripcion";
                DgvProbabilidadRiesgoColTipoRiesgo.DataSource = ObjServer.LlenarTabla(query);
                DgvProbabilidadRiesgoColTipoRiesgo.DisplayMember = "Descripcion";
                DgvProbabilidadRiesgoColTipoRiesgo.ValueMember = "Codigo";

                //<<<<<<<Fin DGVProbabilidad>>>>>>>>>>

                //DgvExamenPacticadoColResultado
                DgvExamenPacticado.Rows.Clear();
                query = "";
                ColCod = "";
                ColDes = "";
                DataTable tabla = new DataTable();
                ColCod = "DgvExamenPacticadoColCodigo";
                ColDes = "DgvExamenPacticadoColExamen";
                query = "SELECT [Exam_Codigo] As [Codigo] ,[Exam_Descripcion] As [Descripcion] FROM [dbo].[Examen] order by Exam_Descripcion";
                tabla = ObjServer.LlenarTabla(query);
                DgvExamenPacticadoColExamen.DataSource = tabla;
                DgvExamenPacticadoColExamen.DisplayMember = "Descripcion";
                DgvExamenPacticadoColExamen.ValueMember = "Codigo";

                DgvExamenPacticado.RowCount = 1;
                DgvExamenPacticado.Rows[0].Cells["DgvExamenPacticadoColAjuntar"].Value = "......";
                DgvExamenPacticado.Rows[0].Cells["DgvExamenPacticadoColFecha"].Value = "01/01/1990";

                //Cargar columnas al DgvRecomendaciones
                Recomendaciones = ObjServer.LlenarTabla("SELECT [Reco_Codigo] ,[Reco_Descripcion] FROM [dbo].[Recomendacion]");
                DataTable DescripcionReco = new DataTable();
                DescripcionReco = ObjServer.LlenarTabla("SELECT [RecDes_Codigo] ,[RecDes_Descripcion] ,[RecDes_Recomendacios_Codigo]  FROM [dbo].[RecomendacionDescripcion] Order By [RecDes_Recomendacios_Codigo]");
                //Se cargan las columnas de las Recomendaciones
                for (int i = 0; i < Recomendaciones.Rows.Count; i++)
                {
                    //Se agregan 3 columnas la que va  a contener el codigo, la descripcion  y la que va a chekear, la que contienes el codigo se oculta:
                    DgvRecomendaciones.Columns.Add(Recomendaciones.Rows[i]["Reco_Descripcion"].ToString() + i.ToString(), Recomendaciones.Rows[i]["Reco_Descripcion"].ToString() + i.ToString());
                    DgvRecomendaciones.Columns.Add(Recomendaciones.Rows[i]["Reco_Descripcion"].ToString(), Recomendaciones.Rows[i]["Reco_Descripcion"].ToString());

                    DataGridViewCheckBoxColumn SI = new DataGridViewCheckBoxColumn();
                    DgvRecomendaciones.Columns.Add(SI);
                    SI.HeaderText = "SI";
                    SI.Name = "chk" + i.ToString();
                    DgvRecomendaciones.Columns["chk" + i.ToString()].Width = 30;
                    DgvRecomendaciones.Columns[(Recomendaciones.Rows[i]["Reco_Descripcion"].ToString() + i.ToString())].Visible = false;
                    DgvRecomendaciones.Columns[(Recomendaciones.Rows[i]["Reco_Descripcion"].ToString())].ReadOnly = true;
                }

                //llenar la cantidad de filas a un vector
                int[] filasReco = new int[Recomendaciones.Rows.Count];
                for (int i = 0; i < Recomendaciones.Rows.Count; i++)
                {
                    int x = 0;
                    for (int j = 0; j < DescripcionReco.Rows.Count; j++)
                    {
                        if (Recomendaciones.Rows[i]["Reco_Codigo"].ToString() == DescripcionReco.Rows[j]["RecDes_Recomendacios_Codigo"].ToString())
                        {
                            x++;
                        }
                    }
                    filasReco[i] = x;
                }

                //Sacar  la cantidad de filas a agregar al DGV
                int y = 0;
                for (int i = 0; i < filasReco.Length; i++)
                {
                    y = 0;
                    Boolean tru = false;
                    for (int x = 0; x < filasReco.Length; x++)
                    {
                        if (filasReco[i] >= filasReco[x])
                        {
                            y++;
                            if (y >= filasReco.Length)
                            {
                                y = filasReco[i];
                                tru = true;
                                break;
                            }
                        }
                    }
                    if (tru)
                        break;
                }

                //Crear las Filas del DGV
                DgvRecomendaciones.RowCount = y;
                //Llegar las filas del DGV 
                for (int i = 0; i < Recomendaciones.Rows.Count; i++)
                {
                    int x = 0;
                    string Col = Recomendaciones.Rows[i]["Reco_Descripcion"].ToString();
                    string Col2 = Recomendaciones.Rows[i]["Reco_Descripcion"].ToString() + i.ToString();
                    for (int j = 0; j < DescripcionReco.Rows.Count; j++)
                    {
                        if (Recomendaciones.Rows[i]["Reco_Codigo"].ToString() == DescripcionReco.Rows[j]["RecDes_Recomendacios_Codigo"].ToString())
                        {
                            DgvRecomendaciones.Rows[x].Cells[Col].Value = DescripcionReco.Rows[j]["RecDes_Descripcion"].ToString();
                            DgvRecomendaciones.Rows[x].Cells[Col2].Value = DescripcionReco.Rows[j]["RecDes_Codigo"].ToString();

                            x++;
                        }
                    }
                }
                EstilosDgv(DgvRecomendaciones);
                //<<<<<<<<<<<<<<Fin cargar dgv Recomendaciones >>>>>>>>>>>>>>>>>>>

                //Cargar los Revisiones al DGVRevisiones
                Dgvrevision.Rows.Clear();
                Dgvrevision.Columns.Clear();
                Tablasistema = ObjServer.LlenarTabla("SELECT [Sist_Codigo] ,[Sist_Descripcion] FROM [dbo].[Sistema]");
                DataTable TablaRevision = ObjServer.LlenarTabla("SELECT [Revi_Codigo], [Revi_Descripcion], [Revi_Sistema_Codigo] FROM [dbo].[Revision] order by [Revi_Sistema_Codigo]");

                //Cargar columnas al DGVRevision
                for (int i = 0; i < Tablasistema.Rows.Count; i++)
                {
                    Dgvrevision.Columns.Add(Tablasistema.Rows[i]["Sist_Descripcion"].ToString() + i.ToString(), Tablasistema.Rows[i]["Sist_Descripcion"].ToString() + i.ToString());
                    Dgvrevision.Columns.Add(Tablasistema.Rows[i]["Sist_Descripcion"].ToString(), Tablasistema.Rows[i]["Sist_Descripcion"].ToString());
                    DataGridViewCheckBoxColumn SI = new DataGridViewCheckBoxColumn();
                    Dgvrevision.Columns.Add(SI);
                    SI.HeaderText = "SI";
                    SI.Name = "chk" + i.ToString();
                    Dgvrevision.Columns["chk" + i.ToString()].Width = 30;
                    Dgvrevision.Columns[(Tablasistema.Rows[i]["Sist_Descripcion"].ToString() + i.ToString())].Visible = false;
                    Dgvrevision.Columns[(Tablasistema.Rows[i]["Sist_Descripcion"].ToString())].ReadOnly = true;
                }

                //llenar la cantidad de filas a un vector
                int[] filas = new int[Tablasistema.Rows.Count];
                for (int i = 0; i < Tablasistema.Rows.Count; i++)
                {
                    int x = 0;
                    for (int j = 0; j < TablaRevision.Rows.Count; j++)
                    {
                        if (Tablasistema.Rows[i]["Sist_Codigo"].ToString() == TablaRevision.Rows[j]["Revi_Sistema_Codigo"].ToString())
                        {
                            x++;
                        }
                    }
                    filas[i] = x;
                }

                //Sacar  la cantidad de filas a agregar al DGV
                y = 0;
                for (int i = 0; i < filas.Length; i++)
                {
                    y = 0;
                    Boolean tru = false;
                    for (int x = 0; x < filas.Length; x++)
                    {
                        if (filas[i] >= filas[x])
                        {
                            y++;
                            if (y >= filas.Length)
                            {
                                y = filas[i];
                                tru = true;
                                break;
                            }
                        }
                    }
                    if (tru)
                        break;
                }

                //Crear las Filas del DGV
                Dgvrevision.RowCount = y;

                //Llegar las filas del DGV 
                for (int i = 0; i < Tablasistema.Rows.Count; i++)
                {
                    int x = 0;
                    string Col = Tablasistema.Rows[i]["Sist_Descripcion"].ToString();
                    string Col2 = Tablasistema.Rows[i]["Sist_Descripcion"].ToString() + i.ToString();
                    for (int j = 0; j < TablaRevision.Rows.Count; j++)
                    {
                        if (Tablasistema.Rows[i]["Sist_Codigo"].ToString() == TablaRevision.Rows[j]["Revi_Sistema_Codigo"].ToString())
                        {
                            Dgvrevision.Rows[x].Cells[Col].Value = TablaRevision.Rows[j]["Revi_Descripcion"].ToString();
                            Dgvrevision.Rows[x].Cells[Col2].Value = TablaRevision.Rows[j]["Revi_Codigo"].ToString();

                            x++;
                        }
                    }
                }
                EstilosDgv(Dgvrevision);
                //--------Fin Cargar revisiones--------

                //Asignar el nombre a las cajas de texto al contenedor del ciclo menstrual
                TxtHijos.Text = "0";
                TxtHijosSanos.Text = "0";
                TxtGestaciones.Text = "0";
                TxtPartos.Text = "0";
                TxtresultadoCitologia.Text = "Resultado de citología";
                TxtAbostos.Text = "0";
                txtedadmenopausia.Text = "0";
                txtedadmenarca.Text = "0";
                Txtmetodo.Text = "";
                //----Fin asugnar nombres-----

                //Cambiar el color a las cajas de texto al contenedor del ciclo menstrual
                TxtHijos.ForeColor = Color.Gray;
                TxtHijosSanos.ForeColor = Color.Gray;
                TxtGestaciones.ForeColor = Color.Gray;
                TxtPartos.ForeColor = Color.Gray;
                TxtresultadoCitologia.ForeColor = Color.Gray;
                TxtAbostos.ForeColor = Color.Gray;
                //-----Fin Cambiar color-----

                //
                //<<<<<<<<<<<
                ////Placeholder_Leave(TxtConcepto, "Concepto");
                TxtRecomendacion.Text = "Recomendación";
                //Placeholder_Leave(TxtRecomendacion, "Recomendación");
                //>>>>>>>>>>>


                TxtPresionArterial.Text = "0";
                TxtFrecuenciaCardiaca.Text = "0";
                TxtLateracidad.Text = "";
                TxtPeso.Text = "0";
                TxtTalla.Text = "0";
                TxtPerimetroCintura.Text = "0";
                TxtIMC.Text = "0";
                TxtInterpretacion.Text = "Interpretación";

                //<<<<<<<<<<<<<
                Placeholder_Leave(TxtPresionArterial, "0");
                Placeholder_Leave(TxtFrecuenciaCardiaca, "0");
                //Placeholder_Leave(TxtLateracidad, "Lateracidad");
                Placeholder_Leave(TxtPeso, "0");
                Placeholder_Leave(TxtTalla, "0");
                Placeholder_Leave(TxtPerimetroCintura, "0");
                Placeholder_Leave(TxtIMC, "0");
                Placeholder_Leave(TxtInterpretacion, "Interpretación");

                ChkGafas.Checked = false;
                ChkCasco.Checked = false;
                ChkMascarilla.Checked = false;
                ChkOverol.Checked = false;
                ChkBotas.Checked = false;
                ChkProtector.Checked = false;
                ChkRespirador.Checked = false;
                ChkGuantes.Checked = false;

                CARGAR_EXAMEN_FISICO();
                CARGAR_PRUEBA_EQUILIBRIO();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fallo en la operación " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //Quitar el nombre/Descripciín de las cajas de texto del contenedor ciclo mestrual
        public void Placeholder_Enter(TextBox caja, string texto, EventArgs e)
        {
            if (caja.Text == texto)
            {
                caja.Clear();
                caja.ForeColor = Color.Black;
            }
        }

        //Poner el nombre/Descripciín de las cajas de texto del contenedor ciclo mestrual
        public void Placeholder_Leave(TextBox caja, string texto)
        {
            if (caja.Text == "")
            {
                caja.Text = texto;
                //caja.ForeColor = Color.Gray;
            }
        }

        public void Placeholder_Leave(TextBox caja, string texto, EventArgs e)
        {
            if (caja.Text == "")
            {
                caja.Text = texto;
                //caja.ForeColor = Color.Gray;
            }
        }
        //>>>>>>>>>>>>>>>>>>>>>>>>>
        private void TxtHijosSanos_Enter(object sender, EventArgs e)
        {
            Placeholder_Enter(TxtHijosSanos, "0", e);
        }
        private void TxtHijos_Enter(object sender, EventArgs e)
        {
            Placeholder_Enter(TxtHijos, "0", e);
        }

        private void TxtPartos_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtPartos, "0", e); }

        private void TxtAbostos_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtAbostos, "0", e); }

        private void TxtGestaciones_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtGestaciones, "0", e); }

        private void TxtresultadoCitologia_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtresultadoCitologia, "Resultado de citología", e); }
        //<<<<<<<<<<<<<<<<<<<<<<<<<

        //>>>>>>>>>>>>>>>>>>>>>>>>>
        private void TxtHijosSanos_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtHijosSanos, "0", e); }

        private void TxtHijos_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtHijos, "0", e); }

        private void TxtPartos_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtPartos, "0", e); }

        private void TxtAbostos_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtAbostos, "0", e); }

        private void TxtGestaciones_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtGestaciones, "0", e); }

        private void TxtresultadoCitologia_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtresultadoCitologia, "Resultado de citología", e); }
        //<<<<<<<<<<<<<<<<<<<<<<<<<

        private void TxtPresionArterial_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtPresionArterial, "0", e); }

        private void TxtFrecuenciaCardiaca_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtFrecuenciaCardiaca, "0", e); }

        private void TxtLateracidad_Leave(object sender, EventArgs e)
        {
            //Placeholder_Leave(TxtLateracidad, "Lateracidad", e);
        }

        private void TxtPeso_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtPeso, "0", e); }

        private void TxtTalla_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtTalla, "0", e); }

        private void TxtPerimetroCintura_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtPerimetroCintura, "0", e); }

        private void TxtIMC_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtIMC, "0", e); }

        private void TxtInterpretacion_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtInterpretacion, "Interpretación", e); }
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //<<<<<<<<<<<<<<<<<<<<<<<<<<<
        private void TxtPresionArterial_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtPresionArterial, "0", e); }
        private void TxtFrecuenciaCardiaca_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtFrecuenciaCardiaca, "0", e); }

        private void TxtLateracidad_Enter(object sender, EventArgs e)
        {
            //Placeholder_Enter(TxtLateracidad, "Lateracidad", e);
        }

        private void TxtPeso_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtPeso, "0", e); }

        private void TxtTalla_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtTalla, "0", e); }

        private void TxtPerimetroCintura_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtPerimetroCintura, "0", e); }

        private void TxtIMC_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtIMC, "0", e); }

        private void TxtInterpretacion_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtInterpretacion, "Interpretación", e); }

        public async Task CargarHistoriaPaciente(string nada)
        {
            //<<<<<<<<<<<<para preciones la tecla Enter en el control de Buscar>>>>>>>>>>>>>>
            if (TxtDocumento.Text != string.Empty)
            {
                //<<<<<<<<<<<<<<<<<<<<<<
                string Query = "SELECT	dbo.Paciente.Pac_Nombre1+ ' ' + isnull(dbo.Paciente.Pac_Nombre2,'')                         " +
                                "		+' '+dbo.Paciente.Pac_Apellido1+ ' '+isnull(dbo.Paciente.Pac_Apellido2,'') As Nombres,      " +
                                "		dbo.Paciente.Pac_TipoIdentificacion, dbo.Paciente.Pac_Identificacion,                       " +
                                "		dbo.Paciente.Pac_FechaNacimiento, dbo.Genero.Gen_Descripcion,Pac_FechaNacimiento,                               " +
                                "		dbo.Paciente.Pac_CodCiudad, dbo.Paciente.Pac_Direccion,                               " +
                                "		dbo.Paciente.Pac_CodNivelEducativo, dbo.Paciente.Pac_CodProfesion,                         " +
                                "		dbo.TipoSangre.TipSan_Descripcion, dbo.Paciente.Pac_EstadoCivil,                            " +
                                "		dbo.EstadoCivil.EstCivil_Descripcion,Pac_Telefono,Pac_Foto,Pac_CodEPS,Pac_CodARL                                          " +
                                "FROM	dbo.Paciente LEFT OUTER JOIN                                                                " +
                                "		dbo.EstadoCivil ON dbo.Paciente.Pac_EstadoCivil =                                           " +
                                "		dbo.EstadoCivil.EstCivil_Codigo LEFT OUTER JOIN                                             " +
                                "		dbo.TipoSangre ON dbo.Paciente.Pac_TipoSangre =                                             " +
                                "		dbo.TipoSangre.TipSan_Codigo LEFT OUTER JOIN                                                " +
                                "		dbo.Genero ON dbo.Paciente.Pac_CodGenero = dbo.Genero.Gen_Codigo                        " +
                                "WHERE	Pac_Identificacion='" + TxtDocumento.Text + "'";

                await Task.Run(() => { TablaPaciente = ObjServer.LlenarTabla(Query); });

                if (TablaPaciente.Rows.Count > 0)
                {
                    TxtNombre.Text = TablaPaciente.Rows[0]["Nombres"].ToString();
                    string edad = DateTime.Now.Year - Convert.ToDateTime(TablaPaciente.Rows[0]["Pac_FechaNacimiento"]).Year + " años";
                    string sexo = TablaPaciente.Rows[0]["Gen_Descripcion"].ToString();
                    LblDatos2.Text = "Fecha : " + DateTime.Now.Date.ToShortDateString();
                    LblDatos3.Text = "Sexo: " + sexo;
                    LblDatos4.Text = "Edad: " + edad;
                    CboEPS.SelectedValue = TablaPaciente.Rows[0]["Pac_CodEPS"];
                    CboARL.SelectedValue = TablaPaciente.Rows[0]["Pac_CodARL"];

                    if (TablaPaciente.Rows[0]["Pac_Foto"].ToString() != "")
                    {
                        byte[] imageBuffer = (byte[])TablaPaciente.Rows[0]["Pac_Foto"];
                        // Se crea un MemoryStream a partir de ese buffer
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);
                        // Se utiliza el MemoryStream para extraer la imagen
                        PctFoto2.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    //LblEntrada.Text = "";
                    TxtNombre.Clear();
                    CargarIinicio();
                    MessageBox.Show("No hay historia para mostrar, puede que el numero de documento no corresponda a ningún paciente", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                CargarIinicio();

            }
        }

        DataTable TablaPaciente = new DataTable();

        public async Task CargarDiagnostico(int NumeroEntrada)
        {
            DgvDiagnostico.Rows.Clear();
            string Sql = "SELECT   dbo.Diagnostico.Diag_Descripcion,         " +
                            "dbo.DiagnosticoPaciente.DiagPaci_CodDiagnostico " +
                            "FROM     dbo.Diagnostico INNER JOIN             " +
                            "dbo.DiagnosticoPaciente ON                      " +
                            "dbo.Diagnostico.Diag_Codigo =                   " +
                            "dbo.DiagnosticoPaciente.DiagPaci_CodDiagnostico" +
                            " WHERE DiagPaci_NumeroHistoria=" + NumeroEntrada;
            DataTable TablaDiagnostico = new DataTable();
            await Task.Run(() => { TablaDiagnostico = ObjServer.LlenarTabla(Sql); });
            if (TablaDiagnostico.Rows.Count > 0)
            {
                for (int i = 0; i < TablaDiagnostico.Rows.Count; i++)
                {
                    //string Descripcion = TablaDiagnostico.Rows[i]["Diag_Descripcion"].ToString();
                    string Descripcion = TablaDiagnostico.Rows[i]["DiagPaci_CodDiagnostico"].ToString();
                    string Codigo = TablaDiagnostico.Rows[i]["DiagPaci_CodDiagnostico"].ToString();
                    DgvDiagnostico.Rows.Add(Descripcion, Codigo);
                }
            }
        }

        public void CargarHistoriaPaciente()
        {
            //<<<<<<<<<<<<para preciones la tecla Enter en el control de Buscar>>>>>>>>>>>>>>
            if (TxtDocumento.Text != string.Empty)
            {
                //<<<<<<<<<<<<<<<<<<<<<<
                string Query = "SELECT	dbo.Paciente.Pac_Nombre1+ ' ' + isnull(dbo.Paciente.Pac_Nombre2,'')                         " +
                                "		+' '+dbo.Paciente.Pac_Apellido1+ ' '+isnull(dbo.Paciente.Pac_Apellido2,'') As Nombres,      " +
                                "		dbo.Paciente.Pac_TipoIdentificacion, dbo.Paciente.Pac_Identificacion,                       " +
                                "		dbo.Paciente.Pac_FechaNacimiento, dbo.Genero.Gen_Descripcion,Pac_FechaNacimiento,                               " +
                                "		dbo.Paciente.Pac_Direccion,                               " +
                                "		dbo.TipoSangre.TipSan_Descripcion, dbo.Paciente.Pac_EstadoCivil,                            " +
                                "		dbo.EstadoCivil.EstCivil_Descripcion,Pac_Telefono,Pac_Foto,Pac_CodEPS,Pac_CodARL                                           " +
                                "FROM	dbo.Paciente LEFT OUTER JOIN                                                                " +
                                "		dbo.EstadoCivil ON dbo.Paciente.Pac_EstadoCivil =                                           " +
                                "		dbo.EstadoCivil.EstCivil_Codigo LEFT OUTER JOIN                                             " +
                                "		dbo.TipoSangre ON dbo.Paciente.Pac_TipoSangre =                                             " +
                                "		dbo.TipoSangre.TipSan_Codigo LEFT OUTER JOIN                                                " +
                                "		dbo.Genero ON dbo.Paciente.Pac_CodGenero = dbo.Genero.Gen_Codigo                        " +
                                "WHERE	Pac_Identificacion='" + TxtDocumento.Text + "'";

                TablaPaciente = ObjServer.LlenarTabla(Query);

                if (TablaPaciente.Rows.Count > 0)
                {
                    TxtNombre.Text = TablaPaciente.Rows[0]["Nombres"].ToString();
                    string edad = DateTime.Now.Year - Convert.ToDateTime(TablaPaciente.Rows[0]["Pac_FechaNacimiento"]).Year + " años";
                    string sexo = TablaPaciente.Rows[0]["Gen_Descripcion"].ToString();
                    //string fecha = TablaPaciente.Rows[0]["Pac_LugarNacimiento"].ToString();
                    LblDatos2.Text = "Fecha : " + DateTime.Now.Date.ToShortDateString();
                    LblDatos3.Text = "Sexo: " + sexo;
                    LblDatos4.Text = "Edad: " + edad;
                    CboEPS.SelectedValue = TablaPaciente.Rows[0]["Pac_CodEPS"];
                    CboARL.SelectedValue = TablaPaciente.Rows[0]["Pac_CodARL"];

                    //LblDatos2.Text ="Sexo: " + sexo + "  Fecha de Ingreso: " + fecha;
                    //DateTime.Now.Year - Convert.ToDateTime(TablaPaciente.Rows[0]["Pac_FechaNacimiento"]).Year + " años"
                    if (TablaPaciente.Rows[0]["Pac_Foto"].ToString() != "")
                    {
                        byte[] imageBuffer = (byte[])TablaPaciente.Rows[0]["Pac_Foto"];
                        // Se crea un MemoryStream a partir de ese buffer
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);
                        // Se utiliza el MemoryStream para extraer la imagen
                        PctFoto2.Image = Image.FromStream(ms);
                    }
                    //MessageBox.Show(LblEntrada.Text);
                    var entrada = TxtDocumento.Text;
                    bool isNumero = false;
                    if (LblEntrada.Text != string.Empty || LblEntrada.Text != "")
                    {
                        entrada = LblEntrada.Text;
                        isNumero = true;
                    }
                    CargarHistoria(entrada, isNumero);
                }
                else
                {
                    TxtNombre.Clear();
                    CargarIinicio();
                    MessageBox.Show("No hay historia para mostrar", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //>>>>>>>>>>>>>>>>>>>>>>
            }
            else
            {
                CargarIinicio();
            }
            //<<<<<<<<<<<<Fin precionar tecla enter>>>>>>>>>>>>>>>>>>>>>>>
        }

        public async void BuscarTodo()
        {
            CargarHistoriaPaciente();
            CargarIconosDGV();
            CargarDatosExamen();
            if (TxtDocumento.BackColor != Color.SteelBlue)
            {
                await CARGAR_INFORMACION_OCUPACIONAL();
            }
            CARGAR_DOMINANCIA();
        }

        private async void TxtDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            //<<<<<<<<<<<<para preciones la tecla Enter en el control de Buscar>>>>>>>>>>>>>>
            if (e.KeyCode == Keys.Enter)
            {
                if (SooloVer == false)
                {
                    if (MessageBox.Show("¿Desea cargar los datos de la ultima atención del paciente?", "Continuar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        BuscarTodo();
                    }
                    else
                    {
                        await CargarHistoriaPaciente("");

                    }
                }
                // BuscarTodo();
            }
        }

        public void CargarEnfasisEn()
        {
            try
            {
                //Cargar combo de tipo examen
                string Query = "SELECT [Enfa_Codigo] as Codigo ,[Enfa_Descripcion] as Descripcion FROM [dbo].[Enfasis]";
                CboEnfasis.DataSource = ObjServer.LlenarTabla(Query);
                CboEnfasis.DisplayMember = "Descripcion";
                CboEnfasis.ValueMember = "Codigo";
            }
            catch (Exception)
            {

            }
        }

        public void CargarDatosEmpresa()
        {
            SqlConnection conexion = new SqlConnection(Historia_Clinica.Conexion.CadenaConexion.cadena());
            conexion.Open();
            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            string consulta = @"SELECT [Empr_Codigo] ,[Empre_Nit] as nit ,[Empre_RazonSocial] as nombre FROM [dbo].[Empresa]";
            SqlCommand cmd = new SqlCommand(consulta, conexion);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    namesCollection.Add(dr["nombre"].ToString());
                    namesCollection.Add(dr["nit"].ToString() + "-" + dr["nombre"].ToString());
                }
            }

            dr.Close();
            conexion.Close();

            TxtNombreEmpresa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TxtNombreEmpresa.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TxtNombreEmpresa.AutoCompleteCustomSource = namesCollection;
        }

        public void CARGAR_DOMINANCIA()
        {
            if (TxtLateracidad.Text == "")
            {
                DataTable TablaDominancia = new DataTable();

                TablaDominancia = ObjServer.LlenarTabla("SELECT dbo.Dominancia.Dom_Descripcion FROM dbo.Paciente INNER JOIN dbo.Dominancia ON dbo.Paciente.Pac_Dominancia_Codigo = dbo.Dominancia.Dom_Codigo WHERE dbo.Paciente.Pac_Identificacion = '" + TxtDocumento.Text + "'");
                if (TablaDominancia.Rows.Count > 0)
                {
                    TxtLateracidad.Text = TablaDominancia.Rows[0]["Dom_Descripcion"].ToString();
                }
            }
        }

        public async Task<string> CARGAR_INFORMACION_OCUPACIONAL()
        {
            try
            {
                DataTable TablaInfoOcupacional = new DataTable();
                await Task.Run(() =>
                {
                    TablaInfoOcupacional = ObjServer.LlenarTabla("SELECT [InfOcu_Numero]  " +
                                                                  ",[InfOcu_paciente]     " +
                                                                  ",[InfOcu_CodEmpresa]      " +
                                                                  ",[InfOcu_FechaIngreso] " +
                                                                  ",[InfOcu_Jornada]      " +
                                                                  ",[InfOcu_CodOcupacion]    " +
                                                                  ",[InfOcu_Area],InfOcu_CodEmpresa         " +
                                                                  "FROM [dbo].[InformacionOcupacionalProvi] WHERE InfOcu_paciente='" + TxtDocumento.Text + "'");
                });
                if (TablaInfoOcupacional.Rows.Count > 0)
                {
                    CboEmpresa.SelectedValue = TablaInfoOcupacional.Rows[0]["InfOcu_CodEmpresa"];
                    CboCargoOcupacion.SelectedValue = TablaInfoOcupacional.Rows[0]["InfOcu_CodOcupacion"];
                    TxtJornada.Text = TablaInfoOcupacional.Rows[0]["InfOcu_Jornada"].ToString();
                    //TxtNombreEmpresa.Text = TablaInfoOcupacional.Rows[0]["InfOcu_Empresa"].ToString();
                    TxtSesion.Text = TablaInfoOcupacional.Rows[0]["InfOcu_Area"].ToString();
                    DtFechaIngreso.Text = TablaInfoOcupacional.Rows[0]["InfOcu_FechaIngreso"].ToString();
                }
                else
                {
                    //LIMPIAR_CONTROLES();
                    //DtFechaIngreso.Clear();
                }
            }
            catch (Exception)
            {

            }
            return string.Empty;
        }

        public async Task<string> CargarLoad()
        {
            TxtElementosProte.ReadOnly = true;
            //Establecer conexión  con el Gestor.
            ObjServer.CadenaCnn = await Conexion.CadenaConexion.cadenaAsync();
            await ObjServer.ConectarAsync();
            //----Fin Establecer conexión-----

            CargarIinicio();

            // Cargo los datos que tendra el combobox Diagnostico
            CboDiagnostico.DataSource = Datos();
            CboDiagnostico.DisplayMember = "Descripcion";
            CboDiagnostico.ValueMember = "Codigo";

            //Cargo los datos que tendra el combobox de las empresas

            CboEmpresa.DataSource = ObjServer.LlenarTabla("SELECT [Empr_Codigo] As Codigo,[Empre_Nit] As Nit,[Empre_RazonSocial] As Descripcion  FROM [dbo].[Empresa]");
            CboEmpresa.DisplayMember = "Descripcion";
            CboEmpresa.ValueMember = "Codigo";

            //CARGAR COMBO DE TIPOS DE EPS
            await Task.Run(() =>
            {
                CboEPS.DataSource = ObjServer.LlenarTabla("SELECT [Eps_Codigo] ,[Eps_Descripcion] FROM [dbo].[EPS]");
            });
            CboEPS.DisplayMember = "Eps_Descripcion";
            CboEPS.ValueMember = "Eps_Codigo";

            //CARGAR COMBO DE TIPOS DE ARL
            await Task.Run(() =>
            {
                CboARL.DataSource = ObjServer.LlenarTabla("SELECT [Arl_Codigo] ,[Arl_Descripcion]  FROM [dbo].[ARL]");
            });
            CboARL.DisplayMember = "Arl_Descripcion";
            CboARL.ValueMember = "Arl_Codigo";

            //

            // cargo la lista de items para el autocomplete dle combobox
            CboDiagnostico.AutoCompleteCustomSource = Autocomplete();
            CboDiagnostico.AutoCompleteMode = AutoCompleteMode.Suggest;
            CboDiagnostico.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //
            //>>>>>>>>>>>>>
            EstilosDgv(DgvProbabilidadRiesgo);
            EstilosDgv(DgvAccidenteLaboral);
            EstilosDgv(DgvAntecedenteFamiliar);
            EstilosDgv(DgvAntecedentePersonal);
            EstilosDgv(DgvEnfermedadProfesional);
            EstilosDgv(DgvExamenPacticado);
            EstilosDgv(DgvHabitos);
            EstilosDgv(DgvProbabilidadRiesgo);
            EstilosDgv(Dgvrevision);
            EstilosDgv(DgvRiesgoOcupacional);
            EstilosDgv(DgvInmunizacion);
            EstilosDgv(DgvExamenLaboratorio);
            EstilosDgv(DgvRecomendaciones);
            DgvEnfermedadProfesionalColFechaDiagnostigo.Width = 150;
            tamañoDGV();
            TxtHijos.Text = "0";
            TxtHijosSanos.Text = "0";

            CargarDatosEmpresa();
            CargarEnfasisEn();
            CargarIconosDGV();

            //CARGAR LOS DATOS DEL PACIENTE SELECIONADOS
            return string.Empty;
        }

        DataTable Tablasistema = new DataTable();
        DataTable Recomendaciones = new DataTable();

        private async void Form2_Load(object sender, EventArgs e)
        {
            await CargarLoad();

            #region CARGAR COMBO DE OCUPACIONES
            await Task.Run(() => { CboCargoOcupacion.DataSource = ObjServer.LlenarTabla("SELECT [Carg_Codigo]  as Codigo ,[Carg_Descripcion] AS Descripcion FROM [dbo].[Cargo]"); });
            CboCargoOcupacion.DisplayMember = "Descripcion";
            CboCargoOcupacion.ValueMember = "Codigo";
            #endregion

            #region CARGAR COMBO DE SI y NO
            await Task.Run(() => { DgvHabitosColSiNo.DataSource = ObjServer.LlenarTabla("SELECT [SiNo_Codigo] as Codigo,[SiNo_Descripcion] as Descripcion FROM [dbo].[SiNo] ORDER BY Descripcion"); });
            DgvHabitosColSiNo.DisplayMember = "Descripcion";
            DgvHabitosColSiNo.ValueMember = "Codigo";
            //DgvHabitosColSiNo
            for (int i = 0; i < DgvHabitos.Rows.Count; i++)
            {
                DgvHabitos.Rows[i].Cells["DgvHabitosColSiNo"].Value = "NO";
            }
            DgvHabitosColObservacion.ReadOnly = true;
            DgvHabitosColCaracteristica.ReadOnly = true;
            DgvHabitosColFrecuencia.ReadOnly = true;
            DgvHabitosColHabito.ReadOnly = true;
            DgvHabitosColTiempoConsumo.ReadOnly = true;
            DgvHabitosColEliminar.ReadOnly = true;

            DgvHabitosColObservacion.DefaultCellStyle.BackColor = Color.LightGray;
            DgvHabitosColCaracteristica.DefaultCellStyle.BackColor = Color.LightGray;
            DgvHabitosColFrecuencia.DefaultCellStyle.BackColor = Color.LightGray;
            DgvHabitosColHabito.DefaultCellStyle.BackColor = Color.LightGray;
            DgvHabitosColTiempoConsumo.DefaultCellStyle.BackColor = Color.LightGray;
            DgvHabitosColEliminar.DefaultCellStyle.BackColor = Color.LightGray;
            #endregion

            EstilosDgv(DgvDiagnostico);
            DgvDiagnosticoColDiagnostico.Width = 340;
            DgvDiagnosticoColDiagnostico.DefaultCellStyle.BackColor = Color.White;
            DgvEnfermedadProfesionalColFechaDiagnostigo.Width = 150;
            DgvAntecedentePersonalColObservacion.Width = 650;

            if (SooloVer == true)
            {
                BuscarTodo();
            }
            else
            {
                if (MessageBox.Show("¿Desea cargar los datos de la ultima atención del paciente?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    BuscarTodo();
                }
                else
                {
                    await CargarHistoriaPaciente("");
                    await CARGAR_INFORMACION_OCUPACIONAL();
                    CargarDatosExamen();
                }
            }


            if (LblDatos3.Text == "Sexo: Hombre")
            {
                TxtGestaciones.Enabled = false;
                TxtPartos.Enabled = false;
                TxtresultadoCitologia.Enabled = false;
                TxtAbostos.Enabled = false;
                txtedadmenopausia.Enabled = false;
                txtedadmenarca.Enabled = false;
                DtFechaCiclo.Enabled = false;
                //Txtmetodo.Enabled = false;
            }
            DgvExamenFisifoHObservación.Width = 920;
            DgvExamenPacticadoColFecha.MaxInputLength = 10;

        }

        public string SiCeldaEsNula(string valor)
        {
            string retornar = null;
            MessageBox.Show(valor);
            if (valor == null)
                retornar = "";
            else
                retornar = valor;

            return retornar;
        }

        public async Task GUARDAR_HISTORIA(int estado)
        {
            if (
               TxtDocumento.Text != "" ||
               TxtInterpretacion.Text != "" ||
               TxtJornada.Text != "" ||
               TxtLateracidad.Text != "" ||
               TxtMateriaPrima.Text != "" ||
               TxtPartos.Text != "" ||
               TxtPerimetroCintura.Text != "" ||
               TxtPeso.Text != "" ||
               TxtPresionArterial.Text != "" ||
               TxtRecomendacion.Text != "" ||

               TxtTalla.Text != "")
            {

                if (MessageBox.Show("¿Esta seguro de guardar la Informacón? ", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //PRIME SE INSERTA LA ENTRADA - INSERTAR EN ENTRADA - HISTORIA
                    int NumeroEntrada = 0;
                    string Query = "";
                    int reubicacion = 0;
                    if (RdbReuSi.Checked)
                        reubicacion = 1;
                    try
                    {
                        var ARL = CboARL.SelectedValue;
                        var EPS = CboEPS.SelectedValue;

                        if (CboARL.SelectedValue == null)
                            ARL = 0;

                        if (CboEPS.SelectedValue == null)
                            EPS = 0;

                        if (ClsUsuarioLogin.Medico == null || ClsUsuarioLogin.Medico == string.Empty)
                        {
                            MessageBox.Show("No puede continuar, al parecer no es un medico \n vuelva a intentarlo");
                            return;
                        }

                        Query = "INSERT INTO [dbo].[EntradaHistoria]([Entr_IdPaciente],[Entr_FechaEntrada],[Entr_Diagnostico], [Entr_Concepto_Codigo], [Entr_Recomendacion], [Entr_Reubicacion],Entr_TipoExamenCodigo,[Ent_Enfasis],Ent_Estado,Ent_conceptoAptitud,Ent_CodEPS,Ent_CodARL,Ent_Medico,Ent_LugarExamen) " +
                                  "VALUES ('" + TxtDocumento.Text + "', '" + DtFechaEntrada.Text + "' ,'" + CboDiagnostico.SelectedValue.ToString() + "'," + CboConcepto.SelectedValue + ",'" + TxtRecomendacion.Text + "'," + reubicacion + "," + CboTipoExamen.SelectedValue + "," + CboEnfasis.SelectedValue + "," + estado + ",'" + TxtCoceptoTarea.Text + "'," + EPS + "," + ARL + ",'" + CboProfecional.SelectedValue.ToString() + "'," + CboLugarExamen.SelectedValue + ")";

                        ObjServer.CadnaSentencia = Query;
                        ObjServer.Sentencia();
                        //OBTENEMOS EL NUMERO DE LA HISTORIA INSERTADA
                        NumeroEntrada = Convert.ToInt32(ObjServer.LlenarTabla("SELECT  top 1 [Entr_Numero] FROM [dbo].[EntradaHistoria] order by Entr_numero desc").Rows[0][0].ToString());

                        ObjServer.CadenaCnn = await CadenaConexion.cadenaAsync();
                        await ObjServer.ConectarAsync();
                    }
                    catch (Exception EX)
                    {
                        NumeroEntrada = 0;
                        MessageBox.Show(EX.Message);
                        MessageBox.Show("[1] La operación  no puedo completarse debido a: \n 1 - No dispone de una conexión  \n 2 - El registro esta duplicado  \n 3 - La información ingrasado no corresponde a la requerida   \n  - El documento no pertenece a ningún paciente \n  - Diagnostico no ingresado \n  - Diagnostico duplicado  \n Vuelva a intentarlo!!! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //:::::::::Comienzo de la transacción:::::::::::::
                    //Establecemos el Objeto que nos va a permitir conectarnos a la base de Datos()
                    SqlConnection cnn = new SqlConnection(await CadenaConexion.cadenaAsync());
                    //Abrimos la conexión()
                    cnn.Open();
                    //Comenzamos la transacción ()
                    SqlTransaction SQLtrans = cnn.BeginTransaction();
                    try
                    {
                        SqlCommand comman = cnn.CreateCommand();
                        comman.Transaction = SQLtrans;

                        //ELIMINAR LOS EXAMENES PRACTICADOS
                        string QueryI = "DELETE FROM [dbo].[ExamenPracticadoProvi]  WHERE ExaPac_Paciente='" + TxtDocumento.Text + "'";
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        //ELIMINAR 
                        QueryI = "DELETE FROM [dbo].[EntradaProvisional] WHERE Entr_IdPaciente='" + TxtDocumento.Text + "'";
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        #region //INSERTAR EN DIAGNOSTICOS DEL PACIENTE

                        for (int i = 0; i < DgvDiagnostico.Rows.Count - 1; i++)
                        {
                            Query = "INSERT INTO [dbo].[DiagnosticoPaciente]  ([DiagPaci_NumeroHistoria],[DiagPaci_CodDiagnostico]) VALUES (" + NumeroEntrada + ",'" + DgvDiagnostico.Rows[i].Cells["DgvDiagnosticoColDiagnostico"].Value.ToString() + "')";
                            comman.CommandText = Query;
                            comman.ExecuteNonQuery();
                        }

                        #endregion

                        #region //INSERTAR EN EXAMENES DE LABORATORIOS
                        for (int i = 0; i < DgvExamenLaboratorio.Rows.Count - 1; i++)
                        {
                            if (DgvExamenLaboratorio.Rows[i].Cells["DgvExamenLaboratorioColExamenLab"].Value != null)
                            {
                                Query = "INSERT INTO [dbo].[ExamenLaboratorio] ([ExaLabo_Entrada_Numero] ,[ExaLabo_ExamenCodigo]) VALUES (" + NumeroEntrada + "," + DgvExamenLaboratorio.Rows[i].Cells["DgvExamenLaboratorioColExamenLab"].Value + ")";
                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                            }
                        }
                        #endregion

                        #region //INSERTAR EN ACCIDENTE LABORAL PACIENTE
                        //Insertar en AccidenteLaboral
                        if (DgvAccidenteLaboral.Rows.Count >= 1)
                        {
                            for (int i = 0; i < DgvAccidenteLaboral.Rows.Count - 1; i++)
                            {
                                //Para saber si hay datos para agregar
                                string secuela = "";
                                string fecha = "";
                                string empresa = "";
                                string parteafectada = "";
                                string naturaleza = "";
                                string dias = "";

                                if (DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColFecha"].Value != null)
                                    fecha = DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColFecha"].Value.ToString() + "";

                                if (DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColEmpresa"].Value != null)
                                    empresa = DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColEmpresa"].Value.ToString() + "";

                                if (DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColParteAfectada"].Value != null)
                                    parteafectada = DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColParteAfectada"].Value.ToString() + "";

                                if (DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralNaturaleza"].Value != null)
                                    naturaleza = DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralNaturaleza"].Value.ToString() + "";

                                if (DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColDias"].Value != null)
                                    dias = DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColDias"].Value.ToString() + "";

                                if (DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColSecuela"].Value != null)
                                    secuela = DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColSecuela"].Value.ToString() + "";

                                if (empresa != "")
                                {
                                    //Posición del vector DatosAgregar 0 Empresa; 4 Fecha; 1 Naturaleza; 2 ParteAfectada; 3 DiasIncapacidad;  
                                    Query = "INSERT INTO [dbo].[AccidenteLaboral] " +
                                           "([AccLab_Fecha]                 " +
                                           ",[AccLab_Entrada_Numero]        " +
                                           ",[AccLab_Empresa]               " +
                                           ",[AccLab_Naturaleza]            " +
                                           ",[AccLab_ParteAfectada_Codigo]  " +
                                           ",[AccLab_DiasIncapacidad],AccLab_Secuela)      " +
                                           "VALUES (" + "'" + fecha + "'," + NumeroEntrada + ",'" + empresa + "','" + naturaleza + "'," + parteafectada + ",'" + dias + "','" + secuela + "')";

                                    comman.CommandText = Query;
                                    comman.ExecuteNonQuery();
                                }
                            }
                        }
                        #endregion

                        #region //INSERTAR EN RIESGO OCUPACIONAL DEL PACIENTE
                        //      //Insertar en DgvRiesgoOcupacional
                        for (int i = 0; i < DgvRiesgoOcupacional.Rows.Count - 1; i++)
                        {
                            //Para saber si hay datos para agregar
                            int riesgo = 0;
                            string empresa = "";
                            string cargo = "";
                            string meses = "";
                            string riesgoespecifico = "";

                            if (DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiesgo"].Value != null)
                            {
                                if (DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColCargo"].Value != null)
                                    cargo = DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColCargo"].Value.ToString();
                                else
                                    cargo = "";
                                if (DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColEmpresa"].Value != null)
                                    empresa = DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColEmpresa"].Value.ToString() + "";
                                else
                                    empresa = "";

                                //if (DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiesgo"].Value!=null)                                    
                                riesgo = Convert.ToInt32(DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiesgo"].Value);

                                if (DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColMeses"].Value != null)
                                    meses = DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColMeses"].Value.ToString() + "";
                                else
                                    meses = "";
                                if (DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiengoEspecifico"].Value != null)
                                    riesgoespecifico = DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiengoEspecifico"].Value.ToString() + "";
                                else
                                    riesgoespecifico = "";

                                //if (empresa != "" || riesgoespecifico != "" || cargo != "" || meses != "")
                                //{
                                Query = "INSERT INTO [dbo].[RiesgoOcupacional] " +
                                       "([RiegOcu_Riesgo_Codigo]    " +
                                       ",[RiegOcu_Entrada_Numero]   " +
                                       ",[RiegOcu_Empresa]          " +
                                       ",[RiegOcu_Cargo]     " +
                                       ",[RiegOcu_Meses],RiegOcu_RiesgoEspecifico)           " +
                                       "VALUES (" + "" + riesgo + "," + NumeroEntrada + ",'" + empresa + "','" + cargo + "','" + meses + "','" + riesgoespecifico + "')";

                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                                //}
                            }
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        #endregion


                        for (int i = 0; i < DgvProbabilidadRiesgo.Rows.Count - 1; i++)
                        {
                            int TipoRiesgo = 0;
                            string Riesgo = "";
                            string probabilidad = "";
                            string estimacion = "";
                            if (DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColTipoRiesgo"].Value != null)
                            {
                                if (DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColRiesgo"].Value != null)
                                    Riesgo = DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColRiesgo"].Value.ToString();

                                if (DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColEstimacion"].Value != null)
                                    estimacion = DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColEstimacion"].Value.ToString();

                                if (DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColTipoRiesgo"].Value != null)
                                    TipoRiesgo = Convert.ToInt32(DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColTipoRiesgo"].Value);

                                if (DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColProbabilidad"].Value != null)
                                    probabilidad = DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColProbabilidad"].Value.ToString();

                                //DatosAgregar[x] = DgvRiesgoOcupacional.Rows[i].Cells[x].Value.ToString();

                                //Insertar en Probabilidad de Riesgo
                                Query = "INSERT INTO [dbo].[ProbabilidadRiego] " +
                                      "([ProbRiesg_Riesgo_Codigo]       " +
                                      ",[ProbRiesg_Entrada_Numero]      " +
                                      ",[ProbRiesg_TipoRiesgo_Codigo]   " +
                                      ",[ProbRiesg_Probabilidad_Codigo] " +
                                      ",[ProbRiesg_Estimacion])         " +
                                          "VALUES (" + "'" + Riesgo + "'," + NumeroEntrada + ",'" + TipoRiesgo + "'," + probabilidad + ",'" + estimacion + "')";
                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                            }
                        } //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                        #region //INSERTAR EN ENFERMEDADES PROFESIONALES DEL PACIENTE
                        //Insertar DgvEnfermedadProfesional
                        for (int i = 0; i < DgvEnfermedadProfesional.Rows.Count - 1; i++)
                        {
                            //Para saber si hay datos para agregar
                            Boolean confirmar = false;
                            int enfermedad = 0;
                            string empresa = "";
                            string Fecha = "";
                            //string Diagnostico = "";

                            //DgvProbabilidadRiesgoColProbabilidad;
                            if (DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColEmpresa"].Value != null || DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColFecha"].Value != null)
                            {
                                if (DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value != null)
                                    Fecha = DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value.ToString();
                                else
                                    Fecha = DtFecha.Text;

                                if (DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColEmpresa"].Value != null)
                                    empresa = DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColEmpresa"].Value.ToString() + "";
                                else
                                    empresa = "";

                                enfermedad = Convert.ToInt32(DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColEnfermedad"].Value);

                                Query = "INSERT INTO [dbo].[EnfermedadProfesional] " +
                                            "([EnfPro_Enfermedad_Codigo]               " +
                                            ",[EnfPro_Entrada_Numero]                  " +
                                            ",[EnfPro_Empresa]                         " +
                                            ",[EnfPro_FechaDiagnostico])                " +
                                               "VALUES (" + "" + enfermedad + "," + NumeroEntrada + ",'" + empresa + "','" + Fecha + "')";

                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                            }
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR EN INFORMACIÓN OCUPACIONAL DEL PACIENTE
                        //insertar en información ocupacional
                        Query = "INSERT INTO [dbo].[InformacionOcupacional] (" +
                            "[InfOcu_Entrada_Numero]        " +
                            ",[InfOcu_FechaIngreso]         " +
                            ",[InfOcu_FechaCargoActual]     " +
                            ",[InfOcu_Jornada]              " +
                            ",[InfOcu_CodOcupacion]            " +
                            ",[InfOcu_Area]                 " +
                            ",[InfOcu_DescripcionFunciones] " +
                            ",[InfOcu_Maquinaria]           " +
                            ",[InfOcu_Herramienta]          " +
                            ",[InfOcu_MateriaPrima]         " +
                            ",InfOcu_ElementoProte          " +
                            ",InfOcu_CodEmpresa)               " +
                            " VALUES(" + NumeroEntrada + ",'" + DtFechaIngreso.Value.ToShortDateString() + "','" + DtFecha.Value.ToShortDateString() + "','" + TxtJornada.Text + "'," + CboCargoOcupacion.SelectedValue + ",'" + TxtSesion.Text + "','" + TxtDescripcionFunciones.Text + "','" + TxtEquipo.Text + "','" + TxtHerramienta.Text + "','" + TxtMateriaPrima.Text + "','" + TxtElementosProte.Text + "'," + CboEmpresa.SelectedValue + ")";

                        comman.CommandText = Query;
                        comman.ExecuteNonQuery();
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR EN ANTECEDENTE FAMILIAR DEL PACIENTE
                        //Insertar DgvAntecedenteFamiliar
                        for (int i = 0; i < DgvAntecedenteFamiliar.Rows.Count - 1; i++)
                        {
                            //Para saber si hay datos para agregar
                            Boolean confirmar = false;
                            string enfermedad;
                            string parentesco = "";
                            Boolean mortalidad;
                            int mortalidad2;

                            //DgvAntecedenteFamiliar;
                            //if (DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColParentesco"].Value != null)
                            //{
                            if (DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColEnfermedad"].Value != null)
                                enfermedad = DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColEnfermedad"].Value.ToString() + "";
                            else
                                enfermedad = "";

                            if (DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColParentesco"].Value != null)
                                parentesco = DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColParentesco"].Value.ToString() + "";
                            else
                                parentesco = "";

                            mortalidad2 = Convert.ToInt32(DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColMortalidad"].Value);
                            confirmar = true;
                            //}
                            //else
                            //{ confirmar = false; break; }

                            //if (confirmar == true)
                            //{
                            //    ////Insertar en DgvAntecedenteFamiliar

                            Query = "INSERT INTO [dbo].[AntecednteFamiliar] " +
                                       "([AntFam_Enfermedad_Codigo] " +
                                       ",[AntFam_Entrada_Numero]    " +
                                       ",[AntFam_Parentesco]        " +
                                       ",[AntFam_Mortalidad])       " +
                                       "VALUES (" + "'" + enfermedad + "'," + NumeroEntrada + ",'" + parentesco + "','" + mortalidad2 + "')";

                            comman.CommandText = Query;
                            comman.ExecuteNonQuery();
                            //}
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR EN ANTECEDENTES PERSONALES DEL PACIENTE
                        //Insertar DgvAntecedentePersonal
                        for (int i = 0; i < DgvAntecedentePersonal.Rows.Count; i++)
                        {
                            //Para saber si hay datos para agregar
                            Boolean confirmar = false;
                            string antecedente; ;
                            string diagnostico = "";
                            string observacion = "";
                            if (DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColDiagnostico"].Value != null || DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColDiagnostico"].Value == null)
                            {
                                if (DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColCodigo"].Value != null)
                                    antecedente = DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColCodigo"].Value.ToString() + "";
                                else
                                    antecedente = "";

                                if (DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColDiagnostico"].Value != null)
                                    diagnostico = DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColDiagnostico"].Value.ToString() + "";
                                else
                                    diagnostico = "";

                                if (DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColObservacion"].Value != null)
                                    observacion = DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColObservacion"].Value.ToString() + "";
                                else
                                    observacion = "";
                                if (observacion != "")
                                {
                                    ////Insertar en DgvAntecedentePersonal
                                    Query = "INSERT INTO [dbo].[AntecedentePersonal] " +
                                               "([AntPer_Antecedende_Codigo] " +
                                               ",[AntPer_Entrada_Numero]     " +
                                               ",[AntPer_Diagnostico]        " +
                                               ",[AntPer_Observacion])       " +
                                               "VALUES (" + "'" + antecedente + "'," + NumeroEntrada + ",'" + diagnostico + "','" + observacion + "')";
                                    comman.CommandText = Query;
                                    comman.ExecuteNonQuery();
                                }
                            }
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR EN EXAMENES PRCTICADOS DEL PACIENTE
                        //Insertar EXAMEN PRACTICADOS
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        for (int i = 0; i < DgvExamenPacticado.Rows.Count - 1; i++)
                        {
                            //if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value.ToString().Length > 7 )
                            //{
                            Boolean confirmar = false; //Para saber si hay datos para agregar
                            int CodExamen = 0;
                            string FechaExa = "";
                            int resultado = 0;
                            resultado = Convert.ToInt32(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value);
                            FechaExa = "01/01/1999";

                            if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value != null)
                            {
                                if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value.ToString() != "")
                                {
                                    FechaExa = DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value.ToString();
                                }
                            }
                            //MessageBox.Show(FechaExa);
                            CodExamen = Convert.ToInt32(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value);
                            confirmar = true;
                            Query = "INSERT INTO [dbo].[ExamenPracticado] VALUES (@examen" + i + ",@entrada" + i + ",@resultado" + i + ",@foto" + i + ",@fecha" + i + ")";

                            comman.CommandText = Query;
                            comman.Parameters.Add("@examen" + i, SqlDbType.Int);
                            comman.Parameters.Add("@resultado" + i, SqlDbType.Int);
                            comman.Parameters.Add("@foto" + i, SqlDbType.Image);
                            comman.Parameters.Add("@fecha" + i, SqlDbType.Date);
                            comman.Parameters.Add("@entrada" + i, SqlDbType.Int);

                            comman.Parameters["@entrada" + i].Value = NumeroEntrada;
                            comman.Parameters["@examen" + i].Value = CodExamen;
                            comman.Parameters["@resultado" + i].Value = resultado;
                            comman.Parameters["@fecha" + i].Value = FechaExa;
                            PctFoto.Image = null;
                            ms = new MemoryStream();
                            string Imagen = DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value.ToString();
                            if (Imagen == "System.Byte[]")
                            {
                                byte[] imageBuffer = (byte[])DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value;
                                // Se crea un MemoryStream a partir de ese buffer
                                System.IO.MemoryStream ms1 = new System.IO.MemoryStream(imageBuffer);
                                // Se utiliza el MemoryStream para extraer la imagen
                                this.PctFoto.Image = Image.FromStream(ms1);
                                //MessageBox.Show(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value.ToString());
                                //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                                //MessageBox.Show("HOLA"+i);
                                PctFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                                comman.Parameters["@foto" + i].Value = ms.GetBuffer();

                            }
                            else
                            {
                                comman.Parameters["@foto" + i].Value = DBNull.Value;
                            }

                            comman.ExecuteNonQuery();
                        }
                        #endregion

                        #region //INSERTAR EN CICLO MENSTRUAL DEL PACIENTE
                        //insertar en Ciclo Menstrual
                        if (ChkCicloMenstrual.Checked == true)
                        {
                            int planificacion = 0;
                            if (RdbCicloSi.Checked)
                                planificacion = 1;
                            Query = "INSERT INTO [dbo].[CicloMenstrual] " +
                                       "([CicMens_Entrada_Numero]      " +
                                       ",[CicMens_FechaUltimaRegla]    " +
                                       ",[CicMens_HijosSanos]          " +
                                       ",[CicMens_Gestaciones]         " +
                                       ",[CicMens_Partos]              " +
                                       ",[CicMens_Abortos]             " +
                                       ",[CicMens_Hijos]               " +
                                       ",[CicMens_ResultadoCitologia]  " +
                                       ",[CicMens_Planificacion] ,[CicMens_Edadmenopausia],[CicMens_Edadmenarca],[CicMens_metodo]) " +
                                         " VALUES(" + NumeroEntrada + ",'" + DtFechaCiclo.Text + "','" +
                                                TxtHijosSanos.Text + "','" + TxtGestaciones.Text + "','" +
                                                TxtPartos.Text + "','" + TxtAbostos.Text + "','" + TxtHijos.Text + "','" +
                                                TxtresultadoCitologia.Text + "','" + planificacion + "','" + txtedadmenopausia.Text + "','" + txtedadmenarca.Text + "','" + Txtmetodo.Text + "')";

                            comman.CommandText = Query;
                            comman.ExecuteNonQuery();
                            //<<<<<<<<<<<<<<<<<<<<<<<<<<<< 
                        }
                        #endregion

                        #region //INSERTAR EN HABITOS DEL PACIENTE
                        //Insertar DgvHabitos
                        for (int i = 0; i < DgvHabitos.Rows.Count; i++)
                        {
                            if (DgvHabitos.Rows[i].Cells["DgvHabitosColSiNo"].Value.ToString() == "SI" || DgvHabitos.Rows[i].Cells["DgvHabitosColSiNo"].Value.ToString() == "Si")
                            {
                                //Para saber si hay datos para agregar
                                Boolean confirmar = false;
                                int habito = 0;
                                string caracteristica = "";
                                string frecuencia = "";
                                string tiempo = "";
                                string observacion = "";
                                if (DgvHabitos.Rows[i].Cells["DgvHabitosColTiempoConsumo"].Value != null)
                                    tiempo = DgvHabitos.Rows[i].Cells["DgvHabitosColTiempoConsumo"].Value.ToString();
                                else
                                    tiempo = "";

                                if (DgvHabitos.Rows[i].Cells["DgvHabitosColCaracteristica"].Value != null)
                                    caracteristica = DgvHabitos.Rows[i].Cells["DgvHabitosColCaracteristica"].Value.ToString();
                                else
                                    caracteristica = "";

                                if (DgvHabitos.Rows[i].Cells["DgvHabitosColObservacion"].Value != null)
                                    observacion = DgvHabitos.Rows[i].Cells["DgvHabitosColObservacion"].Value.ToString();
                                else
                                    observacion = "";

                                if (DgvHabitos.Rows[i].Cells["DgvHabitosColFrecuencia"].Value != null)
                                    frecuencia = DgvHabitos.Rows[i].Cells["DgvHabitosColFrecuencia"].Value.ToString();
                                else
                                    frecuencia = "";
                                habito = Convert.ToInt32(DgvHabitos.Rows[i].Cells["DgvHabitosColCodigo"].Value);
                                //MessageBox.Show(habito.ToString());
                                //if (caracteristica!="" || frecuencia !="" || tiempo != "" || observacion != "")
                                //{
                                Query = "INSERT INTO [dbo].[HabitoPaciente] " +
                                      "([HabPac_Habito_Codigo]  " +
                                      ",[HabPac_Entrada_Numero] " +
                                      ",[HabPac_Caracteristica] " +
                                      ",[HabPac_Frecuencia]     " +
                                      ",[HabPac_Tiempo]         " +
                                      ",[HabPac_Observacion])   " +
                                        "VALUES (" + "" + habito + "," + NumeroEntrada + ",'" + caracteristica + "','" + frecuencia + "','" + tiempo + "','" + observacion + "')";
                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                                //}                                
                            }
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR EN INMUNIZACIÓN DEL PACIENTE
                        //Insertar DgvInmunizacion
                        for (int i = 0; i < DgvInmunizacion.Rows.Count - 1; i++)
                        {
                            //Para saber si hay datos para agregar
                            Boolean confirmar = false;
                            int TipoInmu = 0;
                            string Dosis = "";
                            string fecha = "";

                            if (DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColFecha"].Value != null || DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColDosis"].Value != null)
                            {
                                TipoInmu = Convert.ToInt32(DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColTipoImn"].Value);
                                if (DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColDosis"].Value != null)
                                    Dosis = DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColDosis"].Value.ToString() + "";
                                else
                                    Dosis = "";

                                if (DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColFecha"].Value != null)
                                    fecha = DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColFecha"].Value.ToString() + "";
                                else
                                    fecha = DtFecha.Text;

                                confirmar = true;
                            }
                            else
                            { confirmar = false; break; }

                            if (confirmar == true)
                            {
                                ////Insertar en DgvInmunizacion
                                Query = "INSERT INTO [dbo].[Inmunizar]      " +
                                           "([Inmu_Entrada_Numero]          " +
                                           ",[Inmu_TipoInmunizacion_Codigo] " +
                                           ",[Inmu_Fecha]                   " +
                                           ",[Inmu_Dosis])                  " +
                                            "VALUES (" + "" + NumeroEntrada + "," + TipoInmu + ",'" + fecha + "','" + Dosis + "')";
                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                            }
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR LAS RECOMENDACIONES DEL PACIENTE
                        //Insertar en RECOMENDACIONES (DGV):
                        if (ListRecomendaciones.Count > 0)
                        {
                            for (int i = 0; i < ListRecomendaciones.Count; i++)
                            {
                                int CODIGO = Convert.ToInt32(ListRecomendaciones[i]);
                                ////Insertar en Dgvrevision
                                Query = "INSERT INTO [dbo].[RecomendacionPaciente] " +
                                                           "([RecoPac_Entrada_Numero] " +
                                                           ",[RecoPac_Recomendacion_Codigo]) " +
                                                               "VALUES (" + "" + NumeroEntrada + "," + CODIGO + ")";
                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                            }
                        }
                        //for (int i = 0; i < DgvRecomendaciones.Columns.Count; i++)
                        //{
                        //    for (int x = 0; x < Recomendaciones.Rows.Count; x++)
                        //    {
                        //        string Col = Recomendaciones.Rows[x]["reco_Descripcion"].ToString();
                        //        string Col2 = Recomendaciones.Rows[x]["reco_Descripcion"].ToString() + x.ToString();

                        //        if (DgvRecomendaciones.Columns[i].Name == Col2)
                        //        {
                        //            for (int z = 0; z < DgvRecomendaciones.Rows.Count; z++)
                        //            {
                        //                if (DgvRecomendaciones.Rows[z].Cells[Col2].Value != null)
                        //                {
                        //                    if (Convert.ToBoolean(DgvRecomendaciones.Rows[z].Cells["chk" + x.ToString()].Value) == true)
                        //                    {
                        //                        int CODIGO = Convert.ToInt32(DgvRecomendaciones.Rows[z].Cells[Col2].Value);
                        //                        ////Insertar en Dgvrevision
                        //                        Query = "INSERT INTO [dbo].[RecomendacionPaciente] " +
                        //                                   "([RecoPac_Entrada_Numero] " +
                        //                                   ",[RecoPac_Recomendacion_Codigo]) " +
                        //                                       "VALUES (" + "" + NumeroEntrada + "," + CODIGO + ")";
                        //                        comman.CommandText = Query;
                        //                        comman.ExecuteNonQuery();
                        //                        DgvRecomendaciones.Rows[z].Cells["chk" + x.ToString()].Value = false;
                        //                        //Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Style.BackColor = Color.Red;
                        //                    }
                        //                }
                        //                else
                        //                    break;
                        //            }
                        //        }
                        //    }

                        //}
                        //<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR LAS REVICIONES DEL PACIENTE
                        if (RevisionSistema.Count > 0)
                        {
                            for (int i = 0; i < RevisionSistema.Count; i++)
                            {
                                int CODIGO = Convert.ToInt32(RevisionSistema[i]);
                                ////Insertar en Dgvrevision
                                Query = "INSERT INTO [dbo].[RevisionSistema] " +
                                           "([RevSist_Entrada_Numero]   " +
                                           ",[RevSist_Revision_Codigo]) " +
                                               "VALUES (" + "" + NumeroEntrada + "," + CODIGO + ")";

                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                                //Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Value = false;
                                //Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Style.BackColor = Color.Red;
                            }
                            //RevisionSistema.Clear();
                        }

                        //Insertar en Dgvrevision
                        //for (int i = 0; i < Dgvrevision.Columns.Count; i++)
                        //{
                        //    for (int x = 0; x < Tablasistema.Rows.Count; x++)
                        //    {
                        //        string Col = Tablasistema.Rows[x]["Sist_Descripcion"].ToString();
                        //        string Col2 = Tablasistema.Rows[x]["Sist_Descripcion"].ToString() + x.ToString();

                        //        if (Dgvrevision.Columns[i].Name == Col2)
                        //        {
                        //            //MessageBox.Show(Col2);
                        //            for (int z = 0; z < Dgvrevision.Rows.Count; z++)
                        //            {
                        //                if (Dgvrevision.Rows[z].Cells[Col2].Value != null)
                        //                {
                        //                    if (Convert.ToBoolean(Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Value) == true)
                        //                    {
                        //                        int CODIGO = Convert.ToInt32(Dgvrevision.Rows[z].Cells[Col2].Value);
                        //                        ////Insertar en Dgvrevision
                        //                        Query = "INSERT INTO [dbo].[RevisionSistema] " +
                        //                                   "([RevSist_Entrada_Numero]   " +
                        //                                   ",[RevSist_Revision_Codigo]) " +
                        //                                       "VALUES (" + "" + NumeroEntrada + "," + CODIGO + ")";


                        //                        comman.CommandText = Query;
                        //                        comman.ExecuteNonQuery();
                        //                        Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Value = false;
                        //                        //Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Style.BackColor = Color.Red;
                        //                    }
                        //                }
                        //                else
                        //                    break;
                        //            }
                        //        }
                        //    }

                        //}
                        //<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR EN EXAMENES FISICOS DEL PACIENTE
                        //INSERTAR EN EXAMENES FISICO
                        for (int i = 0; i < DgvExamenFisifoH.Rows.Count; i++)
                        {
                            string Codigo = DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHCodigo"].Value.ToString();
                            string observacion;

                            if (DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHObservación"].Value != null)
                                observacion = DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHObservación"].Value.ToString() + "";
                            else
                                observacion = "";

                            string examen = DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHCodigo"].Value.ToString();
                            string ESTA = DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoH___"].Value.ToString();

                            string Quiery = "INSERT INTO [dbo].[Examen_Paciente] " +
                                          "([ExamPaci_Examen_Codigo]           " +
                                          ",[ExamPaci_Numero_Entrada]          " +
                                          ",[ExamPaci_Observacion],ExamPaci_Estado)            " +
                                          "  VALUES                            " +
                                          "(" + examen + "," + NumeroEntrada + ",'" + observacion + "'," + ESTA + ")";
                            comman.CommandText = Quiery;
                            comman.ExecuteNonQuery();

                        }
                        ///////////////////////
                        #endregion


                        #region //INSERTAR EN PRUEBA DE EQUILIBRIO
                        int col = -1;
                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            if (column.Visible == false)
                            {
                                col++;
                                //MessageBox.Show(dataGridView1.Rows[1].Cells["combo0"].Value.ToString());
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    if (dataGridView1.Rows[i].Cells[column.Name].Value != null)
                                    {
                                        string valorEquilibro;
                                        string valorestado;

                                        if (dataGridView1.Rows[i].Cells[column.Name].Value != null)
                                            valorEquilibro = dataGridView1.Rows[i].Cells[column.Name].Value.ToString();
                                        else
                                            valorEquilibro = "";

                                        valorestado = dataGridView1.Rows[i].Cells["combo" + col.ToString()].Value.ToString();

                                        string query = "INSERT INTO [dbo].[EquilibroPaciente] " +
                                                       "([EqiPa_Equilibro]                   " +
                                                       ",[EqiPa_HistoriaNumero]              " +
                                                       ",[EqiPa_Estado], " +
                                                       "EqiPa_Reflejos, " +
                                                       "EqiPa_Marcha,EqiPa_Piel) " +
                                                       " VALUES                              " +
                                                       "(" + valorEquilibro + "," + NumeroEntrada + "," + valorestado + ",'" + TxtReflejos.Text + "','" + TxtMarcha.Text + "','" + TxtPiel.Text + "')";
                                        //ObjServer.CadnaSentencia = query;
                                        //ObjServer.Sentencia();
                                        comman.CommandText = query;
                                        comman.ExecuteNonQuery();
                                        //MessageBox.Show(valorCol + " " + valorcom);
                                    }
                                }
                            }
                        }
                        #endregion

                        #region //INSERTAR EN EXAMEN FISICO DEL PACIENTE
                        //insertar en Examenes Fisico
                        string Presion = TxtPresionArterial.Text;
                        Query = "INSERT INTO [dbo].[ExamenFisico] " +
                                   "([ExaFisi_Entrada_Numero]     " +
                                   ",[ExaFisi_PresionArterial]    " +
                                   ",[ExaFisi_FrecuenciaCardiaca] " +
                                   ",[ExaFisi_Lateracidad]        " +
                                   ",[ExaFisi_Peso]               " +
                                   ",[ExaFisi_Talla]              " +
                                   ",[ExaFisi_PerimetroCintura]   " +
                                   ",[ExaFisi_IMC]                " +
                                   ",[ExaFisi_Interpretacion])    " +
                                        " VALUES(" + NumeroEntrada + ",'" + TxtPresionArterial.Text.Trim() + "'," +
                                            TxtFrecuenciaCardiaca.Text + ",'" + TxtLateracidad.Text + "','" +
                                            TxtPeso.Text + "','" + TxtTalla.Text + "','" + TxtPerimetroCintura.Text + "','" +
                                            TxtIMC.Text + "','" + TxtInterpretacion.Text + "')";
                        #endregion

                        comman.CommandText = Query;
                        comman.ExecuteNonQuery();
                        LblEntrada.Text = NumeroEntrada.ToString();
                        SQLtrans.Commit();
                        TxtDocumento.BackColor = Color.SteelBlue;
                        if (MessageBox.Show("Número de la atención #" + LblEntrada.Text + " guardada correctamente \n ¿Desea ver el reporte?", "Finalizar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            CargarReporte();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (NumeroEntrada != 0)
                        {
                            MessageBox.Show(ex.ToString());
                            MessageBox.Show("La operación no pudo completarse debido a: \n 1 - No dispone de una conexión  \n 2 - El registro esta duplicado  \n 3 - La información ingrasado no corresponde a la requerida   \n  - El documento no coresponde a ningún paciente \n  - Diagnostico no ingresado o repetido \n  - Examenes practicados sin resultado  \n Verifique y vuelva a intentarlo!!! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            SQLtrans.Rollback();

                            ObjServer.CadnaSentencia = "DELETE FROM [dbo].[EntradaHistoria] WHERE Entr_Numero=" + NumeroEntrada;
                            ObjServer.Sentencia();
                        }
                    }
                }
            }
        }

        public async Task<string> GuardarAsync()
        {
            await Task.Run(() => { GUARDAR_HISTORIA(0); });
            return string.Empty;
        }

        private async void BtnGuardarHistoria_Click(object sender, EventArgs e)
        {
            if (DgvDiagnostico.Rows.Count <= 1)
            {
                if (MessageBox.Show("No hay diagnosticos agregados ¿Desea continuar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (TxtDocumento.BackColor == Color.SteelBlue)
                    {
                        if (MessageBox.Show("Realmente desea guardar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            int NumeroEntrada = Convert.ToInt32(LblEntrada.Text);
                            ActualizarDatos(NumeroEntrada, 0);
                        }
                    }
                    else
                    {
                        //await GuardarAsync();
                        await GUARDAR_HISTORIA(0);
                    }
                }
            }
            else
            {
                if (TxtDocumento.BackColor == Color.SteelBlue)
                {
                    if (MessageBox.Show("Realmente desea guardar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        int NumeroEntrada = Convert.ToInt32(LblEntrada.Text);
                        ActualizarDatos(NumeroEntrada, 0);
                    }
                }
                else
                {
                    //await GuardarAsync();
                    await GUARDAR_HISTORIA(0);
                }
            }

        }

        //>>>>>>>>>FIN METODO GUARDAR>>>>>>>>>>>>>>
        public async void CargarHistoria(string identificacion, bool isNumero)
        {
            DataTable tabla = new DataTable();
            if (isNumero)
                tabla = ObjServer.LlenarTabla("SELECT  top 1 [Entr_Numero] FROM [dbo].[EntradaHistoria] WHERE [Entr_Numero] ='" + identificacion + "' order by Entr_numero desc");
            else
                tabla = ObjServer.LlenarTabla("SELECT  top 1 [Entr_Numero] FROM [dbo].[EntradaHistoria] WHERE [Entr_IdPaciente] ='" + identificacion + "' order by Entr_numero desc");

            if (tabla.Rows.Count > 0)
            {
                //Cargar los datos al DgvAccidenteLaboral
                DataTable Datos = new DataTable();
                int NumeroEntrada;
                //MessageBox.Show(LblEntrada.Text);
                if (LblEntrada.Text == "")
                {
                    LblEntrada.Text = tabla.Rows[0]["Entr_Numero"].ToString();
                    NumeroEntrada = Convert.ToInt32(tabla.Rows[0][0].ToString());
                }
                else
                {
                    NumeroEntrada = Convert.ToInt32(LblEntrada.Text);
                }

                Datos = ObjServer.LlenarTabla("SELECT  dbo.AccidenteLaboral.AccLab_Empresa,            " +
                                                "dbo.AccidenteLaboral.AccLab_Naturaleza,            " +
                                                "dbo.ParteAfectada.PartA_Descripcion,               " +
                                                "dbo.AccidenteLaboral.AccLab_DiasIncapacidad,       " +
                                                "dbo.AccidenteLaboral.AccLab_Fecha,                 " +
                                                "dbo.AccidenteLaboral.AccLab_Entrada_Numero,        " +
                                                "dbo.AccidenteLaboral.AccLab_ParteAfectada_Codigo,AccLab_Secuela   " +
                                                "FROM dbo.AccidenteLaboral INNER JOIN               " +
                                                "dbo.ParteAfectada ON                               " +
                                                "dbo.AccidenteLaboral.AccLab_ParteAfectada_Codigo = " +
                                                "dbo.ParteAfectada.PartA_Codigo WHERE dbo.AccidenteLaboral.AccLab_Entrada_Numero=" + NumeroEntrada);
                CARGAR_DATOS_EXAMEN_FISICO(NumeroEntrada);
                CARGAR_DATOS_PUEBA_EQUILIBRIO(NumeroEntrada);
                await CargarDiagnostico(NumeroEntrada);
                //CARGAR_EXAMENES_PRACTICADOS();
                if (Datos.Rows.Count > 0)
                {
                    DgvAccidenteLaboral.RowCount = Datos.Rows.Count + 1;
                    for (int i = 0; i < Datos.Rows.Count; i++)
                    {
                        DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColEmpresa"].Value = Datos.Rows[i]["AccLab_Empresa"].ToString();
                        DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralNaturaleza"].Value = Datos.Rows[i]["AccLab_Naturaleza"].ToString();
                        DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColParteAfectada"].Value = Datos.Rows[i]["AccLab_ParteAfectada_Codigo"];
                        DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColDias"].Value = Datos.Rows[i]["AccLab_DiasIncapacidad"].ToString();
                        DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColSecuela"].Value = Datos.Rows[i]["AccLab_Secuela"].ToString();
                        string fecha = Convert.ToDateTime(Datos.Rows[i]["AccLab_Fecha"]).ToString("dd/MM/yyyy");
                        DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColFecha"].Value = fecha;
                    }
                }
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<

                //CARGAR LOS DATOS AL DgvRiesgoOcupacional
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT dbo.RiesgoOcupacional.RiegOcu_Riesgo_Codigo, " +
                                               "dbo.RiesgoOcupacional.RiegOcu_Empresa,           " +
                                               "dbo.RiesgoOcupacional.RiegOcu_Cargo,             " +
                                               "dbo.RiesgoOcupacional.RiegOcu_Meses,             " +
                                               "dbo.EntradaHistoria.Entr_Numero,RiegOcu_RiesgoEspecifico                  " +
                                               "FROM dbo.RiesgoOcupacional INNER JOIN            " +
                                               "dbo.EntradaHistoria                              " +
                                               "ON dbo.RiesgoOcupacional.RiegOcu_Entrada_Numero =" +
                                               "dbo.EntradaHistoria.Entr_Numero WHERE dbo.EntradaHistoria.Entr_Numero= " + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    DgvRiesgoOcupacional.Rows.Clear();
                    DgvRiesgoOcupacional.RowCount = Datos.Rows.Count + 1;
                    for (int i = 0; i < Datos.Rows.Count; i++)
                    {

                        DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiesgo"].Value = Datos.Rows[i]["RiegOcu_Riesgo_Codigo"];
                        DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColEmpresa"].Value = Datos.Rows[i]["RiegOcu_Empresa"];
                        DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColCargo"].Value = Datos.Rows[i]["RiegOcu_Cargo"];
                        DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColMeses"].Value = Datos.Rows[i]["RiegOcu_Meses"];
                        DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiengoEspecifico"].Value = Datos.Rows[i]["RiegOcu_RiesgoEspecifico"];
                    }
                }
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                //CARGAR LOS DATOS EN PROBABILIDAD DE RIESGO
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT dbo.ProbabilidadRiego.ProbRiesg_TipoRiesgo_Codigo,                    " +
                                                    "dbo.ProbabilidadRiego.ProbRiesg_Riesgo_Codigo,                        " +
                                                    "dbo.Probabilidad.Prob_Descripcion,                                    " +
                                                    "dbo.ProbabilidadRiego.ProbRiesg_Estimacion,                           " +
                                                    "dbo.ProbabilidadRiego.ProbRiesg_Probabilidad_Codigo,                  " +
                                                    "dbo.Probabilidad.Prob_Codigo, dbo.TipoRiesgo.TipoRiesg_Descripcion,   " +
                                                    "dbo.TipoRiesgo.TipoRiesg_Codigo,                                      " +
                                                    "dbo.ProbabilidadRiego.ProbRiesg_Entrada_Numero                        " +
                                                    "FROM dbo.Probabilidad INNER JOIN                                      " +
                                                    "dbo.ProbabilidadRiego ON dbo.Probabilidad.Prob_Codigo =               " +
                                                    "dbo.ProbabilidadRiego.ProbRiesg_Probabilidad_Codigo INNER JOIN        " +
                                                    "dbo.TipoRiesgo ON dbo.ProbabilidadRiego.ProbRiesg_TipoRiesgo_Codigo = " +
                                                    "dbo.TipoRiesgo.TipoRiesg_Codigo WHERE dbo.ProbabilidadRiego.ProbRiesg_Entrada_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    DgvProbabilidadRiesgo.RowCount = Datos.Rows.Count + 1;
                    for (int i = 0; i < Datos.Rows.Count; i++)
                    {
                        DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColTipoRiesgo"].Value = Datos.Rows[i]["ProbRiesg_TipoRiesgo_Codigo"];
                        DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColRiesgo"].Value = Datos.Rows[i]["ProbRiesg_Riesgo_Codigo"];
                        DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColProbabilidad"].Value = Datos.Rows[i]["ProbRiesg_Probabilidad_Codigo"];
                        DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColEstimacion"].Value = Datos.Rows[i]["ProbRiesg_Estimacion"];

                    }
                }
                //<<<<<<<<<<<<<<<<<<<<<<<<<<

                //CARGAR LOS DATOS EN ENFERMEDAD PROFECIONAL.
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT dbo.EnfermedadProfesional.EnfPro_Enfermedad_Codigo,              " +
                                                "dbo.EnfermedadProfesional.EnfPro_Entrada_Numero,                     " +
                                                "dbo.EnfermedadProfesional.EnfPro_Empresa,                            " +
                                                "dbo.EnfermedadProfesional.EnfPro_FechaDiagnostico,                   " +
                                                "dbo.Enfermedad.Enf_Codigo, dbo.Enfermedad.Enf_Descipcion             " +
                                                "FROM dbo.EnfermedadProfesional INNER JOIN                            " +
                                                "dbo.Enfermedad ON dbo.EnfermedadProfesional.EnfPro_Enfermedad_Codigo " +
                                                "= dbo.Enfermedad.Enf_Codigo WHERE EnfPro_Entrada_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {

                    DgvEnfermedadProfesional.RowCount = Datos.Rows.Count + 1;
                    for (int i = 0; i < Datos.Rows.Count; i++)
                    {
                        DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColEnfermedad"].Value = Datos.Rows[i]["EnfPro_Enfermedad_Codigo"];
                        DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColEmpresa"].Value = Datos.Rows[i]["EnfPro_Empresa"];
                        string fecha = Convert.ToDateTime(Datos.Rows[i]["EnfPro_FechaDiagnostico"]).ToString("dd/MM/yyyy");
                        DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value = fecha;
                    }
                }
                //<<<<<<<<<<<<<<<<<<<<<<<<
                //CARGAR LOS DATOS EN ANTECEDENTES FAMILIARES.
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT dbo.AntecednteFamiliar.AntFam_Numero,       " +
                                               "dbo.AntecednteFamiliar.AntFam_Enfermedad_Codigo,  " +
                                               "dbo.AntecednteFamiliar.AntFam_Entrada_Numero,     " +
                                               "dbo.AntecednteFamiliar.AntFam_Parentesco,         " +
                                               "dbo.AntecednteFamiliar.AntFam_Mortalidad          " +
                                               "FROM   dbo.AntecednteFamiliar  WHERE AntFam_Entrada_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    DgvAntecedenteFamiliar.RowCount = Datos.Rows.Count + 1;
                    for (int i = 0; i < Datos.Rows.Count; i++)
                    {
                        DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColEnfermedad"].Value = Datos.Rows[i]["AntFam_Enfermedad_Codigo"];
                        DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColParentesco"].Value = Datos.Rows[i]["AntFam_Parentesco"];
                        int mortalidad = 0;
                        if (Convert.ToBoolean(Datos.Rows[i]["AntFam_Mortalidad"]) == true)
                        {
                            mortalidad = 1;
                        }
                        DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColMortalidad"].Value = mortalidad;
                    }
                }
                //<<<<<<<<<<<<<<<<<<<<

                //CARGAR LOS DATOS EN ANTECEDENTES PERSONALES.
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT dbo.AntecedentePersonal.AntPer_Numero,                       " +
                                                       "dbo.AntecedentePersonal.AntPer_Antecedende_Codigo,         " +
                                                       "dbo.AntecedentePersonal.AntPer_Entrada_Numero,             " +
                                                       "dbo.AntecedentePersonal.AntPer_Diagnostico,                " +
                                                       "dbo.AntecedentePersonal.AntPer_Observacion,                " +
                                                       "dbo.Antecedente.Ant_Descripcion                            " +
                                                       "FROM dbo.AntecedentePersonal INNER JOIN dbo.Antecedente ON " +
                                                       "dbo.AntecedentePersonal.AntPer_Antecedende_Codigo =        " +
                                                       "dbo.Antecedente.Ant_Codigo WHERE dbo.AntecedentePersonal.AntPer_Entrada_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    for (int i = 0; i < Datos.Rows.Count; i++)
                    {
                        DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColCodigo"].Value = Datos.Rows[i]["AntPer_Antecedende_Codigo"];
                        DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColAntecedente"].Value = Datos.Rows[i]["Ant_Descripcion"];
                        DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColDiagnostico"].Value = Datos.Rows[i]["AntPer_Diagnostico"];
                        DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColObservacion"].Value = Datos.Rows[i]["AntPer_Observacion"];
                    }
                }
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                //CARGAR LOS DATOS EN INMUNIZACIÓN.
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT dbo.Inmunizar.Inmu_Entrada_Numero,               " +
                                                   "dbo.Inmunizar.Inmu_TipoInmunizacion_Codigo,        " +
                                                   "dbo.Inmunizar.Inmu_Fecha, dbo.Inmunizar.Inmu_Dosis," +
                                                   "dbo.TipoInmunizacion.TipInm_Descripcion            " +
                                                   "FROM dbo.Inmunizar INNER JOIN dbo.TipoInmunizacion " +
                                                   "ON dbo.Inmunizar.Inmu_TipoInmunizacion_Codigo      " +
                                                   "= dbo.TipoInmunizacion.TipInm_Codigo WHERE  dbo.Inmunizar.Inmu_Entrada_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    DgvInmunizacion.RowCount = Datos.Rows.Count + 1;
                    for (int i = 0; i < Datos.Rows.Count; i++)
                    {
                        DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColTipoImn"].Value = Datos.Rows[i]["Inmu_TipoInmunizacion_Codigo"];
                        string fecha = Convert.ToDateTime(Datos.Rows[i]["Inmu_Fecha"]).ToString("dd/MM/yyyy");
                        DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColFecha"].Value = fecha;
                        DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColDosis"].Value = Datos.Rows[i]["Inmu_Dosis"];
                    }
                }
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                //CARGAR LOS DATOS EN HABITOS.
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT dbo.HabitoPaciente.HabPac_Habito_Codigo,            " +
                                                   "dbo.HabitoPaciente.HabPac_Entrada_Numero,             " +
                                                   "dbo.HabitoPaciente.HabPac_Caracteristica,             " +
                                                   "dbo.HabitoPaciente.HabPac_Frecuencia,                 " +
                                                   "dbo.HabitoPaciente.HabPac_Tiempo,                     " +
                                                   "dbo.HabitoPaciente.HabPac_Observacion,                " +
                                                   "dbo.Habito.Hab_Descripcion FROM dbo.Habito INNER JOIN " +
                                                   "dbo.HabitoPaciente ON dbo.Habito.Hab_Codigo =         " +
                                                   "dbo.HabitoPaciente.HabPac_Habito_Codigo WHERE dbo.HabitoPaciente.HabPac_Entrada_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    //DgvHabitos.Rows.Clear();
                    //DgvHabitos.RowCount = Datos.Rows.Count;
                    //MessageBox.Show(DgvHabitos.Rows.Count.ToString());
                    for (int i = 0; i < DgvHabitos.Rows.Count; i++)
                    {
                        for (int i2 = 0; i2 < Datos.Rows.Count; i2++)
                        {
                            if (DgvHabitos.Rows[i].Cells["DgvHabitosColCodigo"].Value.ToString() == Datos.Rows[i2]["HabPac_Habito_Codigo"].ToString())
                            {
                                DgvHabitos.Rows[i].Cells["DgvHabitosColSiNo"].Value = "SI";
                                DgvHabitos.Rows[i].Cells["DgvHabitosColCodigo"].Value = Datos.Rows[i2]["HabPac_Habito_Codigo"];
                                DgvHabitos.Rows[i].Cells["DgvHabitosColHabito"].Value = Datos.Rows[i2]["Hab_Descripcion"];
                                DgvHabitos.Rows[i].Cells["DgvHabitosColFrecuencia"].Value = Datos.Rows[i2]["HabPac_Frecuencia"];
                                DgvHabitos.Rows[i].Cells["DgvHabitosColCaracteristica"].Value = Datos.Rows[i2]["HabPac_Caracteristica"];
                                DgvHabitos.Rows[i].Cells["DgvHabitosColObservacion"].Value = Datos.Rows[i2]["HabPac_Observacion"];
                                DgvHabitos.Rows[i].Cells["DgvHabitosColTiempoConsumo"].Value = Datos.Rows[i2]["HabPac_Tiempo"];
                                DesactivarHabitos(i);
                                break;
                            }

                        }
                    }

                }
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                //CARGAR en DGVRECOMENDACIONES
                Datos = new DataTable();
                ListRecomendaciones.Clear();
                Datos = ObjServer.LlenarTabla("SELECT	dbo.Recomendacion.Reco_Descripcion,               " +
                                               "dbo.Recomendacion.Reco_Codigo,                           " +
                                               "dbo.RecomendacionDescripcion.RecDes_Descripcion,         " +
                                               "dbo.RecomendacionDescripcion.RecDes_Codigo,              " +
                                               "dbo.EntradaHistoria.Entr_Numero                          " +
                                               "FROM dbo.Recomendacion INNER JOIN                        " +
                                               "dbo.RecomendacionDescripcion ON                          " +
                                               "dbo.Recomendacion.Reco_Codigo =                          " +
                                               "dbo.RecomendacionDescripcion.RecDes_Recomendacios_Codigo " +
                                               "INNER JOIN dbo.RecomendacionPaciente ON                  " +
                                               "dbo.RecomendacionDescripcion.RecDes_Codigo =             " +
                                               "dbo.RecomendacionPaciente.RecoPac_Recomendacion_Codigo   " +
                                               "INNER JOIN dbo.EntradaHistoria ON                        " +
                                               "dbo.RecomendacionPaciente.RecoPac_Entrada_Numero =       " +
                                               "dbo.EntradaHistoria.Entr_Numero                          " +
                                               "WHERE dbo.EntradaHistoria.Entr_Numero=" + NumeroEntrada);

                for (int i = 0; i < DgvRecomendaciones.Columns.Count; i++)
                {
                    for (int x = 0; x < Recomendaciones.Rows.Count; x++)
                    {
                        string Col = Recomendaciones.Rows[x]["reco_Descripcion"].ToString();
                        string Col2 = Recomendaciones.Rows[x]["reco_Descripcion"].ToString() + x.ToString();

                        if (DgvRecomendaciones.Columns[i].Name == Col2)
                        {
                            for (int z = 0; z < DgvRecomendaciones.Rows.Count; z++)
                            {
                                if (DgvRecomendaciones.Rows[z].Cells[Col2].Value != null)
                                {
                                    for (int y = 0; y < Datos.Rows.Count; y++)
                                    {
                                        if (DgvRecomendaciones.Rows[z].Cells[Col2].Value.ToString() == Datos.Rows[y]["RecDes_Codigo"].ToString())
                                        {
                                            DgvRecomendaciones.Rows[z].Cells["chk" + x.ToString()].Value = true;
                                            ListRecomendaciones.Add(Datos.Rows[y]["RecDes_Codigo"].ToString());
                                        }
                                    }
                                }
                                else
                                    break;
                            }
                        }
                    }

                }//<<<<<<<<<<<<<<<<<<<<

                //CARGAR en Dgvrevision
                Datos = new DataTable();
                RevisionSistema.Clear();
                Datos = ObjServer.LlenarTabla("SELECT dbo.Sistema.Sist_Descripcion,                        " +
                                                   "dbo.Sistema.Sist_Codigo,                               " +
                                                   "dbo.Revision.Revi_Descripcion,                         " +
                                                   "dbo.Revision.Revi_Codigo,                              " +
                                                   "dbo.RevisionSistema.RevSist_Entrada_Numero             " +
                                                   "FROM dbo.Revision INNER JOIN                           " +
                                                   "dbo.RevisionSistema ON dbo.Revision.Revi_Codigo =      " +
                                                   "dbo.RevisionSistema.RevSist_Revision_Codigo INNER JOIN " +
                                                   "dbo.Sistema ON dbo.Revision.Revi_Sistema_Codigo =      " +
                                                   "dbo.Sistema.Sist_Codigo                                " +
                                                   "WHERE dbo.RevisionSistema.RevSist_Entrada_Numero=" + NumeroEntrada);

                for (int i = 0; i < Dgvrevision.Columns.Count; i++)
                {
                    for (int x = 0; x < Tablasistema.Rows.Count; x++)
                    {
                        string Col = Tablasistema.Rows[x]["Sist_Descripcion"].ToString();
                        string Col2 = Tablasistema.Rows[x]["Sist_Descripcion"].ToString() + x.ToString();

                        if (Dgvrevision.Columns[i].Name == Col2)
                        {
                            for (int z = 0; z < Dgvrevision.Rows.Count; z++)
                            {
                                if (Dgvrevision.Rows[z].Cells[Col2].Value != null)
                                {
                                    for (int y = 0; y < Datos.Rows.Count; y++)
                                    {
                                        if (Dgvrevision.Rows[z].Cells[Col2].Value.ToString() == Datos.Rows[y]["Revi_Codigo"].ToString())
                                        {
                                            Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Value = true;
                                            RevisionSistema.Add(Datos.Rows[y]["Revi_Codigo"].ToString());
                                            //MessageBox.Show(TablaRevision.Rows[j]["Revi_Codigo"].ToString());
                                        }
                                    }
                                }
                                else
                                    break;
                            }
                        }
                    }

                }//<<<<<<<<<<<<<<<<<<<<

                //CARGAR LOS DATOS EN EXAMENES  LABORATORIO.
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT [ExaLabo_Entrada_Numero],[ExaLabo_ExamenCodigo] FROM [dbo].[ExamenLaboratorio] WHERE ExaLabo_Entrada_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    DgvExamenLaboratorio.RowCount = Datos.Rows.Count + 1;
                    for (int i = 0; i < Datos.Rows.Count; i++)
                    {
                        DgvExamenLaboratorio.Rows[i].Cells["DgvExamenLaboratorioColExamenLab"].Value = Datos.Rows[i]["ExaLabo_ExamenCodigo"];

                    }
                }
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                //CARGAR LOS DATOS EN EXAMENES  PRACTICADOS:::
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT [ExaPrac_Examen_Codigo], [ExaPrac_Entrada_Numero], [ExaPrac_Resultado], [ExaPrac_Ajuntar], [ExaPrac_FechaExamen] FROM [dbo].[ExamenPracticado] WHERE ExaPrac_Entrada_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    DgvExamenPacticado.RowCount = Datos.Rows.Count + 1;
                    for (int i = 0; i < Datos.Rows.Count; i++)
                    {
                        //DFGHJK
                        //MessageBox.Show(Datos.Rows[i]["ExaPrac_Examen_Codigo"].ToString());
                        DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value = Datos.Rows[i]["ExaPrac_Examen_Codigo"];
                        DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value = Datos.Rows[i]["ExaPrac_Ajuntar"];

                        if (Datos.Rows[i]["ExaPrac_FechaExamen"].ToString().Substring(0, 10).Trim() != "1/01/1999")
                        {
                            DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value = Datos.Rows[i]["ExaPrac_FechaExamen"];
                        }
                        else
                        {
                            DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value = "";
                            DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].ReadOnly = true;
                        }
                        DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value = Datos.Rows[i]["ExaPrac_Resultado"];
                    }
                }
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                //Llena la información  de información laboral
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT [InfOcu_Numero]           " +
                                                ",[InfOcu_Entrada_Numero]       " +
                                                ",[InfOcu_FechaIngreso]         " +
                                                ",[InfOcu_FechaCargoActual]     " +
                                                ",[InfOcu_Jornada]              " +
                                                ",[InfOcu_CodOcupacion]            " +
                                                ",[InfOcu_Area]                 " +
                                                ",[InfOcu_DescripcionFunciones] " +
                                                ",[InfOcu_Maquinaria]           " +
                                                ",[InfOcu_Herramienta]          " +
                                                ",[InfOcu_MateriaPrima]         " +
                                                ",InfOcu_ElementoProte          " +
                                                ",InfOcu_CodEmpresa          " +
                                                " FROM [dbo].[InformacionOcupacional] WHERE InfOcu_Entrada_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    CboEmpresa.SelectedValue = Datos.Rows[0]["InfOcu_CodEmpresa"];
                    DtFechaIngreso.Value = Convert.ToDateTime(Datos.Rows[0]["InfOcu_FechaIngreso"].ToString());
                    TxtJornada.Text = Datos.Rows[0]["InfOcu_Jornada"].ToString();
                    CboCargoOcupacion.SelectedValue = Datos.Rows[0]["InfOcu_CodOcupacion"];
                    TxtSesion.Text = Datos.Rows[0]["InfOcu_Area"].ToString();
                    TxtDescripcionFunciones.Text = Datos.Rows[0]["InfOcu_DescripcionFunciones"].ToString();
                    TxtEquipo.Text = Datos.Rows[0]["InfOcu_Maquinaria"].ToString();
                    TxtHerramienta.Text = Datos.Rows[0]["InfOcu_Herramienta"].ToString();
                    TxtMateriaPrima.Text = Datos.Rows[0]["InfOcu_MateriaPrima"].ToString();
                    string[] ElementosPro = Datos.Rows[0]["InfOcu_ElementoProte"].ToString().Split('-');
                    for (int i = 0; i < ElementosPro.Length; i++)
                    {
                        switch (ElementosPro[i])
                        {
                            case "Gafas":
                                ChkGafas.Checked = true;
                                break;
                            case "Casco":
                                ChkCasco.Checked = true;
                                break;
                            case "Mascarilla":
                                ChkMascarilla.Checked = true;
                                break;
                            case "Overol":
                                ChkOverol.Checked = true;
                                break;
                            case "Botas":
                                ChkBotas.Checked = true;
                                break;
                            case "Protector":
                                ChkProtector.Checked = true;
                                break;
                            case "Respirador":
                                ChkRespirador.Checked = true;
                                break;
                            default:
                                ChkGuantes.Checked = true;
                                break;
                        }
                    }
                    TxtElementosProte.Clear();
                    TxtElementosProte.Text = Datos.Rows[0]["InfOcu_ElementoProte"].ToString();
                }
                else
                {
                    await CARGAR_INFORMACION_OCUPACIONAL();
                }
                //<<<<<<<<<<<<<<<<<<

                //Ciclo mentrual
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT [CicMens_Entrada_Numero] " +
                                                ",[CicMens_FechaUltimaRegla]   " +
                                                ",[CicMens_HijosSanos]         " +
                                                ",[CicMens_Gestaciones]        " +
                                                ",[CicMens_Partos]             " +
                                                ",[CicMens_Abortos]            " +
                                                ",[CicMens_Hijos]              " +
                                                ",[CicMens_ResultadoCitologia] " +
                                                ",[CicMens_Planificacion],[CicMens_Edadmenopausia],[CicMens_Edadmenarca],[CicMens_metodo]     " +
                                                "  FROM [dbo].[CicloMenstrual] WHERE CicMens_Entrada_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    TxtHijos.Text = Datos.Rows[0]["CicMens_Hijos"].ToString();
                    TxtHijosSanos.Text = Datos.Rows[0]["CicMens_HijosSanos"].ToString();
                    TxtGestaciones.Text = Datos.Rows[0]["CicMens_Gestaciones"].ToString();
                    TxtPartos.Text = Datos.Rows[0]["CicMens_Partos"].ToString();
                    TxtAbostos.Text = Datos.Rows[0]["CicMens_Abortos"].ToString();
                    TxtresultadoCitologia.Text = Datos.Rows[0]["CicMens_ResultadoCitologia"].ToString();
                    txtedadmenarca.Text = Datos.Rows[0]["CicMens_Edadmenarca"].ToString();
                    txtedadmenopausia.Text = Datos.Rows[0]["CicMens_Edadmenopausia"].ToString();
                    Txtmetodo.Text = Datos.Rows[0]["CicMens_metodo"].ToString();
                    if (Datos.Rows[0]["CicMens_Planificacion"].ToString() == "False")
                        RdbCicloNo.Checked = true;
                    else
                        RdbCicloSi.Checked = true;
                }

                //Examen Fisico
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT [ExaFisi_Entrada_Numero] " +
                                                ",[ExaFisi_PresionArterial]    " +
                                                ",[ExaFisi_FrecuenciaCardiaca] " +
                                                ",[ExaFisi_Lateracidad]        " +
                                                ",[ExaFisi_Peso]               " +
                                                ",[ExaFisi_Talla]              " +
                                                ",[ExaFisi_PerimetroCintura]   " +
                                                ",[ExaFisi_IMC]                " +
                                                ",[ExaFisi_Interpretacion]     " +
                                                "  FROM [dbo].[ExamenFisico] WHERE ExaFisi_Entrada_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    TxtPresionArterial.Text = Datos.Rows[0]["ExaFisi_PresionArterial"].ToString();
                    TxtFrecuenciaCardiaca.Text = Datos.Rows[0]["ExaFisi_FrecuenciaCardiaca"].ToString();
                    TxtLateracidad.Text = Datos.Rows[0]["ExaFisi_Lateracidad"].ToString();
                    TxtPeso.Text = Datos.Rows[0]["ExaFisi_Peso"].ToString();
                    TxtTalla.Text = Datos.Rows[0]["ExaFisi_Talla"].ToString();
                    TxtPerimetroCintura.Text = Datos.Rows[0]["ExaFisi_PerimetroCintura"].ToString();
                    TxtIMC.Text = Datos.Rows[0]["ExaFisi_IMC"].ToString();
                    TxtInterpretacion.Text = Datos.Rows[0]["ExaFisi_Interpretacion"].ToString();

                }
                else
                {
                }

                //CARGAR DATOS DE ENTRADAHISTORIA
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT [Entr_Numero] " +
                                              ",[Entr_IdPaciente]  " +
                                              ",[Entr_FechaEntrada]    " +
                                              ",[Entr_Diagnostico]     " +
                                              ",[Entr_Concepto_Codigo] " +
                                              ",[Entr_Recomendacion]   " +
                                              ",[Entr_Reubicacion]     " +
                                              ",Entr_TipoExamenCodigo  " +
                                              ",Ent_Enfasis,Ent_conceptoAptitud,Ent_CodEPS,Ent_CodARL,Ent_Medico,Ent_LugarExamen " +
                                              " FROM [dbo].[EntradaHistoria] WHERE Entr_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    TxtRecomendacion.Text = Datos.Rows[0]["Entr_Recomendacion"].ToString();
                    CboConcepto.SelectedValue = Datos.Rows[0]["Entr_Concepto_Codigo"];
                    //DtFechaEntrada.Value = Convert.ToDateTime(Datos.Rows[0]["Entr_FechaEntrada"]);
                    CboDiagnostico.SelectedValue = Datos.Rows[0]["Entr_Diagnostico"];
                    CboTipoExamen.SelectedValue = Datos.Rows[0]["Entr_TipoExamenCodigo"];
                    CboEnfasis.SelectedValue = Datos.Rows[0]["Ent_Enfasis"];

                    CboEPS.SelectedValue = Datos.Rows[0]["Ent_CodEPS"];
                    CboARL.SelectedValue = Datos.Rows[0]["Ent_CodARL"];

                    CboProfecional.SelectedValue = Datos.Rows[0]["Ent_Medico"];
                    CboLugarExamen.SelectedValue = Datos.Rows[0]["Ent_LugarExamen"];  

                    TxtCoceptoTarea.Text = Datos.Rows[0]["Ent_conceptoAptitud"].ToString();
                    if (Datos.Rows[0]["Entr_Reubicacion"].ToString() == "True")
                        RdbReuSi.Checked = true;
                    else
                        RdbReuNo.Checked = true;
                }

                else
                {
                }
                tabla.Dispose();
                Datos.Dispose();
                TablaPaciente.Dispose();
                Tablasistema.Dispose();
            }
        }

        public void ActualizarDatos(int NumeroEntrada, int estado)
        {
            if (TxtAbostos.Text != "" ||
                TxtCargo.Text != "" ||
                TxtDescripcionFunciones.Text != "" ||
                TxtDocumento.Text != "" ||
                TxtEquipo.Text != "" ||
                TxtFrecuenciaCardiaca.Text != "" ||
                TxtGestaciones.Text != "" ||
                TxtHerramienta.Text != "" ||
                TxtHijos.Text != "" ||
                TxtHijosSanos.Text != "" ||
                TxtIMC.Text != "" ||
                TxtInterpretacion.Text != "" ||
                TxtJornada.Text != "" ||
                TxtLateracidad.Text != "" ||
                TxtMateriaPrima.Text != "" ||
                TxtNombre.Text != "" ||
                TxtPartos.Text != "" ||
                TxtPerimetroCintura.Text != "" ||
                TxtPeso.Text != "" ||
                TxtPresionArterial.Text != "" ||
                TxtRecomendacion.Text != "" ||
                TxtresultadoCitologia.Text != "" ||
                TxtSesion.Text != "" ||
                TxtTalla.Text != "")
            {

                if (MessageBox.Show("¿Esta seguro de guardar la Informacón?  ", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {

                    string Query = "";

                    ////OBTENEMOS EL NUMERO DE LA HISTORIA INSERTADA
                    ObjServer.CadenaCnn = CadenaConexion.cadena();
                    ObjServer.Conectar();

                    //:::::::::Comienzo de la transacción:::::::::::::
                    //Establecemos el Objeto que nos va a permitir conectarnos a la base de Datos()
                    SqlConnection cnn = new SqlConnection(CadenaConexion.cadena());
                    //Abrimos la conexión()
                    cnn.Open();
                    //Comenzamos la transacción ()
                    SqlTransaction SQLtrans = cnn.BeginTransaction();
                    try
                    {
                        SqlCommand comman = cnn.CreateCommand();
                        comman.Transaction = SQLtrans;
                        //PRIME SE INSERTA LA ENTRADA - INSERTAR EN ENTRADA - HISTORIA

                        string QueryI = "DELETE FROM [dbo].[DiagnosticoPaciente]  WHERE DiagPaci_NumeroHistoria=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[AccidenteLaboral]  WHERE AccLab_Entrada_Numero=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[ExamenLaboratorio] WHERE ExaLabo_Entrada_Numero=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[AntecedentePersonal] WHERE AntPer_Entrada_Numero=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[AntecednteFamiliar] WHERE AntFam_Entrada_Numero =" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[CicloMenstrual] WHERE CicMens_Entrada_Numero=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[EnfermedadProfesional] WHERE EnfPro_Entrada_Numero=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[ExamenFisico] WHERE ExaFisi_Entrada_Numero=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[ExamenPracticado] WHERE  ExaPrac_Examen_Codigo=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[HabitoPaciente] WHERE HabPac_Entrada_Numero=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[InformacionOcupacional] WHERE InfOcu_Entrada_Numero=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[Inmunizar] WHERE Inmu_Entrada_Numero=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[ProbabilidadRiego] WHERE ProbRiesg_Entrada_Numero=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[RevisionSistema] WHERE RevSist_Entrada_Numero=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[RiesgoOcupacional] WHERE RiegOcu_Entrada_Numero=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[RecomendacionPaciente] WHERE RecoPac_Entrada_Numero=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[EquilibroPaciente] WHERE EqiPa_HistoriaNumero=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[ExamenPracticado] WHERE ExaPrac_Entrada_Numero=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        QueryI = "DELETE FROM [dbo].[Examen_Paciente] WHERE ExamPaci_Numero_Entrada=" + NumeroEntrada;
                        comman.CommandText = QueryI;
                        comman.ExecuteNonQuery();

                        int reubicacion = 0;
                        if (RdbReuSi.Checked)
                            reubicacion = 1;

                        var ARL = CboARL.SelectedValue;
                        var EPS = CboEPS.SelectedValue;

                        if (CboARL.SelectedValue == null)
                            ARL = 0;

                        if (CboEPS.SelectedValue == null)
                            EPS = 0;

                        //string diagnostico = "";
                        Query = "UPDATE [dbo].[EntradaHistoria]  SET [Entr_Diagnostico] = '" + CboDiagnostico.SelectedValue + "', [Entr_Concepto_Codigo] =" + CboConcepto.SelectedValue + ", [Entr_Recomendacion] = '" + TxtRecomendacion.Text + "', [Entr_Reubicacion] = " + reubicacion + " , [Entr_TipoExamenCodigo] = " + CboTipoExamen.SelectedValue + " , [Ent_Estado] = " + estado + ",Ent_Enfasis = " + CboEnfasis.SelectedValue + ", Ent_conceptoAptitud='" + TxtCoceptoTarea.Text + "',Ent_CodEPS=" + EPS + ", Ent_CodARL=" + ARL + ",Ent_Medico='"+CboProfecional.SelectedValue.ToString() +"',Ent_LugarExamen="+CboLugarExamen.SelectedValue +" WHERE Entr_Numero = " + NumeroEntrada ;
                        //MessageBox.Show(Query);

                        comman.CommandText = Query;
                        comman.ExecuteNonQuery();

                        #region //INSERTAR EN DIAGNOSTICOS DEL PACIENTE

                        for (int i = 0; i < DgvDiagnostico.Rows.Count - 1; i++)
                        {
                            Query = "INSERT INTO [dbo].[DiagnosticoPaciente]  ([DiagPaci_NumeroHistoria],[DiagPaci_CodDiagnostico]) VALUES (" + NumeroEntrada + ",'" + DgvDiagnostico.Rows[i].Cells["DgvDiagnosticoColDiagnostico"].Value.ToString() + "')";
                            comman.CommandText = Query;
                            comman.ExecuteNonQuery();
                        }

                        #endregion

                        #region //INSERTAR EN EXAMENES DE LABORATORIOS
                        for (int i = 0; i < DgvExamenLaboratorio.Rows.Count - 1; i++)
                        {
                            if (DgvExamenLaboratorio.Rows[i].Cells["DgvExamenLaboratorioColExamenLab"].Value != null)
                            {
                                Query = "INSERT INTO [dbo].[ExamenLaboratorio] ([ExaLabo_Entrada_Numero] ,[ExaLabo_ExamenCodigo]) VALUES (" + NumeroEntrada + "," + DgvExamenLaboratorio.Rows[i].Cells["DgvExamenLaboratorioColExamenLab"].Value + ")";
                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                            }
                        }
                        #endregion

                        #region //INSERTAR EN ACCIDENTE LABORAL PACIENTE
                        //Insertar en AccidenteLaboral
                        if (DgvAccidenteLaboral.Rows.Count >= 1)
                        {
                            for (int i = 0; i < DgvAccidenteLaboral.Rows.Count - 1; i++)
                            {
                                //Para saber si hay datos para agregar
                                string secuela = "";
                                string fecha = "";
                                string empresa = "";
                                string parteafectada = "";
                                string naturaleza = "";
                                string dias = "";

                                if (DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColFecha"].Value != null)
                                    fecha = DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColFecha"].Value.ToString() + "";

                                if (DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColEmpresa"].Value != null)
                                    empresa = DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColEmpresa"].Value.ToString() + "";

                                if (DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColParteAfectada"].Value != null)
                                    parteafectada = DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColParteAfectada"].Value.ToString() + "";

                                if (DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralNaturaleza"].Value != null)
                                    naturaleza = DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralNaturaleza"].Value.ToString() + "";

                                if (DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColDias"].Value != null)
                                    dias = DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColDias"].Value.ToString() + "";

                                if (DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColSecuela"].Value != null)
                                    secuela = DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColSecuela"].Value.ToString() + "";

                                if (empresa != "")
                                {
                                    //Posición del vector DatosAgregar 0 Empresa; 4 Fecha; 1 Naturaleza; 2 ParteAfectada; 3 DiasIncapacidad;  
                                    Query = "INSERT INTO [dbo].[AccidenteLaboral] " +
                                           "([AccLab_Fecha]                 " +
                                           ",[AccLab_Entrada_Numero]        " +
                                           ",[AccLab_Empresa]               " +
                                           ",[AccLab_Naturaleza]            " +
                                           ",[AccLab_ParteAfectada_Codigo]  " +
                                           ",[AccLab_DiasIncapacidad],AccLab_Secuela)      " +
                                           "VALUES (" + "'" + fecha + "'," + NumeroEntrada + ",'" + empresa + "','" + naturaleza + "'," + parteafectada + ",'" + dias + "','" + secuela + "')";

                                    comman.CommandText = Query;
                                    comman.ExecuteNonQuery();
                                }
                            }
                        }
                        #endregion

                        //Insertar DgvProbabilidad
                        for (int i = 0; i < DgvProbabilidadRiesgo.Rows.Count - 1; i++)
                        {
                            int TipoRiesgo = 0;
                            string Riesgo = "";
                            string probabilidad = "";
                            string estimacion = "";
                            //DgvProbabilidadRiesgoColProbabilidad;
                            if (DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColTipoRiesgo"].Value != null)
                            {
                                if (DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColRiesgo"].Value != null)
                                    Riesgo = DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColRiesgo"].Value.ToString();

                                if (DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColEstimacion"].Value != null)
                                    estimacion = DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColEstimacion"].Value.ToString();

                                if (DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColTipoRiesgo"].Value != null)
                                    TipoRiesgo = Convert.ToInt32(DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColTipoRiesgo"].Value);

                                if (DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColProbabilidad"].Value != null)
                                    probabilidad = DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColProbabilidad"].Value.ToString();

                                //Insertar en Probabilidad de Riesgo
                                Query = "INSERT INTO [dbo].[ProbabilidadRiego] " +
                                      "([ProbRiesg_Riesgo_Codigo]       " +
                                      ",[ProbRiesg_Entrada_Numero]      " +
                                      ",[ProbRiesg_TipoRiesgo_Codigo]   " +
                                      ",[ProbRiesg_Probabilidad_Codigo] " +
                                      ",[ProbRiesg_Estimacion])         " +
                                          "VALUES (" + "'" + Riesgo + "'," + NumeroEntrada + ",'" + TipoRiesgo + "'," + probabilidad + ",'" + estimacion + "')";
                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                            }
                        } //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<



                        #region //INSERTAR EN RIESGO OCUPACIONAL DEL PACIENTE
                        //      //Insertar en DgvRiesgoOcupacional
                        for (int i = 0; i < DgvRiesgoOcupacional.Rows.Count - 1; i++)
                        {
                            //Para saber si hay datos para agregar
                            int riesgo = 0;
                            string empresa = "";
                            string cargo = "";
                            string meses = "";
                            string riesgoespecifico = "";

                            if (DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiesgo"].Value != null)
                            {
                                if (DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColCargo"].Value != null)
                                    cargo = DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColCargo"].Value.ToString();
                                else
                                    cargo = "";
                                if (DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColEmpresa"].Value != null)
                                    empresa = DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColEmpresa"].Value.ToString() + "";
                                else
                                    empresa = "";

                                //if (DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiesgo"].Value!=null)                                    
                                riesgo = Convert.ToInt32(DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiesgo"].Value);

                                if (DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColMeses"].Value != null)
                                    meses = DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColMeses"].Value.ToString() + "";
                                else
                                    meses = "";
                                if (DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiengoEspecifico"].Value != null)
                                    riesgoespecifico = DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiengoEspecifico"].Value.ToString() + "";
                                else
                                    riesgoespecifico = "";

                                //if (empresa != "" || riesgoespecifico != "" || cargo != "" || meses != "")
                                //{
                                Query = "INSERT INTO [dbo].[RiesgoOcupacional] " +
                                       "([RiegOcu_Riesgo_Codigo]    " +
                                       ",[RiegOcu_Entrada_Numero]   " +
                                       ",[RiegOcu_Empresa]          " +
                                       ",[RiegOcu_Cargo]     " +
                                       ",[RiegOcu_Meses],RiegOcu_RiesgoEspecifico)           " +
                                       "VALUES (" + "" + riesgo + "," + NumeroEntrada + ",'" + empresa + "','" + cargo + "','" + meses + "','" + riesgoespecifico + "')";

                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                                //}
                            }
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR EN ENFERMEDADES PROFESIONALES DEL PACIENTE
                        //Insertar DgvEnfermedadProfesional
                        for (int i = 0; i < DgvEnfermedadProfesional.Rows.Count - 1; i++)
                        {
                            //Para saber si hay datos para agregar
                            Boolean confirmar = false;
                            int enfermedad = 0;
                            string empresa = "";
                            string Fecha = "";
                            //string Diagnostico = "";

                            if (DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColEmpresa"].Value != null || DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColFecha"].Value != null)
                            {
                                if (DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value != null)
                                    Fecha = DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value.ToString();
                                else
                                    Fecha = DtFecha.Text;

                                if (DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColEmpresa"].Value != null)
                                    empresa = DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColEmpresa"].Value.ToString() + "";
                                else
                                    empresa = "";

                                enfermedad = Convert.ToInt32(DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColEnfermedad"].Value);

                                Query = "INSERT INTO [dbo].[EnfermedadProfesional] " +
                                            "([EnfPro_Enfermedad_Codigo]               " +
                                            ",[EnfPro_Entrada_Numero]                  " +
                                            ",[EnfPro_Empresa]                         " +
                                            ",[EnfPro_FechaDiagnostico])                " +
                                               "VALUES (" + "" + enfermedad + "," + NumeroEntrada + ",'" + empresa + "','" + Fecha + "')";

                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                            }
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR EN INFORMACIÓN OCUPACIONAL DEL PACIENTE
                        //insertar en información ocupacional
                        Query = "INSERT INTO [dbo].[InformacionOcupacional] (" +
                            "[InfOcu_Entrada_Numero]        " +
                            ",[InfOcu_FechaIngreso]         " +
                            ",[InfOcu_FechaCargoActual]     " +
                            ",[InfOcu_Jornada]              " +
                            ",[InfOcu_CodOcupacion]            " +
                            ",[InfOcu_Area]                 " +
                            ",[InfOcu_DescripcionFunciones] " +
                            ",[InfOcu_Maquinaria]           " +
                            ",[InfOcu_Herramienta]          " +
                            ",[InfOcu_MateriaPrima]         " +
                            ",InfOcu_ElementoProte          " +
                            ",InfOcu_CodEmpresa)               " +
                            " VALUES(" + NumeroEntrada + ",'" + DtFechaIngreso.Value.ToShortDateString() + "','" + DtFecha.Value.ToShortDateString() + "','" + TxtJornada.Text + "'," + CboCargoOcupacion.SelectedValue + ",'" + TxtSesion.Text + "','" + TxtDescripcionFunciones.Text + "','" + TxtEquipo.Text + "','" + TxtHerramienta.Text + "','" + TxtMateriaPrima.Text + "','" + TxtElementosProte.Text + "'," + CboEmpresa.SelectedValue + ")";

                        comman.CommandText = Query;
                        comman.ExecuteNonQuery();
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR EN ANTECEDENTE FAMILIAR DEL PACIENTE
                        //Insertar DgvAntecedenteFamiliar
                        for (int i = 0; i < DgvAntecedenteFamiliar.Rows.Count - 1; i++)
                        {
                            //Para saber si hay datos para agregar
                            Boolean confirmar = false;
                            string enfermedad;
                            string parentesco = "";
                            Boolean mortalidad;
                            int mortalidad2;


                            //DgvAntecedenteFamiliar;
                            //if (DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColParentesco"].Value != null)
                            //{
                            if (DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColEnfermedad"].Value != null)
                                enfermedad = DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColEnfermedad"].Value.ToString() + "";
                            else
                                enfermedad = "";

                            if (DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColParentesco"].Value != null)
                                parentesco = DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColParentesco"].Value.ToString() + "";
                            else
                                parentesco = "";

                            mortalidad2 = Convert.ToInt32(DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColMortalidad"].Value);
                            confirmar = true;
                            //}
                            //else
                            //{ confirmar = false; break; }

                            //if (confirmar == true)
                            //{
                            //    ////Insertar en DgvAntecedenteFamiliar

                            Query = "INSERT INTO [dbo].[AntecednteFamiliar] " +
                                       "([AntFam_Enfermedad_Codigo] " +
                                       ",[AntFam_Entrada_Numero]    " +
                                       ",[AntFam_Parentesco]        " +
                                       ",[AntFam_Mortalidad])       " +
                                       "VALUES (" + "'" + enfermedad + "'," + NumeroEntrada + ",'" + parentesco + "','" + mortalidad2 + "')";

                            comman.CommandText = Query;
                            comman.ExecuteNonQuery();
                        }
                        //}
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR EN ANTECEDENTES PERSONALES DEL PACIENTE
                        //Insertar DgvAntecedentePersonal
                        for (int i = 0; i < DgvAntecedentePersonal.Rows.Count; i++)
                        {
                            //Para saber si hay datos para agregar
                            Boolean confirmar = false;
                            string antecedente; ;
                            string diagnostico = "";
                            string observacion = "";
                            if (DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColDiagnostico"].Value != null || DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColDiagnostico"].Value == null)
                            {
                                if (DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColCodigo"].Value != null)
                                    antecedente = DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColCodigo"].Value.ToString() + "";
                                else
                                    antecedente = "";

                                if (DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColDiagnostico"].Value != null)
                                    diagnostico = DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColDiagnostico"].Value.ToString() + "";
                                else
                                    diagnostico = "";

                                if (DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColObservacion"].Value != null)
                                    observacion = DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColObservacion"].Value.ToString() + "";
                                else
                                    observacion = "";

                                confirmar = true;
                                if (observacion != "")
                                {
                                    ////Insertar en DgvAntecedentePersonal
                                    Query = "INSERT INTO [dbo].[AntecedentePersonal] " +
                                               "([AntPer_Antecedende_Codigo] " +
                                               ",[AntPer_Entrada_Numero]     " +
                                               ",[AntPer_Diagnostico]        " +
                                               ",[AntPer_Observacion])       " +
                                               "VALUES (" + "'" + antecedente + "'," + NumeroEntrada + ",'" + diagnostico + "','" + observacion + "')";
                                    comman.CommandText = Query;
                                    comman.ExecuteNonQuery();
                                }
                            }
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR EN EXAMENES PRCTICADOS DEL PACIENTE
                        //Insertar EXAMEN PRACTICADOS
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        for (int i = 0; i < DgvExamenPacticado.Rows.Count - 1; i++)
                        {
                            //if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value.ToString().Length > 7 )
                            //{
                            Boolean confirmar = false; //Para saber si hay datos para agregar
                            int CodExamen = 0;
                            string FechaExa = "";
                            int resultado = 0;
                            resultado = Convert.ToInt32(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value);

                            FechaExa = "01/01/1999";

                            if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value != null)
                            {
                                if (DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value.ToString() != "")
                                {
                                    FechaExa = DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value.ToString();
                                }
                            }
                            //MessageBox.Show(FechaExa);
                            CodExamen = Convert.ToInt32(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value);
                            confirmar = true;
                            Query = "INSERT INTO [dbo].[ExamenPracticado] VALUES (@examen" + i + ",@entrada" + i + ",@resultado" + i + ",@foto" + i + ",@fecha" + i + ")";

                            comman.CommandText = Query;
                            comman.Parameters.Add("@examen" + i, SqlDbType.Int);
                            comman.Parameters.Add("@resultado" + i, SqlDbType.Int);
                            comman.Parameters.Add("@foto" + i, SqlDbType.Image);
                            comman.Parameters.Add("@fecha" + i, SqlDbType.Date);
                            comman.Parameters.Add("@entrada" + i, SqlDbType.Int);

                            comman.Parameters["@entrada" + i].Value = NumeroEntrada;
                            comman.Parameters["@examen" + i].Value = CodExamen;
                            comman.Parameters["@resultado" + i].Value = resultado;
                            comman.Parameters["@fecha" + i].Value = FechaExa;
                            PctFoto.Image = null;
                            ms = new MemoryStream();
                            string Imagen = DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value.ToString();
                            if (Imagen == "System.Byte[]")
                            {
                                byte[] imageBuffer = (byte[])DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value;
                                // Se crea un MemoryStream a partir de ese buffer
                                System.IO.MemoryStream ms1 = new System.IO.MemoryStream(imageBuffer);
                                // Se utiliza el MemoryStream para extraer la imagen
                                this.PctFoto.Image = Image.FromStream(ms1);
                                //MessageBox.Show(DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value.ToString());
                                //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                                //MessageBox.Show("HOLA"+i);
                                PctFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                                comman.Parameters["@foto" + i].Value = ms.GetBuffer();

                            }
                            else
                            {
                                comman.Parameters["@foto" + i].Value = DBNull.Value;
                            }

                            comman.ExecuteNonQuery();
                        }
                        #endregion

                        #region //INSERTAR EN CICLO MENSTRUAL DEL PACIENTE
                        //insertar en Ciclo Menstrual
                        if (ChkCicloMenstrual.Checked == true)
                        {
                            int planificacion = 0;
                            if (RdbCicloSi.Checked)
                                planificacion = 1;
                            Query = "INSERT INTO [dbo].[CicloMenstrual] " +
                                       "([CicMens_Entrada_Numero]      " +
                                       ",[CicMens_FechaUltimaRegla]    " +
                                       ",[CicMens_HijosSanos]          " +
                                       ",[CicMens_Gestaciones]         " +
                                       ",[CicMens_Partos]              " +
                                       ",[CicMens_Abortos]             " +
                                       ",[CicMens_Hijos]               " +
                                       ",[CicMens_ResultadoCitologia]  " +
                                       ",[CicMens_Planificacion] ,[CicMens_Edadmenopausia],[CicMens_Edadmenarca],[CicMens_metodo]) " +
                                         " VALUES(" + NumeroEntrada + ",'" + DtFechaCiclo.Text + "','" +
                                                TxtHijosSanos.Text + "','" + TxtGestaciones.Text + "','" +
                                                TxtPartos.Text + "','" + TxtAbostos.Text + "','" + TxtHijos.Text + "','" +
                                                TxtresultadoCitologia.Text + "','" + planificacion + "','" + txtedadmenopausia.Text + "','" + txtedadmenarca.Text + "','" + Txtmetodo.Text + "')";

                            comman.CommandText = Query;
                            comman.ExecuteNonQuery();
                            //<<<<<<<<<<<<<<<<<<<<<<<<<<<< 
                        }
                        #endregion

                        #region //INSERTAR EN HABITOS DEL PACIENTE
                        //Insertar DgvHabitos
                        for (int i = 0; i < DgvHabitos.Rows.Count; i++)
                        {
                            if (DgvHabitos.Rows[i].Cells["DgvHabitosColSiNo"].Value.ToString() == "SI" || DgvHabitos.Rows[i].Cells["DgvHabitosColSiNo"].Value.ToString() == "Si")
                            {
                                //Para saber si hay datos para agregar
                                Boolean confirmar = false;
                                int habito = 0;
                                string caracteristica = "";
                                string frecuencia = "";
                                string tiempo = "";
                                string observacion = "";
                                if (DgvHabitos.Rows[i].Cells["DgvHabitosColTiempoConsumo"].Value != null)
                                    tiempo = DgvHabitos.Rows[i].Cells["DgvHabitosColTiempoConsumo"].Value.ToString();
                                else
                                    tiempo = "";

                                if (DgvHabitos.Rows[i].Cells["DgvHabitosColCaracteristica"].Value != null)
                                    caracteristica = DgvHabitos.Rows[i].Cells["DgvHabitosColCaracteristica"].Value.ToString();
                                else
                                    caracteristica = "";

                                if (DgvHabitos.Rows[i].Cells["DgvHabitosColObservacion"].Value != null)
                                    observacion = DgvHabitos.Rows[i].Cells["DgvHabitosColObservacion"].Value.ToString();
                                else
                                    observacion = "";

                                if (DgvHabitos.Rows[i].Cells["DgvHabitosColFrecuencia"].Value != null)
                                    frecuencia = DgvHabitos.Rows[i].Cells["DgvHabitosColFrecuencia"].Value.ToString() ?? " ";
                                else
                                    frecuencia = "";
                                habito = Convert.ToInt32(DgvHabitos.Rows[i].Cells["DgvHabitosColCodigo"].Value);

                                //if (caracteristica != "" || frecuencia != "" || tiempo != "" || observacion != "")
                                //{
                                Query = "INSERT INTO [dbo].[HabitoPaciente] " +
                                      "([HabPac_Habito_Codigo]  " +
                                      ",[HabPac_Entrada_Numero] " +
                                      ",[HabPac_Caracteristica] " +
                                      ",[HabPac_Frecuencia]     " +
                                      ",[HabPac_Tiempo]         " +
                                      ",[HabPac_Observacion])   " +
                                        "VALUES (" + "" + habito + "," + NumeroEntrada + ",'" + caracteristica + "','" + frecuencia + "','" + tiempo + "','" + observacion + "')";
                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                                //}  
                            }

                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR EN INMUNIZACIÓN DEL PACIENTE
                        //Insertar DgvInmunizacion
                        for (int i = 0; i < DgvInmunizacion.Rows.Count - 1; i++)
                        {
                            //Para saber si hay datos para agregar
                            Boolean confirmar = false;
                            int TipoInmu = 0;
                            string Dosis = "";
                            string fecha = "";

                            if (DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColFecha"].Value != null || DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColDosis"].Value != null)
                            {
                                TipoInmu = Convert.ToInt32(DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColTipoImn"].Value);
                                if (DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColDosis"].Value != null)
                                    Dosis = DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColDosis"].Value.ToString() + "";
                                else
                                    Dosis = "";

                                if (DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColFecha"].Value != null)
                                    fecha = DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColFecha"].Value.ToString() + "";
                                else
                                    fecha = DtFecha.Text;

                                confirmar = true;
                            }
                            else
                            { confirmar = false; break; }

                            if (confirmar == true)
                            {
                                ////Insertar en DgvInmunizacion
                                Query = "INSERT INTO [dbo].[Inmunizar]      " +
                                           "([Inmu_Entrada_Numero]          " +
                                           ",[Inmu_TipoInmunizacion_Codigo] " +
                                           ",[Inmu_Fecha]                   " +
                                           ",[Inmu_Dosis])                  " +
                                            "VALUES (" + "" + NumeroEntrada + "," + TipoInmu + ",'" + fecha + "','" + Dosis + "')";
                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                            }
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR LAS RECOMENDACIONES DEL PACIENTE
                        //Insertar en RECOMENDACIONES (DGV):
                        if (ListRecomendaciones.Count > 0)
                        {
                            for (int i = 0; i < ListRecomendaciones.Count; i++)
                            {
                                int CODIGO = Convert.ToInt32(ListRecomendaciones[i]);
                                ////Insertar en Dgvrevision
                                Query = "INSERT INTO [dbo].[RecomendacionPaciente] " +
                                                           "([RecoPac_Entrada_Numero] " +
                                                           ",[RecoPac_Recomendacion_Codigo]) " +
                                                               "VALUES (" + "" + NumeroEntrada + "," + CODIGO + ")";
                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                            }
                        }
                        //for (int i = 0; i < DgvRecomendaciones.Columns.Count; i++)
                        //{
                        //    for (int x = 0; x < Recomendaciones.Rows.Count; x++)
                        //    {
                        //        string Col = Recomendaciones.Rows[x]["reco_Descripcion"].ToString();
                        //        string Col2 = Recomendaciones.Rows[x]["reco_Descripcion"].ToString() + x.ToString();

                        //        if (DgvRecomendaciones.Columns[i].Name == Col2)
                        //        {
                        //            for (int z = 0; z < DgvRecomendaciones.Rows.Count; z++)
                        //            {
                        //                if (DgvRecomendaciones.Rows[z].Cells[Col2].Value != null)
                        //                {
                        //                    if (Convert.ToBoolean(DgvRecomendaciones.Rows[z].Cells["chk" + x.ToString()].Value) == true)
                        //                    {
                        //                        int CODIGO = Convert.ToInt32(DgvRecomendaciones.Rows[z].Cells[Col2].Value);
                        //                        ////Insertar en Dgvrevision
                        //                        Query = "INSERT INTO [dbo].[RecomendacionPaciente] " +
                        //                                   "([RecoPac_Entrada_Numero] " +
                        //                                   ",[RecoPac_Recomendacion_Codigo]) " +
                        //                                       "VALUES (" + "" + NumeroEntrada + "," + CODIGO + ")";
                        //                        comman.CommandText = Query;
                        //                        comman.ExecuteNonQuery();
                        //                        DgvRecomendaciones.Rows[z].Cells["chk" + x.ToString()].Value = false;
                        //                        //Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Style.BackColor = Color.Red;
                        //                    }
                        //                }
                        //                else
                        //                    break;
                        //            }
                        //        }
                        //    }

                        //}
                        //<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR LAS REVICIONES DEL PACIENTE
                        if (RevisionSistema.Count > 0)
                        {
                            for (int i = 0; i < RevisionSistema.Count; i++)
                            {
                                int CODIGO = Convert.ToInt32(RevisionSistema[i]);
                                ////Insertar en Dgvrevision
                                Query = "INSERT INTO [dbo].[RevisionSistema] " +
                                           "([RevSist_Entrada_Numero]   " +
                                           ",[RevSist_Revision_Codigo]) " +
                                               "VALUES (" + "" + NumeroEntrada + "," + CODIGO + ")";


                                comman.CommandText = Query;
                                comman.ExecuteNonQuery();
                                //Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Value = false;
                                //Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Style.BackColor = Color.Red;
                            }
                            //RevisionSistema.Clear();
                        }

                        //Insertar en Dgvrevision
                        //for (int i = 0; i < Dgvrevision.Columns.Count; i++)
                        //{
                        //    for (int x = 0; x < Tablasistema.Rows.Count; x++)
                        //    {
                        //        string Col = Tablasistema.Rows[x]["Sist_Descripcion"].ToString();
                        //        string Col2 = Tablasistema.Rows[x]["Sist_Descripcion"].ToString() + x.ToString();

                        //        if (Dgvrevision.Columns[i].Name == Col2)
                        //        {
                        //            //MessageBox.Show(Col2);
                        //            for (int z = 0; z < Dgvrevision.Rows.Count; z++)
                        //            {
                        //                if (Dgvrevision.Rows[z].Cells[Col2].Value != null)
                        //                {
                        //                    if (Convert.ToBoolean(Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Value) == true)
                        //                    {
                        //                        int CODIGO = Convert.ToInt32(Dgvrevision.Rows[z].Cells[Col2].Value);
                        //                        ////Insertar en Dgvrevision
                        //                        Query = "INSERT INTO [dbo].[RevisionSistema] " +
                        //                                   "([RevSist_Entrada_Numero]   " +
                        //                                   ",[RevSist_Revision_Codigo]) " +
                        //                                       "VALUES (" + "" + NumeroEntrada + "," + CODIGO + ")";


                        //                        comman.CommandText = Query;
                        //                        comman.ExecuteNonQuery();
                        //                        Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Value = false;
                        //                        //Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Style.BackColor = Color.Red;
                        //                    }
                        //                }
                        //                else
                        //                    break;
                        //            }
                        //        }
                        //    }

                        //}
                        //<<<<<<<<<<<<<<<<<<
                        #endregion

                        #region //INSERTAR EN EXAMENES FISICOS DEL PACIENTE
                        //INSERTAR EN EXAMENES FISICO
                        for (int i = 0; i < DgvExamenFisifoH.Rows.Count; i++)
                        {
                            string Codigo = DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHCodigo"].Value.ToString();
                            string observacion;

                            if (DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHObservación"].Value != null)
                                observacion = DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHObservación"].Value.ToString() + "";
                            else
                                observacion = "";

                            string examen = DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHCodigo"].Value.ToString();
                            string ESTA = DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoH___"].Value.ToString();

                            string Quiery = "INSERT INTO [dbo].[Examen_Paciente] " +
                                          "([ExamPaci_Examen_Codigo]           " +
                                          ",[ExamPaci_Numero_Entrada]          " +
                                          ",[ExamPaci_Observacion],ExamPaci_Estado)            " +
                                          "  VALUES                            " +
                                          "(" + examen + "," + NumeroEntrada + ",'" + observacion + "'," + ESTA + ")";
                            comman.CommandText = Quiery;
                            comman.ExecuteNonQuery();

                        }
                        ///////////////////////
                        #endregion

                        #region //INSERTAR EN PRUEBA DE EQUILIBRIO
                        int col = -1;
                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            if (column.Visible == false)
                            {
                                col++;
                                //MessageBox.Show(dataGridView1.Rows[1].Cells["combo0"].Value.ToString());
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    if (dataGridView1.Rows[i].Cells[column.Name].Value != null)
                                    {
                                        string valorEquilibro;
                                        string valorestado;

                                        if (dataGridView1.Rows[i].Cells[column.Name].Value != null)
                                            valorEquilibro = dataGridView1.Rows[i].Cells[column.Name].Value.ToString();
                                        else
                                            valorEquilibro = "";

                                        valorestado = dataGridView1.Rows[i].Cells["combo" + col.ToString()].Value.ToString();

                                        string query = "INSERT INTO [dbo].[EquilibroPaciente] " +
                                                       "([EqiPa_Equilibro]                   " +
                                                       ",[EqiPa_HistoriaNumero]              " +
                                                       ",[EqiPa_Estado], " +
                                                       "EqiPa_Reflejos, " +
                                                       "EqiPa_Marcha,EqiPa_Piel) " +
                                                       " VALUES                              " +
                                                       "(" + valorEquilibro + "," + NumeroEntrada + "," + valorestado + ",'" + TxtReflejos.Text + "','" + TxtMarcha.Text + "','" + TxtPiel.Text + "')";
                                        //ObjServer.CadnaSentencia = query;
                                        //ObjServer.Sentencia();
                                        comman.CommandText = query;
                                        comman.ExecuteNonQuery();
                                        //MessageBox.Show(valorCol + " " + valorcom);
                                    }
                                }
                            }
                        }
                        #endregion

                        #region //INSERTAR EN EXAMEN FISICO DEL PACIENTE
                        //insertar en Examenes Fisico
                        string Presion = TxtPresionArterial.Text;
                        Query = "INSERT INTO [dbo].[ExamenFisico] " +
                                   "([ExaFisi_Entrada_Numero]     " +
                                   ",[ExaFisi_PresionArterial]    " +
                                   ",[ExaFisi_FrecuenciaCardiaca] " +
                                   ",[ExaFisi_Lateracidad]        " +
                                   ",[ExaFisi_Peso]               " +
                                   ",[ExaFisi_Talla]              " +
                                   ",[ExaFisi_PerimetroCintura]   " +
                                   ",[ExaFisi_IMC]                " +
                                   ",[ExaFisi_Interpretacion])    " +
                                        " VALUES(" + NumeroEntrada + ",'" + TxtPresionArterial.Text.Trim() + "'," +
                                            TxtFrecuenciaCardiaca.Text + ",'" + TxtLateracidad.Text + "','" +
                                            TxtPeso.Text + "','" + TxtTalla.Text + "','" + TxtPerimetroCintura.Text + "','" +
                                            TxtIMC.Text + "','" + TxtInterpretacion.Text + "')";
                        #endregion

                        comman.CommandText = Query;
                        comman.ExecuteNonQuery();

                        SQLtrans.Commit();
                        TxtDocumento.BackColor = Color.SteelBlue;
                        if (MessageBox.Show("Número de la atención #" + LblEntrada.Text + " guardada correctamente \n ¿Desea ver el reporte?", "Finalizar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            CargarReporte();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("La operación no pudo completarse debido a: \n 1 - No dispone de una conexión  \n 2 - El registro esta duplicado  \n 3 - La información ingrasado no corresponde a la requerida   \n  - El documento no coresponde a ningún paciente \n  - Diagnostico no ingresado \n  - Diagnostico duplicado  \n Vuelva a intentarlo!!! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SQLtrans.Rollback();
                    }
                }
            }

        }

        private void TxtSesion_KeyPress(object sender, KeyPressEventArgs e)
        { ObjServer.Solo_Letras(e); }

        private void TxtMateriaPrima_KeyPress(object sender, KeyPressEventArgs e)
        { ObjServer.Solo_Letras(e); }

        private void TxtHijosSanos_KeyPress(object sender, KeyPressEventArgs e)
        { }

        private void TxtPresionArterial_KeyPress(object sender, KeyPressEventArgs e)
        { }

        private void DgvAccidenteLaboral_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (DgvAccidenteLaboral.CurrentCell.ColumnIndex == 1)
            {

                TextBox txt = e.Control as TextBox;

                if (txt != null)
                {
                    txt.KeyPress -= new KeyPressEventHandler(DgvAccidenteLaboral_KeyPress);
                    txt.KeyPress += new KeyPressEventHandler(DgvAccidenteLaboral_KeyPress);
                }

            }
        }

        private void DgvAccidenteLaboral_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ChkCicloMenstrual_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCicloMenstrual.Checked)
                GrpCicloMenstrual.Enabled = true;
            else
                GrpCicloMenstrual.Enabled = false;
        }

        public void ChkElementosProtect(CheckBox Chk, string Texto)
        {
            if (Chk.Checked)
            {
                if (TxtElementosProte.Text == "")
                    TxtElementosProte.Text = Texto;
                else
                    TxtElementosProte.Text = Texto + "-" + TxtElementosProte.Text;
            }
            else
            {
                string[] dt = TxtElementosProte.Text.Split('-');
                TxtElementosProte.Clear();
                for (int i = 0; i < dt.Length; i++)
                {
                    if (dt[i] != Texto)
                    {
                        if (i == 0 || TxtElementosProte.Text == "")
                            TxtElementosProte.Text = dt[i];
                        else
                            TxtElementosProte.Text = dt[i] + "-" + TxtElementosProte.Text;
                    }
                }
            }
        }

        string[] elementos = { };
        private void ChkGafas_CheckedChanged(object sender, EventArgs e)
        {
            ChkElementosProtect(ChkGafas, "Gafas");
        }

        private void ChkCasco_CheckedChanged(object sender, EventArgs e)
        {
            ChkElementosProtect(ChkCasco, "Casco");
        }

        private void ChkMascarilla_CheckedChanged(object sender, EventArgs e)
        {
            ChkElementosProtect(ChkMascarilla, "Mascarilla");
        }

        private void ChkOverol_CheckedChanged(object sender, EventArgs e)
        {
            ChkElementosProtect(ChkOverol, "Overol");
        }

        private void ChkBotas_CheckedChanged(object sender, EventArgs e)
        {
            ChkElementosProtect(ChkBotas, "Botas");
        }

        private void ChkProtector_CheckedChanged(object sender, EventArgs e)
        {
            ChkElementosProtect(ChkProtector, "Protector");
        }

        private void ChkRespirador_CheckedChanged(object sender, EventArgs e)
        {
            ChkElementosProtect(ChkRespirador, "Respirador");
        }

        private void ChkGuantes_CheckedChanged(object sender, EventArgs e)
        {
            ChkElementosProtect(ChkGuantes, "Guantes");
        }

        private void DgvEnfermedadProfesional_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DgvEnfermedadProfesional.Rows[DgvEnfermedadProfesional.Rows.Count - 1].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value = "01/01/1990";
        }

        private void DgvInmunizacion_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DgvInmunizacion.Rows[DgvInmunizacion.Rows.Count - 1].Cells["DgvInmunizacionColFecha"].Value = "01/01/1990";
        }

        private void DgvAccidenteLaboral_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DgvAccidenteLaboral.Rows[DgvAccidenteLaboral.Rows.Count - 1].Cells["DgvAccidenteLaboralColFecha"].Value = "01/01/1990";

        }

        public void addItems(AutoCompleteStringCollection col)
        {
            DataTable dt = new DataTable();
            string consulta = "SELECT [Diag_Codigo] as Codigo ,[Diag_Descripcion] as Descripcion FROM [dbo].[Diagnostico]"; //consulta a la tabla paises
            dt = ObjServer.LlenarTabla(consulta);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //if (confir==true)
                col.Add(dt.Rows[i][0].ToString() + "-" + dt.Rows[i][1].ToString());
                //else
                col.Add(dt.Rows[i][1].ToString() + "-" + dt.Rows[i][0].ToString());
            }
        }

        private void DgvExamenLaboratorio_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                int codigo = Convert.ToInt32(e.KeyCode);
                if (codigo == 8 || e.KeyCode == Keys.Delete)
                {
                    if (MessageBox.Show("Los datos serán eliminados de forma permanente", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        if (DgvExamenLaboratorio.Rows.Count > 1)
                        {
                            DgvExamenLaboratorio.Rows.RemoveAt(DgvExamenLaboratorio.CurrentRow.Index);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void DgvAntecedentePersonal_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox autoText = e.Control as TextBox;
            if (DgvAntecedentePersonal.CurrentCell.ColumnIndex == 3)
            {
                if (autoText != null)
                {
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                    addItems(DataCollection);
                    autoText.AutoCompleteCustomSource = DataCollection;
                }
            }
            else
            {
                autoText.AutoCompleteCustomSource = null;
            }
        }

        private void DgvAntecedentePersonal_KeyDown(object sender, KeyEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyCode);
            if (codigo == 8 || e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DgvAntecedentePersonal.Rows[DgvAntecedentePersonal.CurrentRow.Index].Cells["DgvAntecedentePersonalColDiagnostico"].Value = "";
                    DgvAntecedentePersonal.Rows[DgvAntecedentePersonal.CurrentRow.Index].Cells["DgvAntecedentePersonalColObservacion"].Value = "";
                }
            }
        }

        private void DgvAccidenteLaboral_KeyDown(object sender, KeyEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyCode);
            if (codigo == 8 || e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        if (DgvAccidenteLaboral.Rows.Count > 1)
                        {
                            DgvAccidenteLaboral.Rows.RemoveAt(DgvAccidenteLaboral.CurrentRow.Index);
                        }
                        else
                        {
                            DgvAccidenteLaboral.Rows[DgvAccidenteLaboral.CurrentRow.Index].Cells["DgvAccidenteLaboralColDias"].Value = "";
                            DgvAccidenteLaboral.Rows[DgvAccidenteLaboral.CurrentRow.Index].Cells["DgvAccidenteLaboralColEmpresa"].Value = "";
                            DgvAccidenteLaboral.Rows[DgvAccidenteLaboral.CurrentRow.Index].Cells["DgvAccidenteLaboralNaturaleza"].Value = "";
                            DgvAccidenteLaboral.Rows[DgvAccidenteLaboral.CurrentRow.Index].Cells["DgvAccidenteLaboralColSecuela"].Value = "";

                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void DgvProbabilidadRiesgo_KeyDown(object sender, KeyEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyCode);
            if (codigo == 8 || e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        if (DgvProbabilidadRiesgo.Rows.Count > 1)
                        {
                            DgvProbabilidadRiesgo.Rows.RemoveAt(DgvProbabilidadRiesgo.CurrentRow.Index);
                        }
                        else
                        {
                            DgvProbabilidadRiesgo.Rows[DgvProbabilidadRiesgo.CurrentRow.Index].Cells["DgvProbabilidadRiesgoColEstimacion"].Value = "";
                            DgvProbabilidadRiesgo.Rows[DgvProbabilidadRiesgo.CurrentRow.Index].Cells["DgvProbabilidadRiesgoColProbabilidad"].Value = null;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void DgvRiesgoOcupacional_KeyDown(object sender, KeyEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyCode);
            if (codigo == 8 || e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        if (DgvRiesgoOcupacional.Rows.Count > 1)
                        {
                            DgvRiesgoOcupacional.Rows.RemoveAt(DgvRiesgoOcupacional.CurrentRow.Index);
                        }
                        else
                        {

                            DgvProbabilidadRiesgo.Rows[DgvRiesgoOcupacional.CurrentRow.Index].Cells["DgvRiesgoOcupacionalColRiengoEspecifico"].Value = "";
                            DgvRiesgoOcupacional.Rows[DgvRiesgoOcupacional.CurrentRow.Index].Cells["DgvRiesgoOcupacionalColEmpresa"].Value = "";
                            DgvRiesgoOcupacional.Rows[DgvRiesgoOcupacional.CurrentRow.Index].Cells["DgvRiesgoOcupacionalColCargo"].Value = "";
                            DgvProbabilidadRiesgo.Rows[DgvRiesgoOcupacional.CurrentRow.Index].Cells["DgvRiesgoOcupacionalColMeses"].Value = "";
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void DgvEnfermedadProfesional_KeyDown(object sender, KeyEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyCode);
            if (codigo == 8 || e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        if (DgvEnfermedadProfesional.Rows.Count > 1)
                        {
                            DgvEnfermedadProfesional.Rows.RemoveAt(DgvEnfermedadProfesional.CurrentRow.Index);
                        }
                        else
                        {
                            DgvEnfermedadProfesional.Rows[DgvEnfermedadProfesional.CurrentRow.Index].Cells["DgvEnfermedadProfesionalColEmpresa"].Value = "";
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void DgvExamenPacticado_KeyDown(object sender, KeyEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyCode);
            if (codigo == 8 || e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        if (DgvExamenPacticado.Rows.Count > 1)
                        {
                            DgvExamenPacticado.Rows.RemoveAt(DgvExamenPacticado.CurrentRow.Index);
                        }
                        else
                        {
                            DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentRow.Index].Cells["DgvExamenPacticadoColAjuntar"].Value = ".....";
                            DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentRow.Index].Cells["DgvExamenPacticadoColResultado"].Value = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        private void DgvHabitos_KeyDown(object sender, KeyEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyCode);
            if (codigo == 8 || e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        if (DgvHabitos.Rows.Count > 1)
                        {
                            DgvHabitos.Rows[DgvHabitos.CurrentRow.Index].Cells["DgvHabitosColCaracteristica"].Value = "";
                            DgvHabitos.Rows[DgvHabitos.CurrentRow.Index].Cells["DgvHabitosColFrecuencia"].Value = "";
                            DgvHabitos.Rows[DgvHabitos.CurrentRow.Index].Cells["DgvHabitosColObservacion"].Value = "";
                            DgvHabitos.Rows[DgvHabitos.CurrentRow.Index].Cells["DgvHabitosColTiempoConsumo"].Value = "";
                        }
                        else
                        {
                            DgvHabitos.Rows[DgvHabitos.CurrentRow.Index].Cells["DgvHabitosColCaracteristica"].Value = "";
                            DgvHabitos.Rows[DgvHabitos.CurrentRow.Index].Cells["DgvHabitosColFrecuencia"].Value = "";
                            DgvHabitos.Rows[DgvHabitos.CurrentRow.Index].Cells["DgvHabitosColObservacion"].Value = "";
                            DgvHabitos.Rows[DgvHabitos.CurrentRow.Index].Cells["DgvHabitosColTiempoConsumo"].Value = "";
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        private void DgvInmunizacion_KeyDown(object sender, KeyEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyCode);
            if (codigo == 8 || e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        if (DgvInmunizacion.Rows.Count > 1)
                        {
                            DgvInmunizacion.Rows.RemoveAt(DgvInmunizacion.CurrentRow.Index);
                        }
                        else
                        {
                            DgvInmunizacion.Rows[DgvInmunizacion.CurrentRow.Index].Cells["DgvInmunizacionColDosis"].Value = "";
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void DgvAntecedenteFamiliar_KeyDown(object sender, KeyEventArgs e)
        {
            int codigo = Convert.ToInt32(e.KeyCode);
            if (codigo == 8 || e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        if (DgvAntecedenteFamiliar.Rows.Count > 1)
                        {
                            DgvAntecedenteFamiliar.Rows.RemoveAt(DgvAntecedenteFamiliar.CurrentRow.Index);
                        }
                        else
                        {
                            DgvAntecedenteFamiliar.Rows[DgvAntecedenteFamiliar.CurrentRow.Index].Cells["DgvAntecedenteFamiliarColMortalidad"].Value = null;
                            DgvAntecedenteFamiliar.Rows[DgvAntecedenteFamiliar.CurrentRow.Index].Cells["DgvAntecedenteFamiliarColParentesco"].Value = "";

                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void DgvAntecedentePersonal_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string CodigoDi = "";
                string titleText = DgvAntecedentePersonal.Columns["DgvAntecedentePersonalColDiagnostico"].HeaderText;
                if (DgvAntecedentePersonal.CurrentCell.ColumnIndex == DgvAntecedentePersonalColDiagnostico.Index)
                {
                    int x = 0;
                    if (DgvAntecedentePersonal.CurrentRow.Cells["DgvAntecedentePersonalColDiagnostico"].Value != null || DgvAntecedentePersonal.CurrentRow.Cells["DgvAntecedentePersonalColDiagnostico"].Value.ToString().Trim() != string.Empty)
                    {
                        string[] DiagnCode = DgvAntecedentePersonal.CurrentRow.Cells["DgvAntecedentePersonalColDiagnostico"].Value.ToString().Split('-');
                        if (DiagnCode.Length > 1 & DiagnCode.Length <= 2)
                        {
                            for (int i = 0; i < DiagnCode.Length; i++)
                            {
                                if (DiagnCode[i].Length <= 4)
                                {
                                    CodigoDi = DiagnCode[i];
                                    DgvAntecedentePersonal.CurrentRow.Cells["DgvAntecedentePersonalColDiagnostico"].Value = CodigoDi;
                                    x++;
                                }
                            }
                            if (x == 0)
                            {
                                MessageBox.Show("Diagnostico no encontrado");
                            }
                        }
                        else if (DiagnCode[0].Length > 0)
                        {
                            int xy = 0;
                            for (int i = 0; i < Datos().Rows.Count; i++)
                            {
                                //MessageBox.Show(Datos().Rows[i]["Codigo"].ToString() + " - " + DiagnCode[0]);
                                if (Datos().Rows[i]["Codigo"].ToString() == DiagnCode[0])
                                {
                                    xy = 1;
                                    break;
                                }
                            }
                            if (xy == 0)
                            {
                                DgvAntecedentePersonal.CurrentRow.Cells["DgvAntecedentePersonalColDiagnostico"].Value = "";
                                MessageBox.Show("Diagnostico no encontrado");
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void DgvExamenPacticado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                try
                {
                    //string Imagen = DgvExamenPacticado.CurrentCell.RowIndex].Cells["DgvExamenPacticadoColAjuntar"].Value;
                    string Imagen = "";
                    Imagen = DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentCell.RowIndex].Cells["DgvExamenPacticadoColAjuntar"].Value.ToString();
                    if (Imagen == "System.Byte[]")
                    {
                        byte[] imageBuffer = (byte[])DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentCell.RowIndex].Cells["DgvExamenPacticadoColAjuntar"].Value;
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
                    //PctFoto.Image = Image.FromFile(Imagen);

                    startInfo.Arguments = @"/c rundll32 ""C:\Program Files\Windows Photo Viewer\PhotoViewer.dll"", ImageView_Fullscreen " + RutaArchivoDestino;
                    process.StartInfo = startInfo;
                    process.Start();
                }
                catch (Exception)
                {
                    try
                    {
                        ClsGuardarImagen ObjGuardarImagen = new ClsGuardarImagen();
                        string nombreexamen = DgvExamenPacticado.Rows[e.RowIndex].Cells["DgvExamenPacticadoColExamen"].Value.ToString();
                        ObjGuardarImagen.AbrirIMG(TxtDocumento.Text, LblEntrada.Text, nombreexamen);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No hay imagen ", "", MessageBoxButtons.OK);
                    }
                }
            }

            if (e.ColumnIndex == DgvExamenPacticadoColEliminar.Index)
            {
                if (e.RowIndex > -1)
                {
                    if (DgvExamenPacticado.Rows.Count > 1)
                    {
                        DgvExamenPacticado.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        DgvExamenPacticado.Rows[e.RowIndex].Cells["DgvExamenPacticadoColAjuntar"].Value = ".....";
                        DgvExamenPacticado.Rows[e.RowIndex].Cells["DgvExamenPacticadoColResultado"].Value = 1;
                    }
                }
            }

        }

        private void PicAspirante_Click(object sender, EventArgs e)
        {
            Paciente f = new Paciente();
            f.ShowDialog();
        }

        public static int CalcularEdad(DateTime fechaNacimiento)
        {
            // Obtiene la fecha actual:
            DateTime fechaActual = DateTime.Today;

            // Comprueba que la se haya introducido una fecha válida; si
            // la fecha de nacimiento es mayor a la fecha actual se muestra mensaje
            // de advertencia:
            if (fechaNacimiento > fechaActual)
            {
                Console.WriteLine("La fecha de nacimiento es mayor que la actual.");
                return -1;
            }
            else
            {
                int edad = fechaActual.Year - fechaNacimiento.Year;

                // Comprueba que el mes de la fecha de nacimiento es mayor
                // que el mes de la fecha actual:
                if (fechaNacimiento.Month > fechaActual.Month)
                {
                    --edad;
                }

                return edad;
            }
        }

        public void CargarReporte()
        {
            string examenes = "";
            string query = "SELECT  dbo.Examen.Exam_Descripcion, dbo.EntradaHistoria.Entr_Numero, dbo.TipResultado.TipRes_Descripcion               " +
                                "FROM  dbo.ExamenPracticado INNER JOIN                                                                              " +
                                "dbo.Examen ON dbo.ExamenPracticado.ExaPrac_Examen_Codigo = dbo.Examen.Exam_Codigo INNER JOIN                       " +
                                "dbo.EntradaHistoria ON dbo.ExamenPracticado.ExaPrac_Entrada_Numero = dbo.EntradaHistoria.Entr_Numero INNER JOIN    " +
                                "dbo.TipResultado ON dbo.ExamenPracticado.ExaPrac_Resultado = dbo.TipResultado.TipRes_Codigo INNER JOIN             " +
                                "dbo.TipResultado AS TipResultado_1 ON dbo.ExamenPracticado.ExaPrac_Resultado = TipResultado_1.TipRes_Codigo WHERE dbo.EntradaHistoria.Entr_Numero=" + LblEntrada.Text;
            DataTable TablaExamenes = new DataTable();
            TablaExamenes = ObjServer.LlenarTabla(query);
            for (int i = 0; i < TablaExamenes.Rows.Count; i++)
            {
                string x = TablaExamenes.Rows[i]["Exam_Descripcion"].ToString() + " (" + TablaExamenes.Rows[i]["TipRes_Descripcion"].ToString() + ")";
                if (i == 0) examenes = x;
                else examenes = examenes + "      " + x;
            }
            string edad = Convert.ToString(CalcularEdad(Convert.ToDateTime(TablaPaciente.Rows[0]["Pac_FechaNacimiento"]))) + " " + "Años";
            FrmVisualizarReporte f = new FrmVisualizarReporte();
            CrystalReport1 cry = new CrystalReport1();
            cry.SetParameterValue("NombreCompleto", TxtNombre.Text);
            cry.SetParameterValue("Cargo", CboCargoOcupacion.Text);
            cry.SetParameterValue("Documento", TablaPaciente.Rows[0]["Pac_Identificacion"]);
            cry.SetParameterValue("Edad", edad);
            cry.SetParameterValue("Genero", TablaPaciente.Rows[0]["Gen_Descripcion"]);
            cry.SetParameterValue("Telefono", TablaPaciente.Rows[0]["Pac_Telefono"].ToString());
            cry.SetParameterValue("TipoExamen", CboTipoExamen.Text);
            cry.SetParameterValue("NombreEmpresa", CboEmpresa.Text);
            cry.SetParameterValue("Ciudad", Configuracion.LugarExamen);
            cry.SetParameterValue("nit", Configuracion.Nit);
            cry.SetParameterValue("tel", Configuracion.Telefono);
            cry.SetParameterValue("direccion", Configuracion.Direccion);
            cry.SetParameterValue("piepagina", Configuracion.PiePagina);
            cry.SetParameterValue("nitempresa", TablaPaciente.Rows[0]["Empre_Nit"].ToString());

            if (examenes.Trim() == "")
                cry.SetParameterValue("ExamenesPracticados", "NO APLICA");
            else
                cry.SetParameterValue("ExamenesPracticados", examenes);
            //ClsSqlServer cls = new ClsSqlServer();
            //cls.Conectar();
            //cry.DataSourceConnections= cls.
            cry.SetParameterValue("Concepto", CboConcepto.Text);
            cry.SetParameterValue("@NumeroEntrada", Convert.ToInt32(LblEntrada.Text));
            //cry.SetParameterValue("@NumeroEntrada", Convert.ToInt32(NumeroEntrada));
            cry.SetParameterValue("Enfasis", CboEnfasis.Text);
            cry.SetParameterValue("FechaExamen", DtFechaEntrada.Text);
            if (TxtCoceptoTarea.Text == "")
                cry.SetParameterValue("DesAptitud", "");
            else
                cry.SetParameterValue("DesAptitud", "CONCEPTO DE APTITUD PARA LA TAREA:");

            cry.SetParameterValue("ConceptoTarea", TxtCoceptoTarea.Text);

            //Query = "SELECT	dbo.ARL.Arl_Codigo, dbo.ARL.Arl_Descripcion, " +
            //               "dbo.EPS.Eps_Codigo, dbo.EPS.Eps_Descripcion,                              " +
            //               "dbo.Paciente.Pac_Identificacion, dbo.Paciente.Pac_Nombre1,                " +
            //               "dbo.Paciente.Pac_Nombre2, dbo.Paciente.Pac_Apellido1,                     " +
            //               "dbo.Paciente.Pac_Apellido2, dbo.EntradaHistoria.Entr_IdPaciente,          " +
            //               "dbo.EntradaHistoria.Entr_FechaEntrada, dbo.EntradaHistoria.Entr_Numero    " +
            //               "FROM	dbo.EntradaHistoria RIGHT OUTER JOIN                               " +
            //               "dbo.EPS INNER JOIN                                                        " +
            //               "dbo.Paciente ON dbo.EPS.Eps_Codigo = dbo.Paciente.Pac_CodEPS INNER JOIN   " +
            //               "dbo.ARL ON dbo.Paciente.Pac_CodARL = dbo.ARL.Arl_Codigo ON                " +
            //               "dbo.EntradaHistoria.Entr_IdPaciente = dbo.Paciente.Pac_Identificacion     " +
            //               "AND dbo.EntradaHistoria.Entr_IdPaciente = dbo.Paciente.Pac_Identificacion WHERE Pac_Identificacion='" + TablaPaciente.Rows[0]["Pac_Identificacion"].ToString() + "'";
            //TablaPaciente = null;
            //TablaPaciente = NuevoSql.LlenarTabla(Query);

            cry.SetParameterValue("EPS", CboEPS.Text);
            cry.SetParameterValue("ARL", CboARL.Text);

            f.crystalReportViewer1.ReportSource = cry;
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargarReporte();
        }

        private void CboTipoExamen_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        public void CARGAR_EXAMENES_PRACTICADOS()
        {
            //CARGAR LOS DATOS EN EXAMENES  PRACTICADOS:::
            DgvExamenPacticado.Rows.Clear();
            DataTable Datos = new DataTable();
            Datos = ObjServer.LlenarTabla("SELECT [ExaPac_Paciente]  ,[ExaPrac_Examen_Codigo] ,[ExaPrac_Resultado], [ExaPrac_Ajuntar] ,[ExaPrac_FechaExamen]  FROM [dbo].[ExamenPracticadoProvi] where [ExaPac_Paciente] = " + TxtDocumento.Text);
            if (Datos.Rows.Count > 0)
            {
                DgvExamenPacticado.RowCount = Datos.Rows.Count + 1;
                for (int i = 0; i < Datos.Rows.Count; i++)
                {
                    DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value = Datos.Rows[i]["ExaPrac_Examen_Codigo"];
                    DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value = Datos.Rows[i]["ExaPrac_Ajuntar"];
                    DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value = Datos.Rows[i]["ExaPrac_FechaExamen"];
                    DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value = Datos.Rows[i]["ExaPrac_Resultado"];
                }
                CargarIconosDGV();
            }
            else
            {
                MessageBox.Show("No se han cargados los examenes", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        }

        private void BtnCargarExamenes_Click(object sender, EventArgs e)
        {
            if (TxtDocumento.Text != "")
            {
                //CARGAR LOS DATOS EN EXAMENES  PRACTICADOS:::
                DgvExamenPacticado.Rows.Clear();
                DataTable Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT [ExaPac_Paciente]  ,[ExaPrac_Examen_Codigo] ,[ExaPrac_Resultado], [ExaPrac_Ajuntar] ,[ExaPrac_FechaExamen]  FROM [dbo].[ExamenPracticadoProvi] where [ExaPac_Paciente] = '" + TxtDocumento.Text + "'");
                if (Datos.Rows.Count > 0)
                {
                    DgvExamenPacticado.RowCount = Datos.Rows.Count + 1;
                    for (int i = 0; i < Datos.Rows.Count; i++)
                    {
                        DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value = Datos.Rows[i]["ExaPrac_Examen_Codigo"];
                        DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value = Datos.Rows[i]["ExaPrac_Ajuntar"];
                        DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value = Datos.Rows[i]["ExaPrac_FechaExamen"];
                        DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value = 1;

                    }
                    CargarIconosDGV();
                }
                else
                {
                    MessageBox.Show("No se han cargados los examenes", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            }
            else
            {
                TxtDocumento.Focus();
                MessageBox.Show("Ingrese un numero de documento", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        public Boolean SooloVer = false;

        public void CargarDatosExamen()
        {
            DataTable Datos = new DataTable();

            if (SooloVer == false)
            {
                if (TxtDocumento.Text != "")
                {
                    //CARGAR LOS DATOS EN EXAMENES  PRACTICADOS:::

                    Datos = ObjServer.LlenarTabla("SELECT [ExaPac_Paciente]  ,[ExaPrac_Examen_Codigo] ,[ExaPrac_Resultado], [ExaPrac_Ajuntar] ,[ExaPrac_FechaExamen]  FROM [dbo].[ExamenPracticadoProvi] where [ExaPac_Paciente] = '" + TxtDocumento.Text + "'"); ;

                    if (Datos.Rows.Count > 0)
                    {
                        DgvExamenPacticado.Rows.Clear();
                        DgvExamenPacticado.RowCount = Datos.Rows.Count + 1;
                        for (int i = 0; i < Datos.Rows.Count; i++)
                        {
                            DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value = Datos.Rows[i]["ExaPrac_Examen_Codigo"];
                            DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value = Datos.Rows[i]["ExaPrac_Ajuntar"];
                            //MessageBox.Show("Desde examen " + Datos.Rows[i]["ExaPrac_Ajuntar"].ToString());
                            if (Datos.Rows[i]["ExaPrac_Ajuntar"].ToString() != "")
                            {
                                DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value = Datos.Rows[i]["ExaPrac_FechaExamen"];
                            }
                            else
                            {
                                DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value = "";
                                DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].ReadOnly = true;
                            }
                            DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value = 1;
                        }
                        CargarIconosDGV();
                    }
                    else
                    {
                        MessageBox.Show("No se han cargados los examenes", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                }
                else
                {
                    //MessageBox.Show("Ingrese un numero de documento", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (TxtDocumento.Text != "")
                {
                    //CARGAR DATOS DE ENTRADAHISTORIA
                    Datos = ObjServer.LlenarTabla("SELECT [Entr_IdPaciente] ,[Entr_FechaEntrada] ,[Entr_Concepto_Codigo] ,[Entr_TipoExamenCodigo] ,Ent_LugarExamen,Ent_Medico " +
                                                ",[Ent_Enfasis]  FROM [dbo].[EntradaProvisional] WHERE [Entr_IdPaciente]='" + TxtDocumento.Text + "'");
                    if (Datos.Rows.Count > 0)
                    {
                        CboConcepto.SelectedValue = Datos.Rows[0]["Entr_Concepto_Codigo"];
                        CboTipoExamen.SelectedValue = Datos.Rows[0]["Entr_TipoExamenCodigo"];
                        CboEnfasis.SelectedValue = Datos.Rows[0]["Ent_Enfasis"];
                        CboProfecional.SelectedValue = Datos.Rows[0]["Ent_Medico"];
                        CboLugarExamen.SelectedValue = Datos.Rows[0]["Ent_LugarExamen"];
                        LblDatos.Text = "Tipo de Examen: " + CboTipoExamen.Text;
                        LblDatos1.Text = "Enfasis: " + CboEnfasis.Text;
                    }
                    else
                    {
                        MessageBox.Show("No se han cargados los datos de entrada tipo de examen, enfasis, etc.", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un numero de documento", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                CargarFecha();
            }
            else
            {
                //CARGAR LOS DATOS EN EXAMENES  PRACTICADOS:::
                Datos = new DataTable();
                Datos = ObjServer.LlenarTablaAsync("SELECT [ExaPrac_Examen_Codigo], [ExaPrac_Entrada_Numero], [ExaPrac_Resultado], [ExaPrac_Ajuntar], [ExaPrac_FechaExamen] FROM [dbo].[ExamenPracticado] WHERE ExaPrac_Entrada_Numero=" + Convert.ToInt32(LblEntrada.Text)); ;
                //MessageBox.Show(Datos.Rows.Count.ToString());
                if (Datos.Rows.Count > 0)
                {
                    DgvExamenPacticado.RowCount = Datos.Rows.Count + 1;
                    for (int i = 0; i < Datos.Rows.Count; i++)
                    {
                        DgvExamenPacticadoColFecha.MaxInputLength = 10;
                        DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value = Datos.Rows[i]["ExaPrac_Examen_Codigo"];
                        DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value = Datos.Rows[i]["ExaPrac_Ajuntar"];
                        if (Datos.Rows[i]["ExaPrac_FechaExamen"].ToString().Substring(0, 10).Trim() != "1/01/1999")
                        {
                            DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value = Datos.Rows[i]["ExaPrac_FechaExamen"];
                        }
                        else
                        {
                            DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value = "";
                            DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].ReadOnly = true;
                        }
                        //DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value = Datos.Rows[i]["ExaPrac_FechaExamen"].ToString().Substring(0,10);
                        DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value = Datos.Rows[i]["ExaPrac_Resultado"];
                    }
                }
            }
        }

        private void BtnActualizarInfor_Click(object sender, EventArgs e)
        {
            if (TxtDocumento.Text != "")
            {
                //CARGAR DATOS DE ENTRADAHISTORIA
                DataTable Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT [Entr_IdPaciente] ,[Entr_FechaEntrada] ,[Entr_Concepto_Codigo] ,[Entr_TipoExamenCodigo],  Ent_LugarExamen,Ent_Medico " +
                                                ",[Ent_Enfasis]  FROM [dbo].[EntradaProvisional] WHERE [Entr_IdPaciente]=" + TxtDocumento.Text);
                if (Datos.Rows.Count > 0)
                {
                    CboConcepto.SelectedValue = Datos.Rows[0]["Entr_Concepto_Codigo"];
                    CboTipoExamen.SelectedValue = Datos.Rows[0]["Entr_TipoExamenCodigo"];
                    CboEnfasis.SelectedValue = Datos.Rows[0]["Ent_Enfasis"];
                    CboProfecional.SelectedValue = Datos.Rows[0]["Ent_Medico"];
                    CboLugarExamen.SelectedValue = Datos.Rows[0]["Ent_LugarExamen"];
                }
                else
                {
                    MessageBox.Show("No se han cargados los datos de entrada tipo de examen, enfasis, etc.", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                //MessageBox.Show("Ingrese un numero de documento", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            CargarFecha();
        }

        private void TxtNombreEmpresa_Leave(object sender, EventArgs e)
        {
            if (TxtNombreEmpresa.Text != "")
            {
                string[] contenido = TxtNombreEmpresa.Text.Split('-');
                if (contenido.Length > 1)
                {
                    TxtNombreEmpresa.Text = contenido[1];
                }
            }

        }

        public void CargarIconosDGV()
        {

            string Imagen = Application.StartupPath + @"\imagenes\ver.png";
            DgvExamenPacticado.Rows[DgvExamenPacticado.Rows.Count - 1].Cells["DgvExamenPacticadoColVer"].Value = Image.FromFile(Imagen);
            DgvExamenPacticado.Rows[DgvExamenPacticado.Rows.Count - 1].Cells["DgvExamenPacticadoColAjuntar"].Value = ".";
            DgvExamenPacticado.Rows[DgvExamenPacticado.Rows.Count - 1].Cells["DgvExamenPacticadoColFecha"].Value = "01/01/1999";
            DgvExamenPacticado.Rows[DgvExamenPacticado.Rows.Count - 1].Cells["DgvExamenPacticadoColResultado"].Value = 1;

        }

        public void CargarFecha()
        {
            string Imagen = Application.StartupPath + @"\imagenes\ver.png";
            DgvExamenPacticado.Rows[DgvExamenPacticado.Rows.Count - 1].Cells["DgvExamenPacticadoColVer"].Value = Image.FromFile(Imagen);
            DgvExamenPacticado.Rows[DgvExamenPacticado.Rows.Count - 1].Cells["DgvExamenPacticadoColFecha"].Value = "01/01/1999";
            DgvExamenPacticado.Rows[DgvExamenPacticado.Rows.Count - 1].Cells["DgvExamenPacticadoColAjuntar"].Value = ".";
            DgvExamenPacticado.Rows[DgvExamenPacticado.Rows.Count - 1].Cells["DgvExamenPacticadoColResultado"].Value = 1;

        }

        private void DgvExamenPacticado_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            CargarFecha();
        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void FrnHistoriaClinica_FormClosed(object sender, FormClosedEventArgs e)
        {
            //FrmPacientesPendientes f = new FrmPacientesPendientes();
            //f.CargarLoad();
            //f.CARGAR_TODO();
        }

        private void RdbNorml_CheckedChanged(object sender, EventArgs e)
        {
            CARGAR_NORMAL();
        }

        public void CARGAR_NORMAL()
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column is DataGridViewComboBoxColumn)
                {
                    dataGridView1.Columns[column.Name].Width = 140;
                    for (int x = 0; x < dataGridView1.Rows.Count; x++)
                    {
                        dataGridView1.Rows[x].Cells[column.Name].Value = 1;
                    }
                }
            }
        }

        private void RdbAnormal_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column is DataGridViewComboBoxColumn)
                {
                    dataGridView1.Columns[column.Name].Width = 140;
                    for (int x = 0; x < dataGridView1.Rows.Count; x++)
                    {
                        dataGridView1.Rows[x].Cells[column.Name].Value = 2;
                    }
                }
            }
        }

        private void RdbNoAplica_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column is DataGridViewComboBoxColumn)
                {
                    dataGridView1.Columns[column.Name].Width = 140;
                    for (int x = 0; x < dataGridView1.Rows.Count; x++)
                    {
                        dataGridView1.Rows[x].Cells[column.Name].Value = 3;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int col = -1;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Visible == false)
                {
                    col++;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[column.Name].Value != null)
                        {
                            string valorEquilibro = dataGridView1.Rows[i].Cells[column.Name].Value.ToString();
                            string valorestado = dataGridView1.Rows[i].Cells["combo" + col.ToString()].Value.ToString();

                            string query = "INSERT INTO [dbo].[EquilibroPaciente] " +
                                           "([EqiPa_Equilibro]                   " +
                                           ",[EqiPa_HistoriaNumero]              " +
                                           ",[EqiPa_Estado])                     " +
                                           " VALUES                              " +
                                           "(" + valorEquilibro + "," + 13193 + "," + valorestado + ")";
                            ObjServer.CadnaSentencia = query;
                            ObjServer.Sentencia();
                        }
                    }
                }
            }
        }

        public void CARGAR_EXAMEN_FISICO()
        {
            DgvExamenFisifoH.Rows.Clear();
            DataTable TablaExamen = new DataTable();
            TablaExamen = ObjServer.LlenarTabla("SELECT [ExFi_Codigo], [ExFi_Descripcion] FROM [dbo].[Examen_Fisico]");
            //Se cargan las columnas de las Recomendaciones
            DataTable TablaEstado = ObjServer.LlenarTabla("SELECT [EsEqui_Codigo] As Codigo ,[EsEqui_Descripcion] As Descripcion FROM [dbo].[EstadoEquilibrioPaciente]");

            DgvExamenFisifoH___.DataSource = TablaEstado;
            DgvExamenFisifoH___.DataSource = TablaEstado;
            DgvExamenFisifoH___.DisplayMember = "Descripcion";
            DgvExamenFisifoH___.ValueMember = "Codigo";

            DgvExamenFisifoH.RowCount = TablaExamen.Rows.Count;
            for (int i = 0; i < DgvExamenFisifoH.Rows.Count; i++)
            {
                DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHCodigo"].Value = TablaExamen.Rows[i]["ExFi_Codigo"];
                DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHDescripcion"].Value = TablaExamen.Rows[i]["ExFi_Descripcion"];
                DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoH___"].Value = 1;

            }
            EstilosDgv(DgvExamenFisifoH);
            DgvExamenFisifoHObservación.Width = 920;
            DgvExamenFisifoH___.Width = 120;
            //RdbNorml.Checked = true;
            //CARGAR_NORMAL();

        }
        private void button4_Click(object sender, EventArgs e)
        {
            //INSERTAR EN EXAMENES FISICO
            for (int i = 0; i < DgvExamenFisifoH.Rows.Count; i++)
            {
                string Codigo = DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHCodigo"].Value.ToString();
                string observacion;
                if (DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHObservación"].Value != null)
                {
                    observacion = DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHObservación"].Value.ToString();
                }
                else
                {
                    observacion = "";

                }

                string examen = DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHCodigo"].Value.ToString();
                string ESTA = DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoH___"].Value.ToString();

                string Quiery = "INSERT INTO [dbo].[Examen_Paciente] " +
                              "([ExamPaci_Examen_Codigo]           " +
                              ",[ExamPaci_Numero_Entrada]          " +
                              ",[ExamPaci_Observacion],ExamPaci_Estado)            " +
                              "  VALUES                            " +
                              "(" + examen + "," + 13225 + ",'" + observacion + "'," + ESTA + ")";
                ObjServer.CadnaSentencia = Quiery;
                ObjServer.Sentencia();
            }
        }

        public void CARGAR_PRUEBA_EQUILIBRIO()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            DataTable Tablaequilibro = new DataTable();
            DataTable TablaEstado = ObjServer.LlenarTabla("SELECT [EsEqui_Codigo] As Codigo ,[EsEqui_Descripcion] As Descripcion FROM [dbo].[EstadoEquilibrioPaciente]");
            //Cargar columnas al DgvRecomendaciones
            Tablaequilibro = ObjServer.LlenarTabla("SELECT [Equi_Codigo],[Equi_Descripcion] FROM [dbo].[Equilibro]");
            DataTable DescripcionReco = new DataTable();
            DescripcionReco = ObjServer.LlenarTabla("SELECT [EqDes_Codigo] ,[EqDes_Descripcion] ,[EqDes_Eqilibrio] FROM [dbo].[EquilibrioDes]");

            //Se cargan las columnas de las Recomendaciones
            for (int i = 0; i < Tablaequilibro.Rows.Count; i++)
            {
                //Se agregan 3 columnas la que va  a contener el codigo, la descripcion  y la que va a chekear, la que contienes el codigo se oculta:
                dataGridView1.Columns.Add(Tablaequilibro.Rows[i]["Equi_Descripcion"].ToString() + i.ToString(), Tablaequilibro.Rows[i]["Equi_Descripcion"].ToString() + i.ToString());
                dataGridView1.Columns.Add(Tablaequilibro.Rows[i]["Equi_Descripcion"].ToString(), Tablaequilibro.Rows[i]["Equi_Descripcion"].ToString());
                DataGridViewComboBoxColumn ComB = new DataGridViewComboBoxColumn();

                ComB.DataSource = TablaEstado;
                ComB.DisplayMember = "Descripcion";
                ComB.ValueMember = "Codigo";
                dataGridView1.Columns.Add(ComB);
                ComB.HeaderText = "NORMAL";
                ComB.Name = "combo" + i.ToString();
                dataGridView1.Columns["combo" + i.ToString()].Width = 110;

                dataGridView1.Columns[(Tablaequilibro.Rows[i]["Equi_Descripcion"].ToString() + i.ToString())].Visible = false;
                dataGridView1.Columns[(Tablaequilibro.Rows[i]["Equi_Descripcion"].ToString())].ReadOnly = true;

            }

            //llenar la cantidad de filas a un vector
            int[] filasReco = new int[Tablaequilibro.Rows.Count];
            for (int i = 0; i < Tablaequilibro.Rows.Count; i++)
            {
                int x = 0;
                for (int j = 0; j < DescripcionReco.Rows.Count; j++)
                {
                    if (Tablaequilibro.Rows[i]["Equi_Codigo"].ToString() == DescripcionReco.Rows[j]["EqDes_Eqilibrio"].ToString())
                    {
                        x++;
                    }
                }
                filasReco[i] = x;
            }

            //Sacar  la cantidad de filas a agregar al DGV
            int y = 0;
            for (int i = 0; i < filasReco.Length; i++)
            {
                y = 0;
                Boolean tru = false;
                for (int x = 0; x < filasReco.Length; x++)
                {
                    if (filasReco[i] >= filasReco[x])
                    {
                        y++;
                        if (y >= filasReco.Length)
                        {
                            y = filasReco[i];
                            tru = true;
                            break;
                        }
                    }
                }
                if (tru)
                    break;
            }

            //Crear las Filas del DGV
            dataGridView1.RowCount = y;
            //Llegar las filas del DGV 
            for (int i = 0; i < Tablaequilibro.Rows.Count; i++)
            {
                int x = 0;
                string Col = Tablaequilibro.Rows[i]["Equi_Descripcion"].ToString();
                string Col2 = Tablaequilibro.Rows[i]["Equi_Descripcion"].ToString() + i.ToString();
                for (int j = 0; j < DescripcionReco.Rows.Count; j++)
                {
                    if (Tablaequilibro.Rows[i]["Equi_Codigo"].ToString() == DescripcionReco.Rows[j]["EqDes_Eqilibrio"].ToString())
                    {
                        dataGridView1.Rows[x].Cells[Col].Value = DescripcionReco.Rows[j]["EqDes_Descripcion"].ToString();
                        dataGridView1.Rows[x].Cells[Col2].Value = DescripcionReco.Rows[j]["EqDes_Codigo"].ToString();

                        x++;
                    }
                }
            }
            EstilosDgv(dataGridView1);
            CARGAR_NORMAL();

        }
        public void CARGAR_DATOS_PUEBA_EQUILIBRIO(int NumeroEntrada)
        {
            try
            {

                DataTable TablaDatos = new DataTable();
                DataTable Tablaprueba = new DataTable();

                string query = "SELECT  dbo.Equilibro.Equi_Codigo,                       " +
                                "dbo.Equilibro.Equi_Descripcion,                         " +
                                "dbo.EquilibrioDes.EqDes_Codigo,                         " +
                                "dbo.EquilibrioDes.EqDes_Descripcion,                    " +
                                "dbo.EquilibroPaciente.EqiPa_Estado,                     " +
                                "dbo.EquilibroPaciente.EqiPa_HistoriaNumero,EqiPa_Reflejos,EqiPa_Marcha,EqiPa_Piel                " +
                                "FROM    dbo.EquilibrioDes INNER JOIN                    " +
                                "dbo.Equilibro ON dbo.EquilibrioDes.EqDes_Eqilibrio      " +
                                "= dbo.Equilibro.Equi_Codigo INNER JOIN                  " +
                                "dbo.EquilibroPaciente ON dbo.EquilibrioDes.EqDes_Codigo " +
                                "= dbo.EquilibroPaciente.EqiPa_Equilibro WHERE EqiPa_HistoriaNumero = " + NumeroEntrada;
                //TablaDatos = ObjServer.LlenarTabla(query);
                //DataTable TablaEstado = ObjServer.LlenarTabla("SELECT [EsEqui_Codigo] As Codigo ,[EsEqui_Descripcion] As Descripcion FROM [dbo].[EstadoEquilibrioPaciente]");
                //Cargar columnas al DgvRecomendaciones
                Tablaprueba = ObjServer.LlenarTabla(query);
                //DataTable DescripcionReco = new DataTable();
                TablaDatos = ObjServer.LlenarTabla(query);
                int filas = 0;
                //MessageBox.Show(TablaDatos.Rows.Count.ToString());
                int num = -1;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    if (column.Visible == false)
                    {
                        //MessageBox.Show(column.Name);
                        num++;
                        //MessageBox.Show("combo" + num.ToString());
                        for (int x = 0; x < dataGridView1.Rows.Count; x++)
                        {
                            //MessageBox.Show(x.ToString());

                            string coldgv = "";
                            if (dataGridView1.Rows[x].Cells[column.Name].Value != null)
                            {
                                coldgv = dataGridView1.Rows[x].Cells[column.Name].Value.ToString();
                            }
                            else
                                coldgv = "";

                            for (int y = 0; y < TablaDatos.Rows.Count; y++)
                            {
                                string coltabla = TablaDatos.Rows[y]["EqDes_Codigo"].ToString();
                                string valestado = TablaDatos.Rows[y]["EqiPa_Estado"].ToString();

                                if (coldgv == coltabla)
                                {
                                    dataGridView1.Rows[x].Cells["combo" + num.ToString()].Value = Convert.ToInt32(valestado);
                                }
                            }
                        }
                    }
                }
                //,;,,  
                TxtReflejos.Text = TablaDatos.Rows[0]["EqiPa_Reflejos"].ToString();
                TxtMarcha.Text = TablaDatos.Rows[0]["EqiPa_Marcha"].ToString();
                TxtPiel.Text = TablaDatos.Rows[0]["EqiPa_Piel"].ToString();
            }
            catch (Exception)
            {

            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            CARGAR_DATOS_PUEBA_EQUILIBRIO(13229);

        }
        public void CARGAR_DATOS_EXAMEN_FISICO(int NumeroHistoria)
        {
            string query = "SELECT  dbo.EstadoEquilibrioPaciente.EsEqui_Codigo, " +
                           "dbo.Examen_Paciente.ExamPaci_Codigo,                   " +
                           "dbo.Examen_Paciente.ExamPaci_Examen_Codigo,            " +
                           "dbo.Examen_Paciente.ExamPaci_Estado,                   " +
                           "dbo.Examen_Paciente.ExamPaci_Numero_Entrada,           " +
                           "dbo.Examen_Paciente.ExamPaci_Observacion             " +
                           "FROM    dbo.Examen_Fisico INNER JOIN                   " +
                           "dbo.Examen_Paciente ON dbo.Examen_Fisico.ExFi_Codigo = " +
                           "dbo.Examen_Paciente.ExamPaci_Examen_Codigo INNER JOIN  " +
                           "dbo.EstadoEquilibrioPaciente ON                        " +
                           "dbo.Examen_Paciente.ExamPaci_Estado =                  " +
                           "dbo.EstadoEquilibrioPaciente.EsEqui_Codigo where ExamPaci_Numero_Entrada = " + NumeroHistoria;

            DataTable TablaExamen = new DataTable();
            TablaExamen = ObjServer.LlenarTabla(query);

            for (int i = 0; i < DgvExamenFisifoH.Rows.Count; i++)
            {
                string val1 = DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHCodigo"].Value.ToString();
                for (int x = 0; x < TablaExamen.Rows.Count; x++)
                {
                    string cod = TablaExamen.Rows[x]["ExamPaci_Estado"].ToString();
                    string val2 = TablaExamen.Rows[x]["ExamPaci_Examen_Codigo"].ToString();
                    string observacion = TablaExamen.Rows[x]["ExamPaci_Observacion"].ToString();

                    if (val1 == val2)
                    {
                        DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoHObservación"].Value = observacion;
                        DgvExamenFisifoH.Rows[i].Cells["DgvExamenFisifoH___"].Value = Convert.ToInt32(cod);
                    }
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CARGAR_DATOS_EXAMEN_FISICO(13229);
        }
        public void CALCULAR_IMC()
        {
            try
            {
                //if (TxtPeso.Text != "" && TxtTalla.Text != "")
                //{
                double peso = Convert.ToDouble(TxtPeso.Text);
                double talla = Convert.ToDouble(TxtTalla.Text) / 100;
                double resul = 0;

                if (talla == 0)
                    resul = 0;
                else
                    resul = peso / (talla * talla);
                //double d = Convert.ToDouble(TxtTalla.Text, CultureInfo.InvariantCulture);
                TxtIMC.Text = Math.Round(resul, 2).ToString();
            }
            catch (Exception ex)
            {

            }


        }
        private void TxtPeso_TextChanged(object sender, EventArgs e)
        {
            //CALCULAR_IMC();
            try
            {
                if (TxtPeso.Text != "" & TxtTalla.Text != "")
                {
                    if (Convert.ToInt32(TxtPeso.Text) > 0)
                    {
                        CALCULAR_IMC();
                    }
                }
            }
            catch (Exception)
            {
                TxtTalla.Text = "0";
                MessageBox.Show("Caracter no permitido", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TxtTalla_VisibleChanged(object sender, EventArgs e)
        {
            CALCULAR_IMC();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (TxtDocumento.BackColor == Color.SteelBlue)
            {
                //MessageBox.Show("Rojo");
                if (MessageBox.Show("Realmente desea CONTINUAR?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    int NumeroEntrada = Convert.ToInt32(LblEntrada.Text);
                    ActualizarDatos(NumeroEntrada, 1);
                }
            }
            else
            {
                GUARDAR_HISTORIA(1);
            }
        }

        private async void BtnInfoOcupacional_Click(object sender, EventArgs e)
        {
            await CARGAR_INFORMACION_OCUPACIONAL();
        }

        private void DgvDiagnostico_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox autoText = e.Control as TextBox;
            if (DgvDiagnostico.CurrentCell.ColumnIndex == DgvDiagnosticoColDiagnostico.Index)
            {
                if (autoText != null)
                {
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                    addItems(DataCollection);
                    autoText.AutoCompleteCustomSource = DataCollection;
                }
            }
            else
            {
                autoText.AutoCompleteCustomSource = null;
            }
        }

        private void DgvDiagnostico_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string CodigoDi = "";
                string titleText = DgvDiagnostico.Columns["DgvDiagnosticoColDiagnostico"].HeaderText;
                if (DgvDiagnostico.CurrentCell.ColumnIndex == DgvDiagnosticoColDiagnostico.Index)
                {
                    if (DgvDiagnostico.CurrentRow.Cells["DgvDiagnosticoColDiagnostico"].Value != null || DgvDiagnostico.CurrentRow.Cells["DgvDiagnosticoColDiagnostico"].Value.ToString().Trim() != string.Empty)
                    {
                        int x = 0;
                        string[] DiagnCode = DgvDiagnostico.CurrentRow.Cells["DgvDiagnosticoColDiagnostico"].Value.ToString().Split('-');
                        //MessageBox.Show(DiagnCode.Length.ToString());
                        if (DiagnCode.Length > 1 & DiagnCode.Length <= 2)
                        {
                            for (int i = 0; i < DiagnCode.Length; i++)
                            {
                                if (DiagnCode[i].Length <= 4)
                                {
                                    CodigoDi = DiagnCode[i];
                                    DgvDiagnostico.CurrentRow.Cells["DgvDiagnosticoColCodigo"].Value = CodigoDi;
                                    DgvDiagnostico.CurrentRow.Cells["DgvDiagnosticoColDiagnostico"].Value = CodigoDi;

                                    x++;
                                }
                            }
                            if (x == 0)
                            {
                                MessageBox.Show("Diagnostico no encontrado");
                            }
                        }
                        else if (DiagnCode[0].Length > 0)
                        {
                            int xy = 0;
                            for (int i = 0; i < Datos().Rows.Count; i++)
                            {
                                if (Datos().Rows[i]["Codigo"].ToString() == DiagnCode[0])
                                {
                                    xy = 1;
                                    break;
                                }
                            }
                            if (xy == 0)
                            {
                                DgvDiagnostico.CurrentRow.Cells["DgvDiagnosticoColDiagnostico"].Value = "";
                                MessageBox.Show("Diagnostico no encontrado");
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void DgvDiagnostico_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvDiagnosticoColEliminar.Index)
            {
                if (e.RowIndex > -1)
                {

                    if (DgvDiagnostico.Rows.Count > 1)
                    {
                        DgvDiagnostico.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
        }

        private void DgvAntecedentePersonal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvAntecedentePersonalColElimnar.Index)
            {
                if (e.RowIndex > -1)
                {
                    if (DgvAntecedentePersonal.Rows.Count > 1)
                    {
                        DgvAntecedentePersonal.Rows[e.RowIndex].Cells["DgvAntecedentePersonalColDiagnostico"].Value = "";
                        DgvAntecedentePersonal.Rows[e.RowIndex].Cells["DgvAntecedentePersonalColObservacion"].Value = "";
                    }
                }
            }
        }

        private void DgvAntecedenteFamiliar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvAntecedenteFamiliarColEliminar.Index)
            {
                if (e.RowIndex > -1)
                {
                    if (DgvAntecedenteFamiliar.Rows.Count > 1)
                    {
                        DgvAntecedenteFamiliar.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        DgvAntecedenteFamiliar.Rows[e.RowIndex].Cells["DgvAntecedenteFamiliarColMortalidad"].Value = null;
                        DgvAntecedenteFamiliar.Rows[e.RowIndex].Cells["DgvAntecedenteFamiliarColParentesco"].Value = "";

                    }
                }
            }
        }

        private void DgvHabitos_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == DgvHabitosColEliminar.Index)
            {
                if (e.RowIndex > -1)
                {
                    if (DgvHabitos.Rows.Count > 1)
                    {
                        DgvHabitos.Rows[e.RowIndex].Cells["DgvHabitosColCaracteristica"].Value = "";
                        DgvHabitos.Rows[e.RowIndex].Cells["DgvHabitosColFrecuencia"].Value = "";
                        DgvHabitos.Rows[e.RowIndex].Cells["DgvHabitosColObservacion"].Value = "";
                        DgvHabitos.Rows[e.RowIndex].Cells["DgvHabitosColTiempoConsumo"].Value = "";
                    }
                    else
                    {
                        DgvHabitos.Rows[e.RowIndex].Cells["DgvHabitosColCaracteristica"].Value = "";
                        DgvHabitos.Rows[e.RowIndex].Cells["DgvHabitosColFrecuencia"].Value = "";
                        DgvHabitos.Rows[e.RowIndex].Cells["DgvHabitosColObservacion"].Value = "";
                        DgvHabitos.Rows[e.RowIndex].Cells["DgvHabitosColTiempoConsumo"].Value = "";
                    }
                }
            }
        }

        private void DgvInmunizacion_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == DgvInmunizacionColEliminar.Index)
            {
                if (e.RowIndex > -1)
                {
                    if (DgvInmunizacion.Rows.Count > 1)
                    {
                        DgvInmunizacion.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        DgvInmunizacion.Rows[e.RowIndex].Cells["DgvInmunizacionColDosis"].Value = "";
                    }
                }
            }

        }

        private void DgvAccidenteLaboral_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvAccidenteLaboralColEliminar.Index)
            {
                if (e.RowIndex > -1)
                {
                    if (DgvAccidenteLaboral.Rows.Count > 1)
                    {
                        DgvAccidenteLaboral.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        DgvAccidenteLaboral.Rows[e.RowIndex].Cells["DgvAccidenteLaboralColDias"].Value = "";
                        DgvAccidenteLaboral.Rows[e.RowIndex].Cells["DgvAccidenteLaboralColEmpresa"].Value = "";
                        DgvAccidenteLaboral.Rows[e.RowIndex].Cells["DgvAccidenteLaboralNaturaleza"].Value = "";
                        DgvAccidenteLaboral.Rows[e.RowIndex].Cells["DgvAccidenteLaboralColSecuela"].Value = "";
                    }
                }
            }
        }

        private void DgvProbabilidadRiesgo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvProbabilidadRiesgoColEliminar.Index)
            {
                if (e.RowIndex > -1)
                {
                    if (DgvProbabilidadRiesgo.Rows.Count > 1)
                    {
                        DgvProbabilidadRiesgo.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        DgvProbabilidadRiesgo.Rows[e.RowIndex].Cells["DgvProbabilidadRiesgoColEstimacion"].Value = "";
                        DgvProbabilidadRiesgo.Rows[e.RowIndex].Cells["DgvProbabilidadRiesgoColProbabilidad"].Value = null;
                    }
                }
            }
        }

        private void DgvEnfermedadProfesional_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvEnfermedadProfesionalColEliminar.Index)
            {
                if (e.RowIndex > -1)
                {
                    if (DgvEnfermedadProfesional.Rows.Count > 1)
                    {
                        DgvEnfermedadProfesional.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        DgvEnfermedadProfesional.Rows[e.RowIndex].Cells["DgvEnfermedadProfesionalColEmpresa"].Value = "";
                    }
                }
            }
        }

        private void DgvRiesgoOcupacional_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == DgvRiesgoOcupacionalColEliminar.Index)
                {
                    if (e.RowIndex > -1)
                    {
                        if (DgvRiesgoOcupacional.Rows.Count > 1)
                        {
                            DgvRiesgoOcupacional.Rows.RemoveAt(e.RowIndex);
                        }
                        else
                        {
                            DgvProbabilidadRiesgo.Rows[e.RowIndex].Cells["DgvRiesgoOcupacionalColRiengoEspecifico"].Value = "";
                            DgvRiesgoOcupacional.Rows[e.RowIndex].Cells["DgvRiesgoOcupacionalColEmpresa"].Value = "";
                            DgvRiesgoOcupacional.Rows[e.RowIndex].Cells["DgvRiesgoOcupacionalColCargo"].Value = "";
                            DgvProbabilidadRiesgo.Rows[e.RowIndex].Cells["DgvRiesgoOcupacionalColMeses"].Value = "";
                        }
                    }
                }

            }
            catch (Exception)
            {

            }
        }

        private void DgvExamenFisifoH_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == DgvExamenFisifoEliminar.Index)
            {
                if (e.RowIndex > -1)
                {
                    if (DgvExamenFisifoH.Rows.Count > 1)
                    {
                        DgvExamenFisifoH.Rows[e.RowIndex].Cells["DgvExamenFisifoHObservación"].Value = "";
                    }
                }
            }
        }

        public void DesactivarHabitos(int fila)
        {
            DgvHabitos.Rows[fila].Cells["DgvHabitosColObservacion"].ReadOnly = false;
            DgvHabitos.Rows[fila].Cells["DgvHabitosColCaracteristica"].ReadOnly = false;
            DgvHabitos.Rows[fila].Cells["DgvHabitosColFrecuencia"].ReadOnly = false;
            DgvHabitos.Rows[fila].Cells["DgvHabitosColTiempoConsumo"].ReadOnly = false;
            DgvHabitos.Rows[fila].Cells["DgvHabitosColEliminar"].ReadOnly = false;

            DgvHabitos.Rows[fila].Cells["DgvHabitosColObservacion"].Style.BackColor = Color.White;
            DgvHabitos.Rows[fila].Cells["DgvHabitosColCaracteristica"].Style.BackColor = Color.White;
            DgvHabitos.Rows[fila].Cells["DgvHabitosColFrecuencia"].Style.BackColor = Color.White;
            DgvHabitos.Rows[fila].Cells["DgvHabitosColHabito"].Style.BackColor = Color.White;
            DgvHabitos.Rows[fila].Cells["DgvHabitosColTiempoConsumo"].Style.BackColor = Color.White;
            DgvHabitos.Rows[fila].Cells["DgvHabitosColEliminar"].Style.BackColor = Color.White;
        }

        public void DesactivarHabitos()
        {
            DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColObservacion"].ReadOnly = true;
            DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColCaracteristica"].ReadOnly = true;
            DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColFrecuencia"].ReadOnly = true;
            DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColTiempoConsumo"].ReadOnly = true;
            DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColEliminar"].ReadOnly = true;

            DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColObservacion"].Style.BackColor = Color.LightGray;
            DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColCaracteristica"].Style.BackColor = Color.LightGray;
            DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColFrecuencia"].Style.BackColor = Color.LightGray;
            DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColHabito"].Style.BackColor = Color.LightGray;
            DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColTiempoConsumo"].Style.BackColor = Color.LightGray;
            DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColEliminar"].Style.BackColor = Color.LightGray;

            DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColObservacion"].Value = " ";
            DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColCaracteristica"].Value = " ";
            DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColFrecuencia"].Value = " ";
            DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColTiempoConsumo"].Value = " ";
        }

        private void DgvHabitos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DgvHabitos.CurrentCell.ColumnIndex == DgvHabitosColSiNo.Index)
            {

                DgvHabitos.BeginEdit(true);
                ComboBox cmbMiCtrl = (ComboBox)DgvHabitos.EditingControl;
                string Valor = cmbMiCtrl.Text;
                DgvHabitos.EndEdit();
                DataGridViewRow row = DgvHabitos.CurrentRow;
                //DataGridViewCheckBoxCell cell = row.Cells["DgvHabitosColSiNo"] as DataGridViewCheckBoxCell;
                string xx = DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColSiNo"].Value.ToString();

                if (xx == "NO")
                {
                    DesactivarHabitos();
                }
                else
                {
                    DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColObservacion"].ReadOnly = false;
                    DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColCaracteristica"].ReadOnly = false;
                    DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColFrecuencia"].ReadOnly = false;
                    DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColTiempoConsumo"].ReadOnly = false;
                    DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColEliminar"].ReadOnly = false;

                    DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColObservacion"].Style.BackColor = Color.White;
                    DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColCaracteristica"].Style.BackColor = Color.White;
                    DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColFrecuencia"].Style.BackColor = Color.White;
                    DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColHabito"].Style.BackColor = Color.White;
                    DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColTiempoConsumo"].Style.BackColor = Color.White;
                    DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColEliminar"].Style.BackColor = Color.White;

                    DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColObservacion"].Value = "";
                    DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColCaracteristica"].Value = "";
                    DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColFrecuencia"].Value = "";
                    DgvHabitos.Rows[DgvHabitos.CurrentCell.RowIndex].Cells["DgvHabitosColTiempoConsumo"].Value = "";
                }
            }
        }

        private void GrpContenedor_Enter(object sender, EventArgs e)
        {

        }

        private void TxtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjServer.Solo_Numeros(e);
        }

        private void BtnAbrirGestionSiatema_Click(object sender, EventArgs e)
        {
            FrmGestionarSistema f = new FrmGestionarSistema();
            f.ShowDialog();
        }
        //dfghj
        #region REFRESCAR ANTECEDENTES PERSONALES
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            string query = "";
            string ColCod = "";
            string ColDes = "";

            //Cargar los antecedentes  al DgvAntecedentes
            DgvAntecedentePersonal.Rows.Clear();
            query = "SELECT [Ant_Codigo] As [Codigo], [Ant_Descripcion] As [Descripcion] FROM [dbo].[Antecedente]";
            ColCod = "DgvAntecedentePersonalColCodigo";
            ColDes = "DgvAntecedentePersonalColAntecedente";
            CargarColumnas(DgvAntecedentePersonal, query, ColCod, ColDes);
            //----Fin cargar Antecedentes----
            MessageBox.Show("Se ha refrescado", "Aceptar", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        #endregion

        #region REFRESCAR ANTECEDENTES FAMILIARES
        private void pictureBox22_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region REFRESCAR HABITOS
        private void pictureBox20_Click(object sender, EventArgs e)
        {
            string query = "";
            string ColCod = "";
            string ColDes = "";
            //Cargar Los Habitos al DGVhabitos
            DgvHabitos.Rows.Clear();
            query = "SELECT [Hab_Codigo] As [Codigo] ,[Hab_Descripcion] As [Descripcion] FROM [dbo].[Habito]";
            ColCod = "DgvHabitosColCodigo";
            ColDes = "DgvHabitosColHabito";
            CargarColumnas(DgvHabitos, query, ColCod, ColDes);
            for (int i = 0; i < DgvHabitos.Rows.Count; i++)
                DgvHabitos.Rows[i].Cells["DgvHabitosColNO"].Value = 1;
            //-----Fin cargar Habitos-----
            MessageBox.Show("Se ha refrescado", "Aceptar", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        #endregion

        #region REFRESCAR INMUNIZACIÓN
        private void pictureBox21_Click(object sender, EventArgs e)
        {
            //cargar las enfermedades al DgvInmunizacion
            DgvInmunizacion.Rows.Clear();
            DgvInmunizacionColTipoImn.DataSource = ObjServer.LlenarTabla("SELECT [TipInm_Codigo] as Codigo,[TipInm_Descripcion] as Descripcion  FROM [dbo].[TipoInmunizacion] order by TipInm_Descripcion ");
            DgvInmunizacionColTipoImn.DisplayMember = "Descripcion";
            DgvInmunizacionColTipoImn.ValueMember = "Codigo";
            //----Fin cargar los tipos DgvInmunizacion
            MessageBox.Show("Se ha refrescado", "Aceptar", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        #endregion

        #region REFRESCAR   ACCIDENTES LABORALES
        private void pictureBox19_Click(object sender, EventArgs e)
        {
            //
            DgvAccidenteLaboralColParteAfectada.DataSource = ObjServer.LlenarTabla("SELECT [PartA_Codigo] As Codigo,[PartA_Descripcion] As [Descripcion] FROM [dbo].[ParteAfectada]");
            DgvAccidenteLaboralColParteAfectada.DisplayMember = "Descripcion";
            DgvAccidenteLaboralColParteAfectada.ValueMember = "Codigo";
            DgvAccidenteLaboral.Rows[0].Cells[2].Value = 1;
            //MessageBox.Show(ObjServer.x.ToString());
            MessageBox.Show("Se ha refrescado", "Aceptar", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        #endregion

        #region REFRESCAR PROBABILIDAD DE RIESGO
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            string query = "";
            string ColCod = "";
            string ColDes = "";
            //C
            //Combo de Tipo de Riesgo
            query = "";
            query = "SELECT [TipoRiesg_Codigo] As [Codigo], [TipoRiesg_Descripcion] As [Descripcion] FROM [dbo].[TipoRiesgo]";
            DgvProbabilidadRiesgoColTipoRiesgo.DataSource = ObjServer.LlenarTabla(query);
            DgvProbabilidadRiesgoColTipoRiesgo.DisplayMember = "Descripcion";
            DgvProbabilidadRiesgoColTipoRiesgo.ValueMember = "Codigo";
            //DGVProbabilidad
            //<<<<<<Combo de probabilidad>>>>>>>>>
            DgvProbabilidadRiesgo.Rows.Clear();
            query = "";
            query = "";
            DgvProbabilidadRiesgoColProbabilidad.DataSource = ObjServer.LlenarTabla("SELECT [Prob_Codigo] As [Codigo] ,[Prob_Descripcion] As [Descripcion] FROM [dbo].[Probabilidad]");
            DgvProbabilidadRiesgoColProbabilidad.DisplayMember = "Descripcion";
            DgvProbabilidadRiesgoColProbabilidad.ValueMember = "Codigo";
            MessageBox.Show("Se ha refrescado", "Aceptar", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        #endregion


        #region REFRESCAR ENFERMEDAD PROFESIONAL
        private void pictureBox18_Click(object sender, EventArgs e)
        {
            //cargar las enfermedades al DgvEnfermedadProfesional
            DgvEnfermedadProfesionalColEnfermedad.DataSource = ObjServer.LlenarTabla("SELECT [Enf_Codigo] as Codigo ,[Enf_Descipcion] as Descripcion FROM [dbo].[Enfermedad]");
            DgvEnfermedadProfesionalColEnfermedad.DisplayMember = "Descripcion";
            DgvEnfermedadProfesionalColEnfermedad.ValueMember = "Codigo";
            //----Fin cargar enfermedades DgvEnfermedadProfesionalm
            MessageBox.Show("Se ha refrescado", "Aceptar", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        #endregion

        #region REFRESCAR RIESGOS OCUPACIONALES
        private void pictureBox23_Click(object sender, EventArgs e)
        {
            //Cargar los riesgos al DgvRiesgos
            DgvRiesgoOcupacional.Rows.Clear();
            string query = "SELECT [TipoRiesg_Codigo] As [Codigo], [TipoRiesg_Descripcion] As [Descripcion] FROM [dbo].[TipoRiesgo]";
            string ColCod = "DgvRiesgoOcupacionalColRiesgoCodio";
            string ColDes = "DgvRiesgoOcupacionalColRiesgo";
            DataTable tabla_riesgo = new DataTable();
            tabla_riesgo = ObjServer.LlenarTabla(query);
            DgvRiesgoOcupacionalColRiesgo.DataSource = tabla_riesgo;
            DgvRiesgoOcupacionalColRiesgo.DisplayMember = "Descripcion";
            DgvRiesgoOcupacionalColRiesgo.ValueMember = "Codigo";
            //CargarColumnas(DgvRiesgoOcupacional, query, ColCod, ColDes);
            DgvRiesgoOcupacional.RowCount = tabla_riesgo.Rows.Count + 1;
            for (int i = 0; i < DgvRiesgoOcupacional.Rows.Count - 1; i++)
            {
                DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiesgo"].Value = tabla_riesgo.Rows[i]["Codigo"];
                //DgvRiesgoOcupacional.Rows[i].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
            //----Fin cargar riesgos----
            MessageBox.Show("Se ha refrescado", "Aceptar", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        #endregion

        #region EXAMEN FISICO
        private void pictureBox26_Click(object sender, EventArgs e)
        {
            CARGAR_EXAMEN_FISICO();
            MessageBox.Show("Se ha refrescado", "Aceptar", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        #endregion

        #region PRUEBA DE EQUILIBRIO
        private void pictureBox25_Click(object sender, EventArgs e)
        {

            CARGAR_PRUEBA_EQUILIBRIO();
            MessageBox.Show("Se ha refrescado", "Aceptar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region REFRESCAR REVISIÓN POR SISTEMA
        private void pictureBox27_Click(object sender, EventArgs e)
        {
            int y;
            //Cargar los Revisiones al DGVRevisiones
            Dgvrevision.Rows.Clear();
            Dgvrevision.Columns.Clear();
            Tablasistema = ObjServer.LlenarTabla("SELECT [Sist_Codigo] ,[Sist_Descripcion] FROM [dbo].[Sistema]");
            DataTable TablaRevision = ObjServer.LlenarTabla("SELECT [Revi_Codigo], [Revi_Descripcion], [Revi_Sistema_Codigo] FROM [dbo].[Revision] order by [Revi_Sistema_Codigo]");

            //Cargar columnas al DGVRevision
            for (int i = 0; i < Tablasistema.Rows.Count; i++)
            {
                Dgvrevision.Columns.Add(Tablasistema.Rows[i]["Sist_Descripcion"].ToString() + i.ToString(), Tablasistema.Rows[i]["Sist_Descripcion"].ToString() + i.ToString());
                Dgvrevision.Columns.Add(Tablasistema.Rows[i]["Sist_Descripcion"].ToString(), Tablasistema.Rows[i]["Sist_Descripcion"].ToString());
                DataGridViewCheckBoxColumn SI = new DataGridViewCheckBoxColumn();
                Dgvrevision.Columns.Add(SI);
                SI.HeaderText = "SI";
                SI.Name = "chk" + i.ToString();
                Dgvrevision.Columns["chk" + i.ToString()].Width = 30;
                Dgvrevision.Columns[(Tablasistema.Rows[i]["Sist_Descripcion"].ToString() + i.ToString())].Visible = false;
                Dgvrevision.Columns[(Tablasistema.Rows[i]["Sist_Descripcion"].ToString())].ReadOnly = true;
            }

            //llenar la cantidad de filas a un vector
            int[] filas = new int[Tablasistema.Rows.Count];
            for (int i = 0; i < Tablasistema.Rows.Count; i++)
            {
                int x = 0;
                for (int j = 0; j < TablaRevision.Rows.Count; j++)
                {
                    if (Tablasistema.Rows[i]["Sist_Codigo"].ToString() == TablaRevision.Rows[j]["Revi_Sistema_Codigo"].ToString())
                    {
                        x++;
                    }
                }
                filas[i] = x;
            }

            //Sacar  la cantidad de filas a agregar al DGV
            y = 0;
            for (int i = 0; i < filas.Length; i++)
            {
                y = 0;
                Boolean tru = false;
                for (int x = 0; x < filas.Length; x++)
                {
                    if (filas[i] >= filas[x])
                    {
                        y++;
                        if (y >= filas.Length)
                        {
                            y = filas[i];
                            tru = true;
                            break;
                        }
                    }
                }
                if (tru)
                    break;
            }

            //Crear las Filas del DGV
            Dgvrevision.RowCount = y;

            //Llegar las filas del DGV 
            for (int i = 0; i < Tablasistema.Rows.Count; i++)
            {
                int x = 0;
                string Col = Tablasistema.Rows[i]["Sist_Descripcion"].ToString();
                string Col2 = Tablasistema.Rows[i]["Sist_Descripcion"].ToString() + i.ToString();
                for (int j = 0; j < TablaRevision.Rows.Count; j++)
                {
                    if (Tablasistema.Rows[i]["Sist_Codigo"].ToString() == TablaRevision.Rows[j]["Revi_Sistema_Codigo"].ToString())
                    {
                        Dgvrevision.Rows[x].Cells[Col].Value = TablaRevision.Rows[j]["Revi_Descripcion"].ToString();
                        Dgvrevision.Rows[x].Cells[Col2].Value = TablaRevision.Rows[j]["Revi_Codigo"].ToString();

                        x++;
                    }
                }
            }
            EstilosDgv(Dgvrevision);
            //--------Fin Cargar revisiones--------
            MessageBox.Show("Se ha refrescado", "Aceptar", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        #endregion

        #region REFRESCAR RECOMENDACIONES
        private void pictureBox28_Click(object sender, EventArgs e)
        {
            DgvRecomendaciones.Columns.Clear();
            DgvRecomendaciones.Rows.Clear();
            //Cargar columnas al DgvRecomendaciones
            Recomendaciones = ObjServer.LlenarTabla("SELECT [Reco_Codigo] ,[Reco_Descripcion] FROM [dbo].[Recomendacion]");
            DataTable DescripcionReco = new DataTable();
            DescripcionReco = ObjServer.LlenarTabla("SELECT [RecDes_Codigo] ,[RecDes_Descripcion] ,[RecDes_Recomendacios_Codigo]  FROM [dbo].[RecomendacionDescripcion] Order By [RecDes_Recomendacios_Codigo]");
            //Se cargan las columnas de las Recomendaciones
            for (int i = 0; i < Recomendaciones.Rows.Count; i++)
            {
                //Se agregan 3 columnas la que va  a contener el codigo, la descripcion  y la que va a chekear, la que contienes el codigo se oculta:
                DgvRecomendaciones.Columns.Add(Recomendaciones.Rows[i]["Reco_Descripcion"].ToString() + i.ToString(), Recomendaciones.Rows[i]["Reco_Descripcion"].ToString() + i.ToString());
                DgvRecomendaciones.Columns.Add(Recomendaciones.Rows[i]["Reco_Descripcion"].ToString(), Recomendaciones.Rows[i]["Reco_Descripcion"].ToString());

                DataGridViewCheckBoxColumn SI = new DataGridViewCheckBoxColumn();
                DgvRecomendaciones.Columns.Add(SI);
                SI.HeaderText = "SI";
                SI.Name = "chk" + i.ToString();
                DgvRecomendaciones.Columns["chk" + i.ToString()].Width = 30;
                DgvRecomendaciones.Columns[(Recomendaciones.Rows[i]["Reco_Descripcion"].ToString() + i.ToString())].Visible = false;
                DgvRecomendaciones.Columns[(Recomendaciones.Rows[i]["Reco_Descripcion"].ToString())].ReadOnly = true;
            }

            //llenar la cantidad de filas a un vector
            int[] filasReco = new int[Recomendaciones.Rows.Count];
            for (int i = 0; i < Recomendaciones.Rows.Count; i++)
            {
                int x = 0;
                for (int j = 0; j < DescripcionReco.Rows.Count; j++)
                {
                    if (Recomendaciones.Rows[i]["Reco_Codigo"].ToString() == DescripcionReco.Rows[j]["RecDes_Recomendacios_Codigo"].ToString())
                    {
                        x++;
                    }
                }
                filasReco[i] = x;
            }

            //Sacar  la cantidad de filas a agregar al DGV
            int y = 0;
            for (int i = 0; i < filasReco.Length; i++)
            {
                y = 0;
                Boolean tru = false;
                for (int x = 0; x < filasReco.Length; x++)
                {
                    if (filasReco[i] >= filasReco[x])
                    {
                        y++;
                        if (y >= filasReco.Length)
                        {
                            y = filasReco[i];
                            tru = true;
                            break;
                        }
                    }
                }
                if (tru)
                    break;
            }

            //Crear las Filas del DGV
            DgvRecomendaciones.RowCount = y;
            //Llegar las filas del DGV 
            for (int i = 0; i < Recomendaciones.Rows.Count; i++)
            {
                int x = 0;
                string Col = Recomendaciones.Rows[i]["Reco_Descripcion"].ToString();
                string Col2 = Recomendaciones.Rows[i]["Reco_Descripcion"].ToString() + i.ToString();
                for (int j = 0; j < DescripcionReco.Rows.Count; j++)
                {
                    if (Recomendaciones.Rows[i]["Reco_Codigo"].ToString() == DescripcionReco.Rows[j]["RecDes_Recomendacios_Codigo"].ToString())
                    {
                        DgvRecomendaciones.Rows[x].Cells[Col].Value = DescripcionReco.Rows[j]["RecDes_Descripcion"].ToString();
                        DgvRecomendaciones.Rows[x].Cells[Col2].Value = DescripcionReco.Rows[j]["RecDes_Codigo"].ToString();

                        x++;
                    }
                }
            }
            EstilosDgv(DgvRecomendaciones);
            //<<<<<<<<<<<<<<Fin cargar dgv Recomendaciones >>>>>>>>>>>>>>>>>>>
            MessageBox.Show("Se ha refrescado", "Aceptar", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        #endregion

        #region REFRESCAR DIAGNOSTICO
        private void pictureBox30_Click(object sender, EventArgs e)
        {
        }
        #endregion

        #region REFRESCAR CONCEPTO, TIPO DE EXAMEN Y ENFASIS
        private void pictureBox24_Click(object sender, EventArgs e)
        {

            // Cargo los datos que tendra el combobox Concepto
            DataTable tableConcepto = new DataTable();
            CboConcepto.DataSource = ObjServer.LlenarTabla("SELECT [Conc_Codigo] As Codigo,[Conc_Descripcion] As Descripcion FROM [dbo].[Concepto]");
            CboConcepto.DisplayMember = "Descripcion";
            CboConcepto.ValueMember = "Codigo";
            //
            //Cargar combo de tipo examen
            CboTipoExamen.DataSource = ObjServer.LlenarTabla("SELECT [TipoExam_Codigo] As Codigo ,[TipoExam_Descripcion] As Descripcion FROM [dbo].[TipoExamen]");
            CboTipoExamen.DisplayMember = "Descripcion";
            CboTipoExamen.ValueMember = "Codigo";
            //
            CargarEnfasisEn();
            MessageBox.Show("Se ha refrescado", "Aceptar", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        #endregion

        private void TxtTalla_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TxtPeso.Text != "" & TxtTalla.Text != "")
                {
                    if (Convert.ToInt32(TxtTalla.Text) > 0)
                    {
                        CALCULAR_IMC();
                    }
                }
            }
            catch (Exception)
            {
                TxtTalla.Text = "0";
                MessageBox.Show("Caracter no permitido", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void DgvInmunizacion_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DgvInmunizacion_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvInmunizacionColFecha.Index)
            {
                if (e.RowIndex > -1)
                {
                    string fecha = DgvInmunizacion.Rows[e.RowIndex].Cells["DgvInmunizacionColFecha"].Value.ToString();
                    int cell = e.RowIndex;
                    //MessageBox.Show(Convert.ToDateTime(fecha).ToString() + "==" + DateTime.Now.Date.ToString());
                    if (ValidarFecha(fecha) == false)
                    {
                        MessageBox.Show("Fecha incorrecta");
                        DgvInmunizacion.Rows[cell].Cells["DgvInmunizacionColFecha"].Value = "01/01/1990";
                    }
                    else
                    {
                        if (Convert.ToDateTime(fecha) > DateTime.Now.Date)
                        {
                            MessageBox.Show("Fecha mayor a la actual");
                            DgvInmunizacion.Rows[cell].Cells["DgvInmunizacionColFecha"].Value = DateTime.Now.Date.ToString().Substring(0, 10);
                        }
                    }

                }
            }
        }

        private void DgvAccidenteLaboral_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvAccidenteLaboralColFecha.Index)
            {
                if (e.RowIndex > -1)
                {

                    int cell = e.RowIndex;
                    string fecha = DgvAccidenteLaboral.Rows[e.RowIndex].Cells["DgvAccidenteLaboralColFecha"].Value.ToString();
                    if (ValidarFecha(fecha) == false)
                    {
                        MessageBox.Show("Fecha incorrecta");
                        DgvAccidenteLaboral.Rows[cell].Cells["DgvAccidenteLaboralColFecha"].Value = "01/01/1990";
                    }
                    else
                    {
                        if (Convert.ToDateTime(fecha) > DateTime.Now.Date)
                        {
                            MessageBox.Show("Fecha mayor a la actual");
                            DgvAccidenteLaboral.Rows[cell].Cells["DgvAccidenteLaboralColFecha"].Value = DateTime.Now.Date.ToString().Substring(0, 10);
                        }
                    }
                }
            }
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

        private void DgvEnfermedadProfesional_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvEnfermedadProfesionalColFechaDiagnostigo.Index)
            {
                if (e.RowIndex > -1)
                {
                    int cell = e.RowIndex;
                    string fecha = DgvEnfermedadProfesional.Rows[e.RowIndex].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value.ToString();
                    if (ValidarFecha(fecha) == false)
                    {
                        MessageBox.Show("Fecha incorrecta");
                        DgvEnfermedadProfesional.Rows[cell].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value = "01/01/1990";
                    }
                    else
                    {
                        if (Convert.ToDateTime(fecha) > DateTime.Now.Date)
                        {
                            MessageBox.Show("Fecha mayor a la actual");
                            DgvEnfermedadProfesional.Rows[cell].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value = DateTime.Now.Date.ToString().Substring(0, 10);
                        }
                    }
                }
            }
        }

        private void RdbCicloSi_CheckedChanged(object sender, EventArgs e)
        {
            if (RdbCicloSi.Checked)
                Txtmetodo.Enabled = true;
            else
                Txtmetodo.Enabled = false;
        }

        private void FrnHistoriaClinica_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        List<string> RevisionSistema = new List<string>();
        private void Dgvrevision_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    int fila = e.RowIndex;
                    int colum = e.ColumnIndex;
                    bool isCellChecked = Convert.ToBoolean(Dgvrevision.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                    if (isCellChecked)
                    {
                        Dgvrevision.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                        string cod = Dgvrevision.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value.ToString();
                        for (int i = 0; i < RevisionSistema.Count; i++)
                        {
                            if (RevisionSistema[i] == cod)
                            {
                                RevisionSistema.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (Dgvrevision.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value != null)
                        {
                            Dgvrevision.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                            string cod = Dgvrevision.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value.ToString();
                            string Col = Tablasistema.Rows[e.RowIndex]["Sist_Descripcion"].ToString();
                            string Col2 = Tablasistema.Rows[e.RowIndex]["Sist_Descripcion"].ToString() + e.ColumnIndex.ToString();
                            RevisionSistema.Add(cod);
                        }
                        else
                        {
                            Dgvrevision.Rows[fila].Cells[colum].Value = 0;
                            MessageBox.Show("No corresponde a ninguna celda.  \n Verificar", "");
                        }
                    }
                    Dgvrevision.ClearSelection();
                }
            }
            catch (Exception ex)
            {
            }
        }
        List<string> ListRecomendaciones = new List<string>();
        private void DgvRecomendaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    int fila = e.RowIndex;
                    int colum = e.ColumnIndex;
                    bool isCellChecked = Convert.ToBoolean(DgvRecomendaciones.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                    if (isCellChecked)
                    {
                        DgvRecomendaciones.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                        string cod = DgvRecomendaciones.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value.ToString();
                        for (int i = 0; i < ListRecomendaciones.Count; i++)
                        {
                            if (ListRecomendaciones[i] == cod)
                            {
                                ListRecomendaciones.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (DgvRecomendaciones.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value != null)
                        {
                            DgvRecomendaciones.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                            string cod = DgvRecomendaciones.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value.ToString();
                            ListRecomendaciones.Add(cod);
                        }
                        else
                        {
                            DgvRecomendaciones.Rows[fila].Cells[colum].Value = false;
                            MessageBox.Show("No corresponde a ninguna celda.  \n Verificar", "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void TxtBuscarRecomendacion_TextChanged(object sender, EventArgs e)
        {
            var x = 0;

            if (TxtBuscarRecomendacion.Text != "")
            {
                foreach (DataGridViewColumn colmn in DgvRecomendaciones.Columns)
                {
                    foreach (DataGridViewRow row in DgvRecomendaciones.Rows)
                    {
                        if (row.Cells[colmn.Name].Value != null)
                        {
                            var fila = row.Cells[colmn.Name].Value.ToString();
                            if (fila.ToUpper().Contains(TxtBuscarRecomendacion.Text.ToUpper()))
                            {
                                DgvRecomendaciones.Rows[row.Index].Cells[colmn.Index].Selected = true;
                                x++;
                                //break;
                            }
                        }
                    }
                    LblEncontrados.Text = "# " + x;

                    if (x > 0)
                    {
                        //break;
                        //x = 0;
                    }
                    else
                        DgvRecomendaciones.ClearSelection();
                }
            }
            else
            {
                DgvRecomendaciones.ClearSelection();
                x = 0;
            }

        }

    }
}