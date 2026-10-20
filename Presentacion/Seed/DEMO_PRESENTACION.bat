@echo off
setlocal
cd /d "%~dp0"

set SERVER=.\SQLEXPRESS
set DATABASE=EjemploBase

echo ========================================
echo  DEMO PRESENTACION - Notificaciones TFI
echo  Servidor: %SERVER%
echo  Base:     %DATABASE%
echo ========================================
echo.
echo Prepara lotes, ofertas, CtaCte y notificaciones
echo con fechas relativas a HOY (GETDATE).
echo.
echo Ejecuta esto ANTES de abrir la app para la demo.
echo.

sqlcmd -S "%SERVER%" -d "%DATABASE%" -E -b -i "%~dp097_demo_presentacion.sql"
set ERR=%ERRORLEVEL%

echo.
if %ERR% neq 0 (
  echo ERROR: demo fallo con codigo %ERR%
) else (
  echo OK: panel de notificaciones listo para la presentacion
  echo Login: admin / Admin123
)
pause
exit /b %ERR%
