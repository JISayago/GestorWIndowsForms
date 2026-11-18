USE [EjemploBase]
GO
/* =============================================================================
   96_FECHAS_RELATIVAS - corre el seed hacia HOY (QueryData3.0 / trial)

   El seed viene con fechas fijas (mes 2024-08). Este script:
     1) Calcula el desplazamiento en dias entre la ultima venta del seed y AYER
        y lo aplica a todas las fechas de los registros seed (IDs >= 100).
     2) Recalcula esta_vencido de los lotes segun su fecha_vencimiento vs HOY y
        resincroniza el stock de los productos con control por lote.

   - Resultado: el ultimo dia de ventas/caja del seed queda en AYER.
     (HOY queda libre para abrir caja y operar en la version trial.)
   - Idempotente: si ya esta alineado, el desplazamiento es 0 y no mueve fechas.
   - Se ejecuta DESPUES de cargar los meses y ANTES de 98 / 97 / 99.
   - No toca Personas.FechaNacimiento ni registros del bootstrap (IDs < 100).
   ============================================================================= */
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    DECLARE @hoy         DATE = CAST(GETDATE() AS DATE);
    DECLARE @objetivo    DATE = DATEADD(day, -1, @hoy);
    DECLARE @ultimaVenta DATE;
    DECLARE @d           INT;

    SELECT @ultimaVenta = MAX(CAST([fecha_venta] AS DATE))
    FROM [dbo].[Ventas]
    WHERE [id_Venta] >= 100;

    IF @ultimaVenta IS NULL
    BEGIN
        PRINT N'96: sin ventas seed, nada que desplazar.';
        RETURN;
    END

    SET @d = DATEDIFF(day, @ultimaVenta, @objetivo);

    BEGIN TRANSACTION;

    IF @d <> 0
    BEGIN
        /* ---- Transaccionales ---- */
        UPDATE [dbo].[Cajas] SET
            [fecha_apertura] = DATEADD(day, @d, [fecha_apertura]),
            [fecha_cierre]   = DATEADD(day, @d, [fecha_cierre])
        WHERE [CajaId] >= 100;

        UPDATE [dbo].[Ventas] SET [fecha_venta] = DATEADD(day, @d, [fecha_venta])
        WHERE [id_Venta] >= 100;

        UPDATE [dbo].[Movimientos] SET [fecha_movimiento] = DATEADD(day, @d, [fecha_movimiento])
        WHERE [id_movimiento] >= 100;

        UPDATE [dbo].[Gastos] SET
            [fecha_gasto]    = DATEADD(day, @d, [fecha_gasto]),
            [fecha_registro] = DATEADD(day, @d, [fecha_registro])
        WHERE [id_Gasto] >= 100;

        UPDATE [dbo].[VentasLibres] SET [fecha_venta] = DATEADD(day, @d, [fecha_venta])
        WHERE [id_VentaLibre] >= 100;

        /* ---- Catalogo ---- */
        UPDATE [dbo].[Lotes] SET
            [fecha_alta]        = DATEADD(day, @d, [fecha_alta]),
            [fecha_vencimiento] = DATEADD(day, @d, [fecha_vencimiento])
        WHERE [id_Lote] >= 100;

        UPDATE [dbo].[OfertasDescuentos] SET
            [fecha_inicio] = DATEADD(day, @d, [fecha_inicio]),
            [fecha_fin]    = DATEADD(day, @d, [fecha_fin])
        WHERE [id_OfertaDescuento] >= 100;

        UPDATE [dbo].[Clientes] SET
            [fecha_alta] = DATEADD(day, @d, [fecha_alta]),
            [fecha_baja] = DATEADD(day, @d, [fecha_baja])      /* NULL se mantiene NULL */
        WHERE [PersonaId] >= 100;

        UPDATE [dbo].[CuentasCorrientes] SET
            [fecha_activacion]  = DATEADD(day, @d, [fecha_activacion]),
            [fecha_creacion]    = DATEADD(day, @d, [fecha_creacion]),
            [fecha_vencimiento] = DATEADD(day, @d, [fecha_vencimiento])
        WHERE [CuentaCorrienteId] >= 100;

        UPDATE [dbo].[Empleados] SET [fechaIngreso] = DATEADD(day, @d, [fechaIngreso])
        WHERE [PersonaId] >= 100;

        UPDATE [dbo].[Empleados_Roles] SET [FechaAsignacion] = DATEADD(day, @d, [FechaAsignacion])
        WHERE [EmpleadoRolId] >= 100;
    END

    /* ---- Lotes: banderas coherentes con la fecha (siempre, es idempotente) ----
       Un lote con fecha_vencimiento anterior a HOY queda vencido e inactivo.
       Solo se pasa de 0 a 1: los lotes ya vencidos siguen vencidos. */
    UPDATE [dbo].[Lotes] SET
        [esta_vencido] = 1,
        [esta_activo]  = 0
    WHERE [id_Lote] >= 100
      AND [esta_eliminado] = 0
      AND [esta_vencido] = 0
      AND CAST([fecha_vencimiento] AS DATE) < @hoy;

    /* ---- Stock de productos con control por lote = suma de lotes activos no vencidos ---- */
    UPDATE p SET
        p.stock  = ISNULL(l.suma, 0),
        p.estado = CASE WHEN ISNULL(l.suma, 0) = 0 THEN 4
                        ELSE CASE WHEN p.estado = 4 THEN 1 ELSE p.estado END END
    FROM [dbo].[Productos] p
    OUTER APPLY (
        SELECT SUM([stock_actual]) AS suma
        FROM [dbo].[Lotes]
        WHERE [id_Producto] = p.ProductoId
          AND [esta_eliminado] = 0 AND [esta_activo] = 1 AND [esta_vencido] = 0
    ) l
    WHERE p.control_por_lote = 1 AND p.ProductoId >= 100;

    COMMIT TRANSACTION;

    PRINT N'96: fechas desplazadas ' + CAST(@d AS NVARCHAR(10)) + N' dias. '
        + N'Ultima venta: ' + CONVERT(NVARCHAR(10), @objetivo, 103) + N' (ayer). Lotes y stock alineados.';
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    DECLARE @m NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR(@m, 16, 1);
END CATCH
GO
