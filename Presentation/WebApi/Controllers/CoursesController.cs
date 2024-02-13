using Application.Courses.Commands.CreateCourseMaster;
using Application.Courses.Dtos;
using Application.Courses.Queries.GetCourseMasters;
using Application.Courses.Validators;
using Domain.Common;
using Domain.Courses.Queries;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Transactions;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{Roles.CourseManager},{Roles.Admin}")]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CoursesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        #region coursemaster
        // GET: api/<CoursesController>
        [HttpGet]
        public async Task<IActionResult> GetCourses([FromQuery] CourseMasterFilter? filter, [FromQuery] SortingPaging? sortingPaging)
        {
            var res = await mediator.Send(new GetCourseMastersQuery(filter, sortingPaging));
            return Ok(res);
        }


        // POST api/<CoursesController>
        [HttpPost]
        public async Task<IActionResult> CreateCourseMaster([FromBody] CreateCourseMasterDto createCourseMaster)
        {
            try
            {
                if (createCourseMaster == null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var res = await mediator.Send(new CreateCourseMasterRequest(createCourseMaster));
                return CreatedAtAction(nameof(GetCourses), new { id = res });
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
            catch (DomainException dex)
            {
                return StatusCode(dex.ErrorCode, dex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Error");
            }
        }
        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourseMaster(int id, [FromBody] UpdateCourseMasterDto updateCourseMaster)
        {
            try
            {
                if (updateCourseMaster == null)
                {
                    return BadRequest();
                }
                var res = await mediator.Send(new UpdateCourseMasterRequest(id, updateCourseMaster));
                return AcceptedAtAction(nameof(GetCourses), new { id = res });
            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
            catch (DomainException dex)
            {
                return StatusCode(dex.ErrorCode, dex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Error");
            }
        }
        #endregion coursemaster
        #region courseslots
        //GET api/<CourseController>/5/slots
        [HttpGet("{id}/slots")]
        public async Task<IActionResult> GetCourseSlots(int id, [FromQuery] CourseFilter? filter, [FromQuery] SortingPaging? sortingPaging)
        {
            var res = await mediator.Send(new GetCourseSlotsQuery(id, filter, sortingPaging));
            return Ok(res);
        }
        //POST api/<CourseController>/5/slots
        [HttpPost("{id}/slots")]
        public async Task<IActionResult> CreateCourseSlot(int id, [FromBody] CreateCourseDto createCourse)
        {
            if (createCourse == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var res = await mediator.Send(new CreateCourseSlotRequest(id, createCourse));
            return CreatedAtAction(nameof(GetCourses), new { id = res });

        }

        // PUT api/<CoursesController>/5/slots/1
        [HttpPut("{id}/slots/{courseid}")]
        public async Task<IActionResult> Put(int id, int courseid, [FromBody] UpdateCourseDto updateCourse)
        {
            if (updateCourse == null)
            {
                return BadRequest();
            }
            var res = await mediator.Send(new UpdateCourseSlotRequest(id, courseid, updateCourse));
            return AcceptedAtAction(nameof(GetCourses), new { id = res });

        }

        // DELETE api/<CoursesController>/5/slots/1
        [HttpDelete("{id}/slots/{courseid}")]
        public IActionResult Delete(int id, int courseid)
        {
            return Ok();
        }
        #endregion courseslots
    }
}
