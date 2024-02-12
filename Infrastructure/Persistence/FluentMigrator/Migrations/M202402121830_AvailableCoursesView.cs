using FluentMigrator;

namespace DatabaseMigrations.Migrations
{
    [Migration(202402121830)]
    public class M202402121830_AvailableCoursesView : Migration
    {
        public override void Down()
        {
            Delete.Table("[dbo].[vw_AvailableCourses]").IfExists(); 
            Delete.Table("[dbo].[vw_RegisteredCourses]").IfExists();
            Execute.Sql("DROP PROCEDURE [dbo].[usp_CourseRegistration_Insert]");
        }

        public override void Up()
        {
            Execute.EmbeddedScript("M202402121830_AvailableCoursesView.sql");
        }
    }
}
