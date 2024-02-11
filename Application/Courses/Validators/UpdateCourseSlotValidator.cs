using Application.Courses.Dtos;
using FluentValidation;

namespace Application.Courses.Validators
{
    public class UpdateCourseSlotValidator : AbstractValidator<UpdateCourseDto>
    {
        public UpdateCourseSlotValidator()
        {
            When(c => c.StartDate != null,
                () =>
                {
                    RuleFor(c => c.StartDate).LessThan(c => c.EndDate).When(c => c.EndDate != null);
                    RuleFor(c => c.StartDate).GreaterThan(c => c.RegistrationEndDate).When(c => c.RegistrationEndDate != null);
                });
            When(c => c.EndDate != null, () => 
            {
                RuleFor(c => c.EndDate).GreaterThan(c => c.StartDate).When(c => c.StartDate != null);
                RuleFor(c => c.EndDate).GreaterThan(c => c.RegistrationStartDate).When(c => c.RegistrationStartDate != null);
                RuleFor(c => c.EndDate).GreaterThan(c => c.RegistrationEndDate).When(c => c.RegistrationEndDate != null);
                
            });

            When(c => c.RegistrationStartDate != null,
                () =>
                {
                    RuleFor(c => c.RegistrationStartDate).LessThan(c => c.RegistrationEndDate).When(c => c.RegistrationEndDate != null);
                    RuleFor(c => c.RegistrationStartDate).LessThan(c => c.StartDate).When(c => c.StartDate != null);
                    RuleFor(c => c.RegistrationStartDate).LessThan(c => c.EndDate).When(c => c.EndDate != null);
                });

            When(c => c.RegistrationEndDate != null,
    () =>
    {
        RuleFor(c => c.RegistrationEndDate).GreaterThan(c => c.RegistrationStartDate).When(c => c.RegistrationStartDate != null);
        RuleFor(c => c.RegistrationEndDate).LessThan(c => c.EndDate).When(c => c.EndDate != null);
        RuleFor(c => c.RegistrationEndDate).LessThan(c => c.StartDate).When(c => c.StartDate != null);
    });
            When(c => c.Capacity != null, () => RuleFor(c => c.Capacity).GreaterThan(0));
        }
    }
}
