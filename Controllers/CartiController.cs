using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using TestLaborator.Models;
using TestLaborator.ViewModels;

namespace TestLaborator.Controllers
{
    public class CartiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartiController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Carti
        public ActionResult Index()
        {
            var carti = _context.ListaCarti.Include(c => c.Librarie).ToList();
            var cartiSortatePret = carti.OrderByDescending(c => c.Pret);
            return View(cartiSortatePret);

        }



        public ActionResult AdaugaCarte()
        {
            Carte anunt = new Carte();

            anunt.OptiuniLibrarii = GetLibrarii();

            return View("AdaugaCarte", anunt);
        }

        [HttpPost]
        public ActionResult AdaugaCarte(Carte carte)
        {

            //  return View("Index");
            //            ModelState.Remove("CreatedById");

            if (!ModelState.IsValid)
            {
                // in caz de eroare vrem sa trimitem formularul cum a fost completat inapoi la user ( ca sa nu completeze iar toate field-urile pentru o greseala)
                return View("Index", carte);
            }

            var carteNoua = new Carte()
            {
                Titlu = carte.Titlu,
                Descriere = carte.Descriere,
                Editura = carte.Editura,
                Autor = carte.Autor,
                Pret = carte.Pret,
                IDLibrarie = carte.IDLibrarie,
            };

            try
            {
                _context.ListaCarti.Add(carteNoua);
                _context.SaveChanges();
            }
            // mereu la fel ; afiseaza eroarea in caz ca nu s-a putut salva in baza de date ( din orice motiv )
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }

            return RedirectToAction("Index", "Home");

        }

        private IEnumerable<SelectListItem> GetLibrarii()
        {
            var selectList = new List<SelectListItem>();

            var librarii = _context.ListaLibrarii.ToList();

            foreach (var librarie in librarii)
            {
                selectList.Add(
                    new SelectListItem()
                    {
                        Value = librarie.IDLibrarie.ToString(),
                        Text = librarie.Denumire.ToString()
                    }
                );
            }

            return selectList;
        }


        public ActionResult DetaliiCarte(int id)
        {
            var carteSelectata = _context.ListaCarti.Where(c => c.IDCarte == id).Include(c => c.Librarie).FirstOrDefault();
            return View(carteSelectata);
        }


        public ActionResult EditeazaCarte(int id)
        {
            var carte = _context.ListaCarti.Where(a => a.IDCarte == id).Include(c => c.Librarie).FirstOrDefault();
            carte.OptiuniLibrarii = GetLibrarii();

            return View(carte);
        }

        [HttpPost]
        public ActionResult EditeazaCarte(Carte carte)
        {

            if (!ModelState.IsValid)
            {
                // in caz de eroare vrem sa trimitem formularul cum a fost completat inapoi la user ( ca sa nu completeze iar toate field-urile pentru o greseala)
                return View("Index", carte);
            }

            var anuntDeModificat = _context.ListaCarti.Where(a => a.IDCarte == carte.IDCarte).FirstOrDefault();

            anuntDeModificat.Titlu = carte.Titlu;
            anuntDeModificat.Descriere = carte.Descriere;
            anuntDeModificat.Editura = carte.Editura;
            anuntDeModificat.Pret = carte.Pret;
            anuntDeModificat.Autor = carte.Autor;
            anuntDeModificat.Librarie = carte.Librarie;
            anuntDeModificat.IDLibrarie = carte.IDLibrarie;

            try
            {
                _context.Entry(anuntDeModificat).State = EntityState.Modified;
                _context.SaveChanges();
            }
            // mereu la fel ; afiseaza eroarea in caz ca nu s-a putut salva in baza de date ( din orice motiv )
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }

            return RedirectToAction("Index", "Carti");
        }


        public ActionResult StergeCarte(int Id)
        {
            var carteTmp = _context.ListaCarti.Find(Id);
            _context.ListaCarti.Remove(carteTmp);
            _context.SaveChanges();
            return RedirectToAction("Index", "Carti");
        }

        public ActionResult CautaCarte(int query = 0)
        {

            var carti = _context.ListaCarti.Include(C => C.Librarie).ToList();


            if (query != 0)
            {

                carti = carti
                    .Where(c => c.Pret <= query).ToList();
            }

            var cartiViewModel = new SearchViewModel()
            {
                Carti = carti,
                SearchTerm = query,
            };


            return View(cartiViewModel);
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel viewModel)
        {
            return RedirectToAction("CautaCarte", "Carti", new { query = viewModel.SearchTerm });
        }


    }
}