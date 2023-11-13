using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class ConsoleViewSelector
{
    private HeadDAO _headDAO;
    private ConsoleViewKatedra consoleKatedra;
    private ConsoleViewOcena consoleOcena;
    private ConsoleViewStudent consoleStudent;
    private ConsoleViewPredmet consolePredmet;
    private ConsoleViewProfesor consoleProfesor;

    public ConsoleViewSelector()
    {
        _headDAO = new HeadDAO();
        consoleKatedra = new ConsoleViewKatedra(_headDAO);
        consoleOcena = new ConsoleViewOcena(_headDAO);
        consoleStudent = new ConsoleViewStudent(_headDAO);
        consolePredmet = new ConsoleViewPredmet(_headDAO);
        consoleProfesor = new ConsoleViewProfesor(_headDAO);
    }

    public void RunSelector()
    {
        while (true)
        {
            ShowSelector();
            System.Console.Write("\nInput: ");
            string userInput = System.Console.ReadLine() ?? "0";
            ConsoleViewUtils.ConsoleRefresh();
            if (userInput == "0")
            {
                _headDAO.SaveAllDAOs();
                break;
            }
            HandleSelector(userInput);
        }
    }

    private void ShowSelector()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("  1: Katedra");
        System.Console.WriteLine("  2: Ocena");
        System.Console.WriteLine("  3: Predmet");
        System.Console.WriteLine("  4: Profesor");
        System.Console.WriteLine("  5: Student");
        System.Console.WriteLine("  0: Close");
    }

    private void HandleSelector(string input)
    {
        switch (input)
        {
            case "1":
                consoleKatedra.RunMenu();
                break;
            case "2":
                consoleOcena.RunMenu();
                break;
            case "3":
                consolePredmet.RunMenu();
                break;
            case "4":
                consoleProfesor.RunMenu();
                break;
            case "5":
                consoleStudent.RunMenu();
                break;
        }
    }
}
