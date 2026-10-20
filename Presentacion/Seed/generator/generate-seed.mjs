/**
 * Generador de seed SQL - Stockeate / EjemploBase (QueryData3.0)
 * 2 años de historial + CtaCte + ofertas + ventas libres + cancelaciones
 *
 * Uso:
 *   node generate-seed.mjs
 *   node generate-seed.mjs --days 730 --end 2026-08-11 --ventas-dia 28
 */
import fs from "fs";
import path from "path";
import { fileURLToPath } from "url";

const __dirname = path.dirname(fileURLToPath(import.meta.url));
const OUT = path.resolve(__dirname, "..");
const MESES = path.join(OUT, "meses");

// ---------- CLI ----------
const args = Object.fromEntries(
  process.argv.slice(2).flatMap((a, i, arr) => {
    if (!a.startsWith("--")) return [];
    const k = a.slice(2);
    const v = arr[i + 1] && !arr[i + 1].startsWith("--") ? arr[i + 1] : "true";
    return [[k, v]];
  })
);

const END = parseDate(args.end || "2026-08-11");
const DAYS = Number(args.days || 730);
const START = addDays(END, -(DAYS - 1));
const AVG_VENTAS = Number(args["ventas-dia"] || 28);
const SEED = Number(args.seed || 20260811);
const DB = args.db || "EjemploBase";

// ---------- RNG ----------
function mulberry32(a) {
  return function () {
    let t = (a += 0x6d2b79f5);
    t = Math.imul(t ^ (t >>> 15), t | 1);
    t ^= t + Math.imul(t ^ (t >>> 7), t | 61);
    return ((t ^ (t >>> 14)) >>> 0) / 4294967296;
  };
}
const rnd = mulberry32(SEED);
const pick = (arr) => arr[Math.floor(rnd() * arr.length)];
const randInt = (min, max) => min + Math.floor(rnd() * (max - min + 1));
const chance = (p) => rnd() < p;
const money = (n) => Math.round(n * 100) / 100;
const fmt = (n) => money(n).toFixed(2);
const sqlN = (s) => `N'${String(s).replace(/'/g, "''")}'`;
const sqlD = (d) => {
  const p = (n) => String(n).padStart(2, "0");
  return `'${d.getFullYear()}-${p(d.getMonth() + 1)}-${p(d.getDate())} ${p(d.getHours())}:${p(d.getMinutes())}:${p(d.getSeconds())}'`;
};
const sqlDate = (d) => {
  const p = (n) => String(n).padStart(2, "0");
  return `'${d.getFullYear()}-${p(d.getMonth() + 1)}-${p(d.getDate())}'`;
};
function parseDate(s) {
  const [y, m, d] = s.split("-").map(Number);
  return new Date(y, m - 1, d, 0, 0, 0, 0);
}
function addDays(d, n) {
  const x = new Date(d);
  x.setDate(x.getDate() + n);
  return x;
}
function ymd(d) {
  const p = (n) => String(n).padStart(2, "0");
  return `${d.getFullYear()}${p(d.getMonth() + 1)}${p(d.getDate())}`;
}
function ym(d) {
  const p = (n) => String(n).padStart(2, "0");
  return `${d.getFullYear()}-${p(d.getMonth() + 1)}`;
}

// ---------- Catalogo base ----------
const MARCAS = [
  "Nortex","Vitalia","Solmax","Rio Claro","Del Cerro","Buena Cosecha","Andina","Puro Sabor",
  "Casa Blanca","Tierra Fresca","Norteña","Valle Verde","Cumbre Azul","Estrella Sur","Don Alfonso",
];
const RUBROS = ["Almacen","Bebidas","Limpieza","Perfumeria","Lacteos","Panificados","Congelados","Bazar"];
const CATEGORIAS = [
  "Snacks","Gaseosas y Jugos","Higiene Personal","Detergentes y Limpieza","Fiambres y Quesos",
  "Golosinas","Conservas","Infusiones","Cuidado del Hogar","Mascotas","Bebidas Alcoholicas","Congelados y Precocidos",
];

const PRODUCT_DEFS = [
  // [desc, rubroIdx, marcaIdx, costo, venta, lote, catIdxs]
  ["Arroz Largo Fino 1kg",0,0,1200,2100,0,[0,6]],
  ["Fideos Tallarin 500g",0,1,800,1400,0,[0]],
  ["Aceite de Girasol 1.5L",0,2,2500,4200,0,[6]],
  ["Yerba Mate 1kg",0,3,2800,4500,0,[7]],
  ["Azucar Comun 1kg",0,4,900,1500,0,[0]],
  ["Harina 0000 1kg",0,5,700,1200,0,[0]],
  ["Sal Fina 500g",0,6,400,700,0,[0]],
  ["Gaseosa Cola 2.25L",1,7,1500,2800,0,[1]],
  ["Agua Mineral 2L",1,8,500,900,0,[1]],
  ["Jugo Exprimido 1L",1,9,1100,1900,0,[1]],
  ["Cerveza Rubia 1L",1,10,1800,3200,0,[10]],
  ["Vino Tinto 750ml",1,11,2200,4000,0,[10]],
  ["Detergente Concentrado 750ml",2,12,900,1600,0,[3]],
  ["Lavandina 1L",2,0,600,1100,0,[3,8]],
  ["Jabon en Polvo 800g",2,1,1400,2400,0,[3]],
  ["Shampoo 400ml",3,2,1600,2900,0,[2]],
  ["Jabon de Tocador x3",3,3,900,1600,0,[2]],
  ["Desodorante Aerosol 150ml",3,4,1200,2200,0,[2]],
  ["Pasta Dental 90g",3,5,700,1300,0,[2]],
  ["Papel Higienico x4",3,6,1100,1900,0,[2,8]],
  ["Leche Entera 1L",4,7,900,1500,1,[4]],
  ["Yogur Bebible 1L",4,8,1100,1900,1,[4]],
  ["Queso Cremoso 500g",4,9,2500,4200,1,[4]],
  ["Manteca 200g",4,10,1400,2400,1,[4]],
  ["Dulce de Leche 400g",4,11,1600,2800,1,[4]],
  ["Pan Lactal 500g",5,12,900,1500,1,[0]],
  ["Facturas x6",5,0,1200,2000,1,[0]],
  ["Galletitas Dulces 300g",5,1,1000,1800,1,[0,5]],
  ["Hamburguesas Congeladas x4",6,2,2200,3800,1,[11]],
  ["Papas Fritas Congeladas 1kg",6,3,1800,3100,1,[11]],
  ["Helado 1L",6,4,2500,4500,1,[11]],
  ["Verdura Congelada 500g",6,5,900,1600,1,[11]],
  ["Vaso de Vidrio x6",7,6,2000,3500,0,[8]],
  ["Snack Papas Fritas 150g",0,7,800,1500,0,[0]],
  ["Mani Salado 200g",0,8,700,1300,0,[0]],
  ["Chocolate en Barra 100g",0,9,900,1600,0,[5]],
  ["Atun en Lata 170g",0,10,1200,2100,0,[6]],
  ["Te en Saquitos x25",0,11,800,1400,0,[7]],
  ["Cafe Molido 250g",0,12,2200,3800,0,[7]],
  ["Alimento para Perro 3kg",0,0,3500,5800,0,[9]],
  ["Gaseosa Cola Zero 2.25L",1,1,1500,2800,0,[1]],
  ["Gaseosa Naranja 2.25L",1,2,1400,2600,0,[1]],
  ["Soda 2L",1,3,400,700,0,[1]],
  ["Agua Saborizada 1.5L",1,4,800,1400,0,[1]],
  ["Cerveza IPA 473ml",1,5,1000,1800,0,[10]],
  ["Suavizante de Ropa 750ml",2,6,1300,2300,0,[3]],
  ["Limpiavidrios 500ml",2,7,800,1400,0,[3]],
  ["Bolsas de Residuo x10",2,8,600,1100,0,[8]],
  ["Acondicionador 400ml",3,9,1500,2700,0,[2]],
  ["Crema Corporal 400ml",3,10,1800,3200,0,[2]],
  ["Leche Descremada 1L",4,11,900,1500,1,[4]],
  ["Queso Rallado 100g",4,12,1100,1900,1,[4]],
  ["Pan Integral 500g",5,0,1000,1700,1,[0]],
  ["Medialunas x6",5,1,1300,2200,1,[0]],
  ["Nuggets de Pollo 400g",6,2,2000,3500,1,[11]],
  ["Empanadas Congeladas x12",6,3,2800,4800,1,[11]],
  ["Taza de Ceramica",7,4,900,1600,0,[8]],
  ["Mate de Calabaza",7,5,1500,2700,0,[8]],
  ["Termo 1L",7,6,4500,7800,0,[8]],
  ["Galletitas Saladas 200g",0,7,700,1200,0,[0]],
  ["Alfajor de Chocolate x1",0,8,500,900,0,[5]],
  ["Gelatina 30g",0,9,400,700,0,[0]],
  ["Choclo en Lata 300g",0,10,900,1600,0,[6]],
  ["Duraznos en Almibar 820g",0,11,1800,3100,0,[6]],
  ["Alimento para Gato 1.5kg",0,12,2800,4800,0,[9]],
];

