using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace task13
{
    public class Subject
    {
        public required string Name { get; set; }
        public int Grade { get; set; }
    }

    public class Student
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public required List<Subject> Grades { get; set; }
    }
}