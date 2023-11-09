// See https://aka.ms/new-console-template for more information
using CLI.Console;
using CLI.DAO;
using CLI.Model;

class Program
{
    static void Main()
    {
        //Console.SetWindowSize(200, 80);
        ConsoleViewSelector cvs = new ConsoleViewSelector();
        cvs.RunSelector();
    }
}