using Application.Courses.Dtos;
using MediatR;

namespace Application.Courses.Commands.CreateCourseMaster
{
    public class CreateCourseSlotRequest : IRequest<int>
    {
        public int CourseMasterId { get; }
        public CreateCourseDto CreateCourseDto { get; }
        public CreateCourseSlotRequest(int courseMasterId, CreateCourseDto createCourseDto)
        {
            CourseMasterId = courseMasterId;
            CreateCourseDto = createCourseDto;
        }
    }
}
