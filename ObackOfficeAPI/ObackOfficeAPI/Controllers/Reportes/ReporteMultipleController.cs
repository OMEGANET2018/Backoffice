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
        public IHttpActionResult Chart(string Area, string Categoria, string Empresa, int Capacitador, int Condicion, int Asistencia, int SedeId, int EventoId, int CursoId, string NombreEmpleado, string Action)
        {
            if (SedeId == 0 || EventoId == 0 || CursoId == 0 || string.IsNullOrWhiteSpace(Action))
                return BadRequest("Alguno de los parámetros es incorrecto");

            BandejaReporteMultiple data = new BE.Comun.BandejaReporteMultiple()
            {
                Area = Area,
                Categoria = Categoria,
                Empresa = Empresa,
                CapacitadorId = Capacitador,
                SedeId = SedeId,
                EventoId = EventoId,
                CursoId = CursoId,
                NombreEmpleado = NombreEmpleado,
                Condicion = Condicion,
                Asistencia = Asistencia
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
                case "Faltas":
                    {
                        return Ok(Convert.ToBase64String(rmr.ChartFaltas(data)));
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


        [HttpPost]
        public IHttpActionResult DownloadFile(data data)
        {
            string fullPath ="";
            if (data.documento == "Diploma") fullPath = HostingEnvironment.MapPath(@"~/Plantillas Excel/diploma.pdf");
            else fullPath = HostingEnvironment.MapPath(@"~/Plantillas Excel/examen.pdf");

            byte[] binaryData;
            FileStream inFile = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            binaryData = new Byte[inFile.Length];
            inFile.Read(binaryData, 0, (int)inFile.Length);
            MemoryStream response = new MemoryStream();
            response.Write(binaryData, 0, (int)inFile.Length);
            
            return Ok(response);
        }

        [HttpPost]
        public IHttpActionResult DownloadFileValidacion()
        {
            string fullPath = HostingEnvironment.MapPath(@"~/Plantillas Excel/Validacion.xlsx");     

            byte[] binaryData;
            FileStream inFile = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            binaryData = new Byte[inFile.Length];
            inFile.Read(binaryData, 0, (int)inFile.Length);
            MemoryStream response = new MemoryStream();
            response.Write(binaryData, 0, (int)inFile.Length);

            return Ok(response);
        }

        [HttpGet]
        public IHttpActionResult GetAutocomplete(string campo, string valor)
        {
            List<string> data = new List<string>();
            switch (campo)
            {
                case "area":
                    {
                        data = rmr.GetAreaAutocomplete(valor);
                        break;
                    }
                case "categoria":
                    {
                        data = rmr.GetCategoriaAutocomplete(valor);
                        break;
                    }
                case "empresa":
                    {
                        data = rmr.GetEmpresaAutocomplete(valor);
                        break;
                    }
            }

            return Ok(data);
        }
    }
}
