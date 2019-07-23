using SportsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public interface ITestsRepository
    {
        IEnumerable<test> GetTest();
        Task<test> GetTestID(int? testId);
        Task<test> InsertTest(test tests);
        void Update(test tests);
        Task<test> DeleteTest(int testId);
        int NumberOfParticipants(int testId);
       
    }
}
