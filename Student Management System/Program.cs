using Microsoft.Extensions.Configuration;
using Student_Management_System.Data;
using Student_Management_System.Models;
class Program
{
    static void Main()
    {
        var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();

        string connectionString = config.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine("Database configuration missing.");
            return;
        }

        StudentRepository repo = new StudentRepository(connectionString);

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
                        Student student = new Student();
                        //NAME
                        student.Name = ReadRequiredText("Name: ");
                        //AGE
                        student.Age = ReadPositiveInt("Age: ");
                        //COURSE
                        student.Course = ReadRequiredText("Course: ");
                        //YEAR LEVEL
                        student.YearLevel = ReadIntInRange("Year Level: ", 1, 4);
                    try
                    {
                        if (repo.AddStudent(student))
                        {
                            Console.WriteLine("Student Added Successfully");
                        }
                        else
                            Console.WriteLine("Failed To Add Student");
                    }
                    catch(Exception exception)
                    {
                        Console.WriteLine("Something went wrong while adding the student. Please try again later.");
                        LogError(exception);
                        break;
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
                    catch(Exception exception) 
                    { 
                        Console.WriteLine("We couldn't load the student list. Please try again later.");
                        LogError(exception);
                        break;
                    }
                        break;
                    //SEARCH STUDENT
                    case 3:

                        Console.WriteLine("============================");
                        Console.WriteLine("Search Student Using Name");
                        string searchNameInput = ReadRequiredText("Name: ");
                    try
                    {
                        List<Student> SearchStudent = repo.SearchStudentByName(searchNameInput);
                        if (SearchStudent.Count > 0)
                        {
                            foreach (Student students in SearchStudent)
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
                        else
                        {
                            Console.WriteLine("No Student Found!");
                        }
                    }
                    catch(Exception exception)
                    {
                        Console.WriteLine("Something went wrong while searching for students. Please try again later.");
                        LogError(exception);
                        break;
                    }
                        break;
                    //UPDATE STUDENT
                    case 4:
                        Console.WriteLine("============================");
                        Console.WriteLine("Update Student");
                        Student updateStudent = new Student();
                        //ID
                        while (true)
                        {
                            int id = ReadPositiveInt("Student ID: ");

                            if (repo.StudentExists(id))
                            {
                                updateStudent.StudentID = id;
                                break;
                            }

                            Console.WriteLine("Student ID Does Not Exist");
                        }

                        //NAME
                        updateStudent.Name = ReadRequiredText("Name: ");
                        //AGE
                                updateStudent.Age = ReadIntInRange("Age: ",18,100);
                        //COURSE
                            updateStudent.Course = ReadRequiredText("Course: ");
                        //YEAR LEVEL
                                updateStudent.YearLevel = ReadIntInRange("Year Level: ", 1, 4);
                    if (repo.UpdateStudent(updateStudent))
                    {
                        Console.WriteLine("Student Update Successfully");
                    }
                    else
                        Console.WriteLine("Student Failed To Update");

                        Console.WriteLine("============================\n\n");

                        break;
                     //DELETE STUDENT
                    case 5:
                    while (true)
                    {
                        Console.WriteLine("============================");
                        Console.WriteLine("Delete Student");
                        Console.WriteLine("Students You Want To Delete");

                        int id = ReadPositiveInt("ID: ");
                        try
                        {
                            if (!repo.StudentExists(id))
                            {
                                Console.WriteLine("Student ID Does Not Exist");
                                continue;
                            }

                            if (repo.DeleteStudent(id))
                            {
                                Console.WriteLine("Student ID " + id + " Has Been Deleted");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Failed to delete student");
                            }
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine("Something went wrong while deleting a student. Please try again later.");
                            LogError(exception);
                            break;
                        }
                        }
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
            string input = Console.ReadLine().Trim();

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
            string input = Console.ReadLine();
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
            string input = Console.ReadLine();

            if (int.TryParse(input, out int value) && value > 0)
                return value;

            Console.WriteLine("Please enter a valid positive number.");
        }
    }
    static void LogError(Exception ex)
    {
        string logFile = "error_log.txt";

        string message = $"[{DateTime.Now}] {ex.Message}\n{ex.StackTrace}\n";

        File.AppendAllText(logFile, message);
    }
}
