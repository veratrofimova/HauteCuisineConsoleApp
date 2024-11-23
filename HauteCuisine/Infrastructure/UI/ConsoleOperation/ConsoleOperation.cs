using HauteCuisine.BLL;
using HauteCuisine.BLL.Observer;
using HauteCuisine.BLL.Print;
using HauteCuisine.DAL.OM;
using HauteCuisine.Infrastructure.DAL.Database;

namespace HauteCuisine.Infrastructure.UI.ConsoleOperation
{
    public class ConsoleOperation
    {
        public void RunConsole()
        {
            Console.WriteLine("Приветствую! Программа предназначение для помощи в выборе блюд на ужин");
            Console.WriteLine("Введите Ваше имя");
            string name = Console.ReadLine();

            Console.WriteLine($"{name}, введите /start для старта программы и /exit - для завершения");

            bool exitFlag = true;
            do
            {
                Console.WriteLine("Введите команду: ");
                string common = Console.ReadLine();

                switch (common)
                {
                    case "/start":
                        Console.WriteLine("/new - Добавить новое блюдо в справочник");
                        Console.WriteLine("/done - Добавить блюдо, которое приготовлено или только планируется (на дату)");
                        Console.WriteLine("/view - Рейтинг блюд (по частоте приготовления)");
                        break;
                    case "/new":
                        CreateNewDish();
                        break;
                    case "/done":
                        AddDishToDone();
                        break;
                    case "/view":
                        ViewRatingDishes();
                        break;
                    case "/exit":
                        exitFlag = false;
                        break;
                }
            }
            while (exitFlag);

            Console.WriteLine("Программа завершена");
        }

        private void CreateNewDish()
        {
            Console.WriteLine("Введите заголовок:");
            string title = Console.ReadLine();

            Console.WriteLine("Введите рецепт:");
            string recipe = Console.ReadLine();

            Console.WriteLine("Введите ингридиенты:");
            string ingredients = Console.ReadLine();

            Console.WriteLine("Введите нужен ли гарнир (да/нет):");
            string garnish = Console.ReadLine();

            Console.WriteLine("Введите предпочтение от 0 до 10, где 10 очень нравится (Даня/Дима/Женя/Вера) через символ /:");
            string[] preference = Console.ReadLine().Split("/");
            int prefDanya = 0;
            int prefDima = 0;
            int prefEvgen = 0;
            int prefVera = 0;

            if (preference != null && preference.Length == 4)
            {
                int.TryParse(preference[0], out prefDanya);
                int.TryParse(preference[1], out prefDima);
                int.TryParse(preference[2], out prefEvgen);
                int.TryParse(preference[3], out prefVera);
            }

            Console.WriteLine("Введите калорийность на порцию:");
            int calorie;
            bool calorieWellDone = int.TryParse(Console.ReadLine(), out calorie);

            Console.WriteLine("Введите комментарий:");
            string comment = Console.ReadLine();

            Console.WriteLine("Введите время приготовления:");            
            int.TryParse(Console.ReadLine(), out int timeCooking);

            var dishInfo = new DishInfoModel()
            {
                Title = title,
                Recipe = recipe,
                Ingredients = ingredients,
                FlagGarnish = garnish == "да" ? true : false,
                Calorie = calorie,
                Comment = comment,
                PrefDima = prefDima,
                PrefEvgen = prefEvgen,
                PrefVera = prefVera,
                PrefDanya = prefDanya,
                TimeCooking = timeCooking,
            };

            var dish = new DishCreateTask();
            dish.CreateNewDish(dishInfo);
        }

        private void ViewRatingDishes()
        {
            var insertOperation = new QueryOperation();
            List<(string, int)> dishDoneData = insertOperation.GetRatingData();

            IPrintable<List<(string, int)>> omDishDone = new Print<List<(string, int)>>();
            omDishDone.PrintTable(dishDoneData);
        }

        private void AddDishToDone()
        {
            GetDishInfo();

            Console.WriteLine("\r\nВыберите номер блюда:");
            int.TryParse(Console.ReadLine(), out int number);

            Console.WriteLine("Введите дату приготовления (в формате dd.mm.yyyy):");
            DateTime.TryParse(Console.ReadLine(), out DateTime dateCooking);

            Console.WriteLine("При необходимости, добавьте комментарий:");
            string comment = Console.ReadLine();

            var done = new DishDoneModel()
            {
                IdDishInfo = number,
                DateCooking = dateCooking,
                Comment = comment,
            };

            CreateObserver createObserver = new CreateObserver();
            createObserver.AddUser();

            var dish = new DishCreateTask();
            dish.AddDishToDone(done);
        }

        private void GetDishInfo()
        {
            QueryOperation queryOperation = new QueryOperation();
            List<DishInfoModel> dishInfoModels = queryOperation.GetDishInfoData();

            foreach (var dishInfoModel in dishInfoModels.OrderBy(x => x.Id))
            {
                Console.WriteLine($"№ {dishInfoModel.Id} - {dishInfoModel.Title}: калорийность - {dishInfoModel.Calorie}, время приготовления - {dishInfoModel.TimeCooking}");
            }
        }
    }
}