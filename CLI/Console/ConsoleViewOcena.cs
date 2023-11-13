using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class ConsoleViewOcena : ConsoleView<Ocena>
{
    public ConsoleViewOcena(HeadDAO headDAO) : base(headDAO) { }

    public override void ShowAll()
    {
        PrintObjects(_headDAO.daoOcena.GetAllObjects());
    }
    public override void AddObject()
    {
        Ocena obj = new Ocena();
        InputObject(obj);

        try
        {
            _headDAO.AddOcena(obj);
            System.Console.WriteLine("Ocena added!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            System.Console.WriteLine("Ocena not added!");
        }
    }

    public override void UpdateObject()
    {
        Ocena o = ConsoleViewUtils.SafeInputOcenaId(_headDAO.daoOcena, true);
        if (o == null) return;

        InputObject(o, true);
        _headDAO.RemoveOcena(o);
        _headDAO.AddOcena(o);
    }

    public override void RemoveObject()
    {
        Ocena oc = ConsoleViewUtils.SafeInputOcenaId(_headDAO.daoOcena);
        _headDAO.RemoveOcena(oc);
    }

}