const FIRST_NAMES = [
  "Ana","Roberto","Marina","Facundo","Valentina","Ezequiel","Camila","Nicolas","Julieta","Federico",
  "Agustina","Sebastian","Micaela","Tomas","Florencia","Ignacio","Rocio","Gaston","Daniela","Matias",
  "Brenda","Cristian","Milagros","Ramiro","Antonella","Emiliano","Yamila","Bruno","Candela","Franco",
  "Ludmila","Maximiliano","Zoe","Lautaro","Guadalupe","Joaquin","Abril","Ivan","Morena","Santino",
  "Lucia","Martin","Sofia","Diego","Carla","Paula","Andres","Elena","Hugo","Nadia",
];
const LAST_NAMES = [
  "Torres","Diaz","Sosa","Paz","Rios","Luna","Ortiz","Herrera","Molina","Vega",
  "Ponce","Ibarra","Suarez","Cabrera","Acuna","Medina","Silva","Correa","Nunez","Aguirre",
  "Ledesma","Moyano","Farias","Quiroga","Bravo","Castro","Reinoso","Peralta","Villalba","Godoy",
  "Barrionuevo","Chavez","Cardozo","Funes","Toledo","Maldonado","Escobar","Ramallo","Salas","Coronel",
  "Fernandez","Gomez","Rodriguez","Alvarez","Juarez","Lopez","Martinez","Perez","Sanchez","Romero",
];

const EMPLEADOS = [
  { id: 100, nombre: "Lucia", apellido: "Fernandez", user: "lfernandez", legajo: "LEG100" },
  { id: 101, nombre: "Martin", apellido: "Gomez", user: "mgomez", legajo: "LEG101" },
  { id: 102, nombre: "Sofia", apellido: "Rodriguez", user: "srodriguez", legajo: "LEG102" },
  { id: 103, nombre: "Diego", apellido: "Alvarez", user: "dalvarez", legajo: "LEG103" },
  { id: 104, nombre: "Carla", apellido: "Juarez", user: "cjuarez", legajo: "LEG104", baja: true },
];

const CTA_CLIENTES = [105, 107, 109, 111, 115]; // 113 cerrada - no vende CtaCte
// Labels por numero_referencia del enum TipoDePago (NO por PK de TiposPago)
const PAGO_LABELS = {
  1: "Efectivo", 2: "Tarjeta de Credito", 3: "Transferencia", 4: "Tarjeta de Debito",
  5: "Cuenta Corriente", 6: "QR", 7: "Cheque", 8: "Otro",
};
const PAGO_CAJA_REFS = [1, 1, 1, 2, 3, 3, 4, 6, 7, 8]; // sin CtaCte
const GASTO_CAT = {
  1: "Servicios", 2: "Alquiler", 3: "Sueldos", 4: "Impuestos",
  5: "Insumos", 6: "Mantenimiento", 7: "Otros",
};

/** Resuelve id_TipoPago por numero_referencia (bootstrap: PK puede != enum). */
function sqlTipoPago(numeroRef) {
  return `(SELECT TOP 1 [id_TipoPago] FROM [dbo].[TiposPago] WHERE [numero_referencia] = ${numeroRef} AND [EstaEliminado] = 0)`;
}

// ---------- State ----------
const products = [];
const lots = [];
const ctaCtes = new Map(); // clienteId -> { id, saldo, limite, estado }
let nextPersona = 100;
let nextProducto = 100;
let nextLote = 100;
let nextCta = 100;
let nextCatProd = 100;
let nextVenta = 100;
let nextDetalle = 100;
let nextDvl = 100;
let nextMov = 100;
let nextPago = 100;
let nextCaja = 100;
let nextGasto = 100;
let nextVl = 100;
let nextOferta = 100;
let nextAut = 100;

function buildCatalog() {
  // productos con stock inicial alto para aguantar 2 años
  for (const def of PRODUCT_DEFS) {
    const [desc, rubro, marca, costo, venta, lote, cats] = def;
    const id = nextProducto++;
    // stock inicial generoso + ruido
    let stock = lote ? randInt(800, 1600) : randInt(1200, 2500);
    const p = {
      id, desc, rubro: 100 + rubro, marca: 100 + marca,
      costo: money(costo * (0.9 + rnd() * 0.3)),
      venta: money(venta * (0.9 + rnd() * 0.3)),
      stock, controlLote: !!lote, cats: cats.map((c) => 100 + c),
      codigo: `COD${id}`, barra: `77912345${String(id).padStart(4, "0")}`,
    };
    products.push(p);
    if (p.controlLote) {
      // Lotes chicos para forzar multi-lote FEFO en una misma venta
      const forceMulti = products.filter((x) => x.controlLote).length === 1; // primer producto con lote
      const nLots = forceMulti ? 5 : randInt(3, 4);
      let left = stock;
      for (let i = 0; i < nLots; i++) {
        const qty = i === nLots - 1 ? left : Math.max(25, Math.floor(left / (nLots - i) * (0.45 + rnd() * 0.4)));
        left -= qty;
        const alta = addDays(START, -randInt(30, 200));
        // FEFO: algunos vencen pronto, otros lejos
        const venc = i === 0
          ? addDays(START, randInt(3, 12))   // por vencer
          : addDays(START, randInt(40, 400));
        lots.push({
          id: nextLote++,
          productoId: id,
          stockInicial: qty + randInt(10, 40),
          stockActual: Math.max(0, qty),
          numero: `LT-${nextLote - 1}`,
          alta, venc,
          vencido: false,
          activo: true,
          desc: i === 0 ? "Lote por vencer" : "Lote activo",
        });
      }
      // Lote vencido (excluido de ventas)
      lots.push({
        id: nextLote++,
        productoId: id,
        stockInicial: 40,
        stockActual: 40,
        numero: `LT-VENC-${id}`,
        alta: addDays(START, -400),
        venc: addDays(START, -15),
        vencido: true,
        activo: false,
        desc: "Lote vencido - no vender",
      });
      // Lote agotado (stock 0)
      lots.push({
        id: nextLote++,
        productoId: id,
        stockInicial: 80,
        stockActual: 0,
        numero: `LT-ZERO-${id}`,
        alta: addDays(START, -120),
        venc: addDays(START, 200),
        vencido: false,
        activo: false,
        desc: "Lote agotado",
      });
      p.stock = lots.filter((l) => l.productoId === id && l.activo && !l.vencido).reduce((s, l) => s + l.stockActual, 0);
    }
  }

  // CtaCte iniciales (saldo negativo = deuda)
  const ctaDefs = [
    { cliente: 105, saldo: -8000, limite: 50000, estado: 1, nombre: "Ana Torres" },
    { cliente: 107, saldo: 0, limite: 30000, estado: 1, nombre: "Marina Sosa" },
    { cliente: 109, saldo: -12000, limite: 50000, estado: 1, nombre: "Valentina Rios" },
    { cliente: 111, saldo: -5000, limite: 20000, estado: 1, nombre: "Camila Ortiz" },
    { cliente: 113, saldo: 0, limite: 15000, estado: 3, nombre: "Julieta Molina (cerrada)" },
    { cliente: 115, saldo: -10000, limite: 40000, estado: 1, nombre: "Agustina Ponce" },
  ];
  for (const c of ctaDefs) {
    const id = nextCta++;
    ctaCtes.set(c.cliente, {
      id, cliente: c.cliente, saldo: c.saldo, saldoInicial: c.saldo, limite: c.limite, estado: c.estado,
      nombre: `Cuenta Corriente ${c.nombre}`, conDeuda: c.saldo < 0,
    });
  }
}

function availableProducts() {
  return products.filter((p) => p.stock > 0);
}

function consumeStock(productoId, qty) {
  const p = products.find((x) => x.id === productoId);
  if (!p || p.stock < qty) return null;
  p.stock -= qty;
  const dvl = [];
  if (p.controlLote) {
    let left = qty;
    // FEFO: activos, no vencidos, con stock; orden por fecha_vencimiento ASC
    const pls = lots
      .filter((l) => l.productoId === productoId && l.activo && !l.vencido && l.stockActual > 0)
      .sort((a, b) => a.venc - b.venc);
    for (const l of pls) {
      if (left <= 0) break;
      const take = Math.min(l.stockActual, left);
      l.stockActual -= take;
      left -= take;
      dvl.push({ productoId, loteId: l.id, cantidad: take });
      if (l.stockActual === 0) l.activo = false;
    }
    if (left > 0) {
      // rollback simple
      p.stock += qty - left;
      return null;
    }
  }
  if (p.stock === 0) p.estado = 4;
  return dvl;
}

