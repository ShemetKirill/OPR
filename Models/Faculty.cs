namespace CourseOPR.Models
{
    public class Faculty
    {
        public int FacultyId { get; set; }
        public string  FacultyName { get; set; }

        public List<Speciality>? Specialities { get; set; } 
    }
}
