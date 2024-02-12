using Application.Students.Dtos;
using Domain.Exceptions;
using Domain.Students;
using Domain.Students.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Students.Commands.RegisterCourse
{
    public class DeregisterCourseRequestHandler : IRequestHandler<DeregisterCourseRequest, Unit>
    {
        private readonly IStudentCourseRepository studentCourseRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DeregisterCourseRequestHandler(IStudentCourseRepository studentCourseRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.studentCourseRepository = studentCourseRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
        public Task<Unit> Handle(DeregisterCourseRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request?.DeregisterCourseDto is DeregisterCourseDto deregisterCourse)
            {
                var courseReg = studentCourseRepository.GetAll(request.StudentId, new RegisteredCourseFilter { CourseId = deregisterCourse.CourseId }).FirstOrDefault();
                if (courseReg == null) { throw new DomainException("Course not registered", null, DomainErrorCode.NotFound); }
                studentCourseRepository.Delete(courseReg);
            }
            return Task.FromResult(new Unit());
        }
    }
}
