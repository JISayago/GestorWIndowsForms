namespace Servicios.Helpers.Cliente.CtaCte
{
    public enum EstadoCuentaCorriente
    {
        Inactiva = 0,// creada sin activarse nunca estado Default
        Activa = 1, // avivada
        Suspendida = 2, //Vencida por fecha o por deuda 
        Cerrada = 3 // para su no uso. ver el alcance
    }
}
