using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Platypus.Data.Context;
using Platypus.Extensions;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

namespace Platypus {

    public class Startup {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.ConfigureApp(configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            using (IServiceScope scope = app.ApplicationServices.CreateScope()) {
                DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();

                context.Database.Migrate();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Platypus API");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(DocExpansion.None);
                c.DisplayRequestDuration();
            });

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseResponseCompression();
            app.UseResponseCaching();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.Use(async (context, next) => {
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue() {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(60)
                    };
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };

                await next();
            });

            app.ConfigureExceptionHandler();
        }
    }
}