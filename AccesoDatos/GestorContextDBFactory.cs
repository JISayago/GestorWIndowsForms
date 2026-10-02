using AccesoDatos.Config;
using AccesoDatos.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
            // ====================================================
            // OBTENER NOMBRE DE LA BASE
            // ====================================================

            string? nombreBaseDatos = null;

            if (args != null && args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                nombreBaseDatos = args[0].Trim();
            }

            // ====================================================
            // OBTENER CADENA DE CONEXION ACTUAL
            // ====================================================

            string cadenaConexion = Conexion.ObtenerCadenaConexion();

            // ====================================================
            // SI SE ESPECIFICO UNA BASE, CAMBIAR SOLO EL CATALOG
            // ====================================================

            if (!string.IsNullOrWhiteSpace(nombreBaseDatos))
            {
                var builder = new SqlConnectionStringBuilder(cadenaConexion);

                builder.InitialCatalog = nombreBaseDatos;

                cadenaConexion = builder.ConnectionString;
            }

            // ====================================================
            // CREAR DBCONTEXT
            // ====================================================

            var optionsBuilder = new DbContextOptionsBuilder<GestorContextDB>();

            optionsBuilder.UseSqlServer(
                cadenaConexion,
                sql => sql.CommandTimeout(300));

            return new GestorContextDB(optionsBuilder.Options);
        }
    }

}