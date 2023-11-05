using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model;
public class Ocena
{
    public Student student_polozio { get; set; }
    public Predmet predmet { get; set; }
    public int ocena { get; set; }
    public DateTime datum_polaganja { get; set; }
}

