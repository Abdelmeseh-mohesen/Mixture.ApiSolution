using Microsoft.EntityFrameworkCore;
using Mixture.Core.Entity;
using Mixture.Core.Repositery;
using Mixture.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mixture.Repository
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class

    {
        private readonly MixtureContext mixtureContext;

        public GenericRepository(MixtureContext mixtureContext)
        {
            this.mixtureContext = mixtureContext;
        }
        public async Task<T> AddAsync(T Entity)
        {
            await mixtureContext.Set<T>().AddAsync(Entity); 
            await mixtureContext.SaveChangesAsync();    
            return Entity;
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
          return await mixtureContext.Set<T>().ToListAsync();

        }
    }
}
