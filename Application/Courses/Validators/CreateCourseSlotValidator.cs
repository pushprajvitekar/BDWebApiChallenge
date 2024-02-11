using Application.Courses.Dtos;
using FluentValidation;

namespace Application.Courses.Validators
{
    public class CreateCourseSlotValidator: AbstractValidator<CreateCourseDto>
    {
        public CreateCourseSlotValidator()
        {
            RuleFor(c => c.StartDate).NotNull().GreaterThan(c=>c.RegistrationEndDate);
            RuleFor(c => c.EndDate).NotNull().GreaterThan(c => c.StartDate);
            RuleFor(c => c.RegistrationStartDate).NotNull().LessThan(c=>c.StartDate);
            RuleFor(c => c.RegistrationEndDate).NotNull().GreaterThan(c=>c.RegistrationStartDate);
            RuleFor(c => c.Capacity).NotNull().GreaterThan(0);
        }
    }
}
