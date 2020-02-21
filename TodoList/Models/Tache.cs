using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class Tache : IValidatableObject
    {
        public int Id { get; set; }

        [StringLength(250, ErrorMessage = "La description ne peut pas dépasser 250 caractères"), Required(ErrorMessage = "La description doit être renseignée")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Date de Création")]
        [DataType(DataType.Date)]
        public DateTime DateCreation { get; set; }

        [Display(Name = "Date d'Echéance")]
        [DataType(DataType.Date)]
        public DateTime? DateEcheance { get; set; }

        public bool Terminee { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            Tache tache = (Tache)validationContext.ObjectInstance;

            if (tache.DateCreation > tache.DateEcheance)
            {
                yield return new ValidationResult("La date de création ne doit pas être inférieur à la date d'échéance");
                yield return new ValidationResult("*", new string[] { "DateCreation", "DateEcheance" });

            }
        }

        public static explicit operator double(Tache v)
        {
            throw new NotImplementedException();
        }
    }
}
