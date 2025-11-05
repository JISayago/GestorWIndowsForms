using AccesoDatos;
using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.DatosObligatorios
{
    public class TipoDePagoInicial
    {
        public static void Inicializar(GestorContextDB context)
        {
            // Creamos los tipos de pago a partir del enum
            var tiposPago = new[]
            {
                new TipoPago
                {
                    Nombre = "Efectivo",
                    Detalle = "Pago en efectivo",
                    Codigo = "EF",
                    NumeroReferencia = (int)TipoDePago.Efectivo,
                    MetodoPagoHabilitado = true,
                    EstaEliminado = false
                },
                new TipoPago
                {
                    Nombre = "Transferencia Bancaria",
                    Detalle = "Pago mediante transferencia bancaria",
                    Codigo = "TB",
                    NumeroReferencia = (int)TipoDePago.Transferencia,
                    MetodoPagoHabilitado = true,
                    EstaEliminado = false
                },
                new TipoPago
                {
                    Nombre = "Tarjeta de Crédito",
                    Detalle = "Pago con tarjeta de crédito",
                    Codigo = "TC",
                    NumeroReferencia = (int)TipoDePago.Credito,
                    MetodoPagoHabilitado = true,
                    EstaEliminado = false
                },
                new TipoPago
                {
                    Nombre = "Tarjeta de Débito",
                    Detalle = "Pago con tarjeta de débito",
                    Codigo = "TD",
                    NumeroReferencia = (int)TipoDePago.Debito,
                    MetodoPagoHabilitado = true,
                    EstaEliminado = false
                },
                new TipoPago
                {
                    Nombre = "Cuenta Corriente",
                    Detalle = "Pago mediante cuenta corriente",
                    Codigo = "CC",
                    NumeroReferencia = (int)TipoDePago.CtaCte,
                    MetodoPagoHabilitado = true,
                    EstaEliminado = false
                },
                new TipoPago
                {
                    Nombre = "QR",
                    Detalle = "Pago mediante código QR",
                    Codigo = "QR",
                    NumeroReferencia = (int)TipoDePago.QR,
                    MetodoPagoHabilitado = true,
                    EstaEliminado = false
                },
                new TipoPago
                {
                    Nombre = "Cheque",
                    Detalle = "Pago mediante cheque",
                    Codigo = "CH",
                    NumeroReferencia = (int)TipoDePago.Cheque,
                    MetodoPagoHabilitado = true,
                    EstaEliminado = false
                },
                new TipoPago
                {
                    Nombre = "Otro",
                    Detalle = "Otros métodos de pago",
                    Codigo = "OT",
                    NumeroReferencia = (int)TipoDePago.Otro,
                    MetodoPagoHabilitado = true,
                    EstaEliminado = false
                }
            };

            // Agregamos solo si no existe un tipo con el mismo Codigo
            foreach (var tipo in tiposPago)
            {
                if (!context.TiposPago.Any(tp => tp.Codigo == tipo.Codigo))
                {
                    context.TiposPago.Add(tipo);
                }
            }

            context.SaveChanges();
        }
    }
}

