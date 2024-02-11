using Application.Courses.Dtos;
using Domain.Courses;
using Domain.Courses.Queries;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Courses.Commands.CreateCourseMaster
{
    public class CreateCourseSlotRequestHandler : IRequestHandler<CreateCourseSlotRequest, int>
    {
        private readonly ICourseRepository courseRepository;
        private readonly ICourseCategoryRepository courseCategoryRepository;
        private readonly IHttpContextAccessor httpContext;

        public CreateCourseSlotRequestHandler(ICourseRepository courseRepository, ICourseCategoryRepository courseCategoryRepository, IHttpContextAccessor httpContext)
        {
            this.courseRepository = courseRepository;
            this.courseCategoryRepository = courseCategoryRepository;
            this.httpContext = httpContext;
        }

        public Task<int> Handle(CreateCourseSlotRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request?.CreateCourseDto is CreateCourseDto createCourse)
            {
                //check name and category unique
                var cm = courseRepository.GetById(request.CourseMasterId) ?? throw new DomainException($"CourseMaster Id{request.CourseMasterId} not found", null, errorCode: DomainErrorCode.NotFound);
                var principal = httpContext.HttpContext.User;
                var createdBy = principal.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown";
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
