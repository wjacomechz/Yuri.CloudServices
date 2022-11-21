$(document).ready(MainMantFileUpload);

function MainMantFileUpload() {
    $("#tbl-archivos").DataTable({
        "responsive": true,
        "order": [[0, 'desc']],
        "buttons": [
            {
                "extend": 'excelHtml5',
                "text": '<i class="fa fa-file-excel-o" style="color:green; font-size: 15px;"></i> Excel',
                "exportOptions": {
                    "columns": [0, 1, 2, 3]
                }
            },
            {
                "extend": 'pdfHtml5',
                "title": 'Archivos subidos',
                "orientation": 'landscape',
                "exportOptions": {
                    "columns": [0, 1, 2, 3]
                },
                "customize": function (doc) {
                    doc.content[1].table.widths =
                        Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                }
            },
            {
                "extend": 'csv',
                "exportOptions": {
                    "columns": [0, 1, 2, 3]
                }
            },
        ],
        "lengthChange": false,
        "autoWidth": false,
        "pageLength": 10,
        "language": {
            "lengthMenu": "Mostrar _MENU_ registros por página",
            "zeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "info": "Mostrando página _PAGE_ de _PAGES_",
            "infoEmpty": "Ningún dato disponible en esta tabla",
            "infoFiltered": "(filtered from _MAX_ total records)",
            "sSearch": "Filtro:",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
        },
        "columns": [
            { "data": "FechaRegistro", "orderable": false, "Title": "Fecha Registro", "className": "export-col" },
            { "data": "NombreArchivo", "orderable": false, "Title": "Archivo", "className": "export-col" },
            { "data": "ExtensionArchivo", "orderable": false, "Title": "Extensión", "className": "export-col" },
            { "data": "UrlS3", "orderable": false, "Title": "Key S3", "className": "export-col" },
            {
                "data": null, "title": "-", "orderable": false, "className": "not-export-col", "render": function (data, type, row) {
                    var object_lista = (typeof data) == 'string' ? eval('(' + data + ')') : data;
                    var url_action = obtenerPathname("Download", "DescargarArchivo");
                    var etiqueta_form = "<form action='" + url_action + "' enctype='multipart/form-data' method='post'>";
                    etiqueta_form += "<input id='UrlS3' name='UrlS3' type='hidden' value='" + object_lista.UrlS3 + "'>";
                    etiqueta_form += "<input id='NombreArchvo' name='NombreArchivo' type='hidden' value='" + object_lista.NombreArchivo + "'>";
                    etiqueta_form += "<input id='ExtensionArchivo' name='ExtensionArchivo' type='hidden' value='" + object_lista.ExtensionArchivo + "'>";
                    etiqueta_form += "<button type='submit' title='Descargar soporte de la compra'><i class='fas fa-download'></i></button>"
                    etiqueta_form += "</form>";
                    return etiqueta_form;
                }
            },
        ],
    }).buttons().container().appendTo('#tbl-archivos_wrapper .col-md-6:eq(0)');
    $("#frm-subir-archivo").submit(function (e) {
        var data = new FormData();
        data.append("formFile", $("#ArchivoSoporte")[0].files[0]);
        data.append("Directorio", $("#cbxDirectorioS3 option:selected").val());
        e.preventDefault();
        if (!ValidarTamañoArchivo()) {
            alert("warning", "El tamaño del archivo soporte compra máximo es 4mb");
            return;
        }
        $.ajax({
            type: "POST",
            url: obtenerPathname("FileManagement", "SubirArchivo"),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            data: data,
            contentType: false,
            processData: false,
            success: function (result) {
                var jsonRespuesta = JSON.stringify(result);
                var model_respuesta = (typeof jsonRespuesta) == 'string' ? eval('(' + jsonRespuesta + ')') : jsonRespuesta;
                if (model_respuesta.success) {
                    ToastAlertDefault("success", model_respuesta.mensaje);
                }
                else {
                    MensajeSistema("warning", model_respuesta.mensaje);
                }
            },
            error: function () {
                alert("Error no definido frond-end.");
            }
        });
    });
}


function ValidarTamañoArchivo() {
    const MAXIMO_TAMANIO_BYTES = 4000000; // 1MB = 1 millón de bytes
    const archivo = $("#ArchivoSoporte")[0].files[0];
    if (archivo.size > MAXIMO_TAMANIO_BYTES) {
        $('#ArchivoSoporte').fileinput('clear');
        const tamanioEnMb = MAXIMO_TAMANIO_BYTES / 1000000;
        console.log(`El tamaño máximo es ${tamanioEnMb} MB`);
        return false;
    } else {
        return true;
    }
}