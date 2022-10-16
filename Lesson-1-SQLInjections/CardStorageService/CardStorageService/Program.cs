using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using CardStorageService.DAL;
using CardStorageService.Services.impl;
using CardStorageService.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CardStorageService.Utils;

namespace CardStorageService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConnectionString connectionString = new ConnectionString();
            connectionString.DataSourse = "DESKTOP-T4GF27V\\SQLEXPRESS";
            connectionString.DatabaseName = "CardStorageService";
            connectionString.UserId = "CardStorageServiceUser";
            connectionString.Password = "1234567890";

            CacheConnectionString cacheConnectionString = new CacheConnectionString();
            cacheConnectionString.CacheConnections(connectionString);

            var builder = WebApplication.CreateBuilder(args);

            #region Logging service

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                logging.ResponseBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.RequestHeaders.Add("Authorization");
                logging.RequestHeaders.Add("X-Real-IP");
                logging.RequestHeaders.Add("X-Forwarded-For");
            });

            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true});

            #endregion

            #region Configure EF

            builder.Services.AddDbContext<CardStorageServiceDbContext>(options =>
            {
                var configStr = cacheConnectionString.GetConnectionsFromCache();
                options.UseSqlServer(builder.Configuration[$"{configStr.ToString}"]);
                //options.UseSqlServer(builder.Configuration["Settings:DatabaseOptions:ConnectionString"]);
            });
            #endregion

            #region Configure Repository

            builder.Services.AddScoped<IClientRepositoryService, ClientRepository>();
            builder.Services.AddScoped<ICardRepositoryService, CardRepository>();

            #endregion

            #region Configure Service
            builder.Services.AddSingleton<IAuthenticateService, AuthenticateService>();

            #endregion

            #region Configure JWT Tokens

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthenticateService.SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            #endregion

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CardStorageService", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme(Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();
                        
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpLogging();

            app.MapControllers();

            app.Run();
        }
    }
}