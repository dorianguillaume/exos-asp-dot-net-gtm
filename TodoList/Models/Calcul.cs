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

        public int Jour { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateFinale { get; set; }
    }
}
