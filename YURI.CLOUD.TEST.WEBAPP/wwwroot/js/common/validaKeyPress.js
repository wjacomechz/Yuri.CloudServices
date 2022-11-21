// Array de los meses
var monthNames = new makeArray(12);
monthNames[0] = "Enero";
monthNames[1] = "Febrero";
monthNames[2] = "Marzo";
monthNames[3] = "Abril";
monthNames[4] = "Mayo";
monthNames[5] = "Junio";
monthNames[6] = "Julio";
monthNames[7] = "Agosto";
monthNames[8] = "Septiembre";
monthNames[9] = "Octubre";
monthNames[10] = "Noviembre";
monthNames[11] = "Diciembre";

// Array de los días
var dayNames = new makeArray(7);
dayNames[0] = "Domingo";
dayNames[1] = "Lunes";
dayNames[2] = "Martes";
dayNames[3] = "Mi&eacute;rcoles";
dayNames[4] = "Jueves";
dayNames[5] = "Viernes";
dayNames[6] = "S&aacute;bado";

var now = new Date();
var year = now.getYear();

if (year < 2000)
    year = year + 1900;

function validaLetras(e) {//Permite validar solo letras
    var key = document.all ? tecla = e.keyCode : tecla = e.which;
    var especials = [13];
    for (var i in especials) {
        if (key == especials[i]) { return false; }
    }
    return (((key > 96 && key < 123) || (key > 64 && key < 91) || (key == 241 || key == 209) || (key == 32)));
}

function SoloNumeros(evt) {
    if (window.event) {//asignamos el valor de la tecla a que inicio el evento
        keynum = evt.keyCode;
    }
    else {
        keynum = evt.which; 
    }
    //comprobo si se encuentra en el rango numérico y que teclas no recibirá.
    if ((keynum > 47 && keynum < 58) || keynum == 8 || keynum == 13 || keynum == 6) {
        return true;
    }
    else {
        return false;
    }
}

function soloLetras(e){
       key = e.keyCode || e.which;
       tecla = String.fromCharCode(key).toLowerCase();
       letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
       especiales = "8-37-39-46";

       tecla_especial = false
       for(var i in especiales){
            if(key == especiales[i]){
                tecla_especial = true;
                break;
            }
        }

        if(letras.indexOf(tecla)==-1 && !tecla_especial){
            return false;
        }
    }

function soloLetrasNumerosSinCaracteres(e){
    key = e.keyCode || e.which;
    if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar == 8))
        e.Handled = false;
    else
        e.Handled = true;

    // var chr = String.fromCharCode(key);
    // if ("1234567890qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM".indexOf(chr) < 0)
    //     return false;

}
      
function BloquearEnter(e){
    if (e.which == 13) {
            return false;
        }
}

function filterFloat(evt,input){
    // Backspace = 8, Enter = 13, ‘0′ = 48, ‘9′ = 57, ‘.’ = 46, ‘-’ = 43
    var key = window.Event ? evt.which : evt.keyCode;    
    var chark = String.fromCharCode(key);
    var tempValue = input.value+chark;
    if(key >= 48 && key <= 57){
        if(filter(tempValue)=== false){
            return false;
        }else{       
            return true;
        }
    }else{
          if(key == 8 || key == 13 || key == 0) {     
              return true;              
          }else if(key == 46){
                if(filter(tempValue)=== false){
                    return false;
                }else{       
                    return true;
                }
          }else{
              return false;
          }
    }
}

function filter(__val__){
    var preg = /^([0-9]+\.?[0-9]{0,2})$/; 
    if(preg.test(__val__) === true){
        return true;
    }else{
       return false;
    }
    
}

function makeArray(len) {
    for (var i = 0; i < len; i++) this[i] = null;
    this.length = len;
}


function seleccionarTodos(chcklst, select) {
    var seleccion = document.getElementById(select).innerText;

    if (document.getElementById(chcklst) != null) {
        var tags = document.getElementById(chcklst).getElementsByTagName('input');

        if (seleccion == 'Seleccionar Todos') {
            for (var i = 0; i < tags.length; i++)
                tags[i].checked = true;

            document.getElementById(select).innerText = 'Deseleccionar Todos';
        }

        else {
            for (var i = 0; i < tags.length; i++)
                tags[i].checked = false;

            document.getElementById(select).innerText = 'Seleccionar Todos';
        }
    }
}

function validaNumero(evento) {//permite digitar solo numeros
    //---------------------------------------------------------------------------------------//  
    var key = validaEvento(evento);
    if (key == 8) { return true; }

    if (((key > 0 && key < 48) || (key > 57 && key < 255))) {
        if (window.event) {
            evento.keyCode = 0;
            return false;
            //respuesta = false;
        }
        else if (evento.which) {
            return false;
        }
    }
} //end function validaNumero

//Evento de Teclado
function validaEvento(e) {
    var keynum;
    if (window.event)
    { keynum = e.keyCode; }
    else if (e.which)
    { keynum = e.which; }
    return keynum;
}

function validaNumero_letras(evento) {//permite digitar solo numeros
    //---------------------------------------------------------------------------------------//  
    var key = validaEvento(evento);
    if (key == 8) { return true; }

    if (((key > 0 && key < 48) || (key > 57 && key < 65) || (key > 90 && key < 97) || (key > 122)) && (key != 45) && (key != 32) && (key != 46) && (key != 43) && (key != 164) && (key != 165) && (key != 95)) {
        if (window.event) {
            evento.keyCode = 0;
            return false;
            //respuesta = false;
        } else if (evento.which) {
            return false;

        }
    }
}

