using Application.Courses.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Courses.Commands.CreateCourseMaster
{
    public class CreateCourseMasterRequest : IRequest<int>
    {
        public CreateCourseMasterDto CreateCourseMasterDto { get; }
        public CreateCourseMasterRequest(CreateCourseMasterDto createCourseMasterDto)
        {
            CreateCourseMasterDto = createCourseMasterDto;
        }
    }
}
