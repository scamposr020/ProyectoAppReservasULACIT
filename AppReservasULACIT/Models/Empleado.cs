using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Empleado
    {
        public int EMP_CODIGO { get; set; }
        public string EMP_CEDULA { get; set; }
        public string EMP_NOMBRE { get; set; }
        public string EMP_PUESTO { get; set; }
        public string EMP_TELEFONO { get; set; }
        public string EMP_EMAIL { get; set; }
        public int SUC_CODIGO { get; set; }
    }
}