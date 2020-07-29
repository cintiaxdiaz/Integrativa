using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

namespace LabClinico_9418202
{
    public partial class Form_Ingreso : Form
    {
        public Form_Ingreso()
        {
            InitializeComponent();
        }
        MySqlConnection conex = new MySqlConnection("Server = 127.0.0.1; User=root; Database=BBDDLABORATORIOcintiadiaz;password='';");
        private void button1_Click(object sender, EventArgs e)
        {
			{//valida que el ingreso realizado por rut y clave se encuentren en la bbdd
				conex.Open();
				DataTable tabla_transito = new DataTable();
				string clave = textBox1.Text;
				MySqlDataAdapter sentencia = new MySqlDataAdapter("select * from usuario_cintia_diaz where clave='" + clave + "' and rut ='" + textBox2.Text + "'", conex);
				tabla_transito.Clear();
				sentencia.Fill(tabla_transito);
				int total = tabla_transito.Rows.Count;
				if (total < 1)
				{
					conex.Close();
					MessageBox.Show("Clave o usuario inválido");
					return;
				}
				//valida al ingreso si es usuario o administrador
				string rut_tabla = tabla_transito.Rows[0]["rut"].ToString();
				int nivel_tabla = Int32.Parse(tabla_transito.Rows[0]["Nivel"].ToString());
				USUARIO usua = new USUARIO(rut_tabla, nivel_tabla, clave);
				Form_UsuarioNivel_1 UN_1 = new Form_UsuarioNivel_1();
				UN_1.Show();
				this.Hide();
				MessageBox.Show("BIENVENIDO, QUE TENGAS UN EXCELENTE DÍA!");
				conex.Close();
			}

			bool rutValido(string rut)
			{//valida el digito verificador sea correcto 

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
}
