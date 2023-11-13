using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class ConsoleViewKatedra : ConsoleView<Katedra>
{
    public ConsoleViewKatedra(HeadDAO headDAO) : base(headDAO) { }

    public override void ShowAll()
    {
        PrintObjects(_headDAO.daoKatedra.GetAllObjects());
    }

    public override void AddObject()
    {
        Katedra obj = new Katedra();
        InputObject(obj);
        _headDAO.daoKatedra.AddObject(obj);
    }

    public override void UpdateObject()
    {
        Katedra kat = ConsoleViewUtils.SafeInputKatedraId(_headDAO.daoKatedra, true);
        if (kat == null) return;

        InputObject(kat, true);
        _headDAO.daoKatedra.UpdateObject(kat);
    }

    public override void RemoveObject()
    {
        Katedra kat = ConsoleViewUtils.SafeInputKatedraId(_headDAO.daoKatedra, true);
        if (kat == null) return;

        try
        {
            _headDAO.DeleteDepartmant(kat.Id);
            System.Console.WriteLine("Katedra deleted successfully!");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            System.Console.WriteLine("Katedra was not deleted!");
        }
    }
}
