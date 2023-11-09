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
    public void RunSelector()
    {
        while (true)
        {
            ShowSelector();
            System.Console.Write("\nInput: ");
            string userInput = System.Console.ReadLine() ?? "0";
            ConsoleViewUtils.ConsoleRefresh();
            if (userInput == "0") break;
            HandleSelector(userInput);
        }
    }

    private void ShowSelector()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("  1: Adresa");
        System.Console.WriteLine("  2: Katedra");
        System.Console.WriteLine("  3: Ocena");
        System.Console.WriteLine("  4: Profesor predaje na katedri");
        System.Console.WriteLine("  5: Predmet");
        System.Console.WriteLine("  6: Profesor");
        System.Console.WriteLine("  7: Profesor predaje predmet");
        System.Console.WriteLine("  8: Student");
        System.Console.WriteLine("  9: Student slusa predmet");
        System.Console.WriteLine("  0: Close");
    }

    private void HandleSelector(string input)
    {
        switch (input)
        {
            case "1":
                AdresaChoosen();
                break;
            case "2":
                KatedraChoosen();
                break;
            case "3":
                OcenaChoosen();
                break;
            case "4":
                ProfesorRadiNaKatedriChoosen();
                break;
            case "5":
                PredmetChoosen();
                break;
            case "6":
                ProfesorChoosen();
                break;
            case "7":
                ProfesorPredajePredmetChoosen();
                break;
            case "8":
                StudentChoosen();
                break;
            case "9":
                StudentSlusaPredmetChoosen();
                break;     
        }
    }

    private void AdresaChoosen()
    {
        DAO<Adresa> da = new DAO<Adresa>();
        ConsoleView<Adresa> cv = new ConsoleView<Adresa>(da);
        cv.RunMenu();
    }

    private void KatedraChoosen()
    {
        DAO<Katedra> dk = new DAO<Katedra>();
        ConsoleView<Katedra> cv = new ConsoleView<Katedra>(dk);
        cv.RunMenu();
    }

    private void OcenaChoosen()
    {
        DAO<Ocena> doc = new DAO<Ocena>();
        ConsoleView<Ocena> cv = new ConsoleView<Ocena>(doc);
        cv.RunMenu();
    }

    private void ProfesorRadiNaKatedriChoosen()
    {
        DAO<ProfesorRadiNaKatedri> dpnk = new DAO<ProfesorRadiNaKatedri>();
        ConsoleView<ProfesorRadiNaKatedri> cv = new ConsoleView<ProfesorRadiNaKatedri>(dpnk);
        cv.RunMenu();
    }

    private void PredmetChoosen()
    {
        DAO<Predmet> dp = new DAO<Predmet>();
        ConsoleView<Predmet> cv = new ConsoleView<Predmet>(dp);
        cv.RunMenu();
    }

    private void ProfesorChoosen()
    {
        DAO<Profesor> dpr = new DAO<Profesor>();
        ConsoleView<Profesor> cv = new ConsoleView<Profesor>(dpr);
        cv.RunMenu();
    }

    private void ProfesorPredajePredmetChoosen()
    {
        DAO<ProfesorPredajePredmet> dppp = new DAO<ProfesorPredajePredmet>();
        ConsoleView<ProfesorPredajePredmet> cv = new ConsoleView<ProfesorPredajePredmet>(dppp);
        cv.RunMenu();
    }

    private void StudentChoosen()
    {
        DAO<Student> dst = new DAO<Student>();
        ConsoleView<Student> cv = new ConsoleView<Student>(dst);
        cv.RunMenu();
    }

    private void StudentSlusaPredmetChoosen()
    {
        DAO<StudentSlusaPredmet> dssp = new DAO<StudentSlusaPredmet>();
        ConsoleView<StudentSlusaPredmet> cv = new ConsoleView<StudentSlusaPredmet>(dssp);
        cv.RunMenu();
    }
}
