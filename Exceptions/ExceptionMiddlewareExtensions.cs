

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using my_book.Data.ViewModels;
using System.Net;

namespace my_book.Exceptions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureBuilderExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                //construct response
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var contextRequest = context.Features.Get<IHttpRequestFeature>();

                    if (contextFeature != null)   
                    {
                        await context.Response.WriteAsync(new ErrorVM()
                        {
                            Message = contextFeature.Error.Message,
                            Path = contextRequest.Path,
                            StatusCode = context.Response.StatusCode
                        }.ToString());
                    }

                });
            });
        }

        public static void ConfigureCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
