using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Vehiculo
    {
        public int VEH_CODIGO { get; set; }
        public string VEH_MARCA { get; set; }
        public string VEH_TIPO { get; set; }
        public int VEH_CANTI_PUERTAS { get; set; }
        public string VEH_COMBUSTIBLE { get; set; }
        public string VEH_COLOR { get; set; }
        public string VEH_MODELO { get; set; }
        public int VEH_ANO { get; set; }
        public int SUC_CODIGO { get; set; }
    }
}