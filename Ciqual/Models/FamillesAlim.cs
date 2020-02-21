using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ciqual.Models
{
    public class FamillesAlim
    {
        [Display(Name="Liste des Familles")]
        public List<Famille> Famille { get; set; }
        public List<AlimentConsti> Aliments { get; set; }
    }
}
