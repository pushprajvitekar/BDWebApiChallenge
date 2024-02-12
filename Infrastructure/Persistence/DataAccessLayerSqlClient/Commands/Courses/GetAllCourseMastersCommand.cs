using DataAccessLayerSqlClient.Common;
using Domain.Common;
using Domain.Courses;
using Domain.Courses.Queries;
using System.Data;
using System.Text;

namespace DataAccessLayerSqlClient.Commands.Courses
{
    internal class GetAllCourseMastersCommand : GetPagedResultsCommand<CourseMaster>
    {
        private readonly CourseMasterFilter? filter;

        public GetAllCourseMastersCommand(CourseMasterFilter? filter = null, SortingPaging? sortingPaging = null) : base(sortingPaging)
        {
            this.filter = filter;
            SetParameters();
        }
        protected override string SelectClause
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("SELECT Id, Name, Description, CourseCategoryId, CourseCategoryName");
                sb.AppendLine("FROM [dbo].[vw_CourseMaster]");
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
            if (filter.CategoryId != null && filter.CategoryId > 0)
            {
                sb.AppendLine($"CourseCategoryId = @{nameof(filter.CategoryId)}");
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                if (sb.Length > 0) { sb.AppendLine("AND"); }
                sb.AppendLine($"Name LIKE '%' + @{nameof(filter.Name)} + '%'");
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
                if (filter.CategoryId != null && filter.CategoryId > 0)
                {
                    dictionary.Add($"@{nameof(filter.CategoryId)}", filter.CategoryId);
                }
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    dictionary.Add($"@{nameof(filter.Name)}", filter.Name);
                }
            }

            return dictionary.ToList();

        }

        protected override CourseMaster Transform(IDataReader reader)
        {
            var courseId = Convert.ToInt32(reader[nameof(CourseMaster.Id)]);
            var courseName = Convert.ToString(reader[nameof(CourseMaster.Name)]) ?? Constants.Unknown;
            var courseDesc = Convert.ToString(reader[nameof(CourseMaster.Description)]);
            var categoryName = Convert.ToString(reader["CourseCategoryName"]);
            var categoryId = Convert.ToInt32(reader["CourseCategoryId"]);
            var category = new CourseCategory(categoryId, categoryName ?? Constants.Unknown);
            return new CourseMaster(id: courseId, name: courseName, courseCategory: category, description: courseDesc);
        }
    }
}
