$(document).ready(function () {
    $('.selectpicker').selectpicker({
        style: 'btn-default',
        liveSearch: 'true',
        noneSelectedText: 'Ningun objeto seleccionado',
        selectAllText: 'Todos los objetos seleccionados',
        actionsBox: 'true',
        deselectAllText: 'Deseleccionar todos',
        selectAllText: 'Seleccionar todos',
        selectedTextFormat: 'count > 3',
        size: 'auto',
        countSelectedText: "{0} objetos seleccionados"
    });
});