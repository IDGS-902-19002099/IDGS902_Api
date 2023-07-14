using IDGS902_Api.Context;
using IDGS902_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IDGS902_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposController : Controller
    {
        private readonly AppDbContext _context;

        public GruposController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.alumnos.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}", Name = "Alumnos")]
        public ActionResult Get(int id)
        {
            try
            {
                var alumno = _context.alumnos.FirstOrDefault(x => x.Id == id);
                if (alumno == null)
                {
                    return NotFound();
                }
                return Ok(alumno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]

        public ActionResult <Alumnos> Post([FromBody] Alumnos alumno)
        {
            try
            {
                _context.alumnos.Add(alumno);
                _context.SaveChanges();
                return CreatedAtRoute("GetAlumno", new { id = alumno.Id }, alumno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Alumnos alumno)
        {
            try
            {
                if (alumno.Id == id)
                {
                    _context.Entry(alumno).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("GetAlumno", new { id = alumno.Id }, alumno);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var alumno = _context.alumnos.FirstOrDefault(x => x.Id == id);
                if (alumno != null)
                {
                    _context.alumnos.Remove(alumno);
                    _context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
