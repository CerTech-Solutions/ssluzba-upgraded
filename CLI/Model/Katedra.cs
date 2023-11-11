using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Katedra : ISerializable, IAccess<Katedra>, IConsoleWriteRead
{
    private int _idKat;

    public Katedra()
    {
        Profesori = new List<Profesor>();
        SefKatedre = new Profesor();
    }

    public Katedra(int idKat, string sifra, string naziv, Profesor sefKatedre)
    {
        Id = idKat;
        Sifra = sifra;
        Naziv = naziv;
        SefKatedre = sefKatedre;
    }

    public int Id
    {
        get { return _idKat; }
        set { _idKat = value; }
    }

    public string Sifra { get; set; }

    public string Naziv { get; set; }

    public Profesor SefKatedre { get; set; }

    public List<Profesor> Profesori { get; set; }

    public void Copy(Katedra obj)
    {
        Id = obj.Id;
        Sifra = obj.Sifra;
        Naziv = obj.Naziv;
        SefKatedre.Copy(obj.SefKatedre);
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Sifra,
            Naziv,
            SefKatedre.Id.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Sifra = values[1];
        Naziv = values[2];
        SefKatedre.Id = int.Parse(values[3]);
    }

    public string GenerateClassHeader()
    {
        return "Katedre: \n" + $"{"ID",6} | {"Sifra",20} | {"Naziv",20} |";
    }

    public override string ToString()
    {
        string str = $"{Id,6} | {Sifra,20} | {Naziv,20} |\n";
        str += $"\tSef katedre: \n\t{SefKatedre.Id,3} | {SefKatedre.Ime, 10} {SefKatedre.Prezime, 10} \n";
        str += $"\tProfesori na katedri: \n";

        foreach(Profesor prof in Profesori)
        {
            str += $"\t {prof.Id, 3} | {prof.Ime,10} {prof.Prezime,10} \n";
        }

        return str;
    }
}
