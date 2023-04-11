namespace CourseOPR.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public int GroupNumber {get;set;}

        public List<Student>? Students { get; set; }

        public int SpecialityId { get; set; }
        public Speciality? Speciality { get; set; }



    }
}
