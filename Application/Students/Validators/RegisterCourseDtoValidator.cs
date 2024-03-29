﻿using Application.Students.Dtos;
using FluentValidation;

namespace Application.Students.Validators
{
    public class RegisterCourseDtoValidator : AbstractValidator<RegisterCourseDto>
    {
        public RegisterCourseDtoValidator()
        {
            RuleFor(c => c.CourseId).NotEmpty().GreaterThan(0);
        }
    }
}
