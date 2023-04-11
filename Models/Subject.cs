namespace CourseOPR.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public int? Hours { get; set; }
        public int? Semester { get; set; }

        public List<Score>? Scores { get; set; }

        public int SpecialityId { get; set; }
        public Speciality? Speciality { get; set; } 
    }
}
