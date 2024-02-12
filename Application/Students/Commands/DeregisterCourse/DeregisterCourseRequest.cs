using Application.Students.Dtos;
using MediatR;

namespace Application.Students.Commands.RegisterCourse
{
    public class DeregisterCourseRequest : IRequest<Unit>
    {
        public DeregisterCourseRequest(int studentId,DeregisterCourseDto deregisterCourseDto)
        {
            StudentId = studentId;
            DeregisterCourseDto = deregisterCourseDto;
        }

        public int StudentId { get; }
        public DeregisterCourseDto DeregisterCourseDto { get; }
    }
}
