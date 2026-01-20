using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    public partial class AgregarRubroAndFixMarcaAndProductCols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) Crear tabla Rubros si no existe e insertar rubro por defecto
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Rubros' AND schema_id = SCHEMA_ID('dbo'))
                BEGIN
                    CREATE TABLE dbo.Rubros (
                        RubroId BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                        Nombre NVARCHAR(100) NOT NULL,
                        EstaEliminado BIT NOT NULL DEFAULT 0
                    );
                END

                IF NOT EXISTS(SELECT 1 FROM dbo.Rubros WHERE Nombre = 'Sin Rubro')
                BEGIN
                    INSERT INTO dbo.Rubros (Nombre, EstaEliminado) VALUES ('Sin Rubro', 0);
                END
            ");

            // 2) Agregar columna id_Rubro si no existe (nullable temporalmente)
            migrationBuilder.Sql(@"
                IF COL_LENGTH('dbo.Productos','id_Rubro') IS NULL
                BEGIN
                    ALTER TABLE dbo.Productos ADD id_Rubro BIGINT NULL;
                END
            ");

            // 3) Backfill id_Rubro con el rubro 'Sin Rubro'
            migrationBuilder.Sql(@"
                DECLARE @DefaultRubroId BIGINT;
                SELECT TOP 1 @DefaultRubroId = RubroId FROM dbo.Rubros WHERE Nombre = 'Sin Rubro';

                UPDATE dbo.Productos
                SET id_Rubro = @DefaultRubroId
                WHERE id_Rubro IS NULL;
            ");

            // 4) Crear FK id_Rubro -> Rubros si no existe
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1 FROM sys.foreign_keys fk
                    JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
                    JOIN sys.columns c ON fkc.parent_object_id = c.object_id AND fkc.parent_column_id = c.column_id
                    WHERE fk.parent_object_id = OBJECT_ID('dbo.Productos') AND c.name = 'id_Rubro'
                )
                BEGIN
                  ALTER TABLE dbo.Productos
                  ADD CONSTRAINT FK_Productos_Rubros_id_Rubro FOREIGN KEY (id_Rubro)
                  REFERENCES dbo.Rubros(RubroId) ON DELETE NO ACTION;
                END
            ");

            // 5) Eliminar FK y columna MarcaId redundante si existen
            migrationBuilder.Sql(@"
                -- Eliminar FK de MarcaId si existe
                DECLARE @fkName NVARCHAR(200);
                SELECT TOP 1 @fkName = fk.name
                FROM sys.foreign_keys fk
                JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
                JOIN sys.columns c ON fkc.parent_object_id = c.object_id AND fkc.parent_column_id = c.column_id
                WHERE fk.parent_object_id = OBJECT_ID('dbo.Productos') AND c.name = 'MarcaId';

                IF @fkName IS NOT NULL
                BEGIN
                    EXEC('ALTER TABLE dbo.Productos DROP CONSTRAINT [' + @fkName + ']');
                END

                -- Eliminar índice IX_Productos_MarcaId si existe
                IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Productos_MarcaId' AND object_id = OBJECT_ID('dbo.Productos'))
                BEGIN
                    DROP INDEX IX_Productos_MarcaId ON dbo.Productos;
                END

                -- Eliminar columna MarcaId si existe
                IF COL_LENGTH('dbo.Productos','MarcaId') IS NOT NULL
                BEGIN
                    ALTER TABLE dbo.Productos DROP COLUMN MarcaId;
                END
            ");

            // 6) Agregar/asegurar columnas nuevas en Productos con backfill y NOT NULL cuando corresponde

            // stock
            migrationBuilder.Sql(@"
    IF COL_LENGTH('dbo.Productos','stock') IS NULL
        ALTER TABLE dbo.Productos ADD stock DECIMAL(18,2) NULL;
");
            migrationBuilder.Sql("UPDATE dbo.Productos SET stock = 0 WHERE stock IS NULL;");
            migrationBuilder.Sql("ALTER TABLE dbo.Productos ALTER COLUMN stock DECIMAL(18,2) NOT NULL;");

            // precio_costo
            migrationBuilder.Sql(@"
    IF COL_LENGTH('dbo.Productos','precio_costo') IS NULL
        ALTER TABLE dbo.Productos ADD precio_costo DECIMAL(18,2) NULL;
");
            migrationBuilder.Sql("UPDATE dbo.Productos SET precio_costo = 0 WHERE precio_costo IS NULL;");
            migrationBuilder.Sql("ALTER TABLE dbo.Productos ALTER COLUMN precio_costo DECIMAL(18,2) NOT NULL;");

            // precio_venta
            migrationBuilder.Sql(@"
    IF COL_LENGTH('dbo.Productos','precio_venta') IS NULL
        ALTER TABLE dbo.Productos ADD precio_venta DECIMAL(18,2) NULL;
");
            migrationBuilder.Sql("UPDATE dbo.Productos SET precio_venta = 0 WHERE precio_venta IS NULL;");
            migrationBuilder.Sql("ALTER TABLE dbo.Productos ALTER COLUMN precio_venta DECIMAL(18,2) NOT NULL;");

            // descripcion
            migrationBuilder.Sql(@"
    IF COL_LENGTH('dbo.Productos','descripcion') IS NULL
        ALTER TABLE dbo.Productos ADD descripcion NVARCHAR(1000) NULL;
");

            // esta_eliminado
            migrationBuilder.Sql(@"
    IF COL_LENGTH('dbo.Productos','esta_eliminado') IS NULL
        ALTER TABLE dbo.Productos ADD esta_eliminado BIT NULL;
");
            migrationBuilder.Sql("UPDATE dbo.Productos SET esta_eliminado = 0 WHERE esta_eliminado IS NULL;");
            migrationBuilder.Sql("ALTER TABLE dbo.Productos ALTER COLUMN esta_eliminado BIT NOT NULL;");

            // estado
            migrationBuilder.Sql(@"
    IF COL_LENGTH('dbo.Productos','estado') IS NULL
        ALTER TABLE dbo.Productos ADD estado INT NULL;
");

            // medida
            migrationBuilder.Sql(@"
    IF COL_LENGTH('dbo.Productos','medida') IS NULL
        ALTER TABLE dbo.Productos ADD medida NVARCHAR(20) NULL;
");

            // unidad_medida
            migrationBuilder.Sql(@"
    IF COL_LENGTH('dbo.Productos','unidad_medida') IS NULL
        ALTER TABLE dbo.Productos ADD unidad_medida NVARCHAR(50) NULL;
");

            // codigo
            migrationBuilder.Sql(@"
    IF COL_LENGTH('dbo.Productos','codigo') IS NULL
        ALTER TABLE dbo.Productos ADD codigo NVARCHAR(50) NULL;
");

            // codigo_barra
            migrationBuilder.Sql(@"
    IF COL_LENGTH('dbo.Productos','codigo_barra') IS NULL
        ALTER TABLE dbo.Productos ADD codigo_barra NVARCHAR(50) NULL;
");

            // iva_inluido_precio_final
            migrationBuilder.Sql(@"
    IF COL_LENGTH('dbo.Productos','iva_inluido_precio_final') IS NULL
        ALTER TABLE dbo.Productos ADD iva_inluido_precio_final BIT NULL;
");
            migrationBuilder.Sql("UPDATE dbo.Productos SET iva_inluido_precio_final = 0 WHERE iva_inluido_precio_final IS NULL;");
            migrationBuilder.Sql("ALTER TABLE dbo.Productos ALTER COLUMN iva_inluido_precio_final BIT NOT NULL;");

            // es_fraccionable
            migrationBuilder.Sql(@"
    IF COL_LENGTH('dbo.Productos','es_fraccionable') IS NULL
        ALTER TABLE dbo.Productos ADD es_fraccionable BIT NULL;
");
            migrationBuilder.Sql("UPDATE dbo.Productos SET es_fraccionable = 0 WHERE es_fraccionable IS NULL;");
            migrationBuilder.Sql("ALTER TABLE dbo.Productos ALTER COLUMN es_fraccionable BIT NOT NULL;");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revertir adiciones: eliminar columnas nuevas, eliminar FK y columna id_Rubro, eliminar tabla Rubros si fue creada
            migrationBuilder.Sql(@"
                -- Eliminar columnas agregadas (si existen)
                IF COL_LENGTH('dbo.Productos','es_fraccionable') IS NOT NULL
                    ALTER TABLE dbo.Productos DROP COLUMN es_fraccionable;

                IF COL_LENGTH('dbo.Productos','iva_inluido_precio_final') IS NOT NULL
                    ALTER TABLE dbo.Productos DROP COLUMN iva_inluido_precio_final;

                IF COL_LENGTH('dbo.Productos','codigo_barra') IS NOT NULL
                    ALTER TABLE dbo.Productos DROP COLUMN codigo_barra;

                IF COL_LENGTH('dbo.Productos','codigo') IS NOT NULL
                    ALTER TABLE dbo.Productos DROP COLUMN codigo;

                IF COL_LENGTH('dbo.Productos','unidad_medida') IS NOT NULL
                    ALTER TABLE dbo.Productos DROP COLUMN unidad_medida;

                IF COL_LENGTH('dbo.Productos','medida') IS NOT NULL
                    ALTER TABLE dbo.Productos DROP COLUMN medida;

                IF COL_LENGTH('dbo.Productos','estado') IS NOT NULL
                    ALTER TABLE dbo.Productos DROP COLUMN estado;

                IF COL_LENGTH('dbo.Productos','esta_eliminado') IS NOT NULL
                    ALTER TABLE dbo.Productos DROP COLUMN esta_eliminado;

                IF COL_LENGTH('dbo.Productos','descripcion') IS NOT NULL
                    ALTER TABLE dbo.Productos DROP COLUMN descripcion;

                IF COL_LENGTH('dbo.Productos','precio_venta') IS NOT NULL
                    ALTER TABLE dbo.Productos DROP COLUMN precio_venta;

                IF COL_LENGTH('dbo.Productos','precio_costo') IS NOT NULL
                    ALTER TABLE dbo.Productos DROP COLUMN precio_costo;

                IF COL_LENGTH('dbo.Productos','stock') IS NOT NULL
                    ALTER TABLE dbo.Productos DROP COLUMN stock;
            ");

            // Eliminar FK de id_Rubro si existe y la columna id_Rubro
            migrationBuilder.Sql(@"
                DECLARE @fkRubro NVARCHAR(200);
                SELECT TOP 1 @fkRubro = fk.name
                FROM sys.foreign_keys fk
                JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
                JOIN sys.columns c ON fkc.parent_object_id = c.object_id AND fkc.parent_column_id = c.column_id
                WHERE fk.parent_object_id = OBJECT_ID('dbo.Productos') AND c.name = 'id_Rubro';

                IF @fkRubro IS NOT NULL
                BEGIN
                    DECLARE @sql NVARCHAR(MAX) = 'ALTER TABLE dbo.Productos DROP CONSTRAINT [' + @fkRubro + ']';
                    EXEC sp_executesql @sql;
                END

                IF COL_LENGTH('dbo.Productos','id_Rubro') IS NOT NULL
                    ALTER TABLE dbo.Productos DROP COLUMN id_Rubro;
            ");

            // Opcional: eliminar tabla Rubros (si existe)
            migrationBuilder.Sql(@"
                IF OBJECT_ID('dbo.Rubros','U') IS NOT NULL
                BEGIN
                    DROP TABLE dbo.Rubros;
                END
            ");
        }
    }
}
