using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;
namespace CLI.Model;

public enum StatusEnum
{
    B,                  // budzet
    S                   // samofinansiranje
}

public class Student : ISerializable, IAccess, IConsoleWriteRead
{
    private int _idStud;

    public int Id
    {
        get { return _idStud; }
        set { _idStud = value; }
    }

    public string Ime { get; set; }

    public string Prezime { get; set; }

    public DateTime DatumRodjenja { get; set; }

    public int IdAdr { get; set; }

    public string BrojTelefona { get; set; }

    public string Email { get; set; }

    public int IdInd { get; set; }

    public int TrenutnaGodina { get; set; }

    public StatusEnum Status { get; set; }

    public List<int> IdOcnPolozeni { get; set; }

    public List<int> IdOcnNepolozeni { get; set; }

    public double ProsecnaOcena {  get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(), Ime, Prezime, DatumRodjenja.ToString(),
            IdAdr.ToString(), BrojTelefona, Email,
            IdInd.ToString(), TrenutnaGodina.ToString(),
            Status.ToString(), ProsecnaOcena.ToString()
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
        IdInd = int.Parse(values[7]);
        TrenutnaGodina = int.Parse(values[8]);

        if (values[9] == "B")
            Status = StatusEnum.B;
        else
            Status = StatusEnum.S;

        ProsecnaOcena = double.Parse(values[10]);
    }

    //za Console Write
    public string GenerateClassHeader()
    {
        return "Adrese: \n" + $@"{"Id",6} | {"Ime",10} | {"Prezime",15} | {"DatumRodjenja",13} | {"IdAdr",6} | {"BrojTelefona",12} | {"Email",30} | {"IdInd",6} | {"TrenutnaGodina",14} | {"Status",6} |";
    }

    public override string ToString()
    {
        return $@"{Id,6} | {Ime,10} | {Prezime,15} | {DatumRodjenja.ToString("dd/MM/yyyy"),13} | {IdAdr,6} | {BrojTelefona,12} | {Email,30} | {IdInd,6} | {TrenutnaGodina,14} | {Status,6} |";
    }
}

