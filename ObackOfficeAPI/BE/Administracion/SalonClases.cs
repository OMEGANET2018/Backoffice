﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Administracion
{
    [Table("tblSalonClases")]
    public class SalonClases
    {
        [Key]
        [Column(Order = 1)]
        public int SalonClaseId { get; set; }
        public int SalonProgramadoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int EsEliminado { get; set; }
        public int? UsuGraba { get; set; }
        public DateTime? FechaGraba { get; set; }
        public int? UsuActualiza { get; set; }
        public DateTime? FechaActualiza { get; set; }
    }

    public class ClasesProgramada
    {
        public DateTime FechaInicioClase { get; set; }
        public DateTime FechafinClase { get; set; }
    }
}
