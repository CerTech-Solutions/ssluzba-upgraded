using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Department : ISerializable, IAccess<Department>, IConsoleWriteRead
{
    private int _idDep;

    public Department()
    {
        Professors = new List<Professor>();
        Chief = new Professor();
    }

    public Department(int idKat, string code, string name, Professor chief)
    {
        Id = idKat;
        Code = code;
        Name = name;
        Chief = chief;
    }

    public int Id
    {
        get { return _idDep; }
        set { _idDep = value; }
    }

    public string Code { get; set; }

    public string Name { get; set; }

    public Professor Chief { get; set; }

    public List<Professor> Professors { get; set; }

    public void Copy(Department obj)
    {
        Id = obj.Id;
        Code = obj.Code;
        Name = obj.Name;
        Chief.Copy(obj.Chief);
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Code,
            Name,
            Chief.Id.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Code = values[1];
        Name = values[2];
        Chief.Id = int.Parse(values[3]);
    }

    public string GenerateClassHeader()
    {
        return "Departments: \n" + $"{"ID",6} | {"Code",5} | {"Name",20} |";
    }

    public override string ToString()
    {
        string str = $"{Id,6} | {Code,5} | {Name,20} |\n";
        str += $"\t* Chief:\n\t\t{Chief.Id,3} {Chief.Name} {Chief.Surname}\n\n";
        str += $"\t* Professors: \n";

        foreach(Professor prof in Professors)
        {
            str += $"\t\t{prof.Id, 3} {prof.Name} {prof.Surname}\n";
        }

        return str;
    }
}
