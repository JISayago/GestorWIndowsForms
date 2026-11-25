using AccesoDatos;
using AccesoDatos.Config;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Sistema;
using Servicios.LogicaNegocio.Configuracion.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Servicios.LogicaNegocio.Configuracion
{
    public class ConfiguracionServicio : IConfiguracionServicio
    {
        // ===============================================================//
        // LECTURA                                                        //
        // ===============================================================//

        public T Obtener<T>(ClaveConfiguracion clave) where T : class, new()
        {
            return LeerJson<T>(clave) ?? new T();
        }

        public bool ExisteConfiguracion(ClaveConfiguracion clave)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var claveTexto = clave.ToString();

            return context.ParametrosSistema
                .AsNoTracking()
                .Any(p => p.Clave == claveTexto && (p.Valor != null || p.ValorBinario != null));
        }

        public byte[] ObtenerLogo()
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var claveTexto = ClaveConfiguracion.LogoNegocio.ToString();

            return context.ParametrosSistema
                .AsNoTracking()
                .Where(p => p.Clave == claveTexto)
                .Select(p => p.ValorBinario)
                .FirstOrDefault();
        }

        public NomenclaturaCodigoDTO ObtenerNomenclatura(TipoCodigo tipo)
        {
            return LeerJson<NomenclaturaCodigoDTO>(ConfiguracionClaves.Para(tipo))
                   ?? NomenclaturaCodigoDTO.PorDefecto(tipo);
        }

        /// <summary>Uso: _configuracionServicio.GenerarCodigo(TipoCodigo.Venta, numeroVenta)</summary>
        public string GenerarCodigo(TipoCodigo tipo, long numero, DateTime? fecha = null)
        {
            return ObtenerNomenclatura(tipo).Formatear(numero, fecha ?? DateTime.Now);
        }

        private T LeerJson<T>(ClaveConfiguracion clave) where T : class
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var claveTexto = clave.ToString();

            var json = context.ParametrosSistema
                .AsNoTracking()
                .Where(p => p.Clave == claveTexto)
                .Select(p => p.Valor)
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(json))
                return null;

            try
            {
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (JsonException)
            {
                // JSON dañado: se cae al valor por defecto de quien llama.
                return null;
            }
        }

        // ===============================================================//
        // ESCRITURA                                                      //
        // ===============================================================//

        public EstadoOperacion Guardar(LoteConfiguracion lote)
        {
            if (lote == null || lote.Items.Count == 0)
            {
                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "No hay cambios para guardar."
                };
            }

            try
            {
                using var context = new GestorContextDBFactory().CreateDbContext(null);

                var claves = lote.Items.Select(i => i.Clave).Distinct().ToList();

                var existentes = context.ParametrosSistema
                    .Where(p => claves.Contains(p.Clave))
                    .ToDictionary(p => p.Clave);

                foreach (var item in lote.Items)
                {
                    existentes.TryGetValue(item.Clave, out var fila);

                    if (fila == null)
                    {
                        // Nada que limpiar si la clave todavía no existe.
                        if (item.Texto == null && item.Binario == null)
                            continue;

                        fila = new ParametroSistema { Clave = item.Clave };
                        context.ParametrosSistema.Add(fila);
                        existentes[item.Clave] = fila;
                    }

                    fila.Valor = item.Texto;
                    fila.ValorBinario = item.Binario;
                    fila.FechaModificacion = DateTime.Now;
                }

                // Un único SaveChanges: todo el lote entra o no entra nada.
                context.SaveChanges();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Configuración guardada correctamente."
                };
            }
            catch (Exception ex)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        /// <summary>Pasa lo que había en config.json (Comprobantes) a la base, una sola vez.</summary>
        public EstadoOperacion MigrarComprobantesDesdeJson()
        {
            if (ExisteConfiguracion(ClaveConfiguracion.DatosNegocio))
            {
                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "Los datos del negocio ya estaban migrados."
                };
            }

            var viejo = ConfigManager.Config.Comprobantes;

            var lote = new LoteConfiguracion();

            lote.Texto(ClaveConfiguracion.DatosNegocio, new DatosNegocioDTO
            {
                NombreNegocio = string.IsNullOrWhiteSpace(viejo.NombreNegocio) ? "MI NEGOCIO" : viejo.NombreNegocio,
                Subtitulo = viejo.Subtitulo ?? "",
                TextoPie = viejo.TextoPie ?? ""
            });

            if (!string.IsNullOrWhiteSpace(viejo.RutaLogo) && File.Exists(viejo.RutaLogo))
                lote.Binario(ClaveConfiguracion.LogoNegocio, File.ReadAllBytes(viejo.RutaLogo));

            return Guardar(lote);
        }
    }
}