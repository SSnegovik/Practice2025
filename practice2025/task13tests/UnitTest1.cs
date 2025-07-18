using Xunit;
using System;
using System.Text.Json;

public class Tests
{
    [Fact]
    public void SerializeStudent_Tests()
    {
        var student = new Student
        {
            FirstName = "Ivan",
            LastName = "Ivanov",
            BirthDate = new DateTime(2000, 5, 15),
            Grades = new List<Subject>
            {
                new Subject { Name = "Math", Grade = 5 }
            }
        };

        string json = Program.SerializeStudent(student);

        Assert.Contains("Ivan", json);
        Assert.Contains("Ivanov", json);
        Assert.Contains("Math", json);
    }

    [Fact]
    public void DeserializeStudent_Tests()
    {
        string studentJSON = """
        {
            "FirstName": "Petr",
            "LastName": "Petrov",
            "BirthDate": "2000-05-15T00:00:00",
            "Grades": [
                { "Name": "Chemistry", "Grade": 4 }
            ]
        }
        """;

        var student = Program.DeserializeStudent(studentJSON);

        Assert.NotNull(student);
        Assert.Equal("Petr", student.FirstName);
        Assert.Single(student.Grades);
    }
}