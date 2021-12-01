using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace EntidadesSP
{
    public static class Archivo
    {
     
        public static string NewFile()
        {
            string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PARCIAL");

            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }

            return ruta = Path.Combine(ruta, "Diego.Ormeño.zapato.xml");
        }
    }
   


    public class Zapato : Producto , Iserializa, IDeserializa
    {

        #region Constructores
        public Zapato()
        {
        }

        public Zapato(string tipo, string marca, int codigo, double precio) : base(tipo, marca, codigo, precio)
        {
        } 

        #endregion

        public string Path => Archivo.NewFile();

        public override string ToString()
        {
            StringBuilder sw = new StringBuilder();

            sw.AppendLine(base.ToString());
            

            return sw.ToString();
        }

        public bool Xml()
        {
            bool ret = true;

            try
            {
                using (XmlTextWriter writer = new XmlTextWriter(this.Path, System.Text.Encoding.UTF8))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Zapato));

                    serializer.Serialize(writer, this);
                }

            }
            catch (Exception)
            {
                ret = false;
            }

            return ret;
        }

        public bool Xml(out Zapato zapato)
        {
            bool ret = true;

            try
            {
                using (XmlTextReader read = new XmlTextReader(this.Path))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(Zapato));

                    zapato = (Zapato)ser.Deserialize(read);
                }

            }
            catch (Exception)
            {
                ret = false;
                zapato = null;
            }
            return ret;
        }
    }

  
}
