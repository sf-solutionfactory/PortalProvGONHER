function validar() {
    //alert("val");
    var numDiv;
    //numDiv = document.querySelectorAll('txtValidar');
    numDiv = $('.txtValidar');
    //numDiv = $(document.querySelectorAll('.txtValidar'))
    //alert($(numDiv[0]).val());
    $("#ContentPlaceHolder1_hidVerificar").val("si");
    //alert($("#ContentPlaceHolder1_hidVerificar").val());
    var puedePasar = true;
    //alert("antes");
    jQuery.each(numDiv, function (i, val) {
        //alert($(numDiv[i]).val());
        if ($(numDiv[i]).val() == "" || $(numDiv[i]).val() == null) {
            $("#ContentPlaceHolder1_hidVerificar").val("no");
            puedePasar = false;
            return false;
            
        }
    });

    if (puedePasar == true) {
        //alert("todo ok");
        return 1;
    }
    if (puedePasar == false) {
        //alert("existen campos nulos");
        //$("#ContentPlaceHolder1_lblDialog").text("Existen campos nulos");
        
        //$("#ContentPlaceHolder1_lblDialog").text("nuevo texto desde valnull");
        //alert($("#ContentPlaceHolder1_lblDialog").text());
        //mostrarDialog();
        return 0;
    }
   
}

//function validar() {
//    var numDiv;
//    numDiv = document.querySelectorAll('txtValidar');
//    $("#ContentPlaceHolder1_hidVerificar").val("si");
//    alert($("#ContentPlaceHolder1_hidVerificar").val());
//    var puedePasar = true;
//    alert("antes");
//    jQuery.each(numDiv, function (i, val) {
//        alert(numDiv[i].value);
//        if (numDiv[i].value == "" || numDiv[i].value == null) {
//            alert(numDiv[i].value);
//            $("#ContentPlaceHolder1_hidVerificar").val("no");
//            puedePasar = false;
//        }
//    });

//    if (puedePasar == true) {
//        alert("todo ok");
//        return 1;
//    }
//    if (puedePasar == false) {
//        alert("existen campos nulos");
//        return 0;
//    }

//}
