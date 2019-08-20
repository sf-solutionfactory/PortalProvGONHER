function soloNumeros2(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " 1234567890";
    especiales = [8, 37, 39, 46];
    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}

function eliminarEntradasKey(e) {
    return false;
}

function revalidaNumeros() {
    var existenLetras = false;
    var arregloCampos = $(".soloNumeros2");
    for (var j = 0; j < arregloCampos.length; j++) {
        //alert($(arregloCampos[j]).val());
        if ($(arregloCampos[j]).val() != ""
            && $(arregloCampos[j]).val() != null
            ) {
            var miInteger = parseInt($(arregloCampos[j]).val())
            //alert(miInteger);
            //alert(isNaN(miInteger))
            if (isNaN(miInteger) == true) {
                //$(arregloCampos[j]).val("");
                existenLetras = true;
                break;
            }
        }
        
        
    }
    if (existenLetras) {
        $("#ContentPlaceHolder1_hidVerificar").val("noNumeros");

    }
}

function soloNumeros1(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " 1234567890";
    especiales = [8, 37, 39, 46];

    //var numcaracteres = document.forms[0].MainContent_txtUmbral.value.length;
    //numcaracteres = parseInt(numcaracteres);

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    //if (numcaracteres >= 1 && !tecla_especial) {
    //    return false;
    //}

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}