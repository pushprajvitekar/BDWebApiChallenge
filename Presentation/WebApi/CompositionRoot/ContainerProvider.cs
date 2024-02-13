using Application.Courses.Dtos;
using Application.Courses.Validators;
using Application.Students;
using CategoryProvider;
using DataAccessLayerSqlClient.Repositories;
using Domain.Courses;
using Domain.Students;
using FluentValidation;
using FluentValidation.AspNetCore;
using WebApi.Auth;
using WebApi.Auth.JwtService;

namespace WebApi.CompositionRoot
{
    public static class ContainerProvider
    {
        public static void RegisterTypes(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration["Data:DefaultConnection:ConnectionString"] ?? throw new ArgumentNullException(nameof(configuration), $"ConnectionString not defined");
            services.AddScoped<ICourseRepository, CourseRepository>(c => new CourseRepository(connString));
            services.AddScoped<IStudentCourseRepository, StudentCourseRepository>(c => new StudentCourseRepository(connString));
            var key = configuration["Data:Jwt:Key"] ?? throw new ArgumentNullException(nameof(configuration), "Jwt key not found");
            services.AddScoped<ITokenService, JwtTokenService>(c => new JwtTokenService(key));
            var categoryApiUrl = configuration["Data:CategoryApi:Url"] ?? throw new ArgumentNullException(nameof(configuration), "Category api url not defined");
            services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>(c => new CourseCategoryRepository(categoryApiUrl));

            
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CourseDto).Assembly);

            });

        }
    }

}
