using System;
using System.Collections.Generic;

namespace Ciqual.Models
{
    public partial class Famille
    {
        public Famille()
        {
            Aliment = new HashSet<Aliment>();
        }

        public int IdFamille { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<Aliment> Aliment { get; set; }
    }
}
