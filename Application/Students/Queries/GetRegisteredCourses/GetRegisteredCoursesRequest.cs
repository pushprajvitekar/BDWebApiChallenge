using Application.Courses.Dtos;
using Domain.Common;
using Domain.Students.Queries;
using MediatR;

namespace Application.Students.Queries.GetAvailableCourses
{
    public class GetRegisteredCoursesRequest : IRequest<IEnumerable<CourseRegistrationDto>>
    {
        public GetRegisteredCoursesRequest(int studentId, RegisteredCourseFilter? courseFilter, SortingPaging? sortingPaging)
        {
            StudentId = studentId;
            CourseFilter = courseFilter;
            SortingPaging = sortingPaging;
        }

        public int StudentId { get; }
        public RegisteredCourseFilter? CourseFilter { get; }
        public SortingPaging? SortingPaging { get; }
    }
}
