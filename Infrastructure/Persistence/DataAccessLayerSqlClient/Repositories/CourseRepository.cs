using DataAccessLayerSqlClient.Commands.Courses;
using Domain.Common;
using Domain.Courses;
using Domain.Courses.Queries;
using Microsoft.Data.SqlClient;

namespace DataAccessLayerSqlClient.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly string connectionString;

        public CourseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public int Add(CourseMaster coursemaster)
        {
            var cmd = new InsertCourseMasterCommand(coursemaster);
            return ErrorHandlerDecorator.TryCatch(() => cmd.ExecuteScalar(connectionString));
        }

        public int Add(Course course)
        {
            var cmd = new InsertCourseCommand(course);
            return ErrorHandlerDecorator.TryCatch(() => cmd.ExecuteScalar(connectionString));
        }

        public void Delete(Course course)
        {
            throw new NotImplementedException();
        }

        public CourseMaster? GetById(int id)
        {
            var cmd = new GetAllCourseMastersCommand(new CourseMasterFilter(id));
            return ErrorHandlerDecorator.TryCatch(cmd.ExecuteReader(connectionString).FirstOrDefault);
        }

        public IList<CourseMaster> GetAll(CourseMasterFilter? filter = null, SortingPaging? sortingPaging = null)
        {
            var cmd = new GetAllCourseMastersCommand(filter, sortingPaging);
            return ErrorHandlerDecorator.TryCatch(cmd.ExecuteReader(connectionString).ToList);
        }

      
        public void Update(CourseMaster coursemaster)
        {
            var cmd = new UpdateCourseMasterCommand(coursemaster);
            ErrorHandlerDecorator.TryCatch(() => cmd.ExecuteScalar(connectionString));
            
        }

        public void Update(Course course)
        {
            var cmd = new UpdateCourseCommand(course);
            ErrorHandlerDecorator.TryCatch(() => cmd.ExecuteScalar(connectionString));
        }

        public IList<Course> GetAll(int courseMasterId, CourseFilter? courseFilter = null, SortingPaging? sortingPaging = null)
        {
            var cmd = new GetAllCourseSlotsCommand(courseMasterId, courseFilter, sortingPaging);
            return ErrorHandlerDecorator.TryCatch(cmd.ExecuteReader(connectionString).ToList);
        }

        public Course? GetCourseById(int courseMasterId, int id)
        {
            var cmd = new GetAllCourseSlotsCommand(courseMasterId, new CourseFilter(id));
            return ErrorHandlerDecorator.TryCatch(cmd.ExecuteReader(connectionString).FirstOrDefault);
        }
    }
}
