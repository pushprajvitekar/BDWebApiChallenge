namespace Domain.Courses.Queries
{
    public class CourseMasterFilter
    {
        public int? CategoryId { get; }
        public string? Name { get; }
        public int? Id { get; }
        public CourseMasterFilter()
        {

        }
        public CourseMasterFilter(int id)
        {
            Id = id;
        }
        public CourseMasterFilter(int? categoryId = null, string? name = null, int? id = null)
        {
            CategoryId = categoryId;
            Name = name;
            Id = id;
        }

    };
}
