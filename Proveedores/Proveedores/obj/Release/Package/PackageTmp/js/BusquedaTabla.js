function doSearch(valor) {
    if (valor != null) {
        doSearchRecibeP(valor);
    }
    else {
        //alert("ya");
        var tableReg = document.getElementById('tableToOrder');
        if (tableReg != null) {
            var searchText = document.getElementById('searchTerm').value.toLowerCase();
            for ( var i = 1; i < tableReg.rows.length; i++) {
                var cellsOfRow = tableReg.rows[i].getElementsByTagName('td');
                var found = false;
                for (var j = 0; j < cellsOfRow.length && !found; j++) {
                    var compareWith = cellsOfRow[j].innerHTML.toLowerCase();
                    if (searchText.length == 0 || (compareWith.indexOf(searchText) > -1)) {
                        found = true;
                    }
                }
                if (found) {
                    tableReg.rows[i].style.display = '';
                } else {
                    tableReg.rows[i].style.display = 'none';
                }
            }
        }
    }
}

function doSearchSorter() {
    
    var tableReg = document.getElementById('sortable1');
    var searchText = document.getElementById('searchTermSort').value.toLowerCase();
    
        for (var i = 0; i < $("#" + 'sortable1' + " li").size() ; i++) {
            var cellsOfRow = $("#" + 'sortable1' + " li").eq(i).find("div");
            //var cellsOfRow = $("#" + 'sortable1' + " li").eq(i).$("label");
            
            
            var found = false;
            for (var j = 0; j < cellsOfRow.length && !found; j++) {
                //alert(cellsOfRow[j].innerHTML.toLowerCase());
                var compareWith = cellsOfRow[j].innerHTML.toLowerCase();
                if (searchText.length == 0 || (compareWith.indexOf(searchText) > -1)) {
                    found = true;
                }
            }
            if (found) {
                $("#" + 'sortable1' + " li").eq(i).show();
            } else {
                $("#" + 'sortable1' + " li").eq(i).hide();
            }
        }
    
}

function doSearchGroup(valor, numeros) {
    var modo = "ocultar";
    var count = $("." + valor).size();
    var mostrar;
    //var count = $("." + 'tblGroup' + " tr").size();
    for (var i = 0; i < $("." + valor).size() ; i++) {
        var contieneHide = $("." + valor ).eq(i).hasClass('hidd'); // solo los encabezados tienen la clase
        var contieneShow = $("." + valor ).eq(i).hasClass('show'); // solo los encabezados tienen la clase
        if (contieneHide)
        {
            $("." + valor).eq(i).removeClass("hidd");
            $("." + valor).eq(i).addClass("show");
            $("." + valor).eq(i).addClass("kz");
            var celIcon = $("." + valor).eq(i).find(".ico-expandir");
            celIcon.addClass("ico-contraer");
            celIcon.removeClass("ico-expandir");
            //alert(celIcon);
            modo = 'mostrar';
            
        }
        else if (contieneShow) {
            $("." + valor).eq(i).removeClass("show");
            $("." + valor).eq(i).addClass("hidd");
            $("." + valor).eq(i).removeClass("kz");
            var celIcon = $("." + valor).eq(i).find(".ico-contraer");
            celIcon.addClass("ico-expandir");
            celIcon.removeClass("ico-contraer");
            modo = 'ocultar';

        }
        else
        {
            if (modo == 'mostrar') {
                $("." + valor).eq(i).show();
            }
            else if (modo == 'ocultar') {
                $("." + valor).eq(i).hide();
            }
        }
    }
    //if (modo == 'mostrar') {
    //    //var padre = $(this).parent("a");
    //    //    padre = $(padre).parent("td");
    //    //    padre = $(padre).parent("tr");
    //    //    var oID = $(padre).attr("id");
    //    //    $(".fv").addClass("fl_verde");
        
        
    //} else if (modo == 'ocultar') {
    //    if (numeros.length <= 0) {
    //                //$(".fv").removeClass("fl_verde");
            
            
    //    }
    
}

function workDetalles(valor) {
    for (var i = 1; i < $("." + 'tblGroup' + " tr").size() ; i++) {
        var contieneHide = $("." + 'tblGroup' + " tr").eq(i).hasClass('hidd'); // solo los encabezados tienen la clase
        var contieneShow = $("." + 'tblGroup' + " tr").eq(i).hasClass('show'); // solo los encabezados tienen la clase
        if (contieneHide || contieneShow) {
            if (valor == 'ocultar') {
                $("." + 'tblGroup' + " tr").eq(i).removeClass("show");
                $("." + 'tblGroup' + " tr").eq(i).removeClass("hidd");
                $("." + 'tblGroup' + " tr").eq(i).addClass("hidd");
                $("." + 'tblGroup' + " tr").eq(i).removeClass("kz");
                var celIcon = $("." + 'tblGroup' + " tr").eq(i).find(".ico-contraer");
                celIcon.addClass("ico-expandir");
                celIcon.removeClass("ico-contraer");
            }
            else if (valor == 'mostrar') {
                $("." + 'tblGroup' + " tr").eq(i).removeClass("show");
                $("." + 'tblGroup' + " tr").eq(i).removeClass("hidd");
                $("." + 'tblGroup' + " tr").eq(i).addClass("show");
                $("." + 'tblGroup' + " tr").eq(i).addClass("kz");
                var celIcon = $("." + 'tblGroup' + " tr").eq(i).find(".ico-expandir");
                celIcon.addClass("ico-contraer");
                celIcon.removeClass("ico-expandir");
            }
        }
        else
        {
            if (valor == 'ocultar')
            {
                $("." + 'tblGroup' + " tr").eq(i).hide();
            }
            else if (valor == 'mostrar')
            { 
                $("." + 'tblGroup' + " tr").eq(i).show();
            }
        }
    }
}

