using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

internal class ProfesorPredajePredmet : ISerializable, IAccess, IConsoleWriteRead
{
    public ProfesorPredajePredmet() { }

    public ProfesorPredajePredmet(int id, int idProf, int idPred)
    {
        Id = id;
        IdProf = idProf;
        IdPred = idPred;
    }

    public int Id { get; set; }

    public int IdProf { get; set; }

    public int IdPred { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            IdProf.ToString(),
            IdPred.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        IdProf = int.Parse(values[1]);
        IdPred = int.Parse(values[2]);
    }

    public string GenerateClassHeader()
    {
        return "Adrese: \n" + $"{"Id",6} | {"IdProf",6} | {"IdPred",6} |";
    }

    public override string ToString()
    {
        return $"{Id,6} | {IdProf,6} | {IdPred,6} |";
    }
}
