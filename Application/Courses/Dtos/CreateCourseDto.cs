namespace Application.Courses.Dtos
{
    public record CreateCourseDto(DateTime RegistrationStartDate, DateTime RegistrationEndDate, DateTime StartDate, DateTime EndDate,  int Capacity=10);
}
