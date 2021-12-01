using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EntidadesSP
{
    public delegate void MarcaExistenteDelegate(object obj,EventArgs eventArgs);
    
    public class ADO
    {
        public event MarcaExistenteDelegate MarcaExistenteEventArgs;

        static SqlCommand comando;
        static SqlConnection conexion;

        static ADO()
        {

            SqlConnectionStringBuilder strConexion = new SqlConnectionStringBuilder();
            strConexion.DataSource = @"DIEGO-PC\SQLEXPRESS";
            strConexion.InitialCatalog = @"almacen_db";
            strConexion.IntegratedSecurity = true;


            comando = new SqlCommand();
            conexion = new SqlConnection(strConexion.ToString());
            comando.Connection = conexion;
            comando.CommandType = System.Data.CommandType.Text;

        }

        public static List<Producto> ObtenerTodos(string marcaref)
        {
            List<Producto> lista = new List<Producto>();
            
            try
            {
                comando.Parameters.Clear();
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                
                comando.CommandText = $"SELECT * FROM productos WHERE @marca = marca";
                comando.Parameters.AddWithValue("@marca", marcaref);
                SqlDataReader dataReader = comando.ExecuteReader();

                while (dataReader.Read())
                {
                    // Obtengo los datos.
                    string marca = dataReader["marca"].ToString();
                    string tipo = dataReader["tipo"].ToString();
                    int codigo = Convert.ToInt32(dataReader["codigo"]);
                    double precio = Convert.ToDouble(dataReader["codigo"]);

                    // Creo el objecto con los datos.
                    Producto p1 = new Producto(tipo, marca, codigo, precio);

                    // Agrego a la lista.
                    lista.Add(p1);
                }
            }
            catch (Exception e)
            {
                string error = e.Message.ToString();
            }
            finally
            {
                conexion.Close();
            }

            return lista;
        }

        public static List<Producto> ObtenerTodos()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                comando.Parameters.Clear();
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                comando.CommandText = "SELECT * FROM productos";
                SqlDataReader dataReader = comando.ExecuteReader();

                while (dataReader.Read())
                {
                    // Obtengo los datos.
                    string marca = dataReader["marca"].ToString();
                    string tipo = dataReader["tipo"].ToString();
                    int codigo = Convert.ToInt32(dataReader["codigo"]);
                    double precio = Convert.ToDouble(dataReader["codigo"]);

                    // Creo el objecto con los datos.
                    Producto p1 = new Producto(tipo, marca, codigo, precio);

                    // Agrego a la lista.
                    lista.Add(p1);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                conexion.Close();
            }

            return lista;
        }

        public bool Agregar(Producto producto)
        {
            bool ret = true;

            try
            {
                comando.Parameters.Clear();
                conexion.Open();
                comando.CommandText = $"INSERT INTO productos (marca, tipo, codigo, precio)   " +
                    $"                  VALUES (@marca,@tipo,@codigo,@precio) ";

                comando.Parameters.AddWithValue("@marca", producto.Marca);
                comando.Parameters.AddWithValue("@tipo", producto.Tipo);
                comando.Parameters.AddWithValue("@codigo", producto.Codigo);
                comando.Parameters.AddWithValue("@precio", producto.Precio);

                comando.ExecuteNonQuery();

                List<Producto> list = ObtenerTodos(producto.Marca);

                if (list.Count > 1)
                {
                    this.MarcaExistenteEventArgs.Invoke(list, EventArgs.Empty);
                }

            }

            catch (Exception)
            {
                ret = false;
            }

            finally
            {
                conexion.Close();
            }

            return ret;
        }

        public bool Modificar(Producto producto)
        {
            bool ret = true;
            int codigoProductoRef = producto.Codigo;
            try
            {
                comando.Parameters.Clear();
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
               
                comando.CommandText = $"UPDATE productos SET marca = @marca , tipo = @tipo , precio = @precio , codigo = @codigo  WHERE codigo = {codigoProductoRef} ";


                comando.Parameters.AddWithValue("@marca", producto.Marca);
                comando.Parameters.AddWithValue("@tipo", producto.Tipo);
                comando.Parameters.AddWithValue("@codigo", producto.Codigo);
                comando.Parameters.AddWithValue("@precio", producto.Precio);


                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                ret = false;
            }

            finally
            {
                conexion.Close();
            }

            return ret;
        }

        public bool Eliminar(Producto producto)
        {
            bool ret = true;
            int codigoProductoRef = producto.Codigo;

            try
            {
                comando.Parameters.Clear();
                conexion.Open();
                comando.CommandText = $"DELETE FROM productos WHERE codigo = {codigoProductoRef}";
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                ret = false;
            }
            finally
            {
                conexion.Close();
            }

            return ret;
        }

       
    }
}
