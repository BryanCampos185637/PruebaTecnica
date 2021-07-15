using System;

namespace LogicaAccesoDatos.LogicaEstudiante.Helper
{
    public class CalcularEdad
    {
        public static int Calular(DateTime fechaNacimiento)
        {
            DateTime fechaActual = DateTime.Now;
            int añoNacimiento = fechaNacimiento.Year;
            int Edad = 0;
            for(var i = añoNacimiento; i<= fechaActual.Year; i++)
            {
                Edad++;
            }
            return Edad;
        }
    }
}
