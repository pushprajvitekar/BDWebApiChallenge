using Application.Students.Dtos;
using MediatR;

namespace Application.Students.Commands.RegisterCourse
{
    public class RegisterCourseRequest : IRequest<int>
    {
        public RegisterCourseRequest(int studentId, RegisterCourseDto registerCourseDto)
        {
            StudentId = studentId;
            RegisterCourseDto = registerCourseDto;
        }

        public int StudentId { get; }
        public RegisterCourseDto RegisterCourseDto { get; }
    }
}
