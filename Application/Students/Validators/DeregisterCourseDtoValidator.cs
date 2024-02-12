using Application.Students.Dtos;
using FluentValidation;

namespace Application.Students.Validators
{
    public class DeregisterCourseDtoValidator : AbstractValidator<DeregisterCourseDto>
    {
        public DeregisterCourseDtoValidator()
        {
            RuleFor(c => c.CourseId).NotEmpty().GreaterThan(0);
        }
    }
}
