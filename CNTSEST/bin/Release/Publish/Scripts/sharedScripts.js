function deshabilitar() {
    document.getElementById("submit").value = "Enviando...";
    document.getElementById("submit").disabled = true;
    $(".submit").value = "Enviando...";
    $(".submit").disabled = true;
    //setInterval(function () { habilitar(); }, 3000, null);

    return true;
}

function habilitar() {
    document.getElementById("submit").value = "Guardar";
    document.getElementById("submit").disabled = false;

    $(".submit").value = "Guardar";
    $(".submit").disabled = false;
    return true;
}

$(document).ready(function () {
    $('[data-toggle="popover"]').popover({
        container: 'body'
    });
});

function cppc(cadena) {
    var len = cadena.length;
    var i = 0;
    var res = "";
    for (i = 0; i < len; i++) {
        if (cadena.charAt(i) == ',') {
            res += '.';
        }
        else {
            res += cadena.charAt(i);
        }
    }
    return res;
}

function initTables(route) {
    $(route + ' .results').DataTable({
        responsive: true,
        pagingType: "full_numbers",
        lengthChange: true,
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        language: {
            emptyTable: "Sin resultados para los criterios de consulta",
            info: "Registros _START_ al _END_ de _TOTAL_",
            infoEmpty: "Sin resultados que mostrar",
            infoFiltered: "(filtrados de un total de _MAX_)",
            search: "Buscar",
            zeroRecords: "Ning&uacute;n dato encontrado",
            paginate: {
                first: "<<",
                next: ">",
                last: ">>",
                previous: "<"
            },
            lengthMenu: "Mostrar _MENU_ registros"
        }
    });
}

jQuery('.S1').on('keypress', function (e) {
    console.log(e.keyCode);
    if (e.keyCode == 101 || e.keyCode == 45 || e.keyCode == 46 || e.keyCode == 43 || e.keyCode == 44 || e.keyCode == 47) {
        return false;
    }
    soloNumeros(e.keyCode);
});

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}