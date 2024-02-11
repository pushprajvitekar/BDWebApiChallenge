using Application.Courses.Dtos;
using MediatR;

namespace Application.Courses.Commands.CreateCourseMaster
{
    public class UpdateCourseSlotRequest : IRequest<int>
    {
        public int CourseMasterId { get; }
        public int CourseId { get; }
        public UpdateCourseDto UpdateCourseDto { get; }
        public UpdateCourseSlotRequest(int courseMasterId, int courseId, UpdateCourseDto updateCourseMaster)
        {
            CourseMasterId = courseMasterId;
            CourseId = courseId;
            UpdateCourseDto = updateCourseMaster;
        }
    }
}
