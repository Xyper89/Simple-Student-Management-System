using Student_Management_System.Models;
using Student_Management_System.Data;

namespace Student_Management_System
{
    public class FakeStudentRepository : IStudentRepository
    {
        public bool AddStudent(Student student) => true;

        public bool StudentExists(int id) => false;

        public bool DeleteStudent(int id) => false;

        public bool UpdateStudent(Student student) => false;

        public List<Student> SearchStudentByName(string name)
            => new List<Student>();
    }
}