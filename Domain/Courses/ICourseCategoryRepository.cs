namespace Domain.Courses
{
    public interface ICourseCategoryRepository
    {
        public IEnumerable<CourseCategory> GetCategories();
        public CourseCategory? GetCategory(int id);
    }
}
