using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Inventario
    {
        public int INV_CODIGO { get; set; }
        public int INV_CANT_TOTAL { get; set; }
        public int INV_CANT_DISPONIBLE { get; set; }
        public int INV_CANT_RENTADOS { get; set; }
        public int SUC_CODIGO { get; set; }
        public int VEH_CODIGO { get; set; }
    }
}