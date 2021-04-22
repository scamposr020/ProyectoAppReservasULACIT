using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Sucursal
    {
        public int SUC_CODIGO { get; set; }
        public string SUC_NOMBRE { get; set; }
        public string SUC_TELEFONO { get; set; }
        public string SUC_EMAIL { get; set; }
        public int RENTAC_CODIGO { get; set; }
        public int DIREC_CODIGO { get; set; }
    }
}