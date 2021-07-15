using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo;

namespace Persistencia.ConfiModelo
{
    public class SexoConfig
    {
        public SexoConfig(EntityTypeBuilder<Sexo> entityTypeBuilder)
        {
            entityTypeBuilder.Property(p => p.NombreSexo).IsRequired().HasMaxLength(15);
        }
    }
}
