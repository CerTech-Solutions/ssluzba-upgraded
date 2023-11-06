﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model;

public enum SemestarEnum
{
    letnji,
    zimski
}

public class Predmet
{
    public int IdPred { get; set; } 

    public string Sifra { get; set; }
    public string Naziv { get; set; }
    public SemestarEnum Semestar { get; set; }
    public int GodStudija { get; set; } 
    public int IdProfesor { get; set; }
    public int Espb { get; set; }
    public List<int> IdStudentiPolozili = new List<int>();
    public List<int> IdStudentiNisuPolozili = new List<int>();
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            IdPred.ToString(),
            Sifra,
            Naziv,
            Semestar.ToString(),
            GodStudija.ToString(),
            IdProfesor.ToString(),
            Espb.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        IdPred = int.Parse(values[0]);
        Sifra = values[1];
        Naziv = values[2];

        if(values[3] == "letnji")
            Semestar = SemestarEnum.letnji;
        else
            Semestar = SemestarEnum.zimski;

        GodStudija = int.Parse(values[4]);
        IdProfesor = int.Parse(values[5]);
        Espb = int.Parse(values[6]);
    }
}
