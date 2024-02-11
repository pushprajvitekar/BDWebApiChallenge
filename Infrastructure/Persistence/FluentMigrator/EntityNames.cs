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
            internal const string CourseMaster = "CourseMaster";
            internal const string Course = "Course";
            internal const string CourseCategory = "CourseCategory";
            internal const string CourseRegistration = "CourseRegistration";
            internal const string StudentCourse = "StudentCourse";
        }
        internal static class ColumnName
        {
            internal const string Id = "Id";
            internal const string FirstName = "FirstName";
            internal const string LastName = "LastName";
            internal const string Name  = "Name";
            internal const string Description = "Description";
            internal const string StartDate = "StartDate";
            internal const string EndDate = "EndDate";
            internal const string Email = "Email";
            internal const string Capacity = "Capacity";
            internal const string CreatedBy = "CreatedBy";
            internal const string CreatedDate = "CreatedDate";
            internal const string RegisteredBy = "RegisteredBy";
            internal const string RegistrationDate = "RegistrationDate";
            internal const string RegistrationStartDate = "RegistrationStartDate";
            internal const string RegistrationEndDate = "RegistrationEndDate";
            internal const string StudentId = "StudentId";

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
