/*Remueve los tr de */
function RemoverElementosTabla(idTabla)
{
	$("#"+idTabla+" tbody tr").each(function(){
		this.remove();
	});
}

function mostrarProcesando() {
    document.getElementById("divProcesando").style.display = "block";
}

function hideProcesando() {
    var tmp = document.getElementById("divProcesando");
    if (tmp != undefined && tmp != null) {
        tmp.style.display = "none";
    }
}

function MostrarProcesandoLigero() {
    document.getElementById("divProcesando_ligero").style.display = "block";
}

function HideProcesandoLigero() {
    var tmp = document.getElementById("divProcesando_ligero");
    if (tmp != undefined && tmp != null) {
        tmp.style.display = "none";
    }
}

function ActivarAlertaValidacionCampo(idCampo) {
    $("#" + idCampo).addClass("input-validation-error");
    $("#v" + idCampo).css("display", "block");
}

function DesactivarAlertaValidacionCampo(idCampo) {
    $("#v" + idCampo + "").css("display", "none");
    $("#" + idCampo + "").removeClass("input-validation-error");
}

function Convertir_DateToString(dtfecha, formato) {
    var strfecha_convert = "Error formatear fecha";
    if (dtfecha.getFullYear() > 1950) {
        strfecha_convert = moment(dtfecha).format(formato);
    }
    return strfecha_convert;
}

function Convertir_DateToString(dtfecha, formato, mensaje) {
    var strfecha_convert = "Error formatear fecha";
    if (dtfecha.getFullYear() > 1950) {
        strfecha_convert = moment(dtfecha).format(formato);
    }
    else
    {
        strfecha_convert = mensaje;
    }
    return strfecha_convert;
}


function FillTextoVerDetalle(dato) {
    if (dato != "")
        return dato;
    else
        return "No registra";
}

function FillDateVerDetalle(fecha) {
    var dt_fecha = new Date(fecha);
    var strfecha = "No registra";
    if (dt_fecha.getFullYear() > 1990) {
        strfecha = moment(dt_fecha).format('YYYY-MM-DD HH:mm:ss');
    }
    return strfecha;
}

function FillDateVerDetalle(fecha, formato) {
    var dt_fecha = new Date(fecha);
    var strfecha = "No registra";
    if (dt_fecha.getFullYear() > 1990) {
        strfecha = moment(dt_fecha).format(formato);
    }
    return strfecha;
}

function obtenerPathname(controlador, acccion) {
    if (window.location.pathname == "/")
        return "/" + controlador + "/" + acccion;
    else {
        var arrayDeCadenas = window.location.pathname.split("/");
        if (arrayDeCadenas.length > 2) {
            arrayDeCadenas.length = arrayDeCadenas.length - 2
            arrayDeCadenas.push(controlador);
            arrayDeCadenas.push(acccion);
            return arrayDeCadenas.join('/');
        }
        else {
            return window.location.pathname + "/" + controlador + "/" + acccion;
        }
    }
}

function obtenerPathnameEsp(controlador, acccion) {
    if (window.location.pathname == "/")
        return "/" + controlador + "/" + acccion;
    else {
        var arrayDeCadenas = window.location.pathname.split("/");
        if (arrayDeCadenas.length > 2) {
            arrayDeCadenas.length = arrayDeCadenas.length - 3
            arrayDeCadenas.push(controlador);
            arrayDeCadenas.push(acccion);
            return arrayDeCadenas.join('/');
        }
        else {
            return window.location.pathname + "/" + controlador + "/" + acccion;
        }
    }
}

function display_block_process(display) {
    if (display) {
        return $("#block-search-process").show();
    }
    return $("#block-search-process").hide();
}
