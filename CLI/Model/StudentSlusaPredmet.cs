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

public class StudentSlusaPredmet : ISerializable, IAccess, IConsoleWriteRead
{
    public int Id { get; set; }

    public int IdPred { get; set; } 

    public int IdStud { get; set; }

    public PolozenPredmetEnum Status {  get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            IdPred.ToString(),
            IdStud.ToString(),
            Status.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        IdPred = int.Parse(values[0]);
        IdStud = int.Parse(values[1]);

        if (values[2] == "polozio")
            Status = PolozenPredmetEnum.polozio;
        else
            Status = PolozenPredmetEnum.nijePolozio;
    }

    //za Console Write
    public string GenerateClassHeader()
    {
        return "Adrese: \n" + $"{"ID",6} | {"IdPred",6} | {"IdStud",6} |";
    }

    public override string ToString()
    {
        return $"{Id,6} | {IdPred,6} | {IdStud,6} |";
    }
}
