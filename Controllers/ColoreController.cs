using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models;
using ProjectWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Controllers
{
    public class ColoreController : Controller
    {
        private readonly IService<Colore> _service;

        public ColoreController(IService<Colore> service)
        {
            _service = service;
        }

        public IActionResult Elenco()
        {
            List<Colore> listaColori = _service.GetAll();

            return View(listaColori);
        }
        public IActionResult Dettagli(int id)
        {
            Colore p = _service.GetByID(id);

            if (p != null)
            {
                return View(p);
            }
            else
            {
                return Content($"Colore con id {id} non trovato");
            }
        }
        public IActionResult FormNuovoColore()
        {
            return View();
        }
        public IActionResult FormModificaColore(int id)
        {
            Colore p = _service.GetByID(id);
            return View(p);
        }

        public IActionResult Aggiungi(Dictionary<string, string> dati)
        {
            Colore p = new Colore();
            p.FromDictionary(dati);
            _service.Add(p);

            return Redirect("/Colore/Elenco");
        }

        public IActionResult Elimina(int id)
        {
            _service.Delete(id);

            return Redirect("/Colore/Elenco");
        }

        public IActionResult Update(int id, Dictionary<string, string> dati)
        {
            Colore pi = new();

            pi.FromDictionary(dati);

            _service.Update(id, pi);

            return Redirect("/Colore/Elenco");
        }
    }
}
