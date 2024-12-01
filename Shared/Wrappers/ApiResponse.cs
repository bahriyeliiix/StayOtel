namespace Shared.Wrappers
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
        }

        public ApiResponse(T data, string message = null, bool success = true, int statusCode = 200)
        {
            Data = data;
            Message = message;
            Success = success;
            StatusCode = statusCode;
        }

        public T Data { get; set; } 
        public string Message { get; set; } 
        public bool Success { get; set; } 
        public int StatusCode { get; set; }
    }
}
