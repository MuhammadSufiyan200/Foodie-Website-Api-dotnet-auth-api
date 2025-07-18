using Authentication_System_API.Data;
using Authentication_System_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.IO;

namespace Authentication_System_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Data Protection
            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, "Keys")))
                .SetApplicationName("Authentication_System_API");

            // PostgreSQL Database Configuration
            builder.Services.AddDbContext<ApplictionDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Conn_String")));

            // JWT Authentication Configuration
            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero,
                        NameClaimType = "nameid",
                        RoleClaimType = "role"
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                            return Task.CompletedTask;
                        }
                    };
                });

            // CORS Policy for Angular App (Local + Vercel)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", policy =>
                {
                    policy.WithOrigins(
                        "https://angular-auth-ai-app-with-api-v9l5.vercel.app",
                        "http://localhost:4200"
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            // Auth Services
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            // MVC Controllers & Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Authentication API", Version = "v1" });
                // Optional: JWT Auth in Swagger
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                };
                c.AddSecurityDefinition("Bearer", securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Custom Services
            builder.Services.AddScoped<IInventoryService, InventoryService>();

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseCors("AllowAngular");
            app.UseAuthentication();
            app.UseAuthorization();

            // Swagger UI always ON
            app.UseSwagger();
            app.UseSwaggerUI();

            // Optionally disable HTTPS Redirection for development
            // app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
