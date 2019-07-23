using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public interface IUnitOfWork
    {
        ITestsRepository TestsRepository { get; }
        IDetails detailRepository { get; }
        void Save();
    }
}
