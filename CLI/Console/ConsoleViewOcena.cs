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

    public override void RemoveObject()
    {
        Ocena oc = ConsoleViewUtils.SafeInputOcenaId(_headDAO.daoOcena);
        _headDAO.daoOcena.RemoveObject(oc.Id);
    }

}
