using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Storage.Serialization;

namespace CLI.Model;
public class Ocena : ISerializable
{
    public int IdOcn { get; set; }

    public int IdStudentPolozio { get; set; }
    public int IdPredmet { get; set; }
    public int OcenaBr { get; set; }
    public DateTime DatumPolaganja { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            IdOcn.ToString(),
            IdPredmet.ToString(),
            OcenaBr.ToString(),
            DatumPolaganja.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        IdOcn = int.Parse(values[0]);
        IdPredmet = int.Parse(values[1]);
        OcenaBr = int.Parse(values[3]);
        DatumPolaganja = DateTime.Parse(values[4]);
    }
}

