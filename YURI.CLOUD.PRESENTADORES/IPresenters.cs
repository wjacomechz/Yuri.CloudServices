namespace YURI.CLOUD.PRESENTADORES
{
    /// <summary>
    /// Toma la informacion devuelto la capa de aplicacion y formatearla a la interfaz de usuario.
    /// </summary>
    public interface IPresenters<FormatDataType>
    {
        public FormatDataType Content { get; }
    }
}
