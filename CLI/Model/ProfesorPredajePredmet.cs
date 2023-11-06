using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Storage.Serialization;

namespace CLI.Model;

internal class ProfesorPredajePredmet : ISerializable
{
    public int IdProf { get; set; }
    public int IdPred { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            IdProf.ToString(),
            IdPred.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        IdProf = int.Parse(values[0]);
        IdPred = int.Parse(values[1]);
    }
}
