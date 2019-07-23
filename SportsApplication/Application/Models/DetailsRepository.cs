using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsApplication.Models;

namespace Application.Models
{
    public class DetailsRepository : IDetails
    {
        private readonly ApplicationContext context;

        public DetailsRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<Details> DeleteAthlete(int id)
        {
            Details athlete = await context.Details.FindAsync(id);
            if(athlete!=null)
            {
                context.Remove(athlete);
            }
            return athlete;
        }

        public void EditAthlete(Details details)
        {

            context.Entry(details).State = EntityState.Modified;

        }

        public IEnumerable<Details> GetDetails(int id)
        {
            var query = from s in context.Details
                         where s.TestId == id
                         select new Details
                         {
                             TestResultId = s.TestResultId,
                             TestId = s.TestId,
                             Name = s.Name,
                             Distance = s.Distance,
                             FitnessRating = s.FitnessRating
                         };
            var qry = query.ToList();
            foreach (var v in qry)
            {
                if (v.Distance <= 1000)
                {
                    v.FitnessRating = "Below Average";
                }
                else if (v.Distance > 1000 && v.Distance <= 2000)
                {
                    v.FitnessRating = "Average";
                }
                else if (v.Distance > 2000 && v.Distance <= 3500)
                {
                    v.FitnessRating = "Good";
                }
                else
                {
                    v.FitnessRating = "Very Good";
                }
            }
            var newList = qry.OrderByDescending(x => x.Distance).ToList();
            return newList;
        }

        public async Task<Details> GetDetailsID(int id)
        {
            return await context.Details.FindAsync(id);
        }

        public async Task<Details> InsertAthlete(Details Athlete)
        {
            await context.Details.AddAsync(Athlete);
            return Athlete;
        }

    }
}
