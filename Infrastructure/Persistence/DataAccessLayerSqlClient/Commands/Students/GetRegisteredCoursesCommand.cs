using DataAccessLayerSqlClient.Common;
using Domain.Common;
using Domain.Courses;
using Domain.Students.Queries;
using System.Data;
using System.Text;

namespace DataAccessLayerSqlClient.Commands.Students
{
    internal class GetRegisteredCoursesCommand : GetPagedResultsCommand<CourseRegistration>
    {
        private readonly int studentId;
        private readonly RegisteredCourseFilter? filter;

        public GetRegisteredCoursesCommand(int studentId, RegisteredCourseFilter? registeredCourseFilter, SortingPaging? sortingPaging = null) : base(sortingPaging)
        {
            this.studentId = studentId;
            this.filter = registeredCourseFilter;
            SetParameters();
        }

        protected override string SelectClause
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine(@"SELECT 
                               [Id]
                              ,[CourseId]  
                              ,[CourseMasterId] 
                              ,[Name]
                              ,[Description]
                              ,[CourseCategoryId]
                              ,[CourseCategoryName]
                              ,[RegistrationStartDate]
                              ,[RegistrationEndDate]
                              ,[StartDate]
                              ,[EndDate]
                              ,[StudentId]
                              ,[RegisteredBy]
                              ,[RegistrationDate]
                             "
                             );
                sb.AppendLine("FROM [dbo].[vw_RegisteredCourses]");
             
                return sb.ToString();
            }
        }
        protected override string GetWhereClause()
        {
            if (filter == null) return string.Empty;
            var sb = new StringBuilder();
            sb.AppendLine("WHERE StudentId = @StudentId");
            if (filter.Id != null && filter.Id > 0)
            {
                sb.AppendLine("AND");
                sb.AppendLine($"Id = @{nameof(filter.Id)}");
                return $"{sb}";
            }
            if (filter.CourseId != null && filter.CourseId > 0)
            {
                sb.AppendLine("AND");
                sb.AppendLine($"CourseId = @{nameof(filter.CourseId)}");
            }
            if (filter.CategoryId != null && filter.CategoryId > 0)
            {
                sb.AppendLine("AND");
                sb.AppendLine($"CourseCategoryId = @{nameof(filter.CategoryId)}");
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                sb.AppendLine("AND");
                sb.AppendLine($"Name LIKE '%' + @{nameof(filter.Name)} + '%'");
            }
            if (filter.StartDate != null)
            {
                sb.AppendLine("AND");
                sb.AppendLine($"StartDate = @{nameof(filter.StartDate)}");
            }
            if (filter.EndDate != null)
            {
                sb.AppendLine("AND");
                sb.AppendLine($"EndDate = @{nameof(filter.EndDate)}");
            }

            if (sb.Length > 0) { return $"{sb}"; } else return string.Empty;
        }

        protected override IEnumerable<KeyValuePair<string, object>> GetFilterParameters()
        {

            var dictionary = new Dictionary<string, object>();
            if (filter != null)
            {
                dictionary.Add($"@StudentId", studentId);
                if (filter.Id != null && filter.Id > 0)
                {
                    dictionary.Add($"@{nameof(filter.Id)}", filter.Id);
                    return dictionary.ToList();
                }

                if (filter.CourseId != null && filter.CourseId > 0)
                {
                    dictionary.Add($"@{nameof(filter.CourseId)}", filter.CourseId);
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
        protected override CourseRegistration Transform(IDataReader reader)
        {
            var courseRegId = Convert.ToInt32(reader[nameof(CourseRegistration.Id)]);
            var courseId = Convert.ToInt32(reader["CourseId"]);
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
            var studentId = Convert.ToInt32(reader["StudentId"]);
            var regBy = Convert.ToString(reader["RegisteredBy"]) ?? Constants.Unknown;
            var regDate = Convert.ToDateTime(reader["RegistrationDate"]);
            var c = new Course(id: courseId, courseMaster: courseMaster, regstartDate, regendDate, startDate, endDate, 0);
            var cr = new CourseRegistration(courseRegId, c, studentId, regBy, regDate);
            return cr;
        }
    }
}
