using Domain.Common;
using System.Text;

namespace DataAccessLayerSqlClient.Common
{
    internal abstract class GetPagedResultsCommand<T> : SqlCommandBase<T>
    {
        readonly bool paginate = false;
        readonly bool sortAsc = false;
        readonly string? sortby;
        private readonly SortingPaging? sortingPaging;
        const string parameterOrderBy = "@orderBy";
        const string parameterPageNumber = "@pageNumber";
        const string parameterPageSize = "@pageSize";
        public GetPagedResultsCommand(SortingPaging? sortingPaging = null)
        {
            
            sortby = sortingPaging?.SortBy ;
            sortAsc = sortingPaging?.SortAsc ?? false;
            paginate = !string.IsNullOrEmpty(sortby) && sortingPaging?.PageNumber > 0 && sortingPaging?.PageSize > 0;
            this.sortingPaging = sortingPaging;
        }

        protected abstract string SelectClause { get; }
        protected sealed override string CommandText
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine(SelectClause);
                sb.AppendLine(GetWhereClause());
                sb.AppendLine(GetOrderyByClause());
                sb.AppendLine(GetPagingClause());
                return sb.ToString().Trim();
            }
        }
        private string GetOrderyByClause()
        {
            var sortOrder = sortAsc ? @"ASC" : "DESC";
            if (!string.IsNullOrEmpty(sortby))
            {
                return $" ORDER BY {parameterOrderBy} {sortOrder}";
            }
            return string.Empty;
        }

        private string GetPagingClause()
        {
            if (paginate)
            {
                var sb = new StringBuilder();
                sb.Append($"OFFSET {parameterPageNumber} ROWS");
                sb.AppendLine($"FETCH NEXT {parameterPageSize} ROWS ONLY");
                return sb.ToString();
            }
            return string.Empty;
        }
        protected virtual IEnumerable<KeyValuePair<string, object>> GetFilterParameters()
        {
            return new List<KeyValuePair<string, object>>();
        }
        protected virtual string GetWhereClause()
        {
            return string.Empty;
        }
        protected sealed override void SetParameters(params object[] parameters)
        {

            Parameters = [];
            var sortingParams = GetSortingPagingParameters();
            var filterParams = GetFilterParameters();
            Parameters = filterParams.Concat(sortingParams).ToDictionary();
        }
        protected IEnumerable<KeyValuePair<string, object>> GetSortingPagingParameters()
        {

            var dictionary = new Dictionary<string, object>();
            if (sortingPaging is SortingPaging sortingpaging)
            {
                var orderBy = sortingpaging.SortBy;
                if (!string.IsNullOrEmpty(orderBy))
                {
                    dictionary.Add(parameterOrderBy, orderBy);
                };
                if (paginate)
                {
                    var pageNumber = sortingpaging.PageNumber;
                    dictionary.Add(parameterPageNumber, pageNumber);
                    var pageSize = sortingpaging.PageSize;
                    dictionary.Add(parameterPageSize, pageSize);
                }
            }
            return dictionary.ToList();
        }
    }
}
