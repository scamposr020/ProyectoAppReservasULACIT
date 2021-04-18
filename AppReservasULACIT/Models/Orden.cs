using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Orden
    {
        public int ORD_CODIGO { get; set; }
        public DateTime ORD_FEC_SOLI { get; set; }
        public int ORD_DIAS_RENT { get; set; }
        public double ORD_MONTO_DIA { get; set; }
        public int VEH_CODIGO { get; set; }
    }
}