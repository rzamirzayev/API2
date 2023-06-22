using System.ComponentModel.DataAnnotations;

namespace API2.Dtos.StudentDtos
{
    public class StudentPostDto
    {
        [Required]
        [MaxLength(25)]
        public string FullName { get; set; }
        [Required]
        public int GroupId { get; set; }
    }
}
