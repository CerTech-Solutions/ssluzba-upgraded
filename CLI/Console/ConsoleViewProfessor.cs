using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class ConsoleViewProfessor : ConsoleView<Professor>
{
    public ConsoleViewProfessor(HeadDAO headDAO) : base(headDAO) { }

    public override void ShowAll()
    {
        PrintObjects(_headDAO.daoProfessor.GetAllObjects());
    }

    public override void AddObject()
    {
        Professor obj = new Professor();
        InputObject(obj);
        _headDAO.daoProfessor.AddObject(obj);
    }

    public override void UpdateObject()
    {
        Professor prof = ConsoleViewUtils.SafeInputProfessorId(_headDAO.daoProfessor, true);
        if (prof == null) return;

        InputObject(prof, true);
        _headDAO.daoProfessor.UpdateObject(prof);
    }

    public override void RemoveObject()
    {
        Professor prof = ConsoleViewUtils.SafeInputProfessorId(_headDAO.daoProfessor, true);
        if (prof == null) return;

        try
        {
            _headDAO.DeleteProfesor(prof.Id);
            System.Console.WriteLine("Profesor deleted successfully!");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            System.Console.WriteLine("Profesor was not deleted!");
        }
    }

}
