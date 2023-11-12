using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model;

public class ProfesorRadiNaKatedri : ISerializable, IAccess<ProfesorRadiNaKatedri>, IConsoleWriteRead
{
    public ProfesorRadiNaKatedri() { }

    public ProfesorRadiNaKatedri(int id, int idProf, int idKat)
    {
        Id = id;
        IdProf = idProf;
        IdKat = idKat;
    }

    public int Id { get; set; }

    public int IdProf {  get; set; }

    public int IdKat { get; set; }

    public void Copy(ProfesorRadiNaKatedri obj)
    {
        obj.Id = Id;
        obj.IdProf = IdProf;
        obj.IdKat = IdKat;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            IdProf.ToString(),
            IdKat.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        IdProf = int.Parse(values[1]);
        IdKat = int.Parse(values[2]);
    }

    public string GenerateClassHeader()
    {
        return "Predaje na Katedri: \n" + $"{"ID",6} | {"IdProf",10} | {"IdKat",10} |";
    }

    public override string ToString()
    {
        return $"{Id,6} | {IdProf,10} | {IdKat,10} |";
    }
}