function restockIfNeeded() {
  // reposición semanal implícita: si stock bajo, subir
  for (const p of products) {
    if (p.stock < 80) {
      const add = randInt(200, 600);
      p.stock += add;
      p.estado = 1;
      if (p.controlLote) {
        // Dos lotes chicos en restock → multi-lote en ventas posteriores
        const half = Math.floor(add / 2);
        const parts = [half, add - half];
        for (const qty of parts) {
          lots.push({
            id: nextLote++,
            productoId: p.id,
            stockInicial: qty,
            stockActual: qty,
            numero: `LT-${nextLote - 1}`,
            alta: addDays(END, -30),
            venc: addDays(END, randInt(20, 300)),
            vencido: false,
            activo: true,
            desc: "Reposicion",
          });
        }
      }
    }
  }
}

/**
 * Arma pagos de una venta. numeroRef = enum TipoDePago.
 * Reglas del sistema (VentaMontosHelper):
 *   monto_adeudado = Abs(linea CtaCte)
 *   monto_pagado   = Abs(total) - monto_adeudado
 */
function buildSalePayments(total, { allowCta, cta }) {
  const pagos = [];
  let adeudado = 0;
  let pagado = total;
  let esCta = false;
  let mixtos = 0;

  // Full o parcial CtaCte
  if (allowCta && cta && cta.estado === 1) {
    const wantFull = chance(0.55);
    const wantPartial = !wantFull && chance(0.45);
    if (wantFull) {
      const proyectado = cta.saldo - total;
      if (proyectado >= -cta.limite) {
        esCta = true;
        adeudado = total;
        pagado = 0;
        cta.saldo = money(proyectado);
        cta.conDeuda = cta.saldo < 0;
        pagos.push({ numeroRef: 5, monto: total, extra: "Pago Cuenta Corriente" });
        return { pagos, adeudado, pagado, esCta, mixtos: 0 };
      }
    } else if (wantPartial && total >= 1000) {
      const ctaPart = money(Math.min(total * (0.3 + rnd() * 0.4), total - 100));
      const cajaPart = money(total - ctaPart);
      const proyectado = cta.saldo - ctaPart;
      if (proyectado >= -cta.limite && cajaPart > 0) {
        esCta = true;
        adeudado = ctaPart;
        pagado = cajaPart;
        cta.saldo = money(proyectado);
        cta.conDeuda = cta.saldo < 0;
        const refCaja = pick([1, 1, 3, 4]);
        pagos.push({ numeroRef: refCaja, monto: cajaPart, extra: `Pago mixto ${PAGO_LABELS[refCaja]}` });
        pagos.push({ numeroRef: 5, monto: ctaPart, extra: "Pago mixto Cuenta Corriente" });
        return { pagos, adeudado, pagado, esCta, mixtos: 1 };
      }
    }
  }

  // Pago mixto caja (2 medios) ~25%
  if (chance(0.25) && total >= 500) {
    const refA = pick([1, 1, 3]);
    let refB = pick([2, 3, 4, 6]);
    if (refB === refA) refB = refA === 1 ? 3 : 1;
    const partA = money(total * (0.35 + rnd() * 0.3));
    const partB = money(total - partA);
    if (partA > 0 && partB > 0) {
      pagos.push({ numeroRef: refA, monto: partA, extra: `Pago mixto ${PAGO_LABELS[refA]}` });
      pagos.push({ numeroRef: refB, monto: partB, extra: `Pago mixto ${PAGO_LABELS[refB]}` });
      mixtos = 1;
      return { pagos, adeudado: 0, pagado: total, esCta: false, mixtos };
    }
  }

  const ref = pick(PAGO_CAJA_REFS);
  pagos.push({ numeroRef: ref, monto: total, extra: `Pago ${PAGO_LABELS[ref]}` });
  return { pagos, adeudado: 0, pagado: total, esCta: false, mixtos: 0 };
}

// ---------- SQL writers ----------
function SqlWriter(filePath) {
  fs.mkdirSync(path.dirname(filePath), { recursive: true });
  const stream = fs.createWriteStream(filePath, { encoding: "utf8" });
  let buf = "";
  const flush = () => {
    if (buf.length > 200000) {
      stream.write(buf);
      buf = "";
    }
  };
  return {
    w(line) {
      buf += line + "\n";
      flush();
    },
    end() {
      return new Promise((res, rej) => {
        stream.write(buf, (err) => {
          if (err) return rej(err);
          stream.end(res);
        });
      });
    },
  };
}

function writeIdentityBlock(writer, table, cols, rows) {
  if (!rows.length) return;
  const w = typeof writer === "function" ? writer : writer.w.bind(writer);
  w(`SET IDENTITY_INSERT [dbo].[${table}] ON`);
  for (const r of rows) {
    w(`INSERT INTO [dbo].[${table}] (${cols}) VALUES (${r})`);
  }
  w(`SET IDENTITY_INSERT [dbo].[${table}] OFF`);
  w("GO");
  const last = rows[rows.length - 1].split(",")[0].trim();
  w(`DBCC CHECKIDENT ('[dbo].[${table}]', RESEED, ${last})`);
  w("GO");
}

