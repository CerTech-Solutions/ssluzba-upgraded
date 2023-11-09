using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public enum PolozenPredmetEnum
{
    polozio,
    nijePolozio
}

public class StudentSlusaPredmet : ISerializable, IAccess
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

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            IdStud.ToString(),
            IdPred.ToString(),
            Status.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        IdStud = int.Parse(values[0]);
        IdPred = int.Parse(values[1]);

        if (values[2] == "polozio")
            Status = PolozenPredmetEnum.polozio;
        else
            Status = PolozenPredmetEnum.nijePolozio;
    }
}
