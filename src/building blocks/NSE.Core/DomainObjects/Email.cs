using System.Text.RegularExpressions;

namespace NSE.Core.DomainObjects
{
    public class Email
    {
        public Email() { }

        public Email(string endereco)
        {
            if (!Validar(endereco)) throw new DomainException("Email inválido");
            Endereco = endereco;
        }

        public const int EnderecoMaxLength = 254;
        public const int EnderecoMinLength = 5;
        public string Endereco { get; private set; }

        public static bool Validar(string email)
        {
            var regexEmail = new Regex(@"^ ([\w\.\-] +)@([\w\-] +)((\.(\w){ 2, 3 })+)$");
            return regexEmail.IsMatch(email);
        }
    }
}
