using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMigrations
{
    internal static class EntityNames
    {

        internal static class TableName
        {
            internal const string Student = "Student";
            internal const string Course = "Course";
        }
        internal static class ColumnName
        {
            internal const string Id = "Id";
            internal const string FirstName = "FirstName";
            internal const string LastName = "LastName";
            internal const string Name  = "Name";
            internal const string Description = "Description";


        }
        internal static string junctiontablename(string table1, string table2)
        {
            return $"{table1}{table2}";
        }

        internal static string foreignkeyname(string tablename, string idname)
        {
            return $"{tablename}{idname}";
        }
    }
}
