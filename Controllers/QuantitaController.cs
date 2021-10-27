using Microsoft.AspNetCore.Mvc;
using ProjectWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Models
{
    public class QuantitaController : Controller
    {
        private readonly IQuantitaService<Quantita> _service;

        private readonly IService<Prodotto> _service2;
        private readonly IService<Colore> _service3;
        private readonly IService<Taglia> _service4;

        public QuantitaController(IQuantitaService<Quantita> service, IService<Prodotto> service2, IService<Colore> service3, IService<Taglia> service4)
        {
            _service = service;
            _service2 = service2;
            _service3 = service3;
            _service4 = service4;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Elenco()
        {
            List<Quantita> listaQuantita = _service.GetAll();

            return View(listaQuantita);
        }

        [Route("/quantita/dettagli/{idprodotto}&{idcolore}&{idtaglia}")]
        public IActionResult Dettagli(int idprodotto, int idcolore, int idtaglia)
        {
            Quantita q = _service.SearchById(idprodotto, idcolore, idtaglia);

            if (q != null)
            {
                return View(q);
            }
            else
            {
                return Content($"Prodotto non trovato");
            }
        }
        public IActionResult FormNuovaQuantita()
        {
            List<Prodotto> listaProdotti = _service2.GetAll();
            ViewBag.listaProdotti = listaProdotti;
            List<Colore> listaColori = _service3.GetAll();
            ViewBag.listaColori = listaColori;
            List<Taglia> listaTaglie = _service4.GetAll();
            ViewBag.listaTaglie = listaTaglie;

            return View();
        }

        public IActionResult AggiungiQuantita(Dictionary<string, string> dati)
        {
            Quantita q = new Quantita();
            q.FromDictionary(dati);
            _service.Add(q);

            return Redirect("/Quantita/Elenco");
        }

        [Route("/quantita/FormModificaQuantita/{idprodotto}&{idcolore}&{idtaglia}")]
        public IActionResult FormModificaQuantita(int idprodotto, int idcolore, int idtaglia)
        {
            List<Prodotto> listaProdotti = _service2.GetAll();
            ViewBag.listaProdotti = listaProdotti;
            List<Colore> listaColori = _service3.GetAll();
            ViewBag.listaColori = listaColori;
            List<Taglia> listaTaglie = _service4.GetAll();
            ViewBag.listaTaglie = listaTaglie;

            Quantita q = _service.SearchById(idprodotto, idcolore, idtaglia);
            return View(q);
        }

        public IActionResult ModificaQuantita(Dictionary<string, string> dati, int prodottoid, int coloreid, int tagliaid, int oldprodottoid, int oldcoloreid, int oldtagliaid)
        {
            Quantita q = new Quantita();
            q.FromDictionary(dati);
            _service.Update(oldprodottoid, oldcoloreid, oldtagliaid, prodottoid, coloreid, tagliaid, q);

            return Redirect("/quantita/Elenco");
        }

        [Route("/quantita/elimina/{idprodotto}&{idcolore}&{idtaglia}")]
        public IActionResult Elimina(int idprodotto, int idcolore, int idtaglia)
        {
            _service.Delete(idprodotto, idcolore, idtaglia);

            return Redirect("/Quantita/Elenco");
        }
    }
}