// ---------- Generate month simulation ----------
async function main() {
  console.log(`Generando seed ${DAYS} dias: ${ymd(START)} -> ${ymd(END)}`);
  console.log(`Avg ventas/dia ~${AVG_VENTAS}, seed=${SEED}, DB=${DB}`);
  fs.mkdirSync(MESES, { recursive: true });

  buildCatalog();

  // Ofertas con vigencia real (tipo_oferta=2 Producto; % vs fijo vía columnas)
  const mid = addDays(START, Math.floor(DAYS / 2));
  const findProd = (substr) => products.find((p) => p.desc.includes(substr) && !p.desc.includes("Zero"))?.id
    ?? products.find((p) => p.desc.includes(substr))?.id
    ?? products[0].id;
  const ofertas = [
    {
      id: nextOferta++, codigo: "OF-COLA-10", desc: "10% Gaseosa Cola 2.25L (vigente)",
      tipo: 2, pct: 10, precio: null, prod: findProd("Gaseosa Cola 2.25L"),
      inicio: START, fin: END, activa: true,
    },
    {
      id: nextOferta++, codigo: "OF-YERBA-PF", desc: "Precio fijo Yerba Mate 1kg (vigente)",
      tipo: 2, pct: null, precio: 3999.99, prod: findProd("Yerba Mate"),
      inicio: START, fin: addDays(END, 60), activa: true,
    },
    {
      id: nextOferta++, codigo: "OF-SNACK-TEMP", desc: "10% Snack Papas (temporada 1er semestre)",
      tipo: 2, pct: 10, precio: null, prod: findProd("Snack Papas"),
      inicio: START, fin: mid, activa: true,
    },
    {
      id: nextOferta++, codigo: "OF-ARROZ-VENC", desc: "15% Arroz (VENCIDA)",
      tipo: 2, pct: 15, precio: null, prod: findProd("Arroz"),
      inicio: addDays(START, -120), fin: addDays(START, -5), activa: false,
    },
    {
      id: nextOferta++, codigo: "OF-ACEITE-FUT", desc: "8% Aceite (FUTURA)",
      tipo: 2, pct: 8, precio: null, prod: findProd("Aceite"),
      inicio: addDays(END, -10), fin: addDays(END, 90), activa: true,
    },
    {
      id: nextOferta++, codigo: "OF-LECHE-OFF", desc: "12% Leche (INACTIVA)",
      tipo: 2, pct: 12, precio: null, prod: findProd("Leche Entera"),
      inicio: START, fin: END, activa: false,
    },
  ];
  // stats por oferta+producto (OfertaProductoEstadisticas)
  const ofertaStats = new Map(); // key `${oid}:${pid}` -> { oid, pid, qty, costo, venta, oferta, last }

  const stats = {
    ventas: 0, cancel: 0, cta: 0, ofertas: 0, vl: 0, gastos: 0, dias: 0, mixtos: 0,
  };

  // Buffers per month
  let monthKey = null;
  let month = null;

  function ensureMonth(d) {
    const key = ym(d);
    if (key === monthKey) return month;
    // flush previous handled by caller
    monthKey = key;
    month = {
      key,
      cajas: [], ventas: [], detalles: [], dvl: [], movs: [], pagos: [],
      gastos: [], vl: [],
    };
    return month;
  }

  const monthFiles = [];
  let saldoCajaCarry = 20000;

  async function flushMonth(m) {
    if (!m || (!m.ventas.length && !m.cajas.length)) return;
    const file = path.join(MESES, `${m.key}.sql`);
    monthFiles.push(file);
    const writer = SqlWriter(file);
    const w = (line) => writer.w(line);
    w(`USE [${DB}]`);
    w("GO");
    w(`/* Seed mensual ${m.key} - generado QueryData3.0 */`);
    w("SET NOCOUNT ON;");
    w("GO");

    // Cajas
    writeIdentityBlock(
      w, "Cajas",
      "[CajaId],[saldo_inicial],[saldo_actual],[fecha_apertura],[fecha_cierre],[total_ingresos],[total_egresos],[balance_final],[empleado_apertura],[empleado_cierre],[esta_cerrada]",
      m.cajas.map((c) =>
        `${c.id}, ${fmt(c.saldoInicial)}, ${fmt(c.saldoActual)}, ${sqlD(c.apertura)}, ${sqlD(c.cierre)}, ${fmt(c.ingresos)}, ${fmt(c.egresos)}, ${fmt(c.saldoActual)}, ${c.empA}, ${c.empC}, 1`
      )
    );

    // Ventas need @ConsumidorFinalId
    if (m.ventas.length) {
      w("SET IDENTITY_INSERT [dbo].[Ventas] ON");
      w("DECLARE @ConsumidorFinalId BIGINT;");
      w("SELECT @ConsumidorFinalId = PersonaId FROM dbo.Clientes WHERE numero_cliente = '0';");
      w("IF @ConsumidorFinalId IS NULL RAISERROR('Falta Consumidor Final (numero_cliente=0). Ejecuta la app una vez antes del seed.', 16, 1);");
      for (const v of m.ventas) {
        const cli = v.clienteId == null ? "@ConsumidorFinalId" : String(v.clienteId);
        w(`INSERT INTO [dbo].[Ventas] ([id_Venta],[id_Empleado],[numero_venta],[fecha_venta],[total],[estado],[detalle],[descuento],[id_Vendedor],[monto_adeudado],[monto_pagado],[total_sin_descuento],[id_cliente]) VALUES (${v.id}, ${v.emp}, ${sqlN(v.numero)}, ${sqlD(v.fecha)}, ${fmt(v.total)}, ${v.estado}, ${sqlN(v.detalle)}, ${fmt(v.descuento)}, ${v.vend}, ${fmt(v.adeudado)}, ${fmt(v.pagado)}, ${fmt(v.totalSin)}, ${cli})`);
      }
      w("SET IDENTITY_INSERT [dbo].[Ventas] OFF");
      w("GO");
      w(`DBCC CHECKIDENT ('[dbo].[Ventas]', RESEED, ${m.ventas[m.ventas.length - 1].id})`);
      w("GO");
    }

    writeIdentityBlock(
      w, "DetallesVenta",
      "[id_DetalleVenta],[id_Venta],[id_Producto],[cantidad],[subtotal],[id_OfertaDescuento],[descripcion],[es_oferta],[es_oferta_por_grupo],[precio_unitario_final],[precio_unitario_original]",
      m.detalles.map((d) =>
        `${d.id}, ${d.ventaId}, ${d.productoId}, ${d.cantidad}, ${fmt(d.subtotal)}, ${d.ofertaId ?? "NULL"}, ${sqlN(d.desc)}, ${d.esOferta}, 0, ${fmt(d.pFinal)}, ${fmt(d.pOrig)}`
      )
    );

    writeIdentityBlock(
      w, "DetalleVentaLote",
      "[DetalleVentaLoteId],[id_Producto],[id_Venta],[id_Lote],[cantidad]",
      m.dvl.map((d) => `${d.id}, ${d.productoId}, ${d.ventaId}, ${d.loteId}, ${d.cantidad}`)
    );

    // Gastos y VentasLibres ANTES de VentaPagoDetalles (FK dependency)
    writeIdentityBlock(
      w, "Gastos",
      "[id_Gasto],[numero_gasto],[id_Empleado],[categoria_gasto],[fecha_gasto],[fecha_registro],[monto_total],[monto_pagado],[estado_gasto],[detalle]",
      m.gastos.map((g) =>
        `${g.id}, ${sqlN(g.numero)}, ${g.emp}, ${g.cat}, ${sqlD(g.fecha)}, ${sqlD(g.fecha)}, ${fmt(g.total)}, ${fmt(g.pagado)}, ${g.estado}, ${sqlN(g.detalle)}`
      )
    );

    writeIdentityBlock(
      w, "VentasLibres",
      "[id_VentaLibre],[id_Empleado],[id_Vendedor],[id_cliente],[numero_venta],[fecha_venta],[total],[estado],[detalle],[monto_adeudado],[monto_pagado]",
      m.vl.map((v) =>
        `${v.id}, ${v.emp}, ${v.vend}, ${v.clienteId ?? "NULL"}, ${sqlN(v.numero)}, ${sqlD(v.fecha)}, ${fmt(v.total)}, 0, ${sqlN(v.detalle)}, 0.00, ${fmt(v.total)}`
      )
    );

    writeIdentityBlock(
      w, "Movimientos",
      "[id_movimiento],[entidad_id],[tipo_movimiento],[esta_eliminado],[fecha_movimiento],[monto],[numero_movimiento],[tipo_movimiento_detalle],[tipo_entidad]",
      m.movs.map((x) =>
        `${x.id}, ${x.entidadId}, ${x.tipo}, 0, ${sqlD(x.fecha)}, ${fmt(x.monto)}, ${sqlN(x.numero)}, ${x.detalle}, ${x.entidad}`
      )
    );

    // VentaPagoDetalles al final: depende de Ventas, Gastos y VentasLibres
    // id_TipoPago se resuelve por numero_referencia (PK bootstrap != enum)
    if (m.pagos.length) {
      w("SET IDENTITY_INSERT [dbo].[VentaPagoDetalles] ON");
      for (const p of m.pagos) {
        w(`INSERT INTO [dbo].[VentaPagoDetalles] ([id_VentaPagoDetalle],[id_Venta],[id_TipoPago],[monto],[id_Gasto],[IdVentaLibre],[extra_descripcion_pago]) VALUES (${p.id}, ${p.ventaId ?? "NULL"}, ${sqlTipoPago(p.numeroRef)}, ${fmt(p.monto)}, ${p.gastoId ?? "NULL"}, ${p.vlId ?? "NULL"}, ${sqlN(p.extra)})`);
      }
      w("SET IDENTITY_INSERT [dbo].[VentaPagoDetalles] OFF");
      w("GO");
      w(`DBCC CHECKIDENT ('[dbo].[VentaPagoDetalles]', RESEED, ${m.pagos[m.pagos.length - 1].id})`);
      w("GO");
    }

    w(`PRINT N'Mes ${m.key} cargado.';`);
    w("GO");
    await writer.end();
    console.log(`  wrote ${m.key}.sql  ventas=${m.ventas.length} cajas=${m.cajas.length}`);
  }

  let currentMonth = null;
  const activosEmp = EMPLEADOS.filter((e) => !e.baja).map((e) => e.id);
  const clientesActivos = [];
  for (let i = 105; i <= 144; i++) {
    if (![113, 118, 123, 130, 134, 142].includes(i)) clientesActivos.push(i);
  }

  for (let dayIdx = 0; dayIdx < DAYS; dayIdx++) {
    const day = addDays(START, dayIdx);
    if (day.getDay() === 1) restockIfNeeded(); // lunes

    const m = ensureMonth(day);
    if (currentMonth && currentMonth.key !== m.key) {
      await flushMonth(currentMonth);
    }
    currentMonth = m;
    stats.dias++;

    const dow = day.getDay(); // 0 sun
    let nVentas = AVG_VENTAS;
    if (dow === 0) nVentas = Math.round(AVG_VENTAS * 0.55);
    else if (dow === 6) nVentas = Math.round(AVG_VENTAS * 1.15);
    nVentas = Math.max(8, nVentas + randInt(-5, 6));

    const empA = pick(activosEmp);
    const empC = pick(activosEmp);
    const apertura = new Date(day);
    apertura.setHours(9, randInt(0, 50), randInt(0, 59), 0);
    const cierre = new Date(day);
    cierre.setHours(23, randInt(0, 50), randInt(0, 59), 0);

    let ingresos = 0;
    let egresos = 0;
    const cajaId = nextCaja++;
    const dayVentas = [];

    for (let i = 0; i < nVentas; i++) {
      const avail = availableProducts();
      if (avail.length < 2) break;

      const nItems = randInt(1, 4);
      const items = [];
      let totalSin = 0;
      let total = 0;
      let ofertaUsed = null;

      for (let k = 0; k < nItems; k++) {
        const p = pick(avail);
        const qty = randInt(1, 3);
        if (p.stock < qty) continue;
        let pOrig = p.venta;
        let pFinal = pOrig;
        let ofertaId = null;
        let esOferta = 0;

        const of = ofertas.find((o) =>
          o.prod === p.id
          && o.activa
          && day >= o.inicio
          && (!o.fin || day <= o.fin)
        );
        if (of && chance(0.4)) {
          if (of.pct != null) pFinal = money(pOrig * (1 - of.pct / 100));
          else if (of.precio != null) pFinal = of.precio;
          ofertaId = of.id;
          esOferta = 1;
          ofertaUsed = of;
          stats.ofertas++;
          const key = `${of.id}:${p.id}`;
          const st = ofertaStats.get(key) || {
            oid: of.id, pid: p.id, qty: 0, costo: 0, venta: 0, oferta: 0, last: day,
          };
          st.qty += qty;
          st.costo = money(st.costo + p.costo * qty);
          st.venta = money(st.venta + pOrig * qty);
          st.oferta = money(st.oferta + pFinal * qty);
          st.last = day;
          ofertaStats.set(key, st);
        }

        const dvlParts = consumeStock(p.id, qty);
        if (!dvlParts) continue;

        const sub = money(pFinal * qty);
        totalSin += money(pOrig * qty);
        total += sub;
        items.push({
          productoId: p.id, qty, pOrig, pFinal, sub, ofertaId, esOferta, desc: p.desc, dvlParts,
        });
      }
      if (!items.length) continue;

      total = money(total);
      totalSin = money(totalSin);
      const descuento = money(Math.max(0, totalSin - total));

      // descuento ticket extra ocasional
      let extraDesc = 0;
      if (!ofertaUsed && chance(0.08)) {
        extraDesc = money(total * (0.05 + rnd() * 0.07));
        total = money(total - extraDesc);
      }
      descuento; // already from ofertas
      const descTotal = money(totalSin - total);

      const fecha = new Date(day);
      const mins = 9 * 60 + Math.floor(((14 * 60) * (i + 1)) / (nVentas + 1)) + randInt(0, 4);
      fecha.setHours(Math.floor(mins / 60), mins % 60, randInt(0, 59), 0);

      const emp = pick(activosEmp);
      const vend = pick(activosEmp);
      const esMostrador = chance(0.55);
      let clienteId = null;
      if (!esMostrador) {
        // sesgar hacia clientes con CtaCte para tener historial de ese medio
        clienteId = chance(0.45) ? pick(CTA_CLIENTES) : pick(clientesActivos);
      }

      // Pagos (simples, mixtos caja, CtaCte full/parcial) — ids por numero_referencia
      let adeudado = 0;
      let pagado = total;
      let esCta = false;
      let salePagos = [];

      const allowCta = !!(clienteId && CTA_CLIENTES.includes(clienteId));
      const cta = allowCta ? ctaCtes.get(clienteId) : null;
      const pay = buildSalePayments(total, { allowCta, cta });
      salePagos = pay.pagos;
      adeudado = pay.adeudado;
      pagado = pay.pagado;
      esCta = pay.esCta;
      if (esCta) stats.cta++;
      if (pay.mixtos) stats.mixtos++;

      const ventaId = nextVenta++;
      const numero = `VEN-${ymd(day)}-${String(i + 1).padStart(3, "0")}`;
      const venta = {
        id: ventaId, emp, vend, numero, fecha, total, estado: 0,
        detalle: esMostrador ? "Venta de mostrador" : "Venta a cliente registrado",
        descuento: descTotal, adeudado, pagado, totalSin, clienteId,
        items, esCta, salePagos,
      };
      dayVentas.push(venta);
      m.ventas.push(venta);
      stats.ventas++;

      for (const it of items) {
        const did = nextDetalle++;
        m.detalles.push({
          id: did, ventaId, productoId: it.productoId, cantidad: it.qty,
          subtotal: it.sub, ofertaId: it.ofertaId, desc: it.desc,
          esOferta: it.esOferta, pFinal: it.pFinal, pOrig: it.pOrig,
        });
        for (const d of it.dvlParts) {
          m.dvl.push({
            id: nextDvl++, productoId: d.productoId, ventaId, loteId: d.loteId, cantidad: d.cantidad,
          });
        }
      }

      // Movimiento venta
      m.movs.push({
        id: nextMov++, entidadId: ventaId, tipo: 1, fecha, monto: total,
        numero: `MOV-VENTA-${ventaId}-${ymd(day)}${String(fecha.getHours()).padStart(2,"0")}${String(fecha.getMinutes()).padStart(2,"0")}${String(fecha.getSeconds()).padStart(2,"0")}`,
        detalle: 4, entidad: 1,
      });
      if (pagado > 0) ingresos += pagado;

      if (esCta && adeudado > 0) {
        const ctaAcc = ctaCtes.get(clienteId);
        m.movs.push({
          id: nextMov++, entidadId: ctaAcc.id, tipo: 2, fecha, monto: adeudado,
          numero: `MOV${fmt(adeudado)}CTACTE`, detalle: 2, entidad: 2,
        });
      }

      for (const payLine of salePagos) {
        m.pagos.push({
          id: nextPago++, ventaId, numeroRef: payLine.numeroRef, monto: payLine.monto,
          gastoId: null, vlId: null, extra: payLine.extra.slice(0, 50),
        });
      }
    }

    // Cancelaciones ~2%
    const cancelCandidates = dayVentas.filter((v) => !v.esCta && chance(0.025));
    for (const orig of cancelCandidates) {
      orig.estado = 10;
      const canId = nextVenta++;
      const canFecha = new Date(orig.fecha);
      canFecha.setHours(Math.min(22, canFecha.getHours() + randInt(1, 3)), randInt(0, 59), randInt(0, 59), 0);
      const can = {
        id: canId, emp: orig.emp, vend: orig.vend,
        numero: `CAN-${ymd(day)}-${String(stats.cancel + 1).padStart(3, "0")}`,
        fecha: canFecha, total: orig.total, estado: 99,
        detalle: `Cancelacion de venta N. ${orig.numero}`,
        descuento: orig.descuento, adeudado: 0, pagado: orig.pagado,
        totalSin: orig.totalSin, clienteId: orig.clienteId,
      };
      m.ventas.push(can);
      stats.cancel++;

      for (const it of orig.items) {
        m.detalles.push({
          id: nextDetalle++, ventaId: canId, productoId: it.productoId, cantidad: it.qty,
          subtotal: it.sub, ofertaId: it.ofertaId, desc: it.desc,
          esOferta: it.esOferta, pFinal: it.pFinal, pOrig: it.pOrig,
        });
        // stock return for cancel
        const p = products.find((x) => x.id === it.productoId);
        if (p) {
          p.stock += it.qty;
          p.estado = 1;
        }
      }

      m.movs.push({
        id: nextMov++, entidadId: canId, tipo: 2, fecha: canFecha, monto: orig.total,
        numero: `MOV-CAN-${canId}-${ymd(day)}`, detalle: 1, entidad: 1,
      });
      egresos += orig.pagado;

      for (const line of (orig.salePagos || [])) {
        // Solo reverso de medios de caja (no CtaCte: estas cancelaciones ya filtran !esCta)
        m.pagos.push({
          id: nextPago++, ventaId: canId, numeroRef: line.numeroRef, monto: -Math.abs(line.monto),
          gastoId: null, vlId: null, extra: "Reverso por cancelacion",
        });
      }
    }

    // Pago CtaCte ocasional (carga saldo)
    if (chance(0.2)) {
      const cli = pick(CTA_CLIENTES);
      const cta = ctaCtes.get(cli);
      if (cta && cta.estado === 1 && cta.saldo < 0) {
        const pago = money(Math.min(-cta.saldo, randInt(2000, 15000)));
        cta.saldo = money(cta.saldo + pago);
        cta.conDeuda = cta.saldo < 0;
        const f = new Date(day);
        f.setHours(17, randInt(0, 40), 0, 0);
        m.movs.push({
          id: nextMov++, entidadId: cta.id, tipo: 1, fecha: f, monto: pago,
          numero: `MOV${fmt(pago)}CTACTE`, detalle: 2, entidad: 2,
        });
        ingresos += pago; // pago de deuda entra a caja
      }
    }

    // Venta libre 1-2 / semana
    if (dow === 3 || (dow === 5 && chance(0.5))) {
      const vlId = nextVl++;
      const total = money(randInt(800, 5000));
      const f = new Date(day);
      f.setHours(18, randInt(0, 40), 0, 0);
      const emp = pick(activosEmp);
      m.vl.push({
        id: vlId, emp, vend: pick(activosEmp), clienteId: chance(0.4) ? pick(clientesActivos) : null,
        numero: `VL-${ymd(day)}-001`, fecha: f, total,
        detalle: pick(["Servicio de entrega", "Cargo especial", "Armado de pedido", "Flete"]),
      });
      stats.vl++;
      m.pagos.push({
        id: nextPago++, ventaId: null, numeroRef: pick([1, 3]), monto: total,
        gastoId: null, vlId, extra: "Pago VentaLibre",
      });
      m.movs.push({
        id: nextMov++, entidadId: vlId, tipo: 1, fecha: f, monto: total,
        numero: `MOV-VL-${vlId}-${ymd(day)}`, detalle: 7, entidad: 6,
      });
      ingresos += total;
    }

    // Gasto diario ~70%
    if (chance(0.7)) {
      const cat = pick([1, 2, 3, 4, 5, 6, 7]);
      const total = money(cat === 3 ? randInt(80000, 250000) : cat === 2 ? randInt(40000, 90000) : randInt(2000, 25000));
      const pagadoEstado = chance(0.85);
      const gId = nextGasto++;
      const f = new Date(day);
      f.setHours(randInt(11, 20), randInt(0, 59), 0, 0);
      m.gastos.push({
        id: gId, numero: `GAS-${ymd(day)}-001`, emp: pick(activosEmp), cat, fecha: f,
        total, pagado: pagadoEstado ? total : 0, estado: pagadoEstado ? 21 : 22,
        detalle: `${GASTO_CAT[cat]}: gasto operativo del dia`,
      });
      stats.gastos++;
      if (pagadoEstado) {
        egresos += total;
        m.pagos.push({
          id: nextPago++, ventaId: null, numeroRef: pick([1, 3]), monto: total,
          gastoId: gId, vlId: null, extra: "Pago de gasto",
        });
        m.movs.push({
          id: nextMov++, entidadId: gId, tipo: 2, fecha: f, monto: total,
          numero: `MOV-GASTO-${gId}-${ymd(day)}`, detalle: 6, entidad: 5,
        });
      }
    }

    ingresos = money(ingresos);
    egresos = money(egresos);
    const saldoInicial = saldoCajaCarry;
    const saldoActual = money(saldoInicial + ingresos - egresos);
    saldoCajaCarry = saldoActual;

    m.cajas.push({
      id: cajaId, saldoInicial, saldoActual, apertura, cierre, ingresos, egresos, empA, empC,
    });

    if (dayIdx % 60 === 0) {
      console.log(`  dia ${dayIdx + 1}/${DAYS} ${ym(day)} ventas=${stats.ventas}`);
    }
  }

  await flushMonth(currentMonth);

  // Sync lote product stock
  for (const p of products) {
    if (p.controlLote) {
      p.stock = lots.filter((l) => l.productoId === p.id && l.activo && !l.vencido)
        .reduce((s, l) => s + l.stockActual, 0);
      p.estado = p.stock === 0 ? 4 : 1;
    }
  }

  await writeCatalog(ofertas, ofertaStats);
  await writeCtaCteFinal();
  await writeDemoPresentacion();
  await writeValidaciones();
  await writeLimpiar();
  await writeReadme(monthFiles, stats);
  await writeRunAll(monthFiles);

  console.log("\n=== RESUMEN ===");
  console.log(stats);
  console.log("CtaCte finales:");
  for (const c of ctaCtes.values()) {
    console.log(`  cta ${c.id} cli=${c.cliente} saldo=${fmt(c.saldo)} (ini ${fmt(c.saldoInicial)})`);
  }
  console.log(`Meses: ${monthFiles.length}`);
  console.log(`Salida: ${OUT}`);
}

