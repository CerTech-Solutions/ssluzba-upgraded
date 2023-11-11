using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Adresa : ISerializable, IConsoleWriteRead
{
    public Adresa() { }

    public Adresa(string ulica, string broj, string grad, string drzava)
    {
        Ulica = ulica;
        Broj = broj;
        Grad = grad;
        Drzava = drzava;
    }

    public string Ulica { get; set; }

    public string Broj { get; set; }

    public string Grad { get; set;}

    public string Drzava { get; set; }

    public void Copy(Adresa obj)
    {
        Ulica = obj.Ulica;
        Broj = obj.Broj;
        Grad = obj.Grad;
        Drzava = obj.Drzava;
    }

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
        Ulica = values[0];
        Broj = values[1];
        Grad = values[2];
        Drzava = values[3];
    }

    public string GenerateClassHeader()
    {
        return $"| {"Ulica",25} | {"Broj",10} | {"Grad",25} | {"Drzava",25} |";
    }

    public override string ToString()
    {
        return $"| {Ulica,25} | {Broj,10} | {Grad,25} | {Drzava,25} |";
    }
}
