using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Platypus.Data.Context;
using Platypus.Model.Mapping;
using Platypus.Security;
using Platypus.Security.Interface;
using Platypus.Security.Settings;
using Platypus.Service.Data.TokenServices;
using Platypus.Service.Data.TokenServices.Interface;
using Platypus.Service.Data.UnitOfWork;
using Platypus.Service.Data.UnitOfWork.Interface;
using Platypus.Service.Data.UserServices;
using Platypus.Service.Data.UserServices.Interface;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace Platypus.Extensions {

    public static class IServiceCollectionExtensions {

        public static void ConfigureApp(this IServiceCollection services, IConfiguration configuration) {
            services.ConfigureMvc();
            services.AddControllers();
            services.AddRouting(option => option.LowercaseUrls = true);
            services.ConfigureEnvironmentOptions(configuration);
            services.ConfigureDataContext(configuration);
            services.ConfigureContextAccessors();
            services.ConfigureSecurityUtilities();
            services.ConfigureDataServices();
            services.ConfigureCompression();
            services.AddHttpClient();
            services.AddMemoryCache();
            services.AddResponseCaching();
            services.ConfigureAutoMapper();
            services.ConfigureJwt(configuration);
            services.ConfigureSwagger();
        }

        private static void ConfigureMvc(this IServiceCollection services) {
            services.AddMvc(option => {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                option.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        private static void ConfigureEnvironmentOptions(this IServiceCollection services, IConfiguration configuration) {
            IConfigurationSection securitySection = configuration.GetSection("Security");
            securitySection.Get<SecuritySettings>();
            services.Configure<SecuritySettings>(securitySection);

            services.AddOptions();
        }

        private static void ConfigureDataContext(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<DataContext>(cfg => {
                cfg.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
            });
        }

        private static void ConfigureContextAccessors(this IServiceCollection services) {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        private static void ConfigureSecurityUtilities(this IServiceCollection services) {
            services.AddTransient<IHashService, HashService>();
            services.AddTransient<ITokenService, TokenService>();
        }

        private static void ConfigureDataServices(this IServiceCollection services) {
            services.AddScoped<ITokenRefreshService, TokenRefreshService>();
            services.AddScoped<ITokenRequestService, TokenRequestService>();
            services.AddScoped<IUserCreationService, UserCreationService>();
            services.AddScoped<IUserUpdateService, UserUpdateService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void ConfigureCompression(this IServiceCollection services) {
            services.Configure<GzipCompressionProviderOptions>(option => option.Level = CompressionLevel.Fastest);
            services.AddResponseCompression();
        }

        private static void ConfigureAutoMapper(this IServiceCollection services) {
            services.AddAutoMapper(
                typeof(Startup),
                typeof(UserMappingProfile));
        }

        private static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration) {
            IConfigurationSection securitySection = configuration.GetSection("Security");
            SecuritySettings securitySettings = securitySection.Get<SecuritySettings>();
            byte[] key = Encoding.ASCII.GetBytes(securitySettings.Key);

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = true
                };

                options.Events = new JwtBearerEvents {
                    OnAuthenticationFailed = context => {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException)) {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }

        private static void ConfigureSwagger(this IServiceCollection services) {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Platypus API", Version = "v1" });

                c.EnableAnnotations();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                    },
                        new List<string>()
                    }
                });
            });
        }
    }
}