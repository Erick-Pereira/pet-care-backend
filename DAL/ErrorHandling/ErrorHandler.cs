using Commons.Responses;

namespace BLL.ErrorHandling
{
    public static class ErrorHandler
    {
        public static Response Handle(Exception error)
        {
            if (error.Message.Contains("UQ_EMAIL"))
            {
                return ResponseFactory.CreateFailedResponse("Error when registering the user, because the email is already in use.");
            }
            return ResponseFactory.CreateFailedResponse("Error when registering the client, contact the admin.", error);
        }
    }
}