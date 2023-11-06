using CLI.DAO;
using CLI.Storage.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model;

public class PredajeNaKatedri : ISerializable, IAccess
{
    public int Id { get; set; }

    public int IdProf {  get; set; }

    public int IdKat { get; set; }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            IdProf.ToString(),
            IdKat.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        IdProf = int.Parse(values[0]);
        IdKat = int.Parse(values[1]);
    }
}
