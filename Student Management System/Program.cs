using Student_Management_System.Data;
using Student_Management_System.Models;
using Microsoft.Extensions.Configuration;
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
            Console.Write("Choice: ");
            string choiceInput = Console.ReadLine();
            if (!int.TryParse(choiceInput, out int choice) || choice < 1 || choice > 6)
            {
                Console.WriteLine("============================");
                Console.WriteLine("Invalid Choice");
                continue;
            }
            else
            {
                    Console.WriteLine("============================\n\n");
                    switch (choice)
                    {
                        //ADD STUDENT
                        case 1:
                            Console.WriteLine("============================");
                            Console.WriteLine("Add Student");
                            Student student = new Student();
                            //NAME
                            while (true)
                            {
                                Console.Write("Name: ");
                                student.Name = Console.ReadLine();
                                if (!string.IsNullOrEmpty(student.Name) && !string.IsNullOrWhiteSpace(student.Name))
                                    break;
                                else
                                    Console.WriteLine("Invalid Input");
                            }
                            //AGE
                            while (true)
                            {
                                Console.Write("Age: ");
                                string ageInput = Console.ReadLine();
                                if (int.TryParse(ageInput, out int age))
                                {
                                    student.Age = age;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");

                                }
                            }
                            //COURSE
                            while (true)
                            {
                                Console.Write("Course: ");
                                student.Course = Console.ReadLine();
                                if (!string.IsNullOrEmpty(student.Course) && !string.IsNullOrWhiteSpace(student.Course))
                                    break;
                                else
                                    Console.WriteLine("Invalid Input");
                            }
                            //YEAR LEVEL
                            while (true)
                            {

                                Console.Write("Year Level: ");
                                string yearLevelInput = Console.ReadLine();
                                if (int.TryParse(yearLevelInput, out int yearLevel) && yearLevel >= 1 && yearLevel <= 4)
                                {
                                    student.YearLevel = yearLevel;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                }
                            }
                            repo.AddStudent(student);
                            Console.WriteLine("Student Add Successfully");
                            Console.WriteLine("============================\n\n");
                            break;
                        //CHECK STUDENT
                        case 2:
                            Console.WriteLine("============================");
                            Console.WriteLine("Check Students");
                            List<Student> All = repo.GetAllStudents();
                            foreach (Student students in All)
                            {
                                Console.WriteLine("============================");
                                Console.WriteLine("ID" + students.StudentID);
                                Console.WriteLine("Name: " + students.Name);
                                Console.WriteLine("Age: " + students.Age);
                                Console.WriteLine("Course: " + students.Course);
                                Console.WriteLine("Year Level: " + students.YearLevel);
                                Console.WriteLine("============================\n\n");
                            }
                            break;
                        //SEARCH STUDENT
                        case 3:

                            Console.WriteLine("============================");
                            Console.WriteLine("Search Student Using Name");
                            Console.Write("Name: ");
                            string searchNameInput = Console.ReadLine();
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

                            break;
                        //UPDATE STUDENT
                        case 4:
                            Console.WriteLine("============================");
                            Console.WriteLine("Update Student");
                            Student updateStudent = new Student();
                            //ID
                            while (true)
                            {
                                Console.Write("Student ID: ");
                                string idInput = Console.ReadLine();
                                if (int.TryParse(idInput, out int id))
                                {
                                updateStudent.StudentID = id;
                                    if (!repo.StudentExists(updateStudent.StudentID))
                                {
                                        Console.WriteLine("Student ID Does Not Exist");
                                    }
                                    else
                                        break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");

                                }
                            }
                            //NAME
                            while (true)
                            {
                                Console.Write("Name: ");
                            updateStudent.Name = Console.ReadLine();
                                if (!string.IsNullOrEmpty(updateStudent.Name) && !string.IsNullOrWhiteSpace(updateStudent.Name))
                                    break;
                                else
                                    Console.WriteLine("Invalid Input");
                            }
                            //AGE
                            while (true)
                            {
                                Console.Write("Age: ");
                                string ageInput = Console.ReadLine();
                                if (int.TryParse(ageInput, out int age))
                                {
                                    updateStudent.Age = age;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");

                                }
                            }
                            //COURSE
                            while (true)
                            {
                                Console.Write("Course: ");
                            updateStudent.Course = Console.ReadLine();
                                if (!string.IsNullOrEmpty(updateStudent.Course) && !string.IsNullOrWhiteSpace(updateStudent.Course))
                                    break;
                                else
                                    Console.WriteLine("Invalid Input");
                            }
                            //YEAR LEVEL
                            while (true)
                            {

                                Console.Write("Year Level: ");
                                string yearInput = Console.ReadLine();
                                if (int.TryParse(yearInput, out int year) && year >= 1 && year <= 4)
                            {
                                updateStudent.YearLevel = year;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                }
                            }
                            repo.UpdateStudent(updateStudent);
                            Console.WriteLine("Student Update Successfully");
                            Console.WriteLine("============================\n\n");

                            break;
                        //DELETE STUDENT
                        case 5:
                            while (true)
                            {
                                Console.WriteLine("============================");
                                Console.WriteLine("Delete Student");
                                Console.WriteLine("Students You Want To Delete");
                                Console.Write("ID: ");
                                string idInput = Console.ReadLine();
                                if (int.TryParse(idInput, out int id))
                                {
                                    if (repo.StudentExists(id))
                                    {
                                        repo.DeleteStudent(id);
                                        Console.WriteLine("Student ID" + id + " Has Been Deleted");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Student ID Does Not Exist");
                                    }
                                }

                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                }
                            }
                            break;
                        case 6:
                            Console.WriteLine("============================");
                            Console.WriteLine("Thank You For Using The Student Management System!");
                            Console.WriteLine("============================");
                            replay = false;
                            break;
                    }
            }
        }
    }
}
