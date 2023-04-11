namespace CourseOPR.Models
{
    public class Speciality
    {
        public int SpecialityId { get; set; }
        public string SpecialityName { get; set; }

        public List<Group>? Groups { get; set; }
        public List<Subject>? Subjects { get; set; }

        public int FacultyId { get; set; }
        public Faculty? Faculty { get; set; }

    }
}
