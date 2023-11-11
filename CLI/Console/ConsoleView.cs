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

internal class ConsoleView<T> where T : class, IAccess<T>, ISerializable, IConsoleWriteRead, new()
{
    private readonly DAO<T> _daoObjs = new DAO<T>();
    
    public ConsoleView(DAO<T> daoObjs)
    {
        _daoObjs = daoObjs;
    }

    private void PrintObjects(List<T> objs)
    {
        T objHeader = new T();
        string header = objHeader.GenerateClassHeader();
        System.Console.WriteLine(header);
        foreach (T obj in objs) 
        {
            System.Console.WriteLine(obj);      //
        }
    }

    private T? InputObject()
    {
        T obj = new T();
        foreach(var prop in obj.GetType().GetProperties())
        {
            if (prop.Name != "Id")
            {
                System.Console.Write(prop.Name + " : ");
                string str = System.Console.ReadLine() ?? string.Empty;

                if (prop.PropertyType == typeof(int))
                {
                    int br = int.Parse(str);
                    prop.SetValue(obj, br);
                }
                else if (prop.PropertyType == typeof(string))
                {
                    prop.SetValue(obj, str);
                }
                else if (prop.PropertyType == typeof(DateTime))
                {
                    DateTime dt = DateTime.Parse(str);
                    prop.SetValue(obj, dt);
                }
                else if(prop.PropertyType == typeof(StatusEnum))
                {
                    bool success;
                    StatusEnum status;
                    success = Enum.TryParse<StatusEnum>(str, out status);
                    if (!success)
                        return null;

                    prop.SetValue(obj, status);
                }
                else if (prop.PropertyType == typeof(SemestarEnum))
                {
                    bool success;
                    SemestarEnum status;
                    success = Enum.TryParse<SemestarEnum>(str, out status);
                    if (!success)
                        return null;

                    prop.SetValue(obj, status);
                }
            }
        }

        return obj;
    }

    private int InputId()
    {
        T obj = new T();
        System.Console.WriteLine("Enter " + obj.GetType().ToString() + " id: ");
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

    private void HandleMenuInput(string input)
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
            case "5":
                ShowAndSortObjects();
                break;
        }
    }

    private void ShowAll()
    {
        PrintObjects(_daoObjs.GetAllObjects());
    }

    private void AddObject()
    {
        T? obj = InputObject();
        if (obj is null)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Invalid input");
            System.Console.ResetColor();
            return;
        }

        _daoObjs.AddObject(obj);

        System.Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine(typeof(T).Name + " added");
        System.Console.ResetColor();
    }

    private void UpdateObject()
    {
        int id = InputId();
        T obj = InputObject();
        obj.Id = id;
        T? updatedObj = _daoObjs.UpdateObject(obj);
        if(updatedObj is null)
        {
            System.Console.WriteLine(typeof(T).Name + " not found");
            return;
        }

        System.Console.WriteLine(typeof(T).Name + " updated");
    }

    private void RemoveObject()
    {
        int id = InputId();
        T? removedObj = _daoObjs.RemoveObject(id);
        if(removedObj == null)
        {
            System.Console.WriteLine(typeof(T).Name + " not found");
            return;
        }

        System.Console.WriteLine(typeof(T).Name + " updated");
    }

    private void ShowAndSortObjects()
    {

    }

    private void ShowMenu()
    {
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("  1: Show All objects");
            System.Console.WriteLine("  2: Add objects");
            System.Console.WriteLine("  3: Update object");
            System.Console.WriteLine("  4: Remove object");
            System.Console.WriteLine("  5: Show and sort objects");
            System.Console.WriteLine("  0: Back");
    }
}
