namespace StudentManagement.Api.Common
{
    public class ApiErrorResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "An error occurred";
        public IEnumerable<string>? Errors { get; set; }
        public string? TraceId { get; set; }
    }
}
