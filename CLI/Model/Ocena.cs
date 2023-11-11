using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Ocena : ISerializable, IAccess<Ocena>, IConsoleWriteRead
{
    private int _idOcn;

    public Ocena()
    {
        Student = new Student();
        Predmet = new Predmet();
    }

    public Ocena(int id, Student student, Predmet predmet, int ocenaBroj, DateTime datumPolaganja)
    {
        Id = id;
        Student = student;
        Predmet = predmet;
        OcenaBroj = ocenaBroj;  
        DatumPolaganja = datumPolaganja;
    }

    public int Id
    {
        get { return _idOcn; }
        set { _idOcn = value; }
    }

    public Student Student { get; set; }

    public Predmet Predmet { get; set; }

    public int OcenaBroj { get; set; }

    public DateTime DatumPolaganja { get; set; }

    public void Copy(Ocena obj)
    {
        Id = obj.Id;
        Student.Copy(obj.Student);
        Predmet.Copy(obj.Predmet);
        OcenaBroj = obj.OcenaBroj;
        DatumPolaganja = obj.DatumPolaganja;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Student.Id.ToString(),
            Predmet.Id.ToString(),
            OcenaBroj.ToString(),
            DatumPolaganja.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Student.Id = int.Parse(values[1]);
        Predmet.Id = int.Parse(values[2]);
        OcenaBroj = int.Parse(values[3]);
        DatumPolaganja = DateTime.Parse(values[4]);
    }

    public string GenerateClassHeader()
    {
        return "Ocene: \n" + $"{"ID",6} | {"Student",25} | {"Predmet",20} | {"Ocena",5} | {"DatumPolaganja",20} |";
    }

    public override string ToString()
    {
        return $"{Id,6} | {Student.Ime,12} {Student.Prezime,12} | {Predmet.Naziv,20} | {OcenaBroj,5} | {DatumPolaganja,20} |";
    }
}
