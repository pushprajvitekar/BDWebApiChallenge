using Application.Courses.Queries.GetCourseMasters;
using Application.Students;
using Application.Students.Dtos;
using Application.Students.Queries.GetAvailableCourses;
using Domain.Common;
using Domain.Students.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
     
        public async Task<IActionResult> GetAvailableCourses([FromQuery] StudentCourseFilter? filter, [FromQuery] SortingPaging? sortingPaging)
        {
            var res = await mediator.Send(new GetAvailableCoursesQuery(filter, sortingPaging));
            return Ok(res);
        }

        //GET students/{id}/ courses - registered courses
        //status codes 200,404,500

        [HttpGet("{id}/courses")]
        public IActionResult GetStudentCourses(int studentId, [FromQuery] StudentCourseFilter? filter, [FromQuery] SortingPaging? sortingPaging)
        {
            //var res = studentCourseService.GetRegisteredCourses(studentId, filter, sortingPaging);
            return Ok(1);
        }

       

        //insert new student
        // POST api/<StudentsController>
        //status codes 201,400/415(Wrong format /data),500
        //POST students/{id}/courses/{courseid{ courseslotid} register
        [HttpPost("{id}/courses")]
        public IActionResult RegisterCourse(int studentId, [FromBody] RegisterCourseDto  courseDto )
        {
            try
            {
                if (courseDto == null)
                {
                    return BadRequest();
                }
                //var res = studentCourseService.RegisterCourse(courseDto);
                return CreatedAtAction(nameof(GetStudentCourses), new { id = 1 });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        //update student information
        // PUT api/<StudentsController>/5
        //status codes 200,400/415,404,500


        // DELETE api/<StudentsController>/5
        //status codes 200/204,404,500
        //DELETE students/{id}/ courses /{ courseid}{ courseslotid}
        [HttpDelete("{id}/courses/{courseslotid}")]
        public IActionResult DeregisterCourse(int courseslotid)
        {
            //var res = studentCourseService.DeregisterCourse(new DeregisterCourseDto() { });
            return Ok(1);
        }
    }
}
