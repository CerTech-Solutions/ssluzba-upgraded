using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Grade : ISerializable, IAccess<Grade>, IConsoleWriteRead
{
    private int _idGrade;

    public Grade()
    {
        Student = new Student();
        Subject = new Subject();
    }

    public Grade(int id, Student student, Subject subject, int gradeValue, DateOnly passDate)
    {
        Id = id;
        Student = student;
        Subject = subject;
        GradeValue = gradeValue;  
        PassDate = passDate;
    }

    public int Id
    {
        get { return _idGrade; }
        set { _idGrade = value; }
    }

    public Student Student { get; set; }

    public Subject Subject { get; set; }

    public int GradeValue { get; set; }

    public DateOnly PassDate { get; set; }

    public void Copy(Grade obj)
    {
        Id = obj.Id;
        Student.Copy(obj.Student);
        Subject.Copy(obj.Subject);
        GradeValue = obj.GradeValue;
        PassDate = obj.PassDate;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Student.Id.ToString(),
            Subject.Id.ToString(),
            GradeValue.ToString(),
            PassDate.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Student.Id = int.Parse(values[1]);
        Subject.Id = int.Parse(values[2]);
        GradeValue = int.Parse(values[3]);
        PassDate = DateOnly.Parse(values[4]);
    }

    public string GenerateClassHeader()
    {
        return "Grades: \n" + $"{"ID",6} | {"Student",25} | {"Subject",20} | {"Grade",5} | {"PassDate",20} |";
    }

    public override string ToString()
    {
        return $"{Id,6} | {Student.Name + " " + Student.Surname,25} | {Subject.Name,20} | {GradeValue,5} | {PassDate,20} |";
    }
}
