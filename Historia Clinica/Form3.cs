using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Historia_Clinica
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        int x = 0;
        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 3;

            dataGridView1.Columns[0].Name = "Product ID";

            dataGridView1.Columns[1].Name = "Product Name";

            dataGridView1.Columns[2].Name = "Product Price";
            
            string[] row = new string[] { "1", "Product 1", "1000" };

            dataGridView1.Rows.Add(row);

            row = new string[] { "2", "Product 2", "2000" };

            dataGridView1.Rows.Add(row);

            row = new string[] { "3", "Product 3", "3000" };

            dataGridView1.Rows.Add(row);

            row = new string[] { "4", "Product 4", "4000" };

            dataGridView1.Rows.Add(row);
        }
        public void addItems(AutoCompleteStringCollection col)
        {            
            col.Add("Product 1");
            col.Add("jose 2");
            col.Add("Product 3");
            col.Add("ana 4");
            col.Add("Product 5");
            col.Add("pedro 6");

        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //dataGridView1.Refresh();
            //Int32 selectedColumnCount = dataGridView1.Columns.GetColumnCount(DataGridViewElementStates.Selected);
             //num_Col = 0;
             int num_Col = dataGridView1.CurrentCell.ColumnIndex;
             //MessageBox.Show(num_Col.ToString());

             string titleText = dataGridView1.Columns[1].HeaderText;

             if (titleText.Equals("Product Name"))
             {
                 TextBox autoText = e.Control as TextBox;

                 if (autoText != null)
                 {
                     autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
                     autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
                     AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                     addItems(DataCollection);
                     autoText.AutoCompleteCustomSource = DataCollection;
                 }
             }


             //MessageBox.Show("Letra");
             //DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;
         
          //  if (num_Col == 1)
          //{   //DataGridViewTextBoxColumn dText = new (DataGridViewTextBoxColumn)e.Control;
          //    //dText.KeyPress -= new KeyPressEventHandler(dText_KeyPress2);

          //    if (dText != null)
          //    {
          //        dText.KeyPress -= new KeyPressEventHandler(dText_KeyPress);
          //        dText.KeyPress += new KeyPressEventHandler(dText_KeyPress);
          //    }
          //}
          //  else if (num_Col == 0)
          //    {
          //        //dText.KeyPress -= new KeyPressEventHandler(dText_KeyPress);
          //        if (dText != null)
          //        {
          //            dText.KeyPress -= new KeyPressEventHandler(dText_KeyPress2);
          //            dText.KeyPress += new KeyPressEventHandler(dText_KeyPress2);
          //        }

          //    }


        }
        void dText_KeyPress2(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        void dText_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '\b')
            //{
            //    e.Handled = true;
            //}
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dataGridView1.Columns[1].Index.ToString());
        }
    }
}
