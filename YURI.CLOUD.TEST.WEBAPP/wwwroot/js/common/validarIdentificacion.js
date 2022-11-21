  /**
     * @Pasos  del algoritmo
     * 1.- Se debe validar que tenga 10 numeros
     * 2.- Se extrae los dos primero digitos de la izquierda y compruebo que existan las regiones
     * 3.- Extraigo el ultimo digito de la cedula
     * 4.- Extraigo Todos los pares y los sumo
     * 5.- Extraigo Los impares los multiplico x 2 si el numero resultante es mayor a 9 le restamos 9 al resultante
     * 6.- Extraigo el primer Digito de la suma (sumaPares + sumaImpares)
     * 7.- Conseguimos la decena inmediata del digito extraido del paso 6 (digito + 1) * 10
     * 8.- restamos la decena inmediata - suma / si la suma nos resulta 10, el decimo digito es cero
     * 9.- Paso 9 Comparamos el digito resultante con el ultimo digito de la cedula si son iguales todo OK sino existe error.     
    */

function ValidarCedula(cedula)
{
  //Preguntamos si la cedula consta de 10 digitos
  if(cedula.length == 10)
  {

        //Obtenemos el digito de la region que sonlos dos primeros digitos
        var digito_region = cedula.substring(0,2);
        
        //Pregunto si la region existe ecuador se divide en 24 regiones
        if( digito_region >= 1 && digito_region <=24 ){

          // Extraigo el ultimo digito
          var ultimo_digito   = cedula.substring(9,10);

          //Agrupo todos los pares y los sumo
          var pares = parseInt(cedula.substring(1,2)) + parseInt(cedula.substring(3,4)) + parseInt(cedula.substring(5,6)) + parseInt(cedula.substring(7,8));

          //Agrupo los impares, los multiplico por un factor de 2, si la resultante es > que 9 le restamos el 9 a la resultante
          var numero1 = cedula.substring(0,1);
          var numero1 = (numero1 * 2);
          if( numero1 > 9 ){ var numero1 = (numero1 - 9); }

          var numero3 = cedula.substring(2,3);
          var numero3 = (numero3 * 2);
          if( numero3 > 9 ){ var numero3 = (numero3 - 9); }

          var numero5 = cedula.substring(4,5);
          var numero5 = (numero5 * 2);
          if( numero5 > 9 ){ var numero5 = (numero5 - 9); }

          var numero7 = cedula.substring(6,7);
          var numero7 = (numero7 * 2);
          if( numero7 > 9 ){ var numero7 = (numero7 - 9); }

          var numero9 = cedula.substring(8,9);
          var numero9 = (numero9 * 2);
          if( numero9 > 9 ){ var numero9 = (numero9 - 9); }

          var impares = numero1 + numero3 + numero5 + numero7 + numero9;

          //Suma total
          var suma_total = (pares + impares);

          //extraemos el primero digito
          var primer_digito_suma = String(suma_total).substring(0,1);

          //Obtenemos la decena inmediata
          var decena = (parseInt(primer_digito_suma) + 1)  * 10;

          //Obtenemos la resta de la decena inmediata - la suma_total esto nos da el digito validador
          var digito_validador = decena - suma_total;

          //Si el digito validador es = a 10 toma el valor de 0
          if(digito_validador == 10)
            var digito_validador = 0;

          //Validamos que el digito validador sea igual al de la cedula
          if(digito_validador == ultimo_digito){
            return true;
            //console.log('la cedula:' + cedula + ' es correcta');
          }else{
            return false;
            //console.log('la cedula:' + cedula + ' es incorrecta');
          }
          
        }else{
          // imprimimos en consola si la region no pertenece
          return false;
          //console.log('Esta cedula no pertenece a ninguna region');
        }
  }
  else
  {
        //imprimimos en consola si la cedula tiene mas o menos de 10 digitos
        return false;
        //console.log('Esta cedula tiene menos de 10 Digitos');
  }    
}

