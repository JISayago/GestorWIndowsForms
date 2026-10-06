/* =============================================================================
   DEMO PRESENTACION - Notificaciones para pantalla principal
   Ejecutar ANTES de abrir la app (o re-ejecutar y refrescar con F5 / re-login).

   Usa GETDATE(): lotes por vencer, ofertas vencidas/por vencer, bajo stock,
   CtaCte vencidas + registros en Notificaciones con titulos exactos del sistema.

   Orden carga: ... meses -> 98_ctacte_final -> ESTE -> 99_validaciones
   ============================================================================= */

USE [EjemploBase];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @hoy DATE = CAST(GETDATE() AS DATE);
    DECLARE @ahora DATETIME = GETDATE();

    /* ---- 1) LOTES por vencer (ventana 7 dias, activos, con stock) ---- */
    UPDATE l SET
        l.fecha_vencimiento = DATEADD(day, 1, @hoy),
        l.esta_activo = 1,
        l.esta_vencido = 0,
        l.stock_actual = CASE WHEN l.stock_actual <= 0 THEN 35 ELSE l.stock_actual END,
        l.descripcion = N'Demo TFI - vence manana'
    FROM [dbo].[Lotes] l
    WHERE l.id_Lote = 110;  /* LT-110 - Yogur Bebible 1L */

    UPDATE l SET
        l.fecha_vencimiento = DATEADD(day, 2, @hoy),
        l.esta_activo = 1,
        l.esta_vencido = 0,
        l.stock_actual = CASE WHEN l.stock_actual <= 0 THEN 28 ELSE l.stock_actual END,
        l.descripcion = N'Demo TFI - vence en 2 dias'
    FROM [dbo].[Lotes] l
    WHERE l.id_Lote = 118;

    UPDATE l SET
        l.fecha_vencimiento = DATEADD(day, 5, @hoy),
        l.esta_activo = 1,
        l.esta_vencido = 0,
        l.stock_actual = CASE WHEN l.stock_actual <= 0 THEN 42 ELSE l.stock_actual END,
        l.descripcion = N'Demo TFI - vence en 5 dias'
    FROM [dbo].[Lotes] l
    WHERE l.id_Lote = 123;

    UPDATE l SET
        l.fecha_vencimiento = DATEADD(day, 3, @hoy),
        l.esta_activo = 1,
        l.esta_vencido = 0,
        l.stock_actual = CASE WHEN l.stock_actual <= 0 THEN 50 ELSE l.stock_actual END,
        l.descripcion = N'Demo TFI - Leche Entera por vencer'
    FROM [dbo].[Lotes] l
    WHERE l.id_Lote = 100;  /* LT-100 - Leche Entera 1L */

    /* Sync stock productos con lote */
    UPDATE p SET p.stock = ISNULL(l.suma, 0), p.estado = CASE WHEN ISNULL(l.suma, 0) = 0 THEN 4 ELSE 1 END
    FROM [dbo].[Productos] p
    OUTER APPLY (
        SELECT SUM(stock_actual) AS suma
        FROM [dbo].[Lotes]
        WHERE id_Producto = p.ProductoId AND esta_eliminado = 0 AND esta_activo = 1 AND esta_vencido = 0
    ) l
    WHERE p.control_por_lote = 1 AND p.ProductoId >= 100;

    /* ---- 2) OFERTAS vencidas / por vencer (esta_activa=1, fin <= hoy+7) ---- */
    UPDATE [dbo].[OfertasDescuentos]
    SET fecha_fin = DATEADD(day, -2, @hoy), esta_activa = 1
    WHERE codigo = N'OF-SNACK-TEMP';

    UPDATE [dbo].[OfertasDescuentos]
    SET fecha_fin = DATEADD(day, -1, @hoy), esta_activa = 1
    WHERE codigo = N'OF-COLA-10';

    UPDATE [dbo].[OfertasDescuentos]
    SET fecha_fin = DATEADD(day, 2, @hoy), esta_activa = 1
    WHERE codigo = N'OF-ACEITE-FUT';

    /* ---- 3) OFERTAS bajo stock (stock <= cantidad_requerida, oferta activa) ---- */
    UPDATE p SET p.stock = 0, p.estado = 4
    FROM [dbo].[Productos] p
    WHERE p.ProductoId IN (107, 103);  /* Gaseosa Cola, Yerba Mate */

    /* Mantener Snack con stock normal (solo las 2 anteriores en alerta) */
    UPDATE p SET p.stock = CASE WHEN p.ProductoId = 133 THEN 800 ELSE p.stock END
    FROM [dbo].[Productos] p
    WHERE p.ProductoId = 133;

    /* ---- 4) CTACTE vencidas / por vencer (fecha_vencimiento <= hoy+7) ---- */
    UPDATE [dbo].[CuentasCorrientes]
    SET fecha_vencimiento = DATEADD(day, 0, @hoy)  /* vence HOY */
    WHERE CuentaCorrienteId = 100;

    UPDATE [dbo].[CuentasCorrientes]
    SET fecha_vencimiento = DATEADD(day, 3, @hoy)
    WHERE CuentaCorrienteId = 101;

    UPDATE [dbo].[CuentasCorrientes]
    SET fecha_vencimiento = DATEADD(day, -1, @hoy)  /* ya vencida */
    WHERE CuentaCorrienteId = 102;

    /* ---- 5) NOTIFICACIONES (titulos exactos PantallaPrincipalServicio) ---- */
    DELETE FROM [dbo].[Notificaciones]
    WHERE [id_notificacion] >= 100
       OR [titulo] LIKE N'Lote por vencer:%'
       OR [titulo] LIKE N'Oferta vencida:%'
       OR [titulo] LIKE N'Oferta con bajo stock:%'
       OR [titulo] LIKE N'CtaCte vencida:%';

    SET IDENTITY_INSERT [dbo].[Notificaciones] ON;

    /* Lotes - titulo termina con espacio (así lo genera el servicio) */
    INSERT INTO [dbo].[Notificaciones]
        ([id_notificacion],[titulo],[descripcion],[mensaje],[empleado_id],[fecha_vencimiento],[fecha_creacion],[fecha_confirmacion],[esta_leida])
    SELECT 100, N'Lote por vencer: LT-100 ',
           N'El producto Leche Entera 1L - Registra vencimiento el ' + CONVERT(NVARCHAR(10), DATEADD(day, 3, @hoy), 103) + N'.',
           N'Vence en 3 dias', NULL, DATEADD(day, 3, @hoy), @ahora, NULL, 0;

    INSERT INTO [dbo].[Notificaciones]
        ([id_notificacion],[titulo],[descripcion],[mensaje],[empleado_id],[fecha_vencimiento],[fecha_creacion],[fecha_confirmacion],[esta_leida])
    SELECT 101, N'Lote por vencer: LT-110 ',
           N'El producto Yogur Bebible 1L - Registra vencimiento el ' + CONVERT(NVARCHAR(10), DATEADD(day, 1, @hoy), 103) + N'.',
           N'Vence en 1 dias', NULL, DATEADD(day, 1, @hoy), DATEADD(minute, -45, @ahora), NULL, 0;

    INSERT INTO [dbo].[Notificaciones]
        ([id_notificacion],[titulo],[descripcion],[mensaje],[empleado_id],[fecha_vencimiento],[fecha_creacion],[fecha_confirmacion],[esta_leida])
    SELECT 102, N'Lote por vencer: LT-118 ',
           N'El producto Manteca 200g - Registra vencimiento el ' + CONVERT(NVARCHAR(10), DATEADD(day, 2, @hoy), 103) + N'.',
           N'Vence en 2 dias', NULL, DATEADD(day, 2, @hoy), DATEADD(minute, -20, @ahora), NULL, 0;

    INSERT INTO [dbo].[Notificaciones]
        ([id_notificacion],[titulo],[descripcion],[mensaje],[empleado_id],[fecha_vencimiento],[fecha_creacion],[fecha_confirmacion],[esta_leida])
    SELECT 103, N'Lote por vencer: LT-123 ',
           N'El producto Dulce de Leche 400g - Registra vencimiento el ' + CONVERT(NVARCHAR(10), DATEADD(day, 5, @hoy), 103) + N'.',
           N'Vence en 5 dias', NULL, DATEADD(day, 5, @hoy), DATEADD(hour, -2, @ahora), NULL, 1;

    /* Ofertas vencidas */
    INSERT INTO [dbo].[Notificaciones]
        ([id_notificacion],[titulo],[descripcion],[mensaje],[empleado_id],[fecha_vencimiento],[fecha_creacion],[fecha_confirmacion],[esta_leida])
    VALUES (110, N'Oferta vencida: OF-COLA-10',
            N'La oferta OF-COLA-10 - 10% Gaseosa Cola 2.25L (vigente) vencio el ' + CONVERT(NVARCHAR(10), DATEADD(day, -1, @hoy), 103) + N'.',
            N'La promocion ha cumplido su fecha limite de vigencia.', NULL, DATEADD(day, -1, @hoy), DATEADD(hour, -3, @ahora), NULL, 0);

    INSERT INTO [dbo].[Notificaciones]
        ([id_notificacion],[titulo],[descripcion],[mensaje],[empleado_id],[fecha_vencimiento],[fecha_creacion],[fecha_confirmacion],[esta_leida])
    VALUES (111, N'Oferta vencida: OF-SNACK-TEMP',
            N'La oferta OF-SNACK-TEMP - 10% Snack Papas (temporada 1er semestre) vencio el ' + CONVERT(NVARCHAR(10), DATEADD(day, -2, @hoy), 103) + N'.',
            N'La promocion ha cumplido su fecha limite de vigencia.', NULL, DATEADD(day, -2, @hoy), DATEADD(hour, -1, @ahora), NULL, 0);

    INSERT INTO [dbo].[Notificaciones]
        ([id_notificacion],[titulo],[descripcion],[mensaje],[empleado_id],[fecha_vencimiento],[fecha_creacion],[fecha_confirmacion],[esta_leida])
    VALUES (112, N'Oferta vencida: OF-ACEITE-FUT',
            N'La oferta OF-ACEITE-FUT - 8% Aceite (FUTURA) vencio el ' + CONVERT(NVARCHAR(10), DATEADD(day, 2, @hoy), 103) + N'.',
            N'La promocion ha cumplido su fecha limite de vigencia.', NULL, DATEADD(day, 2, @hoy), @ahora, NULL, 0);

    /* Ofertas bajo stock */
    INSERT INTO [dbo].[Notificaciones]
        ([id_notificacion],[titulo],[descripcion],[mensaje],[empleado_id],[fecha_vencimiento],[fecha_creacion],[fecha_confirmacion],[esta_leida])
    VALUES (120, N'Oferta con bajo stock: OF-COLA-10 - Gaseosa Cola 2.25L',
            N'La oferta OF-COLA-10 necesita 1 unidad(es) de Gaseosa Cola 2.25L por aplicacion, pero solo quedan 0 en stock.',
            N'Considerar reponer stock o desactivar la oferta para este producto.', NULL, NULL, DATEADD(minute, -30, @ahora), NULL, 0);

    INSERT INTO [dbo].[Notificaciones]
        ([id_notificacion],[titulo],[descripcion],[mensaje],[empleado_id],[fecha_vencimiento],[fecha_creacion],[fecha_confirmacion],[esta_leida])
    VALUES (121, N'Oferta con bajo stock: OF-YERBA-PF - Yerba Mate 1kg',
            N'La oferta OF-YERBA-PF necesita 1 unidad(es) de Yerba Mate 1kg por aplicacion, pero solo quedan 0 en stock.',
            N'Considerar reponer stock o desactivar la oferta para este producto.', NULL, NULL, DATEADD(minute, -15, @ahora), NULL, 0);

    /* CtaCte vencidas */
    INSERT INTO [dbo].[Notificaciones]
        ([id_notificacion],[titulo],[descripcion],[mensaje],[empleado_id],[fecha_vencimiento],[fecha_creacion],[fecha_confirmacion],[esta_leida])
    VALUES (130, N'CtaCte vencida: Cuenta Corriente Ana Torres',
            N'Cuenta corriente de Ana Torres registra fecha de vencimiento el ' + CONVERT(NVARCHAR(10), @hoy, 103) + N'.',
            N'Revisar saldo pendiente e historial de pagos del cliente.', NULL, @hoy, DATEADD(day, -1, @ahora), NULL, 0);

    INSERT INTO [dbo].[Notificaciones]
        ([id_notificacion],[titulo],[descripcion],[mensaje],[empleado_id],[fecha_vencimiento],[fecha_creacion],[fecha_confirmacion],[esta_leida])
    VALUES (131, N'CtaCte vencida: Cuenta Corriente Marina Sosa',
            N'Cuenta corriente de Marina Sosa registra fecha de vencimiento el ' + CONVERT(NVARCHAR(10), DATEADD(day, 3, @hoy), 103) + N'.',
            N'Revisar saldo pendiente e historial de pagos del cliente.', NULL, DATEADD(day, 3, @hoy), DATEADD(hour, -4, @ahora), NULL, 0);

    INSERT INTO [dbo].[Notificaciones]
        ([id_notificacion],[titulo],[descripcion],[mensaje],[empleado_id],[fecha_vencimiento],[fecha_creacion],[fecha_confirmacion],[esta_leida])
    VALUES (132, N'CtaCte vencida: Cuenta Corriente Valentina Rios',
            N'Cuenta corriente de Valentina Rios registra fecha de vencimiento el ' + CONVERT(NVARCHAR(10), DATEADD(day, -1, @hoy), 103) + N'.',
            N'Revisar saldo pendiente e historial de pagos del cliente.', NULL, DATEADD(day, -1, @hoy), DATEADD(hour, -6, @ahora), NULL, 0);

    SET IDENTITY_INSERT [dbo].[Notificaciones] OFF;

    DBCC CHECKIDENT (N'[dbo].[Notificaciones]', RESEED, 132);

    COMMIT TRANSACTION;

    PRINT N'OK: Demo presentacion lista.';
    PRINT N'  - 4 lotes por vencer (panel Lotes Vencidos)';
    PRINT N'  - 3 ofertas vencidas/por vencer';
    PRINT N'  - 2 ofertas bajo stock';
    PRINT N'  - 3 cuentas corrientes vencidas/por vencer';
    PRINT N'  - 12 notificaciones insertadas';
    PRINT N'Abrí la app con admin / Admin123 y mirá el panel derecho.';
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    DECLARE @m NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR(@m, 16, 1);
END CATCH
GO
