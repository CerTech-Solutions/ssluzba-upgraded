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

    public Katedra() { }

    public Katedra(int idKat, string sifra, string naziv, int idSefKatedre)
    {
        Id = idKat;
        Sifra = sifra;
        Naziv = naziv;
        IdSefKatedre = idSefKatedre;
    }

    public int Id
    {
        get { return _idKat; }
        set { _idKat = value; }
    }

    public string Sifra { get; set; }

    public string Naziv { get; set; }

    public int IdSefKatedre { get; set; }

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
        Naziv = values[2];
        IdSefKatedre = int.Parse(values[3]);
    }

    public string GenerateClassHeader()
    {
        return "Katedre: \n" + $"{"Id",6} | {"Sifra",20} | {"Naziv",20} | {"IdSefKatedre",12} |";
    }

    public override string ToString()
    {
        return $"{Id,6} | {Sifra,20} |  {Naziv,20} | {IdSefKatedre,12} |";
    }
}
