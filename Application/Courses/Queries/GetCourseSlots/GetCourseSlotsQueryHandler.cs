using Application.Courses.Dtos;
using Domain.Courses;
using MediatR;

namespace Application.Courses.Queries.GetCourseMasters
{
    public class GetCourseSlotsQueryHandler : IRequestHandler<GetCourseSlotsQuery, IEnumerable<CourseDto>>

    {
        private readonly ICourseRepository courseRepository;

        public GetCourseSlotsQueryHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public Task<IEnumerable<CourseDto>> Handle(GetCourseSlotsQuery request, CancellationToken cancellationToken)
        {
            var res= courseRepository.GetAll(request.CourseMasterId, request.CourseFilter, request.SortingPaging)
            .Select(c => new CourseDto(c.Id,
                                       c.CourseMaster?.Name ?? "Unknown",
                                       c.CourseMaster?.Description,
                                       c.CourseMaster?.CourseCategory?.Name ?? "Unknown",
                                       c.RegistrationStartDate,
                                       c.RegistrationEndDate,
                                       c.StartDate,
                                       c.EndDate,
                                       c.Capacity));
            return Task.FromResult(res);
        }
    }
}
