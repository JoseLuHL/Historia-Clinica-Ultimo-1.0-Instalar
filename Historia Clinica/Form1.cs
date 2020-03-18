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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            //DgvAccidenteLaboral.RowHeadersVisible = false;
            textBox50.Text = "Ingrese Nombre";
            textBox50.ForeColor = Color.Gray;
                    
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox50_Enter(object sender, EventArgs e)
        {
            if (textBox50.Text=="Ingrese Nombre")
            {
                textBox50.Clear();
                textBox50.ForeColor = Color.Black;
            }
        }

        private void textBox50_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void textBox50_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void textBox50_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void textBox50_Leave(object sender, EventArgs e)
        {
            if (textBox50.Text == "")
            {
                textBox50.Text = "Ingrese Nombre";
                textBox50.ForeColor = Color.Gray;

            }
        }
    }
}
