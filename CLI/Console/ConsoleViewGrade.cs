using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Controller;
using CLI.Model;

namespace CLI.Console;

public class ConsoleViewGrade : ConsoleView<Grade>
{
    public ConsoleViewGrade(Controller.Controller controller) : base(controller) { }

    public override void ShowAll()
    {
        PrintObjects(_controller.daoGrade.GetAllObjects());
    }
    public override void AddObject()
    {
        Grade obj = new Grade();
        InputObject(obj);

        try
        {
            _controller.AddGrade(obj);
            ConsoleViewUtils.ConsoleWriteLineColor("Grade added successfully!", ConsoleColor.Green);
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            ConsoleViewUtils.ConsoleWriteLineColor("Grade was not added!", ConsoleColor.Red);
        }
    }

    public override void UpdateObject()
    {
        Grade o = ConsoleViewUtils.SafeInputGreadeId(_controller.daoGrade, true);
        if (o == null) return;

        InputObject(o, true);
        _controller.DeleteGrade(o);
        _controller.AddGrade(o);
        ConsoleViewUtils.ConsoleWriteLineColor("Grade updated successfully!", ConsoleColor.Green);
    }

    public override void RemoveObject()
    {
        Grade oc = ConsoleViewUtils.SafeInputGreadeId(_controller.daoGrade);
        _controller.DeleteGrade(oc);
        ConsoleViewUtils.ConsoleWriteLineColor("Grade removed successfully!", ConsoleColor.Green);
    }

}
