namespace Domain.Courses
{
    public class CourseCategory
    {
        public int Id { get; private set; }
        public string Name { get; protected set; }
        public CourseCategory(int id, string name)
        {
            if (id <= 0) throw new ArgumentNullException(nameof(id));
            Id = id;
            Name = name;
        }
    }
}
