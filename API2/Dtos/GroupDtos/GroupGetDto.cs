namespace API2.Dtos.GroupDtos
{
        public class GroupGetDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<StudentItemInGroupGetDto> Students { get; set; }
        }

        public class StudentItemInGroupGetDto
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            
        }
    
}