async function writeCatalog(ofertas, ofertaStats) {
  const writer = SqlWriter(path.join(OUT, "00_catalogo.sql"));
  const w = (line) => writer.w(line);
  w(`USE [${DB}]`);
  w("GO");
  w(`/* QueryData3.0 catalogo - ${DAYS} dias ${ymd(START)}..${ymd(END)} */`);
  w("SET NOCOUNT ON;");
  w("GO");

  // Personas empleados + clientes
  w("SET IDENTITY_INSERT [dbo].[Personas] ON");
  for (const e of EMPLEADOS) {
    w(`INSERT INTO [dbo].[Personas] ([PersonaId],[Nombre],[Apellido],[Dni],[Cuil],[Telefono],[Telefono2],[Email],[Direccion],[FechaNacimiento],[EstaEliminado]) VALUES (${e.id}, ${sqlN(e.nombre)}, ${sqlN(e.apellido)}, N'${30000000 + e.id}', N'20-${30000000 + e.id}-1', N'381450${e.id}', NULL, ${sqlN(e.user + "@tienda.com")}, N'San Martin ${e.id}', '1990-01-01', 0)`);
  }
  for (let i = 0; i < 40; i++) {
    const id = 105 + i;
    const nom = FIRST_NAMES[i];
    const ape = LAST_NAMES[i];
    w(`INSERT INTO [dbo].[Personas] ([PersonaId],[Nombre],[Apellido],[Dni],[Cuil],[Telefono],[Telefono2],[Email],[Direccion],[FechaNacimiento],[EstaEliminado]) VALUES (${id}, ${sqlN(nom)}, ${sqlN(ape)}, N'${20000000 + id}', N'27-${20000000 + id}-2', N'3814${100000 + id}', NULL, ${sqlN(nom.toLowerCase() + "." + ape.toLowerCase() + "@mail.com")}, N'Calle Falsa ${id}', '1985-06-15', 0)`);
  }
  w("SET IDENTITY_INSERT [dbo].[Personas] OFF");
  w("GO");
  w("DBCC CHECKIDENT ('[dbo].[Personas]', RESEED, 144)");
  w("GO");

  for (const e of EMPLEADOS) {
    const estado = e.baja ? 1 : 3;
    const hab = e.baja ? 0 : 1;
    w(`INSERT INTO [dbo].[Empleados] ([PersonaId],[legajo],[fechaIngreso],[fechaEgreso],[estado],[username],[pass],[usuarioestahabilitado]) VALUES (${e.id}, ${sqlN(e.legajo)}, '2024-01-01', NULL, ${estado}, ${sqlN(e.user)}, NULL, ${hab})`);
  }
  w("GO");

  w("SET IDENTITY_INSERT [dbo].[Roles] ON");
  w("INSERT INTO [dbo].[Roles] ([id_Rol],[nombre],[detalleRol],[codigo_rol],[EstaEliminado]) VALUES (100, N'Vendedor', N'Ventas y stock', N'ROL_VENDEDOR', 0)");
  w("INSERT INTO [dbo].[Roles] ([id_Rol],[nombre],[detalleRol],[codigo_rol],[EstaEliminado]) VALUES (101, N'Cajero', N'Caja y cobros', N'ROL_CAJERO', 0)");
  w("SET IDENTITY_INSERT [dbo].[Roles] OFF");
  w("GO");
  w("DBCC CHECKIDENT ('[dbo].[Roles]', RESEED, 101)");
  w("GO");

  // Roles_Permisos (permisos bootstrap por codigo; no toca SADMIN)
  const PERMS_VENDEDOR = [
    "Ventas.Ver", "Ventas.Crear", "Ventas.Cobrar", "Ventas.VerDetalle",
    "Productos.Ver", "Stock.Ver", "Lotes.Ver",
    "Clientes.Ver", "CuentasCorrientes.Ver", "Ofertas.Ver",
    "Empleados.AsignarVendedor", "Notificaciones.Ver",
  ];
  const PERMS_CAJERO = [
    "Ventas.Ver", "Ventas.Crear", "Ventas.Cobrar", "Ventas.VerDetalle",
    "Caja.Ver", "Caja.Abrir", "Caja.Cerrar", "Caja.VerMovimientos",
    "Productos.Ver", "Clientes.Ver", "Notificaciones.Ver",
  ];
  for (const codigo of PERMS_VENDEDOR) {
    w(`INSERT INTO [dbo].[Roles_Permisos] ([id_rol],[id_permiso]) SELECT 100, p.[id_permiso] FROM [dbo].[Permisos] p WHERE p.[codigo] = ${sqlN(codigo)} AND NOT EXISTS (SELECT 1 FROM [dbo].[Roles_Permisos] rp WHERE rp.[id_rol]=100 AND rp.[id_permiso]=p.[id_permiso]);`);
  }
  for (const codigo of PERMS_CAJERO) {
    w(`INSERT INTO [dbo].[Roles_Permisos] ([id_rol],[id_permiso]) SELECT 101, p.[id_permiso] FROM [dbo].[Permisos] p WHERE p.[codigo] = ${sqlN(codigo)} AND NOT EXISTS (SELECT 1 FROM [dbo].[Roles_Permisos] rp WHERE rp.[id_rol]=101 AND rp.[id_permiso]=p.[id_permiso]);`);
  }
  w("GO");

  w("SET IDENTITY_INSERT [dbo].[Empleados_Roles] ON");
  w("INSERT INTO [dbo].[Empleados_Roles] ([EmpleadoRolId],[IdEmpleado],[IdRol],[FechaAsignacion]) VALUES (100, 100, 100, '2024-01-05')");
  w("INSERT INTO [dbo].[Empleados_Roles] ([EmpleadoRolId],[IdEmpleado],[IdRol],[FechaAsignacion]) VALUES (101, 101, 100, '2024-01-05')");
  w("INSERT INTO [dbo].[Empleados_Roles] ([EmpleadoRolId],[IdEmpleado],[IdRol],[FechaAsignacion]) VALUES (102, 102, 101, '2024-01-05')");
  w("INSERT INTO [dbo].[Empleados_Roles] ([EmpleadoRolId],[IdEmpleado],[IdRol],[FechaAsignacion]) VALUES (103, 103, 101, '2024-01-05')");
  w("SET IDENTITY_INSERT [dbo].[Empleados_Roles] OFF");
  w("GO");
  w("DBCC CHECKIDENT ('[dbo].[Empleados_Roles]', RESEED, 103)");
  w("GO");

  const baja = new Set([113, 118, 123, 130, 134, 142]);
  const inh = new Set([113, 123, 130]);
  for (let id = 105; id <= 144; id++) {
    let estado = 1;
    let desc = "Cliente activo";
    let fb = "NULL";
    if (baja.has(id)) {
      estado = inh.has(id) ? 3 : 2;
      desc = inh.has(id) ? "Cliente inhabilitado" : "Cliente dado de baja";
      fb = "'2025-11-01'";
    }
    w(`INSERT INTO [dbo].[Clientes] ([PersonaId],[CuentaCorrienteId],[numero_cliente],[fecha_alta],[fecha_baja],[estado],[estado_descripcion]) VALUES (${id}, NULL, N'CLI-${id}', '2024-02-01', ${fb}, ${estado}, ${sqlN(desc)})`);
  }
  w("GO");

  w("SET IDENTITY_INSERT [dbo].[CuentasCorrientes] ON");
  for (const c of ctaCtes.values()) {
    const fv = c.estado === 3 ? "NULL" : "'2027-12-31'";
    const limAct = c.estado === 3 ? 0 : 1;
    w(`INSERT INTO [dbo].[CuentasCorrientes] ([CuentaCorrienteId],[saldo],[limite_deuda],[esta_eliminado],[fecha_vencimiento],[limite_deuda_activo],[nombre_cuenta_corriente],[EstadoCuentaCorriente],[ClienteId],[con_deuda],[tipo_vencimiento],[cantidad_meses_vencimiento],[fecha_creacion],[fecha_activacion]) VALUES (${c.id}, ${fmt(c.saldoInicial)}, ${fmt(c.limite)}, 0, ${fv}, ${limAct}, ${sqlN(c.nombre)}, ${c.estado}, ${c.cliente}, ${c.saldoInicial < 0 ? 1 : 0}, 1, 12, '2024-02-01', '2024-02-01')`);
  }
  w("SET IDENTITY_INSERT [dbo].[CuentasCorrientes] OFF");
  w("GO");
  w(`DBCC CHECKIDENT ('[dbo].[CuentasCorrientes]', RESEED, ${nextCta - 1})`);
  w("GO");
  for (const c of ctaCtes.values()) {
    w(`UPDATE [dbo].[Clientes] SET [CuentaCorrienteId] = ${c.id} WHERE [PersonaId] = ${c.cliente}`);
  }
  w("GO");

  w("SET IDENTITY_INSERT [dbo].[CuentaCorrienteAutorizados] ON");
  let autId = 100;
  for (const c of ctaCtes.values()) {
    if (c.estado === 3) continue;
    w(`INSERT INTO [dbo].[CuentaCorrienteAutorizados] ([CuentaCorrienteAutorizadoId],[CuentaCorrienteId],[dni]) VALUES (${autId++}, ${c.id}, N'${20000000 + c.cliente}')`);
  }
  w("SET IDENTITY_INSERT [dbo].[CuentaCorrienteAutorizados] OFF");
  w("GO");
  w(`DBCC CHECKIDENT ('[dbo].[CuentaCorrienteAutorizados]', RESEED, ${autId - 1})`);
  w("GO");

  w("SET IDENTITY_INSERT [dbo].[Marcas] ON");
  MARCAS.forEach((n, i) => w(`INSERT INTO [dbo].[Marcas] ([MarcaId],[Nombre],[EstaEliminado]) VALUES (${100 + i}, ${sqlN(n)}, 0)`));
  w("SET IDENTITY_INSERT [dbo].[Marcas] OFF");
  w("GO");
  w(`DBCC CHECKIDENT ('[dbo].[Marcas]', RESEED, ${99 + MARCAS.length})`);
  w("GO");

  w("SET IDENTITY_INSERT [dbo].[Rubros] ON");
  RUBROS.forEach((n, i) => w(`INSERT INTO [dbo].[Rubros] ([RubroId],[Nombre],[EstaEliminado]) VALUES (${100 + i}, ${sqlN(n)}, 0)`));
  w("SET IDENTITY_INSERT [dbo].[Rubros] OFF");
  w("GO");
  w(`DBCC CHECKIDENT ('[dbo].[Rubros]', RESEED, ${99 + RUBROS.length})`);
  w("GO");

  w("SET IDENTITY_INSERT [dbo].[Categorias] ON");
  CATEGORIAS.forEach((n, i) => w(`INSERT INTO [dbo].[Categorias] ([CategoriaId],[Nombre],[EstaEliminado]) VALUES (${100 + i}, ${sqlN(n)}, 0)`));
  w("SET IDENTITY_INSERT [dbo].[Categorias] OFF");
  w("GO");
  w(`DBCC CHECKIDENT ('[dbo].[Categorias]', RESEED, ${99 + CATEGORIAS.length})`);
  w("GO");

  w("SET IDENTITY_INSERT [dbo].[Productos] ON");
  for (const p of products) {
    const estado = p.stock === 0 ? 4 : 1;
    w(`INSERT INTO [dbo].[Productos] ([ProductoId],[id_Marca],[stock],[precio_costo],[precio_venta],[descripcion],[esta_eliminado],[estado],[medida],[unidad_medida],[id_Rubro],[codigo],[codigo_barra],[iva_inluido_precio_final],[es_fraccionable],[control_por_lote],[tiene_vencimiento]) VALUES (${p.id}, ${p.marca}, ${p.stock}, ${fmt(p.costo)}, ${fmt(p.venta)}, ${sqlN(p.desc)}, 0, ${estado}, N'unidad', N'unidad', ${p.rubro}, ${sqlN(p.codigo)}, ${sqlN(p.barra)}, 1, 0, ${p.controlLote ? 1 : 0}, ${p.controlLote ? 1 : 0})`);
  }
  w("SET IDENTITY_INSERT [dbo].[Productos] OFF");
  w("GO");
  w(`DBCC CHECKIDENT ('[dbo].[Productos]', RESEED, ${nextProducto - 1})`);
  w("GO");

  w("SET IDENTITY_INSERT [dbo].[Categorias_Productos] ON");
  let cp = 100;
  for (const p of products) {
    for (const c of p.cats) {
      w(`INSERT INTO [dbo].[Categorias_Productos] ([CategoriaProductoId],[IdProducto],[IdCategoria]) VALUES (${cp++}, ${p.id}, ${c})`);
    }
  }
  w("SET IDENTITY_INSERT [dbo].[Categorias_Productos] OFF");
  w("GO");
  w(`DBCC CHECKIDENT ('[dbo].[Categorias_Productos]', RESEED, ${cp - 1})`);
  w("GO");

  w("SET IDENTITY_INSERT [dbo].[Lotes] ON");
  for (const l of lots) {
    const desc = l.desc || ("Lote " + l.numero);
    w(`INSERT INTO [dbo].[Lotes] ([id_Lote],[id_Producto],[stock_inicial],[stock_actual],[numero_lote],[descripcion],[fecha_alta],[fecha_vencimiento],[esta_vencido],[esta_activo],[esta_eliminado]) VALUES (${l.id}, ${l.productoId}, ${l.stockInicial}, ${l.stockActual}, ${sqlN(l.numero)}, ${sqlN(desc)}, ${sqlDate(l.alta)}, ${sqlDate(l.venc)}, ${l.vencido ? 1 : 0}, ${l.activo ? 1 : 0}, 0)`);
  }
  w("SET IDENTITY_INSERT [dbo].[Lotes] OFF");
  w("GO");
  w(`DBCC CHECKIDENT ('[dbo].[Lotes]', RESEED, ${nextLote - 1})`);
  w("GO");

  // sync stock lotes (activos + no vencidos)
  w(`UPDATE p SET p.stock = ISNULL(l.suma, 0), p.estado = CASE WHEN ISNULL(l.suma, 0) = 0 THEN 4 ELSE CASE WHEN p.estado = 4 THEN 1 ELSE p.estado END END
FROM [dbo].[Productos] p
OUTER APPLY (SELECT SUM(stock_actual) AS suma FROM [dbo].[Lotes] WHERE id_Producto = p.ProductoId AND esta_eliminado = 0 AND esta_activo = 1 AND esta_vencido = 0) l
WHERE p.control_por_lote = 1 AND p.ProductoId >= 100;`);
  w("GO");

  w("SET IDENTITY_INSERT [dbo].[OfertasDescuentos] ON");
  for (const o of ofertas) {
    const fi = sqlDate(o.inicio);
    const ff = o.fin ? sqlDate(o.fin) : "NULL";
    w(`INSERT INTO [dbo].[OfertasDescuentos] ([id_OfertaDescuento],[descripcion],[codigo],[fecha_inicio],[fecha_fin],[esta_activa],[tipo_oferta],[porcentaje_descuento],[precio_final]) VALUES (${o.id}, ${sqlN(o.desc)}, ${sqlN(o.codigo)}, ${fi}, ${ff}, ${o.activa ? 1 : 0}, ${o.tipo}, ${o.pct == null ? "NULL" : fmt(o.pct)}, ${o.precio == null ? "NULL" : fmt(o.precio)})`);
  }
  w("SET IDENTITY_INSERT [dbo].[OfertasDescuentos] OFF");
  w("GO");
  w(`DBCC CHECKIDENT ('[dbo].[OfertasDescuentos]', RESEED, ${ofertas[ofertas.length - 1].id})`);
  w("GO");

  for (const o of ofertas) {
    const p = products.find((x) => x.id === o.prod);
    const ofertaBase = o.precio != null ? o.precio : money(p.venta * (1 - (o.pct || 0) / 100));
    w(`INSERT INTO [dbo].[ProductosEnOfertaDescuentos] ([id_Producto],[id_OfertaDescuento],[cantidad_requerida],[limite_venta_producto],[precio_costo_base],[precio_oferta_base],[precio_venta_base]) VALUES (${o.prod}, ${o.id}, 1, NULL, ${fmt(p.costo)}, ${fmt(ofertaBase)}, ${fmt(p.venta)})`);
  }
  w("GO");

  if (ofertaStats && ofertaStats.size) {
    w("/* OfertaProductoEstadisticas acumuladas */");
    for (const st of ofertaStats.values()) {
      w(`INSERT INTO [dbo].[OfertaProductoEstadisticas] ([id_OfertaDescuento],[id_Producto],[cantidad_vendida],[total_costo_acumulado],[total_venta_acumulado],[total_oferta_acumulado],[fecha_ultima_venta]) VALUES (${st.oid}, ${st.pid}, ${fmt(st.qty)}, ${fmt(st.costo)}, ${fmt(st.venta)}, ${fmt(st.oferta)}, ${sqlDate(st.last)});`);
    }
    w("GO");
  }

  w("PRINT N'Catalogo QueryData3.0 OK';");
  w("GO");
  await writer.end();
  console.log("wrote 00_catalogo.sql");
}

