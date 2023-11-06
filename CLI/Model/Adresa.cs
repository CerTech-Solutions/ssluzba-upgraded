using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Adresa : ISerializable
{
    public int IdAdr { get; set; }

    public string Ulica { get; set; }
    public string Broj { get; set; }        //jer moze biti 28A
    public string Grad { get; set;}
    public string Drzava { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Ulica,
            Broj,
            Grad,
            Drzava
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        IdAdr = int.Parse(values[0]);
        Ulica = values[1];
        Broj = values[2];
        Grad = values[3];
        Drzava = values[4];
    }
}

