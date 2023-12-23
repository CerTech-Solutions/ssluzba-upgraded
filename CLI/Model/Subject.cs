using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model;

public enum SemesterEnum
{
    summer,
    winter
}

public class Subject : ISerializable, IAccess<Subject>, IConsoleWriteRead
{
    private int _idSub;

    public Subject()
    {
        Professor = new Professor();
        StudentiPolozili = new List<Student>();
        StudentiNisuPolozili = new List<Student>();
    }

    public Subject(int idSub, string code, string name, SemesterEnum semester, int yearOfStudy, Professor p, int ects)
    {
        Id = idSub;
        Code = code;
        Name = name;
        Semester = semester;
        YearOfStudy = yearOfStudy;
        Ects = ects;
        Professor = p;
        StudentiPolozili = new List<Student>();
        StudentiNisuPolozili = new List<Student>();
    }

    public int Id
    {
        get { return _idSub; }
        set { _idSub = value; }
    }

    public string Code { get; set; }

    public string Name { get; set; }

    public SemesterEnum Semester { get; set; }

    public int YearOfStudy { get; set; }

    public Professor Professor { get; set; }

    public int Ects { get; set; }

    public List<Student> StudentiPolozili { get; set; }

    public List<Student> StudentiNisuPolozili { get; set; }

    public void Copy(Subject obj)
    {
        Id = obj.Id;
        Code = obj.Code;
        Name = obj.Name;
        Semester = obj.Semester;
        YearOfStudy= obj.YearOfStudy;
        Ects = obj.Ects;
        Professor = obj.Professor;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Code,
            Name,
            Semester.ToString(),
            YearOfStudy.ToString(),
            Ects.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Code = values[1];
        Name = values[2];
        Semester = Enum.Parse<SemesterEnum>(values[3]);
        YearOfStudy = int.Parse(values[4]);
        Ects = int.Parse(values[5]);
    }

    public string GenerateClassHeader()
    {
        return "Subjects: \n" + $"{"ID",6} | {"Code",8} | {"Name",20} | {"Semester",8} | {"YearOfStudy",11} | {"Ects",6} | {"Professor",32} |";
    }

    public override string ToString()
    {
        string str = $"{Id,6} | {Code,8} | {Name,20} | {Semester,8} | {YearOfStudy,11} | {Ects,6} | {Professor.Name + " " + Professor.Surname,32} |";
        str += "\n\t* Students that passed: \n";
        foreach (Student p in StudentiPolozili)
        {
            str += $"\t\t{p.Id} {p.Index.ToString()} {p.Name} {p.Surname}\n";
        }
        str += "\n\t* Students that didn't pass: \n";
        foreach (Student np in StudentiNisuPolozili)
        {
            str += $"\t\t{np.Id} {np.Index.ToString()} {np.Name} {np.Surname}\n";
        }
        return str + "\n";
    }
}
