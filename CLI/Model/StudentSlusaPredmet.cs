using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public enum PolozenPredmetEnum
{
    polozio,
    nijePolozio
}

public class StudentSlusaPredmet : ISerializable, IAccess<StudentSlusaPredmet>, IConsoleWriteRead
{
    public StudentSlusaPredmet() { }

    public StudentSlusaPredmet(int id, int idStud, int idPred, PolozenPredmetEnum status)
    {
        Id = id;
        IdPred = idPred;
        IdStud = idStud;
        Status = status;
    }

    public int Id { get; set; }

    public int IdPred { get; set; }

    public int IdStud { get; set; }

    public PolozenPredmetEnum Status {  get; set; }

    public void Copy(StudentSlusaPredmet obj)
    {
        Id = obj.Id;
        IdPred = obj.IdPred;
        IdStud = obj.IdStud;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
			Id.ToString(),
            IdStud.ToString(),
            IdPred.ToString(),
            Status.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        IdStud = int.Parse(values[1]);
        IdPred = int.Parse(values[2]);
		Status = Enum.Parse<PolozenPredmetEnum>(values[3]);
    }

    public string GenerateClassHeader()
    {
        return "Adrese: \n" + $"{"ID",6} | {"IdPred",6} | {"IdStud",6} |";
    }

    public override string ToString()
    {
        return $"{Id,6} | {IdPred,6} | {IdStud,6} |";
    }
}
