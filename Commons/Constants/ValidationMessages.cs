namespace Commons.Constants
{
    public static class ValidationMessages
    {
        // Mensagens de validação para User
        public const string FullNameRequired = "O nome completo deve ser informado.";

        public const string FullNameMinLength = "O nome completo deve conter pelo menos {0} caracteres.";
        public const string FullNameMaxLength = "O nome completo não pode conter mais de {0} caracteres.";

        public const string EmailRequired = "O email deve ser informado.";
        public const string EmailMinLength = "O email deve conter pelo menos {0} caracteres.";
        public const string EmailMaxLength = "O email não pode conter mais de {0} caracteres.";
        public const string EmailInvalid = "O email informado é inválido.";

        // Mensagens de validação para senhas
        public const string PasswordNotNull = "A senha deve ser informada.";

        public const string PasswordMinLength = "A senha deve conter pelo menos {0} caracteres.";
        public const string PasswordMaxLength = "A senha não pode conter mais de {0} caracteres.";
        public const string PasswordMissingUppercase = "A senha deve conter pelo menos um caractere maiúsculo.";
        public const string PasswordMissingLowercase = "A senha deve conter pelo menos um caractere minúsculo.";
        public const string PasswordMissingDigit = "A senha deve conter pelo menos um dígito.";
        public const string PasswordMissingSymbol = "A senha deve conter pelo menos um símbolo.";

        public const string CepInvalid = "CEP inválido.";
        public const string PhoneNumberInvalid = "Número de telefone inválido.";
    }
}