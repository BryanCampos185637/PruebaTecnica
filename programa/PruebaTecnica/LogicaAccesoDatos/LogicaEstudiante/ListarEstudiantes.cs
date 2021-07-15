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
    public class ListarEstudiantes
    {
        public class Ejecuta : IRequest<List<EstudianteVM>>{ }
        public class Manejador : IRequestHandler<Ejecuta, List<EstudianteVM>>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext dbContext)
            {
                context = dbContext;
            }
            public async Task<List<EstudianteVM>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    var listaEstudiante = await context.Estudiante
                                                .Include(p => p.Encabezado)
                                                .Select(p => new EstudianteVM
                                                {
                                                    EstudianteId = p.EstudianteId,
                                                    nombre = p.Encabezado.Nombre,
                                                    edad = p.Encabezado.Edad,
                                                    fechaNacimiento = p.Encabezado.FechaNacimiento.ToShortDateString(),
                                                    detalleId = p.DetalleId
                                                }).ToListAsync();
                    return listaEstudiante;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
