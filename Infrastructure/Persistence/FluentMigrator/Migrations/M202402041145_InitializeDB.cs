using DatabaseMigrations;
using FluentMigrator;

namespace ConsoleFluentMigrator.Migrations
{
    [Migration(202402041145)]
    public class M202402041145_InitializeDB : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table(EntityNames.TableName.Student)
                  .WithColumn(EntityNames.ColumnName.Id).AsInt32().PrimaryKey().Identity()
                  .WithColumn(EntityNames.ColumnName.FirstName).AsString(100).Nullable()
                  .WithColumn(EntityNames.ColumnName.LastName).AsString(100).NotNullable()
                  ;
            Create.Table(EntityNames.TableName.Course)
                  .WithColumn(EntityNames.ColumnName.Id).AsInt32().PrimaryKey().Identity()
                  .WithColumn(EntityNames.ColumnName.Name).AsString(1000).Nullable()
                  .WithColumn(EntityNames.ColumnName.Description).AsString(2000).NotNullable()
                  ;
            string studentcoursetable = EntityNames.junctiontablename(EntityNames.TableName.Student, EntityNames.TableName.Course);
            string studentfkId = EntityNames.foreignkeyname(EntityNames.TableName.Student, EntityNames.ColumnName.Id);
            string coursefkId = EntityNames.foreignkeyname(EntityNames.TableName.Course, EntityNames.ColumnName.Id);
            Create.Table(studentcoursetable)
                  .WithColumn(studentfkId).AsInt32().NotNullable()
                  .WithColumn(coursefkId).AsInt32().NotNullable()
                  ;
            var compKey = new[] { studentfkId, coursefkId };
            Create.PrimaryKey().OnTable(studentcoursetable).Columns(compKey);

            Create.ForeignKey().FromTable(studentcoursetable).ForeignColumn(studentfkId).ToTable(EntityNames.TableName.Student).PrimaryColumn(EntityNames.ColumnName.Id);
            Create.ForeignKey().FromTable(studentcoursetable).ForeignColumn(coursefkId).ToTable(EntityNames.TableName.Course).PrimaryColumn(EntityNames.ColumnName.Id);

        }
    }
}
