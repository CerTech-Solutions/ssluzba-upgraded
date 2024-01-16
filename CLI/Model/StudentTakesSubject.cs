using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class StudentTakesSubject : ISerializable, IAccess<StudentTakesSubject>, IConsoleWriteRead
{
    public StudentTakesSubject() { }

    public StudentTakesSubject(int id, int idStud, int idSub)
    {
        Id = id;
        IdStud = idStud;
        IdSub = idSub;
    }

    public int Id { get; set; }

    public int IdSub { get; set; }

    public int IdStud { get; set; }

    public void Copy(StudentTakesSubject obj)
    {
        Id = obj.Id;
        IdStud = obj.IdStud;
        IdSub = obj.IdSub;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
			Id.ToString(),
            IdStud.ToString(),
            IdSub.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        IdStud = int.Parse(values[1]);
        IdSub = int.Parse(values[2]);
    }

    public string GenerateClassHeader()
    {
        return "Students take subjects: \n" + $"| {"ID",6} | {"IdSub",6} | {"IdStud",6} |";
    }

    public override string ToString()
    {
        return $"| {Id,6} | {IdSub,6} | {IdStud,6} |";
    }
}
