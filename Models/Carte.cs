using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace TestLaborator.Models
{
    public class Carte
    {
        [Key]
        public int IDCarte { get; set; }


        [Required]
        [StringLength(10)]
        public string Titlu { get; set; }

        [Required]
        [StringLength(10)]
        public String Descriere { get; set; }


        [Required]
        [StringLength(10)]
        public string Editura { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Pretul trebuie sa fie pozitiv")]
        public int Pret { get; set; }


        [Required]
        [StringLength(10)]
        public string Autor { get; set; }

        [ForeignKey("Librarie")]
        [Required]
        public int IDLibrarie { get; set; }

        public Librarie Librarie { get; set; }

        public IEnumerable<SelectListItem> OptiuniLibrarii { get; set; }
    }
}