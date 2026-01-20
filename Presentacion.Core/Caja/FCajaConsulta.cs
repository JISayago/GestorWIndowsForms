using AccesoDatos.Entidades;
using Servicios.LogicaNegocio.Caja;
using Servicios.LogicaNegocio.Caja.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Presentacion.Core.Caja
{
    public partial class FCajaConsulta : Form
    {
        List<CajaDTO> _cajas = new CajaServicio().ObetenerTodasLasCajas();

        public FCajaConsulta()
        {
            InitializeComponent();

            var grilla = dgvCajas;

            //TODO FIX COLUMNAS Y TAMA DE LA GRILLA

            //grilla.AutoGenerateColumns = false; si pongo esto despues no puedo setear las columnas, tengo que no mostrar las que no quiero uno por uno? 
            //deberia agregar las columnas en el dvg y desp unir con cada valor del dto

            grilla.DataSource = new CajaServicio().ObetenerTodasLasCajas();

            dtpAbierta.Format = DateTimePickerFormat.Short;
            dtpCerrada.Format = DateTimePickerFormat.Short;

        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            DateTime fechaDesde = dtpAbierta.Value.Date;
            DateTime fechaHasta = dtpCerrada.Value.Date.AddDays(1).AddSeconds(-1);

            var cajasFiltradas = _cajas
                .Where(c =>
                    c.FechaInicio >= fechaDesde &&
                    (
                        c.FechaFin == null ||   // cajas aún abiertas
                        c.FechaFin <= fechaHasta
                    )
                )
                .ToList();

            dgvCajas.DataSource = null;
            dgvCajas.DataSource = cajasFiltradas;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvCajas.DataSource = null;
            dgvCajas.DataSource = _cajas;
        }
    }
}
