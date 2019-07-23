using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApplication.Models
{
    public class Details
    {
        [Key]
        public int TestResultId { get; set; }
        public int TestId { get; set; }
        public String Name { get; set; }
        [DisplayName("Disptance(meter")]
        public int Distance { get; set; }
        [DisplayName("Fitness rating")]
        public string FitnessRating { get; set; }
    }
}
