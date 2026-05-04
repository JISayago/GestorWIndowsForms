using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Seguridad
{
    public static class StaticPermisosCuerpo
    {
        public static readonly List<PermisoDefinicion> Lista = new()
    {
        // ========================
        // VENTAS
        // ========================
        new() { Codigo = "Ventas.Ver", Descripcion = "Ver ventas" },
        new() { Codigo = "Ventas.Crear", Descripcion = "Crear ventas" },
        new() { Codigo = "Ventas.Cobrar", Descripcion = "Cobrar ventas" },
        new() { Codigo = "Ventas.Anular", Descripcion = "Anular ventas" },
        new() { Codigo = "Ventas.VerDetalle", Descripcion = "Ver detalle de ventas" },
        new() { Codigo = "Ventas.Contrasiento", Descripcion = "Realizar contrasiento" },

        // ========================
        // PRODUCTOS
        // ========================
        new() { Codigo = "Productos.Ver", Descripcion = "Ver productos" },
        new() { Codigo = "Productos.ABM", Descripcion = "Gestionar productos (alta, baja, modificación)" },
        new() { Codigo = "Productos.GestionStock", Descripcion = "Gestionar stock de productos" },

        // ========================
        // STOCK / RELACIONADOS
        // ========================
        new() { Codigo = "Stock.Ver", Descripcion = "Ver stock" },
        new() { Codigo = "Stock.Modificar", Descripcion = "Modificar stock" },

        new() { Codigo = "Lotes.Ver", Descripcion = "Ver lotes" },
        new() { Codigo = "Lotes.ABM", Descripcion = "Gestionar lotes" },

        new() { Codigo = "Marcas.Ver", Descripcion = "Ver marcas" },
        new() { Codigo = "Marcas.ABM", Descripcion = "Gestionar marcas" },

        new() { Codigo = "Categorias.Ver", Descripcion = "Ver categorías" },
        new() { Codigo = "Categorias.ABM", Descripcion = "Gestionar categorías" },

        new() { Codigo = "Rubros.Ver", Descripcion = "Ver rubros" },
        new() { Codigo = "Rubros.ABM", Descripcion = "Gestionar rubros" },

        // ========================
        // EMPLEADOS
        // ========================
        new() { Codigo = "Empleados.Ver", Descripcion = "Ver empleados" },
        new() { Codigo = "Empleados.ABM", Descripcion = "Gestionar empleados" },
        new() { Codigo = "Empleados.AsignarRoles", Descripcion = "Asignar roles a empleados" },
        new() { Codigo = "Empleados.CrearUsuario", Descripcion = "Crear usuarios" },
        new() { Codigo = "Empleados.ResetearPassword", Descripcion = "Resetear contraseñas" },
        new() { Codigo = "Empleados.AsignarVendedor", Descripcion = "Asignar vendedor en ventas" },

        // ========================
        // ROLES
        // ========================
        new() { Codigo = "Roles.Ver", Descripcion = "Ver roles" },
        new() { Codigo = "Roles.ABM", Descripcion = "Gestionar roles" },
        new() { Codigo = "Roles.Asignar", Descripcion = "Asignar roles" },

        // ========================
        // PERMISOS
        // ========================
        new() { Codigo = "Permisos.Gestion", Descripcion = "Gestionar permisos del sistema" },

        // ========================
        // CLIENTES
        // ========================
        new() { Codigo = "Clientes.Ver", Descripcion = "Ver clientes" },
        new() { Codigo = "Clientes.ABM", Descripcion = "Gestionar clientes" },

        new() { Codigo = "CuentasCorrientes.Ver", Descripcion = "Ver cuentas corrientes" },
        new() { Codigo = "CuentasCorrientes.ABM", Descripcion = "Gestionar cuentas corrientes" },

        // ========================
        // OFERTAS
        // ========================
        new() { Codigo = "Ofertas.Ver", Descripcion = "Ver ofertas" },
        new() { Codigo = "Ofertas.ABM", Descripcion = "Gestionar ofertas" },
        new() { Codigo = "Ofertas.Activar", Descripcion = "Activar o desactivar ofertas" },

        // ========================
        // CAJA
        // ========================
        new() { Codigo = "Caja.Ver", Descripcion = "Ver estado de caja" },
        new() { Codigo = "Caja.Abrir", Descripcion = "Abrir caja" },
        new() { Codigo = "Caja.Cerrar", Descripcion = "Cerrar caja" },
        new() { Codigo = "Caja.VerMovimientos", Descripcion = "Ver movimientos de caja" },
        new() { Codigo = "Caja.Graficos", Descripcion = "Ver gráficos de caja" },

        // ========================
        // ADMINISTRACIÓN
        // ========================
        new() { Codigo = "Admin.Acceso", Descripcion = "Acceder al panel de administración" },
        new() { Codigo = "Admin.Gastos", Descripcion = "Gestionar gastos" },
        new() { Codigo = "Admin.Movimientos", Descripcion = "Ver movimientos generales" },
        new() { Codigo = "Admin.Comprobantes", Descripcion = "Acceder a comprobantes" },

        // ========================
        // OTROS
        // ========================
        new() { Codigo = "Notificaciones.Ver", Descripcion = "Ver notificaciones" }
    };
    }
}
