using AccesoDatos;
using AccesoDatos.Entidades;
using Servicios.LogicaNegocio.Caja.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Caja
{
    public class CajaServicio
    {

        /*  Antes hacer migracion 
         * 
         * 
         *  Crear la caja (abrir caja) y establecer los valores de inicio del negocio
         *  
         *  Cerrar caja y establecer los valores finales del negocio  
         *
         *  abrir y cerrar deberian ser la misma funcion con un parametro que indique si es apertura o cierre? o cu con su funcion respectiva
         *  
         *  Update balance de caja
         *  
         *  Obtener el estado actual de la caja (abierta o cerrada) y su saldo
         */


        public void AbrirCaja(decimal montoInicial, long empleadoId)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            if (context.Cajas.Any(c => !c.EstaCerrada))
            {
                throw new InvalidOperationException("Ya hay una caja abierta. No se puede abrir otra caja hasta que la actual sea cerrada.");
            }

            var caja = new AccesoDatos.Entidades.Caja();

            caja.SaldoInicial = montoInicial;
            caja.SaldoActual = montoInicial;
            caja.FechaInicio = DateTime.Now;
            caja.EmpleadoApertura = empleadoId;
            caja.EstaCerrada = false;

            context.Cajas.Add(caja);
            context.SaveChanges();
        }

        public void CerrarCaja(long empleadoId)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            //revisar ese llamado a db
            var caja = context.Cajas
                .Where(c => !c.EstaCerrada)
                .OrderByDescending(c => c.FechaInicio)
                .FirstOrDefault();

            if (caja != null)
            {
                caja.FechaFin = DateTime.Now;
                caja.EstaCerrada = true;
                caja.EmpleadoCierre = empleadoId; //asignar el empleado que cierra la caja
                // Asignar otros valores finales como TotalIngresos, TotalEgresos, BalanceFinal si es necesario
                context.SaveChanges();
            }
        }

        public CajaDTO ObtenerCaja(long cajaId)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            var caja = context.Cajas
                .Where(c => c.CajaId == cajaId)
                .Select(c => new CajaDTO
                {
                    CajaId = c.CajaId,
                    SaldoInicial = c.SaldoInicial,
                    SaldoActual = c.SaldoActual,
                    FechaInicio = c.FechaInicio,
                    FechaFin = c.FechaFin,
                    TotalIngresos = c.TotalIngresos,
                    TotalEgresos = c.TotalEgresos,
                    BalanceFinal = c.BalanceFinal,
                    EmpleadoApertura = c.EmpleadoApertura,
                    EmpleadoCierre = c.EmpleadoCierre,
                    EstaCerrada = c.EstaCerrada,
                    MovimientoIds = c.Movimientos.Select(m => m.MovimientoId).ToList()
                })
                .FirstOrDefault();

            if (caja == null)
            {
                throw new InvalidOperationException("No se encontró la caja con el ID proporcionado.");
            }


            return caja;
        }

        public long ObtenerIdCajaAbierta( GestorContextDB context = null)
        {
            if (context == null)
            {
                context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);
            }

            var caja = context.Cajas
                .Where(c => !c.EstaCerrada)
                .OrderByDescending(c => c.FechaInicio)
                .FirstOrDefault();
            if (caja == null)
            {
                return 0;
            }
            return caja.CajaId;
        }

        public bool ObtenerEstadoCaja()
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            return context.Cajas
                .Any(c => !c.EstaCerrada);
        }

        public decimal ObtenerSaldoCaja()
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            var saldo = context.Cajas
                .Where(c => !c.EstaCerrada)
                .Select(c => c.SaldoActual)
                .FirstOrDefault();

            return saldo;
        }

        public void RegistrarTransaccion(GestorContextDB context, decimal monto, string tipo)
        {
            var caja = context.Cajas
                .Where(c => !c.EstaCerrada)
                .OrderByDescending(c => c.FechaInicio)
                .FirstOrDefault();

            if (caja == null)
                throw new InvalidOperationException("No hay una caja abierta.");

            ActualizarSaldoCaja(caja, tipo, monto);

        }

        public void ActualizarSaldoCaja(AccesoDatos.Entidades.Caja caja, string tipo, decimal monto)
        {
            //TipoDeTransaccion podria es un enum en vez de string, fixear
            if (tipo == "Ingreso")
            {
                //caja.SaldoActual += monto;
                caja.TotalIngresos += monto;
            }
            else if (tipo == "Egreso")
            {
                //caja.SaldoActual -= monto;
                caja.TotalEgresos += monto;
            }
            else
            {
                throw new ArgumentException("Tipo de transacción inválido. Debe ser 'Ingreso' o 'Egreso'.");
            }

            caja.SaldoActual = caja.SaldoInicial + (caja.TotalIngresos - caja.TotalEgresos);
            //context.SaveChanges();
        }
        public List<CajaDTO> ObetenerTodasLasCajas()
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);
            
            var cajas = context.Cajas
                .Select(c => new CajaDTO
                {
                    CajaId = c.CajaId,
                    SaldoInicial = c.SaldoInicial,
                    SaldoActual = c.SaldoActual,
                    FechaInicio = c.FechaInicio,
                    FechaFin = c.FechaFin,
                    TotalIngresos = c.TotalIngresos,
                    TotalEgresos = c.TotalEgresos,
                    BalanceFinal = c.BalanceFinal,
                    EmpleadoApertura = c.EmpleadoApertura,
                    EmpleadoCierre = c.EmpleadoCierre,
                    EstaCerrada = c.EstaCerrada,
                    MovimientoIds = c.Movimientos.Select(m => m.MovimientoId).ToList()
                }).ToList();

            return cajas;
        }
    }
}
