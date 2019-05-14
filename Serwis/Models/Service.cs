using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serwis.Models
{
    public class Service : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Customers> Customers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=(local);initial catalog=Serwis;integrated security=True;multipleactiveresultsets=True;");
        }

       
    }
}
