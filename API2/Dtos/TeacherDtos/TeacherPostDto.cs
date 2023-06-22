using System.ComponentModel.DataAnnotations;

namespace API2.Dtos.TeacherDtos
{
    public class TeacherPostDto
    {
        [Required]
        [MaxLength(100)]
        public string Fullname { get; set; }

       
    }
}
