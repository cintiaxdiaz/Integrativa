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
        CONEXION C = new CONEXION();

        public Form_resultados(){
            InitializeComponent();
        }

        private void Form_resultados_Load(object sender, EventArgs e) {
            C.conectar();
            C.llenar_rut(cbx_rut);
            C.llenar_cod_med(cbx_m);
            C.llenar_cod_tec(cbx_t);
            C.llenar_diag(cbx_d1);
            C.llenar_diag(cbx_d2);
            C.llenar_origen(cbx_o);
            C.desconectar();
        }
    }
}

