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

    public void CheckAddDepartmentChief(int katedraId, int chiefId)
    {
        Katedra k = daoKatedra.GetObjectById(katedraId);
        if (k == null) throw new Exception("Katedra sa datim ID-jem ne postoji!");

        Profesor p = daoProfesor.GetObjectById(chiefId);
        if (p == null) throw new Exception("Profesor sa datim ID-jem ne postoji!");

        p = k.Profesori.Find(p => p.Id == chiefId);
        if (p == null) throw new Exception("Profesor ne radi na datoj katedri!");

    }

    public void CheckDeleteDepartmant(int katedraId) 
    {
        
    }

    public void CheckAddOcena(int studentId, int predmetId)
    {

    }

    public void CheckAddPredmet(int profesorId)
    {

    }

    public void CheckDeletePredmet(int predmetId)
    {

    }

    public void CheckDeleteProfesor(int profesorId)
    {

    }

    public void CheckDeleteStudent(int studentId)
    {

    }
}
