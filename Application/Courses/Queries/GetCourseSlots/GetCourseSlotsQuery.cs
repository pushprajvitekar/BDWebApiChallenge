using Application.Courses.Dtos;
using Domain.Common;
using Domain.Courses.Queries;
using MediatR;

namespace Application.Courses.Queries.GetCourseMasters
{
    public class GetCourseSlotsQuery : IRequest<IEnumerable<CourseDto>>
    {
        public GetCourseSlotsQuery(int courseMasterId, CourseFilter? courseFilter, SortingPaging? sortingPaging)
        {
            CourseMasterId = courseMasterId;
            CourseFilter = courseFilter;
            SortingPaging = sortingPaging;
        }

        public int CourseMasterId { get; }
        public CourseFilter? CourseFilter { get; }
        public SortingPaging? SortingPaging { get; }
    }
}
