using System.Collections.Generic;
using Xunit;

public class StudentServiceTests
{
    private readonly List<Student> _testStudents;
    private readonly StudentService _service;

    public StudentServiceTests()
    {
        _testStudents = new List<Student>
        {
            new() { Name = "Иван", Faculty = "ФИТ", Grades = new List<int> { 5, 4, 5 } },
            new() { Name = "Анна", Faculty = "ФИТ", Grades = new List<int> { 3, 4, 3 } },
            new() { Name = "Петр", Faculty = "Экономика", Grades = new List<int> { 5, 5, 5 } },
            new() { Name = "Мария", Faculty = "Экономика", Grades = new List<int> { 4, 3, 4 } }
        };
        _service = new StudentService(_testStudents);
    }

    [Fact]
    public void GetStudentsByFaculty_ReturnsCorrectStudents()
    {
        var result = _service.GetStudentsByFaculty("ФИТ").ToList();

        Assert.Equal(2, result.Count);
        Assert.All(result, s => Assert.Equal("ФИТ", s.Faculty));
    }

    [Fact]
    public void GetStudentsWithMinAverageGrade_ReturnsCorrectStudents()
    {
        const double minAverage = 4.5;

        var result = _service.GetStudentsWithMinAverageGrade(minAverage).ToList();

        Assert.Equal(2, result.Count);
        Assert.All(result, s => Assert.True(s.Grades.Average() >= minAverage));
        Assert.Contains(result, s => s.Name == "Иван");
        Assert.Contains(result, s => s.Name == "Петр");
    }

    [Fact]
    public void GetStudentsOrderedByName_ReturnsCorrectlyOrderedList()
    {
        var result = _service.GetStudentsOrderedByName().ToList();

        Assert.Equal(4, result.Count);
        Assert.Equal("Анна", result[0].Name);
        Assert.Equal("Иван", result[1].Name);
        Assert.Equal("Мария", result[2].Name);
        Assert.Equal("Петр", result[3].Name);
    }

    [Fact]
    public void GroupStudentsByFaculty_ReturnsCorrectGroups()
    {
        var result = _service.GroupStudentsByFaculty().ToList();

        var fitGroup = result.First(g => g.Key == "ФИТ");
        Assert.Equal(2, fitGroup.Count());

        var economyGroup = result.First(g => g.Key == "Экономика");
        Assert.Equal(2, economyGroup.Count());
    }

    [Fact]
    public void GetFacultyWithHighestAverageGrade_ReturnsCorrectFaculty()
    {
        var result = _service.GetFacultyWithHighestAverageGrade();

        Assert.Equal("Экономика", result);
    }
}