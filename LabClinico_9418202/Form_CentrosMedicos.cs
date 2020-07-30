using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LabClinico_9418202
{
    public partial class Form_CentrosMedicos : Form
    {
        public Form_CentrosMedicos()
        {
            InitializeComponent();
        }

        MySqlConnection conex = new MySqlConnection("Server = 127.0.0.1; User=root; Database=BBDDLABORATORIOcintiadiaz;password='';");

        private void button1_Click(object sender, EventArgs e)
        {
            conex.Open();
            DataTable tabla_PERFILES = new DataTable();
            MySqlDataAdapter sentencia = new MySqlDataAdapter("select * from centrosmedicos_cintia_diaz where origen like '%" + textBox1.Text + "%'", conex);
            tabla_PERFILES.Clear();
            sentencia.Fill(tabla_PERFILES);
            dataGridView1.DataSource = tabla_PERFILES;
            conex.Close();
        }

        private void Form_CentrosMedicos_Load(object sender, EventArgs e)
        {
            conex.Open();
            DataTable tabla_PERFILES = new DataTable();
            MySqlDataAdapter sentencia = new MySqlDataAdapter("select * from centrosmedicos_cintia_diaz", conex);
            tabla_PERFILES.Clear();
            sentencia.Fill(tabla_PERFILES);
            dataGridView1.DataSource = tabla_PERFILES;
            conex.Close();
        }
    }
}
