namespace Application.Courses.Dtos
{
    public record UpdateCourseDto(DateTime? StartDate, DateTime? EndDate, DateTime? RegistrationStartDate, DateTime? RegistrationEndDate, int? Capacity);
}
