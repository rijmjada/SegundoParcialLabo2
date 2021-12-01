using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EntidadesSP
{

    public static class Facturadora<T> where T: Producto
    {
        public static string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string archivo = Path.Combine(ruta, "facturas.log");
        public static bool ImprimirFactura(Caja<T> caja)
        {
            bool ret = true;
            
            try
            {
                using (StreamWriter sw = new StreamWriter(archivo, true))
                {
                    sw.WriteLine(DateTime.Now);
                    sw.WriteLine(caja.PrecioTotal);
                    sw.WriteLine("-----------------------------");
                }
            }
            catch(Exception)
            {
                ret = false;
            }

             return ret;
        }

    }


}
