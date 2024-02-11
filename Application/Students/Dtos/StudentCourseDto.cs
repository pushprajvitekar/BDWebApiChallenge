using Application.Courses.Dtos;

namespace Application.Students.Dtos
{
    public class StudentCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string  CategoryName { get; set;}
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RegistrationStartDate { get; set; }
        public DateTime RegistrationEndDate { get; set; }
        public int Capacity { get; set; }
        public int AvailableSeats { get; set; }
    }
}
