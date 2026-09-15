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

            Console.WriteLine("Inicializando DbContext...");

            using var context = new GestorContextDBFactory()
                .CreateDbContext(null);

            Console.WriteLine("Aplicando migraciones...");

            context.Database.Migrate();

            Console.WriteLine();
            Console.WriteLine("Migraciones aplicadas correctamente.");

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