using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Direccion
    {
        public int DIREC_CODIGO { get; set; }
        public string DIREC_PROVIN { get; set; }
        public string DIREC_DISTRI { get; set; }
        public string DIREC_CANTON { get; set; }
        public string DIREC_DETALLE { get; set; }
        public string DIREC_COD_POSTAL { get; set; }
    }
}