
using ElmahCore;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Common.StandardInfrastructure.ConfigExtensions
{
    public static class ConfigureServicesCommonExtension
    {
        public static IServiceCollection ServicesRegisterCommonConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            services.JwtConfig();
            services.SwaggerConfig(configuration);
            services.ElmahConfig();
            services.ServicesCommonConfig(configuration);
            services.AddHttpContextAccessor();
            services.LocalizationConfig();
            

            return services;
        }
        private static void JwtConfig(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "http://localhost:44364",
                    ValidAudience = "http://localhost:44364",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryDifficultSuperSecretKey"))
                };
            });
        }
        private static void SwaggerConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "EASTI", Version = "v1" });

                var documentName = configuration["SwaggerDocmentName"];
                var filePath = Path.Combine(AppContext.BaseDirectory, documentName);
                options.IncludeXmlComments(filePath);
                // Swagger 2.+ support
                //var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", Enumerable.Empty<string>() } };

                var security = new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            //Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                };

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(security);
                options.OperationFilter<AddLanguageHeaderParameter>();
            });
        }
        
        private static void ElmahConfig(this IServiceCollection services)
        {
            services.AddElmah<XmlFileErrorLog>(options =>
            {
                options.LogPath = "./log"; 
            });
        }
        private static void ServicesCommonConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ISendMail, SendMail>();
            services.AddScoped<IUploadConfig, UploadConfig>();
            services.AddTransient<ISessionStorage, SessionStorage>();
        }
        private static void LocalizationConfig(this IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("ar-EG"),
                        new CultureInfo("fr-FR")
                    };
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                {
                    var lang = context.Request.Headers["lang"].ToString();
                    return new ProviderCultureResult(string.IsNullOrWhiteSpace(lang) ? "en-US" : lang);
                }));
            });
        }

    }
    public class AddLanguageHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "lang",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema
                {
                    Type = "String"
                },
                Required = false
            });
        }
    }
}