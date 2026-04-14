namespace Student_Management_System.Models
{
    public class Student
    {
        public int StudentID {  get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        public required string Course { get; set; }
        public int YearLevel { get; set; }

    }
}
