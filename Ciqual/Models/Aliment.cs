using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ciqual.Models
{
    public partial class Aliment : IValidatableObject
    {
        public Aliment()
        {
            Composition = new HashSet<Composition>();
        }    

        [Required]
        [Range(0, 99999, ErrorMessage = "L'Id doit être compris entre 0 et 99999")]
        public int IdAliment { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Le nom ne peut exceder 150 caractères")]
        public string Nom { get; set; }
        [Required]
        public int IdFamille { get; set; }

        public virtual Famille IdFamilleNavigation { get; set; }
        public virtual ICollection<Composition> Composition { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            CiqualContext context = (CiqualContext)validationContext.GetService(typeof(CiqualContext));
            Aliment aliment = (Aliment)validationContext.ObjectInstance;
            List<Aliment> aliments = context.Aliment.AsNoTracking().ToList();

            foreach (var item in aliments)
            {
                if (aliment.IdAliment == item.IdAliment)
                {
                    yield return new ValidationResult("Cet identifiant est déjà utilisé pour : "+item.Nom, new string[] { "IdAliment" });
                }
                if (aliment.Nom == item.Nom)
                {
                    yield return new ValidationResult("Un aliment avec ce nom existe déjà", new string[] { "Nom"});
                }
            }
        }
    }
}
