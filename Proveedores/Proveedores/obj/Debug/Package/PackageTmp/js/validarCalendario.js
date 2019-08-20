function validarCalendario(campo) {
    //var campo = document.getElementById(campo);
    var dato = $(campo).val();
    //alert(dato);
    if (dato != "" && dato != null) {
        var fecha = dato.split('/');
        if (fecha[0].length == 2 && fecha[0] % 1 == 0 &&
            fecha[1].length == 2 && fecha[1] % 1 == 0 &&
            fecha[2].length == 4 && fecha[2] % 1 == 0
            )
        {
            $('#ContentPlaceHolder1_hidVerificar').val("si");
            //alert("all ok");
            return 1;
        }
        else {
            $('#ContentPlaceHolder1_hidVerificar').val("noCalendario");
            //$("#ContentPlaceHolder1_lblDialog").text("llenar proveedores");
            //alert("las fechas no cumplen con el formato adecuado, \n se recoienda elegir la fecha con el calendario de ayuda");
            return 0;
        }
    }

}