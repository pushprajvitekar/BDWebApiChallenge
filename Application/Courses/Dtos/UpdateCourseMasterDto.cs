using Domain.Courses;

namespace Application.Courses.Dtos
{
    public record UpdateCourseMasterDto(string? Name, string? Description, int? CategoryId);
}
