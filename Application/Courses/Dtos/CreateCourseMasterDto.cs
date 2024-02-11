namespace Application.Courses.Dtos
{
    public record CreateCourseMasterDto(string Name="a new course", string? Description="testDesc", int CategoryId=1);
}
