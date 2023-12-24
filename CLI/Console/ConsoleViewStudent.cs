using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Controller;
using CLI.Model;

namespace CLI.Console;

public class ConsoleViewStudent : ConsoleView<Student>
{
    public ConsoleViewStudent(Controller.Controller controller) : base(controller) { }

    public override void ShowAll()
    {
        PrintObjects(_controller.daoStudent.GetAllObjects());
    }

    public override void AddObject()
    {
        Student obj = new Student();
        InputObject(obj);
        _controller.daoStudent.AddObject(obj);
        ConsoleViewUtils.ConsoleWriteLineColor("Student added successfully!", ConsoleColor.Green);
    }

    public override void UpdateObject()
    {
        Student stud = ConsoleViewUtils.SafeInputStudentId(_controller.daoStudent, true);
        if (stud == null) return;

        InputObject(stud, true);
        _controller.daoStudent.UpdateObject(stud);
        ConsoleViewUtils.ConsoleWriteLineColor("Student updated successfully!", ConsoleColor.Green);
    }

    public override void RemoveObject()
    {
        Student stud = ConsoleViewUtils.SafeInputStudentId(_controller.daoStudent, true);
        if (stud == null) return;

        try
        {
            _controller.DeleteStudent(stud.Id);
            ConsoleViewUtils.ConsoleWriteLineColor("Student removed successfully!", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            ConsoleViewUtils.ConsoleWriteLineColor("Student was not removed!", ConsoleColor.Red);
        }
    }
}
