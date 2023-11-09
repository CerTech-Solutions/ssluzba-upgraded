﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Indeks : ISerializable
{
    public Indeks() { }

    public Indeks(string oznakaSmera, int brojUpisa, int godinaUpisa)
    {
        OznakaSmera = oznakaSmera;
        BrojUpisa = brojUpisa;
        GodinaUpisa = godinaUpisa;
    }

    public string OznakaSmera { get; set; }

    public int BrojUpisa { get; set; }   

    public int GodinaUpisa { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            OznakaSmera,
            BrojUpisa.ToString(),
            GodinaUpisa.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        OznakaSmera = values[0];
        BrojUpisa = int.Parse(values[1]);
        GodinaUpisa = int.Parse(values[2]);
    }
}
