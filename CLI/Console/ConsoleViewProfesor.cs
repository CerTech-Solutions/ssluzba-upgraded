using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class ConsoleViewProfesor : ConsoleView<Profesor>
{
    public ConsoleViewProfesor(HeadDAO headDAO) : base(headDAO) { }

    public override void ShowAll()
    {
        PrintObjects(_headDAO.daoProfesor.GetAllObjects());
    }

    public override void AddObject()
    {
        Profesor? obj = InputObject();
        _headDAO.daoProfesor.AddObject(obj);
    }

    public override void UpdateObject()
    {
        Profesor prof = ConsoleViewUtils.SafeInputProfesorId(_headDAO.daoProfesor);
        Profesor? obj = InputObject();
        obj.Id = prof.Id;
        _headDAO.daoProfesor.UpdateObject(obj);
    }

    public override void RemoveObject()
    {
        Profesor prof = ConsoleViewUtils.SafeInputProfesorId(_headDAO.daoProfesor);
        try
        {
            _headDAO.CheckDeleteProfesor(prof.Id);
            System.Console.WriteLine("Profesor deleted successfully!");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            System.Console.WriteLine("Profesor was not deleted!");
        }
        _headDAO.daoProfesor.RemoveObject(prof.Id);
    }

}
