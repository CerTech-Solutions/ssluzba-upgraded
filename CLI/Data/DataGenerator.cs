using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Model;

namespace CLI.Data;

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

        DAO<Katedra> daoKatedra = new DAO<Katedra>(typeof(Katedra).Name + ".csv");
        Katedra k0 = new Katedra(0, "mat", "Katedra za PRN", 0);
        Katedra k1 = new Katedra(1, "aut", "Katedra za automatiku", 1);
        daoKatedra.AddObject(k0);
        daoKatedra.AddObject(k1);

        DAO<Adresa> daoAdresa = new DAO<Adresa>(typeof(Adresa).Name + ".csv");
        Adresa a0 = new Adresa(0, "Bulevar kralja Petra I", "9a", "Novi Sad", "Srbija");
        Adresa a1 = new Adresa(1, "Bulevar vojvode Stepe", "11", "Novi Sad", "Srbija");
        Adresa a2 = new Adresa(3, "Bulevar Jovan Ducica", "17", "Novi Sad", "Srbija");
        Adresa a3 = new Adresa(4, "Bulevar oslobodjenja", "2", "Novi Sad", "Srbija");
        daoAdresa.AddObject(a0);
        daoAdresa.AddObject(a1);
        daoAdresa.AddObject(a2);
        daoAdresa.AddObject(a3);

        DAO<Profesor> daoProfesor = new DAO<Profesor>(typeof(Profesor).Name + ".csv");
        Profesor p0 = new Profesor(0, "Veljko", "Petrovic", DateTime.Parse("2/2/1982"), 0, "021/485-4564", "pveljko@uns.ac.rs", "123456789", "doc.", 10);
        Profesor p1 = new Profesor(1, "Milan", "Rapajic", DateTime.Parse("2/2/1982"), 0, "021/485-4584", "rapaja@uns.ac.rs", "123456789", "prof. dr.", 20);
        Profesor p2 = new Profesor(2, "Vladimir", "Dimitrieski", DateTime.Parse("2/2/1982"), 1, "021/485-2424", "dimitrieski@uns.ac.rs", "123456789", "vanr. prof. dr", 15);
        daoProfesor.AddObject(p0);
        daoProfesor.AddObject(p1);
        daoProfesor.AddObject(p2);

        DAO<Predmet> daoPredmet = new DAO<Predmet>(typeof(Predmet).Name + ".csv");
        Predmet pr0 = new Predmet(0, "bp1", "Baze podataka 1", SemestarEnum.zimski, 3, 2, 8);
        Predmet pr1 = new Predmet(1, "sau", "Sistemi automatskog upravljanja", SemestarEnum.letnji, 2, 1, 8);
        Predmet pr2 = new Predmet(2, "os", "Operativni sistemi", SemestarEnum.letnji, 2, 0, 8);
        Predmet pr3 = new Predmet(3, "mo", "Metode optimizacije", SemestarEnum.zimski, 3, 1, 8);
        daoPredmet.AddObject(pr0);
        daoPredmet.AddObject(pr1);
        daoPredmet.AddObject(pr2);
        daoPredmet.AddObject(pr3);

        // Possibly could change
        DAO<Student> daoStudent = new DAO<Student>(typeof(Student).Name + ".csv");
        Student s0 = new Student(0, "Nikola", "Kuslakovic", DateTime.Parse("2/2/2002"), 3, "123456789", "kuslakovic.ra8.2021@uns.ac.rs", 0, 3, StatusEnum.B, 0.0);
        Student s1 = new Student(1, "Nemanja", "Zekanovic", DateTime.Parse("2/2/2002"), 4, "123456789", "zekanovic.ra73.2021@uns.ac.rs", 1, 3, StatusEnum.S, 0.0);

        // Possibly could change
        DAO<StudentSlusaPredmet> daoStudentSlusaPredmet = new DAO<StudentSlusaPredmet>(typeof(StudentSlusaPredmet).Name + ".csv");
        StudentSlusaPredmet ssp0 = new StudentSlusaPredmet(0, 0, 0, PolozenPredmetEnum.nijePolozio);
        StudentSlusaPredmet ssp1 = new StudentSlusaPredmet(0, 0, 1, PolozenPredmetEnum.nijePolozio);
        StudentSlusaPredmet ssp2 = new StudentSlusaPredmet(0, 1, 0, PolozenPredmetEnum.nijePolozio);
        StudentSlusaPredmet ssp3 = new StudentSlusaPredmet(0, 1, 4, PolozenPredmetEnum.nijePolozio);
        daoStudentSlusaPredmet.AddObject(ssp0);
        daoStudentSlusaPredmet.AddObject(ssp1);
        daoStudentSlusaPredmet.AddObject(ssp2);
        daoStudentSlusaPredmet.AddObject(ssp3);

        DAO<ProfesorRadiNaKatedri> daoProfesorRadiNaKatedri = new DAO<ProfesorRadiNaKatedri>(typeof(ProfesorRadiNaKatedri).Name + ".csv");
        ProfesorRadiNaKatedri pnk0 = new ProfesorRadiNaKatedri(0, 0, 0);
        ProfesorRadiNaKatedri pnk1 = new ProfesorRadiNaKatedri(1, 1, 1);
        ProfesorRadiNaKatedri pnk2 = new ProfesorRadiNaKatedri(2, 2, 0);
        daoProfesorRadiNaKatedri.AddObject(pnk0);
        daoProfesorRadiNaKatedri.AddObject(pnk1);
        daoProfesorRadiNaKatedri.AddObject(pnk2);

        DAO<ProfesorPredajePredmet> daoProfesorPredajePredmet = new DAO<ProfesorPredajePredmet>(typeof(ProfesorPredajePredmet).Name + ".csv");
        ProfesorPredajePredmet ppp0 = new ProfesorPredajePredmet(0, 0, 2);
        ProfesorPredajePredmet ppp1 = new ProfesorPredajePredmet(1, 1, 1);
        ProfesorPredajePredmet ppp2 = new ProfesorPredajePredmet(2, 2, 0);
        ProfesorPredajePredmet ppp3 = new ProfesorPredajePredmet(3, 1, 3);
        daoProfesorPredajePredmet.AddObject(ppp0);
        daoProfesorPredajePredmet.AddObject(ppp1);
        daoProfesorPredajePredmet.AddObject(ppp2);
        daoProfesorPredajePredmet.AddObject(ppp3);

    }

}
