using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

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

		private void button1_Click(object sender, EventArgs e)
		{
			if (rutValido(textBox4.Text))
			{
				conex.Open();
				DataTable tabla = new DataTable();
				string clav = textBox3.Text.Substring(0, 1) + textBox4.Text.Substring(0, 1) + textBox5.Text.Substring(0, 1) + textBox2.Text;
				string sqlinsertar = "insert into PERFILESCINTIADIAZ (rut,nombre,ApPat,ApMat,clave,Nivel) values  ('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + clav + "')";
				MySqlDataAdapter sentencia = new MySqlDataAdapter(sqlinsertar, conex);
				tabla.Clear();
				sentencia.Fill(tabla);
				textBox2.Text = "";
				textBox3.Text = "";
				textBox4.Text = "";
				textBox5.Text = "";
			
				//muestra la actualizacion en el datagrid
				DataTable tabla_PERFILES = new DataTable();
				sentencia = new MySqlDataAdapter("select * from PERFILESCINTIADIAZ", conex);
				tabla_PERFILES.Clear();
				sentencia.Fill(tabla_PERFILES);
				dataGridView1.DataSource = tabla_PERFILES;
				conex.Close();
				MessageBox.Show("RUT válido, datos agregados exitosamente");
			}
			else
			{
				MessageBox.Show("RUT inválido");
			}
		}
		//valida el digito verificador del rut
		private bool rutValido(string rut)
		{

			Regex rgx = new Regex(@"^\d{1,8}-(?:\d|k|K)$");
			if (!rgx.IsMatch(rut))
			{
				MessageBox.Show("Rut con formato inválido");
				return false;
			}
			int RUT_NUM_CHARS = 10;
			rut = rut.Replace(".", "");
			if ((rut.Length < 3) | rut[rut.Length - 2] != '-')
			{
				return false;
			}
			int cerosFaltantes = RUT_NUM_CHARS - rut.Length;
			rut = (new String('0', cerosFaltantes)) + rut;
			int[] nums = { 0, 0, 0, 0, 0, 0, 0, 0 };
			int[] CONSTANTES = { 3, 2, 7, 6, 5, 4, 3, 2 };
			for (int i = 0; i < nums.Length; i++)
			{
				nums[i] = CONSTANTES[i] * Int32.Parse(rut[i].ToString());
			}
			double suma = nums[0] + nums[1] + nums[2] + nums[3] + nums[4] + nums[5] + nums[6] + nums[7];
			double divisiondecimal = suma / 11;
			double divisionentero = (int)divisiondecimal;
			double valordecimal = divisiondecimal - divisionentero;
			double resta11 = 11 - (11 * (valordecimal));
			resta11 = Math.Round(resta11);
			int digito = (int)resta11;
			if (digito == 11)
			{
				digito = 0;
			}
			int digitoVer;
			if ((rut[9] == 'k') | (rut[9] == 'K'))
			{
				digitoVer = 10;
			}
			else
			{
				digitoVer = Int32.Parse(rut[9].ToString());
			}
			return digito == digitoVer;
		}

	
	}
}

