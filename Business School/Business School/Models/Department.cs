namespace Business_School.Models
{
    public class Department
    {

      
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string OfficeLocation { get; set; } = string.Empty;

        public ICollection<Club> Clubs { get; set; } = new List<Club>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }

}