using Application.Courses.Dtos;
using Domain.Common;
using Domain.Courses.Queries;

namespace Application.Courses.Services
{
    public interface ICourseManagerService
    {
        public IEnumerable<CourseMasterDto> GetCourseMasters(CourseMasterFilter? filter, SortingPaging? sortingPaging = null);

        public int CreateCourseMaster(CreateCourseMasterDto courseMaster);

        public int UpdateCourseMaster(UpdateCourseMasterDto courseMaster);
        
        public int DeleteCourseMaster(int id);

        public IEnumerable<CourseDto> GetCourses(int courseMasterId, SortingPaging? sortingPaging = null);

        public int CreateCourse(int courseMasterId, CreateCourseDto courseSlotDto);
        public int UpdateCourse(int courseMasterId, UpdateCourseDto courseSlotDto);
        public int DeleteCourse(int courseMasterId, int id);
    }
}
