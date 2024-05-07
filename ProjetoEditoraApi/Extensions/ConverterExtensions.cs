namespace ProjetoEditoraApi.Extensions
{
    public class ConverterExtensions
    {
        public static string ApenasNumeros(string valor)
        {
            if (valor == null) return string.Empty;
            var onlyNumber = "";
            foreach (var s in valor)
            {
                if (char.IsDigit(s))
                {
                    onlyNumber += s;
                }
            }
            return onlyNumber.Trim();
        }

        public static decimal RealParaDolar(decimal real, decimal ptax)
        {
            var dolar = real * ptax;
            return dolar;
        }

        public static decimal DolarParaReal(decimal dolar, decimal ptax)
        {
            var real = dolar / ptax; ;
            return real;
        }
    }
}
