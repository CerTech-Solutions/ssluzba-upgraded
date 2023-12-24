using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Controller;
using CLI.Model;

namespace CLI.Console;

public class ConsoleViewProfessor : ConsoleView<Professor>
{
    public ConsoleViewProfessor(Controller.Controller controller) : base(controller) { }

    public override void ShowAll()
    {
        PrintObjects(_controller.daoProfessor.GetAllObjects());
    }

    public override void AddObject()
    {
        Professor obj = new Professor();
        InputObject(obj);
        _controller.daoProfessor.AddObject(obj);
        ConsoleViewUtils.ConsoleWriteLineColor("Professor added successfully!", ConsoleColor.Green);
    }

    public override void UpdateObject()
    {
        Professor prof = ConsoleViewUtils.SafeInputProfessorId(_controller.daoProfessor, true);
        if (prof == null) return;

        InputObject(prof, true);
        _controller.daoProfessor.UpdateObject(prof);
        ConsoleViewUtils.ConsoleWriteLineColor("Professor updated successfully!", ConsoleColor.Green);
    }

    public override void RemoveObject()
    {
        Professor prof = ConsoleViewUtils.SafeInputProfessorId(_controller.daoProfessor, true);
        if (prof == null) return;

        try
        {
            _controller.DeleteProfesor(prof.Id);
            ConsoleViewUtils.ConsoleWriteLineColor("Professor removed successfully!", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            ConsoleViewUtils.ConsoleWriteLineColor("Professor was not removed!", ConsoleColor.Red);
        }
    }

}
