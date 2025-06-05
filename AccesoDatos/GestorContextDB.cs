using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class GestorContextDB : DbContext
    {
       
            public GestorContextDB(DbContextOptions<GestorContextDB> options) : base(options) { }

        // Se agregarian los db set en formato public DbSet<Producto> Productos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            // se van a agregando en formato 
            // modelBuilder.Entity<Producto>().ToTable("Productos");
        }


    }
}
