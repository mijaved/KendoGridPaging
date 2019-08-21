using System;
using System.Collections.Generic;
using System.Text;

namespace KendoGridPaging.Models
{
    public class Student
    {
        public int? Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Score { get; set; }
        public bool Active { get; set; }
    }

    public static class StudentRepository
    {
        public static List<Student> Students { get; private set; }
        static StudentRepository()
        {
            Func<char, string> rep4 = x =>
            {
                var result = new StringBuilder();
                for (var i = 0; i < 5; i++)
                {
                    result.Append(x);
                }
                return result.ToString();
            };

            Students = new List<Student>();
            var rand = new Random();
            for(var i = 1; i <= 1000; i++)
            {
                var m = (i - 1)%26;
                var lascii = m + 65;
                var student = new Student()
                {
                    Id = i,
                    LastName = rep4((char)lascii),
                    FirstName = " First Name " + i % 20,
                    Score = 60 + (int) Math.Round(rand.NextDouble()*40),
                    Active = i % 3 == 0
                };

                Students.Add(student);
            }
        }
    }
}