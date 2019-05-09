using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web.Mvc;
using TestLaborator.Models;

namespace TestLaborator.Controllers
{
    public class LibrariiController : Controller
    {
        // GET: Librarii
        private readonly ApplicationDbContext _context;
        public LibrariiController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdaugaLibrarie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdaugaLibrarie(Librarie librarie)
        {
            if (!ModelState.IsValid)
            {
                // in caz de eroare vrem sa trimitem formularul cum a fost completat inapoi la user ( ca sa nu completeze iar toate field-urile pentru o greseala)
                return View("Index", librarie);
            }

            var librarieNoua = new Librarie()
            {
                Denumire = librarie.Denumire
            };

            try
            {
                _context.ListaLibrarii.Add(librarieNoua);
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
    }
}