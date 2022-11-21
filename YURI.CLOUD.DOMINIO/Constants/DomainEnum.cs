using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI.CLOUD.DOMINIO.Constants
{
    /// <summary>
    /// utilizado para metodo de guardado de logs seteados en la startup
    /// </summary>
    public enum DestinosLog : byte
    {
        Disk = 1,
        AzureBlobStorage = 2,
        AmazonBucket = 3,
        DataBase = 4
    }

    /// <summary>
    /// utilizado para metodo de guardado de archivos seteados en la startup
    /// </summary>
    public enum DestinosIo : byte
    {
        Disk = 1,
        AzureBlobStorage = 2,
        AmazonBucket = 3,
        Ftp = 4,
    }

    /// <summary>
    /// Estados de mail en servicio
    /// </summary>
    public enum EstadoMailALMXPRESSEC : int
    {
        ErrorDeConexión = -2,
        CorreoNoVálido = -1,
        NoEnviado = 0,
        Enviado = 1,
        EnviadoParcialmente = 2
    }

    /// <summary>
    /// Acciones de usuarios logueados
    /// </summary>
    public enum TipoAccionUsuarioALMXPRESSEC
    {
        [Description("Inicio de sesión")]
        InicioSesion = 1,
        [Description("Recuperar contraseña")]
        RecuperarContrasena = 2,
        [Description("Agregar")]
        Agregar = 3,
        [Description("Editar")]
        Editar = 4,
        [Description("Eliminar")]
        Eliminar = 5,
        [Description("Cambiar contraseña")]
        CambiarContraseña = 6,
        [Description("Asignar rol")]
        AsignarRol = 7,
        [Description("Correo bienvenida")]
        CorreoBienvenida = 8,
        [Description("Consulta")]
        Consulta = 9,
        [Description("Reenviar correo bienvenida")]
        ReenviarCorreoBienvenida = 10,
    }

    /// <summary>
    /// Utilizado para guardar info en el token de autenticacion
    /// </summary>
    public enum TokenTagALMXPRESSEC
    {
        IdUsuario = 1,
        Usuario = 2,
        ClaveEncriptada = 3,
        Correo = 4,
        IdGrupo = 5,
        Ip = 6,
        MacAddress = 7,
        UserAgent = 8
    }

    public enum OPERSQL : short
    {
        MantEntidad = 1,
        UpdEstado = 2,
        InsRow = 3,
        UpdEntidad = 4,
        DelInsRow = 5,
        Bulkload = 6,
        BulkUpdateAndRegistration = 7
    }


    public enum DenialLevel
    {
        ErrorTecnicoSQL,
        ErrorControlado,
        ErrorNoDefinido
    }
}
