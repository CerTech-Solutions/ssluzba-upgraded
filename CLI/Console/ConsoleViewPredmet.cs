using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using CLI.DAO;

namespace CLI.Console;

public class ConsoleViewPredmet : ConsoleView<Predmet>
{
    public ConsoleViewPredmet(HeadDAO headDAO) : base(headDAO) { }

    public override void ShowAll()
    {
        PrintObjects(_headDAO.daoPredmet.GetAllObjects());
    }

    public override void AddObject()
    {
        Predmet obj = new Predmet();
        InputObject(obj);
        _headDAO.daoPredmet.AddObject(obj);
    }

    public override void UpdateObject()
    {
        Predmet p = ConsoleViewUtils.SafeInputPredmetId(_headDAO.daoPredmet, true);
        if (p == null)

        InputObject(p, true);
        _headDAO.daoPredmet.UpdateObject(p);
    }

    public override void RemoveObject()
    {
        Predmet p = ConsoleViewUtils.SafeInputPredmetId(_headDAO.daoPredmet, true);
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
