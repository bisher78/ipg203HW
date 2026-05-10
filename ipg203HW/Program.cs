using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ipg203HW.Program;

namespace ipg203HW
{

    internal class Program
    {
        public delegate void MenuChoiceHandler(int choice, DateTime time);
        public static event MenuChoiceHandler OnMenuChoice;
        public interface IMember
        {
            void View();
            string GetRole();
        }
        public abstract class Thing : IMember
        {
            private int[] students_id = new int[50];
            private int[] classs = new int[50];
            private string[] Student = new string[50];
            private int[] Teacher = new int[50];
            private int[] Subject = new int[50];
            protected int[] StudentsId { get { return students_id; } set { students_id = value; } }
            protected int[] Classs { get { return classs; } set { classs = value; } }
            protected string[] Students { get { return Student; } set { Student = value; } }
            protected int[] Teachers { get { return Teacher; } }
            protected int[] Subjects { get { return Subject; } }

            public abstract void View();
            public abstract string GetRole();
            public static int TotalMembersCount { get; set; } = 0;

            protected void SetStudent(int id, string name, int classId, int teacherId, int subjectId)
            {
                if (id >= 1 && id <= 50)
                {
                    students_id[id - 1] = id;
                    Student[id - 1] = name;
                    classs[id - 1] = classId;
                    Teacher[id - 1] = teacherId;
                    Subject[id - 1] = subjectId;
                }
            }
            protected string GetStudentName(int id)
            {
                if (id >= 1 && id <= 50 && Student[id - 1] != null)
                    return Student[id - 1];
                return null;
            }
            protected int GetStudentClass(int id)
            {
                if (id >= 1 && id <= 50) return classs[id - 1];
                return 0;
            }
            protected int GetStudentTeacher(int id)
            {
                if (id >= 1 && id <= 50) return Teacher[id - 1];
                return 0;
            }
            protected int GetStudentSubject(int id)
            {
                if (id >= 1 && id <= 50) return Subject[id - 1];
                return 0;
            }
            protected void SetTeacher(int id)
            {
                if (id >= 1 && id <= 50) Teacher[id - 1] = id;
            }
            protected void SetSubject(int id)
            {
                if (id >= 1 && id <= 50) Subject[id - 1] = id;
            }
            protected void SetClass(int id)
            {
                if (id >= 1 && id <= 50) classs[id - 1] = id;
            }
        }
        public class Student : Thing
        {
            private int studentId;

            public Student(int id, string name, int classId, int teacherId, int subjectId)
            {
                studentId = id;
                SetStudent(id, name, classId, teacherId, subjectId);
            }

            public bool HasId(int id)
            {
                return studentId == id;
            }

            public override void View()
            {
                string name = GetStudentName(studentId);
                if (name != null)
                {
                    Console.WriteLine($"Student: {name} (ID: {studentId})");
                    Console.WriteLine($"   Class: {GetStudentClass(studentId)}");
                    Console.WriteLine($"   Subject: {GetStudentSubject(studentId)}");
                    Console.WriteLine($"   Teacher: {GetStudentTeacher(studentId)}");
                }
                else
                {
                    Console.WriteLine($"No student found with ID: {studentId}");
                }
            }
            public override string GetRole() => "Student";
        }
        public class Teacher : Thing
        {
            private readonly int teacherId;
            public int TeacherId
            {
                get { return teacherId; }
            }
            public Teacher(int id)
            {
                teacherId = id;
                SetTeacher(id);
            }

            public override void View()
            {
                Console.WriteLine($"Teacher ID: {teacherId}");
            }

            public override string GetRole() => "Teacher";
        }
        public class Subject : Thing
        {
            private readonly int subjectId;

            public int SubjectId
            {
                get { return subjectId; }
            }

            public Subject(int id)
            {
                subjectId = id;
                SetSubject(id);
            }

            public override void View()
            {
                Console.WriteLine($"Subject ID: {subjectId}");
            }

            public override string GetRole() => "Subject";
        }


        public class Class : Thing
        {
            private int classId;

            public Class(int id)
            {
                classId = id;
                SetClass(id);
            }

            public override void View()
            {
                Console.WriteLine($"Class ID: {classId}");
            }

            public override string GetRole() => "Class";
        }
        public class SchoolManagement
        {

            private List<Thing> items = new List<Thing>();

            public List<Thing> Items
            {
                get { return items; }
            }

            public void AddItem(Thing item)
            {
                items.Add(item);
                Console.WriteLine($"Added: {item.GetRole()}");
            }

            public void DisplayAllItems()
            {
                Console.WriteLine("\n=== All Items (Polymorphism Demo) ===");
                foreach (Thing item in items)
                {

                    item.View();
                    Console.WriteLine($"   Role: {item.GetRole()}");
                    Console.WriteLine("---");
                }
            }

            public void DisplayByRole(string role)
            {
                Console.WriteLine($"\n=== All {role}s ===");
                foreach (Thing item in items)
                {
                    if (item.GetRole() == role)
                    {
                        item.View();
                    }
                }
            }

            public int Count => items.Count;
        }
        public static class DataValidator
        {

