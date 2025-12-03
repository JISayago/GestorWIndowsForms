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


        public void AbrirCerrarCaja(decimal montoInicial, bool abrir)
        {
            // Lógica para abrir la caja con el monto inicial

        }

        public void ObtenerEstadoCaja()
        {
            // Lógica para obtener el estado actual de la caja
            //devolver si esta abierta o cerrada
            return (true, 0m); // Ejemplo de retorno
        }

        public void ObtenerSaldoCaja()
        {
            // Lógica para obtener el saldo actual de la caja
            //devolver el saldo 
            return 0m; // Ejemplo de retorno
        }

        public void RegistrarTransaccion(decimal monto, string tipo)
        {
            // Lógica para registrar una transacción en la caja
            //Modificar la caja
        }

    }
}
