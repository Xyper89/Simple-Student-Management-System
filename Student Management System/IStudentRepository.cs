using Student_Management_System.Models;
namespace Student_Management_System.Data
{
	public interface IStudentRepository
	{
		bool AddStudent(Student student);
		bool StudentExists(int id);
		bool DeleteStudent(int id);
		bool UpdateStudent(Student student);
		List<Student> SearchStudentByName(string name);
	}
}