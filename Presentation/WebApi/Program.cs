using WebApi.CompositionRoot;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using FluentValidation.AspNetCore;
using Application.Courses.Validators;
using FluentValidation;
using WebApi.Filters;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApi.Auth;
namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //configure logging
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddNLog();
            });
            builder.Services.AddAuthorization(options =>
                 options.AddPolicy(PolicyName.SameStudentPolicy, policy => policy.Requirements.Add(new SameStudentAuthorizationRequirement())));
            builder.Services.AddSingleton<IAuthorizationHandler, SameStudentAuthorizationHandler>();
            builder.Services.AddAuthentication()
            .AddJwtBearer(o =>
                            {
                                var key = Encoding.UTF8.GetBytes(builder.Configuration["Data:Jwt:Key"] ?? throw new ArgumentNullException(nameof(args), $"Jwt Key not defined"));
                                o.SaveToken = true;
                                o.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = builder.Configuration["Data:Jwt:Issuer"],
                                    ValidAudience = builder.Configuration["Data:Jwt:Audience"],
                                    IssuerSigningKey = new SymmetricSecurityKey(key)
                                };

                            });
            // Add services to the container.

            builder.Services.AddControllers(o => o.Filters.Add(typeof(ExceptionFilter)));
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateCourseMasterValidator>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter a valid JSON web token here",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });

         
            builder.Services.AddHttpContextAccessor();
            //  builder.Services.AddAntiforgery();


            builder.Services.RegisterTypes(builder.Configuration);



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
