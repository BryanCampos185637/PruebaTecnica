using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo;

namespace Persistencia.ConfiModelo
{
    public class EncabezadoConfig
    {
        public EncabezadoConfig(EntityTypeBuilder<Encabezado> entityTypeBuilder)
        {
            entityTypeBuilder.Property(p => p.Edad).IsRequired();
            entityTypeBuilder.Property(p => p.FechaNacimiento).IsRequired().HasColumnType("date");
            entityTypeBuilder.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
        }
    }
}
