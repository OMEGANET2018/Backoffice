﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObackOffice.Models.Comun
{
    public class Persona
    {
        public int PersonaId { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int GeneroId { get; set; }
        public string NroDocumento { get; set; }
        public int TipoDocumentoId { get; set; }
        public string CorreoElectronico { get; set; }
        public string NumeroCelular { get; set; }
        public int EsEliminado { get; set; }
        public int? UsuGraba { get; set; }
        public DateTime? FechaGraba { get; set; }
        public int? UsuActualiza { get; set; }
        public DateTime? FechaActualiza { get; set; }
    }
}