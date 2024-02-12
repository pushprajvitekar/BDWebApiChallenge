using DataAccessLayerSqlClient.Common;
using Domain.Courses;
using System.Data;

namespace DataAccessLayerSqlClient.Commands.Students
{
    internal class DeleteCourseRegistrationCommand : SqlCommandBase<int>
    {
        public DeleteCourseRegistrationCommand(CourseRegistration course) : base(course)
        {

        }
        protected override string CommandText => @"DELETE FROM [dbo].[CourseRegistration] WHERE Id= @Id";

        protected override CommandType CommandType => CommandType.Text;
        protected override void SetParameters(params object[] parameters)
        {
            if (parameters?.Length != 1) return;
            if (parameters[0] is not CourseRegistration courseRegistration) return;
            var dictionary = new Dictionary<string, object>()
            {
                 { nameof(CourseRegistration.Id),courseRegistration.Id }
            };

            Parameters = dictionary;
        }

        protected override int Transform(IDataReader reader)
        {
            return reader.GetInt32(0);
        }
    }
}
