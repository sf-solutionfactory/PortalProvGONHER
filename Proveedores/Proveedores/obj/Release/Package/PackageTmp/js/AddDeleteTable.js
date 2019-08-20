
function addRow(tableID, val1, val2, val3) {
    //$("#" + tableID + " tbody").html($("#" + tableID + " tbody").html() + "<tr></tr>");

    var tr = $("#" + tableID + " tbody").append("<tr onclick=quitarRow(this);>" +   // onclick=\"alert('newww');\"
                                            "<td>" + val1 + "</td>" +
                                            "<td>" + val2 + "</td>" +
                                            "<td>" + val3 + "</td>" +
                                       "</tr>");
    refrescarEstiloTblComun();
}

function quitarRow(e) {
    //var index = index();
    //alert("quitar");
    var idToFind = $.trim(e.cells[0].innerHTML);
    //alert(idToFind + "primero ID");
    mostrarFilaPrimeraTabla('tableToOrder', idToFind);
    e.parentNode.removeChild(e);

}

function showRow(tableID, idVAlRow) {

}

function deleteRow(tableID, index) {
    try {
        var table = document.getElementById(tableID);
        table.deleteRow(index + 1);
    }
    catch (e) {

        alert(e);
    }
}

function ocultarFila(tableID, index) {
    try {
        var table = document.getElementById(tableID);
        table.deleteRow(index + 1);
    }
    catch (e) {

        alert(e);
    }
}


function takeIdSelecteds(tableID, nombreGrupo, idNoticia) {
    //alert(nombreGrupo + idNoticia);
    try {
        var table = document.getElementById(tableID);
        var rowCount = table.rows.length;
        var complete = "";
        for (var i = 1; i < rowCount; i++) {
            var row = table.rows[i];
            var chkbox = row.cells[0].innerHTML.split();
            if (i + 1 == rowCount) {
                complete += chkbox;
            }
            else {
                complete += chkbox + ',';
            }
        }
//alert(complete);
    } catch (e) {
        //alert(e);
    }
}


function takeIdSelectedsUl(IDObject, nombreGrupo, idNoticia, titulo) {
    //alert(nombreGrupo + " " + idNoticia);
    try {
        var ul = document.getElementById(IDObject);
        //alert($("#" + ID + " li").size());
        var complete = "";
        for (var i = 0; i < $("#" + IDObject + " li").size() ; i++) {
            x = $("#" + IDObject + " li").eq(i).find(".idProv").text();
            if (x != '' && complete != 'C_E') {
                if (i + 1 == $("#" + IDObject + " li").size()) {
                    complete += x;
                }
                else {
                    complete += x + ',';
                }
            }
            else {
                complete = "C_E";
            }

        }
        //$("#ContentPlaceHolder1_hidIDX").val(idNoticia);
        $("#ContentPlaceHolder1_hidIdSelected").val(complete);
        //alert(complete);

    } catch (e) {
    }
}

function ulPintarPrimero(IDObject) {

        if ($("#" + IDObject + " li").size() >= 1) {
            $("#" + IDObject + " li").eq(0).addClass("primerLi");
            $("#" + IDObject + " li").eq(0).attr('title', 'Sera tomado como el titular del nuevo grupo');
        }

}
function ulBorrar(IDObject) {

    for (var i = 0; i < $("#" + IDObject + " li").size() ; i++) {
        $("#" + IDObject + " li").eq(i).removeClass("primerLi");
    }
    

}


function takeIdSelectedsCheckBox(idTable) {
    
    try {
        var table = $("." + idTable);
        //alert($(".toCheck tbody tr"));
        //var rowCount = table.rows.length;
        var rowCount = $(".toCheck tbody tr").length;
        //alert($(".toCheck tbody tr").html());
        var complete = "";
        for (var i = 0; i < rowCount; i++) {
            var chkbox = $(".toCheck tbody tr").eq(i).find(".chk");
            if ($(chkbox).prop("checked")) {
                //alert("in if");
                var res = $(chkbox).val();
                    complete += res + ',';
            }
        }
        complete = complete.substring(0, complete.length - 1);
        //alert(complete);
        $("#ContentPlaceHolder1_hidValCheck").val(complete);
        //alert($("#ContentPlaceHolder1_hidValCheck").val() + "dddd");
        
    } catch (e) {
    }
}

function mostrarFilaPrimeraTabla(tableID, idToFind) {
    try {
        var table = document.getElementById(tableID);
        var rowCount = table.rows.length;
        for (var i = 1; i < rowCount; i++) {
            var row = table.rows[i];
            var valor = $.trim(row.cells[0].innerHTML);
            //alert(valor + "valloooooor");
            if (valor == idToFind) {
               // alert("entra");
                //row.style.display = "block";
                $(row).show();
                break;
            }
        }
    } catch (e) {

        //alert(e);
    }
}
