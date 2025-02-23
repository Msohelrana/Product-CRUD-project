namespace WebApplication1.Controllers
{
    public class ApiResponse<T>
    {
        public bool Successs { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string?> Errors { get; set; }
        public int StatusCode { get; set; }
        public DateTime TimeStamp { get; set; }

        private ApiResponse(bool success,string messsage,T data,List<string?> errors,int statusCode)
        {
            Successs = success;
            Message = messsage;
            Data = data;
            Errors = errors;
            StatusCode = statusCode;
            TimeStamp = DateTime.UtcNow;
        }

        public static ApiResponse<T> SuccessResponse(T data,int statusCode,string message)
        {
            return new ApiResponse<T>(true, message, data, null, statusCode);
        }

        public static ApiResponse<T> ErrorResponse(T data, int statusCode, List<string?> errors)
        {
            return new ApiResponse<T>(true, null, data, errors, statusCode);
        }
    }
}
