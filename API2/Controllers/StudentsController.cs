using API2.DAL;
using API2.Models;
using Microsoft.AspNetCore.Mvc;

namespace API2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly DataContext _context;

        public StudentsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetOne/{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var data = _context.Students.Find(id);

            if (data == null) return NotFound();

            return StatusCode(200, data);

        }

        [HttpGet("GetAll")]
        public ActionResult<List<Student>> GetAllStudent()
        {
            var data = _context.Students.ToList();

            if (!data.Any()) return NotFound();

            return StatusCode(200, data);

        }
        [HttpPost("create")]
        public ActionResult Create(Student student)
        {
            if(student.GroupId==0)
            {
                ModelState.AddModelError("GroupId","GroupId is required.");
                return BadRequest(ModelState);
            }
            var existGroup=_context.Groups.Find(student.GroupId);
            if (existGroup == null) return NotFound();

            student.Group= existGroup;
            _context.Students.Add(student);
            _context.SaveChanges();

            return StatusCode(200, student);
        }

        [HttpPost("delete")]
        public ActionResult Delete(int id)
        {
            var exitsStudent = _context.Students.Find(id);

            if (exitsStudent == null) return NotFound();

            _context.Students.Remove(exitsStudent);
            _context.SaveChanges();

            return StatusCode(200);
        }
        [HttpPost("edit")]
        public ActionResult Edit(Student student)
        {
            var existStudent = _context.Students.Find(student.Id);
            var existGroup = _context.Groups.Find(student.GroupId);

            if (existStudent == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(existStudent.Fullname))
            {
                existStudent.Fullname = student.Fullname;

            }
            if(student.GroupId != 0 && existGroup!=null)
            {
                existStudent.GroupId = student.GroupId;
                existStudent.Group = existGroup;
            }
            
            
            _context.SaveChanges();

            return StatusCode(200);

        }
    }
}
