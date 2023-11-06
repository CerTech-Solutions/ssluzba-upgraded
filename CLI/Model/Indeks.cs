using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Indeks : ISerializable
{
    public int IdInd { get; set; }

    public string OznakaSmera { get; set; }
    public int BrUpisa { get; set; }   
    public int GodUpisa { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            IdInd.ToString(),
            OznakaSmera,
            BrUpisa.ToString(),
            GodUpisa.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        IdInd = int.Parse(values[0]);
        OznakaSmera = values[1];
        BrUpisa = int.Parse(values[2]);
        GodUpisa = int.Parse(values[3]);
    }
}
