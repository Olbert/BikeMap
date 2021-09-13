using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GisButtons.Models
{
    public class CityContext : DbContext
    {
        public CityContext (DbContextOptions<CityContext> options)
            : base(options)
        {
        }

        public DbSet<GisButtons.Models.City> City { get; set; }
    }
}
