using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model;

public class Student
{
    public enum Status
    {
        B,                  //budzet
        S                   //samofinansiranje
    }


    public string ime { get; set; }
    public string prezime { get; set; }
    public DateTime datum_rodj { get; set; }
    public Adresa adresa { get; set; }
    public string br_telefona { get; set; }
    public string email { get; set; }
    public Indeks indeks { get; set; }
    public int trenutna_god { get; set; }
    public Status status { get; set; }
    public Ocena[] polozeni_predmeti { get; set; }
    public Ocena[] nepolozeni_predmeti { get; set; }
    public double prosecna_ocena
    { 
            get 
            {
                int suma = 0;
                foreach (Ocena o in polozeni_predmeti)
                {
                    suma += o.ocena;
                }
                suma = suma / polozeni_predmeti.Length;
                return suma;
            }
            set { prosecna_ocena = value; }    
    }           
}

