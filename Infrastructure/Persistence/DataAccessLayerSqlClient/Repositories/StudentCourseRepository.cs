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
        public int Add(StudentCourse studentCourse)
        {
            throw new NotImplementedException();
        }

        public void Delete(StudentCourse course)
        {
            throw new NotImplementedException();
        }

        public IList<Course> GetAll(StudentCourseFilter? filter = null, SortingPaging? sortingPaging = null)
        {
            var cmd = new GetAvailableCoursesCommand(filter, sortingPaging);
            return ErrorHandlerDecorator.TryCatch(cmd.ExecuteReader(connectionString).ToList);
        }

        public StudentCourse? GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
