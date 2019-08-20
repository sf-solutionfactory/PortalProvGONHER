<%@ Page Title="" Language="C#" MasterPageFile="~/pagMaestra/MtrProveedor.Master" AutoEventWireup="true" CodeBehind="facturasMasiva.aspx.cs" Inherits="Proveedores.portal.facturasMasiva"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/lib/plupload/plupload.full.min.js"></script>
    <script src="../js/lib/plupload/queue/jquery.plupload.queue.min.js"></script>
    <link href="../js/lib/plupload/queue/css/jquery.plupload.queue.css" rel="stylesheet" />
    <script>
        $(function () {
            
            $("#facturas").addClass("active");

            var uploader = $('#uploader').pluploadQueue({
                url: 'facturasCarga.aspx',
                filters: [
                  { title: "Facturas XML", extensions: "xml,XML,Xml" }
                ],
                rename: false,
                flash_swf_url: '../js/lib/plupload/Moxie.swf',
                silverlight_xap_url: '../js/lib/plupload/Moxie.xap',
                prevent_duplicates: true,
                preinit: {
                    UploadComplete: function (up, files) {
                        //      Disparado cuando la carga se complete
                        //alert("Done! -- UP:" + up + " -- files: " + files);
                        $.post("facturasResumen.aspx", {}, function (data) {
                            $("#divResumen").html(data);
                        });

                        //Ocultar el cargador una ves que termina de procesar los archivos
                        uploader.hide();
                        
                    }
                }
            });
            
            $(".plupload_header_title").html("Cargador masivo de facturas");
            $(".plupload_header_text").html("Selecciona las facturas que desea subir simultáneamente");
            $(".plupload_filelist_header > .plupload_file_name").html("Nombre del archivo");
            $(".plupload_filelist_header > .plupload_file_size").html("Tamaño");
            $(".plupload_filelist_header > .plupload_file_status").html("Estatus");

            $(".plupload_add").html("Adjuntar archivos");
            $(".plupload_start").html("Cargar archivos");
        });
    </script>
    <label class="h1">Carga masiva de facturas</label>
    <div id="uploader">
        <p>Your browser doesn't have Flash, Silverlight or HTML5 support.</p>
        <p>Tu navegador no soporta Flash, Silverlight o HTML.</p>
    </div>
    <div id="divResumen">

    </div>
</asp:Content>
