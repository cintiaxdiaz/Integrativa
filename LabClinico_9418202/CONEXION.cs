using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace LabClinico_9418202 {
    class CONEXION {
        MySqlConnection conex;
        MySqlCommand comando;
        MySqlDataReader data_reader;
        MySqlDataAdapter data_adapter;
        DataTable dt;


        public void llenargrid_diagnosticos_cintia_diaz(DataGridView grid)
        {
            MySqlCommand cm = new MySqlCommand("select * from diagnosticos_cintia_diaz", conex);
            MySqlDataAdapter da = new MySqlDataAdapter(cm);
            DataTable diagnosticos_cintia_diaz = new DataTable();
            da.Fill(diagnosticos_cintia_diaz);
            grid.DataSource = diagnosticos_cintia_diaz;


        }

        public void medicos_cintia_diaz(DataGridView grid)
        {
            MySqlCommand cm = new MySqlCommand("select * from medicos_cintia_diaz", conex);
            MySqlDataAdapter da = new MySqlDataAdapter(cm);
            DataTable medicos_cintia_diaz = new DataTable();
            da.Fill(medicos_cintia_diaz);
            grid.DataSource = medicos_cintia_diaz;


        }

        

        public CONEXION() {
            try {
                conex = new MySqlConnection("Server = 127.0.0.1; User=root; Database=BBDDLABORATORIOcintiadiaz;password='';");
                conex.Open();
            }
            catch (Exception ex) {
                //string salida = "No se conecto a los registros: " + ex.ToString();
                MessageBox.Show("No se conecto a los registros: " + ex.ToString());
            }
        }

        public void conectar() {
            try {
                conex = new MySqlConnection("Server = 127.0.0.1; User=root; Database=BBDDLABORATORIOcintiadiaz;password='';");
                conex.Open();
            }
            catch (Exception ex) {
                MessageBox.Show("No se conecto a los registros: " + ex.ToString());
            }
        }

        public void desconectar() {
            try {
                conex.Close();
            }
            catch (Exception) {
                throw;
            }
        }

        public string insertarDatos(string rut , int edad, string fecha_diag, string diag1, string diag2, string origen, string cod_med, string cod_tec) {
            string salida = "Se registro exitosamente";
            try {
                comando = new MySqlCommand("insert into resultados_cintia_diaz(rut, edad, feachadiag, diagnostico1, diagnostico2, origen, codmedico, codtecnologo) values ('" + rut + "'," + edad + ",'" + fecha_diag + "','" + diag1 + "','" + diag2 + "','" + cod_med + "','" + cod_tec + "')", conex);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex) {
                salida = "No se realizo el registro: " + ex.ToString();
                conex.Close();
            }
            return salida;
        }

        public int DiagnosticosRegistrados(int numero) {
            int contador = 0;
            try {
                comando = new MySqlCommand("Select * from resultados_cintia_diaz where num=" + numero + "", conex);
                data_reader = comando.ExecuteReader();
                while (data_reader.Read()) {
                    contador++;
                }
                data_reader.Close();
            }
            catch (Exception ex) {
                //string salida = "No se pudo completar la consulta: " + ex.ToString();
                MessageBox.Show("No se pudo completar la consulta: " + ex.ToString());
            }
            return contador;
        }

        public void CargarDatagrid(DataGridView dgv) {
            try {
                data_adapter = new MySqlDataAdapter("select * from resultados_cintia_diaz", conex);
                dt = new DataTable();
                data_adapter.Fill(dt);
                dgv.DataSource = dt;
            }
            catch (Exception ex) {
                //string salida = "Error al consultar tabla: " + ex.ToString();
                MessageBox.Show("Error al consultar tabla: " + ex.ToString());
            }
        }

        public void llenar_rut(ComboBox cbx) {
            try {
                comando = new MySqlCommand("Select rut from pacientes_cintia_diaz", conex);
                data_reader = comando.ExecuteReader();
                while (data_reader.Read()) {
                    cbx.Items.Add(data_reader["rut"].ToString());
                }
                data_reader.Close();
            }
            catch (Exception ex) {
                MessageBox.Show("No se lleno el ComboBox: " + ex.ToString());
                conex.Close();
            }
        }

        public void llenar_origen(ComboBox cbx) {
            try {
                comando = new MySqlCommand("Select rut from centrosmedicos_cintia_diaz", conex);
                data_reader = comando.ExecuteReader();
                while (data_reader.Read()) {
                    cbx.Items.Add(data_reader["origen"].ToString());
                }
                data_reader.Close();
            }
            catch (Exception ex) {
                MessageBox.Show("No se lleno el ComboBox: " + ex.ToString());
                conex.Close();
            }
        }

        public void llenar_cod_tec(ComboBox cbx) {
            try {
                comando = new MySqlCommand("Select rut from medicos_cintia_diaz", conex);
                data_reader = comando.ExecuteReader();
                while (data_reader.Read()) {
                    cbx.Items.Add(data_reader["codmed"].ToString());
                }
                data_reader.Close();
            }
            catch (Exception ex) {
                MessageBox.Show("No se lleno el ComboBox: " + ex.ToString());
                conex.Close();
            }
        }

        public void llenar_cod_med(ComboBox cbx) {
            try {
                comando = new MySqlCommand("Select rut from medicos_cintia_diaz", conex);
                data_reader = comando.ExecuteReader();
                while (data_reader.Read()) {
                    cbx.Items.Add(data_reader["codmed"].ToString());
                }
                data_reader.Close();
            }
            catch (Exception ex) {
                MessageBox.Show("No se lleno el ComboBox: " + ex.ToString());
                conex.Close();
            }
        }

        public void llenar_diag(ComboBox cbx) {
            try {
                comando = new MySqlCommand("Select rut from diagnosticos_cintia_diaz", conex);
                data_reader = comando.ExecuteReader();
                while (data_reader.Read()) {
                    cbx.Items.Add(data_reader["codigo"].ToString());
                }
                data_reader.Close();
            }
            catch (Exception ex) {
                MessageBox.Show("No se lleno el ComboBox: " + ex.ToString());
                conex.Close();
            }
        }

        public void llenarCamposRegist(int numero, ComboBox cbx_rut, ComboBox cbx_f_d, ComboBox cbx_o, ComboBox cbx_m, ComboBox cbx_d1, ComboBox cbx_t, ComboBox cbx_d2) {
            try {
                comando = new MySqlCommand("Select * from resultados_cintia_diaz where num=" + numero + "", conex);
                data_reader = comando.ExecuteReader();
                if (data_reader.Read()) {
                    
                }
                data_reader.Close();
            }
            catch (Exception ex) {
                MessageBox.Show("No se pudo llenar los campos: " + ex.ToString());
            }
        }
    }
}

