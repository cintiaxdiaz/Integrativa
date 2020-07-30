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
            C.llenar_rut(cbx_rut);
        }
    }
}

