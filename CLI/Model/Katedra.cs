using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Katedra : ISerializable, IAccess
{
    private int _idKat;

    public Katedra() { }

    public Katedra(int idkat, string sifra, string naziv, int idSefKatedre) 
    {
        Id = idkat;
        Sifra = sifra; Naziv = naziv;
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

    //public List<int> IdProfesori = new List<int>();

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
}

