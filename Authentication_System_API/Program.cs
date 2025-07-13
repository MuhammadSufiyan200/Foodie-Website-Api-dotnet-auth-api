
using Authentication_System_API.Data;
using Authentication_System_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Authentication_System_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(@"F:\Sufiyan.net\Keys\"))
            .SetApplicationName("Authentication_System_API");

            //Add for DbConnection
            builder.Services.AddDbContext<ApplictionDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Conn_String")));

            //Add for Jwt
            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
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
                    option.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                            return Task.CompletedTask;
                        }
                    };
                });
            //Add for Angular
            builder.Services.AddCors(options =>
            {
                //options.AddPolicy("AllowAngular",
                //policy => policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
                options.AddPolicy("AllowAngular", policy =>
                {
                    policy.WithOrigins(
                                "https://angular-auth-ai-app-with-api-v9l5.vercel.app", // <-- apka actual Vercel URL
                                "http://localhost:4200" // for local testing (optional)
                            )
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IInventoryService, InventoryService>();
            var app = builder.Build();
            app.UseStaticFiles();
            app.UseCors("AllowAngular");
            app.UseAuthentication();
            app.UseAuthorization();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}


            //app.UseHttpsRedirection(); // ya line hatani hn temparary...???
            app.MapControllers();

            app.Run();
        }
    }
}
