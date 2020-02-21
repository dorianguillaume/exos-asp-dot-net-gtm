using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class StatistiquesTacheViewModel
    {
        [Display(Name ="Nombre de Tâches en Cours")]
        public int NbTacheEnCours { get; set; }
        [Display(Name = "Nombre de Tâches Terminées")]
        public int NbTacheTerminee { get; set; }
        [Display(Name = "Nombre de Tâches en Retard")]
        public int NbTacheEnRetard { get; set; }
        [Display(Name = "Délai Moyen de Réalisation d'une Tâche")]
        public double DelaiMoyenRealisationTache { get; set; }

    }
}
