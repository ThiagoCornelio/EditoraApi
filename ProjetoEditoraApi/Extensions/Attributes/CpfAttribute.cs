using System.ComponentModel.DataAnnotations;

namespace ProjetoEditoraApi.Extensions.Attributes
{
    public class CpfAttribute : ValidationAttribute
    {
        public const int TamanhoCpf = 11;
        public override bool IsValid(object value)
        {
            var cpfNumeros = ConverterExtensions.ApenasNumeros(value as string);

            if (!TamanhoValido(cpfNumeros))
                return false;

            return !TemDigitosRepetidos(cpfNumeros) && TemDigitosValidos(cpfNumeros);
        }
        private static bool TamanhoValido(string valor)
        {
            return TamanhoCpf == valor.Length;
        }

        private static bool TemDigitosRepetidos(string valor)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
            return invalidNumbers.Contains(valor);
        }

        private static bool TemDigitosValidos(string valor)
        {
            var number = valor.Substring(0, TamanhoCpf - 2);
            var digitoVerificador = new CheckDigitCpfCnpj(number)
                .ComMultiplicadoresDeAte(2, 11)
                .Substituindo("0", 10, 11);
            var firstDigit = digitoVerificador.CalculaDigito();
            digitoVerificador.AddDigito(firstDigit);
            var secondDigit = digitoVerificador.CalculaDigito();

            return string.Concat(firstDigit, secondDigit) == valor.Substring(TamanhoCpf - 2, 2);
        }
    }
}
