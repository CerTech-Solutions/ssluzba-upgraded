using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Katedra : ISerializable, IAccess, IConsoleWriteRead
{
    private int _idKat;

    public int Id
    {
        get { return _idKat; }
        set { _idKat = value; }
    }

    public string Sifra { get; set; }

    public string Naziv { get; set; }

    public int IdSefKatedre { get; set; }

    public List<int> IdProfesori = new List<int>();

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Sifra,
            Naziv,
            IdSefKatedre.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Sifra = values[1];
        IdSefKatedre = int.Parse(values[2]);    
    }

    //za Console Write
    public string GenerateClassHeader()
    {
        return "Katedre: \n" + $"{"Id",6} | {"Sifra",20} | {"Naziv",20} | {"IdSefKatedre",12} |";
    }

    public override string ToString()
    {
        return $"{Id,6} | {Sifra,20} |  {Naziv,20} | {IdSefKatedre,12} |";
    }
}

