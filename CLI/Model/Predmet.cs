using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model;

public enum SemestarEnum
{
    letnji,
    zimski
}

public class Predmet : ISerializable, IAccess<Predmet>, IConsoleWriteRead
{
    private int _idPred;

    public Predmet()
    {
        Profesor = new Profesor();
        StudentiPolozili = new List<Student>();
        StudentiNisuPolozili = new List<Student>();
    }

    public Predmet(int idPred, string sifra, string naziv, SemestarEnum semestar, int godStudija, int idProfesor, int espb)
    {
        Id = idPred;
        Sifra = sifra;
        Naziv = naziv;
        Semestar = semestar;
        GodStudija = godStudija;
        Espb = espb;
    }

    public int Id
    {
        get { return _idPred; }
        set { _idPred = value; }
    }

    public string Sifra { get; set; }

    public string Naziv { get; set; }

    public SemestarEnum Semestar { get; set; }

    public int GodStudija { get; set; }

    public Profesor Profesor { get; set; }

    public int Espb { get; set; }

    public List<Student> StudentiPolozili { get; set; }

    public List<Student> StudentiNisuPolozili { get; set; }

    public void Copy(Predmet obj)
    {
        Id = obj.Id;
        Sifra = obj.Sifra;
        Naziv = obj.Naziv;
        Semestar = obj.Semestar;
        GodStudija= obj.GodStudija;
        Espb = obj.Espb;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Sifra,
            Naziv,
            Semestar.ToString(),
            GodStudija.ToString(),
            Espb.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Sifra = values[1];
        Naziv = values[2];
        Semestar = Enum.Parse<SemestarEnum>(values[3]);
        GodStudija = int.Parse(values[4]);
        Espb = int.Parse(values[5]);
    }

    public string GenerateClassHeader()
    {
        return "Predmeti: \n" + $"{"ID",6} | {"Sifra",8} | {"Naziv",20} | {"Semestar",8} | {"GodStudija",8} | {"Espb",6}" + Profesor.GenerateClassHeader();
    }

    public override string ToString()
    {
        return $"{Id,6} | {Sifra,8} | {Naziv,20} | {Semestar,8} | {GodStudija,8} |  {Espb, 6} |" + Profesor.ToString();
    }
}
