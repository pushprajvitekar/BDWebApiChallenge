using Application.Auth;
using Application.Courses.Dtos;
using Application.Students.Dtos;
using Domain.Courses;
using Domain.Exceptions;
using Domain.Students;
using Domain.Students.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Students.Commands.RegisterCourse
{
    public class RegisterCourseRequestHandler : IRequestHandler<RegisterCourseRequest, int>
    {
        private readonly IStudentCourseRepository studentCourseRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RegisterCourseRequestHandler(IStudentCourseRepository studentCourseRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.studentCourseRepository = studentCourseRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
        public Task<int> Handle(RegisterCourseRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request?.RegisterCourseDto is RegisterCourseDto registerCourse)
            {
                var course = studentCourseRepository.GetCourseById(registerCourse.CourseId) ?? throw new ArgumentException("Course not available");
                var courseReg = studentCourseRepository.GetAll(request.StudentId, new RegisteredCourseFilter { CourseId = registerCourse.CourseId }).FirstOrDefault();
                if (courseReg != null) { throw new DomainException("Course already registered", null, DomainErrorCode.Exists); }
                var res = studentCourseRepository.Add(new CourseRegistration(course, request.StudentId, httpContextAccessor.GetUser()));
                return Task.FromResult(res);
            }
            return Task.FromResult(-1);
        }
    }
}
