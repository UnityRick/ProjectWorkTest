using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models;
using ProjectWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Controllers
{
    public class ProdottoController : Controller
    {
        private readonly IService<Prodotto> _service;

        public ProdottoController(IService<Prodotto> service)
        {
            _service = service;
        }

        public IActionResult Elenco()
        {
            List<Prodotto> listaProdotti = _service.GetAll();

            return View(listaProdotti);
        }
        //eheeh
        public IActionResult Dettagli(int id)
        {
            Prodotto p = _service.GetByID(id);

            if (p != null)
            {
                return View(p);
            }
            else
            {
                return Content($"Prodotto con id {id} non trovato");
            }
        }
        public IActionResult FormNuovoProdotto()
        {
            return View();
        }
        public IActionResult FormModificaProdotto(int id)
        {
            Prodotto p = _service.GetByID(id);
            return View(p);
        }

        public IActionResult Aggiungi(Dictionary<string, string> dati)
        {
            Prodotto p = new Prodotto();
            p.FromDictionary(dati);
            _service.Add(p);

            return Redirect("/Prodotto/Elenco");
        }

        public IActionResult Elimina(int id)
        {
            _service.Delete(id);

            return Redirect("/Prodotto/Elenco");
        }

        public IActionResult Update(int id, Dictionary<string,string> dati)
        {
            Prodotto pi = new();

            pi.FromDictionary(dati);

            _service.Update(id, pi);

            return Redirect("/Prodotto/Elenco");
        }

        //public IActionResult Elenco()
        //{
        //     return View(_service.GetAll());
        //}


        //public IActionResult NuovoProdotto([FromBody] Prodotto p)
        //{

        //    return View(_service.Add(p));

        //}
        //[HttpDelete("{id}")]
        //public ActionResult Delete([FromRoute] int id)
        //{

        //    return Json(_service.Delete(id));

        //}
        //[HttpPut("{id}")]
        //public ActionResult Update([FromRoute]int id,[FromBody] Prodotto p)
        //{
        //    return Json(_service.Update(id, p));
        //}
    }
}
