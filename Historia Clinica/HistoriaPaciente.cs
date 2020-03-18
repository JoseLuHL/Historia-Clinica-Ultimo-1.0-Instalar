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
namespace Historia_Clinica
{
    public partial class HistoriaPaciente : Form
    {
        public HistoriaPaciente()
        {
            InitializeComponent();
        }
        //Objeto de la clase que se utiliza donde estan los metodos y demas 
        ClsSqlServer ObjServer = new ClsSqlServer();

        DataTable TablaPaciente = new DataTable();
        public string Nhistoria = "10183";
        DataTable Tablasistema = new DataTable();
        DataTable Recomendaciones = new DataTable();
        public void CargarHistoriaPaciente()
        {
            try
            {
                //<<<<<<<<<<<<para preciones la tecla Enter en el control de Buscar>>>>>>>>>>>>>>

                    //<<<<<<<<<<<<<<<<<<<<<<
                    string Query = "SELECT	dbo.Paciente.Pac_Nombre1+ ' ' + isnull(dbo.Paciente.Pac_Nombre2,'')                         " +
                                    "		+' '+dbo.Paciente.Pac_Apellido1+ ' '+isnull(dbo.Paciente.Pac_Apellido2,'') As Nombres,      " +
                                    "		dbo.Paciente.Pac_TipoIdentificacion, dbo.Paciente.Pac_Identificacion,                       " +
                                    "		dbo.Paciente.Pac_FechaNacimiento, dbo.Genero.Gen_Descripcion,                               " +
                                    "		dbo.Paciente.Pac_LugarNacimiento, dbo.Paciente.Pac_Direccion,                               " +
                                    "		dbo.Paciente.Pac_NivelEducacion, dbo.Paciente.Pac_Profesion_Codigo,                         " +
                                    "		dbo.TipoSangre.TipSan_Descripcion, dbo.Paciente.Pac_EstadoCivil,                            " +
                                    "		dbo.EstadoCivil.EstCivil_Descripcion,Pac_Telefono                                           " +
                                    "FROM	dbo.Paciente LEFT OUTER JOIN                                                                " +
                                    "		dbo.EstadoCivil ON dbo.Paciente.Pac_EstadoCivil =                                           " +
                                    "		dbo.EstadoCivil.EstCivil_Codigo LEFT OUTER JOIN                                             " +
                                    "		dbo.TipoSangre ON dbo.Paciente.Pac_TipoSangre =                                             " +
                                    "		dbo.TipoSangre.TipSan_Codigo LEFT OUTER JOIN                                                " +
                                    "		dbo.Genero ON dbo.Paciente.Pac_Genero_Codigo = dbo.Genero.Gen_Codigo                        " +
                                    "WHERE	Pac_Identificacion=" + TxtDocumento.Text;
                    TablaPaciente = ObjServer.LlenarTabla(Query);
                    //TablaPaciente = ObjServer.LlenarTabla("SELECT [Pac_Nombre1] + ' ' + [Pac_Nombre2]+ ' ' + [Pac_Apellido1]+ ' ' +[Pac_Apellido2] As [Nombres] FROM [dbo].[Paciente] 
                    LblEntrada.Text = "";
                    if (TablaPaciente.Rows.Count > 0)
                    {
                        TxtNombre.Text = TablaPaciente.Rows[0]["Nombres"].ToString();
                        CargarHistoria(Nhistoria);
                    }
                    else
                    {
                        LblEntrada.Text = "";
                        TxtNombre.Clear();
                        MessageBox.Show("No hay historia para mostrar", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //DgvRecomendaciones.Rows.Clear();
                    }
                    //>>>>>>>>>>>>>>>>>>>>>>

                //<<<<<<<<<<<<Fin precionar tecla enter>>>>>>>>>>>>>>>>>>>>>>>

            }
            catch (Exception)
            {
            }
        }
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
        public void EstilosDgv(DataGridView DGV)
        {
            DGV.DefaultCellStyle.Font = new Font("Verdana", 11);
            Font prFont = new Font("Verdana", 11, FontStyle.Bold);
            for (int i = 0; i < DGV.Columns.Count; i++)
            {
                DGV.Columns[i].HeaderCell.Style.Font = prFont;
                DGV.Columns[i].HeaderCell.Style.ForeColor = Color.White;
                DGV.Columns[i].HeaderCell.Style.BackColor = Color.DimGray;
                DGV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            DGV.AutoResizeColumns();
            DGV.EnableHeadersVisualStyles = false;
        }
        public void CargarHistoria(string NUmerpEntrada)
        {
            DataTable tabla = new DataTable();
            tabla = ObjServer.LlenarTabla("SELECT [Entr_Numero] FROM [dbo].[EntradaHistoria] WHERE [Entr_Numero] =" + NUmerpEntrada + " order by Entr_numero desc");

            if (tabla.Rows.Count > 0)
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
                                                "dbo.ParteAfectada.PartA_Codigo WHERE dbo.AccidenteLaboral.AccLab_Entrada_Numero=" + NumeroEntrada);

                if (Datos.Rows.Count > 0)
                {
                    DgvAccidenteLaboral.RowCount = Datos.Rows.Count + 1;
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
                        //DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColFecha"].Value = Datos.Rows[i]["ProbRiesg_Probabilidad_Codigo"];
                        string fecha = Convert.ToDateTime(Datos.Rows[i]["EnfPro_FechaDiagnostico"]).ToString("dd/MM/yyyy");
                        DgvEnfermedadProfesional.Rows[i].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value = fecha;
                        //Datos.Rows[i]["EnfPro_FechaDiagnostico"].ToString();


                    }
                }
                //<<<<<<<<<<<<<<<<<<<<<<<<
                //CARGAR LOS DATOS EN ANTECEDENTES FAMILIARES.
                Datos = new DataTable();
                Datos = ObjServer.LlenarTabla("SELECT dbo.AntecednteFamiliar.AntFam_Numero,                     " +
                                                   "dbo.AntecednteFamiliar.AntFam_Enfermedad_Codigo,            " +
                                                   "dbo.AntecednteFamiliar.AntFam_Entrada_Numero,               " +
                                                   "dbo.AntecednteFamiliar.AntFam_Parentesco,                   " +
                                                   "dbo.AntecednteFamiliar.AntFam_Mortalidad,                   " +
                                                   "dbo.Enfermedad.Enf_Descipcion                               " +
                                                   "FROM   dbo.AntecednteFamiliar INNER JOIN  dbo.Enfermedad ON " +
                                                   "dbo.AntecednteFamiliar.AntFam_Enfermedad_Codigo =           " +
                                                   "dbo.Enfermedad.Enf_Codigo WHERE AntFam_Entrada_Numero=" + NumeroEntrada);
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
                                                       "(SELECT [Diag_Descripcion] FROM [dbo].[Diagnostico] WHERE [Diag_Codigo]=dbo.AntecedentePersonal.AntPer_Diagnostico) As AntPer_Diagnostico ,                " +
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
                    for (int i = 0; i < Datos.Rows.Count; i++)
                    {
                        if (DgvHabitos.Rows[i].Cells["DgvHabitosColCodigo"].Value.ToString() == Datos.Rows[i]["HabPac_Habito_Codigo"].ToString())
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

                //CARGAR en DGVRECOMENDACIONES
                //DgvRecomendaciones.Rows.Clear();
                Datos = new DataTable();
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
                                            DgvRecomendaciones.Rows[z].Cells["chk" + x.ToString()].Style.BackColor = Color.Red;
                                            DgvRecomendaciones.Rows[z].Cells[Col].Style.BackColor = Color.SteelBlue;
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
                                            Dgvrevision.Rows[z].Cells["chk" + x.ToString()].Style.BackColor = Color.Red;
                                            Dgvrevision.Rows[z].Cells[Col].Style.BackColor = Color.SteelBlue;
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
                        //DgvExamenLaboratorio.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value = Datos.Rows[i]["ExaLabo_ExamenCodigo"];

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
                        DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value = Datos.Rows[i]["ExaPrac_Examen_Codigo"];
                        DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value = Datos.Rows[i]["ExaPrac_Ajuntar"];
                        DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value = Datos.Rows[i]["ExaPrac_FechaExamen"];
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
                                                ",InfOcu_Empresa          " +
                                                " FROM [dbo].[InformacionOcupacional] WHERE InfOcu_Entrada_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    TxtNombreEmpresa.Text = Datos.Rows[0]["InfOcu_Empresa"].ToString();
                    DtFechaIngreso.Value = Convert.ToDateTime(Datos.Rows[0]["InfOcu_FechaIngreso"].ToString());
                    TxtJornada.Text = Datos.Rows[0]["InfOcu_Jornada"].ToString();
                    TxtCargo.Text = Datos.Rows[0]["InfOcu_CodOcupacion"].ToString();
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
                                                ",[CicMens_Planificacion], [CicMens_Edadmenopausia],[CicMens_Edadmenarca],[CicMens_metodo]     " +
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
                                              ",(SELECT [Diag_Descripcion] FROM [dbo].[Diagnostico] WHERE [Diag_Codigo]=[Entr_Diagnostico]) As Entr_Diagnostico     " +
                                              ",[Entr_Concepto_Codigo] " +
                                              ",[Entr_Recomendacion]   " +
                                              ",[Entr_Reubicacion]     " +
                                              ",Entr_TipoExamenCodigo  " +
                                              ",Ent_Enfasis            " +
                                              " FROM [dbo].[EntradaHistoria] WHERE Entr_Numero=" + NumeroEntrada);
                if (Datos.Rows.Count > 0)
                {
                    TxtRecomendacion.Text = Datos.Rows[0]["Entr_Recomendacion"].ToString();
                    CboConcepto.SelectedValue = Datos.Rows[0]["Entr_Concepto_Codigo"];
                    DtFechaEntrada.Value = Convert.ToDateTime(Datos.Rows[0]["Entr_FechaEntrada"]);
                    CboDiagnostico.Text = Datos.Rows[0]["Entr_Diagnostico"].ToString();
                    CboTipoExamen.SelectedValue = Datos.Rows[0]["Entr_TipoExamenCodigo"];
                    CboEnfasis.SelectedValue = Datos.Rows[0]["Ent_Enfasis"];
                    if (Datos.Rows[0]["Entr_Reubicacion"].ToString() == "True")
                        RdbReuSi.Checked = true;
                    else
                        RdbReuNo.Checked = true;
                }
                else
                {
                }

            }
            tabla = null;
        }
        public void CargarIinicio()
        {
            try
            {

                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                DgvRecomendaciones.Rows.Clear();
                DgvRecomendaciones.Columns.Clear();
                DgvAccidenteLaboral.Rows.Clear();
                DgvAntecedenteFamiliar.Rows.Clear();
                TxtElementosProte.Clear();
                DgvEnfermedadProfesional.Rows.Clear();
                DgvAccidenteLaboral.Rows[DgvAccidenteLaboral.Rows.Count - 1].Cells["DgvAccidenteLaboralColFecha"].Value = "01/01/1990";
                DgvEnfermedadProfesional.Rows[DgvEnfermedadProfesional.Rows.Count - 1].Cells["DgvEnfermedadProfesionalColFechaDiagnostigo"].Value = "01/01/1990";
                DgvInmunizacion.Rows[DgvInmunizacion.Rows.Count - 1].Cells["DgvInmunizacionColFecha"].Value = "01/01/1990";
                //DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value

                // Cargo los datos que tendra el combobox DgvExamenPacticadoColResultado
                DgvExamenPacticadoColResultado.DataSource = ObjServer.LlenarTabla("SELECT [TipRes_Codigo] As Codigo  ,[TipRes_Descripcion] As Descripcion  FROM [dbo].[TipResultado]");
                DgvExamenPacticadoColResultado.DisplayMember = "Descripcion";
                DgvExamenPacticadoColResultado.ValueMember = "Codigo";
                //

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
                    //DgvRiesgoOcupacional.Rows[i].DefaultCellStyle.BackColor = Color.WhiteSmoke;
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

                DgvExamenPacticado.RowCount = 3;
                for (int i = 0; i < 2; i++)
                {
                    DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColExamen"].Value = tabla.Rows[i]["Codigo"];
                    DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColResultado"].Value = 2;
                    DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColAjuntar"].Value = "......";
                    DgvExamenPacticado.Rows[i].Cells["DgvExamenPacticadoColFecha"].Value = "01/01/1990";
                }
                //<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>

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
                    //DgvRecomendaciones.Columns["chk" + i.ToString()].Width = 30;
                    //DgvRecomendaciones.Columns["chk" + i.ToString()].Visible = false;
                    DgvRecomendaciones.Columns[(Recomendaciones.Rows[i]["Reco_Descripcion"].ToString() + i.ToString())].Visible = false;
                    DgvRecomendaciones.Columns[(Recomendaciones.Rows[i]["Reco_Descripcion"].ToString())].ReadOnly = true;
                    //DgvRecomendaciones.Columns[(Recomendaciones.Rows[i]["Reco_Descripcion"].ToString())].Visible = false;

                    //DgvRecomendaciones.Columns[(Recomendaciones.Rows[i]["Reco_Descripcion"].ToString())].ReadOnly = true;

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

                TxtRecomendacion.Text = "Recomendación";
                

                TxtPresionArterial.Text = "0";
                TxtFrecuenciaCardiaca.Text = "0";
                TxtLateracidad.Text = "Lateracidad";
                TxtPeso.Text = "0";
                TxtTalla.Text = "0";
                TxtPerimetroCintura.Text = "0";
                TxtIMC.Text = "0";
                TxtInterpretacion.Text = "Interpretación";

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
            //MessageBox.Show("cargado");
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

        private void HistoriaPaciente_Load(object sender, EventArgs e)
        {
            //Establecer conexión  con el Gestor.
            ObjServer.CadenaCnn = Conexion.CadenaConexion.cadena();
            ObjServer.Conectar();
            CargarIinicio();

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

            tamañoDGV();
            TxtHijos.Text = "0";
            TxtHijosSanos.Text = "0";

            //CargarEnfasisEn(CboTipoExamen.SelectedValue.ToString());
            CargarDatosEmpresa();
            CargarEnfasisEn();
            //CargarIconosDGV();
            //MessageBox.Show("gfhj");
            //----Fin Establecer conexión-----
            CargarHistoriaPaciente();
            DgvAccidenteLaboral.ReadOnly = true;
            DgvAntecedenteFamiliar.ReadOnly = true;
            DgvAntecedentePersonal.ReadOnly = true;
            DgvEnfermedadProfesional.ReadOnly = true;
            DgvExamenLaboratorio.ReadOnly = true;
            DgvExamenPacticado.ReadOnly = true;
            DgvHabitos.ReadOnly = true;
            DgvInmunizacion.ReadOnly = true;
            DgvProbabilidadRiesgo.ReadOnly = true;
            DgvRecomendaciones.ReadOnly = true;
            Dgvrevision.ReadOnly = true;
            DgvRiesgoOcupacional.ReadOnly = true;    
        }

        private void DgvExamenPacticado_CellClick_1(object sender, DataGridViewCellEventArgs e)
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
                }
            }
        }
    }
}
