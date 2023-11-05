using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace CLI.Model;

public class Profesor
{
    public string ime { get; set; }
    public string prezime { get; set; }
    public DateTime datum_rodj { get; set; }
    public Adresa adresa { get; set; }
    public string br_telefona { get; set; } 
    public string email { get; set; }   
    public string br_licne_karte { get; set; }
    public string zvanje { get; set; }  
    public int god_staza { get; set; }
    public Predmet[] predmeti { get; set; }
}

