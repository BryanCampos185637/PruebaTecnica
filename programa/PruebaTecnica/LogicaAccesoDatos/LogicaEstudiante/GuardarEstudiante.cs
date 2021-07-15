using MediatR;
using Modelo;
using Persistencia;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.LogicaEstudiante
{
    public class GuardarEstudiante
    {
        public class Ejecuta : IRequest<string>
        {
            [Required(ErrorMessage = "El nombre es requerido")]
            [Display(Name ="Nombre alumno:")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
            [DataType(DataType.Date)]
            [Display(Name = "Fecha nacimiento:")]
            public DateTime? FechaNacimiento { get; set; }

            [Required(ErrorMessage ="El sexo es requerido")]
            [Display(Name = "Sexo del alumno:")]
            public int? SexoId { get; set; }



            //propiedades extra
            [Display(Name = "Institucion:")]
            public string Intitucion { get; set; }
            [DataType(DataType.Date)]
            [Display(Name = "Fecha de inicio:")]
            public DateTime FechaInicio { get; set; }
            [DataType(DataType.Date)]
            [Display(Name = "Fecha de finalizacion:")]
            public DateTime FechaFinalizo { get; set; }

            //crear listados en los formularios
            public List<Sexo>ListaSexo { get; set; }
            public List<Detalle>ListaDetalle { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, string>
        {
            private readonly AppDbContext context;
            public Manejador(AppDbContext dbContext)
            {
                context = dbContext;
            }
            public async Task<string> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        #region guardar encabezado
                        var encabezado = new Encabezado
                        {
                            Nombre = request.Nombre,
                            FechaNacimiento = (DateTime)request.FechaNacimiento,
                            Edad = Helper.CalcularEdad.Calular((DateTime)request.FechaNacimiento),
                            SexoId = (int)request.SexoId
                        };
                        context.Encabezado.Add(encabezado);
                        await context.SaveChangesAsync();
                        #endregion

                        #region guardar detalle y estudiante
                        if (request.ListaDetalle!=null && request.ListaDetalle.Count > 0)
                        {
                            foreach(var item in request.ListaDetalle)
                            {
                                #region insercion de detalle
                                var detalle = item;
                                context.Detalle.Add(detalle);
                                await context.SaveChangesAsync();
                                #endregion

                                #region insercion de estudiante
                                //una vez e guardado el encabezado y detalle, inserto en la tabla estudiante
                                context.Estudiante.Add(new Estudiante
                                {
                                    EncabezadoId = encabezado.EncabezadoId,
                                    DetalleId = detalle.DetalleId
                                });
                                await context.SaveChangesAsync();
                                #endregion
                            }
                        }
                        else
                        {
                            return "Debes agregar por lo menos un detalle";
                        }
                        #endregion
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return "Error no se pudo guardar el estudiante";
                    }
                }
                return "ok";
            }
        }
    }
}
