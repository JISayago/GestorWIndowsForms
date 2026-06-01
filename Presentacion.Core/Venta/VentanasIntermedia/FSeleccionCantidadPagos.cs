using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Venta
{
    public partial class FSeleccionCantidadPagos : Form
    {
        // Resultado: si el usuario elige múltiples pagos -> true, si elige 1 pago -> false
        public bool multiplePagos = false;

        // Cantidad elegida (por defecto 1)
        public int CantidadPagos { get; private set; } = 1;

        // Flag para evitar que los handlers reaccionen cuando actualizamos los checkboxes desde código
        private bool _suspendHandlers = false;

        public FSeleccionCantidadPagos()
        {
            InitializeComponent();
        }

        private void FSeleccionCantidadPagos_Load(object sender, EventArgs e)
        {
            // Estado por defecto: 1 pago
            SetMode(false);
        }

        // Método centralizado para setear el modo y actualizar los checks sin provocar efectos colaterales
        private void SetMode(bool multiple)
        {
            _suspendHandlers = true;

            multiplePagos = multiple;

            // Actualizamos los checkbox según el modo elegido
            cbxMultiplesPagos.Checked = multiple;
            cbx1pago.Checked = !multiple;

            _suspendHandlers = false;
        }

        private void cbx1pago_CheckedChanged(object sender, EventArgs e)
        {
            if (_suspendHandlers) return;

            // Si el usuario marcó 1 pago, forzamos el modo "1 pago"
            if (cbx1pago.Checked)
                SetMode(false);
            else
                // Si se desmarcó manualmente, dejamos como 1 pago por defecto (evita estados inconsistentes)
                SetMode(false);
        }

        private void cbxMultiplesPagos_CheckedChanged(object sender, EventArgs e)
        {
            if (_suspendHandlers) return;

            // Si el usuario marcó múltiples, forzamos el modo "múltiples pagos"
            if (cbxMultiplesPagos.Checked)
                SetMode(true);
            else
                // Si se desmarcó manualmente, volvemos a 1 pago
                SetMode(false);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (!multiplePagos)
            {
                // 1 pago: devolvemos OK y CantidadPagos queda en 1
                CantidadPagos = 1;
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            // Si es múltiples, abrimos el formulario que pide la cantidad
            using (var fCantidad = new FCantidadItem())
            {
                // Opcional: ajustar límites predeterminados del dialog desde aquí si quisieras:
                // fCantidad.MaxCantidad = 12; fCantidad.MinCantidad = 2;

                var dr = fCantidad.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // El usuario confirmó la cantidad
                    CantidadPagos = (int)fCantidad.cantidad;
                    // Asegurar que la cantidad sea al menos 2
                    if (CantidadPagos < 2)
                    {
                        MessageBox.Show("La cantidad debe ser al menos 2 para pagos múltiples.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                else
                {
                    // Usuario canceló el diálogo de cantidad: volvemos al form sin cerrar
                    MessageBox.Show("Operación cancelada. Por favor, seleccione nuevamente la cantidad de pagos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }
    }

}
