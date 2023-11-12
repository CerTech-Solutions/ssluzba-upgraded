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
        Katedra? obj = InputObject();
        _headDAO.daoKatedra.AddObject(obj);
    }

    public override void UpdateObject()
    {
        Katedra kat = ConsoleViewUtils.SafeInputKatedraId(_headDAO.daoKatedra);
        Katedra? obj = InputObject();
        obj.Id = kat.Id;
        _headDAO.daoKatedra.UpdateObject(obj);
    }

    public override void RemoveObject()
    {
        Katedra kat = ConsoleViewUtils.SafeInputKatedraId(_headDAO.daoKatedra);
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
