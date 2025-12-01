using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Common;
using System.Net;
using System.Text.Json;

namespace StudentManagement.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next,ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "{Exception} in {Controller}.{Action} |\n Route : {Route} |",
                    ex.GetType().Name,
                    context.GetEndpoint()?.Metadata
                    .OfType<Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor>()
                    .FirstOrDefault().ControllerName,
                    context.GetEndpoint()?.Metadata
                    .OfType<Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor>()
                    .FirstOrDefault().ActionName,
                    context.Request.Path
                    );
                var (statuscode, message, errors) = MapExceptionToResponse(ex, context);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statuscode;

                var response = new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Error occurred",
                    Error = new ErrorDetails
                    {
                        ErrorCode = statuscode.ToString(),
                        ErrormEssage = ex.GetType().Name,
                    }
                };

                var jsonResponse = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonResponse);
            }
        }

        private static (HttpStatusCode statuscode, string message, IEnumerable<string>? Errors) MapExceptionToResponse(Exception ex, HttpContext context)
        {
            var errors = new List<string>();

            switch (ex)
            {
                case BadRequestException br:
                    return (HttpStatusCode.BadRequest, br.Message, null);
                case NotFoundException nf:
                    return (HttpStatusCode.BadRequest, nf.Message, null);
                case ConflictException ce:
                    return (HttpStatusCode.BadRequest, ce.Message, null);
                case DbUpdateException du:
                    return (HttpStatusCode.BadRequest, "Db update error", new[] { du.Message });
                default:
                    return (HttpStatusCode.InternalServerError, "An unexpected error occurred", null);
            }
        }
    }
}
