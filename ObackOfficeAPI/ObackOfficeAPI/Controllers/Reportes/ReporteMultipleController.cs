﻿using BE.Comun;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Hosting;
using System.Web.Http;
using System.IO;
using Newtonsoft.Json;

namespace ObackOfficeAPI.Controllers.Reportes
{
    public class ReporteMultipleController : ApiController
    {
        private ReporteMultipleRepository rmr = new ReporteMultipleRepository();

        [HttpPost]
        public IHttpActionResult BandejaReporteMultiple(BandejaReporteMultiple data)
        {
            BandejaReporteMultiple response = rmr.BandejaReporteMultiple(data);
            return Ok(response);
        }

        [HttpGet]
        public IHttpActionResult Chart(int SedeId, int EventoId, int CursoId, string NombreEmpleado, string DNIEmpleado, string Action)
        {
            if (SedeId == 0 || EventoId == 0 || CursoId == 0 || string.IsNullOrWhiteSpace(Action))
                return BadRequest("Alguno de los parámetros es incorrecto");

            BandejaReporteMultiple data = new BE.Comun.BandejaReporteMultiple()
            {
                SedeId = SedeId,
                EventoId = EventoId,
                CursoId = CursoId,
                NombreEmpleado = NombreEmpleado,
                DNIEmpleado = DNIEmpleado
            };

            switch (Action)
            {
                case "Asistencia":
                    {
                        return Ok(Convert.ToBase64String(rmr.ChartAsistencia(data)));
                    }
                case "Aprobados":
                    {
                        return Ok(Convert.ToBase64String(rmr.ChartAprobados(data)));
                    }
                case "Promedios":
                    {
                        return Ok(Convert.ToBase64String(rmr.ChartPromedio(data)));
                    }
            }

            return BadRequest("No se encontró la acción dentro del controlador.");
        }

        [HttpPost]
        public IHttpActionResult BandejaReporteMultipleExcel(MultiDataModel multi)
        {
            string fullPath = HostingEnvironment.MapPath(@"~/Plantillas Excel/Plantilla Reporte Academico.xlsx");
            FileStream TemplateFile = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            BandejaReporteMultiple data = JsonConvert.DeserializeObject<BandejaReporteMultiple>(multi.String1);

            MemoryStream response = rmr.BandejaReporteMultipleExcel(data, TemplateFile);

            return Ok(response);
        }
    }
}
