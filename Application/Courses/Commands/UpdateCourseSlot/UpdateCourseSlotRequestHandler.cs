using Application.Courses.Dtos;
using Domain.Courses;
using Domain.Courses.Queries;
using Domain.Exceptions;
using MediatR;

namespace Application.Courses.Commands.CreateCourseMaster
{
    public class UpdateCourseSlotRequestHandler : IRequestHandler<UpdateCourseSlotRequest, int>
    {
        private readonly ICourseRepository courseRepository;
        private readonly ICourseCategoryRepository courseCategoryRepository;

        public UpdateCourseSlotRequestHandler(ICourseRepository courseRepository, ICourseCategoryRepository courseCategoryRepository)
        {
            this.courseRepository = courseRepository;
            this.courseCategoryRepository = courseCategoryRepository;
        }

        public Task<int> Handle(UpdateCourseSlotRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request?.UpdateCourseDto is UpdateCourseDto updateCourseDto)
            {
                var cs = courseRepository.GetCourseById(request.CourseMasterId, request.CourseId) ?? throw new DomainException($"Course Id {request.CourseId} not found", null, DomainErrorCode.NotFound);
                var startDate = cs.StartDate;
                var endDate = cs.EndDate;
                if (updateCourseDto.StartDate.HasValue)
                {
                    startDate = updateCourseDto.StartDate;
                    if (updateCourseDto.EndDate.HasValue)
                    {
                        endDate = updateCourseDto.EndDate;
                    }
                    cs.UpdateDuration(startDate.Value, endDate ?? startDate.Value.AddDays(1));
                }
                var regStartDate = cs.RegistrationStartDate;
                var regEndDate = cs.RegistrationEndDate;
                if (updateCourseDto.RegistrationStartDate.HasValue)
                {
                    regStartDate = updateCourseDto.RegistrationStartDate;
                    if (updateCourseDto.RegistrationEndDate.HasValue)
                    {
                        regEndDate = updateCourseDto.RegistrationEndDate;
                    }
                    cs.UpdateRegistrationWindow(regStartDate.Value, regEndDate ?? regStartDate.Value.AddDays(1));
                }
                if (updateCourseDto.Capacity.HasValue && updateCourseDto.Capacity > 0 && cs.Capacity != updateCourseDto.Capacity)
                {
                    cs.UpdateCapacity(updateCourseDto.Capacity.Value);
                }

                courseRepository.Update(cs);
                return Task.FromResult(1);
            }
            return Task.FromResult(-1);
        }
    }
}
