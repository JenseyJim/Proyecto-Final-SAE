namespace SistemaSAE.API.Utils
{
    public class TimeHelper
    {
        public static int CalcularDuracionEnMinutos(DateTime horaInicio, DateTime horaFin)
        {
            return (int)(horaFin - horaInicio).TotalMinutes;
        }

        public static decimal CalcularCobro(int duracionEnMinutos, decimal tarifaPorHora, int minutosCortesia)
        {
            if (duracionEnMinutos <= minutosCortesia)
            {
                return 0; 
            }

            // con esto resto los minutos de cortesía
            int minutosRestantes = duracionEnMinutos - minutosCortesia;

            int horasCompletas = minutosRestantes / 60;

            // aca verifico si hay minutos adicionales para cobrar una fraccion extra
            bool tieneMinutosAdicionales = (minutosRestantes % 60 > 0);

            // si hay minutos adicionales despues de las horas completas, se cobra una hora extra
            int horasCobrables = horasCompletas + (tieneMinutosAdicionales ? 1 : 0);

            return horasCobrables * tarifaPorHora;
        }

        public static bool EstaDentroDelHorarioDeOperacion(DateTime horaActual, TimeSpan horaApertura, TimeSpan horaCierre)
        {
            var horaActualDelDia = horaActual.TimeOfDay;
            return horaActualDelDia >= horaApertura && horaActualDelDia <= horaCierre;
        }

    }
}
