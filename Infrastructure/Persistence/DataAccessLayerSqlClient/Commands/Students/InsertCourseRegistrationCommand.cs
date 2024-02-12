using DataAccessLayerSqlClient.Common;
using Domain.Courses;
using System.Data;

namespace DataAccessLayerSqlClient.Commands.Students
{
    internal class InsertCourseRegistrationCommand : SqlCommandBase<int>
    {
        public InsertCourseRegistrationCommand(CourseRegistration course) : base(course)
        {

        }
        protected override string CommandText => @"dbo.usp_CourseRegistration_Insert";

        protected override CommandType CommandType => CommandType.StoredProcedure;
        protected override void SetParameters(params object[] parameters)
        {
            if (parameters?.Length != 1) return;
            if (parameters[0] is not CourseRegistration courseRegistration) return;
            var dictionary = new Dictionary<string, object>()
            {
                 { nameof(CourseRegistration.CourseId),courseRegistration.Course.Id }
                ,{ nameof(CourseRegistration.StudentId),courseRegistration.StudentId}
                 ,{ nameof(CourseRegistration.RegistrationDate),courseRegistration.RegistrationDate }
                 ,{ nameof(CourseRegistration.RegisteredBy),courseRegistration.RegisteredBy }
            };

            Parameters = dictionary;
        }

        protected override int Transform(IDataReader reader)
        {
            return reader.GetInt32(0);
        }
    }
}
