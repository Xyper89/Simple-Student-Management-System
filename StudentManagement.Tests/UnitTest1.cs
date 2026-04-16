using Xunit;
using Student_Management_System;
using Student_Management_System.Models;
using Student_Management_System.utils;

namespace StudentManagement.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void AddStudent_InvalidAge_ReturnsFalse()
        {
            var repo = new FakeStudentRepository();
            var service = new StudentService(repo);

            var student = new Student
            {
                Name = "John",
                Age = 10,
                Course = "IT",
                YearLevel = 1
            };

            var result = service.AddStudent(student, out _);

            Assert.False(result);
        }

        [Fact]
        public void AddStudent_InvalidYearLevel_ReturnsFalse()
        {
            var repo = new FakeStudentRepository();
            var service = new StudentService(repo);

            var student = new Student
            {
                Name = "John",
                Age = 20,
                Course = "IT",
                YearLevel = 5
            };

            var result = service.AddStudent(student, out _);

            Assert.False(result);
        }

        [Fact]
        public void AddStudent_EmptyName_ReturnsFalse()
        {
            var repo = new FakeStudentRepository();
            var service = new StudentService(repo);

            var student = new Student
            {
                Name = "",
                Age = 20,
                Course = "IT",
                YearLevel = 1
            };

            var result = service.AddStudent(student, out _);

            Assert.False(result);
        }

        [Fact]
        public void DeleteStudent_IdNotFound_ReturnsFalse()
        {
            var repo = new FakeStudentRepository();
            var service = new StudentService(repo);

            var result = service.DeleteStudent(999);

            Assert.False(result.Success);
        }

        [Fact]
        public void SearchStudent_NoResults_ReturnsEmptyList()
        {
            var repo = new FakeStudentRepository();
            var service = new StudentService(repo);

            var result = service.SearchStudentByName("Unknown");

            Assert.Empty(result.Students);
        }
    }
}