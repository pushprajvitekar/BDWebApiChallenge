using Domain.Common;
using Domain.Courses;
using Domain.Students.Queries;

namespace Domain.Students
{
    public interface IStudentCourseRepository
    {
        int Add(StudentCourse studentCourse);
        void Delete(StudentCourse course);
        StudentCourse? GetById(int id);

        IList<Course> GetAll(StudentCourseFilter? filter = null, SortingPaging? sortingPaging = null);
    }
}
