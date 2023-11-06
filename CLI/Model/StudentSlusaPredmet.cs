using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Storage.Serialization;

namespace CLI.Model;

public enum PolozenPredmetEnum
{
    polozio,
    nijePolozio
}

public class StudentSlusaPredmet : ISerializable
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
}
