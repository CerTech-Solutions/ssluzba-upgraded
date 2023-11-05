using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model;

public class Adresa
{
    public string ulica { get; set; }
    public string broj { get; set; }        //jer moze biti 28A
    public string grad { get; set;}
    public string drzava { get; set; }
}

