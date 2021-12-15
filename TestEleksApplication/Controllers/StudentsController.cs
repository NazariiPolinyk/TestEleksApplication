using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEleksApplication.DataLayer.Models;

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

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Student>> Get()
        {
            return await _context.Students.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Student> Get(int id)
        {
            return await _context.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

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

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> Put(int id, [FromBody] Student student)
        {
            if (student == null) return BadRequest();
            if (_context.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) == null) return NotFound();
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
