﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class ConsoleViewStudent : ConsoleView<Student>
{
    public ConsoleViewStudent(HeadDAO headDAO) : base(headDAO) { }

    public override void ShowAll()
    {
        PrintObjects(_headDAO.daoStudent.GetAllObjects());
    }

    public override void AddObject()
    {
        Student obj = new Student();
        InputObject(obj);
        _headDAO.daoStudent.AddObject(obj);
    }

    public override void UpdateObject()
    {
        Student stud = ConsoleViewUtils.SafeInputStudentId(_headDAO.daoStudent, true);
        if (stud == null) return;

        InputObject(stud, true);
        _headDAO.daoStudent.UpdateObject(stud);
    }

    public override void RemoveObject()
    {
        Student stud = ConsoleViewUtils.SafeInputStudentId(_headDAO.daoStudent, true);
        if (stud == null) return;

        try
        {
            _headDAO.DeleteStudent(stud.Id);
            System.Console.WriteLine("Student deleted successfully!");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            System.Console.WriteLine("Student was not deleted!");
        }
    }
}
