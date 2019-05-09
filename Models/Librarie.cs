using System;
using System.ComponentModel.DataAnnotations;

namespace TestLaborator.Models
{
    public class Librarie
    {
        [Key]
        public int IDLibrarie { get; set; }

        [Required]
        [StringLength(10)]
        public String Denumire { get; set; }
    }
}