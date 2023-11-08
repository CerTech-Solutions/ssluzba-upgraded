using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Indeks : ISerializable, IAccess, IConsoleWR
{
    private int _idInd;

    public int Id
    {
        get { return _idInd; } 
        set { _idInd = value; } 
    }

    public string OznakaSmera { get; set; }

    public int BrUpisa { get; set; }   

    public int GodUpisa { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            OznakaSmera,
            BrUpisa.ToString(),
            GodUpisa.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        OznakaSmera = values[1];
        BrUpisa = int.Parse(values[2]);
        GodUpisa = int.Parse(values[3]);
    }

    //za Console Write
    public string GenerateClassHeader()
    {
        return "Indeksi: \n" + $"{"ID",6} | {"OznakaSmera",11} | {"BrUpisa",7} | {"Godina",6} |";
    }
    public override string ToString()
    {
        return $"{Id,6} | {OznakaSmera,11} | {BrUpisa,7} | {GodUpisa,6} |";
    }
}
