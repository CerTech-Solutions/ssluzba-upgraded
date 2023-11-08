using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model;

public class PredajeNaKatedri : ISerializable, IAccess, IConsoleWR
{
    public int Id { get; set; }

    public int IdProf {  get; set; }

    public int IdKat { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            IdProf.ToString(),
            IdKat.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        IdProf = int.Parse(values[0]);
        IdKat = int.Parse(values[1]);
    }

    //za Console Write
    public string GenerateClassHeader()
    {
        return "Predaje na Katedri: \n" + $"{"ID",6} | {"IdProf",10} | {"IdKat",10} |";
    }

    public override string ToString()
    {
        return $"{Id,6} | {IdProf,10} | {IdKat,10} |";
    }
}
