using Application.Courses.Dtos;
using Application.Students.Dtos;
using Domain.Students;
using MediatR;

namespace Application.Students.Queries.GetAvailableCourses
{
    public class GetAvailableCoursesQueryHandler : IRequestHandler<GetAvailableCoursesQuery, IEnumerable<StudentCourseDto>>

    {
        private readonly IStudentCourseRepository studentCourseRepository;

        public GetAvailableCoursesQueryHandler(IStudentCourseRepository studentCourseRepository)
        {
            this.studentCourseRepository = studentCourseRepository;
        }

        public Task<IEnumerable<StudentCourseDto>> Handle(GetAvailableCoursesQuery request, CancellationToken cancellationToken)
        {
            var res = studentCourseRepository.GetAll(request.CourseFilter, request.SortingPaging)
            .Select(c => new StudentCourseDto()
            {

                Id = c.Id,
                Name = c.CourseMaster?.Name ?? string.Empty,
                Description = c.CourseMaster?.Description?? string.Empty,
                CategoryName = c.CourseMaster?.CourseCategory?.Name ?? "Unknown",
                RegistrationStartDate = c.RegistrationStartDate.GetValueOrDefault(),
                RegistrationEndDate = c.RegistrationEndDate.GetValueOrDefault(),
                StartDate = c.StartDate.GetValueOrDefault(),
                EndDate = c.EndDate.GetValueOrDefault(),
                Capacity = c.Capacity,
                AvailableSeats = c.RemainingPlaces
            });
            return Task.FromResult(res);
        }
    }
}
