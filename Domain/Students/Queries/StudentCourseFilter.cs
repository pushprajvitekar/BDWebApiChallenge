namespace Domain.Students.Queries
{
    public class StudentCourseFilter
    {
        public int? CourseId { get; set; }
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
