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
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace LabClinico_9418202
{
	public partial class Form_Pacientes : Form
	{
		public Form_Pacientes()
		{
			InitializeComponent();
		}

		MySqlConnection conex = new MySqlConnection("Server = 127.0.0.1; User=root; Database=BBDDLABORATORIOcintiadiaz;password='';");

		private void Form_Pacientes_Load(object sender, EventArgs e)
		{
			conex.Open();
			DataTable tabla_PERFILES = new DataTable();
			MySqlDataAdapter sentencia = new MySqlDataAdapter("select * from pacientes_cintia_diaz;", conex);
			tabla_PERFILES.Clear();
			sentencia.Fill(tabla_PERFILES);
			dataGridView1.DataSource = tabla_PERFILES;
			conex.Close();

			button1.Enabled = false;
			button2.Enabled = true;
			button3.Enabled = false;
			button4.Enabled = false;
			textBox2.Enabled = false;
			textBox3.Enabled = true;
			textBox4.Enabled = false;
		}

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





		private void button1_Click(object sender, EventArgs e)
		{
			if (rutValido(textBox1.Text))
			{
				conex.Open();
				DataTable tabla_perfiles = new DataTable();
				string sqlinsertar = "insert into pacientes_cintia_diaz (rut,nombre,ApPat,ApMat) values  ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";
				MySqlDataAdapter sentencia = new MySqlDataAdapter(sqlinsertar, conex);
				tabla_perfiles.Clear();
				sentencia.Fill(tabla_perfiles);
				textBox1.Text = "";
				textBox2.Text = "";
				textBox3.Text = "";
				textBox4.Text = "";
				conex.Close();
				MessageBox.Show("Paciente agregado Exitosamente");
			}
		}

	

		

		

		private void button5_Click_1(object sender, EventArgs e)
		{
			if (rutValido(textBox1.Text))
			{
				conex.Open();
				DataTable tabla_transito = new DataTable();
				MySqlDataAdapter sentencia = new MySqlDataAdapter("select * from pacientes_cintia_diaz where rut like '%" + textBox1.Text + "%' ", conex);
				tabla_transito.Clear();
				sentencia.Fill(tabla_transito);
				conex.Close();

				int total = tabla_transito.Rows.Count;
				if (total >= 1)
				{
					for (int i = 0; i < total; i++)
					{
						textBox1.Text = tabla_transito.Rows[i]["rut"].ToString();
						textBox2.Text = tabla_transito.Rows[i]["nombre"].ToString();
						textBox3.Text = tabla_transito.Rows[i]["ApPat"].ToString();
						textBox4.Text = tabla_transito.Rows[i]["ApMat"].ToString();
					}
					MessageBox.Show("Usuario ya registrado");
					tabla_transito.Rows.Clear();
					return;
				}
				else
				{
					textBox2.Text = "";
					textBox3.Text = "";
					textBox4.Text = "";

					button1.Enabled = true;
					button2.Enabled = true;
					button3.Enabled = true;
					button4.Enabled = true;
					button5.Enabled = false;
					textBox2.Enabled = true;
					textBox3.Enabled = true;
					textBox4.Enabled = true;

					conex.Close();
				}
			}
		}

		private void button2_Click_1(object sender, EventArgs e)
		{
			//Búsqueda por apellido paterno con un textbox
			conex.Open();
			DataTable tabla_PERFILES = new DataTable();
			MySqlDataAdapter sentencia = new MySqlDataAdapter("select * from pacientes_cintia_diaz where ApPat like '%" + textBox3.Text + "%'", conex);
			tabla_PERFILES.Clear();
			sentencia.Fill(tabla_PERFILES);
			dataGridView1.DataSource = tabla_PERFILES;
			conex.Close();
		}

		private void button3_Click_1(object sender, EventArgs e)
		{
			//Actualizacion (modifica valores) de datos a partir de los textbox
			conex.Open();
			DataTable tabla_PERFILES = new DataTable();
			MySqlDataAdapter sentencia = new MySqlDataAdapter("update pacientes_cintia_diaz set nombre ='" + textBox2.Text + "', ApPat ='" + textBox3.Text + "', ApMat ='" + textBox4.Text + "',  where rut='" + textBox1.Text + "'; select * from pacientes_cintia_diaz;", conex);
			tabla_PERFILES.Clear();
			sentencia.Fill(tabla_PERFILES);
			dataGridView1.DataSource = tabla_PERFILES;
			conex.Close();
			textBox1.Text = "";
			textBox2.Text = "";
			textBox3.Text = "";
			textBox4.Text = "";
		}

		private void button4_Click_1(object sender, EventArgs e)
		{
			// muestra en los textbox y elimina segun la clave de la bbdd
			conex.Open();
			DataTable tabla_transito = new DataTable();
			string rut = textBox1.Text;
			MySqlDataAdapter sentencia = new MySqlDataAdapter
			("select * from pacientes_cintia_diaz where rut='" + rut + "'", conex);
			sentencia.Fill(tabla_transito);

			int total = tabla_transito.Rows.Count;
			if (total < 1)
			{
				MessageBox.Show("El Rut Ingresado NO EXISTE!!!");
				conex.Close();
			}
			else
			{
				for (int i = 0; i < total; i++)
				{
					textBox1.Text = tabla_transito.Rows[i]["rut"].ToString();
					textBox2.Text = tabla_transito.Rows[i]["nombre"].ToString();
					textBox3.Text = tabla_transito.Rows[i]["ApPat"].ToString();
					textBox4.Text = tabla_transito.Rows[i]["ApMat"].ToString();
				}

				MessageBox.Show("Datos encontrados, mostrados en Texbox y eliminados!!!");
				MySqlDataAdapter sentencia2 = new MySqlDataAdapter
				("delete from pacientes_cintia_diaz where rut='" + rut + "'", conex);
				tabla_transito.Clear();
				sentencia2.Fill(tabla_transito);
			}
		}
	}
}
