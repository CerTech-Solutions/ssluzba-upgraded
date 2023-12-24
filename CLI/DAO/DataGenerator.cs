using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Controller;
using CLI.Model;

namespace CLI.DAO;

public class DataGenerator
{
    public static void Generate()
    {
        
        /*
         * Deleting all csv file so new ones can be generated
         */
        string dataPath = AppDomain.CurrentDomain.BaseDirectory + "../../../../CLI/Data/";

        // Creating empty files
        Controller.Controller controller = new Controller.Controller();

        foreach (string file in Directory.GetFiles(dataPath, "*.csv"))
            File.Delete(file);

        Address a0 = new Address("Bulevar kralja Petra I", "9a", "Novi Sad", "Srbija");
        Address a1 = new Address("Bulevar vojvode Stepe", "11", "Novi Sad", "Srbija");
        Address a2 = new Address("Bulevar Jovan Ducica", "17", "Novi Sad", "Srbija");
        Address a3 = new Address("Bulevar oslobodjenja", "2", "Novi Sad", "Srbija");

        DAO<Professor> daoProfessor = new DAO<Professor>();
        Professor p0 = new Professor(0, "Veljko", "Petrovic", DateOnly.Parse("2/2/1982"), a0, "021/485-4564", "pveljko@uns.ac.rs", "123456789", "doc.", 10);
        Professor p1 = new Professor(1, "Milan", "Rapajic", DateOnly.Parse("2/2/1982"), a0, "021/485-4584", "rapaja@uns.ac.rs", "123456789", "prof.", 20);
        Professor p2 = new Professor(2, "Vladimir", "Dimitrieski", DateOnly.Parse("2/2/1982"), a1, "021/485-2424", "dimi@uns.ac.rs", "123456789", "prof.", 15);
        Professor p3 = new Professor(3, "Miodrag", "Djukic", DateOnly.Parse("2/2/1982"), a3, "021/480-1123", "misa@uns.ac.rs", "123456789", "prof.", 10);
        daoProfessor.AddObject(p0);
        daoProfessor.AddObject(p1);
        daoProfessor.AddObject(p2);
        daoProfessor.AddObject(p3);

        DAO<Department> daoDepartment = new DAO<Department>();
        Department k0 = new Department(0, "mat", "Katedra za PRN", p0);
        Department k1 = new Department(1, "aut", "Katedra za automatiku", p1);
        daoDepartment.AddObject(k0);
        daoDepartment.AddObject(k1);

        DAO<Subject> daoSubject = new DAO<Subject>();
        Subject pr0 = new Subject(0, "bp1", "Baze podataka 1", SemesterEnum.winter, 3, p2, 8);
        Subject pr1 = new Subject(1, "sau", "SAU", SemesterEnum.summer, 2, p1, 8);
        Subject pr2 = new Subject(2, "os", "Operativni sistemi", SemesterEnum.summer, 2, p0, 8);
        Subject pr3 = new Subject(3, "mo", "Metode optimizacije", SemesterEnum.winter, 3, p1, 8);
        daoSubject.AddObject(pr0);
        daoSubject.AddObject(pr1);
        daoSubject.AddObject(pr2);
        daoSubject.AddObject(pr3);

        DAO<Student> daoStudent = new DAO<Student>();
        Student s0 = new Student(0, "Nikola", "Kuslakovic", DateOnly.Parse("2/2/2002"), a2, "123456789", "kuslakovic.ra8.2021@uns.ac.rs", 3, StatusEnum.B, 10.0, new Model.Index("RA", 8, 2021));
        Student s1 = new Student(1, "Nemanja", "Zekanovic", DateOnly.Parse("2/2/2002"), a3, "123456789", "zekanovic.ra73.2021@uns.ac.rs", 3, StatusEnum.S, 8.0, new Model.Index("RA", 73, 2021));
        daoStudent.AddObject(s0);
        daoStudent.AddObject(s1);

        DAO<StudentTakesSubject> daoStudentTakesSubject = new DAO<StudentTakesSubject>();
        StudentTakesSubject ssp0 = new StudentTakesSubject(0, 1, 3, PassedSubjectEnum.PASSED);
        StudentTakesSubject ssp1 = new StudentTakesSubject(0, 1, 0, PassedSubjectEnum.PASSED);
        StudentTakesSubject ssp2 = new StudentTakesSubject(0, 0, 2, PassedSubjectEnum.PASSED);
        StudentTakesSubject ssp3 = new StudentTakesSubject(0, 1, 2, PassedSubjectEnum.NOTPASSED);
        daoStudentTakesSubject.AddObject(ssp0);
        daoStudentTakesSubject.AddObject(ssp1);
        daoStudentTakesSubject.AddObject(ssp2);
        daoStudentTakesSubject.AddObject(ssp3);

        DAO<ProfessorWorksAtDepartment> daoProfessorWorksAtDepartment = new DAO<ProfessorWorksAtDepartment>();
        ProfessorWorksAtDepartment pnk0 = new ProfessorWorksAtDepartment(0, 0, 0);
        ProfessorWorksAtDepartment pnk1 = new ProfessorWorksAtDepartment(1, 1, 1);
        ProfessorWorksAtDepartment pnk2 = new ProfessorWorksAtDepartment(2, 2, 0);
        daoProfessorWorksAtDepartment.AddObject(pnk0);
        daoProfessorWorksAtDepartment.AddObject(pnk1);
        daoProfessorWorksAtDepartment.AddObject(pnk2);

        DAO<ProfessorTeachesSubject> daoProfesorPredajePredmet = new DAO<ProfessorTeachesSubject>();
        ProfessorTeachesSubject ppp0 = new ProfessorTeachesSubject(0, 0, 2);
        ProfessorTeachesSubject ppp1 = new ProfessorTeachesSubject(1, 1, 1);
        ProfessorTeachesSubject ppp2 = new ProfessorTeachesSubject(2, 2, 0);
        ProfessorTeachesSubject ppp3 = new ProfessorTeachesSubject(3, 1, 3);
        daoProfesorPredajePredmet.AddObject(ppp0);
        daoProfesorPredajePredmet.AddObject(ppp1);
        daoProfesorPredajePredmet.AddObject(ppp2);
        daoProfesorPredajePredmet.AddObject(ppp3);

        DAO<Grade> daoGrade = new DAO<Grade>();
        Grade o0 = new Grade(0, s1, pr3, 6, DateOnly.Parse("2/2/2002"));
        Grade o1 = new Grade(1, s1, pr0, 10, DateOnly.Parse("2/2/2002"));
        Grade o2 = new Grade(2, s0, pr2, 10, DateOnly.Parse("2/2/2002"));
        daoGrade.AddObject(o0);
        daoGrade.AddObject(o1);
        daoGrade.AddObject(o2);
    }

}
