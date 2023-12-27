using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Model;
using CLI.Observer;

namespace CLI.Controller;

public class Controller
{
    public DAO<Student> daoStudent;
    public DAO<Professor> daoProfessor;
    public DAO<Department> daoDepartment;
    public DAO<Grade> daoGrade;
    public DAO<Subject> daoSubject;
    public DAO<ProfessorTeachesSubject> daoProfessorTeachesSubject;
    public DAO<ProfessorWorksAtDepartment> daoProfessorWorksAtDepartment;
    public DAO<StudentTakesSubject> daoStudentTakesSubject;

    public Publisher publisher;

    public Controller()
    {
        publisher = new Publisher();
        daoStudent = new DAO<Student>();
        daoProfessor = new DAO<Professor>();
        daoDepartment = new DAO<Department>();
        daoGrade = new DAO<Grade>();
        daoSubject = new DAO<Subject>();
        daoProfessorTeachesSubject = new DAO<ProfessorTeachesSubject>();
        daoProfessorWorksAtDepartment = new DAO<ProfessorWorksAtDepartment>();
        daoStudentTakesSubject = new DAO<StudentTakesSubject>();

        LinkObjectLists();
    }

    private void LinkObjectLists()
    {
        // Povezivanje nepolozeni predmet-student
        foreach (StudentTakesSubject ssp in daoStudentTakesSubject.GetAllObjects())
        {
            Student s = daoStudent.GetObjectById(ssp.IdStud);
            Subject p = daoSubject.GetObjectById(ssp.IdSub);

            s.NotPassedSubjects.Add(p);
            p.StudentsNotPassed.Add(s);
        }

        //Povezivanje ocena-student-predmet
        foreach(Grade g in daoGrade.GetAllObjects())
        {
            Student s = daoStudent.GetObjectById(g.Student.Id);
            Subject p = daoSubject.GetObjectById(g.Subject.Id);

            g.Student = s;
            g.Subject = p;
            s.PassedSubjects.Add(g);
            p.StudentsPassed.Add(s);
        }   

        // Povezivanje predmet-profesor
        foreach (ProfessorTeachesSubject ppp in daoProfessorTeachesSubject.GetAllObjects())
        {
            Professor prof = daoProfessor.GetObjectById(ppp.IdProf);
            Subject p = daoSubject.GetObjectById(ppp.IdSub);

            prof.Predmeti.Add(p);
            p.Professor = prof;
        }

        // Povezivanje profesor-katedra
        foreach (ProfessorWorksAtDepartment prnk in daoProfessorWorksAtDepartment.GetAllObjects())
        {
            Professor prof = daoProfessor.GetObjectById(prnk.IdProf);
            Department kat = daoDepartment.GetObjectById(prnk.IdDep);

            kat.Professors.Add(prof);
        }

        // Povezivanje sefa katedre-katedra
        foreach (Department k in daoDepartment.GetAllObjects())
        {
            k.Chief = daoProfessor.GetObjectById(k.Chief.Id);
        }

        // Povezivanje ocena-predmet-student
        foreach (Grade o in daoGrade.GetAllObjects())
        {
            o.Student = daoStudent.GetObjectById(o.Student.Id);
            o.Subject = daoSubject.GetObjectById(o.Subject.Id);
        }
    }

    public List<Student> GetAllStudents()
    {
        return daoStudent.GetAllObjects();
    }

    public List<Professor> GetAllProfessors()
    {
        return daoProfessor.GetAllObjects();
    }

    public List<Subject> GetAllSubjects()
    {
        return daoSubject.GetAllObjects();
    }

    public void AddSubject(Subject subject)
    {
        daoSubject.AddObject(subject);
        subject.Professor = daoProfessor.GetObjectById(subject.Professor.Id);
        daoProfessorTeachesSubject.AddObject(new ProfessorTeachesSubject(0, subject.Professor.Id, subject.Id));

        publisher.NotifyObservers();
    }

    public void UpdateSubject(Subject subject, int oldProfessorId)
    {
        daoSubject.UpdateObject(subject);
        if (subject.Professor.Id != oldProfessorId)
        {
            ProfessorTeachesSubject pts = daoProfessorTeachesSubject.GetAllObjects().Find(pts => pts.IdSub == subject.Id && pts.IdProf == oldProfessorId);
            pts.IdProf = subject.Professor.Id;
            daoProfessorTeachesSubject.UpdateObject(pts);
        }
        publisher.NotifyObservers();
    }
    
    public void AddProfessor(Professor professor)
    {
        daoProfessor.AddObject(professor);
        publisher.NotifyObservers();
    }

    public void UpdateProfessor(Professor professor)
    {
        daoProfessor.UpdateObject(professor);
        publisher.NotifyObservers();
    }

    public void AddStudent(Student student)
    {
        daoStudent.AddObject(student);
        publisher.NotifyObservers();
    }

