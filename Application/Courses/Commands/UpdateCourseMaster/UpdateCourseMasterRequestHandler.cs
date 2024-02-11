using Application.Courses.Dtos;
using Domain.Courses;
using Domain.Courses.Queries;
using Domain.Exceptions;
using MediatR;

namespace Application.Courses.Commands.CreateCourseMaster
{
    public class UpdateCourseMasterRequestHandler : IRequestHandler<UpdateCourseMasterRequest, int>
    {
        private readonly ICourseRepository courseRepository;
        private readonly ICourseCategoryRepository courseCategoryRepository;

        public UpdateCourseMasterRequestHandler(ICourseRepository courseRepository, ICourseCategoryRepository courseCategoryRepository)
        {
            this.courseRepository = courseRepository;
            this.courseCategoryRepository = courseCategoryRepository;
        }

        public Task<int> Handle(UpdateCourseMasterRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (request?.UpdateCourseMasterDto is UpdateCourseMasterDto updateCourseMaster)
            {
                var cms = courseRepository.GetById(request.CourseId) ?? throw new DomainException($"Course not found Id {request.CourseId}", null, DomainErrorCode.NotFound);
                CourseCategory category = cms.CourseCategory;
                bool update = false;
                if (updateCourseMaster.CategoryId.HasValue && category.Id != updateCourseMaster.CategoryId.Value)
                {
                    category = courseCategoryRepository.GetCategory(updateCourseMaster.CategoryId.Value) ?? throw new ArgumentOutOfRangeException(nameof(request), "Invalid Category");
                    update = true;
                }
                var name = cms.Name;
                if (!string.IsNullOrEmpty(updateCourseMaster.Name) && !cms.Name.Equals(updateCourseMaster.Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    var cmsls = courseRepository.GetAll(new CourseMasterFilter(category.Id, updateCourseMaster.Name));
                    if (cmsls.Any(c => c.Name.Equals(updateCourseMaster.Name, StringComparison.CurrentCultureIgnoreCase))) { throw new DomainException($"Course already exists with Name {updateCourseMaster.Name} and CategoryId {category.Id}", null, DomainErrorCode.Exists); }
                    name = updateCourseMaster.Name;
                    update = true;

                }
                var newDesc = cms.Description;
                if (!string.IsNullOrEmpty(updateCourseMaster.Description) && cms.Description != null && !cms.Description.Equals(updateCourseMaster.Description, StringComparison.CurrentCultureIgnoreCase))
                {
                    newDesc = updateCourseMaster.Description;
                    update = true;
                }
                if (update)
                {
                    cms.Update(name, category, newDesc);
                    courseRepository.Update(cms);
                    return Task.FromResult(1);
                }
            }
            return Task.FromResult(-1);
        }
    }
}
