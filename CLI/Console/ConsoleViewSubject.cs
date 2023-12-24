using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using CLI.Controller;

namespace CLI.Console;

public class ConsoleViewSubject : ConsoleView<Subject>
{
    public ConsoleViewSubject(Controller.Controller controller) : base(controller) { }

    public override void ShowAll()
    {
        PrintObjects(_controller.daoSubject.GetAllObjects());
    }

    public override void AddObject()
    {
        Subject obj = new Subject();
        InputObject(obj);
        _controller.daoSubject.AddObject(obj);
        ConsoleViewUtils.ConsoleWriteLineColor("Subject added successfully!", ConsoleColor.Green);
    }

    public override void UpdateObject()
    {
        Subject p = ConsoleViewUtils.SafeInputSubjectId(_controller.daoSubject, true);
        if (p == null) return;

        InputObject(p, true);
        _controller.daoSubject.UpdateObject(p);
        ConsoleViewUtils.ConsoleWriteLineColor("Subject updated successfully!", ConsoleColor.Green);
    }

    public override void RemoveObject()
    {
        Subject p = ConsoleViewUtils.SafeInputSubjectId(_controller.daoSubject, true);
        if (p == null) return;

        try
        {
            _controller.DeleteSubject(p.Id);
            ConsoleViewUtils.ConsoleWriteLineColor("Subject removed successfully!", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            ConsoleViewUtils.ConsoleWriteLineColor("Subject was not removed!", ConsoleColor.Red);
        }
    }
}