            public static int TotalItems { get; set; } = 0;
            public static bool IsValidId(int id)
            {
                return id >= 1 && id <= 50;
            }
        }
            static void Main(string[] args)
            {
                SchoolManagement school = new SchoolManagement();

                OnMenuChoice += LogMenuChoice;
                OnMenuChoice += CheckMenuChoice;
                while (true)
                {
                    Console.WriteLine("choice from: ");
                    Console.WriteLine("1. add student");
                    Console.WriteLine("2. add teacher");
                    Console.WriteLine("3. add class");
                    Console.WriteLine("4. add subject");
                    Console.WriteLine("5. search for student");
                    Console.WriteLine("6. view students");
                    Console.WriteLine("7. view teachers");
                    Console.WriteLine("8. view classes");
                    Console.WriteLine("9. view subjects");
                    Console.WriteLine("10. exit");
                    Console.Write("your choice : ");
                    string input = Console.ReadLine();
                    int choice;
                    if (int.TryParse(input, out choice))
                    {
                        OnMenuChoice?.Invoke(choice, DateTime.Now);
                    }

                    switch (input)
                    {
                        case "1":
                            ADDSTUDENT(school);
                            break;
                        case "2":
                            ADDTEACHER(school);
                            break;

case "3":
                            ADDCLASS(school);
                            break;
                        case "4":
                            ADDSUBJECT(school);
                            break;
                        case "5":
                            SEARCHSTUDENT(school);
                            break;
                    case "6":
                        VIEWSTUDENTS(school);
                        break;
                    case "7":
                        VIEWTEACHERS(school);
                        break;
                    case "8":
                        VIEWCLASSES(school);
                        break;
                    case "9":
                        VIEWSUBJECTS(school);
                        break;

                    default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            public static void ADDSTUDENT(SchoolManagement school)
            {
                Console.Write("Enter student ID (1-50): ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("Enter student name: ");
                string name = Console.ReadLine();
                Console.Write("Enter class ID (1-50): ");
                int classId = int.Parse(Console.ReadLine());
                Console.Write("Enter teacher ID (1-50): ");
                int teacherId = int.Parse(Console.ReadLine());
                Console.Write("Enter subject ID (1-50): ");
                int subjectId = int.Parse(Console.ReadLine());
                if (DataValidator.IsValidId(id) && DataValidator.IsValidId(classId) && DataValidator.IsValidId(teacherId) && DataValidator.IsValidId(subjectId))
                {
                    Student student = new Student(id, name, classId, teacherId, subjectId);
                    school.AddItem(student);
                    Console.WriteLine($"Student {name} added successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid input. IDs must be between 1 and 50.");
                }
            }
            public static void ADDTEACHER(SchoolManagement school)
            {
                Console.Write("Enter teacher ID (1-50): ");
                int id = int.Parse(Console.ReadLine());
                if (DataValidator.IsValidId(id))
                {
                    Teacher teacher = new Teacher(id);
                    school.AddItem(teacher);
                    Console.WriteLine($"Teacher with ID {id} added successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid input. IDs must be between 1 and 50.");
                }
            }
            public static void ADDCLASS(SchoolManagement school)
            {
    Console.Write("Enter class ID (1-50): ");
    int id = int.Parse(Console.ReadLine());
    if (DataValidator.IsValidId(id))
    {
        Class classItem = new Class(id);
            school.AddItem(classItem);
        Console.WriteLine($"Class with ID {id} added successfully!");
    }
    else
    {
        Console.WriteLine("Invalid input. IDs must be between 1 and 50.");
    }
}
public static void ADDSUBJECT(SchoolManagement school)
{
    Console.Write("Enter subject ID (1-50): ");
    int id = int.Parse(Console.ReadLine());
    if (DataValidator.IsValidId(id))
    {
        Subject subject = new Subject(id);
        school.AddItem(subject);
        Console.WriteLine($"Subject with ID {id} added successfully!");
    }
    else
    {
        Console.WriteLine("Invalid input. IDs must be between 1 and 50.");
    }
}
static void LogMenuChoice(int choice, DateTime time)
            {
                Console.WriteLine($"[LOG] User selected option {choice} at {time:HH:mm:ss}");
            }
            static void CheckMenuChoice(int choice, DateTime time)
            {
                if (choice == 10)
                {
                    Console.WriteLine($"[EVENT] Exit selected. Goodbye!");
                }
            }
        public static void SEARCHSTUDENT(SchoolManagement school)
        {
            Console.Write("Enter student ID to search: ");
            int id = int.Parse(Console.ReadLine());

            foreach (Thing item in school.Items)
            {
                if (item is Student student)
                {
                    if (student.HasId(id))
                    {
                        student.View();
                        return;
                    }
                }
            }
            Console.WriteLine($"No student found with ID: {id}");
        }
        public static void VIEWSTUDENTS(SchoolManagement school)
        {
            school.DisplayByRole("Student");
        }
        public static void VIEWTEACHERS(SchoolManagement school)
        {
            school.DisplayByRole("Teacher");
        }
        public static void VIEWCLASSES(SchoolManagement school)
        {
            school.DisplayByRole("Class");
        }
        public static void VIEWSUBJECTS(SchoolManagement school)
        {
            school.DisplayByRole("Subject");
        }
    }
} 