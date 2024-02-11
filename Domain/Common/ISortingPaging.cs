namespace Domain.Common
{
    public interface ISortingPaging
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        bool SortAsc { get; set; }
        string? SortBy { get; set; }

        bool Equals(object? obj);
        bool Equals(SortingPaging? other);
        int GetHashCode();
        string ToString();
    }
}