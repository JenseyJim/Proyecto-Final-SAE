using System.Text;

namespace SistemaSAE.API.Utils
{
    public static class stringGenerator
    {
        public static string GenerarCodigoUnico()
        {
            const int numberLength = 5;
            var random = new Random();
            var numberPart = new StringBuilder();

            for (int i = 0; i < numberLength; i++)
            {
                numberPart.Append(random.Next(0, 10)); 
            }

            return $"TICKET-{numberPart}";
        }

    }
}
