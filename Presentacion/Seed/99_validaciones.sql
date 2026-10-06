USE [EjemploBase]
GO

/* =============================================================================
   VALIDACIONES POST-CARGA - QueryData3.0
   "ESPERADO: 0 filas" debe devolver vacio si los datos cuadran.
   ============================================================================= */

PRINT N'--- 1) Ventas cuyo total NO coincide con DetallesVenta (ESPERADO: 0) ---';
SELECT v.id_Venta, v.total, SUM(dv.subtotal) AS total_detalles
FROM Ventas v
JOIN DetallesVenta dv ON dv.id_Venta = v.id_Venta
WHERE v.id_Venta >= 100
GROUP BY v.id_Venta, v.total
HAVING ABS(v.total - SUM(dv.subtotal)) > 0.05;

PRINT N'--- 2) Venta Confirmada/Cancelada sin Movimiento Ingreso/Venta (ESPERADO: 0) ---';
SELECT v.id_Venta
FROM Ventas v
WHERE v.id_Venta >= 100 AND v.estado IN (0,10)
  AND NOT EXISTS (
    SELECT 1 FROM Movimientos m
    WHERE m.entidad_id = v.id_Venta AND m.tipo_entidad = 1 AND m.tipo_movimiento_detalle = 4
  );

PRINT N'--- 3) CancelacionVenta (99) sin Movimiento Egreso/Cancelacion (ESPERADO: 0) ---';
SELECT v.id_Venta
FROM Ventas v
WHERE v.id_Venta >= 100 AND v.estado = 99
  AND NOT EXISTS (
    SELECT 1 FROM Movimientos m
    WHERE m.entidad_id = v.id_Venta AND m.tipo_entidad = 1 AND m.tipo_movimiento_detalle = 1
  );

PRINT N'--- 4) Cantidades no enteras en DetallesVenta (ESPERADO: 0) ---';
SELECT id_DetalleVenta, id_Venta, cantidad
FROM DetallesVenta
WHERE id_DetalleVenta >= 100 AND cantidad <> FLOOR(cantidad);

PRINT N'--- 5) DetalleVentaLote apuntando a CancelacionVenta (ESPERADO: 0) ---';
SELECT dvl.*
FROM DetalleVentaLote dvl
JOIN Ventas v ON v.id_Venta = dvl.id_Venta
WHERE dvl.DetalleVentaLoteId >= 100 AND v.estado = 99;

PRINT N'--- 6) CtaCte: con_deuda desalineado con saldo (ESPERADO: 0) ---';
SELECT CuentaCorrienteId, saldo, con_deuda
FROM CuentasCorrientes
WHERE CuentaCorrienteId >= 100
  AND con_deuda <> CASE WHEN saldo < 0 THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END;

PRINT N'--- 7) Producto control_por_lote: stock != suma lotes activos no vencidos (ESPERADO: 0) ---';
SELECT p.ProductoId, p.descripcion, p.stock,
       ISNULL(SUM(l.stock_actual),0) AS stock_lotes
FROM Productos p
LEFT JOIN Lotes l
       ON l.id_Producto = p.ProductoId
      AND l.esta_eliminado = 0
      AND l.esta_activo = 1
      AND l.esta_vencido = 0
WHERE p.ProductoId >= 100 AND p.control_por_lote = 1
GROUP BY p.ProductoId, p.descripcion, p.stock
HAVING ABS(p.stock - ISNULL(SUM(l.stock_actual),0)) > 0.01;

PRINT N'--- 8) Venta: monto_pagado + monto_adeudado != total (ESPERADO: 0) ---';
SELECT v.id_Venta, v.total, v.monto_pagado, v.monto_adeudado
FROM Ventas v
WHERE v.id_Venta >= 100 AND v.estado IN (0,10)
  AND ABS(v.total - (v.monto_pagado + v.monto_adeudado)) > 0.05;

PRINT N'--- 8b) Venta confirmada: suma VentaPagoDetalles != total (ESPERADO: 0; incluye mixtos) ---';
SELECT v.id_Venta, v.total, SUM(pd.monto) AS suma_pagos
FROM Ventas v
JOIN VentaPagoDetalles pd ON pd.id_Venta = v.id_Venta
WHERE v.id_Venta >= 100 AND v.estado IN (0,10)
GROUP BY v.id_Venta, v.total
HAVING ABS(v.total - SUM(pd.monto)) > 0.05;

PRINT N'--- 8c) Ventas con mas de un medio de pago (INFORMATIVO mixtos) ---';
SELECT v.id_Venta, COUNT(*) AS n_medios, SUM(pd.monto) AS suma
FROM Ventas v
JOIN VentaPagoDetalles pd ON pd.id_Venta = v.id_Venta
WHERE v.id_Venta >= 100 AND v.estado IN (0,10)
GROUP BY v.id_Venta
HAVING COUNT(*) > 1;

