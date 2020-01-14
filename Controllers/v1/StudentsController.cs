using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Models.v1;
using StudentsAPI.Services.v1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentsAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly IStudentsService studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            this.studentsService = studentsService;
        }

        // GET: api/Students
        [HttpGet]
        public IEnumerable<Student> Get([FromQuery] Filter filter)
        {
            return studentsService.Get(filter).ToList();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public ActionResult<Student> Get(long id)
        {
            try
            {
                return studentsService.Get().Single(s => s.Id == id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        // POST: api/Students
        [HttpPost]
        public ActionResult Post([FromBody] Student student)
        {
            if (studentsService.Get().Any(s => s.Id == student.Id))
            {
                studentsService.Update(student);
                return Ok(student);
            }

            studentsService.Add(student);
            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public ActionResult Put(long id, [FromBody] Student student)
        {
            if (student.Id != id)
            {
                return BadRequest();
            }
            if (!studentsService.Get().Any(s => s.Id == student.Id))
            {
                return NotFound();
            }

            studentsService.Update(student);
            return Ok(student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            if (!studentsService.Get().Any(s => s.Id == id))
            {
                return NotFound();
            }

            studentsService.Delete(id);
            return Ok();
        }
    }
}
