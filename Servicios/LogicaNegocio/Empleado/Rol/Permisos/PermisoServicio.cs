using AccesoDatos;
using AccesoDatos.Entidades;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Empleado.Rol.Tareas;
using Servicios.LogicaNegocio.Empleado.Rol.Tareas.DTO; // ajustá namespace si cambiaste a PermisoDTO
using System;
using System.Collections.Generic;
using System.Linq;

public class PermisoServicio : IPermisoServicio
{
    public List<PermisoDTO> ObtenerPermisos(FiltroConsulta filtro)
    {
        var context = new GestorContextDBFactory().CreateDbContext(null);
        var query = context.Permisos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtro.TextoBuscar))
        {
            query = query.Where(p =>
                p.Codigo.Contains(filtro.TextoBuscar) ||
                p.Descripcion.Contains(filtro.TextoBuscar));
        }

        var total = query.Count();

        var items = query
            .OrderBy(p => p.Descripcion)
            .Skip((filtro.Page - 1) * filtro.PageSize)
            .Take(filtro.PageSize)
            .Select(p => new PermisoDTO
            {
                PermisoId = p.PermisoId,
                Codigo = p.Codigo,
                Descripcion = p.Descripcion
            })
            .ToList();

        return items;
    }

    public IEnumerable<PermisoDTO> ObtenerPermisosAsignadosARol(long rolId)
    {
        var context = new GestorContextDBFactory().CreateDbContext(null);

        return context.RolesPermisos
            .Where(rp => rp.IdRol == rolId)
            .Select(rp => new PermisoDTO
            {
                PermisoId = rp.Permiso.PermisoId,
                Codigo = rp.Permiso.Codigo,
                Descripcion = rp.Permiso.Descripcion
            })
            .ToList();
    }

    public EstadoOperacion ActualizarPermisosDeRol(List<PermisoDTO> permisos, long rolId)
    {
        var context = new GestorContextDBFactory().CreateDbContext(null);

        using var transaction = context.Database.BeginTransaction();

        try
        {
            var actuales = context.RolesPermisos
                .Where(rp => rp.IdRol == rolId)
                .ToList();

            // eliminar todos los actuales
            context.RolesPermisos.RemoveRange(actuales);

            // insertar nuevos
            var nuevos = permisos.Select(p => new RolPermiso
            {
                IdRol = rolId,
                IdPermiso = p.PermisoId
            });

            context.RolesPermisos.AddRange(nuevos);

            context.SaveChanges();
            transaction.Commit();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Permisos actualizados correctamente"
            };
        }
        catch (Exception ex)
        {
            transaction.Rollback();

            return new EstadoOperacion
            {
                Exitoso = false,
                Mensaje = ex.Message
            };
        }
    }
}