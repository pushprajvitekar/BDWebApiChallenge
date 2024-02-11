using DataAccessLayerSqlClient.Common;
using Domain.Courses;
using System.Data;

namespace DataAccessLayerSqlClient.Commands.Courses
{
    internal class UpdateCourseCommand : SqlCommandBase<int>
    {
        public UpdateCourseCommand(Course course) : base(course)
        {

        }
        protected override string CommandText => @"dbo.usp_Course_Update";

        protected override CommandType CommandType => CommandType.StoredProcedure;
        protected override void SetParameters(params object[] parameters)
        {
            if (parameters?.Length != 1) return;
            if (parameters[0] is not Course course) return;
            var dictionary = new Dictionary<string, object>()
            {
                  { nameof(Course.Id),course.Id }
                 ,{ nameof(Course.CourseMasterId),course.CourseMasterId }
                 ,{ nameof(Course.Capacity),course.Capacity}
            };
            dictionary[nameof(Course.StartDate)] = course.StartDate.GetValueOrDefault();
            dictionary[nameof(Course.EndDate)] = course.EndDate.GetValueOrDefault();
            dictionary[nameof(Course.RegistrationStartDate)] = course.RegistrationStartDate.GetValueOrDefault();
            dictionary[nameof(Course.RegistrationEndDate)] = course.RegistrationEndDate.GetValueOrDefault();
            Parameters = dictionary;
        }

        protected override int Transform(IDataReader reader)
        {
            return reader.GetInt32(0);
        }
    }
}
