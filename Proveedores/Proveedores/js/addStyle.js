$(function(){

    $(".tblComun tbody tr").mouseover(function () {
        var tds = $(this).find("td");
        var center = $(this).find("center");
        tds.addClass("td-hover");
        center.addClass("td-hover");
        return false;
    });

    $(".tblComun tbody tr center").mouseover(function () {

        //var padre = $(".tblComun");
        //padre = $(padre).parent("tr");
        padre = $(this).parent(".tblComun");
        var tds = $(padre).find("td");
        var center = $(this).find("center");
        tds.removeClass("td-hover");
        center.removeClass("td-hover");

        var padre = $(this).parent("td");
        padre = $(padre).parent("tr");
        //alert(padre);
        var tds = $(padre).find("td");
        //alert(tds.length);
        var center = $(this).find("center");
        tds.addClass("td-hover");
        center.addClass("td-hover");
        return false;
    });

    $(".tblComun tbody tr").mouseout(function () {
        var tds = $(this).find("td");
        var center = $(this).find("center");
        tds.removeClass("td-hover");
        center.removeClass("td-hover");
        return false;
    });

    $(".tblComun tbody tr center").mouseout(function () {
        //alert("out center");
        var padre = $(this).parent("td");
        padre = $(padre).parent("tr");
        //padre = $(padre).parent("tr");
        var tds = $(padre).find("td");
        var center = $(this).find("center");
        tds.removeClass("td-hover");
        center.removeClass("td-hover");
        return false;
    });
});