function validaNumeroDec(evento, txt) {//permite digitar solo numeros y punto para decimales
    //---------------------------------------------------------------------------------------//  
    var key = validaEvento(evento);

    if (key == 8) { return true; }

    if (((key > 0 && key < 48) || (key > 57 && key < 255)) && (key != 46)) {

        if (window.event) {
            evento.keyCode = 0;
            return false;
        }
        else if (evento.which) {
            return false;
        }

    }
}

function validaNumero_letras_P(evento) {//permite digitar solo numeros
    //---------------------------------------------------------------------------------------//  
    var key = validaEvento(evento);
    if (key == 8) { return true; }

    if (((key > 0 && key < 48) || (key > 57 && key < 65) || (key > 90 && key < 97) || (key > 122)) && (key != 40) && (key != 41) && (key != 32)) {

        if (window.event) {
            evento.keyCode = 0;
            return false;
            //respuesta = false;
        }
        else if (evento.which) {
            return false;
        }
    }
}

function validaNumero_letras_C(evento) {//permite digitar solo numeros y Corchetes
    //---------------------------------------------------------------------------------------//  
    var key = validaEvento(evento);
    if (key == 8) { return true; }

    if (((key > 0 && key < 48) || (key > 57 && key < 65) || (key > 90 && key < 97) || (key > 122)) && (key != 91) && (key != 93) && (key != 32) && (key != 61) && (key != 59)) {

        if (window.event) {
            evento.keyCode = 0;
            return false;
            //respuesta = false;
        }
        else if (evento.which) {
            return false;
        }
    }
}

function validaNumero_letras_Abr(evento) {//permite digitar letras, numeros, simbolos, para abreviatura.
    //---------------------------------------------------------------------------------------//  
    var key = validaEvento(evento);
    if (key == 8) { return true; }

    if (((key > 0 && key < 48) || (key > 57 && key < 65) || (key > 90 && key < 97) || (key > 122)) && (key != 43) && (key != 45) && (key != 46) && (key != 95) && (key != 32)) {

        if (window.event) {
            evento.keyCode = 0;
            return false;
            //respuesta = false;
        }
        else if (evento.which) {
            return false;
        }
    }
}


function ValidarFormatoAlias(evt)
{
    if (window.event) {//asignamos el valor de la tecla a que inicio el evento
        keynum = evt.keyCode;
    }
    else {
        keynum = evt.which;
    }
    //Formato: NO aceptar espacios en blanco
    if (keynum != 32) {
        return true;
    }
    else {
        return false;
    }
}

// formatea un numero según una mascara dada ej: "-$###,###,##0.00"
//
// elm   = elemento html <input> donde colocar el resultado
// n     = numero a formatear
// mask  = mascara ej: "-$###,###,##0.00"
// force = formatea el numero aun si n es igual a 0
//
// La función devuelve el numero formateado

function MASK(form, n, mask, format) {
    if (format == "undefined") format = false;
    if (format || NUM(n)) {
        dec = 0, point = 0;
        x = mask.indexOf(".") + 1;
        if (x) { dec = mask.length - x; }

        if (dec) {
            n = NUM(n, dec) + "";
            x = n.indexOf(".") + 1;
            if (x) { point = n.length - x; } else { n += "."; }
        } else {
            n = NUM(n, 0) + "";
        }
        for (var x = point; x < dec; x++) {
            n += "0";
        }
        x = n.length, y = mask.length, XMASK = "";
        while (x || y) {
            if (x) {
                while (y && "#0.".indexOf(mask.charAt(y - 1)) == -1) {
                    if (n.charAt(x - 1) != "-")
                        XMASK = mask.charAt(y - 1) + XMASK;
                    y--;
                }
                XMASK = n.charAt(x - 1) + XMASK, x--;
            } else if (y && "$0".indexOf(mask.charAt(y - 1)) + 1) {
                XMASK = mask.charAt(y - 1) + XMASK;
            }
            if (y) { y-- }
        }
    } else {
        XMASK = "";
    }
    if (form) {
        form.value = XMASK;
        if (NUM(n) < 0) {
            form.style.color = "#FF0000";
        } else {
            form.style.color = "#000000";
        }
    }
    return XMASK;
}

// Convierte una cadena alfanumérica a numérica (incluyendo formulas aritméticas)
//
// s   = cadena a ser convertida a numérica
// dec = numero de decimales a redondear
//
// La función devuelve el numero redondeado

function NUM(s, dec) {
    for (var s = s + "", num = "", x = 0; x < s.length; x++) {
        c = s.charAt(x);
        if (".-+/*".indexOf(c) + 1 || c != " " && !isNaN(c)) { num += c; }
    }
    if (isNaN(num)) { num = eval(num); }
    if (num == "") { num = 0; } else { num = parseFloat(num); }
    if (dec != undefined) {
        r = .5; if (num < 0) r = -r;
        e = Math.pow(10, (dec > 0) ? dec : 0);
        return parseInt(num * e + r) / e;
    } else {
        return num;
    }
}

