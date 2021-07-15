using System.ComponentModel.DataAnnotations.Schema;

namespace Modelo
{
    public class Estudiante
    {
        public int EstudianteId { get; set; }
        [ForeignKey("Encabezado")]
        public int EncabezadoId { get; set; }
        public Encabezado Encabezado { get; set; }
        [ForeignKey("Detalle")]
        public int DetalleId { get; set; }
        public Detalle Detalle { get; set; }
    }
}
