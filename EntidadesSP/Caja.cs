using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * ///Si el precio total de la caja supera los 999 pesos, se disparará el evento EventoPrecio. 
        ///Diseñarlo (de acuerdo a las convenciones vistas) en la clase caja. 
        ///Adaptar la sobrecarga del operador +, para que lance el evento, según lo solicitado.
        ///Crear el manejador necesario para que, una vez capturado el evento, se imprima en un archivo de texto: 
        ///la fecha (con hora, minutos y segundos) y el total de la caja (en un nuevo renglón). 
        ///Se deben acumular los mensajes. 
        ///El archivo se guardará con el nombre 'facturas.log' en la carpeta 'Mis documentos' del cliente.
        ///El manejador de eventos (Caja_EventoPrecio) invocará al método (de clase) 
        ///ImprimirFactura(Caja<T>) (se alojará en la clase Facturadora<T>), que retorna un booleano 
        ///indicando si se pudo escribir o no.
        ///la clase Facturadora<T> sólo podrá 'facturar' Zapatos, Fósforos o Remedios.
 */

namespace EntidadesSP
{
    public delegate void LimitePrecio(object sender, EventArgs e);
    public class Caja<T> where T: Producto
    {
        public  event LimitePrecio EventoPrecio;

        #region Atributos

        protected int capacidad;
        protected List<T> elementos;

        #endregion

        #region Propiedades

        public List<T> Elementos
        {
            get { return this.elementos; }
        }


        public double PrecioTotal
        {
            get
            {
                double precioAcu = 0;

                foreach(T item in Elementos)
                {
                    precioAcu += item.Precio;
                }

                return precioAcu;
            }
        }

        #endregion

        #region Constructor
        public Caja()
        {
            this.elementos = new List<T>();
        }

        public Caja(int capacidad) :this()
        {
            this.capacidad = capacidad;
        }

        #endregion

        #region Metodos

        public override string ToString()
        {
            StringBuilder sw = new StringBuilder();

            sw.AppendLine("Tipo caja: " + this.elementos.GetType().ToString());
            sw.AppendLine("Cantidad actual: " + this.elementos.Count);
            sw.AppendLine("Capacidad caja: " + this.capacidad);
            sw.AppendLine("Precio total: " + this.PrecioTotal);
            sw.AppendLine("Listado: ");

            foreach(T item in this.elementos)
            {
                sw.AppendLine(item.ToString());
            }

            return sw.ToString();
        }

        public static Caja<T> operator +(Caja<T> caja, T a)
        {
            if (caja.capacidad >= caja.elementos.Count)
            {
                caja.elementos.Add(a);
            }
            else
            {
                throw new CajaLlenaException();
            }

            if(caja.PrecioTotal > 999 && caja.EventoPrecio is not null)
            {
                caja.EventoPrecio.Invoke(caja, EventArgs.Empty);
            }

            return caja;
        }

   

        #endregion
    }

}
