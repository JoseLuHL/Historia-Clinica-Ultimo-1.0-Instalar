using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Conexion
{
    public partial class FrmSistema : Form
    {
        public FrmSistema()
        {
            InitializeComponent();
        }
        string OrigenesDatos;
        ClsSqlServer NuevoSql= new ClsSqlServer();
        private void FrmContenido_Load(object sender, EventArgs e)
        {
            //OrigenesDatos = FrmSqlServer.OrigenDatos;

            DgvTablas.Columns[0].Width = 210;
            DgvDetalleTablas.EndEdit();
        }

        public string where(string[] columna, string[] valor, string[] TipoCol)
        {
            string clausula = "";
            string valor2 = "";
            for (int i = 0; i < columna.Length; i++)
            {
                 valor2="";

                 if (TipoCol[i] == "System.String")
                    valor2 = "'" + valor[i] + "'";
                else
                    valor2 = valor[i];

                if (i>0)
                    clausula= clausula + " AND " + columna[i] + "=" + valor2;
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

                if (i>0)
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
                    Consulta = Consulta + ", " +""+ "" + valor2;
                else
                    Consulta = "" + "" + valor2;
            }
            Consulta = "INSERT " + tabla + " VALUES (" + Consulta + ")";
            return Consulta;
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
                        ColPK++;
                        break;
                    }
                }
            }
            try
            {
                
            string Imagen = @"C:\Users\JOSEPH\Desktop\Administrador de Usuarios\WindowsApplication7\WindowsApplication6\WindowsApplication1\bin\Debug\img\guardar.ico";
            DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 1].Cells["DgvTablaDetalleColGuardar"].Value = Image.FromFile(Imagen);
            
                Imagen = @"C:\Users\JOSEPH\Desktop\Administrador de Usuarios\WindowsApplication7\WindowsApplication6\WindowsApplication1\bin\Debug\img\editar.ico";
            DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 1].Cells["DgvTablaDetalleColEditar"].Value = Image.FromFile(Imagen);

                Imagen = @"C:\Users\JOSEPH\Desktop\Administrador de Usuarios\WindowsApplication7\WindowsApplication6\WindowsApplication1\bin\Debug\img\eliminar.ico";
            DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count - 1].Cells["DgvTablaDetalleColEliminar"].Value = Image.FromFile(Imagen);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                
                throw;
            }
            for (int i = 3; i < ColPK+3; i++)
            {
                DgvDetalleTablas.Rows[DgvDetalleTablas.Rows.Count-1].Cells[i].Style.BackColor = Color.WhiteSmoke;
                DgvDetalleTablas.Columns[i].ReadOnly = false;
            }
        }
           
        private void DgvTablas_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (DgvTablas.CurrentRow.Index > -1)
                {
                    NuevoSql.CargarDatos(DgvDetalleTablas, "Select * from " + DgvTablas.CurrentCell.Value.ToString());
                    NuevoSql.CargarDatos(DgvClavePrimaria, "SELECT i.name AS IndexName,OBJECT_NAME(ic.OBJECT_ID) AS TableName,COL_NAME(ic.OBJECT_ID,ic.column_id) AS ColumnName FROM sys.indexes AS i INNER JOIN sys.index_columns AS ic ON i.OBJECT_ID = ic.OBJECT_ID AND i.index_id = ic.index_id and i.is_primary_key = 1 where OBJECT_NAME(ic.OBJECT_ID)='" + DgvTablas.CurrentCell.Value.ToString() + "'");
                    MarcarClavePK();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }       
        }
        //Para saber el numero de columnas que pertenecen a la clave primaria
        int ColPK = 0;
        private void DgvDetalleTablas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Para la columna de Agregar
                if (e.ColumnIndex == 0)
                {

                    switch (OrigenesDatos)
                    {
                        case "Microsoft SQL Server":

                            string Sentencia = "";
                            //para que no tome la ultima fila
                            if (e.RowIndex < DgvDetalleTablas.Rows.Count - 2  )
                            {
                                if (DgvDetalleTablas.CurrentRow.Cells["DgvTablaDetalleColEditar"].Style.BackColor == Color.GreenYellow)
                                {
                                    
                                        if (MessageBox.Show("Desea eliminar el registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            string[] columnasA = { };
                                            string[] columnasPK = { };

                                            string[] valoresA = { };
                                            string[] valoresPK = { };

                                            string[] TipoColA = { };
                                            string[] TipoColPK = { };
                                            int y = 0;
                                            //llenar los vectores de las columnas y valos que seran modificados
                                            for (int x = 3 + ColPK; x < DgvDetalleTablas.Columns.Count; x++)
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
                                            string Query = sentencia(columnasA, valoresA, DgvTablas.CurrentCell.Value.ToString(), TipoColA) + " " + where(columnasPK, valoresPK, TipoColPK);
                                            NuevoSql.CadnaSentencia = Query;
                                            NuevoSql.Sentencia();
                                            MessageBox.Show("Operacion Exitosa!", "Editado");
                                        }

                                    else
                                        MessageBox.Show("No es posible editar el registro");
                                }


                                else if (DgvDetalleTablas.CurrentRow.Cells["DgvTablaDetalleColEliminar"].Style.BackColor == Color.GreenYellow)
                                {
                                    MessageBox.Show("SI");
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
                                            string Query = "DELETE FROM " + DgvTablas.CurrentCell.Value.ToString() + " " + where(columnasPK, valoresPK, TipoColPK);
                                            NuevoSql.CadnaSentencia = Query;
                                            NuevoSql.Sentencia();
                                            DgvDetalleTablas.Rows.RemoveAt(e.RowIndex);
                                            MessageBox.Show("Operacion Exitosa!", "Eliminado");
                                        }
                                    }
                                    else
                                        MessageBox.Show("No es posible eliminar el registro");
                                }

                            else
                            {
                                MessageBox.Show("eNTRE");
                                string[] columnasA = { };
                                string[] valoresA = { };
                                string[] TipoColA = { };
                                int y = 0;
                                //llenar los vectores de las columnas y valos que seran modificados
                                for (int x = 3; x < DgvDetalleTablas.Columns.Count; x++)
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
                                MessageBox.Show(Insertar(columnasA,valoresA,DgvTablas.CurrentCell.Value.ToString(),TipoColA));
                                string Query = Insertar(columnasA, valoresA, DgvTablas.CurrentCell.Value.ToString(), TipoColA);
                                NuevoSql.CadnaSentencia = Query;
                                NuevoSql.Sentencia();
                                //DgvDetalleTablas.Rows.RemoveAt(e.RowIndex);
                                MessageBox.Show("Operacion Exitosa!", "Eliminado");
                            }
                                //fghj;
                                //MessageBox.Show("hola");
                        break;
                    }
                    DgvDetalleTablas.CurrentRow.Cells["DgvTablaDetalleColEliminar"].Style.BackColor = Color.WhiteSmoke;
                    DgvDetalleTablas.CurrentRow.Cells["DgvTablaDetalleColEditar"].Style.BackColor = Color.WhiteSmoke;
                }

                //Para la columna de Editar
                if (e.ColumnIndex == 1)
                {
                    if (DgvDetalleTablas.CurrentRow.Cells["DgvTablaDetalleColEditar"].Style.BackColor == Color.GreenYellow)
                    {
                        DgvDetalleTablas.CurrentRow.Cells["DgvTablaDetalleColEditar"].Style.BackColor = Color.WhiteSmoke;
                    }
                    else
                    {
                        DgvDetalleTablas.CurrentRow.Cells["DgvTablaDetalleColEliminar"].Style.BackColor = Color.WhiteSmoke;
                        DgvDetalleTablas.CurrentRow.Cells["DgvTablaDetalleColEditar"].Style.BackColor = Color.GreenYellow;
                    }
                }

                //Para la columna de Eliminar
                if (e.ColumnIndex == 2)
                {
                    if (DgvDetalleTablas.CurrentRow.Cells["DgvTablaDetalleColEliminar"].Style.BackColor == Color.GreenYellow)
                    {
                        DgvDetalleTablas.CurrentRow.Cells["DgvTablaDetalleColEliminar"].Style.BackColor = Color.WhiteSmoke;
                    }
                    else
                    {
                        DgvDetalleTablas.CurrentRow.Cells["DgvTablaDetalleColEliminar"].Style.BackColor = Color.GreenYellow;
                        DgvDetalleTablas.CurrentRow.Cells["DgvTablaDetalleColEditar"].Style.BackColor = Color.WhiteSmoke;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        private void DgvDetalleTablas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
