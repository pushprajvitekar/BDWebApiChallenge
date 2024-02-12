using DataAccessLayerSqlClient.Commands.Students;
using Domain.Common;
using Domain.Courses;
using Domain.Students;
using Domain.Students.Queries;

namespace DataAccessLayerSqlClient.Repositories
{
    public class StudentCourseRepository : IStudentCourseRepository
    {
        private readonly string connectionString;

        public StudentCourseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int Add(CourseRegistration courseRegistration)
        {
            var cmd = new InsertCourseRegistrationCommand(courseRegistration);
            return ErrorHandlerDecorator.TryCatch(() => cmd.ExecuteScalar(connectionString));
        }

        public int Delete(CourseRegistration courseRegistration)
        {
            var cmd = new DeleteCourseRegistrationCommand(courseRegistration);
            return ErrorHandlerDecorator.TryCatch(() => cmd.ExecuteNonQuery(connectionString));
        }

        public IList<Course> GetAll(AvailableCourseFilter? filter = null, SortingPaging? sortingPaging = null)
        {
            var cmd = new GetAvailableCoursesCommand(filter, sortingPaging);
            return ErrorHandlerDecorator.TryCatch(cmd.ExecuteReader(connectionString).ToList);
        }
        public Course? GetCourseById(int id)
        {
            var cmd = new GetAvailableCoursesCommand(new AvailableCourseFilter() { CourseId = id });
            return ErrorHandlerDecorator.TryCatch(cmd.ExecuteReader(connectionString).FirstOrDefault);
        }
        public IList<CourseRegistration> GetAll(int studentId, RegisteredCourseFilter? filter = null, SortingPaging? sortingPaging = null)
        {
            var cmd = new GetRegisteredCoursesCommand(studentId, filter, sortingPaging);
            return ErrorHandlerDecorator.TryCatch(cmd.ExecuteReader(connectionString).ToList);
        }

        public CourseRegistration? GetById(int studentId, int id)
        {
            var cmd = new GetRegisteredCoursesCommand(studentId, new RegisteredCourseFilter { Id = id });
            return ErrorHandlerDecorator.TryCatch(cmd.ExecuteReader(connectionString).FirstOrDefault);
        }


    }
}
