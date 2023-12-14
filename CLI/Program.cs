using CLI.Console;
using CLI.DAO;


// Uncomment when creating data for the first time
DataGenerator.Generate();

ConsoleViewSelector console = new ConsoleViewSelector();
console.RunSelector();

return 0;
 