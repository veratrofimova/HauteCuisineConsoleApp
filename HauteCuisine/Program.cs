using HauteCuisine.Infrastructure.UI.ConsoleOperation;

try
{
    ConsoleOperation consoleOperation = new ConsoleOperation();
    consoleOperation.RunConsole();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}