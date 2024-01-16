using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using CLI.Storage.Serialization;
using CLI.DAO;
using CLI.Console;

namespace CLI.Model;

public class Professor : ISerializable, IAccess<Professor>, IConsoleWriteRead
{
    private int _idProf;

    public Professor()
    {
        Subjects = new List<Subject>();
        Adresa = new Address();
    }

    public Professor(int idProf, string name, string surname, DateOnly birthDate, Address address, string phoneNumber, string email, string idNumber, string title, int serviceYears)
    {
        Id = idProf;
        Name = name;
        Surname = surname;
        BirthDate = birthDate;
        Adresa = address;
        PhoneNumber = phoneNumber;
        Email = email;
        IdNumber = idNumber;
        Title = title;
        ServiceYears = serviceYears;
        Subjects = new List<Subject>();
    }

    public int Id
    {
        get { return _idProf; }
        set { _idProf = value; }
    }

    public string Name { get; set; }

    public string Surname { get; set; }

    public DateOnly BirthDate { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string IdNumber { get; set; }

    public string Title { get; set; }

    public int ServiceYears { get; set; }

    public List<Subject> Subjects { get; set; }

    public Address Adresa { get; set; }

    public void Copy(Professor obj)
    {
        Id = obj.Id;
        Name = obj.Name;
        Surname = obj.Surname;
        BirthDate = obj.BirthDate;
        PhoneNumber = obj.PhoneNumber;
        Email = obj.Email;
        IdNumber = obj.IdNumber;
        Title = obj.Title;
        ServiceYears = obj.ServiceYears;
        Adresa.Copy(obj.Adresa);
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(), Name, Surname, BirthDate.ToString("yyyy-MM-dd"),
            PhoneNumber, Email, IdNumber,
            Title, ServiceYears.ToString()
        };

        string[] result = csvValues.Concat(Adresa.ToCSV()).ToArray();
        return result;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Name = values[1];
        Surname = values[2];
        BirthDate = DateOnly.ParseExact(values[3], "yyyy-MM-dd", null);
        PhoneNumber = values[4];
        Email = values[5];
        IdNumber = values[6];
        Title = values[7];
        ServiceYears = int.Parse(values[8]);
        Adresa.FromCSV(values[9..]);
    }

	public string GenerateClassHeader()
    {
        return "Professors: \n" + $"{"ID",6} | {"Name",12} | {"Surname",12} | {"BirthDate",13} | {"PhoneNumber",12} | {"Email",20} | {"IdNumber",16} | {"Title",8} | {"ServiceYears",12} |" + Adresa.GenerateClassHeader();
    }

    public override string ToString()
    {
        string str = $"{Id,6} | {Name,12} | {Surname,12} | {BirthDate.ToString("dd/MM/yyyy"),13} | {PhoneNumber,12} | {Email,20} | {IdNumber,16} | {Title,8} | {ServiceYears,12} |" + Adresa.ToString();
        str += "\n\t* Teaches subjects: \n";
        foreach (Subject p in Subjects)
        {
            str += $"\t\t{p.Code} {p.Name}\n";
        }
        return str;
    }
}
