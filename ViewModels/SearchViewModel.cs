using System.Collections.Generic;
using TestLaborator.Models;

namespace TestLaborator.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Carte> Carti { get; set; }
        public int SearchTerm { get; set; }
    }
}