using HauteCuisine.BLL.Observer;
using HauteCuisine.Infrastructure.UI.ConsoleOperation;

Console.WriteLine("Приветствую! Программа предназначение для помощи в выборе блюд на ужин");
Console.WriteLine("Введите Ваше имя");
string name = Console.ReadLine();

Console.WriteLine($"{name}, введите /start для старта программы и /exit - для завершения");

CreateObserver createObserver = new CreateObserver();
createObserver.AddUser();

bool exitFlag = true;
var consoleOp = new ConsoleOperation();
do
{
    Console.WriteLine("Введите команду: ");
    string common = Console.ReadLine();

    switch (common)
    {
        case "/start":
            Console.WriteLine("/new - Добавить новое блюдо в справочник");
            Console.WriteLine("/done - Добавить блюдо, которое приготовлено или еще планируется (на дату)");
            Console.WriteLine("/view - Рейтинг блюд (по частоте приготовления)");
            break;
        case "/new":
            consoleOp.CreateNewDish();
            break;
        case "/done":
            consoleOp.AddDishToDone();
            break;
        case "/view":
            consoleOp.ViewRatingDishes();
            break;
        case "/exit":
            exitFlag = false;
            break;
    }
}
while (exitFlag);

Console.WriteLine("Программа завершена");