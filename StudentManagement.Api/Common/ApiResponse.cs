namespace StudentManagement.Api.Common
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public ErrorDetails Error { get; set; }
    }

    public class ErrorDetails
    {
        public string ErrorCode { get; set; }
        public string ErrormEssage { get; set; }
    }
}
