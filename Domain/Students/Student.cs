using Domain.Courses;

namespace Domain.Students
{
    public class Student
    {
        public int Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public IList<CourseRegistration>? Courses { get; protected set; }
        public Student(string firstname, string lastname, string email)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
        }
        public void RegisterCourse(CourseRegistration registration)
        {
            Courses ??= new List<CourseRegistration>();

            Courses.Add(registration);
        }

        public void DeregisterCourse(CourseRegistration registration)
        {
            Courses ??= new List<CourseRegistration>();

            var reg =Courses.FirstOrDefault( r=>r.Id == registration.Id);
            if (reg != null)
            {
                Courses.Remove(reg);
            }
        }
    }
}
