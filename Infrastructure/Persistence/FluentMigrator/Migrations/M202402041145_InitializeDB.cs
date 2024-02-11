using DatabaseMigrations;
using FluentMigrator;

namespace ConsoleFluentMigrator.Migrations
{
    [Migration(202402041145)]
    public class M202402041145_InitializeDB : ForwardOnlyMigration
    {
        public override void Up()
        {
            //Create.Table(EntityNames.TableName.Student)
            //      .WithColumn(EntityNames.ColumnName.Id).AsInt32().PrimaryKey().Identity()
            //      .WithColumn(EntityNames.ColumnName.FirstName).AsString(100).Nullable()
            //      .WithColumn(EntityNames.ColumnName.LastName).AsString(100).NotNullable()
            //      .WithColumn(EntityNames.ColumnName.Email).AsString(100).NotNullable().Unique();
            //      ;
            Create.Table(EntityNames.TableName.CourseCategory)
                  .WithColumn(EntityNames.ColumnName.Id).AsInt32().PrimaryKey()
                  .WithColumn(EntityNames.ColumnName.Name).AsString(1000).NotNullable()
                  ;
            string categoryfkId = EntityNames.foreignkeyname(EntityNames.TableName.CourseCategory, EntityNames.ColumnName.Id);
            Create.Table(EntityNames.TableName.CourseMaster)
                  .WithColumn(EntityNames.ColumnName.Id).AsInt32().PrimaryKey().Identity()
                  .WithColumn(EntityNames.ColumnName.Name).AsString(1000).NotNullable()
                  .WithColumn(EntityNames.ColumnName.Description).AsString(2000).Nullable()
                  .WithColumn(categoryfkId).AsInt32().NotNullable()
                   .WithColumn(EntityNames.ColumnName.CreatedBy).AsString(100).NotNullable()
                    .WithColumn(EntityNames.ColumnName.CreatedDate).AsDateTime().NotNullable()
                  ;
            Create.ForeignKey().FromTable(EntityNames.TableName.CourseMaster).ForeignColumn(categoryfkId).ToTable(EntityNames.TableName.CourseCategory).PrimaryColumn(EntityNames.ColumnName.Id);

            string courseMasterfkId = EntityNames.foreignkeyname(EntityNames.TableName.CourseMaster, EntityNames.ColumnName.Id);
            Create.Table(EntityNames.TableName.Course)
                  .WithColumn(EntityNames.ColumnName.Id).AsInt32().PrimaryKey().Identity()
                  .WithColumn(courseMasterfkId).AsInt32().NotNullable()
                  .WithColumn(EntityNames.ColumnName.RegistrationStartDate).AsDateTime().NotNullable()
                  .WithColumn(EntityNames.ColumnName.RegistrationEndDate).AsDateTime().NotNullable()
                  .WithColumn(EntityNames.ColumnName.StartDate).AsDateTime().NotNullable()
                  .WithColumn(EntityNames.ColumnName.EndDate).AsDateTime().NotNullable()
                  .WithColumn(EntityNames.ColumnName.Capacity).AsInt32().NotNullable()
                   .WithColumn(EntityNames.ColumnName.CreatedBy).AsString(100).NotNullable()
                    .WithColumn(EntityNames.ColumnName.CreatedDate).AsDateTime().NotNullable()
                  ;
           

        }
    }
}
