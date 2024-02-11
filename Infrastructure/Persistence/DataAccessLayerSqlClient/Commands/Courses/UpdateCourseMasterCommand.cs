using DataAccessLayerSqlClient.Common;
using Domain.Courses;
using System.Data;

namespace DataAccessLayerSqlClient.Commands.Courses
{
    internal class UpdateCourseMasterCommand : SqlCommandBase<int>
    {

        public UpdateCourseMasterCommand(CourseMaster courseMaster) : base(courseMaster) { }
        protected override string CommandText => @"dbo.usp_CourseMaster_Update";
        protected override CommandType CommandType => CommandType.StoredProcedure;
        protected override void SetParameters(params object[] parameters)
        {
            if (parameters?.Length != 1) return;
            var courseMaster = parameters[0] as CourseMaster;
            if (courseMaster == null) return;
            var dictionary = new Dictionary<string, object>()
            {
                    { nameof(CourseMaster.Id),courseMaster.Id }
                 ,{ nameof(CourseMaster.Name),courseMaster.Name }
             
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
            return reader.GetInt32(0);
        }
    }
}
