using Application.Courses.Dtos;
using Domain.Courses;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Application.Auth;

namespace Application.Courses.Commands.CreateCourseMaster
{
    public class CreateCourseSlotRequestHandler : IRequestHandler<CreateCourseSlotRequest, int>
    {
        private readonly ICourseRepository courseRepository;
        private readonly IHttpContextAccessor httpContext;

        public CreateCourseSlotRequestHandler(ICourseRepository courseRepository,
                                              IHttpContextAccessor httpContext)
        {
            this.courseRepository = courseRepository;
            this.httpContext = httpContext;
        }

        public Task<int> Handle(CreateCourseSlotRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request?.CreateCourseDto is CreateCourseDto createCourse)
            {
                //check name and category unique
                var cm = courseRepository.GetById(request.CourseMasterId) ?? throw new DomainException($"CourseMaster Id{request.CourseMasterId} not found", null, errorCode: DomainErrorCode.NotFound);
                string createdBy = httpContext.GetUser();
                var courseSlot = new Course(cm.Id, createdBy);
                courseSlot.UpdateRegistrationWindow(createCourse.RegistrationStartDate, createCourse.RegistrationEndDate);
                courseSlot.UpdateDuration(createCourse.StartDate, createCourse.EndDate);
                courseSlot.UpdateCapacity(createCourse.Capacity);
                var res = courseRepository.Add(courseSlot);
                return Task.FromResult(res);
            }
            return Task.FromResult(-1);
        }

    }
}
