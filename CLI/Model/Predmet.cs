using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model;

public class Predmet
{
    public enum Semestar
    {
        letnji,
        zimski
    }

    public string sifra { get; set; }
    public string naziv { get; set; }
    public Semestar semestar { get; set; }
    public int god_studija { get; set; } 
    public Profesor profesor { get; set; }
    public int espb { get; set; }
    public Student[] studenti_polozili { get; set; }
    public Student[] studenti_nisu_polozili { get; set; }


}
