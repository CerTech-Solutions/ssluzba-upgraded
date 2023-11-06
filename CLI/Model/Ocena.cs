using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;
public class Ocena : ISerializable, IAccess
{
    private int _idOcn;

    public int Id
    {
        get { return _idOcn; }
        set { _idOcn = value; }
    }

    public int IdStudentPolozio { get; set; }

    public int IdPredmet { get; set; }

    public int OcenaBr { get; set; }

    public DateTime DatumPolaganja { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            IdPredmet.ToString(),
            OcenaBr.ToString(),
            DatumPolaganja.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        IdPredmet = int.Parse(values[1]);
        OcenaBr = int.Parse(values[3]);
        DatumPolaganja = DateTime.Parse(values[4]);
    }
}

