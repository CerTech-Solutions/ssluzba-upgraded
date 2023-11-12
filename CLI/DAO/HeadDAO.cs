using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;

namespace CLI.DAO;

public class HeadDAO
{
    public DAO<Student> daoStudent;
    public DAO<Profesor> daoProfesor;
    public DAO<Katedra> daoKatedra;
    public DAO<Ocena> daoOcena;
    public DAO<Predmet> daoPredmet;
    private DAO<ProfesorPredajePredmet> daoProfesorPredajePredmet;
    public DAO<ProfesorRadiNaKatedri> daoProfesorRadiNaKatedri;
    public DAO<StudentSlusaPredmet> daoStudentSlusaPredmet;

    public HeadDAO() 
    {
        daoStudent = new DAO<Student>();
        daoProfesor = new DAO<Profesor>();
        daoKatedra = new DAO<Katedra>();
        daoOcena = new DAO<Ocena>();
        daoPredmet = new DAO<Predmet>();
        daoProfesorPredajePredmet = new DAO<ProfesorPredajePredmet>();
        daoProfesorRadiNaKatedri = new DAO<ProfesorRadiNaKatedri>();
        daoStudentSlusaPredmet = new DAO<StudentSlusaPredmet>();

        LinkObjectLists();
    }

    private void LinkObjectLists()
    {
        // Povezivanje predmeta-studentima
        foreach (StudentSlusaPredmet ssp in daoStudentSlusaPredmet.GetAllObjects())
        {
            Student s = daoStudent.GetObjectById(ssp.IdStud);
            Predmet p = daoPredmet.GetObjectById(ssp.IdPred);

            if (ssp.Status == PolozenPredmetEnum.nijePolozio)
            {
                s.NepolozeniPredmeti.Add(p);
                p.StudentiNisuPolozili.Add(s);
            }  
            else
            {
                s.PolozeniPredmeti.Add(p);
                p.StudentiPolozili.Add(s);
            }
        }

        // Povezivanje predmet-profesor
        foreach (ProfesorPredajePredmet ppp in daoProfesorPredajePredmet.GetAllObjects())
        {
            Profesor prof = daoProfesor.GetObjectById(ppp.IdProf);
            Predmet p = daoPredmet.GetObjectById(ppp.IdPred);

            prof.Predmeti.Add(p);
            p.Profesor = prof; 
        }

        // Povezivanje profesor-katedra
        foreach (ProfesorRadiNaKatedri prnk in daoProfesorRadiNaKatedri.GetAllObjects())
        {
            Profesor prof = daoProfesor.GetObjectById(prnk.IdProf);
            Katedra kat = daoKatedra.GetObjectById(prnk.IdKat);

            kat.Profesori.Add(prof);
        }

        // Povezivanje sefa katedre-katedra
        foreach (Katedra k in daoKatedra.GetAllObjects())
        {
            k.SefKatedre = daoProfesor.GetObjectById(k.SefKatedre.Id);
        }

        // Povezivanje ocena-predmet-student
        foreach (Ocena o in daoOcena.GetAllObjects())
        {
            o.Student = daoStudent.GetObjectById(o.Student.Id);
            o.Predmet = daoPredmet.GetObjectById(o.Predmet.Id);
        }
    }

    public void DeleteDepartmant(int katedraId) 
    {
        ProfesorRadiNaKatedri prk = daoProfesorRadiNaKatedri.GetAllObjects().Find(prk => prk.IdKat == katedraId);
        if (prk != null) throw new Exception("Na datoj katedri predaju neki profesori!");

        daoKatedra.RemoveObject(katedraId);
    }

    public void DeletePredmet(int predmetId)
    {
        ProfesorPredajePredmet ppp = daoProfesorPredajePredmet.GetAllObjects().Find(ppp => ppp.IdPred == predmetId);
        if (ppp != null) throw new Exception("Dati predmet predaju neki profesori!");

        StudentSlusaPredmet sss = daoStudentSlusaPredmet.GetAllObjects().Find(sss => sss.IdPred == predmetId);
        if (ppp != null) throw new Exception("Dati predmet slusaju neki studenti!");

        Ocena oc = daoOcena.GetAllObjects().Find(oc => oc.Predmet.Id == predmetId);
        if (oc != null) throw new Exception("Neki studenti imaju ocenu iz datog predmeta!");

        daoPredmet.RemoveObject(predmetId);
    }

    public void DeleteProfesor(int profesorId)
    {
        ProfesorPredajePredmet ppp = daoProfesorPredajePredmet.GetAllObjects().Find(ppp => ppp.IdProf == profesorId);
        if (ppp != null) throw new Exception("Dati profesor predaje neke predmete!");

        ProfesorRadiNaKatedri prk = daoProfesorRadiNaKatedri.GetAllObjects().Find(prk => prk.IdProf == profesorId);
        if (prk != null) throw new Exception("Dati profesor predaje na nekoj katedri!");

        daoProfesor.RemoveObject(profesorId);
    }

    public void DeleteStudent(int studentId)
    {
        StudentSlusaPredmet sss = daoStudentSlusaPredmet.GetAllObjects().Find(sss => sss.IdStud == studentId);
        if (sss != null) throw new Exception("Dati student slusa neki predmet!");

        Ocena oc = daoOcena.GetAllObjects().Find(oc => oc.Student.Id == studentId);
        if (oc != null) throw new Exception("Dati student ima neke ocene!");

        daoStudent.RemoveObject(studentId);
    }
}
