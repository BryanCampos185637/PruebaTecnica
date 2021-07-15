

window.onload = () => {
    mostrarDetalles();
};

function mostrarDetalles() {
    $.get('/Estudiante/mostrarDetalles', (data) => {
        let html = '';
        if (data.length > 0) {
            for (let i = 0; i < data.length; i++) {
                html += '<tr>';
                html += '<td>' + data[i].intitucion + '</td>';
                html += '<td>' + data[i].fechaInicio.replace('T00:00:00','') + '</td>';
                html += '<td>' + data[i].fechaFinalizo.replace('T00:00:00', '') + '</td>';
                html += '</tr>';
            }
        } else {
            html += '<tr class="text-center">';
            html += '<td colspan="3">No hay detalles</td>';
            html += '</tr>';
        }
        $('#tablaDetalle').html(html);
    })
}

function AgregarDetalle() {
    if (ValidarVacios()) {
        var frm = new FormData();
        capturarData(frm);
        $.ajax({
            url: '/estudiante/AgregarDetalleTemporal',
            type:'POST',
            processData: false,
            contentType: false,
            data: frm,
            success: function (rpt) {
                if (rpt == 'ok')
                {
                    Limpiar();
                    mostrarDetalles();
                }
                else
                    alert('Ocurrio un error intente mas tarde')
            }
        })
    } else {
        alert('Debes completar los campos')
    }
}

function Limpiar() {
    let input = $('.validacion');
    for (let i = 0; i < input.length; i++) {
        input[i].value = '';
        input[i].style.borderColor = '#ccc';
    }
}
function capturarData(frm) {
    frm.append('Intitucion', $('#Intitucion').val());
    frm.append('FechaInicio', $('#FechaInicio').val());
    frm.append('FechaFinalizo', $('#FechaFinalizo').val());
}

function ValidarVacios() {
    let result = true;
    let input = $('.validacion');
    for (let i = 0; i < input.length; i++) {
        if (input[i].value.trim() == '') {
            input[i].style.borderColor = 'red';
            result = false;
        } else {
            input[i].style.borderColor = '#ccc';
        }
    }
    return result;
}