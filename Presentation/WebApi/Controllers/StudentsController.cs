using Application.Students.Commands.RegisterCourse;
using Application.Students.Dtos;
using Application.Students.Queries.GetAvailableCourses;
using Domain.Common;
using Domain.Students.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = $"{Roles.Admin},{Roles.Student}")]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public StudentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("courses")]
        public async Task<IActionResult> GetAvailableCourses([FromQuery] AvailableCourseFilter? filter, [FromQuery] SortingPaging? sortingPaging)
        {
            var res = await mediator.Send(new GetAvailableCoursesQuery(filter, sortingPaging));
            return Ok(res);
        }

        //GET students/{id}/ courses - registered courses
        //status codes 200,404,500

        [HttpGet()]
        [Route("{id}/courses", Name = "GetStudentCourses")]
        public async Task<IActionResult> GetStudentCourses(int id, [FromQuery] RegisteredCourseFilter? filter, [FromQuery] SortingPaging? sortingPaging)
        {
            var res = await mediator.Send(new GetRegisteredCoursesRequest(id, filter, sortingPaging));
            return Ok(res);
        }



        //insert new student
        // POST api/<StudentsController>
        //status codes 201,400/415(Wrong format /data),500
        //POST students/{id}/courses/ register
        [HttpPost("{id}/courses")]
        public async Task<IActionResult> RegisterCourse(int id, [FromBody] RegisterCourseDto courseDto)
        {
            if (courseDto == null)
            {
                return BadRequest();
            }
            var res = await mediator.Send(new RegisterCourseRequest(id, courseDto));
            return CreatedAtRoute($"GetStudentCourses", new { id },res);
            //return Created(new Uri($"/{id}/courses", UriKind.Relative), res);
        }


        // DELETE api/<StudentsController>/5
        //status codes 200/204,404,500
        //DELETE students/{id}/ courses /{ courseid}
        [HttpDelete("{id}/courses")]
        public async Task<IActionResult> DeregisterCourse(int id, [FromBody] DeregisterCourseDto courseDto)
        {
            if (courseDto == null)
            {
                return BadRequest();
            }
            var res = await mediator.Send(new DeregisterCourseRequest(id, courseDto));
            // return AcceptedAtRoute($"GetStudentCourses", new { id });
            return Ok(res);
        }
    }
}
