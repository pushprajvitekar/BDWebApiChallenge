namespace Application.Courses.Dtos
{
    public record CourseDto(int Id, string Name, string? Description, string CategoryName, DateTime? RegistrationStartDate, DateTime? RegistrationEndDate, DateTime? StartDate, DateTime? EndDate, int Capacity);
}
