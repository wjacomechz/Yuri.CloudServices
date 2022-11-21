
function MensajeSistema(tipo, mensaje)
{
    //'error|success|warning|info', // Any of the following
    Lobibox.alert(tipo, {
        msg: mensaje
    });
}

function SwalAlertDefault(tipo, mensaje) {
    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });
    Toast.fire({
        icon: tipo,
        title: mensaje
    })
}

function ToastAlertDefault(tipo, mensaje) {
    if (tipo == "success")
        toastr.success(mensaje)
    else if (tipo == "info")
        toastr.info(mensaje)
    else if (tipo == "error")
        toastr.error(mensaje)
    else if (tipo == "warning")
        toastr.warning(mensaje)
}



function MostrarNotificacionEsquinera(tipo,mensaje)
{
    // Available types 'warning', 'info', 'success', 'error'
    Lobibox.notify(tipo, {
        size: 'mini',
        rounded: true,
        delayIndicator: false,
        msg: mensaje
    });
}

function ConfirmarAccion()
{
    Lobibox.confirm({
        msg: "Are you sure you want to delete this user?",
    });
}


