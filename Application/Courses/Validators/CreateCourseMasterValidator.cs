using Application.Courses.Dtos;
using FluentValidation;

namespace Application.Courses.Validators
{
    public class CreateCourseMasterValidator : AbstractValidator<CreateCourseMasterDto>
    {
        public CreateCourseMasterValidator()
        {
            RuleFor(c=>c.Name).NotNull().MaximumLength(100);
            RuleFor(c => c.Description).MaximumLength(1000);
            RuleFor(c => c.CategoryId).GreaterThan(0);
        }
    }
}
