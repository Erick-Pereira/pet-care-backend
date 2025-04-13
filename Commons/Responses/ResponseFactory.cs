namespace Commons.Responses
{
    public class ResponseFactory
    {
        #region Singleton

        private static ResponseFactory? _factory;

        public static ResponseFactory CreateInstance()
        {
            return _factory ??= new ResponseFactory();
        }

        private ResponseFactory()
        { }

        #endregion Singleton

        #region General Responses

        // Success Responses
        public static Response CreateSuccessResponse()
        {
            return new Response("Operation performed successfully.", true);
        }

        public Response CreateSuccessResponse(string message)
        {
            return new Response
            {
                Success = true,
                Message = message
            };
        }

        // Failure Responses
        public static Response CreateFailedResponse(string message, Exception? ex = null)
        {
            return new Response(message, false, ex);
        }

        public static Response CreateFailedResponse(Exception? ex = null)
        {
            return new Response("Operation failed.", false, ex);
        }

        public Response CreateFailedResponseZeroRowsUpdatedOnDatabase()
        {
            return new Response
            {
                Success = false,
                Message = "No records have been changed for the record in question."
            };
        }

        public static Response CreateFailedResponseNotFoundId()
        {
            return new Response("Not found Id.", false);
        }

        public static Response CreateFailedPermissionResponse()
        {
            return new Response
            {
                Success = false,
                Message = "Permission not granted for the operation."
            };
        }

        #endregion General Responses

        #region Single Responses

        // Success Single Responses
        public static SingleResponse<T> CreateSuccessSingleResponse<T>(T item)
        {
            return new SingleResponse<T>("Data collected successfully", true, item);
        }

        public static SingleResponse<T> CreateSuccessSingleResponse<T>(string message)
        {
            return new SingleResponse<T>
            {
                Success = true,
                Message = message
            };
        }

        // Failure Single Responses
        public SingleResponse<T> CreateFailedSingleResponse<T>(string message, Exception? ex = null)
        {
            return new SingleResponse<T>
            {
                Success = false,
                Message = message,
                Exception = ex
            };
        }

        public SingleResponse<T> CreateFailedSingleResponse<T>(Exception? ex = null)
        {
            return new SingleResponse<T>
            {
                Success = false,
                Message = "Operation failed.",
                Exception = ex
            };
        }

        public SingleResponse<T> CreateFailedSingleResponseNotFoundItem<T>(Exception? ex = null)
        {
            return new SingleResponse<T>
            {
                Success = false,
                Message = "Item not found.",
                Exception = ex
            };
        }

        public SingleResponse<T> CreateItemResponse<T>(string message, bool success, T item)
        {
            return new SingleResponse<T>
            {
                Success = success,
                Message = message,
                Item = item
            };
        }

        #endregion Single Responses

        #region Data Responses

        // Success Data Responses
        public DataResponse<T> CreateSuccessDataResponse<T>(string message)
        {
            return new DataResponse<T>
            {
                Success = true,
                Message = message
            };
        }

        public DataResponse<T> CreateSuccessDataResponse<T>(List<T> data)
        {
            return new DataResponse<T>
            {
                Success = true,
                Message = "Operation performed successfully.",
                Data = data
            };
        }

        public DataResponse<T> CreateResponseBasedOnCollectionData<T>(List<T> data)
        {
            if (data == null || data.Count == 0)
            {
                return new DataResponse<T>
                {
                    Success = false,
                    Message = "Data not found.",
                    Data = new List<T>()
                };
            }

            return new DataResponse<T>("Data collected successfully.", true, data);
        }

        // Failure Data Responses
        public DataResponse<T> CreateFailedDataResponse<T>(string message, Exception? ex = null)
        {
            return new DataResponse<T>
            {
                Success = false,
                Message = message,
                Exception = ex
            };
        }

        public DataResponse<T> CreateFailedDataResponse<T>(Exception? ex = null)
        {
            return new DataResponse<T>
            {
                Success = false,
                Message = "Operation failed.",
                Exception = ex
            };
        }

        public DataResponse<T> CreateFailureDataResponse<T>(Exception ex)
        {
            return new DataResponse<T>
            {
                Success = false,
                Message = "Database error, contact the administrator.",
                Exception = ex
            };
        }

        public DataResponse<T> CreateFailureDataResponse<T>(string message, Exception ex)
        {
            return new DataResponse<T>
            {
                Success = false,
                Message = message,
                Exception = ex
            };
        }

        #endregion Data Responses
    }
}