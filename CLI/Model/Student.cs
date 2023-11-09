using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Storage.Serialization;
namespace CLI.Model;

public enum StatusEnum
{
    B,                  // budzet
    S                   // samofinansiranje
}

public class Student : ISerializable, IAccess
{
    private int _idStud;

    public Student() { }

    public Student(int idStud, string ime, string prezime, DateTime datumRodjenja, int idAdr, 
        string brojTelefona, string email, int idInd, int trenutnaGodina, StatusEnum status, double prosecnaOcena, Indeks indeks)
    {
        Id = idStud;
        Ime = ime;
        Prezime = prezime;
        DatumRodjenja = datumRodjenja;
        IdAdr = idAdr;
        BrojTelefona = brojTelefona;
        Email = email;
        IdInd = idInd;
        TrenutnaGodina = trenutnaGodina;
        Status = status;
        ProsecnaOcena = prosecnaOcena;
        Indeks = indeks;
    }

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

    //public List<int> IdOcnPolozeni { get; set; }

    //public List<int> IdOcnNepolozeni { get; set; }

    public double ProsecnaOcena {  get; set; }

    public Indeks Indeks { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(), Ime, Prezime, DatumRodjenja.ToString("dd-MM-yyyy"),
            IdAdr.ToString(), BrojTelefona, Email,
            IdInd.ToString(), TrenutnaGodina.ToString(),
            Status.ToString(), ProsecnaOcena.ToString()
        };

        string[] result = csvValues.Concat(Indeks.ToCSV()).ToArray();   
        return result;
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
        Status = Enum.Parse<StatusEnum>(values[9]);
        ProsecnaOcena = double.Parse(values[10]);

        Indeks = new Indeks();
        Indeks.FromCSV(values[11..]);
    }
}

