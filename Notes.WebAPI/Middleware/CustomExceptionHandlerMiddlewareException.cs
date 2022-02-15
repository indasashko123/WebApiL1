using Microsoft.AspNetCore.Builder;

namespace Notes.WebAPI.Middleware
{
    public static class CustomExceptionHandlerMiddlewareException
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        } 
    }
}
