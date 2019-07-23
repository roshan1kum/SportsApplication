using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext Context;
        private ITestsRepository testsRepository;
        private IDetails _detailRepository;
        public UnitOfWork(ApplicationContext Context)
        {
            this.Context = Context;
        }
        public ITestsRepository TestsRepository
        {
            get
            {
                return testsRepository = new TestRepository(Context);
            }
        }
        public IDetails detailRepository
        {
            get
            {
                return _detailRepository = new DetailsRepository(Context);
            }
        }

      //  public IDetails detailRepository => throw new NotImplementedException();

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
