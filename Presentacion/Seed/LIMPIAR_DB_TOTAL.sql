/* =============================================================================
   QueryData3.0 - LIMPIAR DB TOTAL
   Borra TODOS los registros. Pegar en SSMS (F5) o LIMPIAR_DB_TOTAL.bat

   - Vacia todas las tablas de negocio
   - PRESERVA __EFMigrationsHistory (esquema / migraciones)
   - Resetea IDENTITY a 0

   Flujo:
     1) Este script
     2) Abrir la app UNA vez
     3) CARGAR_SEED.bat

   NO usar en una base productiva.
   ============================================================================= */

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

PRINT N'Wipe total sobre: ' + DB_NAME();
GO

BEGIN TRY
    BEGIN TRANSACTION;

    -- Desactivar FKs para poder vaciar sin importar el orden
    DECLARE @sql NVARCHAR(MAX) = N'';

    SELECT @sql = @sql + N'ALTER TABLE '
        + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + N'.'
        + QUOTENAME(OBJECT_NAME(parent_object_id))
        + N' NOCHECK CONSTRAINT ' + QUOTENAME(name) + N';' + CHAR(10)
    FROM sys.foreign_keys;

    EXEC sp_executesql @sql;

    -- Borrar todas las tablas de usuario excepto migraciones EF
    SET @sql = N'';
    SELECT @sql = @sql + N'DELETE FROM '
        + QUOTENAME(s.name) + N'.' + QUOTENAME(t.name) + N';' + CHAR(10)
    FROM sys.tables t
    INNER JOIN sys.schemas s ON s.schema_id = t.schema_id
    WHERE t.is_ms_shipped = 0
      AND t.name <> N'__EFMigrationsHistory'
      AND s.name = N'dbo';

    EXEC sp_executesql @sql;

    -- Reactivar FKs
    SET @sql = N'';
    SELECT @sql = @sql + N'ALTER TABLE '
        + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + N'.'
        + QUOTENAME(OBJECT_NAME(parent_object_id))
        + N' WITH CHECK CHECK CONSTRAINT ' + QUOTENAME(name) + N';' + CHAR(10)
    FROM sys.foreign_keys;

    EXEC sp_executesql @sql;

    -- Reseed IDENTITY a 0 en tablas con identity
    DECLARE @tbl SYSNAME;
    DECLARE @schema SYSNAME;
    DECLARE reseed CURSOR LOCAL FAST_FORWARD FOR
        SELECT s.name, t.name
        FROM sys.tables t
        INNER JOIN sys.schemas s ON s.schema_id = t.schema_id
        WHERE t.is_ms_shipped = 0
          AND t.name <> N'__EFMigrationsHistory'
          AND s.name = N'dbo'
          AND EXISTS (
              SELECT 1
              FROM sys.identity_columns ic
              WHERE ic.object_id = t.object_id
          );

    DECLARE @cmd NVARCHAR(400);
    OPEN reseed;
    FETCH NEXT FROM reseed INTO @schema, @tbl;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @cmd =
            N'DBCC CHECKIDENT (N''' + @schema + N'.' + @tbl + N''', RESEED, 0) WITH NO_INFOMSGS;';
        EXEC sp_executesql @cmd;
        FETCH NEXT FROM reseed INTO @schema, @tbl;
    END
    CLOSE reseed;
    DEALLOCATE reseed;

    COMMIT TRANSACTION;
    PRINT N'OK: base vaciada. Abrí la app UNA vez y después corré CARGAR_SEED.bat';
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrState INT = ERROR_STATE();

    PRINT N'ERROR: se revirtió el wipe total (ROLLBACK).';
    RAISERROR (@ErrMsg, @ErrSeverity, @ErrState);
END CATCH
GO
