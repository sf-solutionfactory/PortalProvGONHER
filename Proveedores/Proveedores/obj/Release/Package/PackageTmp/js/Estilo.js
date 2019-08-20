/*
 * Autor : Luis Jesús Castro Guerrero.
 * Fecha : lunes 10 de marzo 2014, 9:16 AM.
 */
$(function(){
    $(".tblComun td:nth-child(2n+1)").addClass("td-impar");
    $(".tblComun td:nth-child(2n)").addClass("td-par");
    
    $("#btnPopUp").click(function(){
        //$("body").html($("body").html() + "<div style='position:absolute;height:100%;weight:100%;background-color:red;z-index:1000;'>nb</div>");
       //$("#pop-up").dialog();
//       $.colorbox({html:"<h1>Welcome</h1>"});
       $.colorbox({
           html: $("#pop-up").html()
       });
    });
    
    $(".tblCV tr:nth-child(2n+1) td:first-child").addClass("tblCV-CamImpar");
    $(".tblCV tr:nth-child(2n) td:first-child").addClass("tblCV-CamPar");
    $(".tblCV tr:nth-child(2n) td").addClass("tblCV-ValPar");
    $(".tblCV tr:nth-child(2n+1) td").addClass("tblCV-ValImpar");
//        $("#msgCloser").click(function(){
//            $("#pop-up").hide();
    //        });

    $(".estatus-detalle").click(function () {
        var mensaje = $(this).attr('msg');
        mensaje = $.parseHTML(mensaje);
        if (mensaje != "" && mensaje != null) {
            $("#ContentPlaceHolder1_lblDialog").text("");
            $("#ContentPlaceHolder1_lblDialog").append(mensaje);
            if ($.trim($("#ContentPlaceHolder1_lblDialog").text()) != "") {
                dialog = $("#ContentPlaceHolder1_lblDialog").dialog({
                    autoOpen: false,
                    height: 350,
                    width: 400,
                    modal: true,
                    buttons: {
                        "Aceptar": function () {
                            dialog.dialog("close");
                        },
                        Cancel: function () {
                            dialog.dialog("close");
                        }
                    },
                    close: function () {

                    }
                });
                dialog.dialog("open");
                return true;
            }
            
        }
    });
    

    $(".cargadorXml").click(function(){
        //$.colorbox({html: $("#pop-up").html()});//Esto es mientras debugueo
    });
});

function refrescarEstiloTblComun() {
    $(".tblComun td:nth-child(2n+1)").addClass("td-impar");
    $(".tblComun td:nth-child(2n)").addClass("td-par");
}
