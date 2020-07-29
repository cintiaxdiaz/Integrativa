using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabClinico_9418202
{
    public partial class Form_UsuarioNivel_1 : Form
    {
        public Form_UsuarioNivel_1()
        {
            InitializeComponent();
        }

        private void UsuarioNivel_1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_resultados Form_resultados = new Form_resultados();
            Form_resultados.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_Pacientes Form_Pacientes = new Form_Pacientes();
            Form_Pacientes.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form_Diagnosticos Form_Diagnosticos = new Form_Diagnosticos();
            Form_Diagnosticos.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form_CentrosMedicos Form_CentrosMedicos = new Form_CentrosMedicos();
            Form_CentrosMedicos.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form_Medicos Form_Medicos = new Form_Medicos();
            Form_Medicos.Show();
        }
    }
}
