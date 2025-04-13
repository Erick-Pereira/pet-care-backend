using Commons.Constants;
using Commons.Responses;
using System.Text.RegularExpressions;

namespace Commons.Validators
{
    public static class CommonValidators
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = tempCpf.Select((t, i) => int.Parse(t.ToString()) * multiplicador1[i]).Sum();
            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            string digito = resto.ToString();

            tempCpf += digito;
            soma = tempCpf.Select((t, i) => int.Parse(t.ToString()) * multiplicador2[i]).Sum();
            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        public static bool IsValidCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return false;

            cep = cep.Trim().Replace("-", "").Replace(".", "");
            return cep.Length == 8 && long.TryParse(cep, out _);
        }

        public static Response ValidateCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                return ResponseFactory.CreateFailedResponse("CEP não pode ser vazio.");

            cep = cep.Trim().Replace("-", "").Replace(".", "");

            if (cep.Length != 8)
                return ResponseFactory.CreateFailedResponse("CEP deve conter exatamente 8 caracteres.");

            if (!long.TryParse(cep, out _))
                return ResponseFactory.CreateFailedResponse("CEP deve conter apenas números.");

            return ResponseFactory.CreateSuccessResponse();
        }

        public static Response ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return ResponseFactory.CreateFailedResponse("O número de telefone não pode ser vazio.");

            phoneNumber = phoneNumber.Trim()
                                     .Replace("(", "")
                                     .Replace(")", "")
                                     .Replace("-", "")
                                     .Replace(" ", "")
                                     .Replace(".", "")
                                     .Replace("+", "");

            if (phoneNumber.Length < 8 || phoneNumber.Length > 11)
                return ResponseFactory.CreateFailedResponse("O número de telefone deve conter entre 8 e 11 dígitos.");

            if (!long.TryParse(phoneNumber, out _))
                return ResponseFactory.CreateFailedResponse("O número de telefone deve conter apenas números.");

            return ResponseFactory.CreateSuccessResponse();
        }

        public static Response ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return ResponseFactory.CreateFailedResponse(ValidationMessages.PasswordNotNull);

            if (password.Length < PasswordConstants.MinLength)
                return ResponseFactory.CreateFailedResponse(string.Format(ValidationMessages.PasswordMinLength, PasswordConstants.MinLength));

            if (password.Length > PasswordConstants.MaxLength)
                return ResponseFactory.CreateFailedResponse(string.Format(ValidationMessages.PasswordMaxLength, PasswordConstants.MaxLength));

            if (!Regex.IsMatch(password, "[A-Z]"))
                return ResponseFactory.CreateFailedResponse(ValidationMessages.PasswordMissingUppercase);

            if (!Regex.IsMatch(password, "[a-z]"))
                return ResponseFactory.CreateFailedResponse(ValidationMessages.PasswordMissingLowercase);

            if (!Regex.IsMatch(password, @"\d"))
                return ResponseFactory.CreateFailedResponse(ValidationMessages.PasswordMissingDigit);

            if (!Regex.IsMatch(password, @"[\W_]"))
                return ResponseFactory.CreateFailedResponse(ValidationMessages.PasswordMissingSymbol);

            return ResponseFactory.CreateSuccessResponse();
        }
    }
}