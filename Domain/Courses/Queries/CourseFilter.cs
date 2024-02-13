namespace Domain.Courses.Queries
{
    public class CourseFilter
    {
        public CourseFilter(int? id = null, DurationFilter? courseDuration = null, DurationFilter? registrationWindow = null)
        {
            Id = id;
            CourseDuration = courseDuration;
            RegistrationWindow = registrationWindow;
        }
        public CourseFilter()
        {
            
        }
        public int? Id { get; }
        public DurationFilter? CourseDuration { get; }
        public DurationFilter? RegistrationWindow { get; }
    }
}
