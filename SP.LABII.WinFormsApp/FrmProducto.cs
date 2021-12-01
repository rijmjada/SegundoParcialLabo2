using System;
using System.Windows.Forms;
using EntidadesSP;

namespace SP.LABII.WinFormsApp
{
    public partial class FrmProducto : Form
    {
        private EntidadesSP.Producto miProducto;

        public EntidadesSP.Producto MiProducto
        {
            get { return this.miProducto; }
        }

        public FrmProducto()
        {
            InitializeComponent();

        }
        public FrmProducto(Producto producto) :this()
        {
            this.txtMarca.Text = producto.Marca;
            this.txtCodigo.Text = producto.Codigo.ToString();
            this.txtTipo.Text = producto.Tipo;
            this.txtPrecio.Text = producto.Precio.ToString();
        }

        /// Crar una instancia de tipo Producto
        /// Establecer, como valor de la propiedad, el atributo miProducto
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(this.txtCodigo.Text) && !String.IsNullOrEmpty(this.txtMarca.Text) &&
               !String.IsNullOrEmpty(this.txtTipo.Text) && !String.IsNullOrEmpty(this.txtPrecio.Text))
            {
                string marca = this.txtMarca.Text;
                string tipo = this.txtTipo.Text;
                int codigo = Convert.ToInt32(this.txtCodigo.Text);
                double precio = Convert.ToDouble(this.txtPrecio.Text);

                miProducto = new Producto(tipo, marca, codigo, precio);
            }
            
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
