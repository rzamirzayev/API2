namespace API2.Dtos.TeacherDtos
{
    public class TeacherGetAllItemDto
    {
        public int Id { get; set; } 

        public string Fullname { get; set; }

        public List<GroupInTeacherDto> Groups { get; set; }
    }

}
