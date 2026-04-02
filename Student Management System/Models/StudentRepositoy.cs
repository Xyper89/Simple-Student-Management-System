using Microsoft.Data.SqlClient;
using Student_Management_System.Models;
using System;
using System.Collections.Generic;

namespace Student_Management_System.Data
{
    public class StudentRepository
    {
        private string connectionString;

        public StudentRepository(string connStr)
        {
            connectionString = connStr;
        }

        // -------------------
        // 1 CREATE
        // -------------------
        public void AddStudent(Student s)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Students(Name, Age, Course, YearLevel)" +
                    "VALUES(@Name, @Age, @Course, @YearLevel)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", s.Name);
                    cmd.Parameters.AddWithValue("@Age", s.Age);
                    cmd.Parameters.AddWithValue("@Course", s.Course);
                    cmd.Parameters.AddWithValue("@YearLevel", s.YearLevel);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        // -------------------
        // 2 READ (All Students)
        // -------------------
        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT StudentID, Name, Age, Course, YearLevel FROM Students";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student s = new Student
                            {
                                StudentID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Age = reader.GetInt32(2),
                                Course = reader.GetString(3),
                                YearLevel = reader.GetInt32(4)
                            };
                            students.Add(s);
                        }
                    }
                }
            }
            return students;
        }

        // -------------------
        // 3 READ / SEARCH (By Name)
        // -------------------
        public List<Student> SearchStudentByName(string name)
        {
            List<Student> students = new List<Student>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT StudentID, Name, Age, Course, YearLevel " +
                               "FROM Students WHERE Name LIKE @Name";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", "%" + name + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student s = new Student
                            {
                                StudentID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Age = reader.GetInt32(2),
                                Course = reader.GetString(3),
                                YearLevel = reader.GetInt32(4)
                            };
                            students.Add(s);
                        }
                    }
                }
            }
            return students;
        }

        // -------------------
        // 4 UPDATE
        // -------------------
        public void UpdateStudent(Student s)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Students SET Name=@Name, Age=@Age, Course=@Course, YearLevel=@YearLevel " +
                               "WHERE StudentID=@StudentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", s.Name);
                    cmd.Parameters.AddWithValue("@Age", s.Age);
                    cmd.Parameters.AddWithValue("@Course", s.Course);
                    cmd.Parameters.AddWithValue("@YearLevel", s.YearLevel);
                    cmd.Parameters.AddWithValue("@StudentID", s.StudentID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // -------------------
        // 5 DELETE
        // -------------------
        public void DeleteStudent(int studentID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Students WHERE StudentID=@StudentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        // -------------------
        // 6 CHECK STUDENT IF STILL EXIST
        // -------------------
        public bool StudentExists(int studentId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM Students WHERE StudentID = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", studentId);

                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }
    }
}