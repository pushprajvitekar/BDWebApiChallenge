using FluentMigrator;

namespace DatabaseMigrations.Migrations
{
    [Migration(202402121800)]
    public class M202402121800_CourseRegistration : Migration
    {
        string coursefkId = EntityNames.foreignkeyname(EntityNames.TableName.Course, EntityNames.ColumnName.Id);
        public override void Down()
        {
            Delete.ForeignKey().FromTable(EntityNames.TableName.CourseRegistration).ForeignColumn(coursefkId).ToTable(EntityNames.TableName.Course).PrimaryColumn(EntityNames.ColumnName.Id);
            Delete.Table(EntityNames.TableName.CourseRegistration).IfExists();
        }

        public override void Up()
        {
            Create.Table(EntityNames.TableName.CourseRegistration)
                  .WithColumn(EntityNames.ColumnName.Id).AsInt32().PrimaryKey().Identity()
                  .WithColumn(EntityNames.ColumnName.StudentId).AsInt32().NotNullable()
                  .WithColumn(coursefkId).AsInt32().NotNullable()
                  .WithColumn(EntityNames.ColumnName.RegisteredBy).AsString(100).NotNullable()
                  .WithColumn(EntityNames.ColumnName.RegistrationDate).AsDateTime().NotNullable()
                  ;
            Create.ForeignKey().FromTable(EntityNames.TableName.CourseRegistration).ForeignColumn(coursefkId).ToTable(EntityNames.TableName.Course).PrimaryColumn(EntityNames.ColumnName.Id);
        }
    }
}
