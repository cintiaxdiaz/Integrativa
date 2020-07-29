using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabClinico_9418202 {
    class USUARIO {
        public string rut;
        public string clave;
        public int nivel;

        public USUARIO(string Rut, int Nivel) {
            rut = Rut;
            nivel = Nivel;
        }

        public USUARIO(string Rut, int Nivel, string Clave) {
            rut = Rut;
            clave = Clave;
            nivel = Nivel;
        }

        public USUARIO(string Rut) {
            rut = Rut;
        }

        public bool is_admin() {

            return nivel == 1;
        }

    }
}
