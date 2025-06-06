using AccesoDatos.Config;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    
    
        public class GestorContextDBFactory : IDesignTimeDbContextFactory<GestorContextDB>
        {
            public GestorContextDB CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<GestorContextDB>();
                optionsBuilder.UseSqlServer(Conexion.ObtenerCadenaConexion());

                return new GestorContextDB(optionsBuilder.Options);
            }

    }
    
}
