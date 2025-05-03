namespace Commons.Constants
{
    public static class CommonConstants
    {
        public const int DefaultPageSize = 10;

        public const string EntityNotFoundMessage = "A entidade solicitada não foi encontrada.";

        public const string IdNotNullMessage = "O Id não pode ser nulo.";

        public const string InvalidFieldMessage = "O campo informado é inválido.";

        public const string InvalidPageNumberMessage = "O número da página deve ser maior que zero.";

        public const int MaxPageSize = 100;

        public const string OperationFailedMessage = "A operação falhou. Tente novamente mais tarde.";

        public const string OperationSuccessMessage = "A operação foi concluída com sucesso.";

        public const string RequiredFieldMessage = "O campo é obrigatório.";

        public static readonly string PageSizeExceededMessage = $"O tamanho da página não pode exceder {MaxPageSize} registros.";
    }
}