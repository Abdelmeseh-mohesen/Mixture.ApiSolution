using Mixture.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mixture.Core.Repositery
{
    public interface IGenericRepository<T> where T : class
    {
         public Task<T> AddAsync(T Entity);
        public Task<IReadOnlyCollection<T>>GetAllAsync(); 
    }
}
