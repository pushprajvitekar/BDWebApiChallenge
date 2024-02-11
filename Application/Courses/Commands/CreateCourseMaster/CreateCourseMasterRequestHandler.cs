using Application.Courses.Dtos;
using Domain.Courses;
using Domain.Courses.Queries;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Courses.Commands.CreateCourseMaster
{
    public class CreateCourseMasterRequestHandler : IRequestHandler<CreateCourseMasterRequest, int>
    {
        private readonly ICourseRepository courseRepository;
        private readonly ICourseCategoryRepository courseCategoryRepository;
        private readonly IHttpContextAccessor httpContext;

        public CreateCourseMasterRequestHandler(ICourseRepository courseRepository, ICourseCategoryRepository courseCategoryRepository, IHttpContextAccessor httpContext)
        {
            this.courseRepository = courseRepository;
            this.courseCategoryRepository = courseCategoryRepository;
            this.httpContext = httpContext;
        }

        public Task<int> Handle(CreateCourseMasterRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request?.CreateCourseMasterDto is CreateCourseMasterDto createCourseMaster)
            {
                //check name and category unique
                var category = courseCategoryRepository.GetCategory(createCourseMaster.CategoryId) ?? throw new ArgumentOutOfRangeException(nameof(request), "Invalid Category");
                var cms = courseRepository.GetAll(new CourseMasterFilter(createCourseMaster.CategoryId, createCourseMaster.Name));
                if (cms.Any(c => c.Name.Equals(createCourseMaster.Name, StringComparison.CurrentCultureIgnoreCase))) { throw new DomainException("Course already exists with Name {} and CategoryId {}", null, DomainErrorCode.Exists); }
                var principal = httpContext.HttpContext.User;
                var createdBy = principal.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown";
                var courseMaster = new CourseMaster(createCourseMaster.Name, category, createdBy, createCourseMaster.Description);

                var res = courseRepository.Add(courseMaster);
                return Task.FromResult(res);
            }
            return Task.FromResult(-1);
        }
    }
}
