using Application.Courses.Dtos;
using MediatR;

namespace Application.Courses.Commands.CreateCourseMaster
{
    public class UpdateCourseMasterRequest : IRequest<int>
    {
        public int CourseId { get; }
        public UpdateCourseMasterDto UpdateCourseMasterDto { get; }
        public UpdateCourseMasterRequest(int courseId, UpdateCourseMasterDto updateCourseMaster)
        {
            CourseId = courseId;
            UpdateCourseMasterDto = updateCourseMaster;
        }
    }
}
