using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassStore.Domain.Entities;
using System.Data.Entity;

namespace ClassStore.Domain.Concrete
{
    internal class EFDbContext : DbContext
    {
        public DbSet<Class> Classes { get; set; }
    }
}
