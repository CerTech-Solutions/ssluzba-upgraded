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

public class Student : ISerializable, IAccess<Student>, IConsoleWriteRead
{
    private int _idStud;

    public Student() 
    {
        Adresa = new Adresa();
        Indeks = new Indeks();
        PolozeniPredmeti = new List<Predmet>();
        NepolozeniPredmeti = new List<Predmet>();
    }

    public Student(int idStud, string ime, string prezime, DateOnly datumRodjenja, Adresa adresa,
        string brojTelefona, string email, int trenutnaGodina, StatusEnum status, double prosecnaOcena, Indeks indeks)
    {
        Id = idStud;
        Ime = ime;
        Prezime = prezime;
        DatumRodjenja = datumRodjenja;
        Adresa = adresa;
        BrojTelefona = brojTelefona;
        Email = email;
        TrenutnaGodina = trenutnaGodina;
        Status = status;
        ProsecnaOcena = prosecnaOcena;
        Indeks = indeks;

        PolozeniPredmeti = new List<Predmet>();
        NepolozeniPredmeti = new List<Predmet>();
    }

    public int Id
    {
        get { return _idStud; }
        set { _idStud = value; }
    }

    public string Ime { get; set; }

    public string Prezime { get; set; }

    public DateOnly DatumRodjenja { get; set; }

    public string BrojTelefona { get; set; }

    public string Email { get; set; }

    public int TrenutnaGodina { get; set; }

    public StatusEnum Status { get; set; }

    public List<Predmet> PolozeniPredmeti { get; set; }

    public List<Predmet> NepolozeniPredmeti { get; set; }

    public double ProsecnaOcena {  get; set; }

    public Indeks Indeks { get; set; }

    public Adresa Adresa { get; set; }

    public void Copy(Student obj)
    {
        Id = obj.Id;
        Ime = obj.Ime;
        Prezime = obj.Prezime;
        DatumRodjenja = obj.DatumRodjenja;
        BrojTelefona = obj.BrojTelefona;
        Email = obj.Email;
        TrenutnaGodina = obj.TrenutnaGodina;
        Status = obj.Status;
        ProsecnaOcena = obj.ProsecnaOcena;
        Indeks.Copy(obj.Indeks);
        Adresa.Copy(obj.Adresa);
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(), Ime, Prezime, DatumRodjenja.ToString("dd-MM-yyyy"),
            BrojTelefona.ToString(), Email.ToString(), TrenutnaGodina.ToString(),
            Status.ToString(), ProsecnaOcena.ToString()
        };

        csvValues = csvValues.Concat(Indeks.ToCSV()).ToArray();
        csvValues = csvValues.Concat(Adresa.ToCSV()).ToArray();
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Ime = values[1];
        Prezime = values[2];
        DatumRodjenja = DateOnly.Parse(values[3]);
        BrojTelefona = values[4];
        Email = values[5];
        TrenutnaGodina = int.Parse(values[6]);
		Status = Enum.Parse<StatusEnum>(values[7]);
        ProsecnaOcena = double.Parse(values[8]);

        Indeks.FromCSV(values[9..12]);
        Adresa.FromCSV(values[12..]);

    }

    public string GenerateClassHeader()
    {
        return "Studenti: \n" + $@"{"ID",6} | {"Ime",10} | {"Prezime",15} | {"DatumRodjenja",13} | {"BrojTelefona",12} | {"Email",30} | {"TrenutnaGodina",14} | {"Status",6} | " + Indeks.GenerateClassHeader();
    }

    public override string ToString()
    {
        string str = $"{Id,6} | {Ime,10} | {Prezime,15} | {DatumRodjenja.ToString("dd/MM/yyyy"),13} | {BrojTelefona,12} | {Email,30} | {TrenutnaGodina,14} | {Status,6} | " + Indeks.ToString();
        str += "\n\t* Nepolozeni predmeti: \n";
        foreach(Predmet np in NepolozeniPredmeti)
        {
            str += $"\t\t{np.Sifra} {np.Naziv}\n";
        }
        str += "\n\t* Polozeni predmeti: \n";
        foreach (Predmet np in PolozeniPredmeti)
        {
            str += $"\t\t{np.Sifra} {np.Naziv}\n";
        }
        return str;
    }
}
