using FluentMigrator;

namespace DatabaseMigrations.Migrations
{
    [Migration(202402111800)]
    public class M202402111800_AddCourseStoredProcsAndViews : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Execute.EmbeddedScript("M202402111800_AddCourseStoredProcsAndViews.sql");
        }
    }
}
