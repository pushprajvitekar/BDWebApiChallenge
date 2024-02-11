using Application.Courses.Dtos;
using Domain.Common;
using Domain.Courses.Queries;
using MediatR;

namespace Application.Courses.Queries.GetCourseMasters
{
    public class GetCourseMastersQuery : IRequest<IEnumerable<CourseMasterDto>>
    {
        public GetCourseMastersQuery(CourseMasterFilter? courseMasterFilter, SortingPaging? sortingPaging)
        {
            CourseMasterFilter = courseMasterFilter;
            SortingPaging = sortingPaging;
        }

        public CourseMasterFilter? CourseMasterFilter { get; }
        public SortingPaging? SortingPaging { get; }
    }
}
