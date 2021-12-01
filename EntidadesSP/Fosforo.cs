using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesSP
{
    public class Fosforo : Producto
    {
        public Fosforo()
        {
        }

        public Fosforo(string tipo, string marca, int codigo, double precio) : base(tipo, marca, codigo, precio)
        {
        }

        public override string ToString()
        {
            StringBuilder sw = new StringBuilder();

            sw.AppendLine(base.ToString());


            return sw.ToString();
        }
    }
}
