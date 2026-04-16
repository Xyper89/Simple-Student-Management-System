using Azure.Core;
using Student_Management_System.Data;
using Student_Management_System.Models;
using Student_Management_System.utils;

namespace Student_Management_System.utils
{
    public class StudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public bool AddStudent(Student student, out string message)
        {
            if (student == null)
            {
                message = "Invalid student data";
                return false;
            }

            if (string.IsNullOrWhiteSpace(student.Name))
            {
                message = "Name is required";
                return false;
            }

            if (student.Age <= 15)
            {
                message = "Invalid age";
                return false;
            }

            if (student.YearLevel < 1 || student.YearLevel > 4)
            {
                message = "Invalid year level";
                return false;
            }

            if (_repo.AddStudent(student))
            {
                message = "Student added successfully";
                return true;
            }

            message = "Failed to add student";
            return false;
        }

        public (bool Success, string Message) UpdateStudent(Student student)
        {
            if (student == null)
                return (false, "Invalid student");

            if (!_repo.StudentExists(student.StudentID))
                return (false, "Student does not exist");

            if (_repo.UpdateStudent(student))
                return (true, "Updated successfully");

            return (false, "Update failed");
        }

        public (bool Success, string Message) DeleteStudent(int id)
        {
            if (id <= 0)
                return (false, "Invalid ID");

            if (!_repo.StudentExists(id))
                return (false, "Student does not exist");

            if (_repo.DeleteStudent(id))
                return (true, $"Student ID {id} deleted successfully");

            return (false, "Delete failed");
        }

        public (bool Success, List<Student> Students, string Message)
            SearchStudentByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, new List<Student>(), "Name required");

            var result = _repo.SearchStudentByName(name);

            if (result.Count == 0)
                return (false, result, "No student found");

            return (true, result, "Students found");
        }
    }
}