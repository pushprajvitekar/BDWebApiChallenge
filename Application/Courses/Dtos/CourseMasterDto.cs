namespace Application.Courses.Dtos
{
    public record CourseMasterDto(int Id ,string Name, string? Description, int CategoryId, string CategoryName);
}
