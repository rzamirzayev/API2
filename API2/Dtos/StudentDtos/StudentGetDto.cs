namespace API2.Dtos.StudentDtos
{
  
        public class StudentGetDto
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public GroupInStudentGetDto Group { get; set; }
        }

        public class GroupInStudentGetDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    
}
