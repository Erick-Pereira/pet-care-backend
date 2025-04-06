namespace Commons.Responses
{
    public class Response
    {
        public Response(string message, bool hasSuccess, Exception? ex = null)
        {
            Message = message;
            Success = hasSuccess;
            Exception = ex;
        }

        public Response()
        {
        }

        public string Message { get; set; }
        public bool Success { get; set; }
        public Exception? Exception { get; set; }

        public bool IsInfrastructureError
        { get { return Exception != null; } }
    }
}