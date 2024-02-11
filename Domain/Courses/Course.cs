using Domain.Exceptions;
using Domain.Students;

namespace Domain.Courses
{
    public class Course
    {
        public int Id { get; protected set; }
        public DateTime? RegistrationStartDate { get; protected set; }
        public DateTime? RegistrationEndDate { get; protected set; }
        public DateTime? StartDate { get; protected set; }
        public DateTime? EndDate { get; protected set; }
        public int CourseMasterId { get; protected set; }
        public CourseMaster? CourseMaster { get; protected set; }

        public IList<Student>? Students { get; protected set; }

        public int Capacity { get; protected set; }
        public int RemainingPlaces { get; private set; }
        public string CreatedBy { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public Course(int courseMasterId, string createdBy)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(courseMasterId);
            CourseMasterId = courseMasterId;
            CreatedBy = createdBy;
            CreatedDate = DateTime.Now;
        }

        public void UpdateCapacity(int capacity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
            Capacity = capacity;
        }
        public void UpdateRegistrationWindow(DateTime start, DateTime end)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(end, start);
            if (StartDate.HasValue && (start >= StartDate || end >= StartDate))
            {
                throw new DomainException("Registration Dates need to be before Course Start date", null, DomainErrorCode.Conflict);
            }
            else
            {
                RegistrationStartDate = start;
                RegistrationEndDate = end;
            }
        }
        public void UpdateDuration(DateTime start, DateTime end)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(end, start);
            if(!RegistrationStartDate.HasValue || !RegistrationEndDate.HasValue)
                throw new DomainException("Registration dates need to be set before Duration dates", null, DomainErrorCode.Conflict);
            if (start < RegistrationEndDate || end < RegistrationEndDate)
            {
                throw new DomainException("Course Dates need to be after Course Registration dates", null, DomainErrorCode.Conflict);
            }
            else
            {
                StartDate = start;
                EndDate = end;
            }
            
        }
        public Course(int id, CourseMaster courseMaster, DateTime regstartDate, DateTime regendDate, DateTime startDate, DateTime endDate, int capacity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
            Id = id;
            ArgumentNullException.ThrowIfNull(courseMaster);
            CourseMaster = courseMaster;
            StartDate = startDate;
            EndDate = endDate;
            Capacity = capacity;
            RegistrationStartDate = regstartDate; 
            RegistrationEndDate =regendDate;
        }
        public void CalaculateRemainingPlaces(int registered)
        {
            RemainingPlaces = Capacity - registered;
        }
        
    }
}
