using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model;

public class Katedra
{
    public string sifra { get; set; }
    public string naziv { get; set; }
    public Profesor sef_katedre { get; set; }
    public Profesor[] profesors { get; set; }
}

