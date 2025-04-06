namespace Commons.Constants
{
    public static class CommonConstants
    {
        // Mensagens de validação genéricas
        public const string IdNotNullMessage = "O Id não pode ser nulo.";

        public const string EntityNotFoundMessage = "A entidade solicitada não foi encontrada.";
        public const string InvalidFieldMessage = "O campo informado é inválido.";
        public const string RequiredFieldMessage = "O campo é obrigatório.";
        public const string OperationFailedMessage = "A operação falhou. Tente novamente mais tarde.";
        public const string OperationSuccessMessage = "A operação foi concluída com sucesso.";

        // Limites genéricos
        public const int DefaultPageSize = 10;

        public const int MaxPageSize = 100;

        // Mensagens de paginação
        public static readonly string PageSizeExceededMessage = $"O tamanho da página não pode exceder {MaxPageSize} registros.";

        public const string InvalidPageNumberMessage = "O número da página deve ser maior que zero.";
    }
}