PRINT N'--- 9) Totales de Caja recalculados por dia (INFORMATIVO) ---';
SELECT c.CajaId, CAST(c.fecha_apertura AS DATE) AS dia, c.total_ingresos, c.total_egresos,
       ISNULL(SUM(CASE WHEN m.tipo_movimiento = 1 THEN m.monto ELSE 0 END),0) AS ingresos_recalculados,
       ISNULL(SUM(CASE WHEN m.tipo_movimiento = 2 THEN m.monto ELSE 0 END),0) AS egresos_recalculados
FROM Cajas c
LEFT JOIN Movimientos m
       ON CAST(m.fecha_movimiento AS DATE) = CAST(c.fecha_apertura AS DATE)
      AND m.id_movimiento >= 100
WHERE c.CajaId >= 100
GROUP BY c.CajaId, c.fecha_apertura, c.total_ingresos, c.total_egresos
ORDER BY c.CajaId;

PRINT N'--- 10) Cobertura medios de pago por numero_referencia (INFORMATIVO) ---';
SELECT tp.numero_referencia, tp.nombre, COUNT(*) AS cantidad_pagos
FROM VentaPagoDetalles pd
LEFT JOIN TiposPago tp ON tp.id_TipoPago = pd.id_TipoPago
WHERE pd.id_VentaPagoDetalle >= 100
GROUP BY tp.numero_referencia, tp.nombre
ORDER BY tp.numero_referencia;

PRINT N'--- 11) Roles_Permisos seed (INFORMATIVO) ---';
SELECT r.nombre, COUNT(*) AS n_permisos
FROM Roles_Permisos rp
JOIN Roles r ON r.id_Rol = rp.id_rol
WHERE rp.id_rol >= 100
GROUP BY r.nombre;

PRINT N'--- 12) Ofertas vigencia (INFORMATIVO) ---';
SELECT id_OfertaDescuento, codigo, fecha_inicio, fecha_fin, esta_activa
FROM OfertasDescuentos
WHERE id_OfertaDescuento >= 100
ORDER BY id_OfertaDescuento;

PRINT N'--- 13) Lotes al limite (INFORMATIVO) ---';
SELECT
  SUM(CASE WHEN esta_vencido = 1 THEN 1 ELSE 0 END) AS vencidos,
  SUM(CASE WHEN stock_actual = 0 AND esta_eliminado = 0 THEN 1 ELSE 0 END) AS stock_cero,
  SUM(CASE WHEN esta_activo = 1 AND esta_vencido = 0 AND fecha_vencimiento <= DATEADD(day, 15, GETDATE()) THEN 1 ELSE 0 END) AS por_vencer
FROM Lotes
WHERE id_Lote >= 100;

PRINT N'--- 14) DetalleVentaLote multi-lote por venta+producto (INFORMATIVO) ---';
SELECT id_Venta, id_Producto, COUNT(*) AS n_lotes
FROM DetalleVentaLote
WHERE DetalleVentaLoteId >= 100
GROUP BY id_Venta, id_Producto
HAVING COUNT(*) > 1;

PRINT N'--- 15) Resumen volumen seed (INFORMATIVO) ---';
SELECT 'Ventas' AS entidad, COUNT(*) AS n FROM Ventas WHERE id_Venta >= 100
UNION ALL SELECT 'DetallesVenta', COUNT(*) FROM DetallesVenta WHERE id_DetalleVenta >= 100
UNION ALL SELECT 'Movimientos', COUNT(*) FROM Movimientos WHERE id_movimiento >= 100
UNION ALL SELECT 'Cajas', COUNT(*) FROM Cajas WHERE CajaId >= 100
UNION ALL SELECT 'Gastos', COUNT(*) FROM Gastos WHERE id_Gasto >= 100
UNION ALL SELECT 'Productos', COUNT(*) FROM Productos WHERE ProductoId >= 100
UNION ALL SELECT 'CuentasCorrientes', COUNT(*) FROM CuentasCorrientes WHERE CuentaCorrienteId >= 100
UNION ALL SELECT 'OfertasDescuentos', COUNT(*) FROM OfertasDescuentos WHERE id_OfertaDescuento >= 100
UNION ALL SELECT 'OfertaProductoEstadisticas', COUNT(*) FROM OfertaProductoEstadisticas WHERE id_OfertaDescuento >= 100
UNION ALL SELECT 'VentasLibres', COUNT(*) FROM VentasLibres WHERE id_VentaLibre >= 100
UNION ALL SELECT 'Roles_Permisos', COUNT(*) FROM Roles_Permisos WHERE id_rol >= 100;

GO
