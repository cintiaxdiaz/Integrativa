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
    public partial class Form_Ingreso : Form
    {
        public Form_Ingreso()
        {
            InitializeComponent();
        }
        MySqlConnection conex = new MySqlConnection("Server = 127.0.0.1; User=root; Database=BBDDLABORATORIOcintiadiaz;password='';");
        private void button1_Click(object sender, EventArgs e)
        {
            
            Form_UsuarioNivel_1 UsuarioNivel_1 = new Form_UsuarioNivel_1();
            UsuarioNivel_1.Show();
            this.Hide();
        }
    }
}