async function writeCtaCteFinal() {
  const file = path.join(OUT, "98_ctacte_final.sql");
  const writer = SqlWriter(file);
  const w = (line) => writer.w(line);
  w(`USE [${DB}]`);
  w("GO");
  w("/* Alineacion final de saldos CtaCte post-movimientos (QueryData3.0) */");
  w("SET NOCOUNT ON;");
  w("GO");
  for (const c of ctaCtes.values()) {
    w(`UPDATE [dbo].[CuentasCorrientes] SET [saldo] = ${fmt(c.saldo)}, [con_deuda] = ${c.saldo < 0 ? 1 : 0} WHERE [CuentaCorrienteId] = ${c.id};`);
  }
  w("GO");
  w("PRINT N'CtaCte saldos finales alineados.';");
  w("GO");
  await writer.end();
  console.log("wrote 98_ctacte_final.sql");
}

async function writeDemoPresentacion() {
  const src = path.join(OUT, "97_demo_presentacion.sql");
  if (fs.existsSync(src)) {
    console.log("keeping 97_demo_presentacion.sql");
    return;
  }
  // Fallback minimo si falta el archivo estatico
  fs.writeFileSync(src, `USE [${DB}]\nGO\nPRINT N'Ejecutar DEMO_PRESENTACION.bat o copiar 97_demo_presentacion.sql';\nGO\n`);
}

