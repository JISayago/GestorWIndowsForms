# QueryData3.0 — Seed 2 años

Paquete listo para probar Stockeate / `EjemploBase`.

## Flujo (obligatorio)

1. **`LIMPIAR_DB_TOTAL.bat`** — vacía **todos** los registros (deja esquema + migraciones)
2. **Abrir la app UNA vez** — crea Admin, Consumidor Final, TiposPago, SADMIN, Permisos
3. **`CARGAR_SEED.bat`** — limpia solo seed viejo (IDs ≥ 100) y carga data

Login smoke: `admin` / `Admin123`

## Demo presentación (mediodía TFI)

Si ya tenés la DB cargada, **solo antes de abrir la app**:

```bat
DEMO_PRESENTACION.bat
```

Eso ajusta con `GETDATE()`:
- **4 lotes por vencer** (LT-100, LT-110, LT-118, LT-123)
- **3 ofertas** vencidas / por vencer (OF-COLA-10, OF-SNACK-TEMP, OF-ACEITE-FUT)
- **2 ofertas bajo stock** (Gaseosa Cola, Yerba Mate sin stock)
- **3 CtaCte** vencidas o por vencer (Ana Torres, Marina Sosa, Valentina Rios)
- **12 notificaciones** en el panel derecho de la pantalla principal

Carga completa: `CARGAR_SEED.bat` ya incluye `97_demo_presentacion.sql` al final.

## Novedades vs 2.0

| Feature | Detalle |
|---------|---------|
| **Pagos mixtos** | ~5k ventas con 2 medios; CtaCte parcial + caja; `id_TipoPago` por `numero_referencia` |
| **CtaCte alineado** | `98_ctacte_final.sql` actualiza `saldo`/`con_deuda` post-movimientos |
| **Ofertas con vigencia** | vigentes, temporada, vencida, futura, inactiva + `OfertaProductoEstadisticas` |
| **Roles_Permisos** | Vendedor (100) y Cajero (101) con permisos reales del sistema |
| **Lotes al límite** | vencidos, stock 0, por vencer, multi-lote FEFO en una venta |

## Contenido (~)

- 730 días / 25 meses
- ~19.7k ventas, ~5k pagos mixtos, ~330 CtaCte, ~780 ítems en oferta

## Scripts

| Archivo | Uso |
|---------|-----|
| `LIMPIAR_DB_TOTAL.bat` | Wipe total |
| `CARGAR_SEED.bat` | Carga seed |
| `98_ctacte_final.sql` | Saldos CtaCte finales |
| `97_demo_presentacion.sql` | Demo TFI: notificaciones con fechas relativas a HOY |
| `DEMO_PRESENTACION.bat` | Solo ejecuta el demo (sin recargar todo el seed) |
| `99_validaciones.sql` | Checks (mixtos, lotes, roles, ofertas) |
| `generator/generate-seed.mjs` | Regenerar |

## Regenerar

```bash
cd generator
node generate-seed.mjs
```
