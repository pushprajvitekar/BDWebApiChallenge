using Domain.Common;
using Domain.Courses;
using Domain.Students.Queries;

namespace Domain.Students
{
    public interface IStudentCourseRepository
    {
        int Add(CourseRegistration courseRegistration);
        int Delete(CourseRegistration courseRegistration);
        CourseRegistration? GetById(int studentId, int id);
        IList<CourseRegistration> GetAll(int studentId,RegisteredCourseFilter? filter = null, SortingPaging? sortingPaging = null);
        IList<Course> GetAll(AvailableCourseFilter? filter = null, SortingPaging? sortingPaging = null);
        Course? GetCourseById(int id);
    }
}
