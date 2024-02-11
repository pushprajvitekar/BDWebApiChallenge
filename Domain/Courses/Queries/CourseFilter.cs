using Domain.Common;

namespace Domain.Courses.Queries
{
    public class CourseFilter
    {
        public CourseFilter(int? id = null, FilterDuration? courseDuration = null, FilterDuration? registrationWindow = null)
        {
            Id = id;
            CourseDuration = courseDuration;
            RegistrationWindow = registrationWindow;
        }
        public CourseFilter()
        {
            
        }
        public int? Id { get; }
        public FilterDuration? CourseDuration { get; }
        public FilterDuration? RegistrationWindow { get; }
    }
}
