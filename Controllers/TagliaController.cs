using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models;
using ProjectWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Controllers
{
    public class TagliaController : Controller
    {
        private readonly IService<Taglia> _service;

        public TagliaController(IService<Taglia> service)
        {
            _service = service;
        }

        public IActionResult Elenco()
        {
            List<Taglia> listaTaglie = _service.GetAll();

            return View(listaTaglie);
        }
        //eheeh
        public IActionResult Dettagli(int id)
        {
            Taglia p = _service.GetByID(id);

            if (p != null)
            {
                return View(p);
            }
            else
            {
                return Content($"Taglia con id {id} non trovato");
            }
        }
        public IActionResult FormNuovoTaglia()
        {
            return View();
        }
        public IActionResult FormModificaTaglia(int id)
        {
            Taglia p = _service.GetByID(id);
            return View(p);
        }

        public IActionResult Aggiungi(Dictionary<string, string> dati)
        {
            Taglia p = new Taglia();
            p.FromDictionary(dati);
            _service.Add(p);

            return Redirect("/Taglia/Elenco");
        }

        public IActionResult Elimina(int id)
        {
            _service.Delete(id);

            return Redirect("/Taglia/Elenco");
        }

        public IActionResult Update(int id, Dictionary<string, string> dati)
        {
            Taglia pi = new();

            pi.FromDictionary(dati);

            _service.Update(id, pi);

            return Redirect("/Taglia/Elenco");
        }
    }
}
