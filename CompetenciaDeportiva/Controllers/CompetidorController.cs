using Microsoft.AspNetCore.Mvc;
using CompetenciaDeportiva.Models;
using CompetenciaDeportiva.Datos;
using System.Linq;
using CompetenciaDeportiva.Models;

namespace TuProyecto.Controllers
{
    public class CompetidorController : Controller
    {
        CompetidorDatos competidorDatos = new CompetidorDatos();

        public IActionResult Index()
        {
            var lista = competidorDatos.Listar();
            return View(lista);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Competidor competidor)
        {
            if (!ModelState.IsValid)
            {
                return View(competidor);
            }

            competidorDatos.Guardar(competidor);
            return RedirectToAction("Index");
        }

        public IActionResult CantidadPorDisciplina()
        {
            var lista = competidorDatos.Listar();

            var agrupados = lista.GroupBy(x => x.Disciplina)
                                 .Select(g => new ParticipantePorDisciplina
                                 {
                                     Disciplina = g.Key,
                                     Cantidad = g.Count()
                                 })
                                 .ToList();

            return View(agrupados);
        }

    }
}
