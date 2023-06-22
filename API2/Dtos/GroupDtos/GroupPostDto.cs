using System.ComponentModel.DataAnnotations;

namespace API2.Dtos.GroupDtos
{
    public class GroupPostDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
