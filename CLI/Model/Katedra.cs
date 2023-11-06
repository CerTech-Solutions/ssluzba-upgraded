using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Katedra : ISerializable
{
    public int IdKat { get; set; }  

    public string Sifra { get; set; }
    public string Naziv { get; set; }
    public int IdSefKatedre { get; set; }
    public List<int> IdProfesori = new List<int>();
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            IdKat.ToString(),
            Sifra,
            Naziv,
            IdSefKatedre.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        IdKat = int.Parse(values[0]);
        Sifra = values[1];
        IdSefKatedre = int.Parse(values[2]);    
    }
}

