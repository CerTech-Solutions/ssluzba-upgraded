using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Profesor : ISerializable
{
    public int IdProf {  get; set; }
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
            IdProf.ToString(), Ime, Prezime, DatumRodjenja.ToString(),
            IdAdr.ToString(), BrojTelefona, Email, BrojLicneKarte,
            Zvanje, GodinaStaza.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        IdProf = int.Parse(values[0]);
        Ime = values[1];
        Prezime = values[2];
        DatumRodjenja = DateTime.Parse(values[3]);
        IdAdr = int.Parse(values[4]);
        BrojTelefona = values[5];
        Email = values[6];
        Zvanje = values[7];
        GodinaStaza = int.Parse(values[8]);
    }
}

