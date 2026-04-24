using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipg203HW
{
    internal class Program
    {
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

            protected int[] StudentsId { get { return students_id; } }
            protected int[] Classs { get { return classs; } }
            protected string[] Students { get { return Student; } }
            protected int[] Teachers { get { return Teacher; } }
            protected int[] Subjects { get { return Subject; } }

            public abstract void View();
            public abstract string GetRole();

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
        }
        static void Main(string[] args)
        {
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
            }
        }
    }
}