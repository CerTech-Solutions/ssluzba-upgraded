using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using CLI.Storage.Serialization;
using CLI.DAO;
using CLI.Console;

namespace CLI.Model;

public class Profesor : ISerializable, IAccess, IConsoleWR
{
    private int _idProf;

    public int Id 
    {
        get { return _idProf; } 
        set { _idProf = value; }
    }

    public string Ime { get; set; }

    public string Prezime { get; set; }

    public DateTime DatumRodjenja { get; set; }

    public int IdAdr { get; set; }

    public string BrojTelefona { get; set; }

    public string Email { get; set; }

    public string BrojLicneKarte { get; set; }

    public string Zvanje { get; set; }

    public int GodinaStaza { get; set; }

    public List<int> IdPred { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(), Ime, Prezime, DatumRodjenja.ToString(),
            IdAdr.ToString(), BrojTelefona, Email, BrojLicneKarte,
            Zvanje, GodinaStaza.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Ime = values[1];
        Prezime = values[2];
        DatumRodjenja = DateTime.Parse(values[3]);
        IdAdr = int.Parse(values[4]);
        BrojTelefona = values[5];
        Email = values[6];
        BrojLicneKarte = values[7];
        Zvanje = values[8];
        GodinaStaza = int.Parse(values[9]);
    }

    //za Console Write
    public string GenerateClassHeader()
    {
        return "Profesori: \n" + $"{"ID",6} | {"Ime",20} | {"Prezime",20} | {"DatumRodjenja",13} | {"IdAdr",6} | {"BrojTelefona",12} | {"Email", 20} | {"BrojLicneKarte",20} | {"Zvanje",20} | {"GodinaStaza",12} |";
    }

    public override string ToString()
    {
        return $"{Id,6} | {Ime,20} | {Prezime,20} | {DatumRodjenja.ToString("dd/MM/yyyy"),13} | {IdAdr,6} | {BrojTelefona,12} | {Email,20} | {BrojLicneKarte,20} | {Zvanje,20} | {GodinaStaza,12} |";
    }
}

