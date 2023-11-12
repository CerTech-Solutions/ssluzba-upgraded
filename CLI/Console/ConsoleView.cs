using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Model;
using CLI.Storage;
using CLI.Storage.Serialization;

namespace CLI.Console;

public class ConsoleView<T> where T : class, IAccess<T>, ISerializable, IConsoleWriteRead, new()
{
    protected HeadDAO _headDAO;

    public ConsoleView(HeadDAO headDAO)
    {
        _headDAO = headDAO;
    }

    protected void PrintObjects(List<T> objs)
    {
        T objHeader = new T();
        string header = objHeader.GenerateClassHeader();
        System.Console.WriteLine(header);
        foreach (T obj in objs) 
        {
            System.Console.WriteLine(obj);      
        }
    }

    protected T? InputObject()
    {
        T obj = new T();
        foreach(var prop in typeof(T).GetProperties())
        {
            if (prop.Name != "Id")
            {
                continue;
            }

            if (prop.PropertyType == typeof(int))
            {
                System.Console.Write(prop.Name + " : ");
                int br = ConsoleViewUtils.SafeInputInt();
                prop.SetValue(obj, br);
            }
            else if (prop.PropertyType == typeof(string))
            {
                System.Console.Write(prop.Name + " : ");
                string str = ConsoleViewUtils.SafeInputString();
                prop.SetValue(obj, str);
            }
            else if (prop.PropertyType == typeof(DateTime))
            {
                System.Console.Write(prop.Name + " (dd.mm.yyyy.) : ");
                DateTime dt = ConsoleViewUtils.SafeInputDate();
                prop.SetValue(obj, dt);
            }
            else if(prop.PropertyType == typeof(StatusEnum))
            {
                System.Console.Write(prop.Name + " (B/S) : ");
                StatusEnum status = ConsoleViewUtils.SafeInputStatusEnum();
                prop.SetValue(obj, status);
            }
            else if (prop.PropertyType == typeof(SemestarEnum))
            {
                System.Console.Write(prop.Name + " (summer/winter) : ");
                SemestarEnum status = ConsoleViewUtils.SafeInputSemestarEnum();
                prop.SetValue(obj, status);
            }
            else if (prop.PropertyType == typeof(Adresa))
            {
                System.Console.Write(prop.Name + " : ");
                Adresa adr = ConsoleViewUtils.SafeInputAdresa();
                prop.SetValue(obj, adr);
            }
            else if (prop.PropertyType == typeof(Indeks))
            {
                System.Console.Write(prop.Name + " : ");
                Indeks ind = ConsoleViewUtils.SafeInputIndeks();
                prop.SetValue(obj, ind);
            }
            else if (prop.PropertyType == typeof(Profesor))
            {
                System.Console.Write(prop.Name + " : ");
                Profesor p = ConsoleViewUtils.SafeInputProfesorId(_headDAO.daoProfesor);
                prop.SetValue(obj, p);
            }
        }

        return obj;
    }

    protected int InputId()
    {
        System.Console.WriteLine("Enter " + typeof(T).Name + " id: ");
        return ConsoleViewUtils.SafeInputInt();
    }

    public void RunMenu()
    {
        while (true)
        {
            ShowMenu();
            System.Console.Write("\nInput: ");
            string userInput = System.Console.ReadLine() ?? "0";
            ConsoleViewUtils.ConsoleRefresh();
            if (userInput == "0") break;
            HandleMenuInput(userInput);
        }   
    }

    protected void HandleMenuInput(string input)
    {   
        switch (input)
        {
            case "1":
                ShowAll();
                break;
            case "2":
                AddObject();
                break;
            case "3":
                UpdateObject();
                break;
            case "4":
                RemoveObject();
                break;
        }
    }

    public virtual void ShowAll() { }

    public virtual void AddObject() { }

    public virtual void UpdateObject() { }

    public virtual void RemoveObject() { }

    public void ShowMenu()
    {
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("  1: Show All objects");
            System.Console.WriteLine("  2: Add objects");
            System.Console.WriteLine("  3: Update object");
            System.Console.WriteLine("  4: Remove object");
            System.Console.WriteLine("  0: Back");
    }
}
