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
        System.Console.WriteLine("  2: Indeks");
        System.Console.WriteLine("  3: Katedra");
        System.Console.WriteLine("  4: Ocena");
        System.Console.WriteLine("  5: Predaje Na Katedri");
        System.Console.WriteLine("  6: Predmet");
        System.Console.WriteLine("  7: Profesor");
        System.Console.WriteLine("  8: Profesor Predaje Predmet");
        System.Console.WriteLine("  9: Student");
        System.Console.WriteLine("  10: Student Slusa Predmet");
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
                IndeksChoosen();
                break;
            case "3":
                KatedraChoosen();
                break;
            case "4":
                OcenaChoosen();
                break;
            case "5":
                PredajeNaKatedriChoosen();
                break;
            case "6":
                PredmetChoosen();
                break;
            case "7":
                ProfesorChoosen();
                break;
            case "8":
                ProfesorPredajePredmetChoosen();
                break;
            case "9":
                StudentChoosen();
                break;
            case "10":
                StudentSlusaPredmetChoosen();
                break;     
        }
    }

    string path1 = "Adrese.csv";
    string path2 = "Indeksi.csv";
    string path3 = "Katedre.csv";
    string path4 = "Ocene.csv";
    string path5 = "PredajeNaKatedri.csv";
    string path6 = "Predmeti.csv";
    string path7 = "Profesori.csv";
    string path8 = "ProfesoriPredajuPredmet.csv";
    string path9 = "Studenti.csv";
    string path10 = "StudentiSlusajuPredmet.csv";

    private void AdresaChoosen()
    {
        DAO<Adresa> da = new DAO<Adresa>(path1);
        ConsoleView<Adresa> cv = new ConsoleView<Adresa>(da);
        cv.RunMenu();
    }

    private void IndeksChoosen()
    {
        DAO<Indeks> di = new DAO<Indeks>(path2);
        ConsoleView<Indeks> cv = new ConsoleView<Indeks>(di);
        cv.RunMenu();
    }

    private void KatedraChoosen()
    {
        DAO<Katedra> dk = new DAO<Katedra>(path3);
        ConsoleView<Katedra> cv = new ConsoleView<Katedra>(dk);
        cv.RunMenu();
    }

    private void OcenaChoosen()
    {
        DAO<Ocena> doc = new DAO<Ocena>(path4);
        ConsoleView<Ocena> cv = new ConsoleView<Ocena>(doc);
        cv.RunMenu();
    }

    private void PredajeNaKatedriChoosen()
    {
        DAO<PredajeNaKatedri> dpnk = new DAO<PredajeNaKatedri>(path5);
        ConsoleView<PredajeNaKatedri> cv = new ConsoleView<PredajeNaKatedri>(dpnk);
        cv.RunMenu();
    }

    private void PredmetChoosen()
    {
        DAO<Predmet> dp = new DAO<Predmet>(path6);
        ConsoleView<Predmet> cv = new ConsoleView<Predmet>(dp);
        cv.RunMenu();
    }

    private void ProfesorChoosen()
    {
        DAO<Profesor> dpr = new DAO<Profesor>(path7);
        ConsoleView<Profesor> cv = new ConsoleView<Profesor>(dpr);
        cv.RunMenu();
    }

    private void ProfesorPredajePredmetChoosen()
    {
        DAO<ProfesorPredajePredmet> dppp = new DAO<ProfesorPredajePredmet>(path8);
        ConsoleView<ProfesorPredajePredmet> cv = new ConsoleView<ProfesorPredajePredmet>(dppp);
        cv.RunMenu();
    }

    private void StudentChoosen()
    {
        DAO<Student> dst = new DAO<Student>(path9);
        ConsoleView<Student> cv = new ConsoleView<Student>(dst);
        cv.RunMenu();
    }

    private void StudentSlusaPredmetChoosen()
    {
        DAO<StudentSlusaPredmet> dssp = new DAO<StudentSlusaPredmet>(path10);
        ConsoleView<StudentSlusaPredmet> cv = new ConsoleView<StudentSlusaPredmet>(dssp);
        cv.RunMenu();
    }
}
