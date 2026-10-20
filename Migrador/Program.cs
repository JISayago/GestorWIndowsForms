using AccesoDatos;
using Microsoft.EntityFrameworkCore;

namespace Migrador;

internal static class Program
{
    [STAThread]
    static int Main(string[] args)
    {
        try
        {
            Console.WriteLine("========================================");
            Console.WriteLine(" Stockeate - Migrador de base de datos");
            Console.WriteLine("========================================");
            Console.WriteLine();

            // ====================================================
            // VALIDAR ARGUMENTO
            // ====================================================

            if (args == null || args.Length == 0 || string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine("ERROR: No se especifico la base de datos.");
                Console.WriteLine();
                Console.WriteLine("Uso:");
                Console.WriteLine("Migrador.exe Stockeate");
                Console.WriteLine("Migrador.exe StockeateTrial");

                return 1;
            }

            string nombreBaseDatos = args[0].Trim();

            // ====================================================
            // VALIDAR BASE PERMITIDA
            // ====================================================

            if (!nombreBaseDatos.Equals("Stockeate", StringComparison.OrdinalIgnoreCase) &&
                !nombreBaseDatos.Equals("StockeateTrial", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"ERROR: Base de datos no permitida: {nombreBaseDatos}");
                Console.WriteLine();
                Console.WriteLine("Bases permitidas:");
                Console.WriteLine("Stockeate");
                Console.WriteLine("StockeateTrial");

                return 1;
            }

            Console.WriteLine($"Base de datos seleccionada: {nombreBaseDatos}");
            Console.WriteLine();

            // ====================================================
            // INICIALIZAR DBCONTEXT
            // ====================================================

            Console.WriteLine("Inicializando DbContext...");

            using var context = new GestorContextDBFactory()
                .CreateDbContext(new[] { nombreBaseDatos });

            // ====================================================
            // APLICAR MIGRACIONES
            // ====================================================

            Console.WriteLine($"Aplicando migraciones sobre {nombreBaseDatos}...");

            context.Database.Migrate();

            Console.WriteLine();
            Console.WriteLine($"Migraciones de {nombreBaseDatos} aplicadas correctamente.");

            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR DURANTE LA MIGRACION");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(ex);
            Console.WriteLine("----------------------------------------");

            return 1;
        }
    }
}