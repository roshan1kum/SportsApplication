using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApplication.Models
{
    public class test
    {
        [Key]
        public int TestId { get; set; }
        public int Date { get; set; }
        [DisplayName("Number of Participants")]
        public int number_participants { get; set; }
        [DisplayName("Test Type")]
        public string test_type { get; set; }
    }
}
