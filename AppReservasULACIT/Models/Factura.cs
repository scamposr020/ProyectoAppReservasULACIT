using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Factura
    {
        public int FACT_CODIGO { get; set; }
        public DateTime FACT_FEC_RENT { get; set; }
        public DateTime FACT_FEC_DEVOLU { get; set; }
        public double FACT_MONTO_TOT { get; set; }
        public int EMP_CODIGO { get; set; }
        public int SUC_CODIGO { get; set; }
        public int USU_CODIGO { get; set; }
        public int ORD_CODIGO { get; set; }
    }
}