function detalle(id) {
    $.get('/Estudiante/MostrarDetalle?id=' + id, (data) =>
    {
        let contenido = '';
        //pintamos la data
        if (data.length > 0) {
            for (let i = 0; i < data.length; i++) {
                contenido += '<tr>';
                contenido += '<td>' + data[i].nombreEstudiante + '</td>';
                contenido += '<td>' + data[i].intitucion + '</td>';
                contenido += '<td>' + data[i].fechaInicio + '</td>';
                contenido += '<td>' + data[i].fechaFinalizo + '</td>';
                contenido += '</tr>';
            }
        }
        
        $('#tablaContenido').html(contenido);
    })
}