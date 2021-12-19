using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEleksApplication.DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace TestEleksApplication.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly TestEleksDbContext _context;

        public StudentsController(TestEleksDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await _context.Students.AsNoTracking().ToListAsync();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (student == null) return NotFound();
            return new ObjectResult(student);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Student>> Post([FromBody] Student student)
        {
            if (student == null) return BadRequest();
            try
            {
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();

                return Ok(student);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> Put(int id, [FromBody] Student student)
        {
            if (student == null) return BadRequest();
            if (await _context.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) == null) return NotFound();
            try
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();

                return Ok(student);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> Delete(int id)
        {
            var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (student == null) return NotFound();
            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return Ok(student);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
