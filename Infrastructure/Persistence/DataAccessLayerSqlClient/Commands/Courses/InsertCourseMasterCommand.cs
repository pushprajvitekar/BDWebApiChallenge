using DataAccessLayerSqlClient.Common;
using Domain.Courses;
using System.Data;

namespace DataAccessLayerSqlClient.Commands.Courses
{
    internal class InsertCourseMasterCommand : SqlCommandBase<int>
    {

        public InsertCourseMasterCommand(CourseMaster courseMaster) : base(courseMaster) { }
        protected override string CommandText => @"dbo.usp_CourseMaster_Insert";
        protected override CommandType CommandType => CommandType.StoredProcedure;
        protected override void SetParameters(params object[] parameters)
        {
            if (parameters?.Length != 1) return;
            var courseMaster = parameters[0] as CourseMaster;
            if (courseMaster == null) return;
            var dictionary = new Dictionary<string, object>()
            {
                 { nameof(CourseMaster.Name),courseMaster.Name }
                 ,{ nameof(CourseMaster.CreatedBy),courseMaster.CreatedBy }
                 ,{ nameof(CourseMaster.CreatedDate),courseMaster.CreatedDate }
            };
            if (!string.IsNullOrEmpty(courseMaster.Description))
            {
                dictionary.Add(nameof(CourseMaster.Description), courseMaster.Description);
            }
            if (courseMaster.CourseCategory != null)
            {
                dictionary.Add("CourseCategoryId", courseMaster.CourseCategory.Id);
                if (!string.IsNullOrEmpty(courseMaster.CourseCategory.Name))
                {
                    dictionary.Add("CourseCategoryName", courseMaster.CourseCategory.Name);
                }
            }
            Parameters = dictionary;
        }

        protected override int Transform(IDataReader reader)
        {
            //return new CourseMaster($"{reader["Name"]}", new CourseCategory($"{reader["CourseCategoryName"]}"));
            return reader.GetInt32(0);
        }
    }
}
