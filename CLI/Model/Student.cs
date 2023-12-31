using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;
namespace CLI.Model;

public enum StatusEnum
{
    B,
    S
}

public class Student : ISerializable, IAccess<Student>, IConsoleWriteRead
{
    private int _idStud;

    public Student() 
    {
        Address = new Address();
        Index = new Index();
        PassedSubjects = new List<Grade>();
        NotPassedSubjects = new List<Subject>();
    }

    public Student(int idStud, string name, string surname, DateOnly birthDate, Address address,
        string phoneNumber, string email, int currentYear, StatusEnum status, double gpa, Index index)
    {
        Id = idStud;
        Name = name;
        Surname = surname;
        BirthDate = birthDate;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        CurrentYear = currentYear;
        Status = status;
        GPA = gpa;
        Index = index;

        PassedSubjects = new List<Grade>();
        NotPassedSubjects = new List<Subject>();
    }

    public int Id
    {
        get { return _idStud; }
        set { _idStud = value; }
    }

    public string Name { get; set; }

    public string Surname { get; set; }

    public DateOnly BirthDate { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public int CurrentYear { get; set; }

    public StatusEnum Status { get; set; }

    public List<Grade> PassedSubjects { get; set; }

    public List<Subject> NotPassedSubjects { get; set; }

    public double GPA { get; set; }

    public Index Index { get; set; }

    public Address Address { get; set; }

    public void CalculateGPA()
    {
        GPA = 0.0;

        foreach (Grade o in PassedSubjects)
            GPA += o.GradeValue;

        if(PassedSubjects.Count > 0)
            GPA /= PassedSubjects.Count;
    }

    public void Copy(Student obj)
    {
        Id = obj.Id;
        Name = obj.Name;
        Surname = obj.Surname;
        BirthDate = obj.BirthDate;
        PhoneNumber = obj.PhoneNumber;
        Email = obj.Email;
        CurrentYear = obj.CurrentYear;
        Status = obj.Status;
        GPA = obj.GPA;
        Index.Copy(obj.Index);
        Address.Copy(obj.Address);
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(), Name, Surname, BirthDate.ToString("dd-MM-yyyy"),
            PhoneNumber.ToString(), Email.ToString(), CurrentYear.ToString(),
            Status.ToString(), GPA.ToString()
        };

        csvValues = csvValues.Concat(Index.ToCSV()).ToArray();
        csvValues = csvValues.Concat(Address.ToCSV()).ToArray();
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Name = values[1];
        Surname = values[2];
        BirthDate = DateOnly.Parse(values[3]);
        PhoneNumber = values[4];
        Email = values[5];
        CurrentYear = int.Parse(values[6]);
		Status = Enum.Parse<StatusEnum>(values[7]);
        GPA = double.Parse(values[8]);

        Index.FromCSV(values[9..12]);
        Address.FromCSV(values[12..]);

    }

    public string GenerateClassHeader()
    {
        return "Students: \n" +
               $@"{"ID",6} | {"Name",10} | {"Surname",15} | {"BirthDate",13} | {"PhoneNumber",12} | {"Email",30} | {"CurrentYear",14} | {"Status",6} | {"GPA",5} | " + Index.GenerateClassHeader() + Address.GenerateClassHeader();
    }

    public override string ToString()
    {
        string str = $"{Id,6} | {Name,10} | {Surname,15} | {BirthDate.ToString("dd/MM/yyyy"),13} | {PhoneNumber,12} | {Email,30} | {CurrentYear,14} | {Status,6} | {GPA,5:0.00} | " + Index.ToString() + Address.ToString();
        str += "\n\t* Not passed subjects: \n";
        foreach(Subject np in NotPassedSubjects)
        {
            str += $"\t\t{np.Code} {np.Name}\n";
        }
        str += "\n\t* Passed subjects: \n";
        foreach (Grade np in PassedSubjects)
        {
            str += $"\t\t{np.Subject.Code} {np.Subject.Name}\n";
        }
        return str;
    }
}
