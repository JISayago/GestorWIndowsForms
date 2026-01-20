using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Caja
{
    public interface ICajaServicio
    {
        public void AbrirCaja(decimal montoInicial);

        public void CerrarCaja();

        public bool CajaEstaAbierta();

        public decimal ObtenerSaldoActual();

        public decimal SaldoActual();

        public void RegistrarIngreso(decimal monto, string descripcion);

        public void RegistrarEgreso(decimal monto, string descripcion);

    }
}
