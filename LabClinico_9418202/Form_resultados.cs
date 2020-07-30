using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Data.Sql;
using System.Data.SqlClient;

namespace LabClinico_9418202
{
    public partial class Form_resultados : Form
    {
        //CONEXION C = new CONEXION();
        MySqlConnection conex = new MySqlConnection("Server = 127.0.0.1; User=root; Database=BBDDLABORATORIOcintiadiaz;password='';");

        public Form_resultados(){
            InitializeComponent();
        }

        void llenar_origen() {
            MySqlCommand comando = new MySqlCommand("Select * from centrosmedicos_cintia_diaz", conex);
            MySqlDataReader data_reader = comando.ExecuteReader();
            while (data_reader.Read()) {
                cbx_o.Items.Add(data_reader["origen"].ToString());
            }
            data_reader.Close();
        }

        void llenar_rut() {
            MySqlCommand comando = new MySqlCommand("Select * from pacientes_cintia_diaz", conex);
            MySqlDataReader data_reader = comando.ExecuteReader();
            while (data_reader.Read()) {
                cbx_rut.Items.Add(data_reader["rut"].ToString());
            }
            data_reader.Close();
        }

        void llenar_cod_medicos() {
            MySqlCommand comando = new MySqlCommand("Select * from medicos_cintia_diaz", conex);
            MySqlDataReader data_reader = comando.ExecuteReader();
            while (data_reader.Read()) {
                if ((data_reader["tipo"].ToString()) == "Tecnologo") {
                    cbx_t.Items.Add(data_reader["codmed"].ToString());
                }
                else {
                    cbx_m.Items.Add(data_reader["codmed"].ToString());
                }
            }
            data_reader.Close();
        }

        void llenar_diagnostico() {
            MySqlCommand comando = new MySqlCommand("Select * from diagnosticos_cintia_diaz", conex);
            MySqlDataReader data_reader = comando.ExecuteReader();
            while (data_reader.Read()) {
                cbx_d1.Items.Add(data_reader["codigo"].ToString());
                cbx_d2.Items.Add(data_reader["codigo"].ToString());
            }
            data_reader.Close();
        }

        private void Form_resultados_Load(object sender, EventArgs e) {
            conex.Open(); 
            llenar_origen();
            llenar_rut();
            llenar_cod_medicos();
            llenar_diagnostico();
            cbx_d1.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_d2.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_m.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_o.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_rut.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_t.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btn_ingresar_Click(object sender, EventArgs e) { 
            DataTable tabla_aux = new DataTable();
            string fecha = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            string sqlinsertar = "insert into resultados_cintia_diaz (rut, feachadiag, diagnostico1, diagnostico2, origen, codmedico, codtecnologo) VALUES  ('" + cbx_rut.Text + "','" + fecha + "','" + cbx_d1.Text + "','" + cbx_d2.Text + "','" + cbx_o.Text + "','" + cbx_m.Text + "','" + cbx_t.Text + "')";
            MySqlDataAdapter sentencia = new MySqlDataAdapter(sqlinsertar, conex);
            tabla_aux.Clear();
            sentencia.Fill(tabla_aux);
            cbx_d1.Text = null;
            cbx_d2.Text = null;
            cbx_m.Text = null;
            cbx_o.Text = null;
            cbx_rut.Text = null;
            cbx_t.Text = null;
            dateTimePicker1.Text = null;
            MessageBox.Show("Resultado de examenes agregados exitosamente");
        }

        private void btn_modificar_Click(object sender, EventArgs e) {
            DataTable tabla_aux = new DataTable();
            string fecha = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            MySqlDataAdapter sentencia = new MySqlDataAdapter("update resultados_cintia_diaz set rut ='" + cbx_rut.Text + "', fechadiag ='" + fecha + "', diagnostico1 ='" + cbx_d1.Text + "', diagnostico2 ='" + cbx_d2.Text + "', origen ='" + cbx_o + "', codmedico ='" + cbx_m.Text + "', codtecnologo ='" + cbx_t.Text + "',  where num=" + txt_numero.Text + ";", conex);
            //MySqlDataAdapter sentencia2 = new MySqlDataAdapter("select * from resultados_cintia_diaz; ", conex);
            tabla_aux.Clear();
            sentencia.Fill(tabla_aux);
            dataGridView1.DataSource = tabla_aux;
            txt_numero.Text = "";
            cbx_d1.Text = null;
            cbx_d2.Text = null;
            cbx_m.Text = null;
            cbx_o.Text = null;
            cbx_rut.Text = null;
            cbx_t.Text = null;
            dateTimePicker1.Text = null;
            MessageBox.Show("Resultado de examenes agregados exitosamente");
        }

        private void btn_eliminar_Click(object sender, EventArgs e) {
            DataTable tabla_transito = new DataTable();
            MySqlDataAdapter sentencia = new MySqlDataAdapter("delete from resultados_cintia_diaz where num=" + txt_numero.Text + "", conex);
            tabla_transito.Clear();
            sentencia.Fill(tabla_transito);
            txt_numero.Text = "";
            cbx_d1.Text = null;
            cbx_d2.Text = null;
            cbx_m.Text = null;
            cbx_o.Text = null;
            cbx_rut.Text = null;
            cbx_t.Text = null;
            dateTimePicker1.Text = null;
            MessageBox.Show("Resultado de examenes agregados exitosamente");
        }

        private void btn_buscar_num_Click(object sender, EventArgs e) {
            DataTable tabla_transito = new DataTable();
            string NUMERO = txt_numero.Text;
            MySqlDataAdapter sentencia = new MySqlDataAdapter
            ("select * from resultados_cintia_diaz where num='" + NUMERO + "'", conex);
            sentencia.Fill(tabla_transito);

            int total = tabla_transito.Rows.Count;
            if (total < 1) {
                MessageBox.Show("El NUMERO DE REGISTRO NO EXISTE!!!");
                conex.Close();
            }
            else {
                for (int i = 0; i < total; i++) {
                    cbx_d1.Text = tabla_transito.Rows[i]["diagnostico1"].ToString();
                    cbx_d2.Text = tabla_transito.Rows[i]["diagnostico2"].ToString();
                    cbx_m.Text = tabla_transito.Rows[i]["codmedico"].ToString();
                    cbx_t.Text = tabla_transito.Rows[i]["codtecnologo"].ToString();
                    cbx_o.Text = tabla_transito.Rows[i]["origen"].ToString();
                    cbx_rut.Text = tabla_transito.Rows[i]["rut"].ToString();
                    dataGridView1.DataSource = tabla_transito;
                }
            }
        }
    }
}

