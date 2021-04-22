using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Rentacar
    {
        public int RENTAC_CODIGO { get; set; }
        public string RENTAC_NOMBRE { get; set; }
        public int RENTAC_CANT_SUC { get; set; }
        public string RENTAC_TIPO { get; set; }
        public string RENTAC_WEBPAGE { get; set; }
    }
}