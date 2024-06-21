using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaTecnicaAlmarchivos.Models
{
    public class USUARIO
    {
        public int IdUsuario { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ConfirmarClave { get; set; }

    }
}