async function writeValidaciones() {
  // Columna real en SQL: id_VentaLibre (no VentaLibreId de EF)
  const content = `USE [${DB}]
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
`;
  fs.writeFileSync(path.join(OUT, "99_validaciones.sql"), content);
}

async function writeLimpiar() {
  // LIMPIAR_DB_TOTAL.sql y LIMPIAR_DB_PEGAR.sql se mantienen como archivos estaticos del paquete.
  // Solo regeneramos bats de conveniencia.
  const totalBat = `@echo off
setlocal
cd /d "%~dp0"
set SERVER=.\\SQLEXPRESS
set DATABASE=${DB}
echo QueryData3.0 - WIPE TOTAL
echo Servidor: %SERVER%  Base: %DATABASE%
choice /C SN /M "Continuar con el wipe total"
if errorlevel 2 exit /b 0
sqlcmd -S "%SERVER%" -d "%DATABASE%" -E -b -i "%~dp0LIMPIAR_DB_TOTAL.sql"
set ERR=%ERRORLEVEL%
if %ERR% neq 0 (echo ERROR: wipe fallo) else (echo OK: wipe total finalizado)
pause
exit /b %ERR%
`;
  const seedBat = `@echo off
setlocal
cd /d "%~dp0"
set SERVER=.\\SQLEXPRESS
set DATABASE=${DB}
echo QueryData3.0 - CARGAR SEED
echo Servidor: %SERVER%  Base: %DATABASE%
echo Flujo: LIMPIAR_DB_TOTAL -> app 1 vez -> este script
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0run_all.ps1" -Server "%SERVER%" -Database "%DATABASE%"
set ERR=%ERRORLEVEL%
if %ERR% neq 0 (echo ERROR: carga fallo) else (echo OK: carga finalizada)
pause
exit /b %ERR%
`;
  fs.writeFileSync(path.join(OUT, "LIMPIAR_DB_TOTAL.bat"), totalBat);
  fs.writeFileSync(path.join(OUT, "CARGAR_SEED.bat"), seedBat);
}

