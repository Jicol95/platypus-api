using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Platypus.Extensions {

    public static class IApplicationBuilderExtensions {

        private class ErrorResponseModel {
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app) {
            app.UseExceptionHandler(appError => {
                appError.Run(async context => {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    IExceptionHandlerFeature contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null) {
                        await context.Response.WriteAsync(new ErrorResponseModel {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}