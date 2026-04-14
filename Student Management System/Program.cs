using Microsoft.Extensions.Configuration;
using Student_Management_System.Data;
using Student_Management_System.Models;
using Student_Management_System.utils;
using Student_Management_System;
class Program
{
    static void Main()
    {
        var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();

        string? connectionString = config.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Connection string 'DefaultConnection' is missing.");
        }

        if (string.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine("Database configuration missing.");
            return;
        }
        StudentRepository repo = new StudentRepository(connectionString);
        StudentService service = new StudentService(repo);

        Console.WriteLine("App started successfully!");

        bool replay = true;
        while (replay)
        {
            Console.WriteLine("============================");
            Console.WriteLine("Student Management System");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Check Students");
            Console.WriteLine("3. Search Student");
            Console.WriteLine("4. Update Student");
            Console.WriteLine("5. Delete Student");
            Console.WriteLine("6. Exit");
            int choice = ReadIntInRange("Choice: ", 1, 6);
            Console.WriteLine("============================\n\n");
            switch (choice)
            {
                //ADD STUDENT
                case 1:
                    Console.WriteLine("============================");
                    Console.WriteLine("Add Student");
                    Student student = new Student
                    {
                        Name = ReadRequiredText("Name: "),
                        Age = ReadPositiveInt("Age: "),
                        Course = ReadRequiredText("Course: "),
                        YearLevel = ReadIntInRange("Year Level: ", 1, 4)
                    };
                    if (service.AddStudent(student, out string message))
                    {
                        Console.WriteLine(message);
                    }
                    else
                    {
                        Console.WriteLine(message);
                    }
                    Console.WriteLine("============================\n\n");
                    break;

                //CHECK STUDENT
                case 2:
                    Console.WriteLine("============================");
                    Console.WriteLine("Check Students");
                    try
                    {
                        List<Student> allStudents = repo.GetAllStudents();
                        foreach (Student students in allStudents)
                        {
                            Console.WriteLine("============================");
                            Console.WriteLine("ID" + students.StudentID);
                            Console.WriteLine("Name: " + students.Name);
                            Console.WriteLine("Age: " + students.Age);
                            Console.WriteLine("Course: " + students.Course);
                            Console.WriteLine("Year Level: " + students.YearLevel);
                            Console.WriteLine("============================\n\n");
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("We couldn't load the student list. Please try again later.");
                        Logger.LogError(exception);
                        break;
                    }
                    break;
                //SEARCH STUDENT
                case 3:

                    Console.WriteLine("============================");
                    Console.WriteLine("Search Student Using Name");

                    string searchNameInput = ReadRequiredText("Name: ");

                    var result = service.SearchStudentByName(searchNameInput);

                    if (result.Success && result.Students.Count > 0)
                    {
                        foreach (Student students in result.Students)
                        {
                            Console.WriteLine("============================");
                            Console.WriteLine("ID: " + students.StudentID);
                            Console.WriteLine("Name: " + students.Name);
                            Console.WriteLine("Age: " + students.Age);
                            Console.WriteLine("Course: " + students.Course);
                            Console.WriteLine("Year Level: " + students.YearLevel);
                            Console.WriteLine("============================\n\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine(result.Message);
                    }
                    break;
                //UPDATE STUDENT
                case 4:
                    Console.WriteLine("============================");
                    Console.WriteLine("Update Student");
 
                    int id = ReadPositiveInt("Student ID: ");

                    string name = ReadRequiredText("Name: ");
                    int age = ReadIntInRange("Age: ", 18, 100);
                    string course = ReadRequiredText("Course: ");
                    int yearLevel = ReadIntInRange("Year Level: ", 1, 4);
                    Student updateStudent = new Student()
                    {
                        StudentID = id,
                        Name = name,
                        Age = age,
                        Course = course,
                        YearLevel = yearLevel
                    };
                    var results = service.UpdateStudent(updateStudent);

                    Console.WriteLine(results.Message);
                    Console.WriteLine("============================\n\n");
                    break;
            
                     //DELETE STUDENT
                case 5:
                    Console.WriteLine("============================");
                    Console.WriteLine("Delete Student");
                    Console.WriteLine("Students You Want To Delete");

                    int studentId = ReadPositiveInt("ID: ");

                    var msg = service.DeleteStudent(studentId);

                    Console.WriteLine(msg.Message);
                    Console.WriteLine("============================\n\n");

                    break;
                //QUIT
                case 6:
                    Console.WriteLine("============================");
                    Console.WriteLine("Thank You For Using The Student Management System!");
                    Console.WriteLine("============================");
                    replay = false;
                    break;
                }
        }

    }

    //INPUT HELPERS
    static string ReadRequiredText(string promt)
    {
        while (true)
        {
            Console.Write(promt);
            string input = (Console.ReadLine() ?? "").Trim();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
                Console.WriteLine("Input Is Required. Please Try Again.");
        }
    }
    static int ReadIntInRange(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";
            if (int.TryParse(input, out int value) && value >= min && value <= max)
                return value;

            Console.WriteLine($"Please Enter A Number Between {min} And {max}.");
        }
    }
    static int ReadPositiveInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int value) && value > 0)
                return value;

            Console.WriteLine("Please enter a valid positive number.");
        }
    }
}
