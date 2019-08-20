function validateEmail(idCampo) {
    $("#ContentPlaceHolder1_hidVerificar").val("si");
    var campo = document.getElementById(idCampo);
    //var RegExPattern = /(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$/; // ejemplo clave segura
    var RegExPattern = /[\w-\.]{3,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;  /// correo electronico
    var errorMessage = ' el email no cumple con caracteristicas necesarias';
    if ((campo.value.match(RegExPattern)) && (campo.value != '')) {
        return 1;
    } else {
        //alert(errorMessage);
        
        $("#ContentPlaceHolder1_hidVerificar").val("noEmail");
        //$("#ContentPlaceHolder1_lblDialog").val(errorMessage);
        mostrarDialog();
        campo.focus();
        return 0;
    }
   
}