//Validador de Cedula
function fnValidacionCedula(valCedula)
{
    try {
        var i = 0;
        var cedula = valCedula;
        if (cedula == "")
            return true;
        var Verificador = 0;
        var coefValCedula = [2, 1, 2, 1, 2, 1, 2, 1, 2];
        var cedulaCorrecta;
        if (isNaN(cedula)) {
            cedulaCorrecta = false;
        }
        else {
            if (cedula.substr(0, 3) == "000") {
                cedulaCorrecta = false;
            }
            else {

                if (cedula.length == 10) {
                    // PROVINCIA  DE EMISION DE RUC
                    // ECUADOR: 1-24 
                    // EXTRANJEROS: 30
                    var provincia = cedula.substr(0, 2);
                    if ((provincia > 0 && provincia <= 24) || provincia == 30) {

                        var tercerDigito = parseInt(cedula.substr(2, 1));
                        if (tercerDigito < 7) {
                            // Coeficientes de validación cédula
                            // El decimo digito se lo considera dígito verificador
                            var verificador = parseInt(cedula.substr(9, 1));// obtiene EL 10
                            var suma = 0;
                            var digito = 0;
                            for (var i = 0; i < (cedula.length - 1) ; i++) {
                                digito = parseInt(cedula.substr(i, 1)) * coefValCedula[i];
                                suma += parseInt(((digito % 10)) + parseInt((digito / 10)));
                            }

                            if ((suma % 10 == 0) && (suma % 10 == verificador)) {//CORRECTA
                                cedulaCorrecta = true;

                            } else if ((10 - (suma % 10)) == verificador) {//CORRECTA
                                cedulaCorrecta = true;

                            } else {
                                cedulaCorrecta = false;
                            }
                        } else {
                            cedulaCorrecta = false;// valida tercer digito
                        }
                    } else {
                        cedulaCorrecta = false;
                    }
                } else {
                    cedulaCorrecta = false;
                }
            }
        }
        if (!cedulaCorrecta) {
            return false;
        }
        else {
            return true;
        }

        return cedulaCorrecta;
    } catch (err) {

        alert("Ocurrió un error en la función Validación de Cédula.");
        return false;
    }
}

function fnValidarRUC(valRUC) {
    var number = valRUC;
    var dto = number.length;
    var valor;
    var acu = 0;
    if (number == "") {
        alert('No has ingresado ningún dato, porfavor ingresar los datos correspondientes.');
    }
    else {
        for (var i = 0; i < dto; i++) {
            valor = number.substring(i, i + 1);
            if (valor == 0 || valor == 1 || valor == 2 || valor == 3 || valor == 4 || valor == 5 || valor == 6 || valor == 7 || valor == 8 || valor == 9) {
                acu = acu + 1;
            }
        }
        if (acu == dto) {
            while (number.substring(10, 13) != 001) {
                alert('Los tres últimos dígitos no tienen el código del RUC 001.');
                return;
            }
            while (number.substring(0, 2) > 24) {
                alert('Los dos primeros dígitos no pueden ser mayores a 24.');
                return;
            }
            alert('El RUC está escrito correctamente');
            alert('Se procederá a analizar el respectivo RUC.');
            var porcion1 = number.substring(2, 3);
            if (porcion1 < 6) {
                alert('El tercer dígito es menor a 6, por lo \ntanto el usuario es una persona natural.\n');
            }
            else {
                if (porcion1 == 6) {
                    alert('El tercer dígito es igual a 6, por lo \ntanto el usuario es una entidad pública.\n');
                }
                else {
                    if (porcion1 == 9) {
                        alert('El tercer dígito es igual a 9, por lo \ntanto el usuario es una sociedad privada.\n');
                    }
                }
            }
        }
        else {
            alert("ERROR: Por favor no ingrese texto");
        }
    }
}