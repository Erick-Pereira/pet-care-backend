namespace Commons.Responses
{
    public class ResponseFactory
    {
        #region Singleton

        private static ResponseFactory _factory;

        public static ResponseFactory CreateInstance()
        {
            if (_factory == null)
            {
                _factory = new ResponseFactory();
            }
            return _factory;
        }

        private ResponseFactory()
        { }

        #endregion Singleton

        #region Responses

        //Success
        public Response CreateSuccessResponse()
        {
            return new Response("Operation performed successfully.", true);
        }

        public Response CreateSuccessResponse(string mensagem)
        {
            return new Response()
            {
                Success = true,
                Message = mensagem
            };
        }

        //Failed
        public Response CreateFailedResponse(string mensagem, Exception? ex = null)
        {
            return new Response(mensagem, false, ex);
        }

        public Response CreateFailedResponse(Exception? ex = null)
        {
            return new Response("Operation failed.", false, ex);
        }

        public Response CreateFailedResponseZeroRowsUpdatedOnDatabase()
        {
            return new Response()
            {
                Success = false,
                Message = "No records have been changed for the record in question."
            };
        }

        public static Response CreateFailedPermissionResponse()
        {
            return new Response()
            {
                Success = false,
                Message = "Permission not granted for the operation."
            };
        }

        public Response CreateFailedResponseNotFoundId()
        {
            return new Response("Not found Id.", false);
        }

        #endregion Responses

        #region SingleResponses

        //Success
        public SingleResponse<T> CreateSuccessSingleResponse<T>(T item)
        {
            return new SingleResponse<T>("Data collected successfully", true, item);
        }

        public static SingleResponse<T> CreateSuccessSingleResponse<T>(string message)
        {
            return new SingleResponse<T>()
            {
                Success = true,
                Message = message
            };
        }

        //Failed
        public SingleResponse<T> CreateFailedSingleResponse<T>(string mensagem, Exception? ex = null)
        {
            return new SingleResponse<T>()
            {
                Success = false,
                Message = mensagem,
                Exception = ex,
            };
        }

        public SingleResponse<T> CreateFailedSingleResponse<T>(Exception? ex = null)
        {
            return new SingleResponse<T>()
            {
                Success = false,
                Message = "Operation failed.",
                Exception = ex,
            };
        }

        public SingleResponse<T> CreateFailedSingleResponseNotFoundItem<T>(Exception? ex = null)
        {
            return new SingleResponse<T>()
            {
                Success = false,
                Message = "Item not found.",
                Exception = ex
            };
        }

        /*
                 public SingleResponseWToken<T> CreateSuccessSingleResponseWToken<T>(string token, T? item, Exception? ex = null)
        {
            return new SingleResponseWToken<T>("Data collected successfully", true, item, token, ex);
        }

          public SingleResponseWToken<T> CreateFailedSingleResponseWToken<T>(String message, Exception? ex = null)
        {
            return new SingleResponseWToken<T>()
            {
                HasSuccess = false,
                Message = message,
                Exception = ex,
            };
        }
        public SingleResponseWToken<T> CreateFailedSingleResponseWToken<T>(Exception? ex = null)
        {
            return new SingleResponseWToken<T>()
            {
                HasSuccess = false,
                Message = "Operation failed.",
                Exception = ex,
            };
        }*/

        #endregion SingleResponses

        #region DataResponses

        public DataResponse<T> CreateSuccessDataResponse<T>(string message)
        {
            return new DataResponse<T>()
            {
                Success = true,
                Message = message
            };
        }

        public DataResponse<T> CreateResponseBasedOnCollectionData<T>(List<T> data)
        {
            if (data == null || data.Count == 0)
            {
                return new DataResponse<T>()
                {
                    Data = new List<T>(),
                    Exception = null,
                    Success = false,
                    Message = "Data not found."
                };
            }
            return new DataResponse<T>("Data collected successfully.", true, data);
        }

        public DataResponse<T> CreateFailedDataResponse<T>(string message, Exception? ex = null)
        {
            return new DataResponse<T>()
            {
                Success = false,
                Message = message,
                Exception = ex
            };
        }

        public DataResponse<T> CreateFailedDataResponse<T>(Exception? ex = null)
        {
            return new DataResponse<T>()
            {
                Success = false,
                Message = "Operation failed.",
                Exception = ex
            };
        }

        #endregion DataResponses
    }
}