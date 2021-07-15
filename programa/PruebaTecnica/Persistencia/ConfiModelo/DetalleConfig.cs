
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo;

namespace Persistencia.ConfiModelo
{
    public class DetalleConfig
    {
        public DetalleConfig(EntityTypeBuilder<Detalle> entityTypeBuilder)
        {
            entityTypeBuilder.Property(p => p.FechaInicio).IsRequired().HasColumnType("date");
            entityTypeBuilder.Property(p => p.FechaFinalizo).IsRequired().HasColumnType("date");
            entityTypeBuilder.Property(p => p.Intitucion).IsRequired().HasMaxLength(100);
        }
    }
}
