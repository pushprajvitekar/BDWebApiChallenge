using DataAccessLayerSqlClient.Common;
using Domain.Common;
using Domain.Courses;
using Domain.Courses.Queries;
using System.Data;
using System.Text;

namespace DataAccessLayerSqlClient.Commands.Courses
{
    internal class GetAllCourseSlotsCommand : GetPagedResultsCommand<Course>
    {
        private readonly int courseMasterId;
        private readonly CourseFilter? filter;

        public GetAllCourseSlotsCommand(int courseMasterId, CourseFilter? courseFilter = null, SortingPaging? sortingPaging = null) : base(sortingPaging) 
        {
            this.courseMasterId = courseMasterId;
            this.filter = courseFilter;
            SetParameters();
        }


        protected override string SelectClause
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine(@"SELECT [Id]
                              ,[CourseMasterId]
                              ,[Name]
                              ,[Description]
                              ,[CourseCategoryId]
                              ,[CourseCategoryName]
                              ,[RegistrationStartDate]
                              ,[RegistrationEndDate]
                              ,[StartDate]
                              ,[EndDate]
                              ,[Capacity]");
                sb.AppendLine("FROM[dbo].[vw_Course]");
                return sb.ToString();
            }
        }
        protected override string GetWhereClause()
        {
            if (filter == null) return string.Empty;
            var sb = new StringBuilder();
            if (filter.Id != null && filter.Id > 0)
            {
                sb.AppendLine($"Id = @{nameof(filter.Id)}");
                return $"WHERE {sb}";
            }
            sb.AppendLine($"CourseMasterId = @CourseMasterId");
            if (filter.CourseDuration != null)
            {
                if (filter.CourseDuration.StartDate != null)
                {
                    if (sb.Length > 0) { sb.AppendLine("AND"); }
                    sb.AppendLine($"StartDate = @{nameof(filter.CourseDuration.StartDate)}");
                }
                if (filter.CourseDuration.EndDate != null)
                {
                    if (sb.Length > 0) { sb.AppendLine("AND"); }
                    sb.AppendLine($"EndDate = @{nameof(filter.CourseDuration.EndDate)}");
                }
            }
            if (filter.RegistrationWindow != null)
            {
                if (filter.RegistrationWindow.StartDate != null)
                {
                    if (sb.Length > 0) { sb.AppendLine("AND"); }
                    sb.AppendLine($"RegistrationStartDate = @{nameof(filter.RegistrationWindow.StartDate)}");
                }
                if (filter.RegistrationWindow.EndDate != null)
                {
                    if (sb.Length > 0) { sb.AppendLine("AND"); }
                    sb.AppendLine($"RegistrationEndDate = @{nameof(filter.RegistrationWindow.EndDate)}");
                }
            }
            

            if (sb.Length > 0) { return $"WHERE {sb}"; } else return string.Empty;
        }
        protected override IEnumerable<KeyValuePair<string, object>> GetFilterParameters()
        {

            var dictionary = new Dictionary<string, object>();
            if (filter != null)
            {
                if (filter.Id != null && filter.Id > 0)
                {
                    dictionary.Add($"@{nameof(filter.Id)}", filter.Id);
                    return dictionary.ToList();
                }
                dictionary.Add($"@CourseMasterId", courseMasterId);
                if (filter.CourseDuration != null)
                {
                    if (filter.CourseDuration.StartDate != null)
                    {
                        dictionary.Add($"@{nameof(filter.CourseDuration.StartDate)}",filter.CourseDuration.StartDate);
                    }
                    if (filter.CourseDuration.EndDate != null)
                    {
                        dictionary.Add($"@{nameof(filter.CourseDuration.EndDate)}",filter.CourseDuration.EndDate);
                    }
                }
                if (filter.RegistrationWindow != null)
                {
                    if (filter.RegistrationWindow.StartDate != null)
                    {
                        dictionary.Add($"@{nameof(filter.RegistrationWindow.StartDate)}", filter.RegistrationWindow.StartDate);
                    }
                    if (filter.RegistrationWindow.EndDate != null)
                    {
                        dictionary.Add($"@{nameof(filter.RegistrationWindow.EndDate)}", filter.RegistrationWindow.EndDate);
                    }
                }
            }

            return dictionary.ToList();

        }
        protected override Course Transform(IDataReader reader)
        {
            var courseId = Convert.ToInt32(reader[nameof(Course.Id)]);
            var courseMasterId = Convert.ToInt32(reader["CourseMasterId"]);
            var courseName = Convert.ToString(reader["Name"]) ?? Constants.Unknown;
            var courseDesc = Convert.ToString(reader["Description"]);
            var categoryName = Convert.ToString(reader["CourseCategoryName"]);
            var categoryId = Convert.ToInt32(reader["CourseCategoryId"]);
            var category = new CourseCategory(categoryId, categoryName ?? Constants.Unknown);
            var courseMaster = new CourseMaster(courseMasterId, courseName, courseCategory: category, description: courseDesc);
            var startDate = Convert.ToDateTime(reader["StartDate"]);
            var endDate = Convert.ToDateTime(reader["EndDate"]);
            var regstartDate = Convert.ToDateTime(reader["RegistrationStartDate"]);
            var regendDate = Convert.ToDateTime(reader["RegistrationEndDate"]);
            var capacity = Convert.ToInt32(reader["Capacity"]);
            return new Course(id: courseId, courseMaster: courseMaster,regstartDate,regendDate, startDate, endDate, capacity);
        }
    }
}
