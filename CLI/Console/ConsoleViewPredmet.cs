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
        Predmet? obj = InputObject();
        _headDAO.daoPredmet.AddObject(obj);
    }

    public override void UpdateObject()
    {
        Predmet p = ConsoleViewUtils.SafeInputPredmetId(_headDAO.daoPredmet);
        Predmet? obj = InputObject();
        obj.Id = p.Id;
        _headDAO.daoPredmet.UpdateObject(obj);
    }

    public override void RemoveObject()
    {
        Predmet p = ConsoleViewUtils.SafeInputPredmetId(_headDAO.daoPredmet);
        try
        {
            _headDAO.CheckDeletePredmet(p.Id);
            _headDAO.daoOcena.RemoveObject(p.Id);
            System.Console.WriteLine("Predmet deleted successfully!");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            System.Console.WriteLine("Predmet was not deleted!");
        }
    }
}
