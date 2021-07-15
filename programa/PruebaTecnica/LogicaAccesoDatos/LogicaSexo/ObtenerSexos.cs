using MediatR;
using Microsoft.EntityFrameworkCore;
using Modelo;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.LogicaSexo
{
    public class ObtenerSexos
    {
        public class Ejecuta : IRequest<List<Sexo>> 
        {
        }

        public class Manejador : IRequestHandler<Ejecuta, List<Sexo>>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext appDb)
            {
                context = appDb;
            }
            public async Task<List<Sexo>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                try
                {
                    var lst = await context.Sexo.ToListAsync();
                    return lst;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
