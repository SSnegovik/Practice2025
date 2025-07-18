using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable disable

public class Subject
{
    public string Name { get; set; }
    public int Grade { get; set; }
}

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public List<Subject> Grades { get; set; }
}

public static class Program
{
    private static readonly JsonSerializerOptions jsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true
    };

    static void Main(string[] args)
    {
        var student = new Student
        {
            FirstName = "Ivan",
            LastName = "Ivanov",
            BirthDate = new DateTime(2000, 5, 15),
            Grades = new List<Subject>
            {
                new Subject { Name = "Math", Grade = 5 },
                new Subject { Name = "Chemistry", Grade = 4 },
                new Subject { Name = "History", Grade = 3 }
            }
        };

        string jsonString = SerializeStudent(student);

        string filePath = "student.json";

        SaveToFile(jsonString, filePath);
        Console.WriteLine($"\nJSON сохранен в файл: {filePath}");

        string loadedJson = LoadFromFile(filePath);
        Student deserializedStudent = DeserializeStudent(loadedJson);
        Console.ReadKey();
    }

    public static string SerializeStudent(Student student)
    {
        return JsonSerializer.Serialize(student, jsonOptions);
    }

    public static Student DeserializeStudent(string json)
    {
        var student = JsonSerializer.Deserialize<Student>(json, jsonOptions);

        if (student == null)
        {
            Console.WriteLine("Ошибка: десериализованный объект null");
            return null;
        }

        if (string.IsNullOrWhiteSpace(student.FirstName))
        {
            Console.WriteLine("Ошибка: имя студента не может быть пустым");
            return null;
        }

        if (string.IsNullOrWhiteSpace(student.LastName))
        {
            Console.WriteLine("Ошибка: фамилия студента не может быть пустой");
            return null;
        }

        if (student.BirthDate > DateTime.Now)
        {
            Console.WriteLine("Ошибка: дата рождения не может быть в будущем");
            return null;
        }

        return student;
    }

    static void SaveToFile(string json, string filePath)
    {
        File.WriteAllText(filePath, json);
    }

    static string LoadFromFile(string filePath)
    {
        return File.ReadAllText(filePath);
    }
}