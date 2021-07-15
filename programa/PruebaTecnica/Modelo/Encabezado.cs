using System;

namespace Modelo
{
    public class Encabezado
    {
        public int EncabezadoId { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public int SexoId { get; set; }
        public Sexo Sexo { get; set; }
    }
}
