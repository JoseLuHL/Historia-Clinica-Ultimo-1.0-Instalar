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

namespace Historia_Clinica
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        ClsSqlServer ObjServer = new ClsSqlServer();
        
        //Procedimiento para cargar la descripcion y codigo a los DGV que lo requieren
        public void CargarColumnas( DataGridView dgv ,string query, string ColCode, string ColDes)
        {
            DataTable Tabla = new DataTable();
            Tabla = ObjServer.LlenarTabla(query);
            dgv.RowCount = Tabla.Rows.Count;
            for (int i = 0; i < Tabla.Rows.Count; i++)
            {
                dgv.Rows[i].Cells[ColCode].Value = Tabla.Rows[i]["Codigo"];
                dgv.Rows[i].Cells[ColDes].Value = Tabla.Rows[i]["Descripcion"];
            }
            Tabla=null;
        }
        //-----Fin Procedimiento para cargar-----

        public void tamañoDGV()
        {
            DgvInmunizacionColDosis.Width = 300;
            DgvInmunizacionColFecha.Width = 150;
            DgvInmunizacionColTipoImn.Width = 200;

            DgvAntecedenteFamiliarColEnfermedad.Width = 300;
            DgvAntecedenteFamiliarColMortalidad.Width = 100;
            DgvAntecedenteFamiliarColParentesco.Width = 240;

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
            DGV.DefaultCellStyle.Font = new Font("Verdana", 11) ;
            Font prFont = new Font("Verdana", 11, FontStyle.Bold);
            for (int i = 0; i < DGV.Columns.Count; i++)
            {
			 DGV.Columns[i].HeaderCell.Style.Font=prFont;
             DGV.Columns[i].HeaderCell.Style.ForeColor=Color.White;
             DGV.Columns[i].HeaderCell.Style.BackColor = Color.DimGray;
             DGV.Columns[i].HeaderCell.Style.Alignment=DataGridViewContentAlignment.MiddleCenter;
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
                SqlConnection conexion = new SqlConnection("Data Source=.;Initial Catalog=HistoriaClinica;Integrated Security=True");//cadena conexion
                string consulta = "SELECT [Diag_Codigo] as Codigo ,[Diag_Descripcion] as Descripcion FROM [dbo].[Diagnostico]"; //consulta a la tabla paises
                SqlCommand comando = new SqlCommand(consulta, conexion);
                SqlDataAdapter adap = new SqlDataAdapter(comando);
                adap.Fill(dt);
            }
            catch (Exception)
            {
                dt = null;
                MessageBox.Show("no se puede conectar con la Base de Datos");
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
                coleccion.Add(Convert.ToString(row["Descripcion"] ));
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
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                //ObjServer.Solo_Letras()
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                DgvAccidenteLaboral.Rows.Clear();
                DgvAntecedenteFamiliar.Rows.Clear();
                TxtElementosProte.Clear();
                DgvEnfermedadProfesional.Rows.Clear();
                DgvAccidenteLaboral.Rows[DgvAccidenteLaboral.Rows.Count - 1].Cells["DgvAccidenteLaboralColFecha"].Value = "01/01/1990";
                DgvEnfermedadProfesional.Rows[DgvEnfermedadProfesional.Rows.Count - 1].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value = "01/01/1990";
                DgvInmunizacion.Rows[DgvInmunizacion.Rows.Count - 1].Cells["DgvInmunizacionColFecha"].Value = "01/01/1990";



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
                
                //Examenes de laboratorio - Combo
                DgvExamenLaboratorioColExamenLab.DataSource = ObjServer.LlenarTabla("SELECT [TipExaLabo_Codigo] As Codigo ,[TipExaLabo_Descripcion] As Descripcion FROM [dbo].[TipoExamenLaboratorio]");
                DgvExamenLaboratorioColExamenLab.DisplayMember = "Descripcion";
                DgvExamenLaboratorioColExamenLab.ValueMember = "Codigo";
                DgvExamenLaboratorio.RowCount = 2;
                DgvExamenLaboratorio.Rows[0].Cells["DgvExamenLaboratorioColExamenLab"].Value = 1;

                //
                DgvAccidenteLaboralColParteAfectada.DataSource = ObjServer.LlenarTabla("SELECT [PartA_Codigo] As Codigo,[PartA_Descripcion] As [Descripcion] FROM [dbo].[ParteAfectada]");
                DgvAccidenteLaboralColParteAfectada.DisplayMember = "Descripcion";
                DgvAccidenteLaboralColParteAfectada.ValueMember = "Codigo";
                DgvAccidenteLaboral.Rows[0].Cells[2].Value = 1;
                //MessageBox.Show(ObjServer.x.ToString());

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
                }
                //----Fin cargar riesgos----

                //cargar las enfermedades al DgvEnfermedadProfesional
                DgvEnfermedadProfesionalColEnfermedad.DataSource = ObjServer.LlenarTabla("SELECT [Enf_Codigo] as Codigo ,[Enf_Descipcion] as Descripcion FROM [dbo].[Enfermedad]");
                DgvEnfermedadProfesionalColEnfermedad.DisplayMember = "Descripcion";
                DgvEnfermedadProfesionalColEnfermedad.ValueMember = "Codigo";
                //----Fin cargar enfermedades DgvEnfermedadProfesional

                //cargar las enfermedades al DgvInmunizacion
                DgvInmunizacion.Rows.Clear();
                DgvInmunizacionColTipoImn.DataSource = ObjServer.LlenarTabla("SELECT [TipInm_Codigo] as Codigo,[TipInm_Descripcion] as Descripcion  FROM [dbo].[TipoInmunizacion]");
                DgvInmunizacionColTipoImn.DisplayMember = "Descripcion";
                DgvInmunizacionColTipoImn.ValueMember = "Codigo";
                //----Fin cargar los tipos DgvInmunizacion

                //cargar las enfermedades al DgvAntecedenteFamiliar
                DgvAntecedenteFamiliar.Rows.Clear();
                DgvAntecedenteFamiliarColEnfermedad.DataSource = ObjServer.LlenarTabla("SELECT [Enf_Codigo] as Codigo ,[Enf_Descipcion] as Descripcion FROM [dbo].[Enfermedad]");
                DgvAntecedenteFamiliarColEnfermedad.DisplayMember = "Descripcion";
                DgvAntecedenteFamiliarColEnfermedad.ValueMember = "Codigo";
                //----Fin cargar enfermedades DgvAntecedenteFamiliar

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
                DgvHabitos.Rows.Clear();
                query = "";
                ColCod = "";
                ColDes = "";
                //Cargar Los Habitos al DGVhabitos
                query = "SELECT [Hab_Codigo] As [Codigo] ,[Hab_Descripcion] As [Descripcion] FROM [dbo].[Habito]";
                ColCod = "DgvHabitosColCodigo";
                ColDes = "DgvHabitosColHabito";
                CargarColumnas(DgvHabitos, query, ColCod, ColDes);
                //-----Fin cargar Habitos-----


                //DGVProbabilidad
                //<<<<<<Combo de probabilidad>>>>>>>>>
                DgvProbabilidadRiesgo.Rows.Clear();
                query = "";
                query = "";
                DgvProbabilidadRiesgoColProbabilidad.DataSource = ObjServer.LlenarTabla("SELECT [Prob_Codigo] As [Codigo] ,[Prob_Descripcion] As [Descripcion] FROM [dbo].[Probabilidad]");
                DgvProbabilidadRiesgoColProbabilidad.DisplayMember = "Descripcion";
                DgvProbabilidadRiesgoColProbabilidad.ValueMember = "Codigo";

                //Combo de Tipo de Riesgo
                query = "";
                query = "SELECT [TipoRiesg_Codigo] As [Codigo], [TipoRiesg_Descripcion] As [Descripcion] FROM [dbo].[TipoRiesgo]";
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
                query = "SELECT [Exam_Codigo] As [Codigo] ,[Exam_Descripcion] As [Descripcion] FROM [dbo].[Examen]";
                tabla = ObjServer.LlenarTabla(query);
                DgvExamenPacticadoColExamen.DataSource = tabla;
                DgvExamenPacticadoColExamen.DisplayMember = "Descripcion";
                DgvExamenPacticadoColExamen.ValueMember = "Codigo";

                DgvExamenPacticado.RowCount = ObjServer.LlenarTabla(query).Rows.Count;
                for (int i = 0; i < DgvExamenPacticado.Rows.Count; i++)
                {

                    DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value = tabla.Rows[i]["Codigo"];
                    DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value = "......";
                }

                //Cargar los Revisiones al DGVRevisiones
                Dgvrevision.Rows.Clear();
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
                int y = 0;
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
                //--------Fin Cargar revisiones--------

                //Asignar el nombre a las cajas de texto al contenedor del ciclo menstrual
                TxtHijos.Text = "0";
                TxtHijosSanos.Text = "0";
                TxtGestaciones.Text = "0";
                TxtPartos.Text = "0";
                TxtresultadoCitologia.Text = "Resultado de citología";
                TxtAbostos.Text = "0";
                //----Fin asugnar nombres-----

                //Cambiar el color a las cajas de texto al contenedor del ciclo menstrual
                TxtHijos.ForeColor = Color.Gray;
                TxtHijosSanos.ForeColor = Color.Gray;
                TxtGestaciones.ForeColor = Color.Gray;
                TxtPartos.ForeColor = Color.Gray;
                TxtresultadoCitologia.ForeColor = Color.Gray;
                TxtAbostos.ForeColor = Color.Gray;
                //-----Fin Cambiar color-----

                //<<<<<<<<<<<<
                TxtJornada.Text = "Jornada";
                TxtCargo.Text = "Cargo/Ocupacioón";
                TxtSesion.Text = "Sección/Departamento";
                TxtDescripcionFunciones.Text = "Descripción Funciones";
                TxtEquipo.Text = "Equipo/Maquinaria";
                TxtHerramienta.Text = "Herramienta";
                TxtMateriaPrima.Text = "Materia Prima";

                //<<<<<<<<<<<<
                Placeholder_Leave(TxtJornada, "Jornada");
                Placeholder_Leave(TxtCargo, "Cargo/Ocupacioón");
                Placeholder_Leave(TxtSesion, "Sección/Departamento");
                Placeholder_Leave(TxtDescripcionFunciones, "Descripción Funciones");
                Placeholder_Leave(TxtEquipo, "Equipo/Maquinaria");
                Placeholder_Leave(TxtHerramienta, "Herramienta");
                Placeholder_Leave(TxtMateriaPrima, "Materia Prima");
                //<<<<<<<<<<<<
                //Placeholder_Leave(TxtExamenLaboratorio, "Ingrese aquí los examenes de laboratorío");
                //<<<<<<<<<<<<

                //<<<<<<<<<<<
                //Placeholder_Leave(TxtConcepto, "Concepto");
                TxtRecomendacion.Text = "Recomendación";
                Placeholder_Leave(TxtRecomendacion, "Recomendación");
                //>>>>>>>>>>>


                TxtPresionArterial.Text="0";
                TxtFrecuenciaCardiaca.Text="0";
                TxtLateracidad.Text="Lateracidad";
                TxtPeso.Text="0";
                TxtTalla.Text="0";
                TxtPerimetroCintura.Text="0";
                TxtIMC.Text="0";
                TxtInterpretacion.Text="Interpretación";

                //<<<<<<<<<<<<<
                Placeholder_Leave(TxtPresionArterial, "0");
                Placeholder_Leave(TxtFrecuenciaCardiaca, "0");
                Placeholder_Leave(TxtLateracidad, "Lateracidad");
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                caja.ForeColor = Color.Gray;
            }
        }
        public void Placeholder_Leave(TextBox caja, string texto, EventArgs e)
        {
            if (caja.Text == "")
            {
                caja.Text = texto;
                caja.ForeColor = Color.Gray;
            }
        }
        //>>>>>>>>>>>>>>>>>>>>>>>>>
        private void TxtHijosSanos_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtHijosSanos, "0", e); }

        private void TxtHijos_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtHijos, "0", e); }

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

        //<<<<<<<<Evento Leave>>>>>>>>>>>>>
        private void TxtJornada_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtJornada, "Jornada", e); }

        private void TxtCargo_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtCargo, "Cargo/Ocupacioón", e); }

        private void TxtSesion_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtSesion, "Sección/Departamento", e); }

        private void TxtDescripcionFunciones_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtDescripcionFunciones, "Descripción Funciones", e); }

        private void TxtEquipo_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtEquipo, "Equipo/Maquinaria", e); }

        private void TxtHerramienta_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtHerramienta, "Herramienta", e); }

        private void TxtMateriaPrima_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtMateriaPrima, "Materia Prima", e); }
        //<<<<<<<<<Fin evento Leave>>>>>>>>>

        //-----Evento enter------
        private void TxtJornada_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtJornada, "Jornada", e); }

        private void TxtCargo_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtCargo, "Cargo/Ocupacioón", e); }

        private void TxtSesion_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtSesion, "Sección/Departamento", e); }

        private void TxtDescripcionFunciones_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtDescripcionFunciones, "Descripción Funciones", e); }

        private void TxtEquipo_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtEquipo, "Equipo/Maquinaria", e); }

        private void TxtHerramienta_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtHerramienta, "Herramienta", e); }

        private void TxtMateriaPrima_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtMateriaPrima, "Materia Prima", e); }
        //-------Fin evento enter-----------

        //<<<<<<<<<<<<<<<<<<<<<<<<<
        //private void TxtExamenLaboratorio_Enter(object sender, EventArgs e)
        //{ Placeholder_Enter(TxtExamenLaboratorio, "Ingrese aquí los examenes de laboratorío", e); }

        //private void TxtExamenLaboratorio_Leave(object sender, EventArgs e)
        //{ Placeholder_Leave(TxtExamenLaboratorio, "Ingrese aquí los examenes de laboratorío", e); }
        //>>>>>>>>>>>>>>>>>>>>>>>>

        //Para los controles de entrada
        private void TxtRecomendacion_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtRecomendacion, "Recomendación", e); }
        //private void TxtConcepto_Leave(object sender, EventArgs e)
        //{ Placeholder_Leave(TxtConcepto, "Concepto", e); }
        //<<<<<<<<<<<<<<<<<<<<<<<<
        //private void TxtConcepto_Enter(object sender, EventArgs e)
        //{ Placeholder_Enter(TxtConcepto, "Concepto", e); }
        private void TxtRecomendacion_Enter(object sender, EventArgs e)
        { Placeholder_Enter(TxtRecomendacion, "Recomendación", e); }
        //private void TxtReubicacion_Enter(object sender, EventArgs e)
        //{ Placeholder_Enter(TxtReubicacion, "Reubicación", e); }
        //>>>>>Fin contriles entrada>>>>>>>>

        //<<<<<<<<<<<<<<<<<<<<<<
        private void TxtPresionArterial_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtPresionArterial, "0", e); }

        private void TxtFrecuenciaCardiaca_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtFrecuenciaCardiaca, "0", e); }

        private void TxtLateracidad_Leave(object sender, EventArgs e)
        { Placeholder_Leave(TxtLateracidad, "Lateracidad", e); }

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
        { Placeholder_Enter(TxtLateracidad, "Lateracidad", e); }

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

        private void TxtDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //<<<<<<<<<<<<para preciones la tecla Enter en el control de Buscar>>>>>>>>>>>>>>
                if (e.KeyCode == Keys.Enter)
                {
                    if (TxtDocumento.Text != string.Empty)
                    {
                        //<<<<<<<<<<<<<<<<<<<<<<
                        DataTable TablaPaciente = new DataTable();
                        TablaPaciente = ObjServer.LlenarTabla("SELECT [Pac_Nombre1] + ' ' + [Pac_Nombre2]+ ' ' + [Pac_Apellido1]+ ' ' +[Pac_Apellido2] As [Nombres] FROM [dbo].[Paciente] WHERE Pac_Identificacion=" + TxtDocumento.Text);

                        if (TablaPaciente.Rows.Count > 0)
                        {
                            TxtNombre.Text = TablaPaciente.Rows[0]["Nombres"].ToString();
                            CargarHistoria(TxtDocumento.Text);
                        }
                        else
                        {
                            LblEntrada.Text = "";
                            //
                            TxtNombre.Clear();
                            CargarIinicio();
                            MessageBox.Show("No se ha encontrado el paciente");
                        }
                        //>>>>>>>>>>>>>>>>>>>>>>
                    }
                    else
                        CargarIinicio();
                }
                //<<<<<<<<<<<<Fin precionar tecla enter>>>>>>>>>>>>>>>>>>>>>>>

            }
            catch (Exception)
            {
            }
        }
        DataTable Tablasistema = new DataTable();
        private void Form2_Load(object sender, EventArgs e)
        {
            TxtElementosProte.ReadOnly = true;
            //Establecer conexión  con el Gestor.
            ObjServer.CadenaCnn = "Data Source=.;Initial Catalog=HistoriaClinica;Integrated Security=True";
            ObjServer.Conectar();
            //----Fin Establecer conexión-----
            CargarIinicio();

            // Cargo los datos que tendra el combobox Diagnostico
            CboDiagnostico.DataSource = Datos();
            CboDiagnostico.DisplayMember = "Descripcion";
            CboDiagnostico.ValueMember = "Codigo";

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
            tamañoDGV();
            TxtHijos.Text = "0";
            TxtHijosSanos.Text = "0";
        }

        private void BtnGuardarHistoria_Click(object sender, EventArgs e)
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

                if (MessageBox.Show("¿Desea guardar?", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    try
                    {
                        //Insertar en entradaHistoria
                        int reubicacion = 0;
                        if (RdbReuSi.Checked)
                            reubicacion = 1;

                        string Query = "INSERT INTO [dbo].[EntradaHistoria]([Entr_Paciente_ident],[Entr_FechaEntrada],[Entr_Diagnostico], [Entr_Concepto_Codigo], [Entr_Recomendacion], [Entr_Reubicacion],Entr_TipoExamenCodigo) " +
                                        "VALUES (" + TxtDocumento.Text + ", '" + DtFechaEntrada.Text + "' ,'" + CboDiagnostico.SelectedValue.ToString() + "'," + CboConcepto.SelectedValue + ",'" + TxtRecomendacion.Text + "'," + reubicacion + "," + CboTipoExamen.SelectedValue + ")";
                        ObjServer.CadnaSentencia = Query;
                        ObjServer.Sentencia();

                        int NumeroEntrada = Convert.ToInt32(ObjServer.LlenarTabla("SELECT  top 1 [Entr_Numero] FROM [dbo].[EntradaHistoria] order by Entr_numero desc").Rows[0][0].ToString());
                        
                        //Insertar examenes de laboratorio
                        for (int i = 0; i < DgvExamenLaboratorio.Rows.Count-1; i++)
                        {
                            if (DgvExamenLaboratorio.Rows[i].Cells["DgvExamenLaboratorioColExamenLab"].Value!=null)
                            {
                                Query = "INSERT INTO [dbo].[ExamenLaboratorio] ([ExaLabo_Entrada_Numero] ,[ExaLabo_ExamenCodigo]) VALUES ("+ NumeroEntrada + "," + DgvExamenLaboratorio.Rows[i].Cells["DgvExamenLaboratorioColExamenLab"].Value +")";
                                ObjServer.CadnaSentencia = Query;
                                ObjServer.Sentencia();
                            }
                        }
                        //      //<<<<<<<<<<<<<<<<<<<<<<<<<<<

                        //      //Insertar en AccidenteLaboral
                        for (int i = 0; i < DgvAccidenteLaboral.Rows.Count - 1; i++)
                        {
                            //Para saber si hay datos para agregar
                            Boolean confirmar = false;
                            string[] DatosAgregar = new string[DgvAccidenteLaboral.Columns.Count]; //Guarda los datos a agregar en caso que hayan
                            for (int x = 0; x < DgvAccidenteLaboral.Columns.Count; x++)
                            {
                                if (DgvAccidenteLaboral.Rows[i].Cells[x].Value != null)
                                {
                                    DatosAgregar[x] = DgvAccidenteLaboral.Rows[i].Cells[x].Value.ToString();
                                    confirmar = true;
                                }
                                else
                                { confirmar = false; break; }
                            }
                            if (confirmar == true)
                            {
                                //Posición del vector DatosAgregar 0 Empresa; 4 Fecha; 1 Naturaleza; 2 ParteAfectada; 3 DiasIncapacidad;  
                                Query = "INSERT INTO [dbo].[AccidenteLaboral] " +
                                       "([AccLab_Fecha]                 " +
                                       ",[AccLab_Entrada_Numero]        " +
                                       ",[AccLab_Empresa]               " +
                                       ",[AccLab_Naturaleza]            " +
                                       ",[AccLab_ParteAfectada_Codigo]  " +
                                       ",[AccLab_DiasIncapacidad])      " +
                                       "VALUES (" + "'" + DatosAgregar[4] + "'," + NumeroEntrada + ",'" + DatosAgregar[0] + "','" + DatosAgregar[1] + "'," + DatosAgregar[2] + "," + DatosAgregar[3] + ")";
                                //MessageBox.Show(Query);
                                ObjServer.CadnaSentencia = Query;
                                ObjServer.Sentencia();
                            }
                        }
                        ////<<<<<<<<<<<<<<<<<<<<<<<<<<<

                        //      //Insertar en DgvRiesgoOcupacional
                        for (int i = 0; i < DgvRiesgoOcupacional.Rows.Count - 1; i++)
                        {
                            //Para saber si hay datos para agregar
                            Boolean confirmar = false;
                            int riesgo = 0;
                            string empresa = "";
                            string cargo = "";
                            string meses = "";

                            if (DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColEmpresa"].Value != null || DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColCargo"].Value != null || DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColMeses"].Value != null)
                            {
                                //MessageBox.Show("No");
                                cargo = DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColCargo"].Value.ToString();
                                empresa = DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColEmpresa"].Value.ToString();
                                riesgo = Convert.ToInt32(DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiesgo"].Value);
                                meses = DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColMeses"].Value.ToString();
                                //DatosAgregar[x] = DgvRiesgoOcupacional.Rows[i].Cells[x].Value.ToString();
                                confirmar = true;
                            }
                            else
                            { confirmar = false; break; }

                            if (confirmar == true)
                            {
                                //Posición del vector DatosAgregar 0 Riesgo; 1 Empresa; 2 Cargo; 3 meses;  
                                //Insertar en Riesgo Ocupacional
                                //empresa = "";
                                Query = "INSERT INTO [dbo].[RiesgoOcupacional] " +
                                           "([RiegOcu_Riesgo_Codigo]    " +
                                           ",[RiegOcu_Entrada_Numero]   " +
                                           ",[RiegOcu_Empresa]          " +
                                           ",[RiegOcu_Cargo]     " +
                                           ",[RiegOcu_Meses])           " +
                                           "VALUES (" + "'" + riesgo + "'," + NumeroEntrada + ",'" + empresa + "','" + cargo + "'," + meses + ")";

                                //MessageBox.Show(Query);
                                ObjServer.CadnaSentencia = Query;
                                ObjServer.Sentencia();
                            }
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                        //Insertar DgvProbabilidadRiesgo
                        for (int i = 0; i < DgvProbabilidadRiesgo.Rows.Count - 1; i++)
                        {
                            //Para saber si hay datos para agregar
                            Boolean confirmar = false;
                            int TipoRiesgo = 0;
                            string Riesgo = "";
                            string probabilidad = "";
                            string estimacion = "";

                            //DgvProbabilidadRiesgoColProbabilidad;
                            if (DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColRiesgo"].Value != null || DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColEstimacion"].Value != null)
                            {
                                //MessageBox.Show("No");
                                Riesgo = DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColRiesgo"].Value.ToString();
                                estimacion = DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColEstimacion"].Value.ToString();
                                TipoRiesgo = Convert.ToInt32(DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColTipoRiesgo"].Value);
                                probabilidad = DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColProbabilidad"].Value.ToString();
                                //DatosAgregar[x] = DgvRiesgoOcupacional.Rows[i].Cells[x].Value.ToString();
                                confirmar = true;
                            }
                            else
                            { confirmar = false; break; }

                            if (confirmar == true)
                            {
                                //Insertar en Riesgo Ocupacional

                                //Insertar en Probabilidad de Riesgo
                                Query = "INSERT INTO [dbo].[ProbabilidadRiego] " +
                                      "([ProbRiesg_Riesgo_Codigo]       " +
                                      ",[ProbRiesg_Entrada_Numero]      " +
                                      ",[ProbRiesg_TipoRiesgo_Codigo]   " +
                                      ",[ProbRiesg_Probabilidad_Codigo] " +
                                      ",[ProbRiesg_Estimacion])         " +
                                          "VALUES (" + "'" + Riesgo + "'," + NumeroEntrada + ",'" + TipoRiesgo + "'," + probabilidad + ",'" + estimacion + "')";

                                //MessageBox.Show(Query);
                                ObjServer.CadnaSentencia = Query;
                                ObjServer.Sentencia();
                            }
                        }

                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                        //DgvEnfermedadProfesional
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
                                enfermedad = Convert.ToInt32(DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColEnfermedad"].Value);
                                Fecha = DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value.ToString();
                                empresa = DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColEmpresa"].Value.ToString();
                                confirmar = true;
                            }
                            else
                            { confirmar = false; break; }

                            if (confirmar == true)
                            {
                                //Insertar en Probabilidad de Riesgo
                                Query = "INSERT INTO [dbo].[EnfermedadProfesional] " +
                                            "([EnfPro_Enfermedad_Codigo]               " +
                                            ",[EnfPro_Entrada_Numero]                  " +
                                            ",[EnfPro_Empresa]                         " +
                                            ",[EnfPro_FechaDiagnostico])                " +
                                               "VALUES (" + "" + enfermedad + "," + NumeroEntrada + ",'" + empresa + "','" + Fecha + "')";
                                //MessageBox.Show(Query);
                                ObjServer.CadnaSentencia = Query;
                                ObjServer.Sentencia();
                            }
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                        //insertar en información ocupacional
                        Query = "INSERT INTO [dbo].[InformacionOcupacional] (" +
                            "[InfOcu_Entrada_Numero]        " +
                            ",[InfOcu_FechaIngreso]         " +
                            ",[InfOcu_FechaCargoActual]     " +
                            ",[InfOcu_Jornada]              " +
                            ",[InfOcu_Ocupacion]            " +
                            ",[InfOcu_Area]                 " +
                            ",[InfOcu_DescripcionFunciones] " +
                            ",[InfOcu_Maquinaria]           " +
                            ",[InfOcu_Herramienta]          " +
                            ",[InfOcu_MateriaPrima]         " +
                            ",InfOcu_ElementoProte)         " +
                            " VALUES(" + NumeroEntrada + ",'" + DtFechaIngreso.Value.ToShortDateString() + "','" + DtFecha.Value.ToShortDateString() + "','" + TxtJornada.Text + "','" + TxtCargo.Text + "','" + TxtSesion.Text + "','" + TxtDescripcionFunciones.Text + "','" + TxtEquipo.Text + "','" + TxtHerramienta.Text + "','" + TxtMateriaPrima.Text + "','" + TxtElementosProte.Text + "')";
                        //MessageBox.Show(Query);
                        ObjServer.CadnaSentencia = Query;
                        ObjServer.Sentencia();
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                        //Insertar DgvAntecedenteFamiliar
                        for (int i = 0; i < DgvAntecedenteFamiliar.Rows.Count - 1; i++)
                        {
                            //Para saber si hay datos para agregar
                            Boolean confirmar = false;
                            int enfermedad = 0;
                            string parentesco = "";
                            int mortalidad = 0;

                            //DgvAntecedenteFamiliar;
                            if (DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColParentesco"].Value != null)
                            {
                                enfermedad = Convert.ToInt32(DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColEnfermedad"].Value);
                                parentesco = DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColParentesco"].Value.ToString();
                                mortalidad = Convert.ToInt32(DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColMortalidad"].Value);

                                confirmar = true;
                            }
                            else
                            { confirmar = false; break; }

                            if (confirmar == true)
                            {
                                ////Insertar en DgvAntecedenteFamiliar

                                Query = "INSERT INTO [dbo].[AntecednteFamiliar] " +
                                           "([AntFam_Enfermedad_Codigo] " +
                                           ",[AntFam_Entrada_Numero]    " +
                                           ",[AntFam_Parentesco]        " +
                                           ",[AntFam_Mortalidad])       " +
                                           "VALUES (" + "" + enfermedad + "," + NumeroEntrada + ",'" + parentesco + "','" + mortalidad + "')";
                                //MessageBox.Show(Query);
                                ObjServer.CadnaSentencia = Query;
                                ObjServer.Sentencia();
                            }
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


                        //Insertar DgvAntecedentePersonal
                        for (int i = 0; i < DgvAntecedentePersonal.Rows.Count - 1; i++)
                        {
                            //Para saber si hay datos para agregar
                            Boolean confirmar = false;
                            int antecedente = 0;
                            string diagnostico = "";
                            string observacion = "";
                            if (DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColDiagnostico"].Value != null)
                            {
                                antecedente = Convert.ToInt32(DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColCodigo"].Value);
                                diagnostico = DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColDiagnostico"].Value.ToString();
                                observacion = DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColObservacion"].Value.ToString();
                                confirmar = true;
                            }
                            else
                            { confirmar = false; break; }

                            if (confirmar == true)
                            {
                                ////Insertar en DgvAntecedentePersonal
                                Query = "INSERT INTO [dbo].[AntecedentePersonal] " +
                                           "([AntPer_Antecedende_Codigo] " +
                                           ",[AntPer_Entrada_Numero]     " +
                                           ",[AntPer_Diagnostico]        " +
                                           ",[AntPer_Observacion])       " +
                                           "VALUES (" + "" + antecedente + "," + NumeroEntrada + ",'" + diagnostico + "','" + observacion + "')";

                                //MessageBox.Show(Query);
                                ObjServer.CadnaSentencia = Query;
                                ObjServer.Sentencia();
                            }
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                        //insertar en Ciclo Menstrual
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
                                   ",[CicMens_Planificacion]) " +
                                     " VALUES(" + NumeroEntrada + ",'" + "01/01/2017" + "'," +
                                            TxtHijosSanos.Text + ",'" + TxtGestaciones.Text + "','" +
                                            TxtPartos.Text + "','" + TxtAbostos.Text + "','" + TxtHijos.Text + "','" +
                                            TxtresultadoCitologia.Text + "','" + planificacion + "')";
                        //MessageBox.Show(Query);
                        ObjServer.CadnaSentencia = Query;
                        ObjServer.Sentencia();
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                        //Insertar DgvHabitos
                        for (int i = 0; i < DgvHabitos.Rows.Count - 1; i++)
                        {
                            //Para saber si hay datos para agregar
                            Boolean confirmar = false;
                            int habito = 0;
                            string caracteristica = "";
                            string frecuencia = "";
                            int tiempo = 0;
                            string observacion = "";
                            if (DgvHabitos.Rows[i].Cells["DgvHabitosColTiempoConsumo"].Value != null || DgvHabitos.Rows[i].Cells["DgvHabitosColCaracteristica"].Value != null || DgvHabitos.Rows[i].Cells["DgvHabitosColTiempoConsumo"].Value != null)
                            {
                                habito = Convert.ToInt32(DgvHabitos.Rows[i].Cells["DgvHabitosColCodigo"].Value);
                                caracteristica = DgvHabitos.Rows[i].Cells["DgvHabitosColCaracteristica"].Value.ToString();
                                frecuencia = DgvHabitos.Rows[i].Cells["DgvHabitosColFrecuencia"].Value.ToString();
                                tiempo = Convert.ToInt32(DgvHabitos.Rows[i].Cells["DgvHabitosColTiempoConsumo"].Value);
                                observacion = DgvHabitos.Rows[i].Cells["DgvHabitosColObservacion"].Value.ToString();

                                confirmar = true;
                            }
                            else
                            { confirmar = false; break; }

                            if (confirmar == true)
                            {
                                ////Insertar en DgvAntecedentePersonal
                                Query = "INSERT INTO [dbo].[HabitoPaciente] " +
                                          "([HabPac_Habito_Codigo]  " +
                                          ",[HabPac_Entrada_Numero] " +
                                          ",[HabPac_Caracteristica] " +
                                          ",[HabPac_Frecuencia]     " +
                                          ",[HabPac_Tiempo]         " +
                                          ",[HabPac_Observacion])   " +
                                            "VALUES (" + "" + habito + "," + NumeroEntrada + ",'" + caracteristica + "','" + frecuencia + "','" + tiempo + "','" + observacion + "')";

                                //MessageBox.Show(Query);
                                ObjServer.CadnaSentencia = Query;
                                ObjServer.Sentencia();
                            }
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

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
                                Dosis = DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColDosis"].Value.ToString();
                                fecha = DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColFecha"].Value.ToString();

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

                                //MessageBox.Show(Query);
                                ObjServer.CadnaSentencia = Query;
                                ObjServer.Sentencia();
                            }
                        }
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


                        //Insertar en Dgvrevision
                        for (int i = 0; i < Dgvrevision.Columns.Count; i++)
                        {
                            for (int x = 0; x < Tablasistema.Rows.Count; x++)
                            {
                                string Col = Tablasistema.Rows[x]["Sist_Descripcion"].ToString();
                                string Col2 = Tablasistema.Rows[x]["Sist_Descripcion"].ToString() + x.ToString();

                                if (Dgvrevision.Columns[i].Name == Col2)
                                {
                                    //MessageBox.Show(Col2);
                                    for (int z = 0; z < Dgvrevision.Rows.Count; z++)
                                    {
                                        if (Dgvrevision.Rows[z].Cells[Col2].Value != null)
                                        {
                                            if (Convert.ToBoolean(Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Value) == true)
                                            {
                                                int CODIGO = Convert.ToInt32(Dgvrevision.Rows[z].Cells[Col2].Value);
                                                ////Insertar en Dgvrevision
                                                Query = "INSERT INTO [dbo].[RevisionSistema] " +
                                                           "([RevSist_Entrada_Numero]   " +
                                                           ",[RevSist_Revision_Codigo]) " +
                                                               "VALUES (" + "" + NumeroEntrada + "," + CODIGO + ")";

                                                //MessageBox.Show(Query);
                                                ObjServer.CadnaSentencia = Query;
                                                ObjServer.Sentencia();
                                                Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Value = false;
                                                //Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Style.BackColor = Color.Red;
                                            }
                                        }
                                        else
                                            break;
                                    }
                                }
                            }

                        }
                        //<<<<<<<<<<<<<<<<<<

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
                        //MessageBox.Show(Query);
                        ObjServer.CadnaSentencia = Query;
                        ObjServer.Sentencia();
                        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("La operación no puedo completarse debido a: \n 1 - No dispone de una conexión  \n 2 - El registro esta duplicado  \n 3 - La información ingrasado no corresponde a la requerida  \n Vuelva a intentarlo!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            } //Fin IF de verificar campos 
            else
                MessageBox.Show("Hay campos que son requeridos, completar para continuar");
        }
        //>>>>>>>>>FIN METODO GUARDAR>>>>>>>>>>>>>>

        public void CargarHistoria(string identificacion)
        {
            DataTable tabla = new DataTable(); 
            tabla = ObjServer.LlenarTabla("SELECT  top 1 [Entr_Numero] FROM [dbo].[EntradaHistoria] WHERE [Entr_Paciente_ident] ="+identificacion+" order by Entr_numero desc");

            if (tabla.Rows.Count>0)
            {
                LblEntrada.Text = tabla.Rows[0]["Entr_Numero"].ToString();
                //Cargar los datos al DgvAccidenteLaboral
                DataTable Datos = new DataTable();
                int NumeroEntrada = Convert.ToInt32(tabla.Rows[0][0].ToString());
                Datos = ObjServer.LlenarTabla("SELECT  dbo.AccidenteLaboral.AccLab_Empresa,            " +
                                                "dbo.AccidenteLaboral.AccLab_Naturaleza,            " +
                                                "dbo.ParteAfectada.PartA_Descripcion,               " +
                                                "dbo.AccidenteLaboral.AccLab_DiasIncapacidad,       " +
                                                "dbo.AccidenteLaboral.AccLab_Fecha,                 " +
                                                "dbo.AccidenteLaboral.AccLab_Entrada_Numero,        " +
                                                "dbo.AccidenteLaboral.AccLab_ParteAfectada_Codigo   " +
                                                "FROM dbo.AccidenteLaboral INNER JOIN               " +
                                                "dbo.ParteAfectada ON                               " +
                                                "dbo.AccidenteLaboral.AccLab_ParteAfectada_Codigo = " +
                                                "dbo.ParteAfectada.PartA_Codigo WHERE dbo.AccidenteLaboral.AccLab_Entrada_Numero="+NumeroEntrada);
                
                if (Datos.Rows.Count>0)
                {
                    DgvAccidenteLaboral.RowCount = Datos.Rows.Count+1;
                    for (int i = 0; i < Datos.Rows.Count; i++)
                    {
                        DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColEmpresa"].Value = Datos.Rows[i]["AccLab_Empresa"].ToString();
                        DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralNaturaleza"].Value = Datos.Rows[i]["AccLab_Naturaleza"].ToString();
                        DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColParteAfectada"].Value = Datos.Rows[i]["AccLab_ParteAfectada_Codigo"];
                        DgvAccidenteLaboral.Rows[i].Cells["DgvAccidenteLaboralColDias"].Value = Datos.Rows[i]["AccLab_DiasIncapacidad"].ToString();
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
                                               "dbo.EntradaHistoria.Entr_Numero                  " +
                                               "FROM dbo.RiesgoOcupacional INNER JOIN            " +
                                               "dbo.EntradaHistoria                              " +
                                               "ON dbo.RiesgoOcupacional.RiegOcu_Entrada_Numero =" +
                                               "dbo.EntradaHistoria.Entr_Numero WHERE dbo.EntradaHistoria.Entr_Numero= " + NumeroEntrada);
                if (Datos.Rows.Count>0)
                {                 
                DgvRiesgoOcupacional.Rows.Clear();
                DgvRiesgoOcupacional.RowCount = Datos.Rows.Count+1;
                for (int i = 0; i < Datos.Rows.Count; i++)
                {
                    DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiesgo"].Value = Datos.Rows[i]["RiegOcu_Riesgo_Codigo"];
                    DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColEmpresa"].Value = Datos.Rows[i]["RiegOcu_Empresa"];
                    DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColCargo"].Value = Datos.Rows[i]["RiegOcu_Cargo"];
                    DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColMeses"].Value = Datos.Rows[i]["RiegOcu_Meses"];
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
             if (Datos.Rows.Count>0)
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
                     //DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColFecha"].Value = Datos.Rows[i]["ProbRiesg_Probabilidad_Codigo"];
                     string fecha = Convert.ToDateTime(Datos.Rows[i]["EnfPro_FechaDiagnostico"]).ToString("dd/MM/yyyy");
                     DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value = fecha;
                         //Datos.Rows[i]["EnfPro_FechaDiagnostico"].ToString();
                     

                 }
             }
                //<<<<<<<<<<<<<<<<<<<<<<<<
                //CARGAR LOS DATOS EN ANTECEDENTES FAMILIARES.
             Datos = new DataTable();
             Datos = ObjServer.LlenarTabla("SELECT dbo.AntecednteFamiliar.AntFam_Numero,                     "+
                                                "dbo.AntecednteFamiliar.AntFam_Enfermedad_Codigo,            "+
			                                    "dbo.AntecednteFamiliar.AntFam_Entrada_Numero,               "+
                                                "dbo.AntecednteFamiliar.AntFam_Parentesco,                   "+
                                                "dbo.AntecednteFamiliar.AntFam_Mortalidad,                   "+
                                                "dbo.Enfermedad.Enf_Descipcion                               "+
			                                    "FROM   dbo.AntecednteFamiliar INNER JOIN  dbo.Enfermedad ON "+
			                                    "dbo.AntecednteFamiliar.AntFam_Enfermedad_Codigo =           "+
                                                "dbo.Enfermedad.Enf_Codigo WHERE AntFam_Entrada_Numero=" + NumeroEntrada);
             if (Datos.Rows.Count > 0)
             {
                 DgvAntecedenteFamiliar.RowCount = Datos.Rows.Count + 1;
                 for (int i = 0; i < Datos.Rows.Count; i++)
                 {
                     DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColEnfermedad"].Value = Datos.Rows[i]["AntFam_Enfermedad_Codigo"];
                     DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColParentesco"].Value = Datos.Rows[i]["AntFam_Parentesco"];
                    int mortalidad= 0;
                     if (Convert.ToBoolean( Datos.Rows[i]["AntFam_Mortalidad"])==true)
	                        {
		                        mortalidad=1;
	                        }
                     DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColMortalidad"].Value = mortalidad;
                 }
             }
                //<<<<<<<<<<<<<<<<<<<<

             //CARGAR LOS DATOS EN ANTECEDENTES PERSONALES.
             Datos = new DataTable();
             Datos = ObjServer.LlenarTabla("SELECT dbo.AntecedentePersonal.AntPer_Numero,                       "+ 
                                                    "dbo.AntecedentePersonal.AntPer_Antecedende_Codigo,         "+
			                                        "dbo.AntecedentePersonal.AntPer_Entrada_Numero,             "+
                                                    "dbo.AntecedentePersonal.AntPer_Diagnostico,                "+
                                                    "dbo.AntecedentePersonal.AntPer_Observacion,                "+
                                                    "dbo.Antecedente.Ant_Descripcion                            "+
			                                        "FROM dbo.AntecedentePersonal INNER JOIN dbo.Antecedente ON "+
			                                        "dbo.AntecedentePersonal.AntPer_Antecedende_Codigo =        "+
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
             Datos = ObjServer.LlenarTabla("SELECT dbo.Inmunizar.Inmu_Entrada_Numero,               "+
                                                "dbo.Inmunizar.Inmu_TipoInmunizacion_Codigo,        "+
			                                    "dbo.Inmunizar.Inmu_Fecha, dbo.Inmunizar.Inmu_Dosis,"+
                                                "dbo.TipoInmunizacion.TipInm_Descripcion            "+
			                                    "FROM dbo.Inmunizar INNER JOIN dbo.TipoInmunizacion "+
                                                "ON dbo.Inmunizar.Inmu_TipoInmunizacion_Codigo      "+
                                                "= dbo.TipoInmunizacion.TipInm_Codigo WHERE  dbo.Inmunizar.Inmu_Entrada_Numero=" + NumeroEntrada);
             if (Datos.Rows.Count > 0)
             {
                 DgvInmunizacion.RowCount = Datos.Rows.Count+1;
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
             Datos = ObjServer.LlenarTabla("SELECT dbo.HabitoPaciente.HabPac_Habito_Codigo,            "+
                                                "dbo.HabitoPaciente.HabPac_Entrada_Numero,             "+
			                                    "dbo.HabitoPaciente.HabPac_Caracteristica,             "+
                                                "dbo.HabitoPaciente.HabPac_Frecuencia,                 "+
                                                "dbo.HabitoPaciente.HabPac_Tiempo,                     "+
                                                "dbo.HabitoPaciente.HabPac_Observacion,                "+
                                                "dbo.Habito.Hab_Descripcion FROM dbo.Habito INNER JOIN "+
                                                "dbo.HabitoPaciente ON dbo.Habito.Hab_Codigo =         "+
                                                "dbo.HabitoPaciente.HabPac_Habito_Codigo WHERE dbo.HabitoPaciente.HabPac_Entrada_Numero=" + NumeroEntrada);
             if (Datos.Rows.Count > 0)
             {
                 for (int i = 0; i < Datos.Rows.Count; i++)
                 {
                     if (DgvHabitos.Rows[i].Cells["DgvHabitosColCodigo"].Value.ToString()==Datos.Rows[i]["HabPac_Habito_Codigo"].ToString())
                     {                         
                     DgvHabitos.Rows[i].Cells["DgvHabitosColCodigo"].Value = Datos.Rows[i]["HabPac_Habito_Codigo"];
                     DgvHabitos.Rows[i].Cells["DgvHabitosColHabito"].Value = Datos.Rows[i]["Hab_Descripcion"];
                     DgvHabitos.Rows[i].Cells["DgvHabitosColFrecuencia"].Value = Datos.Rows[i]["HabPac_Frecuencia"];
                     DgvHabitos.Rows[i].Cells["DgvHabitosColCaracteristica"].Value = Datos.Rows[i]["HabPac_Caracteristica"];
                     DgvHabitos.Rows[i].Cells["DgvHabitosColObservacion"].Value = Datos.Rows[i]["HabPac_Observacion"];
                     DgvHabitos.Rows[i].Cells["DgvHabitosColTiempoConsumo"].Value = Datos.Rows[i]["HabPac_Tiempo"];
                     }
                 }
             }
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

             //CARGAR en Dgvrevision
             Datos = new DataTable();
             Datos = ObjServer.LlenarTabla("SELECT dbo.Sistema.Sist_Descripcion,                        "+
                                                "dbo.Sistema.Sist_Codigo,                               "+
                                                "dbo.Revision.Revi_Descripcion,                         "+
			                                    "dbo.Revision.Revi_Codigo,                              "+
                                                "dbo.RevisionSistema.RevSist_Entrada_Numero             "+
			                                    "FROM dbo.Revision INNER JOIN                           "+
                                                "dbo.RevisionSistema ON dbo.Revision.Revi_Codigo =      "+
			                                    "dbo.RevisionSistema.RevSist_Revision_Codigo INNER JOIN "+
			                                    "dbo.Sistema ON dbo.Revision.Revi_Sistema_Codigo =      "+
                                                "dbo.Sistema.Sist_Codigo                                "+
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
                                 for (int y = 0; y  < Datos.Rows.Count; y ++)
                                 {
                                     if (Dgvrevision.Rows[z].Cells[Col2].Value.ToString()==Datos.Rows[y]["Revi_Codigo"].ToString())
                                     {
                                         Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Value = true;
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


                //Llena la información  de información laboral
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT [InfOcu_Numero]           "+
                                                ",[InfOcu_Entrada_Numero]       "+
                                                ",[InfOcu_FechaIngreso]         "+
                                                ",[InfOcu_FechaCargoActual]     "+
                                                ",[InfOcu_Jornada]              "+
                                                ",[InfOcu_Ocupacion]            "+
                                                ",[InfOcu_Area]                 "+
                                                ",[InfOcu_DescripcionFunciones] "+
                                                ",[InfOcu_Maquinaria]           "+
                                                ",[InfOcu_Herramienta]          "+
                                                ",[InfOcu_MateriaPrima]         "+
                                                ",InfOcu_ElementoProte          "+
                                                " FROM [dbo].[InformacionOcupacional] WHERE InfOcu_Entrada_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count>0)
                {                 
                DtFechaIngreso.Value = Convert.ToDateTime(Datos.Rows[0]["InfOcu_FechaIngreso"].ToString());
                TxtJornada.Text = Datos.Rows[0]["InfOcu_Jornada"].ToString();
                TxtCargo.Text = Datos.Rows[0]["InfOcu_Ocupacion"].ToString();
                TxtSesion.Text = Datos.Rows[0]["InfOcu_Area"].ToString();
                TxtDescripcionFunciones.Text = Datos.Rows[0]["InfOcu_DescripcionFunciones"].ToString();
                TxtEquipo.Text = Datos.Rows[0]["InfOcu_Maquinaria"].ToString();
                TxtHerramienta.Text = Datos.Rows[0]["InfOcu_Herramienta"].ToString();
                TxtMateriaPrima.Text=Datos.Rows[0]["InfOcu_MateriaPrima"].ToString();                
                string[] ElementosPro = Datos.Rows[0]["InfOcu_ElementoProte"].ToString().Split('-');
                //MessageBox.Show(ElementosPro.Length.ToString());
                for (int i = 0; i < ElementosPro.Length; i++)
                {
                    //MessageBox.Show(ElementosPro[i]);
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
                              
                }
                //<<<<<<<<<<<<<<<<<<
                
                //Ciclo mentrual
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT [CicMens_Entrada_Numero] "+
                                                ",[CicMens_FechaUltimaRegla]   "+
                                                ",[CicMens_HijosSanos]         "+
                                                ",[CicMens_Gestaciones]        "+
                                                ",[CicMens_Partos]             "+
                                                ",[CicMens_Abortos]            "+
                                                ",[CicMens_Hijos]              "+
                                                ",[CicMens_ResultadoCitologia] "+
                                                ",[CicMens_Planificacion]      "+
                                                "  FROM [dbo].[CicloMenstrual] WHERE CicMens_Entrada_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    TxtHijos.Text = Datos.Rows[0]["CicMens_Hijos"].ToString();
                    TxtHijosSanos.Text = Datos.Rows[0]["CicMens_HijosSanos"].ToString();
                    TxtGestaciones.Text = Datos.Rows[0]["CicMens_Gestaciones"].ToString();
                    TxtPartos.Text = Datos.Rows[0]["CicMens_Partos"].ToString();
                    TxtAbostos.Text = Datos.Rows[0]["CicMens_Abortos"].ToString();
                    TxtresultadoCitologia.Text = Datos.Rows[0]["CicMens_ResultadoCitologia"].ToString();                   
                    if (Datos.Rows[0]["CicMens_Planificacion"].ToString() == "False")
                        RdbCicloNo.Checked = true;
                    else
                        RdbCicloSi.Checked = true;

                    //MessageBox.Show(Datos.Rows[0]["CicMens_Planificacion"].ToString());
                }

                //Examen Fisico
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT [ExaFisi_Entrada_Numero] "+
                                                ",[ExaFisi_PresionArterial]    "+
                                                ",[ExaFisi_FrecuenciaCardiaca] "+
                                                ",[ExaFisi_Lateracidad]        "+
                                                ",[ExaFisi_Peso]               "+
                                                ",[ExaFisi_Talla]              "+
                                                ",[ExaFisi_PerimetroCintura]   "+
                                                ",[ExaFisi_IMC]                "+
                                                ",[ExaFisi_Interpretacion]     "+
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
                    //DtFechaIngreso.Value ; 
                }

                //CARGAR DATOS DE ENTRADAHISTORIA
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT [Entr_Numero] "+
                                              ",[Entr_Paciente_ident]  "+
                                              ",[Entr_FechaEntrada]    "+
                                              ",[Entr_Diagnostico]     "+
                                              ",[Entr_Concepto_Codigo] "+
                                              ",[Entr_Recomendacion]   "+
                                              ",[Entr_Reubicacion]     "+
                                              ",Entr_TipoExamenCodigo  "+
                                              " FROM [dbo].[EntradaHistoria] WHERE Entr_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    TxtRecomendacion.Text = Datos.Rows[0]["Entr_Recomendacion"].ToString();
                    CboConcepto.SelectedValue = Datos.Rows[0]["Entr_Concepto_Codigo"];
                    DtFechaEntrada.Value = Convert.ToDateTime( Datos.Rows[0]["Entr_FechaEntrada"]);
                    CboDiagnostico.SelectedValue = Datos.Rows[0]["Entr_Diagnostico"];
                    CboTipoExamen.SelectedValue = Datos.Rows[0]["Entr_TipoExamenCodigo"];
                    if (Datos.Rows[0]["Entr_Reubicacion"].ToString() == "True")
                        RdbReuSi.Checked = true;
                    else
                        RdbReuNo.Checked = true;                      
                }
                else
                {
                    //DtFechaIngreso.Value ; 
                }
                
            } 
            tabla = null;
        }
        public void ActualizarDatos(int NumeroEntrada)
        {
            //Insertar en entradaHistoria
            int reubicacion = 0;
                        if (RdbReuSi.Checked)
                            reubicacion = 1;
                        string Query = "UPDATE [dbo].[EntradaHistoria]  SET [Entr_FechaEntrada] =" + "'" + DtFechaEntrada.Text + "', [Entr_Diagnostico] = '" + CboDiagnostico.SelectedValue + "', [Entr_Concepto_Codigo] =" + CboConcepto.SelectedValue + ", [Entr_Recomendacion] = '" + TxtRecomendacion.Text + "', [Entr_Reubicacion] = " + reubicacion + " , [Entr_TipoExamenCodigo] = " + CboTipoExamen.SelectedValue;
                        ObjServer.CadnaSentencia = Query;
                        ObjServer.Sentencia();
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<

            //Insertar examenes de laboratorio
                        for (int i = 0; i < DgvExamenLaboratorio.Rows.Count - 1; i++)
                        {
                            if (DgvExamenLaboratorio.Rows[i].Cells["DgvExamenLaboratorioColExamenLab"].Value != null)
                            {
                                Query = "INSERT INTO [dbo].[ExamenLaboratorio] ([ExaLabo_Entrada_Numero] ,[ExaLabo_ExamenCodigo]) VALUES (" + NumeroEntrada + "," + DgvExamenLaboratorio.Rows[i].Cells["DgvExamenLaboratorioColExamenLab"].Value + ")";
                                ObjServer.CadnaSentencia = Query;
                                ObjServer.Sentencia();
                            }
                        }
             //<<<<<<<<<<<<<<<<<<<<<<<<<<<
            
            //Insertar en AccidenteLaboral
            for (int i = 0; i < DgvAccidenteLaboral.Rows.Count - 1; i++)
            {
                //Para saber si hay datos para agregar
                Boolean confirmar = false;
                string[] DatosAgregar = new string[DgvAccidenteLaboral.Columns.Count]; //Guarda los datos a agregar en caso que hayan
                for (int x = 0; x < DgvAccidenteLaboral.Columns.Count; x++)
                {
                    if (DgvAccidenteLaboral.Rows[i].Cells[x].Value != null)
                    {
                        DatosAgregar[x] = DgvAccidenteLaboral.Rows[i].Cells[x].Value.ToString();
                        confirmar = true;
                    }
                    else
                    { confirmar = false; break; }
                }
                if (confirmar == true)
                {
                    //Posición del vector DatosAgregar 0 Empresa; 4 Fecha; 1 Naturaleza; 2 ParteAfectada; 3 DiasIncapacidad;  
                    Query = "INSERT INTO [dbo].[AccidenteLaboral] " +
                           "([AccLab_Fecha]                 " +
                           ",[AccLab_Entrada_Numero]        " +
                           ",[AccLab_Empresa]               " +
                           ",[AccLab_Naturaleza]            " +
                           ",[AccLab_ParteAfectada_Codigo]  " +
                           ",[AccLab_DiasIncapacidad])      " +
                           "VALUES (" + "'" + DatosAgregar[4] + "'," + NumeroEntrada + ",'" + DatosAgregar[0] + "','" + DatosAgregar[1] + "'," + DatosAgregar[2] + "," + DatosAgregar[3] + ")";
                    //MessageBox.Show(Query);
                    ObjServer.CadnaSentencia = Query;
                    ObjServer.Sentencia();
                }
            }
            ////<<<<<<<<<<<<<<<<<<<<<<<<<<<

            //      //Insertar en DgvRiesgoOcupacional
            for (int i = 0; i < DgvRiesgoOcupacional.Rows.Count - 1; i++)
            {
                //Para saber si hay datos para agregar
                Boolean confirmar = false;
                int riesgo = 0;
                string empresa = "";
                string cargo = "";
                string meses = "";

                if (DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColEmpresa"].Value != null || DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColCargo"].Value != null || DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColMeses"].Value != null)
                {
                    //MessageBox.Show("No");
                    cargo = DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColCargo"].Value.ToString();
                    empresa = DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColEmpresa"].Value.ToString();
                    riesgo = Convert.ToInt32(DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColRiesgo"].Value);
                    meses = DgvRiesgoOcupacional.Rows[i].Cells["DgvRiesgoOcupacionalColMeses"].Value.ToString();
                    //DatosAgregar[x] = DgvRiesgoOcupacional.Rows[i].Cells[x].Value.ToString();
                    confirmar = true;
                }
                else
                { confirmar = false; break; }

                if (confirmar == true)
                {
                    //Insertar en Riesgo Ocupacional
                    Query = "INSERT INTO [dbo].[RiesgoOcupacional] " +
                               "([RiegOcu_Riesgo_Codigo]    " +
                               ",[RiegOcu_Entrada_Numero]   " +
                               ",[RiegOcu_Empresa]          " +
                               ",[RiegOcu_Cargo]     " +
                               ",[RiegOcu_Meses])           " +
                               "VALUES (" + "'" + riesgo + "'," + NumeroEntrada + ",'" + empresa + "','" + cargo + "'," + meses + ")";

                    //MessageBox.Show(Query);
                    ObjServer.CadnaSentencia = Query;
                    ObjServer.Sentencia();
                }
            }
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            //Insertar DgvProbabilidadRiesgo
            for (int i = 0; i < DgvProbabilidadRiesgo.Rows.Count - 1; i++)
            {
                //Para saber si hay datos para agregar
                Boolean confirmar = false;
                int TipoRiesgo = 0;
                string Riesgo = "";
                string probabilidad = "";
                string estimacion = "";

                //DgvProbabilidadRiesgoColProbabilidad;
                if (DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColRiesgo"].Value != null || DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColEstimacion"].Value != null)
                {
                    Riesgo = DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColRiesgo"].Value.ToString();
                    estimacion = DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColEstimacion"].Value.ToString();
                    TipoRiesgo = Convert.ToInt32(DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColTipoRiesgo"].Value);
                    probabilidad = DgvProbabilidadRiesgo.Rows[i].Cells["DgvProbabilidadRiesgoColProbabilidad"].Value.ToString();
                    confirmar = true;
                }
                else
                { confirmar = false; break; }

                if (confirmar == true)
                {
                    //Insertar en Riesgo Ocupacional

                    //Insertar en Probabilidad de Riesgo
                    Query = "INSERT INTO [dbo].[ProbabilidadRiego] " +
                          "([ProbRiesg_Riesgo_Codigo]       " +
                          ",[ProbRiesg_Entrada_Numero]      " +
                          ",[ProbRiesg_TipoRiesgo_Codigo]   " +
                          ",[ProbRiesg_Probabilidad_Codigo] " +
                          ",[ProbRiesg_Estimacion])         " +
                              "VALUES (" + "'" + Riesgo + "'," + NumeroEntrada + ",'" + TipoRiesgo + "'," + probabilidad + ",'" + estimacion + "')";

                    ObjServer.CadnaSentencia = Query;
                    ObjServer.Sentencia();
                }
            }

            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            //DgvEnfermedadProfesional
            //Insertar DgvEnfermedadProfesional
            for (int i = 0; i < DgvEnfermedadProfesional.Rows.Count - 1; i++)
            {
                //Para saber si hay datos para agregar
                Boolean confirmar = false;
                int enfermedad = 0;
                string empresa = "";
                string Fecha = "";

                if (DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColEmpresa"].Value != null || DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColFecha"].Value != null)
                {
                    enfermedad = Convert.ToInt32(DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColEnfermedad"].Value);
                    Fecha = DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value.ToString();
                    empresa = DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColEmpresa"].Value.ToString();
                    confirmar = true;
                }
                else
                { confirmar = false; break; }

                if (confirmar == true)
                {
                    //Insertar en Probabilidad de Riesgo
                    Query = "INSERT INTO [dbo].[EnfermedadProfesional] " +
                                "([EnfPro_Enfermedad_Codigo]               " +
                                ",[EnfPro_Entrada_Numero]                  " +
                                ",[EnfPro_Empresa]                         " +
                                ",[EnfPro_FechaDiagnostico])                " +
                                   "VALUES (" + "" + enfermedad + "," + NumeroEntrada + ",'" + empresa + "','" + Fecha + "')";
                    ObjServer.CadnaSentencia = Query;
                    ObjServer.Sentencia();
                }
            }
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            //insertar en información ocupacional
                    Query = "INSERT INTO [dbo].[InformacionOcupacional] (" +
                   "[InfOcu_Entrada_Numero]        " +
                   ",[InfOcu_FechaIngreso]         " +
                   ",[InfOcu_FechaCargoActual]     " +
                   ",[InfOcu_Jornada]              " +
                   ",[InfOcu_Ocupacion]            " +
                   ",[InfOcu_Area]                 " +
                   ",[InfOcu_DescripcionFunciones] " +
                   ",[InfOcu_Maquinaria]           " +
                   ",[InfOcu_Herramienta]          " +
                   ",[InfOcu_MateriaPrima]         " +
                   ",InfOcu_ElementoProte)         " +
                   " VALUES(" + NumeroEntrada + ",'" + DtFechaIngreso.Value.ToShortDateString() + "','" + DtFecha.Value.ToShortDateString() + "','" + TxtJornada.Text + "','" + TxtCargo.Text + "','" + TxtSesion.Text + "','" + TxtDescripcionFunciones.Text + "','" + TxtEquipo.Text + "','" + TxtHerramienta.Text + "','" + TxtMateriaPrima.Text + "','" + TxtElementosProte.Text + "')";

            //MessageBox.Show(Query);
            ObjServer.CadnaSentencia = Query;
            ObjServer.Sentencia();
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            //Insertar DgvAntecedenteFamiliar
            for (int i = 0; i < DgvAntecedenteFamiliar.Rows.Count - 1; i++)
            {
                //Para saber si hay datos para agregar
                Boolean confirmar = false;
                int enfermedad = 0;
                string parentesco = "";
                int mortalidad = 0;

                //DgvAntecedenteFamiliar;
                if (DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColParentesco"].Value != null)
                {
                    enfermedad = Convert.ToInt32(DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColEnfermedad"].Value);
                    parentesco = DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColParentesco"].Value.ToString();
                    if (Convert.ToBoolean(DgvAntecedenteFamiliar.Rows[i].Cells["DgvAntecedenteFamiliarColMortalidad"].Value)==true)
                        mortalidad = 1;
                    else
                        mortalidad = 0;

                    confirmar = true;
                }
                else
                { confirmar = false; break; }

                if (confirmar == true)
                {
                    ////Insertar en DgvAntecedenteFamiliar

                    Query = "INSERT INTO [dbo].[AntecednteFamiliar] " +
                               "([AntFam_Enfermedad_Codigo] " +
                               ",[AntFam_Entrada_Numero]    " +
                               ",[AntFam_Parentesco]        " +
                               ",[AntFam_Mortalidad])       " +
                               "VALUES (" + "" + enfermedad + "," + NumeroEntrada + ",'" + parentesco + "','" + mortalidad + "')";
                    //MessageBox.Show(Query);
                    ObjServer.CadnaSentencia = Query;
                    ObjServer.Sentencia();
                }
            }
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


            //Insertar DgvAntecedentePersonal
            for (int i = 0; i < DgvAntecedentePersonal.Rows.Count - 1; i++)
            {
                //Para saber si hay datos para agregar
                Boolean confirmar = false;
                int antecedente = 0;
                string diagnostico = "";
                string observacion = "";
                if (DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColDiagnostico"].Value != null)
                {
                    antecedente = Convert.ToInt32(DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColCodigo"].Value);
                    diagnostico = DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColDiagnostico"].Value.ToString();
                    observacion = DgvAntecedentePersonal.Rows[i].Cells["DgvAntecedentePersonalColObservacion"].Value.ToString();
                    confirmar = true;
                }
                else
                { confirmar = false; break; }

                if (confirmar == true)
                {
                    ////Insertar en DgvAntecedentePersonal
                    Query = "INSERT INTO [dbo].[AntecedentePersonal] " +
                               "([AntPer_Antecedende_Codigo] " +
                               ",[AntPer_Entrada_Numero]     " +
                               ",[AntPer_Diagnostico]        " +
                               ",[AntPer_Observacion])       " +
                               "VALUES (" + "" + antecedente + "," + NumeroEntrada + ",'" + diagnostico + "','" + observacion + "')";

                    //MessageBox.Show(Query);
                    ObjServer.CadnaSentencia = Query;
                    ObjServer.Sentencia();
                }
            }
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            //insertar en Ciclo Menstrual
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
                       ",[CicMens_Planificacion]) " +
                         " VALUES(" + NumeroEntrada + ",'" + "01/01/2017" + "'," +
                                TxtHijosSanos.Text + ",'" + TxtGestaciones.Text + "','" +
                                TxtPartos.Text + "','" + TxtAbostos.Text + "','" + TxtHijos.Text + "','" +
                                TxtresultadoCitologia.Text + "','" + planificacion + "')";
            //MessageBox.Show(Query);
            ObjServer.CadnaSentencia = Query;
            ObjServer.Sentencia();
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            //Insertar DgvHabitos
            for (int i = 0; i < DgvHabitos.Rows.Count - 1; i++)
            {
                //Para saber si hay datos para agregar
                Boolean confirmar = false;
                int habito = 0;
                string caracteristica = "";
                string frecuencia = "";
                int tiempo = 0;
                string observacion = "";
                if (DgvHabitos.Rows[i].Cells["DgvHabitosColTiempoConsumo"].Value != null || DgvHabitos.Rows[i].Cells["DgvHabitosColCaracteristica"].Value != null || DgvHabitos.Rows[i].Cells["DgvHabitosColTiempoConsumo"].Value != null)
                {
                    habito = Convert.ToInt32(DgvHabitos.Rows[i].Cells["DgvHabitosColCodigo"].Value);
                    caracteristica = DgvHabitos.Rows[i].Cells["DgvHabitosColCaracteristica"].Value.ToString();
                    frecuencia = DgvHabitos.Rows[i].Cells["DgvHabitosColFrecuencia"].Value.ToString();
                    tiempo = Convert.ToInt32(DgvHabitos.Rows[i].Cells["DgvHabitosColTiempoConsumo"].Value);
                    observacion = DgvHabitos.Rows[i].Cells["DgvHabitosColObservacion"].Value.ToString();

                    confirmar = true;
                }
                else
                { confirmar = false; break; }

                if (confirmar == true)
                {
                    ////Insertar en DgvAntecedentePersonal
                    Query = "INSERT INTO [dbo].[HabitoPaciente] " +
                              "([HabPac_Habito_Codigo]  " +
                              ",[HabPac_Entrada_Numero] " +
                              ",[HabPac_Caracteristica] " +
                              ",[HabPac_Frecuencia]     " +
                              ",[HabPac_Tiempo]         " +
                              ",[HabPac_Observacion])   " +
                                "VALUES (" + "" + habito + "," + NumeroEntrada + ",'" + caracteristica + "','" + frecuencia + "','" + tiempo + "','" + observacion + "')";

                    //MessageBox.Show(Query);
                    ObjServer.CadnaSentencia = Query;
                    ObjServer.Sentencia();
                }
            }
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

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
                    Dosis = DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColDosis"].Value.ToString();
                    fecha = Convert.ToDateTime(DgvInmunizacion.Rows[i].Cells["DgvInmunizacionColFecha"].Value.ToString()).ToShortDateString();

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

                    //MessageBox.Show(Query);
                    ObjServer.CadnaSentencia = Query;
                    ObjServer.Sentencia();
                }
            }
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


            //Insertar en Dgvrevision
            for (int i = 0; i < Dgvrevision.Columns.Count; i++)
            {
                for (int x = 0; x < Tablasistema.Rows.Count; x++)
                {
                    string Col = Tablasistema.Rows[x]["Sist_Descripcion"].ToString();
                    string Col2 = Tablasistema.Rows[x]["Sist_Descripcion"].ToString() + x.ToString();

                    if (Dgvrevision.Columns[i].Name == Col2)
                    {
                        //MessageBox.Show(Col2);
                        for (int z = 0; z < Dgvrevision.Rows.Count; z++)
                        {
                            if (Dgvrevision.Rows[z].Cells[Col2].Value != null)
                            {
                                if (Convert.ToBoolean(Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Value) == true)
                                {
                                    int CODIGO = Convert.ToInt32(Dgvrevision.Rows[z].Cells[Col2].Value);
                                    ////Insertar en Dgvrevision
                                    Query = "INSERT INTO [dbo].[RevisionSistema] " +
                                               "([RevSist_Entrada_Numero]   " +
                                               ",[RevSist_Revision_Codigo]) " +
                                                   "VALUES (" + "" + NumeroEntrada + "," + CODIGO + ")";

                                    //MessageBox.Show(Query);
                                    ObjServer.CadnaSentencia = Query;
                                    ObjServer.Sentencia();
                                    Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Value = false;
                                    //Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Style.BackColor = Color.Red;
                                }
                            }
                            else
                                break;
                        }
                    }
                }

            }
            //<<<<<<<<<<<<<<<<<<

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
            //MessageBox.Show(Query);
            ObjServer.CadnaSentencia = Query;
            ObjServer.Sentencia();
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        }
        //public 
        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                
            if (MessageBox.Show("Realmente desea modificar?","Confirmar",MessageBoxButtons.OKCancel,MessageBoxIcon.Information)==DialogResult.OK)
            {
                
            int NumeroEntrada = Convert.ToInt32(LblEntrada.Text);
            string QueryI = " DELETE FROM [dbo].[AccidenteLaboral]  WHERE AccLab_Entrada_Numero="+NumeroEntrada;
            ObjServer.CadnaSentencia = QueryI;
            ObjServer.Sentencia();

            QueryI = "DELETE FROM [dbo].[ExamenLaboratorio] WHERE ExaLabo_Entrada_Numero=" + NumeroEntrada;
            ObjServer.CadnaSentencia = QueryI;
            ObjServer.Sentencia();
                
            QueryI = "DELETE FROM [dbo].[AntecedentePersonal] WHERE AntPer_Entrada_Numero=" + NumeroEntrada;
            ObjServer.CadnaSentencia = QueryI;
            ObjServer.Sentencia();

            QueryI = "DELETE FROM [dbo].[AntecednteFamiliar] WHERE AntFam_Entrada_Numero =" + NumeroEntrada;
            ObjServer.CadnaSentencia = QueryI;
            ObjServer.Sentencia();
                         
            QueryI = "DELETE FROM [dbo].[CicloMenstrual] WHERE CicMens_Entrada_Numero=" + NumeroEntrada;
            ObjServer.CadnaSentencia = QueryI;
            ObjServer.Sentencia();

            QueryI = "DELETE FROM [dbo].[EnfermedadProfesional] WHERE EnfPro_Entrada_Numero=" + NumeroEntrada;
            ObjServer.CadnaSentencia = QueryI;
            ObjServer.Sentencia();

            QueryI = "DELETE FROM [dbo].[ExamenFisico] WHERE ExaFisi_Entrada_Numero=" + NumeroEntrada;
            ObjServer.CadnaSentencia = QueryI;
            ObjServer.Sentencia();

            QueryI = "DELETE FROM [dbo].[ExamenPracticado] WHERE  ExaPrac_Examen_Codigo=" + NumeroEntrada;
            ObjServer.CadnaSentencia = QueryI;
            ObjServer.Sentencia();

            QueryI = "DELETE FROM [dbo].[HabitoPaciente] WHERE HabPac_Entrada_Numero=" + NumeroEntrada;
            ObjServer.CadnaSentencia = QueryI;
            ObjServer.Sentencia();

            QueryI = "DELETE FROM [dbo].[InformacionOcupacional] WHERE InfOcu_Entrada_Numero=" + NumeroEntrada;
            ObjServer.CadnaSentencia = QueryI;
            ObjServer.Sentencia();

            QueryI = "DELETE FROM [dbo].[Inmunizar] WHERE Inmu_Entrada_Numero=" + NumeroEntrada;
            ObjServer.CadnaSentencia = QueryI;
            ObjServer.Sentencia();

            QueryI = "DELETE FROM [dbo].[ProbabilidadRiego] WHERE ProbRiesg_Entrada_Numero=" + NumeroEntrada;
            ObjServer.CadnaSentencia = QueryI;
            ObjServer.Sentencia();

            QueryI = "DELETE FROM [dbo].[RevisionSistema] WHERE RevSist_Entrada_Numero=" + NumeroEntrada;
            ObjServer.CadnaSentencia = QueryI;
            ObjServer.Sentencia();

            QueryI = "DELETE FROM [dbo].[RiesgoOcupacional] WHERE RiegOcu_Entrada_Numero=" + NumeroEntrada;
            ObjServer.CadnaSentencia = QueryI;
            ObjServer.Sentencia();

            ActualizarDatos(NumeroEntrada);
            }
            }
            catch (Exception)
            {
                MessageBox.Show("La operación no puedo completarse debido a: \n 1 - No dispone de una conexión  \n 2 - El registro esta duplicado  \n 3 - La información ingrasado no corresponde a la requerida  \n Vuelva a intentarlo!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtSesion_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjServer.Solo_Letras(e);
        }

        private void TxtMateriaPrima_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjServer.Solo_Letras(e);
        }

        private void TxtHijosSanos_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjServer.Solo_Numeros(e);
        }

        private void TxtPresionArterial_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjServer.Solo_Numeros(e);
        }

        private void DgvAccidenteLaboral_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (DgvAccidenteLaboral.CurrentCell.ColumnIndex == 1)
    {

        TextBox txt = e.Control as TextBox;

        if (txt != null) {
            txt.KeyPress -= new KeyPressEventHandler(DgvAccidenteLaboral_KeyPress );
            txt.KeyPress += new  KeyPressEventHandler(DgvAccidenteLaboral_KeyPress );
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
        string[] elementos={} ;
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
            DgvEnfermedadProfesional.Rows[DgvEnfermedadProfesional.Rows.Count-1].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value = "01/01/1990";
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
                    if (MessageBox.Show("Los datos serán eliminados de forma permanente", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
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
            //string titleText = DgvAntecedentePersonal.Columns["DgvAntecedentePersonalColDiagnostico"].HeaderText;
            if (DgvAntecedentePersonal.CurrentCell.ColumnIndex==3)
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
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
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
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
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
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                        try
                        {
                            if (DgvAccidenteLaboral.Rows.Count > 1)
                            {
                                DgvProbabilidadRiesgo.Rows.RemoveAt(DgvProbabilidadRiesgo.CurrentRow.Index);
                            }
                            else
                            {
                                DgvProbabilidadRiesgo.Rows[DgvProbabilidadRiesgo.CurrentRow.Index].Cells["DgvProbabilidadRiesgoColEstimacion"].Value = "";
                                DgvProbabilidadRiesgo.Rows[DgvProbabilidadRiesgo.CurrentRow.Index].Cells["DgvProbabilidadRiesgoColProbabilidad"].Value = "";
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
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                        try
                        {
                            if (DgvRiesgoOcupacional.Rows.Count > 1)
                            {
                                DgvRiesgoOcupacional.Rows.RemoveAt(DgvRiesgoOcupacional.CurrentRow.Index);
                            }
                            else
                            {
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
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
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
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                        try
                        {
                            if (DgvExamenPacticado.Rows.Count > 1)
                            {
                                DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentRow.Index].Cells["DgvExamenPacticadoColAjuntar"].Value = ".....";
                                DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentRow.Index].Cells["DgvExamenPacticadoColResultado"].Value = "";
                            }
                            else
                            {
                                DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentRow.Index].Cells["DgvExamenPacticadoColAjuntar"].Value = ".....";
                                DgvExamenPacticado.Rows[DgvExamenPacticado.CurrentRow.Index].Cells["DgvExamenPacticadoColResultado"].Value = "";
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
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
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
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
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
                if (MessageBox.Show("Los datos serán eliminados de la fila", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    try
                    {
                        if (DgvAntecedenteFamiliar.Rows.Count > 1)
                        {
                            DgvAntecedenteFamiliar.Rows.RemoveAt(DgvAntecedenteFamiliar.CurrentRow.Index);
                        }
                        else
                        {
                            DgvAntecedenteFamiliar.Rows[DgvAntecedenteFamiliar.CurrentRow.Index].Cells["DgvAntecedenteFamiliarColMortalidad"].Value = "";
                            DgvAntecedenteFamiliar.Rows[DgvAntecedenteFamiliar.CurrentRow.Index].Cells["DgvAntecedenteFamiliarColParentesco"].Value = 0;

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
            string CodigoDi = "";
            string titleText = DgvAntecedentePersonal.Columns["DgvAntecedentePersonalColDiagnostico"].HeaderText;
            if (DgvAntecedentePersonal.CurrentCell.ColumnIndex == 3)
            {
                string[] DiagnCode = DgvAntecedentePersonal.CurrentRow.Cells["DgvAntecedentePersonalColDiagnostico"].Value.ToString().Split('-');
                for (int i = 0; i < DiagnCode.Length; i++)
                {
                    if (DiagnCode[i].Length==4)
                    {
                        CodigoDi = DiagnCode[i];
                        DgvAntecedentePersonal.CurrentRow.Cells["DgvAntecedentePersonalColDiagnostico"].Value = CodigoDi;
                    }
                }
            }
        }        
    }
}
