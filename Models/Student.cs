namespace CourseOPR.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentFullName { get; set;}

        public List<Score>? Scores { get; set; }

        public int GroupId { get; set; }
        public Group? Group { get; set; }

    }
}
