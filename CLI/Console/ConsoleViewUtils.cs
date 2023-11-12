using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using CLI.DAO;

namespace CLI.Console;

internal class ConsoleViewUtils
{
    public static int? SafeInputInt(bool skippable = false)
    {
        int input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;

        if(skippable && rawInput == string.Empty)
        {
            return null;
        }

        while (!int.TryParse(rawInput, out input))
        {
            System.Console.Write("Not a valid number, try again: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
            if(skippable && rawInput == string.Empty)
            {
                return null;
            }
        }

        return input;
    }

    public static string SafeInputString(bool skippable = false)
    {
        string input = System.Console.ReadLine() ?? string.Empty;

        if (skippable && input == string.Empty)
        {
            return null;
        }

        while (input == string.Empty)
        {
            System.Console.Write("Empty string, try again: ");

            input = System.Console.ReadLine() ?? string.Empty;
            if (skippable && input == string.Empty)
            {
                return null;
            }
        }

        return input;
    }

    public static DateOnly? SafeInputDate(bool skippable = false)
    {
        DateOnly input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;

        if (skippable && rawInput == string.Empty)
        {
            return null;
        }

        while (!DateOnly.TryParse(rawInput, out input))
        {
            System.Console.Write("Wrong date format (dd.mm.yyyy), try again: ");

            rawInput = SafeInputString(skippable);
            if (skippable && rawInput == string.Empty)
            {
                return null;
            }
        }

        return input;
    }

    public static StatusEnum? SafeInputStatusEnum(bool skippable = false)
    {
        StatusEnum input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;
        if(skippable && rawInput == string.Empty)
        {
            return null;
        }

        while (!Enum.TryParse<StatusEnum>(rawInput, out input))
        {
            System.Console.Write("Wrong student status (B/S), try again: ");

            rawInput = SafeInputString(skippable);
            if (skippable && rawInput == string.Empty)
            {
                return null;
            }
        }

        return input;
    }

    public static SemestarEnum? SafeInputSemestarEnum(bool skippable = false)
    {
        SemestarEnum input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;
        if (skippable && rawInput == string.Empty)
        {
            return null;
        }

        while (!Enum.TryParse<SemestarEnum>(rawInput, out input))
        {
            System.Console.Write("Wrong type semester (summer/winter), try again: ");

            rawInput = SafeInputString(skippable);
            if (skippable && rawInput == string.Empty)
            {
                return null;
            }
        }

        return input;
    }

    public static void SafeInputAdresa(Adresa obj, bool skippable = false)
    {
        foreach (var prop in obj.GetType().GetProperties())
        {
            if (prop.Name != "Id")
            {   
                if (prop.PropertyType == typeof(string))
                {
                    System.Console.Write("\t" + prop.Name + " : ");
                    string str = ConsoleViewUtils.SafeInputString(skippable);
                    if (str == null) continue;
                    prop.SetValue(obj, str);
                }   
            }
        }
    }

    public static void SafeInputIndeks(Indeks obj, bool skippable = false)
    {
        foreach (var prop in obj.GetType().GetProperties())
        {
            if (prop.Name != "Id")
            {
                if (prop.PropertyType == typeof(string))
                {
                    System.Console.Write("\t" + prop.Name + " : ");
                    string str = ConsoleViewUtils.SafeInputString(skippable);
                    if (str == null) continue;
                    prop.SetValue(obj, str);
                }
                else if (prop.PropertyType == typeof(int))
                {
                    System.Console.Write("\t" + prop.Name + " : ");
                    int? br = ConsoleViewUtils.SafeInputInt(skippable);
                    if (br == null) continue;
                    prop.SetValue(obj, br);
                }
            }
        }
    }

    public static Profesor SafeInputProfesorId(DAO<Profesor> daoProfesor, bool skippable = false)
    {
        System.Console.Write("Enter profesor ID: ");
        int? idProf = SafeInputInt(skippable);
        if (idProf == null) return null;

        Profesor prof = daoProfesor.GetObjectById(idProf.Value);

        while (prof == null)
        {
            System.Console.Write("Professor with that ID doesn't exist, try again: ");
            idProf = SafeInputInt(skippable);
            if (idProf == null) return null;
            prof = daoProfesor.GetObjectById(idProf.Value);
        }

        return prof;
    }

    public static Katedra SafeInputKatedraId(DAO<Katedra> daoKatedra, bool skippable = false)
    {
        System.Console.Write("Enter katedra ID: ");
        int? idKat = SafeInputInt(skippable);
        if (idKat == null) return null;

        Katedra kat = daoKatedra.GetObjectById(idKat.Value);
       
        while (kat == null)
        {
            System.Console.Write("Katedra with that ID doesn't exist, try again: ");
            idKat = SafeInputInt(skippable);
            if (idKat == null) return null;
            kat = daoKatedra.GetObjectById(idKat.Value);
        }

        return kat;
    }

    public static Predmet SafeInputPredmetId(DAO<Predmet> daoPredmet, bool skippable = false)
    {
        System.Console.Write("Enter predmet ID: ");
        int? idPred = SafeInputInt(skippable);
        if (idPred == null) return null;

        Predmet pred = daoPredmet.GetObjectById(idPred.Value);

        while (pred == null)
        {
            System.Console.Write("Predmet with that ID doesn't exist, try again: ");
            idPred = SafeInputInt(skippable);
            if (idPred == null) return null;
            pred = daoPredmet.GetObjectById(idPred.Value);
        }

        return pred;
    }

    public static Student SafeInputStudentId(DAO<Student> daoStudent, bool skippable = false)
    {
        System.Console.Write("Enter student ID: ");
        int? idStud = SafeInputInt(skippable);
        if (idStud == null) return null;

        Student stud = daoStudent.GetObjectById(idStud.Value);

        while (stud == null)
        {
            System.Console.Write("Student with that ID doesn't exist, try again: ");
            idStud = SafeInputInt(skippable);
            if (idStud == null) return null;
            stud = daoStudent.GetObjectById(idStud.Value);
        }

        return stud;
    }

    public static Ocena SafeInputOcenaId(DAO<Ocena> daoOcena, bool skippable = false)
    {
        System.Console.Write("Enter ocena ID: ");
        int? idOcena = SafeInputInt(skippable);
        if (idOcena == null) return null;

        Ocena oc = daoOcena.GetObjectById(idOcena.Value);

        while (oc == null)
        {
            System.Console.Write("Ocena with that ID doesn't exist, try again: ");
            idOcena = SafeInputInt(skippable);
            if (idOcena == null) return null;
            oc = daoOcena.GetObjectById(idOcena.Value);
        }

        return oc;
    }

    public static void ConsoleRefresh()
    {
        System.Console.Clear();
        System.Console.WriteLine("\x1b[3J");
    }
}
