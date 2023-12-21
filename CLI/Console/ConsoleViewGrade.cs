using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class ConsoleViewGrade : ConsoleView<Grade>
{
    public ConsoleViewGrade(HeadDAO headDAO) : base(headDAO) { }

    public override void ShowAll()
    {
        PrintObjects(_headDAO.daoGrade.GetAllObjects());
    }
    public override void AddObject()
    {
        Grade obj = new Grade();
        InputObject(obj);

        try
        {
            _headDAO.AddOcena(obj);
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
        Grade o = ConsoleViewUtils.SafeInputGreadeId(_headDAO.daoGrade, true);
        if (o == null) return;

        InputObject(o, true);
        _headDAO.DeleteGrade(o);
        _headDAO.AddOcena(o);
        ConsoleViewUtils.ConsoleWriteLineColor("Grade updated successfully!", ConsoleColor.Green);
    }

    public override void RemoveObject()
    {
        Grade oc = ConsoleViewUtils.SafeInputGreadeId(_headDAO.daoGrade);
        _headDAO.DeleteGrade(oc);
        ConsoleViewUtils.ConsoleWriteLineColor("Grade removed successfully!", ConsoleColor.Green);
    }

}
