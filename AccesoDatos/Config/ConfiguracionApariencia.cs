namespace AccesoDatos.Config
{
    public class ConfiguracionApariencia
    {
        // Se guarda como texto para que AccesoDatos no dependa de Presentacion (valores: "Original", "Clasico").
        public string Tema { get; set; } = "Original";
    }
}