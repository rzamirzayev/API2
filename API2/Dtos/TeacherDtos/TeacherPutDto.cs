using System.ComponentModel.DataAnnotations;

namespace API2.Dtos.TeacherDtos
{
    public class TeacherPutDto
    {
        [MaxLength(100)]        
        
        public string Fullname { get; set; }
    }
}
