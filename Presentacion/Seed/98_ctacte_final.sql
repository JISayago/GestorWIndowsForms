USE [EjemploBase]
GO
/* Alineacion final de saldos CtaCte post-movimientos (QueryData3.0) */
SET NOCOUNT ON;
GO
UPDATE [dbo].[CuentasCorrientes] SET [saldo] = -49905.33, [con_deuda] = 1 WHERE [CuentaCorrienteId] = 100;
UPDATE [dbo].[CuentasCorrientes] SET [saldo] = -29292.47, [con_deuda] = 1 WHERE [CuentaCorrienteId] = 101;
UPDATE [dbo].[CuentasCorrientes] SET [saldo] = -49962.22, [con_deuda] = 1 WHERE [CuentaCorrienteId] = 102;
UPDATE [dbo].[CuentasCorrientes] SET [saldo] = -19405.54, [con_deuda] = 1 WHERE [CuentaCorrienteId] = 103;
UPDATE [dbo].[CuentasCorrientes] SET [saldo] = 0.00, [con_deuda] = 0 WHERE [CuentaCorrienteId] = 104;
UPDATE [dbo].[CuentasCorrientes] SET [saldo] = -38191.93, [con_deuda] = 1 WHERE [CuentaCorrienteId] = 105;
GO
PRINT N'CtaCte saldos finales alineados.';
GO
