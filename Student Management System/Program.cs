using System;
using System.Threading.Channels;
using Microsoft.Data.SqlClient;
using Student_Management_System.Data;
using Student_Management_System.Models;
class Program
{
    static void Main()
    {
        string connectionString = "Server=.\\SQLEXPRESS;Database=StudentDB;Trusted_Connection=True;Encrypt=False;";
        StudentRepository repo = new StudentRepository(connectionString);
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
            string SChoice = Console.ReadLine();
            if (!int.TryParse(SChoice, out int choice) || choice < 1 || choice > 6)
            {
                Console.WriteLine("============================");
                Console.WriteLine("Invalid Choice");
                continue;
            }
            else
            {
                int Choice = choice;
                    Console.WriteLine("============================\n\n");
                    switch (Choice)
                    {
                        //ADD STUDENT
                        case 1:
                            Console.WriteLine("============================");
                            Console.WriteLine("Add Student");
                            Student s = new Student();
                            //NAME
                            while (true)
                            {
                                Console.Write("Name: ");
                                s.Name = Console.ReadLine();
                                if (!string.IsNullOrEmpty(s.Name) && !string.IsNullOrWhiteSpace(s.Name))
                                    break;
                                else
                                    Console.WriteLine("Invalid Input");
                            }
                            //AGE
                            while (true)
                            {
                                Console.Write("Age: ");
                                string age = Console.ReadLine();
                                if (int.TryParse(age, out int agee))
                                {
                                    s.Age = agee;
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
                                s.Course = Console.ReadLine();
                                if (!string.IsNullOrEmpty(s.Course) && !string.IsNullOrWhiteSpace(s.Course))
                                    break;
                                else
                                    Console.WriteLine("Invalid Input");
                            }
                            //YEAR LEVEL
                            while (true)
                            {

                                Console.Write("Year Level: ");
                                string year = Console.ReadLine();
                                if (int.TryParse(year, out int yearr) && yearr >= 1 && yearr <= 4)
                                {
                                    s.YearLevel = yearr;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                }
                            }
                            repo.AddStudent(s);
                            Console.WriteLine("Student Add Successfully");
                            Console.WriteLine("============================\n\n");
                            break;
                        //CHECK STUDENT
                        case 2:
                            Console.WriteLine("============================");
                            Console.WriteLine("Check Students");
                            List<Student> All = repo.GetAllStudents();
                            foreach (Student ss in All)
                            {
                                Console.WriteLine("============================");
                                Console.WriteLine("ID" + ss.StudentID);
                                Console.WriteLine("Name: " + ss.Name);
                                Console.WriteLine("Age: " + ss.Age);
                                Console.WriteLine("Course: " + ss.Course);
                                Console.WriteLine("Year Level: " + ss.YearLevel);
                                Console.WriteLine("============================\n\n");
                            }
                            break;
                        //SEARCH STUDENT
                        case 3:

                            Console.WriteLine("============================");
                            Console.WriteLine("Search Student Using Name");
                            Console.Write("Name: ");
                            string SearchName = Console.ReadLine();
                            List<Student> SearchStudent = repo.SearchStudentByName(SearchName);
                            if (SearchStudent.Count > 0)
                            {
                                foreach (Student ss in SearchStudent)
                                {
                                    Console.WriteLine("============================");
                                    Console.WriteLine("ID" + ss.StudentID);
                                    Console.WriteLine("Name: " + ss.Name);
                                    Console.WriteLine("Age: " + ss.Age);
                                    Console.WriteLine("Course: " + ss.Course);
                                    Console.WriteLine("Year Level: " + ss.YearLevel);
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
                            Student sss = new Student();
                            //ID
                            while (true)
                            {
                                Console.Write("Student ID: ");
                                string ID = Console.ReadLine();
                                if (int.TryParse(ID, out int IDD))
                                {
                                    sss.StudentID = IDD;
                                    if (!repo.StudentExists(sss.StudentID))
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
                                sss.Name = Console.ReadLine();
                                if (!string.IsNullOrEmpty(sss.Name) && !string.IsNullOrWhiteSpace(sss.Name))
                                    break;
                                else
                                    Console.WriteLine("Invalid Input");
                            }
                            //AGE
                            while (true)
                            {
                                Console.Write("Age: ");
                                string age = Console.ReadLine();
                                if (int.TryParse(age, out int agee))
                                {
                                    sss.Age = agee;
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
                                sss.Course = Console.ReadLine();
                                if (!string.IsNullOrEmpty(sss.Course) && !string.IsNullOrWhiteSpace(sss.Course))
                                    break;
                                else
                                    Console.WriteLine("Invalid Input");
                            }
                            //YEAR LEVEL
                            while (true)
                            {

                                Console.Write("Year Level: ");
                                string year = Console.ReadLine();
                                if (int.TryParse(year, out int yearr) && yearr >= 1 && yearr <= 4)
                            {
                                    sss.YearLevel = yearr;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                }
                            }
                            repo.UpdateStudent(sss);
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
                                string IDDD = Console.ReadLine();
                                if (int.TryParse(IDDD, out int IDDDr))
                                {
                                    int IDDDD = IDDDr;
                                    if (repo.StudentExists(IDDDD))
                                    {
                                        repo.DeleteStudent(IDDDD);
                                        Console.WriteLine("Student ID" + IDDD + " Has Been Deleted");
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