using SportsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
   public interface IDetails
    {
        IEnumerable<Details> GetDetails(int id);
        Task<Details> GetDetailsID(int id);
        Task<Details> InsertAthlete(Details Athlete);
        void EditAthlete(Details Athlete);
        Task<Details> DeleteAthlete(int id);
        
    }
}
