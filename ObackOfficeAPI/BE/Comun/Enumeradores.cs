﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Comun
{
    public class Enumeradores
    {
        public enum GrupoParametros
        {
            Roles = 100,
            TipoDocumentos = 101,
            Generos = 102,
            TipoEmpresas = 104,
            PreguntasTaller = 105,
            Sedes = 106,
            Condición = 107,
            Colores = 108
        }

        public enum EsEliminado
        {
            No = 0,
            Si = 1
        }
    }
}
