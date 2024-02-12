using Application.Courses.Dtos;
using Domain.Students;
using MediatR;

namespace Application.Students.Queries.GetAvailableCourses
{
    public class GetRegisteredCoursesRequestHandler : IRequestHandler<GetRegisteredCoursesRequest, IEnumerable<CourseRegistrationDto>>

    {
        private readonly IStudentCourseRepository studentCourseRepository;

        public GetRegisteredCoursesRequestHandler(IStudentCourseRepository studentCourseRepository)
        {
            this.studentCourseRepository = studentCourseRepository;
        }

        public Task<IEnumerable<CourseRegistrationDto>> Handle(GetRegisteredCoursesRequest request, CancellationToken cancellationToken)
        {
            var res = studentCourseRepository.GetAll(request.StudentId, request.CourseFilter, request.SortingPaging)
            .Select(c => new CourseRegistrationDto()
            {

                Id = c.Id,
                Name = c.Course?.CourseMaster?.Name ?? string.Empty,
                Description = c.Course?.CourseMaster?.Description ?? string.Empty,
                CategoryName = c.Course?.CourseMaster?.CourseCategory?.Name ?? "Unknown",
                StartDate = c.Course?.StartDate.GetValueOrDefault(),
                EndDate = c.Course?.EndDate.GetValueOrDefault(),
            });
            return Task.FromResult(res);
        }
    }
}
