using Application.Students.Dtos;
using Domain.Common;
using Domain.Students.Queries;

namespace Application.Students
{
    public interface IStudentCourseService
    {
        public IEnumerable<StudentCourseDto> GetAvailableCourses(StudentCourseFilter? studentCourseFilter, SortingPaging? sortingPaging = null);
        public int RegisterCourse(RegisterCourseDto registerCourseDto);
        public int DeregisterCourse(DeregisterCourseDto registerCourseDto);

        public IEnumerable<StudentCourseDto> GetRegisteredCourses(int studentID, StudentCourseFilter? studentCourseFilter, SortingPaging? sortingPaging = null);

    }
}
