using Domain.Courses;

namespace Domain.Students
{
    public class Student
    {
        public int Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public IList<StudentCourse>? Courses { get; protected set; }
        public Student(string firstname, string lastname, string email)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
        }
        public void RegisterCourse(StudentCourse studentCourse)
        {
            Courses ??= new List<StudentCourse>();

            Courses.Add(studentCourse);
        }
    }
}
