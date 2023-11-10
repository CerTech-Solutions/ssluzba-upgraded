﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Adresa : ISerializable, IAccess<Adresa>, IConsoleWriteRead
{
    private int _idAdr;

    public Adresa() { }

    public Adresa(int idAdr, string ulica, string broj, string grad, string drzava)
    {
        Id = idAdr;
        Ulica = ulica;
        Broj = broj;
        Grad = grad;
        Drzava = drzava;
    }

    public int Id
    {
        get { return _idAdr; }
        set { _idAdr = value; }
    }

    public string Ulica { get; set; }

    public string Broj { get; set; }

    public string Grad { get; set;}

    public string Drzava { get; set; }

    public void Copy(Adresa obj)
    {
        Id = obj.Id;
        Ulica = obj.Ulica;
        Broj = obj.Broj;
        Grad = obj.Grad;
        Drzava = obj.Drzava;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Ulica,
            Broj,
            Grad,
            Drzava
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Ulica = values[1];
        Broj = values[2];
        Grad = values[3];
        Drzava = values[4];
    }

    public string GenerateClassHeader()
    {
        return "Adrese: \n" + $"{"Id",6} | {"Ulica",25} | {"Broj",10} | {"Grad",25} | {"Drzava",25} |";
    }

    public override string ToString()
    {
        return $"{Id,6} | {Ulica,25} | {Broj,10} | {Grad,25} | {Drzava,25} |";
    }
}
