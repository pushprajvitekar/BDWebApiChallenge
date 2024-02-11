using Application.Courses.Dtos;
using Domain.Courses;
using MediatR;

namespace Application.Courses.Queries.GetCourseMasters
{
    public class GetCourseMastersQueryHandler : IRequestHandler<GetCourseMastersQuery, IEnumerable<CourseMasterDto>>

    {
        private readonly ICourseRepository courseRepository;

        public GetCourseMastersQueryHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public Task<IEnumerable<CourseMasterDto>> Handle(GetCourseMastersQuery request, CancellationToken cancellationToken)
        {
            var res= courseRepository.GetAll(request.CourseMasterFilter, request.SortingPaging)
                                     .Select(c => new CourseMasterDto(c.Id, c.Name, c.Description, c.CourseCategory.Id, c.CourseCategory.Name));

            return Task.FromResult(res);
        }
    }
}
