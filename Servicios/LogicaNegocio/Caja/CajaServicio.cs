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


        /*public void AbrirCaja(decimal montoInicial, long empleadoId)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            if (context.Cajas.any(c => !c.EstaCerrada))
            {
                throw new InvalidOperationException("Ya hay una caja abierta. No se puede abrir otra caja hasta que la actual sea cerrada.");
            }

            var caja = new AccesoDatos.Entidades.Caja();

            caja.SaldoInicial = montoInicial;
            caja.SaldoActual = montoInicial;
            caja.FechaInicio = DateTime.Now;
            caja.EmpleadoApertura = empleadoId; //recibir el empleado que abre la caja? manejar x memoria?
            caja.EstaCerrada = false;

            context.Cajas.Add(caja);
            context.SaveChanges();
        }*/

        public void CerrarCaja()
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
                caja.EmpleadoCierre = 0; //asignar el empleado que cierra la caja
                // Asignar otros valores finales como TotalIngresos, TotalEgresos, BalanceFinal si es necesario
                context.SaveChanges();
            }
        }

        public CajaDTO ObtenerCaja(long cajaId)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);
            
            return context.Cajas
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
        }

        /*public bool ObtenerEstadoCaja()
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            return context.Cajas
                .any(c => !c.EstaCerrada);
        }

        public decimal ObtenerSaldoCaja()
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            return context.Cajas
                .Where(c => !c.EstaCerrada)
                .Select(c => c.SaldoActual);
        }*/

        public void RegistrarTransaccion(decimal monto, string tipo)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            var caja = context.Cajas
                .Where(c => !c.EstaCerrada)
                .OrderByDescending(c => c.FechaInicio)
                .FirstOrDefault();

            if (caja == null)
            {
                throw new InvalidOperationException("No hay una caja abierta para registrar la transacción.");
            }

            if (tipo == "ingreso")
            {
                caja.SaldoActual += monto;
                caja.TotalIngresos += monto;
            }
            else if (tipo == "egreso")
            {
                caja.SaldoActual -= monto;
                caja.TotalEgresos += monto;
            }
            else
            {
                throw new ArgumentException("Tipo de transacción inválido. Debe ser 'ingreso' o 'egreso'.");
            }

            ActualizarSaldoCaja(); //Lo dejo aca? asi cada vez que cambiamos el saldo se actuliza 
            //ActualizarBalanceCaja(); // si quiero actualizar el balance final tambien

            //ACA DEBERIA CAGAR EL MOVIMIENTO EN LA CAJA? O ESO LO HACE EL SERVICIO DE MOVIMIENTOS?
            //

            context.SaveChanges();
        }

        public void ActualizarSaldoCaja()
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            var caja = context.Cajas
                .Where(c => !c.EstaCerrada)
                .OrderByDescending(c => c.FechaInicio)
                .FirstOrDefault();

            if (caja != null)
            {
                caja.SaldoActual = caja.SaldoInicial + caja.TotalIngresos - caja.TotalEgresos;
                context.SaveChanges();
            }
        }

    }
}