    public void UpdateStudent(Student student)
    {
        daoStudent.UpdateObject(student);
        publisher.NotifyObservers();
    }

    public void DeleteDepartmant(int katedraId)
    {
        ProfessorWorksAtDepartment prk = daoProfessorWorksAtDepartment.GetAllObjects().Find(prk => prk.IdDep == katedraId);
        if (prk != null) throw new Exception("Chosen department has some professors that teach there!");

        daoDepartment.RemoveObject(katedraId);
    }

    public void DeleteSubject(int predmetId)
    {
        ProfessorTeachesSubject ppp = daoProfessorTeachesSubject.GetAllObjects().Find(ppp => ppp.IdSub == predmetId);
        if (ppp != null) throw new Exception("Chosen subject is taught by some professors!");

        StudentTakesSubject sss = daoStudentTakesSubject.GetAllObjects().Find(sss => sss.IdSub == predmetId);
        if (ppp != null) throw new Exception("Chosen subject is taken by some students!");

        Grade oc = daoGrade.GetAllObjects().Find(oc => oc.Subject.Id == predmetId);
        if (oc != null) throw new Exception("Some students have grade in that subject!");

        daoSubject.RemoveObject(predmetId);
        publisher.NotifyObservers();
    }

    public void DeleteProfesor(int profesorId)
    {
        ProfessorTeachesSubject ppp = daoProfessorTeachesSubject.GetAllObjects().Find(ppp => ppp.IdProf == profesorId);
        if (ppp != null) throw new Exception("Chosen professor teaches some subjects!");

        ProfessorWorksAtDepartment prk = daoProfessorWorksAtDepartment.GetAllObjects().Find(prk => prk.IdProf == profesorId);
        if (prk != null) throw new Exception("Chosen professor  teaches at some department!");

        daoProfessor.RemoveObject(profesorId);
        publisher.NotifyObservers();
    }

    public void DeleteStudent(int studentId)
    {
        StudentTakesSubject sss = daoStudentTakesSubject.GetAllObjects().Find(sss => sss.IdStud == studentId);
        if (sss != null) throw new Exception("Chosen student takes some subjects!");

        Grade oc = daoGrade.GetAllObjects().Find(oc => oc.Student.Id == studentId);
        if (oc != null) throw new Exception("Chosen student has some grades!");

        daoStudent.RemoveObject(studentId);
        publisher.NotifyObservers();
    }

    public void AddOcena(Grade ocena)
    {
        Grade temp = daoGrade.GetAllObjects().Find(o =>
            o.Student.Id == ocena.Student.Id && o.Subject.Id == ocena.Subject.Id);

        if (temp != null) throw new Exception("Student already has grade for that subject!");

        daoGrade.AddObject(ocena);

        StudentTakesSubject sss = daoStudentTakesSubject.GetAllObjects().Find(sss =>
            sss.IdSub == ocena.Subject.Id && sss.IdStud == ocena.Student.Id);

        if (sss != null)
            sss.Status = PassedSubjectEnum.PASSED;

        PassSubjectForStudent(ocena.Student, ocena.Subject);
        CalculateGPA(ocena.Student);
    }

    public void DeleteGrade(Grade grade)
    {
        /*StudentTakesSubject sss = daoStudentTakesSubject.GetAllObjects().Find(sss =>
             sss.IdSub == grade.Subject.Id && sss.IdStud == grade.Student.Id);

        if (sss != null)
            sss.Status = PassedSubjectEnum.NOTPASSED;

        grade.Student.NotPassedSubjects.Add(grade.Subject);
        grade.Student.PassedSubjects.Remove(grade.Subject);

        daoGrade.RemoveObject(grade.Id);
        CalculateGPA(grade.Student);*/
    }

    public void PassSubjectForStudent(Student student, Subject subject)
    {
        /*student.PassedSubjects.Add(subject);
        Subject np = student.NotPassedSubjects.Find(np => np.Id == subject.Id);

        if (np == null) return;

        student.NotPassedSubjects.Remove(np);*/
    }

    public void CalculateGPA(Student student)
    {
        student.GPA = 0.0;
        int count = 0;

        foreach (Grade o in daoGrade.GetAllObjects())
        {
            if (o.Student.Id == student.Id)
            {
                student.GPA += o.GradeValue;
                count++;
            }
        }

        student.GPA /= count;
    }
    
    public void SaveAllToStorage()
    {
        daoStudent.SaveToStorage();
        daoProfessor.SaveToStorage();
        daoDepartment.SaveToStorage();
        daoGrade.SaveToStorage();
        daoSubject.SaveToStorage();
        daoProfessorTeachesSubject.SaveToStorage();
        daoProfessorWorksAtDepartment.SaveToStorage();
        daoStudentTakesSubject.SaveToStorage();
    }
}
