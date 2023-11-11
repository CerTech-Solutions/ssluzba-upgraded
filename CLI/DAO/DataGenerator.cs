using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;

namespace CLI.DAO;

public class DataGenerator
{
    public static void Generate()
    {

        /*
         * Deleting all csv file so new ones can be generated
         */
        string dataPath = "../../../Data";

        foreach (string file in Directory.GetFiles(dataPath, "*.csv"))
            File.Delete(file);

        Adresa a0 = new Adresa("Bulevar kralja Petra I", "9a", "Novi Sad", "Srbija");
        Adresa a1 = new Adresa("Bulevar vojvode Stepe", "11", "Novi Sad", "Srbija");
        Adresa a2 = new Adresa("Bulevar Jovan Ducica", "17", "Novi Sad", "Srbija");
        Adresa a3 = new Adresa("Bulevar oslobodjenja", "2", "Novi Sad", "Srbija");

        DAO<Profesor> daoProfesor = new DAO<Profesor>();
        Profesor p0 = new Profesor(0, "Veljko", "Petrovic", DateTime.Parse("2/2/1982"), a0, "021/485-4564", "pveljko@uns.ac.rs", "123456789", "doc.", 10);
        Profesor p1 = new Profesor(1, "Milan", "Rapajic", DateTime.Parse("2/2/1982"), a0, "021/485-4584", "rapaja@uns.ac.rs", "123456789", "prof.", 20);
        Profesor p2 = new Profesor(2, "Vladimir", "Dimitrieski", DateTime.Parse("2/2/1982"), a1, "021/485-2424", "dimi@uns.ac.rs", "123456789", "prof.", 15);
        daoProfesor.AddObject(p0);
        daoProfesor.AddObject(p1);
        daoProfesor.AddObject(p2);

        DAO<Katedra> daoKatedra = new DAO<Katedra>();
        Katedra k0 = new Katedra(0, "mat", "Katedra za PRN", p0);
        Katedra k1 = new Katedra(1, "aut", "Katedra za automatiku", p1);
        daoKatedra.AddObject(k0);
        daoKatedra.AddObject(k1);

        DAO<Predmet> daoPredmet = new DAO<Predmet>();
        Predmet pr0 = new Predmet(0, "bp1", "Baze podataka 1", SemestarEnum.zimski, 3, 2, 8);
        Predmet pr1 = new Predmet(1, "sau", "SAU", SemestarEnum.letnji, 2, 1, 8);
        Predmet pr2 = new Predmet(2, "os", "Operativni sistemi", SemestarEnum.letnji, 2, 0, 8);
        Predmet pr3 = new Predmet(3, "mo", "Metode optimizacije", SemestarEnum.zimski, 3, 1, 8);
        daoPredmet.AddObject(pr0);
        daoPredmet.AddObject(pr1);
        daoPredmet.AddObject(pr2);
        daoPredmet.AddObject(pr3);

        // Possibly could change
        DAO<Student> daoStudent = new DAO<Student>();
        Student s0 = new Student(0, "Nikola", "Kuslakovic", DateTime.Parse("2/2/2002"), a2, "123456789", "kuslakovic.ra8.2021@uns.ac.rs", 3, StatusEnum.B, 0.0, new Indeks("RA", 8, 2021));
        Student s1 = new Student(1, "Nemanja", "Zekanovic", DateTime.Parse("2/2/2002"), a3, "123456789", "zekanovic.ra73.2021@uns.ac.rs", 3, StatusEnum.S, 0.0, new Indeks("RA", 73, 2021));
        daoStudent.AddObject(s0);
        daoStudent.AddObject(s1);

        // Possibly could change
        DAO<StudentSlusaPredmet> daoStudentSlusaPredmet = new DAO<StudentSlusaPredmet>();
        StudentSlusaPredmet ssp0 = new StudentSlusaPredmet(0, 1, 3, PolozenPredmetEnum.polozio);
        StudentSlusaPredmet ssp1 = new StudentSlusaPredmet(0, 1, 0, PolozenPredmetEnum.polozio);
        StudentSlusaPredmet ssp2 = new StudentSlusaPredmet(0, 0, 2, PolozenPredmetEnum.polozio);
        StudentSlusaPredmet ssp3 = new StudentSlusaPredmet(0, 1, 2, PolozenPredmetEnum.nijePolozio);
        daoStudentSlusaPredmet.AddObject(ssp0);
        daoStudentSlusaPredmet.AddObject(ssp1);
        daoStudentSlusaPredmet.AddObject(ssp2);
        daoStudentSlusaPredmet.AddObject(ssp3);

        DAO<ProfesorRadiNaKatedri> daoProfesorRadiNaKatedri = new DAO<ProfesorRadiNaKatedri>();
        ProfesorRadiNaKatedri pnk0 = new ProfesorRadiNaKatedri(0, 0, 0);
        ProfesorRadiNaKatedri pnk1 = new ProfesorRadiNaKatedri(1, 1, 1);
        ProfesorRadiNaKatedri pnk2 = new ProfesorRadiNaKatedri(2, 2, 0);
        daoProfesorRadiNaKatedri.AddObject(pnk0);
        daoProfesorRadiNaKatedri.AddObject(pnk1);
        daoProfesorRadiNaKatedri.AddObject(pnk2);

        DAO<ProfesorPredajePredmet> daoProfesorPredajePredmet = new DAO<ProfesorPredajePredmet>();
        ProfesorPredajePredmet ppp0 = new ProfesorPredajePredmet(0, 0, 2);
        ProfesorPredajePredmet ppp1 = new ProfesorPredajePredmet(1, 1, 1);
        ProfesorPredajePredmet ppp2 = new ProfesorPredajePredmet(2, 2, 0);
        ProfesorPredajePredmet ppp3 = new ProfesorPredajePredmet(3, 1, 3);
        daoProfesorPredajePredmet.AddObject(ppp0);
        daoProfesorPredajePredmet.AddObject(ppp1);
        daoProfesorPredajePredmet.AddObject(ppp2);
        daoProfesorPredajePredmet.AddObject(ppp3);

        DAO<Ocena> daoOcena = new DAO<Ocena>();
        Ocena o0 = new Ocena(0, s1, pr3, 6, DateTime.Parse("2/2/2002"));
        Ocena o1 = new Ocena(1, s1, pr0, 10, DateTime.Parse("2/2/2002"));
        Ocena o2 = new Ocena(2, s0, pr2, 10, DateTime.Parse("2/2/2002"));
        daoOcena.AddObject(o0);
        daoOcena.AddObject(o1);
        daoOcena.AddObject(o2);
    }

}
