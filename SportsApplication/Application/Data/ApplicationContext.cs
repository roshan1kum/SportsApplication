using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsApplication.Models;

namespace Application.Models
{
    public class ApplicationContext : DbContext
    {
       public ApplicationContext (DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }


        public DbSet<SportsApplication.Models.test> test { get; set; }

        public DbSet<SportsApplication.Models.Details> Details { get; set; }
    }
}