async function writeReadme(monthFiles, stats) {
  const names = monthFiles.map((f) => path.basename(f)).sort();
  const md = `# QueryData3.0 — Seed 2 años

Paquete listo para probar Stockeate / \`${DB}\`.

## Flujo (obligatorio)

1. **\`LIMPIAR_DB_TOTAL.bat\`** — vacía **todos** los registros (deja esquema + migraciones)
2. **Abrir la app UNA vez** — crea Admin, Consumidor Final, TiposPago, SADMIN
3. **\`CARGAR_SEED.bat\`** — limpia solo seed viejo (IDs ≥ 100) y carga data

Login smoke: \`admin\` / \`Admin123\`

## Contenido
- Rango: **${ymd(START)} → ${ymd(END)}** (${DAYS} días)
- Ventas: ~${stats.ventas} (+ ${stats.cancel} cancelaciones)
- Pagos mixtos: ~${stats.mixtos}
- Pagos CtaCte: ~${stats.cta}
- Ítems con oferta: ~${stats.ofertas}
- Ventas libres: ~${stats.vl}
- Gastos: ~${stats.gastos}
- Archivos mensuales: ${names.length}

Novedades 3.0: pagos mixtos, CtaCte alineado (\`98_ctacte_final.sql\`), ofertas con vigencia, Roles_Permisos, lotes al límite.

## Regenerar
\`\`\`bash
cd generator
node generate-seed.mjs
node generate-seed.mjs --days 730 --ventas-dia 35 --end 2026-08-11 --db ${DB}
\`\`\`
`;
  fs.writeFileSync(path.join(OUT, "README.md"), md);
}

async function writeRunAll(monthFiles) {
  const names = monthFiles.map((f) => path.basename(f)).sort();
  const ps = `# QueryData3.0 - carga seed (NO hace wipe total)
# Flujo: LIMPIAR_DB_TOTAL -> abrir app 1 vez -> este script
param(
  [string]$Server   = ".\\SQLEXPRESS",
  [string]$Database = "${DB}",
  [switch]$SkipClean
)
$ErrorActionPreference = "Stop"
$root = $PSScriptRoot
Write-Host "QueryData3.0 | Server=$Server Database=$Database" -ForegroundColor Yellow
sqlcmd -S $Server -E -b -Q "IF DB_ID(N'$Database') IS NULL CREATE DATABASE [$Database];" | Out-Host
function Invoke-SqlFile($file) {
  Write-Host ">> $file" -ForegroundColor Cyan
  sqlcmd -S $Server -d $Database -E -b -i (Join-Path $root $file) | Out-Host
  if ($LASTEXITCODE -ne 0) { throw "Fallo $file (exit $LASTEXITCODE)" }
}
if (-not $SkipClean) { Invoke-SqlFile "LIMPIAR_DB_PEGAR.sql" }
Invoke-SqlFile "00_catalogo.sql"
${names.map((n) => `Invoke-SqlFile "meses\\${n}"`).join("\n")}
Invoke-SqlFile "98_ctacte_final.sql"
Invoke-SqlFile "97_demo_presentacion.sql"
Invoke-SqlFile "99_validaciones.sql"
Write-Host "Carga QueryData3.0 OK" -ForegroundColor Green
`;
  fs.writeFileSync(path.join(OUT, "run_all.ps1"), ps);
}

main().catch((e) => {
  console.error(e);
  process.exit(1);
});
