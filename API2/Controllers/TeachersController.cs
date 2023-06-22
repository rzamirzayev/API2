using API2.DAL;
using API2.Dtos.GroupDtos;
using API2.Dtos.TeacherDtos;
using API2.Dtos.TeacherDtos;
using API2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly DataContext _context;

        public TeachersController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("all")]
        public ActionResult<List<TeacherGetAllItemDto>> GetAll()
        {
            var data = _context.Teachers.Include(x=>x.Groups).Select(x => new TeacherGetAllItemDto { Id = x.Id, Fullname = x.Fullname }).ToList();
            return Ok(data);
        }

        /// <summary>
        /// Get all Teachers by selected page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("")]
        public ActionResult<List<TeacherGetAllItemDto>> GetAll(int page = 1)
        {
            var query = _context.Teachers.Include(x => x.Groups).AsQueryable();

            var items = query.Skip((page - 1) * 4).Take(4).Select(x => new TeacherGetAllItemDto { Id = x.Id, Fullname = x.Fullname}).ToList();
            var totalPages = (int)Math.Ceiling(query.Count() / 4d);

            var data = new PaginatedListDto<TeacherGetAllItemDto>(items, totalPages, page);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult<TeacherGetDto> Get(int id)
        {
            var data = _context.Teachers.Include(x => x.Groups).FirstOrDefault(x => x.Id == id);

            if (data == null)
                return NotFound();

            TeacherGetDto TeacherDto = new TeacherGetDto
            {
                Id = data.Id,
                Fullname = data.Fullname,
              
            };

            return Ok(TeacherDto);
        }

        [HttpPost("")]
        public ActionResult Create(TeacherPostDto TeacherDto)
        {
            if (_context.Teachers.Any(x => x.Fullname == TeacherDto.Fullname))
            {
                ModelState.AddModelError("Name", "Name is already exist");
                return BadRequest(ModelState);
            }

             Teacher teacher = new Teacher
            {
                Fullname = TeacherDto.Fullname,
            };

            _context.Teachers.Add(teacher);
            _context.SaveChanges();

            return StatusCode(201, new { Id = teacher.Id });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TeacherDto"></param>
        /// <returns></returns>
        /// <response code="204">Data updated</response>
        /// <response code="404">Data Namet found</response>
        /// <response code="400">Data is Namet valid</response>
        /// 
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult Edit(int id, TeacherPutDto TeacherDto)
        {
            Teacher Teacher = _context.Teachers.Find(id);

            if (Teacher == null)
                return NotFound();

            if (Teacher.Fullname != TeacherDto.Fullname && _context.Teachers.Any(x => x.Fullname == TeacherDto.Fullname))
            {
                ModelState.AddModelError("Name", "Name is already exist");
                return BadRequest(ModelState);
            }

            Teacher.Fullname = TeacherDto.Fullname;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Teacher Teacher = _context.Teachers.Find(id);
            if (Teacher == null)
                return NotFound();

            _context.Teachers.Remove(Teacher);
            _context.SaveChanges();

            return NoContent();
        }
    }

    internal class PaginatedListDto<T>
    {
        private List<TeacherGetAllItemDto> items;
        private int totalPages;
        private int page;
        private List<GroupGetAllItemDto> items1;

        public PaginatedListDto(List<TeacherGetAllItemDto> items, int totalPages, int page)
        {
            this.items = items;
            this.totalPages = totalPages;
            this.page = page;
        }

        public PaginatedListDto(List<GroupGetAllItemDto> items1, int totalPages, int page)
        {
            this.items1 = items1;
            this.totalPages = totalPages;
            this.page = page;
        }
    }
}
