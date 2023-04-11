using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using SoftwareEngineering2.Models;

namespace SoftwareEngineering2.Middleware; 

public static class ExceptionHandlerMiddleware {
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, bool detailedErrors = false) {
        app.UseExceptionHandler(errorApp => {
            errorApp.Run(async context => {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null & detailedErrors) {
                    await context.Response.WriteAsync(new ErrorDetails() {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message
                    }.ToString());
                }
            });
        });
    }
}