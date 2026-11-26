using Servicios.Helpers.Sistema;
using Servicios.LogicaNegocio.Configuracion.DTO;
using System;

namespace Servicios.LogicaNegocio.Configuracion
{
    public interface IConfiguracionServicio
    {
        T Obtener<T>(ClaveConfiguracion clave) where T : class, new();
        bool ExisteConfiguracion(ClaveConfiguracion clave);
        byte[] ObtenerLogo();
        NomenclaturaCodigoDTO ObtenerNomenclatura(TipoCodigo tipo);
        string GenerarCodigo(TipoCodigo tipo, long numero, DateTime? fecha = null);
        EstadoOperacion Guardar(LoteConfiguracion lote);
        EstadoOperacion MigrarComprobantesDesdeJson();
    }
}