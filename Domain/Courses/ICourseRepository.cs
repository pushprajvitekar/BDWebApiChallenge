using Domain.Common;
using Domain.Courses.Queries;

namespace Domain.Courses
{
    public interface ICourseRepository
    {
        int Add(CourseMaster coursemaster);

        void Update(CourseMaster coursemaster);
        CourseMaster? GetById(int id);
        IList<CourseMaster> GetAll(CourseMasterFilter? filter = null, SortingPaging? sortingPaging = null);
        int Add(Course course);
        void Update(Course course);
        void Delete(Course course);
        IList<Course> GetAll(int courseMasterId, CourseFilter? courseFilter = null, SortingPaging? sortingPaging = null);
        Course? GetCourseById(int courseMasterId, int id);
    }
}
