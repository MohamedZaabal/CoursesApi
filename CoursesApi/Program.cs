
using System.Text;
using CoursesApi.Authorization;
using CoursesApi.Data;
using CoursesApi.Data_;
using CoursesApi.Filters;
using CoursesApi.Middlewares;
using CoursesApi.Models;
using CoursesApi.Services.Implement;
using CoursesApi.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CoursesApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>//انا فعلت عليه فلتر عشا ال Exeption
            {
                options.Filters.Add<GlobalExceptionFilter>();
                options.Filters.Add<PermissionBasedAuthorizationFilter>();
            });

            #region identity

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();


            #endregion

            #region Connection String

            builder.Services.AddDbContext<ApplicationDbContext>(opt=>
                                        opt.UseSqlServer(builder.Configuration
                                        .GetConnectionString("DefaultConnection")));
            #endregion

            #region Swagger 
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #endregion

            #region Dependency injection 

            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<ILessonService, LessonService>();
            #endregion

            #region ActionFilter
            builder.Services.AddScoped<ActionLoggingFilter>();
            builder.Services.AddScoped<GlobalExceptionFilter>();
          

            #endregion

            #region authenticatin + JWT

            var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
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
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            #endregion

            var app = builder.Build();

            // Seed Roles & Admin User
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await SeedData.SeedRoles(roleManager);
                await SeedData.SeedAdmin(userManager);
                await SeedData.SeedPermissions(context);
                await SeedData.SeedRolePermissions(context);
            }




            #region Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            #endregion

            app.UseHttpsRedirection();

            #region midleware

            app.UseMiddleware<CoursesApi.Middlewares.RequestTimingMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}
