
function workThis(button) {
    $("#dialog-confirm").dialog({
        resizable: false,
        height: 140,
        modal: true,
        buttons: {
            "Continuar": function () {
                $(this).dialog("close");
            
    var textoBoton = $.trim($(button).text());
    var rows = $(button).parents('tr');
    var cellsOfRow = rows[0].getElementsByTagName('td');
    var val = $.trim(cellsOfRow[0].innerHTML);
    var val2 = "";
    var val3 = "";
    var val4 = "";
    try {
        val2 = $.trim(cellsOfRow[1].innerHTML); 
        val3 = $.trim(cellsOfRow[2].innerHTML);
        val4 = $.trim(cellsOfRow[3].innerHTML);
    } catch (e) {
        val2 = "";
        val3 = "";
        val4 = "";
    }
    //alert(val +  " " + val2);
    var pantalla = $.trim($("#ContentPlaceHolder1_hidPantalla").val());
    switch (pantalla) {
        case "DesvincularGrupoProv":
            val2 = "";
            val3 = "";
            val4 = "";
            break;
        case "DesvincularGrupoNoticia":
            val2 = "";
            val3 = "";
            val4 = "";
            break;
        case "Proveedores":
            break;
        case "Roles":
            break;
        case "MostrarPantalla":
            break;
        case "Noticia":
            break;
        case "usuario":
            break;
        
        default:
            break;
    }
    var complemento = $.trim($("#ContentPlaceHolder1_hidComplementoUr").val());
    //alert(val);
    //alert(pantalla);
    //alert(textoBoton);
    //alert(complemento);
    //alert(val2);
    //alert(val3);
    //alert(val4);
    if (textoBoton == "Desactivar" || textoBoton == "Activar" || textoBoton ==  "Eliminar") {
        //alert("entra")
        if (textoBoton != "Desactivar" && textoBoton != "Eliminar" && textoBoton != "Activar") {
            $.post("DeleteAndUpdate.aspx",
            {
                identificador: val,
                pantalla: pantalla,
                desicion: textoBoton,
                complemento: complemento,
                valor2: val2,
                valor3: val3,
                valor4: val4
            },
            function (data) {
                document.location.href = data;
            }
        ).fail(function () {
            alert("La aplicación es actualmente inaccesible. Por favor revisa tu conexión a internet");
        });
        } else {
            $.post("DeleteAndUpdate.aspx",
            {
                identificador: val,
                pantalla: pantalla,
                desicion: textoBoton,
                complemento: complemento,
                valor2: val2,
                valor3: "",
                valor4: val4
            },
            function (data) {
                document.location.href = data;
            }
        ).fail(function () {
            alert("La aplicación es actualmente inaccesible. Por favor revisa tu conexión a internet");
        });
        }
            
        
        

    }
    else if (textoBoton == "Ver más") {
        switch (pantalla) {
            case "DesvincularGrupoNoticia":
                document.location.href = "DesvincularGrupoNoticia.aspx?toSee=" + val;
                break;
            case "DesvincularGrupoProv":
                document.location.href = "DesvincularGrupoProv.aspx?toSee=" + val;
                break;
            default:
                break;
        }
    }
    else {
        //alert("else");
        if (textoBoton == "Modificar") {
            switch (pantalla) {
                case "Instancia":
                    document.location.href = "instancia.aspx?toEdit=" + val;
                    break;
                case "Proveedores":
                    //document.location.href = "instancia.aspx?toEdit=" + val;
                    break;
                case "Roles":
                    document.location.href = "Roles.aspx?toEdit=" + val;
                    break;
                case "MostrarPantalla":
                    //document.location.href = "instancia.aspx?toEdit=" + val;
                    break;
                case "Noticia":
                    document.location.href = "Noticia.aspx?toEdit=" + val;
                    break;
                case "usuario":
                    document.location.href = "usuario.aspx?toEdit=" + val+"&"+ complemento;
                    //alert("usuario.aspx?toEdit=" + val + "&" + complemento);
                    break;
                case "DesvincularGrupoNoticia":
                    document.location.href = "GrupoNoticia.aspx?toEdit=" + val;
                    break;
                default:
                    break;
            }

        }
        else {
            alert("error, intente nuevamente");
        }
    
    }
                //}
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });

}

