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
        _headDAO.daoOcena.AddObject(obj);
    }

    public override void UpdateObject()
    {
        Ocena o = ConsoleViewUtils.SafeInputOcenaId(_headDAO.daoOcena, true);
        if (o == null) return;

            InputObject(o, true);
        _headDAO.daoOcena.UpdateObject(o);
    }

    public override void RemoveObject()
    {
        Ocena oc = ConsoleViewUtils.SafeInputOcenaId(_headDAO.daoOcena);
        _headDAO.daoOcena.RemoveObject(oc.Id);
    }

}