function ocultaTablas(valor, posicion) {
    for (var i = 0; i < $("." + valor ).size() ; i++) {
        var mostrar = false; 
        if (posicion == i) {
            mostrar = true;
        }
        if (mostrar) {
            $("." + valor).eq(i).show();
        }
        else 
        {
            $("." + valor ).eq(i).hide();
        }

        }   
}

function doSearchSorterMostrarNoticia() {

    var tableReg = document.getElementById('sortable1');
    var searchText = document.getElementById('searchTermNoticia').value.toLowerCase();

    for (var i = 0; i < $("#" + 'sortable1' + " li").size() ; i++) {
        var cellsOfRow = $("#" + 'sortable1' + " li").eq(i).find(".DivSort");
        //var cellsOfRow = cellsOfRow.find("DivSort");
        //alert(cellsOfRow.length);
        //var cellsOfRow = $("#" + 'sortable1' + " li").eq(i).$("label");


        var found = false;
        for (var j = 0; j < cellsOfRow.length && !found; j++) {
            //alert(cellsOfRow[j].innerHTML.toLowerCase());
            var compareWith = cellsOfRow[j].innerHTML.toLowerCase();
            if (searchText.length == 0 || (compareWith.indexOf(searchText) > -1)) {
                found = true;
            }
        }
        if (found) {
            $("#" + 'sortable1' + " li").eq(i).show();
        } else {
            $("#" + 'sortable1' + " li").eq(i).hide();
        }
    }

}


function doSearchSelect() {

    var searchText = document.getElementById('searchTerm').value.toLowerCase();
    alert(searchText)
    //alert($("#" + 'ContentPlaceHolder1_lstBoxNoSelectedProv' + " option").size());
    for (var i = 0; i < $("#" + 'ContentPlaceHolder1_lstBoxNoSelectedProv' + " option").size() ; i++) {
        var cellsOfRow = $("#" + 'ContentPlaceHolder1_lstBoxNoSelectedProv' + " option").eq(i);
        var found = false;
        for (var j = 0; j < cellsOfRow.length && !found; j++) {
            //alert(cellsOfRow[j].innerHTML.toLowerCase());
            var compareWith = cellsOfRow[j].innerHTML.toLowerCase();
            if (searchText.length == 0 || (compareWith.indexOf(searchText) > -1)) {
                found = true;
                //alert("found")
            }
        }
        if (found) {
            alert("show")
            $("#" + 'ContentPlaceHolder1_lstBoxNoSelectedProv' + " option").eq(i).show();
        } else {
            $("#" + 'ContentPlaceHolder1_lstBoxNoSelectedProv' + " option").eq(i).hide();
            alert("hide")
        }
    }
}


function obtenerUUID() {
    var arruuid = [];
    $('.chkuuid:checked').each(
        function () {
            //alert("El checkbox con valor " + $(this).attr("id") + " está seleccionado");
            arruuid.push($(this).attr("id"));
        });
    if (arruuid.length > 0) {
        desadjuntar(arruuid.toString())
    }    
}
function desadjuntar(uuid) {
    var params = new Object();
    params.uuid = uuid;
    params = JSON.stringify(params);

    $.ajax({
        type: "POST",
        url: "../portal/facturas.aspx/desadjuntar",
        data: params,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: mostrar,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + ": " + XMLHttpRequest.responseText);
        }
    });
}
function mostrar(result) {
    var dialog;
    result = $.parseHTML(result.d);
    $("#ContentPlaceHolder1_lblDialog").text("");
    $("#ContentPlaceHolder1_lblDialog").append(result);
    dialog = $("#ContentPlaceHolder1_lblDialog").dialog({
        autoOpen: false,
        height: 350,
        width: 350,
        modal: true,
        buttons: {
            "Aceptar": function () {                
                dialog.dialog("close");
                recargarpag();
            },            
        },
        close: function () {

        }
    });
    dialog.dialog("open");
}
function recargarpag() {
    //Create a Form
    var $form = $("<form/>").attr("id", "data_form")
                .attr("action", "facturas.aspx")
                .attr("method", "post");
    $("body").append($form);

    //Append the values to be send
    AddParameter($form, "actualizar", "actualiza");
    //AddParameter($form, "technology", "de londres presenta");

    //Send the Form
    $form[0].submit();
}

function AddParameter(form, name, value) {
    var $input = $("<input />").attr("type", "hidden")
                        .attr("name", name)
                        .attr("value", value);
    form.append($input);
}