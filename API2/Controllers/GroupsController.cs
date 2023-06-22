using API2.DAL;
using API2.Dtos;
using API2.Dtos.GroupDtos;
using API2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : Controller
    {
        private readonly DataContext _context;

        public GroupsController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("all")]
        public ActionResult<List<GroupGetAllItemDto>> GetAll()
        {
            var data = _context.Groups.Include(x => x.Students).Select(x => new GroupGetAllItemDto { Id = x.Id, Name = x.Name, StudentsCount = x.Students.Count }).ToList();
            return Ok(data);
        }

        /// <summary>
        /// Get all groups by selected page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("")]
        public ActionResult<List<GroupGetAllItemDto>> GetAll(int page = 1)
        {
            var query = _context.Groups.Include(x => x.Students).AsQueryable();

            var items = query.Skip((page - 1) * 4).Take(4).Select(x => new GroupGetAllItemDto { Id = x.Id, Name = x.Name, StudentsCount = x.Students.Count }).ToList();
            var totalPages = (int)Math.Ceiling(query.Count() / 4d);

            var data = new PaginatedListDto<GroupGetAllItemDto>(items, totalPages, page);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult<GroupGetDto> Get(int id)
        {
            var data = _context.Groups.Include(x => x.Students).FirstOrDefault(x => x.Id == id);

            if (data == null)
                return NotFound();

            GroupGetDto groupDto = new GroupGetDto
            {
                Id = data.Id,
                Name = data.Name,
                Students = data.Students.Select(x => new StudentItemInGroupGetDto { Id = x.Id, FullName = x.Fullname }).ToList(),
            };

            return Ok(groupDto);
        }

        [HttpPost("")]
        public ActionResult Create(GroupPostDto groupDto)
        {
            if (_context.Groups.Any(x => x.Name == groupDto.Name))
            {
                ModelState.AddModelError("Name", "Name is already exist");
                return BadRequest(ModelState);
            }

            Group group = new Group
            {
                Name = groupDto.Name,
            };

            _context.Groups.Add(group);
            _context.SaveChanges();

            return StatusCode(201, new { Id = group.Id });
        }
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult Edit(int id, GroupPutDto groupDto)
        {
            Group group = _context.Groups.Find(id);

            if (group == null)
                return NotFound();

            if (group.Name != groupDto.Name && _context.Groups.Any(x => x.Name == groupDto.Name))
            {
                ModelState.AddModelError("Name", "Name is already exist");
                return BadRequest(ModelState);
            }

            group.Name = groupDto.Name;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Group group = _context.Groups.Find(id);
            if (group == null)
                return NotFound();

            _context.Groups.Remove(group);
            _context.SaveChanges();

            return NoContent();
        }


    }
}
