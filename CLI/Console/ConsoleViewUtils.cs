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
    public static int SafeInputInt()
    {
        int input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (!int.TryParse(rawInput, out input))
        {
            System.Console.Write("Not a valid number, try again: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return input;
    }

    public static string SafeInputString()
    {
        string input = System.Console.ReadLine() ?? string.Empty;

        while (input == "")
        {
            System.Console.Write("Empty string, try again: ");

            input = System.Console.ReadLine() ?? string.Empty;
        }

        return input;
    }

    public static DateTime SafeInputDate()
    {
        DateTime input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (!DateTime.TryParse(rawInput, out input))
        {
            System.Console.Write("Wrong date format (dd.mm.yyyy), try again: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return input;
    }

    public static StatusEnum SafeInputStatusEnum()
    {
        StatusEnum input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (!Enum.TryParse<StatusEnum>(rawInput, out input))
        {
            System.Console.Write("Wrong student status (B/S), try again: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return input;
    }

    public static SemestarEnum SafeInputSemestarEnum()
    {
        SemestarEnum input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (!Enum.TryParse<SemestarEnum>(rawInput, out input))
        {
            System.Console.Write("Wrong type semester (summer/winter), try again: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return input;
    }

    public static Adresa SafeInputAdresa()
    {
        Adresa obj = new Adresa();
        foreach (var prop in obj.GetType().GetProperties())
        {
            if (prop.Name != "Id")
            {   
                if (prop.PropertyType == typeof(string))
                {
                    System.Console.Write("\t" + prop.Name + " : ");
                    string str = ConsoleViewUtils.SafeInputString();
                    prop.SetValue(obj, str);
                }   
            }
        }

        return obj;
    }

    public static Indeks SafeInputIndeks()
    {
        Indeks obj = new Indeks();
        foreach (var prop in obj.GetType().GetProperties())
        {
            if (prop.Name != "Id")
            {
                if (prop.PropertyType == typeof(string))
                {
                    System.Console.Write("\t" + prop.Name + " : ");
                    string str = ConsoleViewUtils.SafeInputString();
                    prop.SetValue(obj, str);
                }
                else if (prop.PropertyType == typeof(int))
                {
                    System.Console.Write("\t" + prop.Name + " : ");
                    int br = ConsoleViewUtils.SafeInputInt();
                    prop.SetValue(obj, br);
                }
            }
        }

        return obj;
    }

    public static Profesor SafeInputProfesorId(DAO<Profesor> daoProfesor)
    {
        System.Console.Write("Enter profesor ID: ");
        int idProf = SafeInputInt();
        Profesor prof = daoProfesor.GetObjectById(idProf);

        while (prof == null)
        {
            System.Console.Write("Professor with that ID doesn't exist, try again: ");
            idProf = SafeInputInt();
            prof = daoProfesor.GetObjectById(idProf);
            
        }

        return prof;
    }

    public static Katedra SafeInputKatedraId(DAO<Katedra> daoKatedra)
    {
        System.Console.Write("Enter katedra ID: ");
        int idKat = SafeInputInt();
        Katedra kat = daoKatedra.GetObjectById(idKat);

        while (kat == null)
        {
            System.Console.Write("Katedra with that ID doesn't exist, try again: ");
            idKat = SafeInputInt();
            kat = daoKatedra.GetObjectById(idKat);

        }

        return kat;
    }

    public static Predmet SafeInputPredmetId(DAO<Predmet> daoPredmet)
    {
        System.Console.Write("Enter predmet ID: ");
        int idPred = SafeInputInt();
        Predmet pred = daoPredmet.GetObjectById(idPred);

        while (pred == null)
        {
            System.Console.Write("Predmet with that ID doesn't exist, try again: ");
            idPred = SafeInputInt();
            pred = daoPredmet.GetObjectById(idPred);

        }

        return pred;
    }

    public static Student SafeInputStudentId(DAO<Student> daoStudent)
    {
        System.Console.Write("Enter student ID: ");
        int idStud = SafeInputInt();
        Student stud = daoStudent.GetObjectById(idStud);

        while (stud == null)
        {
            System.Console.Write("Student with that ID doesn't exist, try again: ");
            idStud = SafeInputInt();
            stud = daoStudent.GetObjectById(idStud);

        }

        return stud;
    }

    public static Ocena SafeInputOcenaId(DAO<Ocena> daoOcena)
    {
        System.Console.Write("Enter ocena ID: ");
        int idOcena = SafeInputInt();
        Ocena oc = daoOcena.GetObjectById(idOcena);

        while (oc == null)
        {
            System.Console.Write("Ocena with that ID doesn't exist, try again: ");
            idOcena = SafeInputInt();
            oc = daoOcena.GetObjectById(idOcena);

        }

        return oc;
    }

    public static void ConsoleRefresh()
    {
        System.Console.Clear();
        System.Console.WriteLine("\x1b[3J");
    }
}
