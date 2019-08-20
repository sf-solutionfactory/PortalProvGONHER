$(function () {
    

    $(".carga").click(function () {

        $("#dialog-confirm").dialog({
            resizable: false,
            height: 140,
            modal: true,
            buttons: {
                "Continuar": function () {
                    $(this).dialog("close");
                    $('body').css({ 'overflow': 'hidden'});
                    $('#ventana-flotante').fadeIn();
                    var ancho = $(window).width() + 10;
                    var alto = $(window).height() + 10;
                    var elemento = $('#ventana-flotante');
                    var posicion = elemento.position();

                    $('#ventana-flotante').css({
                        'margin-top': posicion.top - posicion.top - posicion.top,
                        'margin-left': posicion.left - posicion.left - posicion.left,
                        'width': ancho, 'height': alto,
                    });
                    

        $.post("CargaAutomaticaProv.aspx",
            {
                
            },
            function (data) {
                //alert(data);
                //var info = $(data).find("span");
                //var info = data.getElementById('lblRetroalimentacion');
                //alert(info);
                //$("#contenidoPost").html(data);
                //alert(data);
                //alert(data);
                document.location.href = "Proveedores.aspx";
            }).fail(function () {

                alert("La aplicación es actualmente inaccesible. Por favor revisa tu conexión a internet");
            });

                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });

    });

            
    
});