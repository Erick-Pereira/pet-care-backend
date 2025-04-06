using Commons.Responses;
using FluentValidation.Results;
using System.Text;

namespace Commons.Extensions
{
    public static class ResponseExtensions
    {
        public static Response ToResponse(this ValidationResult result)
        {
            if (result.IsValid)
            {
                return ResponseFactory.CreateInstance().CreateSuccessResponse("Entidade validada com sucesso.");
            }

            StringBuilder errorMessages = new StringBuilder();
            foreach (var error in result.Errors)
            {
                errorMessages.AppendLine(error.ErrorMessage);
            }

            return new Response
            {
                Success = false,
                Message = errorMessages.ToString()
            };
        }
    }
}