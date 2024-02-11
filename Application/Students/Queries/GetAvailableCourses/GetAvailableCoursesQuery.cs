using Application.Courses.Dtos;
using Application.Students.Dtos;
using Domain.Common;
using Domain.Students.Queries;
using MediatR;

namespace Application.Students.Queries.GetAvailableCourses
{
    public class GetAvailableCoursesQuery : IRequest<IEnumerable<StudentCourseDto>>
    {
        public GetAvailableCoursesQuery( StudentCourseFilter? courseFilter, SortingPaging? sortingPaging)
        {
            CourseFilter = courseFilter;
            SortingPaging = sortingPaging;
        }

        public int CourseMasterId { get; }
        public StudentCourseFilter? CourseFilter { get; }
        public SortingPaging? SortingPaging { get; }
    }
}
