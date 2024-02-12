namespace Domain.Courses
{
    public class CourseRegistration
    {
        public CourseRegistration(Course course,int studentId, string registeredBy)
        {
            Course = course;
            StudentId = studentId;
            RegisteredBy = registeredBy;
            RegistrationDate = DateTime.Now;
        }
        public int Id { get; protected set; }
        public int StudentId { get; protected set; }
        public int CourseId { get; protected set; }
        public Course Course { get; protected set; }
        public string RegisteredBy { get; protected set; }
        public DateTime RegistrationDate { get; protected set; }
        public CourseRegistration(int id,Course course, int studentId, string registeredBy, DateTime regDate)
        {
            Id = id;
            Course = course;
            StudentId = studentId;
            RegisteredBy = registeredBy;
            RegistrationDate = regDate;
        }
    }
}
