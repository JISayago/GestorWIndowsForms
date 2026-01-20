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
            //TODO FIX COLUMNAS Y TAMAÑO DE LA GRILLA
            //grilla.AutoGenerateColumns = false; si pongo esto despues no puedo setear las columnas, tengo que no mostrar las que no quiero uno por uno? 
            //deberia agregar las columnas en el dvg y desp unir con cada valor del dto

            //ajustar la fecha default de los dateTimePicker, cual poner de deault nuse
            var cajarOrdenadas = _cajas.OrderByDescending(x => x.FechaInicio).ToList();

            dgvCajas.DataSource = cajarOrdenadas;

            dtpAbierta.Format = DateTimePickerFormat.Short;
            dtpCerrada.Format = DateTimePickerFormat.Short;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            DateTime fechaDesde = dtpAbierta.Value.Date;
            DateTime fechaHasta = dtpCerrada.Value.Date.AddDays(1).AddSeconds(-1);

            var cajasFiltradas = _cajas
                .OrderByDescending(c => c.FechaInicio)
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
            var cajas = _cajas.OrderByDescending(x => x.FechaInicio).ToList();

            dgvCajas.DataSource = null;
            dgvCajas.DataSource = cajas;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cajasFiltrada = _cajas.OrderByDescending(x => x.FechaInicio).Take(7).ToList();

            dgvCajas.DataSource = null;
            dgvCajas.DataSource = cajasFiltrada;
        }
    }
}
