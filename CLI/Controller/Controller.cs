using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
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

            prof.Subjects.Add(p);
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

        // Racunanje prosecne ocene
        daoStudent.GetAllObjects().ForEach(s => s.CalculateGPA());
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

    public List<Department> GetAllDepartments()
    {
        return daoDepartment.GetAllObjects();
    }

    public List<Subject> GetSubjectsForStudent(int studentId)
    {
        Student stud = daoStudent.GetObjectById(studentId);

        return daoSubject.GetAllObjects().FindAll(
            s => s.YearOfStudy == stud.CurrentYear 
            && !stud.NotPassedSubjects.Exists(sub => sub.Id == s.Id)
            && !stud.PassedSubjects.Exists(sub => sub.Subject.Id == s.Id));
    }

    public List<Subject> GetSubjectsForProfessor(int professorId)
    {
        Professor prof = daoProfessor.GetObjectById(professorId);

        return daoSubject.GetAllObjects().FindAll(
            s => s.Professor == null
            && !prof.Subjects.Exists(sub => sub.Id == s.Id));
    }

    public void AddSubject(Subject subject)
    {
        daoSubject.AddObject(subject);
        subject.Professor = daoProfessor.GetObjectById(subject.Professor.Id);
        daoProfessorTeachesSubject.AddObject(new ProfessorTeachesSubject(0, subject.Professor.Id, subject.Id));

        publisher.NotifyObservers();
    }

    public void UpdateSubject(Subject subject)
    {
        // Ovo azurira sva polja sem profesora
        Subject oldSubject = daoSubject.UpdateObject(subject);

        if (oldSubject.Professor == null && subject.Professor != null)
        {
            AddSubjectToProfessor(oldSubject.Id, subject.Professor.Id);
        }
        else if (oldSubject.Professor != null && subject.Professor == null)
        {
            DeleteSubjectFromProfessorList(oldSubject.Id, oldSubject.Professor.Id);

        }
        else if (oldSubject.Professor.Id != subject.Professor.Id)
        {
            ProfessorTeachesSubject pts = daoProfessorTeachesSubject.GetAllObjects().Find(pts => pts.IdSub == subject.Id && pts.IdProf == oldSubject.Professor.Id);
            pts.IdProf = subject.Professor.Id;
            daoProfessorTeachesSubject.UpdateObject(pts);

            Professor oldProf = daoProfessor.GetObjectById(oldSubject.Professor.Id);
            Professor newProf = daoProfessor.GetObjectById(subject.Professor.Id);

            oldProf.Subjects.Remove(oldSubject);
            newProf.Subjects.Add(oldSubject);

            oldSubject.Professor = newProf;
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

    public void AddGrade(Grade grade)
    {
        Grade temp = daoGrade.GetAllObjects().Find(o =>
            o.Student.Id == grade.Student.Id && o.Subject.Id == grade.Subject.Id);

        if (temp != null) throw new Exception("Student already has grade for that subject!");

        StudentTakesSubject sts = daoStudentTakesSubject.GetAllObjects().Find(sts =>
            sts.IdStud == grade.Student.Id && sts.IdSub == grade.Subject.Id);

        Student s = daoStudent.GetObjectById(grade.Student.Id);
        Subject p = daoSubject.GetObjectById(grade.Subject.Id);

        daoGrade.AddObject(grade);
        daoStudentTakesSubject.RemoveObject(sts.Id);

        s.PassedSubjects.Add(grade);
        s.NotPassedSubjects.Remove(p);
        s.CalculateGPA();

        p.StudentsPassed.Add(s);
        p.StudentsNotPassed.Remove(s);
        
        publisher.NotifyObservers();
    }

    public void DeleteGrade(Grade grade)
    {
        Student s = daoStudent.GetObjectById(grade.Student.Id);
        Subject p = daoSubject.GetObjectById(grade.Subject.Id);
        Grade g = daoGrade.GetObjectById(grade.Id);

        s.PassedSubjects.Remove(g);
        s.NotPassedSubjects.Add(p);
        s.CalculateGPA();

        p.StudentsPassed.Remove(s);
        p.StudentsNotPassed.Add(s);

        daoGrade.RemoveObject(grade.Id);
        daoStudentTakesSubject.AddObject(new StudentTakesSubject(0, grade.Student.Id, grade.Subject.Id));

        publisher.NotifyObservers();
    }
    
    public void AddSubjectToStudent(int subjectId, int studentId)
    {
        Subject sub = daoSubject.GetObjectById(subjectId);
        Student stud = daoStudent.GetObjectById(studentId);

        stud.NotPassedSubjects.Add(sub);
        sub.StudentsNotPassed.Add(stud);

        daoStudentTakesSubject.AddObject(new StudentTakesSubject(0, studentId, subjectId));

        publisher.NotifyObservers();
    }

    public void DeleteSubjectFromStudentList(int subjectId, int studentId)
    {
        Subject sub = daoSubject.GetObjectById(subjectId);
        Student stud = daoStudent.GetObjectById(studentId);

        stud.NotPassedSubjects.Remove(sub);
        sub.StudentsNotPassed.Remove(stud);

        StudentTakesSubject sts = daoStudentTakesSubject.GetAllObjects().Find(sts =>
            sts.IdStud == studentId && sts.IdSub == subjectId);

        daoStudentTakesSubject.RemoveObject(sts.Id);

        publisher.NotifyObservers();
    }

    public void AddSubjectToProfessor(int subjectId, int professorId)
    {
        Subject sub = daoSubject.GetObjectById(subjectId);
        Professor prof = daoProfessor.GetObjectById(professorId);

        prof.Subjects.Add(sub);
        sub.Professor = prof;

        daoProfessorTeachesSubject.AddObject(new ProfessorTeachesSubject(0, professorId, subjectId));

        publisher.NotifyObservers();
    }

    public void DeleteSubjectFromProfessorList(int subjectId, int professorId)
    {
        Subject sub = daoSubject.GetObjectById(subjectId);
        Professor prof = daoProfessor.GetObjectById(professorId);

        prof.Subjects.Remove(sub);
        sub.Professor = null;

        ProfessorTeachesSubject pts = daoProfessorTeachesSubject.GetAllObjects().Find(pts =>
                   pts.IdProf == professorId && pts.IdSub == subjectId);

        daoProfessorTeachesSubject.RemoveObject(pts.Id);

        publisher.NotifyObservers();
    }

    public void AddChiefToDepartment(int professorId, int departmentId)
    {
        Professor prof = daoProfessor.GetObjectById(professorId);
        Department dep = daoDepartment.GetObjectById(departmentId);

        // Add additional check for professor title
        if(prof.ServiceYears < 5)
            throw new Exception("Professor must have at least 5 years of service\nto become chief of department!");

        dep.Chief = prof;

        daoDepartment.SaveToStorage();
        publisher.NotifyObservers();
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
