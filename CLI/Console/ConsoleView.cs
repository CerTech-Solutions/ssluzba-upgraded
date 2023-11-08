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

internal class ConsoleView<T> where T : class, IAccess, ISerializable, IConsoleWR, new()
{
    private readonly DAO<T> _daoObjs = new DAO<T>("data.csv");
    
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
            System.Console.WriteLine(obj);
        }
    }

    private T InputObject()
    {
        T obj = new T();
        foreach(var prop in obj.GetType().GetProperties())
        {
            if (prop.Name != "Id")
            {
                System.Console.WriteLine(prop.Name + " :");
                if (prop.PropertyType == typeof(int))
                {
                    string str = System.Console.ReadLine() ?? string.Empty;
                    int br = int.Parse(str);
                    prop.SetValue(obj, br);
                }
                else if (prop.PropertyType == typeof(string))
                {
                    string str = System.Console.ReadLine() ?? string.Empty;
                    prop.SetValue(obj, str);
                }
                else if (prop.PropertyType == typeof(DateTime))
                {
                    string str = System.Console.ReadLine() ?? string.Empty;
                    DateTime dt = DateTime.Parse(str);
                    prop.SetValue(obj, dt);
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
            string userInput = System.Console.ReadLine() ?? "0";
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
        T obj = InputObject();
        _daoObjs.AddObject(obj);
        System.Console.WriteLine(obj.GetType().ToString() + " added");
    }

    private void UpdateObject()
    {
        int id = InputId();
        T obj = InputObject();
        obj.Id = id;
        T? updatedObj = _daoObjs.UpdateObject(obj);
        if(updatedObj == null)
        {
            System.Console.WriteLine(obj.GetType().ToString() + " not found");
            return;
        }

        System.Console.WriteLine(obj.GetType().ToString() + " updated");
    }

    private void RemoveObject()
    {
        int id = InputId();
        T? removedObj = _daoObjs.RemoveObject(id);
        if(removedObj == null)
        {
            System.Console.WriteLine(removedObj.GetType().ToString() + " not found");
            return;
        }

        System.Console.WriteLine(removedObj.GetType().ToString() + " updated");
    }

    private void ShowAndSortObjects()
    {

    }

    private void ShowMenu()
    {
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("1: Show All Objects");
            System.Console.WriteLine("2: Add Objects");
            System.Console.WriteLine("3: Update Object");
            System.Console.WriteLine("4: Remove Object");
            System.Console.WriteLine("5: Show and sort Objects");
            System.Console.WriteLine("0: Close");
    }
}
