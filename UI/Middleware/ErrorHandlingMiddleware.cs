using Application.Exceptions;
using Application.Wrapper;
using System.Net;
using System.Text.Json;

namespace UI.Middleware
{
    public class ErrorHandlingMiddleware 
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
               var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new ApiResponse<string> { Message = ex.Message };
                switch (ex)
                {
                    case ApiException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound; 
                        break;
                    case ValidationErrorException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.ErrorList;
                        break;
                    default:
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);
               await response.WriteAsync(result);
            }
        }
    }
}
