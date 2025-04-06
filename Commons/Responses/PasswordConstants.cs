namespace Commons.Responses
{
    public class PasswordConstants
    {
        public readonly int MaxLength;
        public readonly int MinLength;

        public readonly string MaxLengthMessage;
        public readonly string MinLengthMessage;
        public readonly string NotNullMessage;
        public readonly string InvalidPasswordMessage;
        public readonly string ConfirmPasswordMessage;

        public PasswordConstants()
        {
            MaxLength = 15;
            MinLength = 6;
            MaxLengthMessage = $"Senha não pode conter mais de {MaxLength} caracteres.";
            MinLengthMessage = $"Senha deve conter pelo menos {MinLength} caracteres.";
            NotNullMessage = "Senha deve ser informada.";
            InvalidPasswordMessage = "Pelo menos um caractere minusculo.\r\n" +
                                     "Pelo menos um caractere maiusculo.\r\n" +
                                     "Pelo menos um dígito.\r\n" +
                                     "Pelo menos um símbolo.";
            ConfirmPasswordMessage = "As senhas devem corresponder.";
        }
    }
}