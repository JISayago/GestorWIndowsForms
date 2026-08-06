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
            optionsBuilder.UseSqlServer(
                Conexion.ObtenerCadenaConexion(),
                sql => sql.CommandTimeout(300)); // 5 min, para migraciones pesadas (creación de índices sobre tablas grandes)

            return new GestorContextDB(optionsBuilder.Options);
        }

    }

}