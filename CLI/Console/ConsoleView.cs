using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Controller;
using CLI.DAO;
using CLI.Model;
using CLI.Storage;
using CLI.Storage.Serialization;

namespace CLI.Console;

public class ConsoleView<T> where T : class, IAccess<T>, ISerializable, IConsoleWriteRead, new()
{
    protected Controller.Controller _controller;

    public ConsoleView(Controller.Controller controller)
    {
        _controller = controller;
    }

    protected void PrintObjects(List<T> objs)
    {
        T objHeader = new T();
        string header = objHeader.GenerateClassHeader() + "\n";
        ConsoleViewUtils.ConsoleWriteLineColor(header, ConsoleColor.Magenta);
        foreach (T obj in objs) 
        {
            System.Console.WriteLine(obj);      
        }
    }

    protected void InputObject(T obj, bool skippable = false)
    {
        foreach(var prop in typeof(T).GetProperties())
        {
            if (prop.Name == "Id")
            {
                continue;
            }

            if (prop.PropertyType == typeof(int))
            {
                bool isOcena = false;
                if(prop.Name == "OcenaBroj") { isOcena = true; }
                System.Console.Write(prop.Name + " : ");
                int? br = ConsoleViewUtils.SafeInputInt(skippable, isOcena);
                if (br == null) continue;
                prop.SetValue(obj, br.Value);
            }
            else if (prop.PropertyType == typeof(string))
            {
                System.Console.Write(prop.Name + " : ");
                string str = ConsoleViewUtils.SafeInputString(skippable);
                if (str == null) continue;
                prop.SetValue(obj, str);
            }
            else if (prop.PropertyType == typeof(DateOnly))
            {
                System.Console.Write(prop.Name + " (dd.mm.yyyy.) : ");
                DateOnly? dt = ConsoleViewUtils.SafeInputDate(skippable);
                if (dt == null) continue;
                prop.SetValue(obj, dt.Value);
            }
            else if(prop.PropertyType == typeof(StatusEnum))
            {
                System.Console.Write(prop.Name + " (B/S) : ");
                StatusEnum? status = ConsoleViewUtils.SafeInputStatusEnum(skippable);
                if (status == null) continue;
                prop.SetValue(obj, status.Value);
            }
            else if (prop.PropertyType == typeof(SemesterEnum))
            {
                System.Console.Write(prop.Name + " (summer/winter) : ");
                SemesterEnum? status = ConsoleViewUtils.SafeInputSemesterEnum(skippable);
                if (status == null) continue;
                prop.SetValue(obj, status.Value);
            }
            else if (prop.PropertyType == typeof(Address))
            {
                System.Console.Write(prop.Name + " : \n");
                ConsoleViewUtils.SafeInputAdresa((Address) prop.GetValue(obj), skippable);
            }
            else if (prop.PropertyType == typeof(Model.Index))
            {
                System.Console.Write(prop.Name + " : \n");
                ConsoleViewUtils.SafeInputIndex((Model.Index)prop.GetValue(obj), skippable);
            }
            else if (prop.PropertyType == typeof(Professor))
            {
                System.Console.Write(prop.Name + " : \n");
                Professor p = ConsoleViewUtils.SafeInputProfessorId(_controller.daoProfessor, skippable);
                if (p == null) continue;
                prop.SetValue(obj, p);
            }
            else if (prop.PropertyType == typeof(Student))
            {
                System.Console.Write(prop.Name + " : \n");
                Student s = ConsoleViewUtils.SafeInputStudentId(_controller.daoStudent, skippable);
                if (s == null) continue;
                prop.SetValue(obj, s);
            }
            else if (prop.PropertyType == typeof(Subject))
            {
                System.Console.Write(prop.Name + " : \n");
                Subject p = ConsoleViewUtils.SafeInputSubjectId(_controller.daoSubject, skippable);
                if (p == null) continue;
                prop.SetValue(obj, p);
            }
        }
    }

    protected int InputId()
    {
        System.Console.WriteLine("Enter " + typeof(T).Name + " id: ");
        return ConsoleViewUtils.SafeInputInt().Value;
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
        System.Console.WriteLine("  1: Show all");
        System.Console.WriteLine("  2: Add");
        System.Console.WriteLine("  3: Update");
        System.Console.WriteLine("  4: Remove");
        System.Console.WriteLine("  0: Back");
    }
}
