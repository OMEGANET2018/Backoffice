﻿using BE.Administracion;
using BE.Comun;
using BL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace ObackOfficeAPI.Controllers.Administracion
{
    public class RegistroNotasController : ApiController
    {
        private RegistroNotasRepository rr = new RegistroNotasRepository();

        [HttpPost]
        public IHttpActionResult GetBandejaRegistroNotas(BandejaRegistroNotas data)
        {
            BandejaRegistroNotas result = rr.BandejaRegistroNotas(data);
            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult GetRegistroNotas(int salonProgramadoId)
        {
            List<RegistroNotas> result = rr.GetRegistroNotas(salonProgramadoId);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult GrabarRegistro(MultiDataModel data)
        {
            try
            {
                List<RegistroNotas> registros = JsonConvert.DeserializeObject<List<RegistroNotas>>(data.String1);
                return Ok(rr.GrabarRegistro(registros, 1));
            }
            catch (Exception e)
            {
                return BadRequest("Parámetros incorrectos");
            }
        }

        [HttpPost]
        public IHttpActionResult CargaExamenDiploma(MultiDataModel data)
        {
            Dictionary<string, byte[]> listado = JsonConvert.DeserializeObject<Dictionary<string, byte[]>>(data.String1);
            string directorioExamenes = string.Format("{0}{1}\\", System.Web.Hosting.HostingEnvironment.MapPath("~/"), System.Configuration.ConfigurationManager.AppSettings["directorioExamenes"].ToString());
            string directorioDiplomas = string.Format("{0}{1}\\", System.Web.Hosting.HostingEnvironment.MapPath("~/"), System.Configuration.ConfigurationManager.AppSettings["directorioDiplomas"].ToString());

            var response = rr.CargaExamenDiploma(listado,directorioExamenes,directorioDiplomas);

            return Ok(response);
        }
    }
}
