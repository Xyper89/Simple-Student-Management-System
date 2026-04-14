using Azure.Core;
using Student_Management_System.Data;
using Student_Management_System.Models;
using Student_Management_System.utils;
namespace Student_Management_System
{
    public class StudentService
    {
        private readonly StudentRepository _repo;

        public StudentService(StudentRepository repo)
        {
            _repo = repo;
        }
        public bool AddStudent(Student student, out string message)
        {
            try
            {
                if (student == null)
                {
                    message = "Invalid student data";
                    return false;
                }
                if (string.IsNullOrWhiteSpace(student.Name)) {
                    message = "Name is required.";
                    return false;
                }

                if (student.Age <= 15)
                {
                    message = "Invalid age.";
                    return false;
                    ; }

                if (_repo.AddStudent(student))
                {
                    message = "Student added successfully.";
                    return true;
                }

                message = "Failed to add student.";
                return false;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                message = "Something went wrong while adding the student. Please try again later.";
                return false;
            }
        }
        public (bool Success, List<Student> Students, string Message) SearchStudentByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return (false, new List<Student>(), "Name is required.");
                }

                var students = _repo.SearchStudentByName(name);

                if (students.Count > 0)
                {
                    return (true, students, "Students found.");
                }

                return (false, students, "No student found.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return (false, new List<Student>(), "Something went wrong while searching.");
            }
        }
        public (bool Success, string Message) UpdateStudent(Student student)
        {
            try
            {
                if(student == null)
                {
                    return (false, "Invalid student data.");
                }
                if (_repo.StudentExists(student.StudentID))
                {
                    return (false, "Student does not exist.");
                }
                if (_repo.StudentExists(student.StudentID))
                {
                    return (true, "Student update successfully.");
                }
                return (false, "Failed to updated student.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return (false, "Something went wrong while updating student.");
            }
        }

        public (bool Success, string Message) DeleteStudent(int id)
        {
            try
            {
                if (id == 0 )
                {
                    return (false, "Invalid student data");
                }
                if (_repo.StudentExists(id))
                {
                    return (false, "Student does not exist.");
                }
                if (_repo.DeleteStudent(id))
                {
                    return (true, "Student ID" + id + "deleted successfully.");
                }
                return (false, "Failed to delete student.");
            }
            catch (Exception ex) 
            {
                Logger.LogError(ex);
                return (false, "Something went wrong while deleting student.");
            }
        }
    }
}

