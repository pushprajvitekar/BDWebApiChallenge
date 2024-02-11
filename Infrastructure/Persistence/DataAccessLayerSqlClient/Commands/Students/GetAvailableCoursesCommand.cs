using DataAccessLayerSqlClient.Common;
using Domain.Common;
using Domain.Courses;
using Domain.Students.Queries;
using System.Data;
using System.Text;

namespace DataAccessLayerSqlClient.Commands.Students
{
    internal class GetAvailableCoursesCommand : GetPagedResultsCommand<Course>
    {
        private readonly StudentCourseFilter? filter;

        public GetAvailableCoursesCommand(StudentCourseFilter? studentCourseFilter, SortingPaging? sortingPaging) : base(sortingPaging)
        {
            this.filter = studentCourseFilter;
            SetParameters();
        }

        protected override string SelectClause
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine(@"SELECT 
                               [Id]
                              ,[CourseMasterId] 
                              ,[Name]
                              ,[Description]
                              ,[CourseCategoryId]
                              ,[CourseCategoryName]
                              ,[RegistrationStartDate]
                              ,[RegistrationEndDate]
                              ,[StartDate]
                              ,[EndDate]
                              ,[Capacity]
                              ,[Registrations]"
                             );
                sb.AppendLine("FROM[dbo].[vw_AvailableCourses]");
                return sb.ToString();
            }
        }
        protected override string GetWhereClause()
        {
            if (filter == null) return string.Empty;
            var sb = new StringBuilder();
            if (filter.CourseId != null && filter.CourseId > 0)
            {
                sb.AppendLine($"Id = @{nameof(filter.CourseId)}");
                return $"WHERE {sb}";
            }
            if (filter.CategoryId != null && filter.CategoryId > 0)
            {
                sb.AppendLine($"CourseCategoryId = @{nameof(filter.CategoryId)}");
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                if (sb.Length > 0) { sb.AppendLine("AND"); }
                sb.AppendLine($"Name LIKE '%' + @{nameof(filter.Name)} + '%'");
            }
            if (filter.StartDate != null)
            {
                if (sb.Length > 0) { sb.AppendLine("AND"); }
                sb.AppendLine($"StartDate = @{nameof(filter.StartDate)}");
            }
            if (filter.EndDate != null)
            {
                if (sb.Length > 0) { sb.AppendLine("AND"); }
                sb.AppendLine($"EndDate = @{nameof(filter.EndDate)}");
            }

            if (sb.Length > 0) { return $"WHERE {sb}"; } else return string.Empty;
        }

        protected override IEnumerable<KeyValuePair<string, object>> GetFilterParameters()
        {

            var dictionary = new Dictionary<string, object>();
            if (filter != null)
            {
                if (filter.CourseId != null && filter.CourseId > 0)
                {
                    dictionary.Add($"@{nameof(filter.CourseId)}", filter.CourseId);
                    return dictionary.ToList();
                }
                if (filter.CategoryId != null && filter.CategoryId > 0)
                {
                    dictionary.Add($"@{nameof(filter.CategoryId)}", filter.CategoryId);
                }
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    dictionary.Add($"@{nameof(filter.Name)}", filter.Name);
                }
                if (filter.StartDate != null)
                {
                    dictionary.Add($"@{nameof(filter.StartDate)}", filter.StartDate);
                }
                if (filter.EndDate != null)
                {
                    dictionary.Add($"@{nameof(filter.EndDate)}", filter.EndDate);
                }

            }

            return dictionary.ToList();

        }
        protected override Course Transform(IDataReader reader)
        {
            var courseId = Convert.ToInt32(reader[nameof(Course.Id)]);
            var courseMasterId = Convert.ToInt32(reader["CourseMasterId"]);
            var courseName = Convert.ToString(reader["Name"]) ?? "Unknown";
            var courseDesc = Convert.ToString(reader["Description"]);
            var categoryName = Convert.ToString(reader["CourseCategoryName"]);
            var categoryId = Convert.ToInt32(reader["CourseCategoryId"]);
            var category = new CourseCategory(categoryId, categoryName ?? "Unknown");
            var courseMaster = new CourseMaster(courseMasterId, courseName, courseCategory: category, description: courseDesc);
            var startDate = Convert.ToDateTime(reader["StartDate"]);
            var endDate = Convert.ToDateTime(reader["EndDate"]);
            var regstartDate = Convert.ToDateTime(reader["RegistrationStartDate"]);
            var regendDate = Convert.ToDateTime(reader["RegistrationEndDate"]);
            var capacity = Convert.ToInt32(reader["Capacity"]);
            var registrations = Convert.ToInt32(reader["Registrations"]);
            var c = new Course(id: courseId, courseMaster: courseMaster, regstartDate, regendDate, startDate, endDate, capacity);
            c.CalaculateRemainingPlaces(registrations);
            return c;
        }
    }
}
