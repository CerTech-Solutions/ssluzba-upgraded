using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Storage.Serialization;
using CLI.Console;

namespace CLI.Model;

public class Index : ISerializable, IConsoleWriteRead
{
    public Index() { }

    public Index(string courseLabel, int regNumber, int enrollmentYear)
    {
        CourseLabel = courseLabel;
        RegNumber = regNumber;
        EnrollmentYear = enrollmentYear;
    }

    public string CourseLabel { get; set; }

    public int RegNumber { get; set; }

    public int EnrollmentYear { get; set; }

    public void Copy(Index obj)
    {
        CourseLabel = obj.CourseLabel;
        RegNumber = obj.RegNumber;
        EnrollmentYear = obj.EnrollmentYear;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            CourseLabel,
            RegNumber.ToString(),
            EnrollmentYear.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        CourseLabel = values[0];
        RegNumber = int.Parse(values[1]);
        EnrollmentYear = int.Parse(values[2]);
    }

    public string GenerateClassHeader()
    {     
        return $" {"Index",12} |";
    }
    
    public string ToString()
    {
        return $" {CourseLabel,3} {RegNumber,3} {EnrollmentYear,4} |";
    }

}


