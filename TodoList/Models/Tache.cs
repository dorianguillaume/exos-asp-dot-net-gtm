using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class Tache
    {
        public int Id { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Date de Création")]
        [DataType(DataType.Date)]
        public DateTime DateCreation { get; set; }

        [Display(Name = "Date d'Echéance")]
        [DataType(DataType.Date)]
        public DateTime DateEcheance { get; set; }

        public bool Terminee { get; set; }
    }
}
