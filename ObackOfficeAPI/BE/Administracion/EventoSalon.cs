﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Administracion
{
    [Table("tblEventoSalones")]
    public class EventoSalon
    {
        public int EventoSalonId { get; set; }
        public int EventoId { get; set; }
        public string Nombre { get; set; }
        public string Ubicación { get; set; }
        public int EsEliminado { get; set; }
        public int? UsuGraba { get; set; }
        public DateTime? FechaGraba { get; set; }
        public int? UsuActualiza { get; set; }
        public DateTime? FechaActualiza { get; set; }
    }
}
