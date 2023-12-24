using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class ProfessorTeachesSubject : ISerializable, IAccess<ProfessorTeachesSubject>, IConsoleWriteRead
{
    public ProfessorTeachesSubject() { }

    public ProfessorTeachesSubject(int id, int idProf, int idPred)
    {
        Id = id;
        IdProf = idProf;
        IdSub = idPred;
    }

    public int Id { get; set; }

    public int IdProf { get; set; }

    public int IdSub { get; set; }

    public void Copy(ProfessorTeachesSubject obj)
    {
        obj.Id = Id;
        obj.IdProf = IdProf;
        obj.IdSub = IdSub;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            IdProf.ToString(),
            IdSub.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        IdProf = int.Parse(values[1]);
        IdSub = int.Parse(values[2]);
    }

    public string GenerateClassHeader()
    {
        return "Professors teach subjects: \n" + $"{"Id",6} | {"IdProf",6} | {"IdSub",6} |";
    }

    public override string ToString()
    {
        return $"{Id,6} | {IdProf,6} | {IdSub,6} |";
    }
}
