using MediatR;
using Microsoft.EntityFrameworkCore;
using Modelo.ViewModels;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.LogicaEstudiante
{
    public class ObtenerDetalles
    {
        public class Ejecuta : IRequest<List<DetalleVM>>
        {
            public int idEstudiante { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, List<DetalleVM>>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext appDbContext)
            {
                context = appDbContext;
            }
            public async Task<List<DetalleVM>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    var lista = await context.Estudiante.Where(p => p.EstudianteId.Equals(request.idEstudiante))
                    .Include(p => p.Detalle).Include(p=>p.Encabezado).Select(p => new DetalleVM
                    {
                        NombreEstudiante=p.Encabezado.Nombre,
                        Intitucion = p.Detalle.Intitucion,
                        FechaFinalizo = p.Detalle.FechaFinalizo.ToShortDateString(),
                        FechaInicio = p.Detalle.FechaInicio.ToShortDateString()
                    }).ToListAsync();
                    return lista;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
