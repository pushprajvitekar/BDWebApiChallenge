using Domain.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Students
{
    public class StudentCourse
    {
        public StudentCourse(Course course, string registeredBy, string registeredDate)
        {
            Course = course;
            RegisteredBy = registeredBy;
            RegisteredDate = registeredDate;
        }

        public Course Course { get; set; }
        public string RegisteredBy { get; set; }
        public string RegisteredDate { get; set; }
    }
}
