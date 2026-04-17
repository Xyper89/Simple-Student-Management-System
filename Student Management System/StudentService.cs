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
            try
            {
                if (student == null)
                {
                    message = "Invalid student data";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(student.Name))
                {
                    message = "Name is required.";
                    return false;
                }

                if (student.Age <= 15)
                {
                    message = "Invalid age.";
                    return false;
                }

                if (student.YearLevel < 1 || student.YearLevel > 4)
                {
                    message = "Invalid year level.";
                    return false;
                }

                if (_repo.AddStudent(student))
                {
                    message = "Student added successfully.";
                    return true;
                }

                message = "Failed to add student.";
                return false;
            }
            catch
            {
                message = "Something went wrong.";
                return false;
            }
        }

        public (bool Success, string Message) UpdateStudent(Student student)
        {
            if (student == null)
                return (false, "Invalid student data.");

            if (!_repo.StudentExists(student.StudentID))
                return (false, "Student does not exist.");

            if (_repo.UpdateStudent(student))
                return (true, "Student updated successfully.");

            return (false, "Failed to update student.");
        }

        public (bool Success, string Message) DeleteStudent(int id)
        {
            if (id <= 0)
                return (false, "Invalid student data");

            if (!_repo.StudentExists(id))
                return (false, "Student does not exist.");

            if (_repo.DeleteStudent(id))
                return (true, $"Student ID {id} deleted successfully.");

            return (false, "Failed to delete student.");
        }

        public (bool Success, List<Student> Students, string Message)
            SearchStudentByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return (false, new List<Student>(), "Name is required.");

                var students = _repo.SearchStudentByName(name);

                if (students.Count > 0)
                    return (true, students, "Students found.");

                return (false, students, "No student found.");
            }
            catch
            {
                return (false, new List<Student>(), "Search failed.");
            }
        }
    }
}