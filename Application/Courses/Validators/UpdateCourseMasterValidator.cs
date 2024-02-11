using Application.Courses.Dtos;
using FluentValidation;

namespace Application.Courses.Validators
{
    public class UpdateCourseMasterValidator : AbstractValidator<UpdateCourseMasterDto>
    {
        public UpdateCourseMasterValidator()
        {
            When(c => !string.IsNullOrEmpty(c.Name), () => RuleFor(c => c.Name).MaximumLength(100));
            When(c => !string.IsNullOrEmpty(c.Description), () => RuleFor(c => c.Description).MaximumLength(1000));
            When(c => c.CategoryId!=null,()=> RuleFor(c => c.CategoryId).GreaterThan(0));
        }
    }
}
