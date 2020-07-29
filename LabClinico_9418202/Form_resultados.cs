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
    public partial class Form_resultados : Form
    {
        public Form_resultados()
        {
            InitializeComponent();
        }
		MySqlConnection conex = new MySqlConnection("Server = 127.0.0.1; User=root; Database=BBDDLABORATORIOcintiadiaz;password='';");
		
		private void button2_Click(object sender, EventArgs e)
		{
			conex.Open();
			DataTable tabla = new DataTable();
			MySqlDataAdapter adapter = new MySqlDataAdapter("select * from resultados_cintia_diaz ", conex);
			tabla.Clear();
			adapter.Fill(tabla);
			dataGridView1.DataSource = tabla;
			conex.Close();
		}
	}
}
