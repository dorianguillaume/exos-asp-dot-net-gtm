using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class Calcul
    {
        [DataType(DataType.Date)]
        public DateTime DateInitiale { get; set; }

        [Range(1, 9999, ErrorMessage = "Le nombre de jours doit être compris entre 1 et 9999")]
        public int Jour { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateFinale { get; set; }

        public char Operateur { get; set; }
    }
}
