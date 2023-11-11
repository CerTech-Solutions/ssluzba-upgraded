using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Storage.Serialization;
using CLI.Console;

namespace CLI.Model;

public class Indeks : ISerializable, IConsoleWriteRead
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

    public void Copy(Indeks obj)
    {
        OznakaSmera = obj.OznakaSmera;
        BrojUpisa = obj.BrojUpisa;
        GodinaUpisa = obj.GodinaUpisa;
    }

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

    public string GenerateClassHeader()
    {     
        return $"| {"OznakaSmera",11} | {"BrojUpisa",9} | {"GodinaUpisa",11} |";
    }
    
    public string ToString()
    {
        return $"| {OznakaSmera,11} | {BrojUpisa,9} | {GodinaUpisa,11} |";
    }

}


