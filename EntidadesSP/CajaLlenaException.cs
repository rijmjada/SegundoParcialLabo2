using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesSP
{
    public class CajaLlenaException : Exception
    {
        public CajaLlenaException() : base("Caja llena")
        {

        }
    }

    public static class Extension
    {
        public static string InformarNovedad(this CajaLlenaException exception)
        {
            return exception.Message.ToString();
        }
    }
}


