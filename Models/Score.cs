namespace CourseOPR.Models
{
    public class Score
    {
        public int ScoreId { get; set; }
        public string? ScoreValue { get; set; }

        public int? StudentId { get; set; }
        public Student? Student { get; set; }
        
        public int? SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}
