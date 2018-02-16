﻿using BE.Administracion;
using BE.RegistroNotas;
using BE.Comun;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class RegistroNotasRepository
    {
        private DatabaseContext ctx = new DatabaseContext();

        public BandejaRegistroNotas BandejaRegistroNotas(BandejaRegistroNotas data)
        {
            try
            {
                int skip = (data.Index - 1) * data.Take;
                var query = (from a in ctx.CursosProgramados
                             join b in ctx.SalonProgramados on a.CursoProgramadoId equals b.CursoProgramadoId
                             join c in ctx.Eventos on a.EventoId equals c.EventoId
                             join d in ctx.Parametros on new {a = c.SedeId, b = 106} equals new {a = d.ParametroId, b = d.GrupoId }
                             join e in ctx.Cursos on a.CursoId equals e.CursoId
                             join f in ctx.Capacitadores on b.CapacitadorId equals f.CapacitadorId
                             join g in ctx.Personas on f.PersonaId equals g.PersonaId
                             where  (data.capacitadorId == -1 || b.CapacitadorId == data.capacitadorId) &&
                                    (data.cursoId == -1 || a.CursoId == data.cursoId) &&
                                    a.EsEliminado == 0
                             select new RegistroNotasList()
                             {
                                 cursoProgramadoId = a.CursoProgramadoId,
                                 salonProgramadoId = b.SalonProgramadoId,
                                 sedeId = c.EventoId,
                                 sede = d.Valor1,
                                 eventoId = c.EventoId,
                                 evento = c.Nombre,
                                 cursoId = a.CursoId,
                                 curso = e.NombreCurso,
                                 CapacitadorId = f.CapacitadorId,
                                 Capacitador = g.Nombres + " " + g.ApellidoPaterno + " " + g.ApellidoMaterno,

                             }).ToList();
                int TotalRegistros = query.Count;
                query = query.Skip(skip).Take(data.Take).ToList();
                BandejaRegistroNotas returnData = new BandejaRegistroNotas()
                {
                    Lista = query,
                    TotalRegistros = TotalRegistros
                };

                return returnData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<RegistroNotas> GetRegistroNotas(int salonProgramadoId)
        {
            try
            {
                var query = (from a in ctx.EmpleadoCursos
                             join b in ctx.EmpleadoAsistencias on a.EmpleadoCursoId equals b.EmpleadoCursoId
                             join c in ctx.Empleados on a.EmpleadoId equals c.EmpleadoId
                             join d in ctx.Personas on c.PersonaId equals d.PersonaId
                             join e in ctx.Parametros on new { a = a.CondicionId, b = 107 } equals new { a = e.ParametroId, b = e.GrupoId }
                             join f in ctx.EmpleadoTalleres on a.EmpleadoCursoId equals f.EmpleadoCursoId
                             where a.SalonProgramadoId == salonProgramadoId
                             group new { a, b, d, e, f } by new { a.EmpleadoId, a.SalonProgramadoId } into grp
                             select new RegistroNotas
                             {
                                 SalonProgramadoId = grp.FirstOrDefault().a.SalonProgramadoId,
                                 EmpleadoId = grp.Key.EmpleadoId,
                                 PersonaId = grp.FirstOrDefault().d.PersonaId,
                                 NombreCompletoEmpleado = grp.FirstOrDefault().d.Nombres + " " + grp.FirstOrDefault().d.ApellidoPaterno + " " + grp.FirstOrDefault().d.ApellidoMaterno,
                                 Nota = grp.FirstOrDefault().a.Nota,
                                 Taller = grp.FirstOrDefault().a.NotaTaller,
                                 Condicion = grp.FirstOrDefault().e.Valor1,
                                 Observacion = grp.FirstOrDefault().a.Observacion,
                                 EmpleadoAsistencia = grp.Select(x => new Asistencia { EmpleadoAsistenciaId = x.b.EmpleadoAsistenciaId, EmpleadoCursoId = x.b.EmpleadoCursoId, FechaClase = x.b.FechaClase, Asistio = x.b.Asistio }).GroupBy(x => x.EmpleadoAsistenciaId).Select(g => g.FirstOrDefault()).ToList(),
                                 EmpleadoTaller = grp.Select(x => new Taller { EmpleadoTallerId = x.f.EmpleadoTallerId, EmpleadoCursoId = x.f.EmpleadoCursoId, PreguntaId = x.f.PreguntaId, Valor = x.f.Valor }).GroupBy(x => x.EmpleadoTallerId).Select(g => g.FirstOrDefault()).ToList()
                             }
                             ).ToList();

                return query;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}