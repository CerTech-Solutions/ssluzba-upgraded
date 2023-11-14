using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class ConsoleViewDepartment : ConsoleView<Department>
{
    public ConsoleViewDepartment(HeadDAO headDAO) : base(headDAO) { }

    public override void ShowAll()
    {
        PrintObjects(_headDAO.daoDepartment.GetAllObjects());
    }

    public override void AddObject()
    {
        Department obj = new Department();
        InputObject(obj);
        _headDAO.daoDepartment.AddObject(obj);
        ConsoleViewUtils.ConsoleWriteLineColor("Department added successfully!", ConsoleColor.Green);
    }

    public override void UpdateObject()
    {
        Department kat = ConsoleViewUtils.SafeInputDepartmentId(_headDAO.daoDepartment, true);
        if (kat == null) return;

        InputObject(kat, true);
        _headDAO.daoDepartment.UpdateObject(kat);
        ConsoleViewUtils.ConsoleWriteLineColor("Department updated successfully!", ConsoleColor.Green);
    }

    public override void RemoveObject()
    {
        Department kat = ConsoleViewUtils.SafeInputDepartmentId(_headDAO.daoDepartment, true);
        if (kat == null) return;

        try
        {
            _headDAO.DeleteDepartmant(kat.Id);
            ConsoleViewUtils.ConsoleWriteLineColor("Department deleted successfully!", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            ConsoleViewUtils.ConsoleWriteLineColor("Department was not removed!", ConsoleColor.Red);
        }
    }
}
