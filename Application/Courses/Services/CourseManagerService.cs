using Application.Courses.Dtos;
using Domain.Common;
using Domain.Courses;
using Domain.Courses.Queries;

namespace Application.Courses.Services
{
    public class CourseManagerService //: ICourseManagerService
    {
        private readonly ICourseRepository courseRepository;
        public CourseManagerService(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        public int CreateCourseMaster(CreateCourseMasterDto createCourseMasterDto)
        {
            //check name and category unique
            var courseMaster = new CourseMaster(createCourseMasterDto.Name, new CourseCategory(createCourseMasterDto.CategoryId, ""), createCourseMasterDto.Description);
            return courseRepository.Add(courseMaster);
        }

        public int CreateCourse(int courseMasterId, CreateCourseDto createCourseDto)
        {
            var course = new Course(courseMasterId);
            return courseRepository.Add(course);
        }

        public IEnumerable<CourseMasterDto> GetCourseMasters(CourseMasterFilter? filter, SortingPaging? sortingPaging = null)
        {
            return courseRepository.GetAll(filter, sortingPaging).Select(c => new CourseMasterDto(c.Id, c.Name, c.Description, c.CourseCategory.Id, c.CourseCategory.Name));

        }

        //public IEnumerable<CourseDto> GetCourses(int courseMasterId, SortingPaging? sortingPaging = null)
        //{
        //    return courseRepository.GetAll(courseMasterId, sortingPaging).Select(c => new CourseDto(c.Id, c.CourseMaster?.Name ?? "Unknown", c.CourseMaster?.Description, c.CourseMaster?.CourseCategory?.Name ?? "Unknown", c.StartDate, c.EndDate, c.Capacity));
        //}

        public int UpdateCourseMaster(UpdateCourseMasterDto courseMaster)
        {
            throw new NotImplementedException();
        }

        public int UpdateCourse(int courseMasterId, UpdateCourseDto courseSlotDto)
        {
            throw new NotImplementedException();
        }

        public int DeleteCourse(int courseMasterId, int id)
        {
            throw new NotImplementedException();
        }

        public int DeleteCourseMaster(int id)
        {
            throw new NotImplementedException();
        }
    }
}
