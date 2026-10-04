using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccesoDatos.Database
{
    public sealed record SeedProgreso(int Paso, int Total, string Archivo);

    public enum EstadoSeed { Vacia, Parcial, Completa }

    public static class PreparadorBase
    {
        /// Crea la base si no existe y aplica migraciones. Usa la base que indique Conexion.
        public static void Migrar()
        {
            using var ctx = new GestorContextDBFactory().CreateDbContext(null);
            ctx.Database.Migrate();
        }
    }

    public static class SeedTrial
    {
        public static EstadoSeed Estado()
        {
            using var cn = new SqlConnection(Conexion.ObtenerCadenaConexion());
            cn.Open();
            // 132 = última notificación que inserta 97_demo_presentacion.sql (último paso de escritura)
            using var cmd = new SqlCommand(@"
                SELECT CASE
                  WHEN EXISTS (SELECT 1 FROM Notificaciones WHERE id_notificacion = 132) THEN 2
                  WHEN EXISTS (SELECT 1 FROM Ventas WHERE id_Venta >= 100)
                    OR EXISTS (SELECT 1 FROM Productos WHERE ProductoId >= 100) THEN 1
                  ELSE 0 END", cn);
            return (EstadoSeed)(int)cmd.ExecuteScalar()!;
        }
    }

    public sealed class SeedRunner
    {
        private static readonly Regex GoRegex =
            new(@"^\s*GO(\s+\d+)?\s*(--.*)?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex UseRegex =
            new(@"^\s*USE\s+\[[^\]]+\]\s*;?\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

        private readonly string _carpeta;
        public SeedRunner(string carpetaScripts) => _carpeta = carpetaScripts;

        public static List<string> Orden(bool limpiarParcial)
        {
            var l = new List<string>();
            if (limpiarParcial) l.Add("LIMPIAR_DB_PEGAR.sql");
            l.Add("00_catalogo.sql");
            for (var m = new DateTime(2024, 8, 1); m <= new DateTime(2026, 8, 1); m = m.AddMonths(1))
                l.Add(Path.Combine("meses", $"{m:yyyy-MM}.sql"));
            l.Add("98_ctacte_final.sql");
            l.Add("97_demo_presentacion.sql");
            return l;
        }

        public async Task EjecutarAsync(bool limpiarParcial, IProgress<SeedProgreso> progreso, CancellationToken ct = default)
        {
            var archivos = Orden(limpiarParcial);
            foreach (var a in archivos)
            {
                var ruta = Path.Combine(_carpeta, a);
                if (!File.Exists(ruta))
                    throw new FileNotFoundException(
                        $"Falta el script de datos de prueba.\nBuscado en: {Path.GetFullPath(ruta)}");
            }
            await using var cn = new SqlConnection(Conexion.ObtenerCadenaConexion());
            await cn.OpenAsync(ct);

            int paso = 0;
            foreach (var archivo in archivos)
            {
                ct.ThrowIfCancellationRequested();
                progreso.Report(new SeedProgreso(++paso, archivos.Count, archivo));
                try
                {
                    foreach (var lote in LeerLotes(Path.Combine(_carpeta, archivo)))
                    {
                        await using var cmd = new SqlCommand(lote, cn) { CommandTimeout = 0 };
                        await cmd.ExecuteNonQueryAsync(ct);
                    }
                }
                catch (SqlException ex)
                {
                    throw new InvalidOperationException($"Falló {archivo}: {ex.Message}", ex);
                }
            }
        }

        private static IEnumerable<string> LeerLotes(string ruta)
        {
            using var sr = new StreamReader(ruta, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
            var sb = new StringBuilder();
            string? linea;
            while ((linea = sr.ReadLine()) != null)
            {
                if (GoRegex.IsMatch(linea))
                {
                    var t = UseRegex.Replace(sb.ToString(), "");   // quita "USE [EjemploBase]"
                    if (t.Trim().Length > 0) yield return t;
                    sb.Clear();
                }
                else sb.AppendLine(linea);
            }
            var resto = UseRegex.Replace(sb.ToString(), "");
            if (resto.Trim().Length > 0) yield return resto;
        }
    }
}
