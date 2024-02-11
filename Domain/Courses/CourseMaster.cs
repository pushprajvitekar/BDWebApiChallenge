using System.Xml.Linq;

namespace Domain.Courses
{
    public class CourseMaster
    {
        public int Id { get; private set; }
        public string Name { get; protected set; }
        public string? Description { get; protected set; }
        public int CourseCategoryId { get; protected set; }
        public CourseCategory CourseCategory { get; protected set; }

        public IList<Course>? Courses { get; protected set; }
        public string CreatedBy { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public CourseMaster(string name, CourseCategory courseCategory,  string createdBy, string? description = null)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));   
            CreatedDate = DateTime.Now;
            CreatedBy = createdBy;
            Update(name, courseCategory, description);
        }
        public CourseMaster(int id, string name, CourseCategory courseCategory, string? description = null)// :this(name,  courseCategory,  string ? description = null)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
            Id = id;
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            Update(name, courseCategory, description);
        }

        public void Update(string name, CourseCategory courseCategory, string? description)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentNullException.ThrowIfNull(nameof(courseCategory));
            Name = name;
            Description = description;
            CourseCategory = courseCategory;
        }
        
    }
}
