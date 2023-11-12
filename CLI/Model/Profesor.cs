﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using CLI.Storage.Serialization;
using CLI.DAO;
using CLI.Console;

namespace CLI.Model;

public class Profesor : ISerializable, IAccess<Profesor>, IConsoleWriteRead
{
    private int _idProf;

    public Profesor()
    {
        Predmeti = new List<Predmet>();
        Adresa = new Adresa();
    }

    public Profesor(int idProf, string ime, string prezime, DateOnly datumRodjenja, Adresa adresa, string brojTelefona, string email, string brojLicneKarte, string zvanje, int godinaStaza)
    {
        Id = idProf;
        Ime = ime;
        Prezime = prezime;
        DatumRodjenja = datumRodjenja;
        Adresa = adresa;
        BrojTelefona = brojTelefona;
        Email = email;
        BrojLicneKarte = brojLicneKarte;
        Zvanje = zvanje;
        GodinaStaza = godinaStaza;
    }

    public int Id
    {
        get { return _idProf; }
        set { _idProf = value; }
    }

    public string Ime { get; set; }

    public string Prezime { get; set; }

    public DateOnly DatumRodjenja { get; set; }

    public string BrojTelefona { get; set; }

    public string Email { get; set; }

    public string BrojLicneKarte { get; set; }

    public string Zvanje { get; set; }

    public int GodinaStaza { get; set; }

    public List<Predmet> Predmeti { get; set; }

    public Adresa Adresa { get; set; }

    public void Copy(Profesor obj)
    {
        Id = obj.Id;
        Ime = obj.Ime;
        Prezime = obj.Prezime;
        DatumRodjenja = obj.DatumRodjenja;
        BrojTelefona = obj.BrojTelefona;
        Email = obj.Email;
        BrojLicneKarte = obj.BrojLicneKarte;
        Zvanje = obj.Zvanje;
        GodinaStaza = obj.GodinaStaza;
        Adresa.Copy(obj.Adresa);
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(), Ime, Prezime, DatumRodjenja.ToString("dd-MM-yyyy"),
            BrojTelefona, Email, BrojLicneKarte,
            Zvanje, GodinaStaza.ToString()
        };

        string[] result = csvValues.Concat(Adresa.ToCSV()).ToArray();
        return result;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Ime = values[1];
        Prezime = values[2];
        DatumRodjenja = DateOnly.Parse(values[3]);
        BrojTelefona = values[4];
        Email = values[5];
        BrojLicneKarte = values[6];
        Zvanje = values[7];
        GodinaStaza = int.Parse(values[8]);
        Adresa.FromCSV(values[9..]);
    }

	public string GenerateClassHeader()
    {
        return "Profesori: \n" + $"{"ID",6} | {"Ime",12} | {"Prezime",12} | {"DatumRodjenja",13} | {"BrojTelefona",12} | {"Email",20} | {"BrojLicneKarte",16} | {"Zvanje",8} | {"Staza",5} |"; // + Adresa.GenerateClassHeader();
    }

    public override string ToString()
    {
        string str = $"{Id,6} | {Ime,12} | {Prezime,12} | {DatumRodjenja.ToString("dd/MM/yyyy"),13} | {BrojTelefona,12} | {Email,20} | {BrojLicneKarte,16} | {Zvanje,8} | {GodinaStaza,5} |"; // + Adresa.ToString();
        str += "\n\t* Teaches subjects: \n";
        foreach (Predmet p in Predmeti)
        {
            str += $"\t\t{p.Sifra} {p.Naziv}\n";
        }
        return str;
    }
}
