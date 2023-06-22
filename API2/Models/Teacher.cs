namespace API2.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Fullname { get; set; }    

        public int Salary { get; set; }

        public List<Group> Groups { get; set; }
    }
}
