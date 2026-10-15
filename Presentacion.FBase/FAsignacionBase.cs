using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Producto.Asignacion
{
    // IMPORTANTE: el form tiene que ser la PRIMERA clase del archivo para que el
    // diseñador lo abra. Por eso ItemSeleccion y PaginaItems están en AsignacionModelos.cs.
    //
    // NO es abstract a propósito: el diseñador no abre formularios abstractos/genéricos.
    // Los métodos "abstractos" son virtual y tiran excepción.
    public partial class FAsignacionBase : FBase.FBase
    {
        protected const int TAMANIO_PAGINA = 10;

        // Ids tildados: viven aparte de la grilla para no perderse al filtrar/paginar.
        private readonly HashSet<long> _seleccionados = new HashSet<long>();
        // Nombres de todo lo que se vio o se cargó, para armar las descripciones al aceptar.
        private readonly Dictionary<long, string> _nombres = new Dictionary<long, string>();

        private bool _cargandoGrilla;
        private int _paginaActual = 1;
        private int _totalPaginas = 1;

        /// <summary>true = checks múltiples (categoría). false = solo uno (marca, rubro).</summary>
        protected bool PermiteMultiples { get; private set; } = true;

        protected long? EntidadID { get; private set; }

        public List<long> IdsSeleccionados { get; private set; } = new List<long>();
        public List<string> DescripcionesSeleccionadas { get; private set; } = new List<string>();

        // Azúcar para selección simple
        public long? IdSeleccionado => IdsSeleccionados.Count > 0 ? IdsSeleccionados[0] : (long?)null;
        public string DescripcionSeleccionada => DescripcionesSeleccionadas.FirstOrDefault();

        // Constructor que usa el diseñador
        public FAsignacionBase()
        {
            InitializeComponent();
        }

        protected FAsignacionBase(long? entidadId, bool permiteMultiples) : this()
        {
            EntidadID = entidadId;
            PermiteMultiples = permiteMultiples;
        }

        private bool EnDiseno
            => DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        #region 🔷 Contrato para los hijos

        protected virtual string TextoTitulo => "Asignar";
        protected virtual string TextoPlaceholder => "Buscar...";

        /// <summary>Llama al servicio paginado y mapea a PaginaItems.</summary>
        protected virtual PaginaItems ObtenerPagina(string textoBuscar, int pagina, int pageSize)
            => throw new NotImplementedException();

        /// <summary>Para resolver nombres de lo ya asignado (que puede no estar en la página visible).</summary>
        protected virtual ItemSeleccion ObtenerPorId(long id)
            => throw new NotImplementedException();

        /// <summary>Ids que ya tiene asignados la entidad (producto). Vacío si es alta.</summary>
        protected virtual IEnumerable<long> ObtenerIdsAsignados()
            => Enumerable.Empty<long>();

        #endregion

        #region 🔷 Eventos de UI (conectados en el Designer)

        private void FAsignacionBase_Load(object sender, EventArgs e)
        {
            if (EnDiseno)
                return;

            Inicializar();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Reinicia el timer en cada tecla: busca recién cuando el usuario deja de escribir
            timerBusqueda.Stop();
            timerBusqueda.Start();
        }

        private void timerBusqueda_Tick(object sender, EventArgs e)
        {
            timerBusqueda.Stop();
            CargarPagina(1);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
            => CargarPagina(_paginaActual - 1);

        private void btnSiguiente_Click(object sender, EventArgs e)
            => CargarPagina(_paginaActual + 1);

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvItems.IsCurrentCellDirty)
                dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);

            IdsSeleccionados = _seleccionados.ToList();

            DescripcionesSeleccionadas = IdsSeleccionados
                .Select(id => _nombres.TryGetValue(id, out var nombre) ? nombre : null)
                .Where(n => n != null)
                .ToList();

            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region 🔷 Carga

        private void Inicializar()
        {
            Text = TextoTitulo;
            txtBuscar.Text = string.Empty;
            txtBuscar.PlaceholderText = TextoPlaceholder;

            foreach (var id in ObtenerIdsAsignados())
            {
                _seleccionados.Add(id);

                var item = ObtenerPorId(id);
                if (item != null)
                    _nombres[id] = item.Nombre;
            }

            // Si no permite múltiples, nos quedamos con un solo id aunque vengan más
            if (!PermiteMultiples && _seleccionados.Count > 1)
            {
                var primero = _seleccionados.First();
                _seleccionados.Clear();
                _seleccionados.Add(primero);
            }

            CargarPagina(1);
        }

        private void CargarPagina(int pagina)
        {
            if (pagina < 1) pagina = 1;

            var texto = txtBuscar.Text?.Trim() ?? string.Empty;

            var resultado = ObtenerPagina(texto, pagina, TAMANIO_PAGINA);

            // Si se pidió una página que ya no existe (ej. cambió el filtro), vamos a la última
            var size = resultado.PageSize <= 0 ? TAMANIO_PAGINA : resultado.PageSize;
            var totalPaginas = Math.Max(1, (int)Math.Ceiling(resultado.TotalRegistros / (double)size));

            if (pagina > totalPaginas)
                resultado = ObtenerPagina(texto, totalPaginas, TAMANIO_PAGINA);

            _cargandoGrilla = true;
            try
            {
                dgvItems.Rows.Clear();

                foreach (var item in resultado.Items)
                {
                    _nombres[item.Id] = item.Nombre;

                    var index = dgvItems.Rows.Add(_seleccionados.Contains(item.Id), item.Nombre);
                    dgvItems.Rows[index].Tag = item.Id;
                }
            }
            finally
            {
                _cargandoGrilla = false;
            }

            ActualizarPaginacionUI(resultado);
        }

        private void ActualizarPaginacionUI(PaginaItems pagina)
        {
            _paginaActual = pagina.Page < 1 ? 1 : pagina.Page;

            var size = pagina.PageSize <= 0 ? TAMANIO_PAGINA : pagina.PageSize;
            _totalPaginas = Math.Max(1, (int)Math.Ceiling(pagina.TotalRegistros / (double)size));

            lblPaginacion.Text =
                $"Página {_paginaActual} de {_totalPaginas}  ({pagina.TotalRegistros} registros)";

            btnAnterior.Enabled = _paginaActual > 1;
            btnSiguiente.Enabled = _paginaActual < _totalPaginas;
        }

        #endregion

        #region 🔷 Selección

        // Confirma el click del checkbox enseguida para que dispare CellValueChanged
        private void Dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvItems.IsCurrentCellDirty)
                dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void Dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_cargandoGrilla || e.RowIndex < 0)
                return;

            if (dgvItems.Columns[e.ColumnIndex].Name != "Seleccionado")
                return;

            var row = dgvItems.Rows[e.RowIndex];
            if (!(row.Tag is long id))
                return;

            var tildado = Convert.ToBoolean(row.Cells["Seleccionado"].Value);

            if (tildado)
            {
                if (!PermiteMultiples)
                {
                    // Selección simple: se destildan los demás
                    _seleccionados.Clear();
                    _seleccionados.Add(id);
                    RefrescarChecksVisibles();
                    return;
                }

                _seleccionados.Add(id);
            }
            else
            {
                _seleccionados.Remove(id);
            }
        }

        private void RefrescarChecksVisibles()
        {
            _cargandoGrilla = true;
            try
            {
                foreach (DataGridViewRow row in dgvItems.Rows)
                {
                    if (row.Tag is long id)
                        row.Cells["Seleccionado"].Value = _seleccionados.Contains(id);
                }
            }
            finally
            {
                _cargandoGrilla = false;
            }
        }

        #endregion
    }
}
