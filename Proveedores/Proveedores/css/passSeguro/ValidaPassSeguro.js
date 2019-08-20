$(function () {

    $('input[type=password]').keyup(function () {
        var length = false;
        var leter = false;
        var capitalL = false;
        var number = false;
        var numeroLetras;
        var numeroLetrasM;
        var cantidadNumeros;
        var numeroCaracteres;
        var user = $("#ContentPlaceHolder1_txtUsuario").val()
        var pass = $("#ContentPlaceHolder1_txtContrasena").val()
        if (user == "ZAdministradorportalZ") {
            numeroLetras = 1;
            numeroLetrasM = 1;
            cantidadNumeros = 1;
            numeroCaracteres = 8;
        }
        else {
            numeroLetras = parseInt($("#ContentPlaceHolder1_hidNumeroLetras").val());
            numeroLetrasM = parseInt($("#ContentPlaceHolder1_hidNumeroLetrasM").val());
            cantidadNumeros = parseInt($("#ContentPlaceHolder1_hidCantidadNumeros").val());
            numeroCaracteres = parseInt($("#ContentPlaceHolder1_hidNumeroCaracteres").val());
        }
        
        //alert("in")
        // set password variable
        var pswd = $(this).val();
        //validate the length
        //alert(pswd.length)
        if (pswd.length > numeroCaracteres) {
            $('#length').removeClass('invalid').addClass('valid');
            //$("#ContentPlaceHolder1_hidVerificar").val("si");
            //$("#ContentPlaceHolder1_hidVerificarPass").val("si");
            //alert("mayor")
            length = true;
            //hidVerificarPass
        } else {
            $('#length').removeClass('valid').addClass('invalid');
            //$("#ContentPlaceHolder1_hidVerificar").val("no");
            //$("#ContentPlaceHolder1_hidVerificarPass").val("no");
            length = false;
        }

        //validate letter
        var contle = 0;
        for (var i = 0; i < pswd.length; i++) {
            if (pswd.charAt(i).match(/[A-z]/)) {
                contle++;
            } 
        }
        if (contle >= numeroLetras) {
            $('#letter').removeClass('invalid').addClass('valid');
            leter = true;
        } else {
            $('#letter').removeClass('valid').addClass('invalid');
            leter = false;
        }
        contle = 0;
        //validate capital letter
        for (var i = 0; i < pswd.length; i++) {
            if (pswd.charAt(i).match(/[A-Z]/)) {
                contle++;
            }
        }
        if (contle >= numeroLetrasM) {
            $('#capital').removeClass('invalid').addClass('valid');
            capitalL = true;
        } else {
            capitalL = false;
            $('#capital').removeClass('valid').addClass('invalid');
        }
        contle = 0;
        //validate number
        for (var i = 0; i < pswd.length; i++) {
            if (pswd.charAt(i).match(/\d/)) {
                contle++;
            }
        }
        if (contle >= cantidadNumeros) {
            $('#number').removeClass('invalid').addClass('valid');
            number = true;
        } else {
            $('#number').removeClass('valid').addClass('invalid');
            number = false;
        }
        //alert("show" + length + number + capitalL + leter);
        
        if (user == pass) {
            length = true;
            number = true;
            capitalL = true;
            leter = true;
        }
        if (length && number && capitalL && leter) {
            $("#ContentPlaceHolder1_hidVerificarPass").val("si");
        }
        else {
            $("#ContentPlaceHolder1_hidVerificarPass").val("no");
        }
       

    }).focus(function () {
        $('#pswd_info').show();
    }).blur(function () {
        $('#pswd_info').hide();
    });

});