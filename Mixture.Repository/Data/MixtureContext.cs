using Microsoft.EntityFrameworkCore;
using Mixture.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mixture.Repository.Data
{
    public class MixtureContext:DbContext
    {
        public MixtureContext(DbContextOptions<MixtureContext>options):base(options)
        {
            
        }
      /*  protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration ( new ProductConfugration());
            //modelBuilder.ApplyConfiguration(new ProductBrandConfugration());
            //modelBuilder.ApplyConfiguration(new ProductTypConfugration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }*/

        public DbSet<PumpReading>PumpReading{ get;set;}  


    }
}
