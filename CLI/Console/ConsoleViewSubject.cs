using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using CLI.DAO;

namespace CLI.Console;

public class ConsoleViewSubject : ConsoleView<Subject>
{
    public ConsoleViewSubject(HeadDAO headDAO) : base(headDAO) { }

    public override void ShowAll()
    {
        PrintObjects(_headDAO.daoSubject.GetAllObjects());
    }

    public override void AddObject()
    {
        Subject obj = new Subject();
        InputObject(obj);
        _headDAO.daoSubject.AddObject(obj);
    }

    public override void UpdateObject()
    {
        Subject p = ConsoleViewUtils.SafeInputSubjectId(_headDAO.daoSubject, true);
        if (p == null) return;

        InputObject(p, true);
        _headDAO.daoSubject.UpdateObject(p);
    }

    public override void RemoveObject()
    {
        Subject p = ConsoleViewUtils.SafeInputSubjectId(_headDAO.daoSubject, true);
        if (p == null) return;

        try
        {
            _headDAO.DeletePredmet(p.Id);
            System.Console.WriteLine("Predmet deleted successfully!");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            System.Console.WriteLine("Predmet was not deleted!");
        }
    }
}
