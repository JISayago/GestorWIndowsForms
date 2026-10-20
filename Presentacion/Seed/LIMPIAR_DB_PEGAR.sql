/* =============================================================================
   QueryData3.0 - LIMPIAR DB suave (seed)
   Borra datos de prueba / seed (IDs >= 100).
   PRESERVA: Admin, Consumidor Final, TiposPago, SADMIN, Permisos.

   Wipe total: LIMPIAR_DB_TOTAL.sql / .bat
   Flujo: LIMPIAR_DB_TOTAL -> abrir app 1 vez -> CARGAR_SEED.bat

   NO usar en una base productiva.
   ============================================================================= */

USE [EjemploBase];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    -- Desvincular clientes seed de sus CtaCte
    UPDATE [dbo].[Clientes]
    SET [CuentaCorrienteId] = NULL
    WHERE [PersonaId] >= 100;

    -- Hojas / movimientos / ventas
    IF OBJECT_ID(N'dbo.Notificaciones', N'U') IS NOT NULL
        DELETE FROM [dbo].[Notificaciones] WHERE [id_notificacion] >= 100;

    IF OBJECT_ID(N'dbo.Usuarios_Sesiones', N'U') IS NOT NULL
        DELETE FROM [dbo].[Usuarios_Sesiones] WHERE [UsuarioSesionId] >= 100;

    IF OBJECT_ID(N'dbo.CodigosRecuperacionPass', N'U') IS NOT NULL
        DELETE FROM [dbo].[CodigosRecuperacionPass] WHERE [CodigoRecuperacionId] >= 100;

    DELETE FROM [dbo].[Movimientos] WHERE [id_movimiento] >= 100;
    DELETE FROM [dbo].[VentaPagoDetalles] WHERE [id_VentaPagoDetalle] >= 100;

    IF OBJECT_ID(N'dbo.VentasLibres', N'U') IS NOT NULL
        DELETE FROM [dbo].[VentasLibres] WHERE [id_VentaLibre] >= 100;

    IF OBJECT_ID(N'dbo.DetalleVentaLote', N'U') IS NOT NULL
        DELETE FROM [dbo].[DetalleVentaLote] WHERE [DetalleVentaLoteId] >= 100;

    DELETE FROM [dbo].[DetallesVenta] WHERE [id_DetalleVenta] >= 100;
    DELETE FROM [dbo].[Ventas] WHERE [id_Venta] >= 100;

    IF OBJECT_ID(N'dbo.Gastos', N'U') IS NOT NULL
        DELETE FROM [dbo].[Gastos] WHERE [id_Gasto] >= 100;

    -- Ofertas (por oferta Y por producto, para no dejar FKs huérfanas)
    IF OBJECT_ID(N'dbo.OfertaProductoEstadisticas', N'U') IS NOT NULL
        DELETE FROM [dbo].[OfertaProductoEstadisticas]
        WHERE [id_OfertaDescuento] >= 100 OR [id_Producto] >= 100;

    IF OBJECT_ID(N'dbo.ProductosEnOfertaDescuentos', N'U') IS NOT NULL
        DELETE FROM [dbo].[ProductosEnOfertaDescuentos]
        WHERE [id_OfertaDescuento] >= 100 OR [id_Producto] >= 100;

    IF OBJECT_ID(N'dbo.OfertasDescuentos', N'U') IS NOT NULL
        DELETE FROM [dbo].[OfertasDescuentos] WHERE [id_OfertaDescuento] >= 100;

    -- Caja / CtaCte
    IF OBJECT_ID(N'dbo.Cajas', N'U') IS NOT NULL
        DELETE FROM [dbo].[Cajas] WHERE [CajaId] >= 100;

    IF OBJECT_ID(N'dbo.CuentaCorrienteAutorizados', N'U') IS NOT NULL
        DELETE FROM [dbo].[CuentaCorrienteAutorizados] WHERE [CuentaCorrienteAutorizadoId] >= 100;

    DELETE FROM [dbo].[CuentasCorrientes] WHERE [CuentaCorrienteId] >= 100;

    -- Detalles que apunten a productos u ofertas seed
    DELETE FROM [dbo].[DetallesVenta]
    WHERE [id_Producto] >= 100
       OR [id_OfertaDescuento] >= 100;

    IF OBJECT_ID(N'dbo.DetalleVentaLote', N'U') IS NOT NULL
        DELETE FROM [dbo].[DetalleVentaLote] WHERE [id_Producto] >= 100 OR [id_Lote] >= 100;

    -- Catalogo productos
    IF OBJECT_ID(N'dbo.Lotes', N'U') IS NOT NULL
        DELETE FROM [dbo].[Lotes] WHERE [id_Lote] >= 100 OR [id_Producto] >= 100;

    DELETE FROM [dbo].[Categorias_Productos]
    WHERE [CategoriaProductoId] >= 100 OR [IdProducto] >= 100;

    DELETE FROM [dbo].[Productos] WHERE [ProductoId] >= 100;
    DELETE FROM [dbo].[Categorias] WHERE [CategoriaId] >= 100;
    DELETE FROM [dbo].[Rubros] WHERE [RubroId] >= 100;
    DELETE FROM [dbo].[Marcas] WHERE [MarcaId] >= 100;

    -- Personas / empleados / roles de prueba
    DELETE FROM [dbo].[Clientes] WHERE [PersonaId] >= 100;
    DELETE FROM [dbo].[Empleados_Roles] WHERE [EmpleadoRolId] >= 100 OR [IdEmpleado] >= 100;

    IF OBJECT_ID(N'dbo.Roles_Permisos', N'U') IS NOT NULL
        DELETE FROM [dbo].[Roles_Permisos] WHERE [id_rol] >= 100;

    DELETE FROM [dbo].[Empleados] WHERE [PersonaId] >= 100;
    DELETE FROM [dbo].[Roles]
    WHERE [id_Rol] >= 100
      AND ISNULL([codigo_rol], N'') <> N'SADMIN';
    DELETE FROM [dbo].[Personas] WHERE [PersonaId] >= 100;

    -- Reseed IDENTITY al maximo que quedo (bootstrap)
    DECLARE @m BIGINT;

    IF OBJECT_ID(N'dbo.Notificaciones', N'U') IS NOT NULL
    BEGIN SELECT @m = ISNULL(MAX([id_notificacion]),0) FROM [dbo].[Notificaciones]; DBCC CHECKIDENT (N'[dbo].[Notificaciones]', RESEED, @m); END

    IF OBJECT_ID(N'dbo.Usuarios_Sesiones', N'U') IS NOT NULL
    BEGIN SELECT @m = ISNULL(MAX([UsuarioSesionId]),0) FROM [dbo].[Usuarios_Sesiones]; DBCC CHECKIDENT (N'[dbo].[Usuarios_Sesiones]', RESEED, @m); END

    IF OBJECT_ID(N'dbo.CodigosRecuperacionPass', N'U') IS NOT NULL
    BEGIN SELECT @m = ISNULL(MAX([CodigoRecuperacionId]),0) FROM [dbo].[CodigosRecuperacionPass]; DBCC CHECKIDENT (N'[dbo].[CodigosRecuperacionPass]', RESEED, @m); END

    SELECT @m = ISNULL(MAX([id_movimiento]),0) FROM [dbo].[Movimientos]; DBCC CHECKIDENT (N'[dbo].[Movimientos]', RESEED, @m);
    SELECT @m = ISNULL(MAX([id_VentaPagoDetalle]),0) FROM [dbo].[VentaPagoDetalles]; DBCC CHECKIDENT (N'[dbo].[VentaPagoDetalles]', RESEED, @m);

    IF OBJECT_ID(N'dbo.VentasLibres', N'U') IS NOT NULL
    BEGIN SELECT @m = ISNULL(MAX([id_VentaLibre]),0) FROM [dbo].[VentasLibres]; DBCC CHECKIDENT (N'[dbo].[VentasLibres]', RESEED, @m); END

    IF OBJECT_ID(N'dbo.DetalleVentaLote', N'U') IS NOT NULL
    BEGIN SELECT @m = ISNULL(MAX([DetalleVentaLoteId]),0) FROM [dbo].[DetalleVentaLote]; DBCC CHECKIDENT (N'[dbo].[DetalleVentaLote]', RESEED, @m); END

    SELECT @m = ISNULL(MAX([id_DetalleVenta]),0) FROM [dbo].[DetallesVenta]; DBCC CHECKIDENT (N'[dbo].[DetallesVenta]', RESEED, @m);
    SELECT @m = ISNULL(MAX([id_Venta]),0) FROM [dbo].[Ventas]; DBCC CHECKIDENT (N'[dbo].[Ventas]', RESEED, @m);

    IF OBJECT_ID(N'dbo.Gastos', N'U') IS NOT NULL
    BEGIN SELECT @m = ISNULL(MAX([id_Gasto]),0) FROM [dbo].[Gastos]; DBCC CHECKIDENT (N'[dbo].[Gastos]', RESEED, @m); END

    IF OBJECT_ID(N'dbo.OfertasDescuentos', N'U') IS NOT NULL
    BEGIN SELECT @m = ISNULL(MAX([id_OfertaDescuento]),0) FROM [dbo].[OfertasDescuentos]; DBCC CHECKIDENT (N'[dbo].[OfertasDescuentos]', RESEED, @m); END

    IF OBJECT_ID(N'dbo.Cajas', N'U') IS NOT NULL
    BEGIN SELECT @m = ISNULL(MAX([CajaId]),0) FROM [dbo].[Cajas]; DBCC CHECKIDENT (N'[dbo].[Cajas]', RESEED, @m); END

    IF OBJECT_ID(N'dbo.CuentaCorrienteAutorizados', N'U') IS NOT NULL
    BEGIN SELECT @m = ISNULL(MAX([CuentaCorrienteAutorizadoId]),0) FROM [dbo].[CuentaCorrienteAutorizados]; DBCC CHECKIDENT (N'[dbo].[CuentaCorrienteAutorizados]', RESEED, @m); END

    SELECT @m = ISNULL(MAX([CuentaCorrienteId]),0) FROM [dbo].[CuentasCorrientes]; DBCC CHECKIDENT (N'[dbo].[CuentasCorrientes]', RESEED, @m);

    IF OBJECT_ID(N'dbo.Lotes', N'U') IS NOT NULL
    BEGIN SELECT @m = ISNULL(MAX([id_Lote]),0) FROM [dbo].[Lotes]; DBCC CHECKIDENT (N'[dbo].[Lotes]', RESEED, @m); END

    SELECT @m = ISNULL(MAX([CategoriaProductoId]),0) FROM [dbo].[Categorias_Productos]; DBCC CHECKIDENT (N'[dbo].[Categorias_Productos]', RESEED, @m);
    SELECT @m = ISNULL(MAX([ProductoId]),0) FROM [dbo].[Productos]; DBCC CHECKIDENT (N'[dbo].[Productos]', RESEED, @m);
    SELECT @m = ISNULL(MAX([CategoriaId]),0) FROM [dbo].[Categorias]; DBCC CHECKIDENT (N'[dbo].[Categorias]', RESEED, @m);
    SELECT @m = ISNULL(MAX([RubroId]),0) FROM [dbo].[Rubros]; DBCC CHECKIDENT (N'[dbo].[Rubros]', RESEED, @m);
    SELECT @m = ISNULL(MAX([MarcaId]),0) FROM [dbo].[Marcas]; DBCC CHECKIDENT (N'[dbo].[Marcas]', RESEED, @m);
    SELECT @m = ISNULL(MAX([EmpleadoRolId]),0) FROM [dbo].[Empleados_Roles]; DBCC CHECKIDENT (N'[dbo].[Empleados_Roles]', RESEED, @m);
    SELECT @m = ISNULL(MAX([id_Rol]),0) FROM [dbo].[Roles]; DBCC CHECKIDENT (N'[dbo].[Roles]', RESEED, @m);
    SELECT @m = ISNULL(MAX([PersonaId]),0) FROM [dbo].[Personas]; DBCC CHECKIDENT (N'[dbo].[Personas]', RESEED, @m);

    COMMIT TRANSACTION;
    PRINT N'OK: limpieza completada. Quedaron Admin / Consumidor Final / TiposPago.';
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrState INT = ERROR_STATE();

    PRINT N'ERROR: se revirtio la limpieza (ROLLBACK).';
    RAISERROR (@ErrMsg, @ErrSeverity, @ErrState);
END CATCH
GO
