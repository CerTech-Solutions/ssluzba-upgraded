using CLI.Console;
using CLI.DAO;
using CLI.Model;


// Uncomment when creating data for the first time
DataGenerator.Generate();

//ConsoleViewSelector console = new ConsoleViewSelector();
//console.RunSelector();


HeadDAO headDao = new HeadDAO();

System.Console.WriteLine(headDao.daoStudent.GetObjectById(1).GenerateClassHeader());
foreach (Student st in headDao.daoStudent.GetAllObjects())
    System.Console.WriteLine(st);

System.Console.WriteLine("\n\n\n");

System.Console.WriteLine(headDao.daoProfesor.GetObjectById(1).GenerateClassHeader());
foreach (Profesor st in headDao.daoProfesor.GetAllObjects())
    System.Console.WriteLine(st);

System.Console.WriteLine("\n\n\n");

System.Console.WriteLine(headDao.daoKatedra.GetObjectById(0).GenerateClassHeader());
foreach (Katedra kat in headDao.daoKatedra.GetAllObjects())
    System.Console.WriteLine(kat);

System.Console.WriteLine("\n\n\n");

System.Console.WriteLine("\n\n\n");
System.Console.WriteLine(headDao.daoOcena.GetObjectById(0).GenerateClassHeader());
foreach (Ocena oc in headDao.daoOcena.GetAllObjects())
    System.Console.WriteLine(oc);

System.Console.WriteLine("\n\n\n");

System.Console.WriteLine(headDao.daoPredmet.GetObjectById(0).GenerateClassHeader());
foreach (Predmet p in headDao.daoPredmet.GetAllObjects())
    System.Console.WriteLine(p);

return 0;
 