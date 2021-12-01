using System;
using System.Text;

namespace EntidadesSP
{
    public class Producto
    {

        #region Atributos

        protected string marca;
        protected string tipo;
        protected int codigo;
        protected double precio;

        #endregion

        #region Propiedades

        public string Marca
        {
            get { return this.marca; }
            set { this.marca = value; }
        }

        public string Tipo
        {
            get { return this.tipo; }
            set { this.tipo = value; }
        }

        public int Codigo
        {
            get { return this.codigo; }
            set { this.codigo = value; }
        }

        public double Precio
        {
            get { return this.precio; }
            set { this.precio = value; }
        }

        #endregion

        #region Constructores

        public Producto(string tipo, string marca, int codigo, double precio)
        {
            this.Marca = marca;
            this.Tipo = tipo;
            this.Codigo = codigo;
            this.Precio = precio;
        }
        public Producto()
        {
         
        }

        #endregion

        #region Metodos

        public override string ToString()
        {
            StringBuilder sw = new StringBuilder();

            sw.AppendLine("Marca: " + this.Marca);
            sw.AppendLine("Tipo: " + this.Tipo);
            sw.AppendLine("Codigo: " + this.Codigo);
            sw.AppendLine("Precio: " + this.Precio);

            return sw.ToString();

        }

        #endregion

    }
}


