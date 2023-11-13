using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model;

public class ProfessorWorksAtDepartment : ISerializable, IAccess<ProfessorWorksAtDepartment>, IConsoleWriteRead
{
    public ProfessorWorksAtDepartment() { }

    public ProfessorWorksAtDepartment(int id, int idProf, int idDep)
    {
        Id = id;
        IdProf = idProf;
        IdDep = idDep;
    }

    public int Id { get; set; }

    public int IdProf {  get; set; }

    public int IdDep { get; set; }

    public void Copy(ProfessorWorksAtDepartment obj)
    {
        obj.Id = Id;
        obj.IdProf = IdProf;
        obj.IdDep = IdDep;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            IdProf.ToString(),
            IdDep.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        IdProf = int.Parse(values[1]);
        IdDep = int.Parse(values[2]);
    }

    public string GenerateClassHeader()
    {
        return "Professors work at departments: \n" + $"{"ID",6} | {"IdProf",10} | {"IdDep",10} |";
    }

    public override string ToString()
    {
        return $"{Id,6} | {IdProf,10} | {IdDep,10} |";
    }
}
