using HauteCuisine.BLL;
using HauteCuisine.BLL.Print;
using HauteCuisine.Controllers;
using HauteCuisine.DAL.OM;
using HauteCuisine.Infrastructure.DAL.Database;

namespace HauteCuisine.Infrastructure.UI.ConsoleOperation
{
    public class ConsoleOperation
    {
        public void CreateNewDish()
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
            double calorie;
            bool calorieWellDone = double.TryParse(Console.ReadLine(), out calorie);

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

        public void ViewRatingDishes()
        {
            var insertOperation = new QueryOperation();
            List<(string, int)> dishDoneData = insertOperation.GetRatingData();

            IPrintable<List<(string, int)>> omDishDone = new Print<List<(string, int)>>();
            omDishDone.PrintTable(dishDoneData);
        }

        public void AddDishToDone()
        {
            GetDishInfo();

            Console.WriteLine("\r\nВыберите номер блюда:");
            int.TryParse(Console.ReadLine(), out int number);

            Console.WriteLine("Введите дату приготовления (в формате dd.mm.yyyy):");
            DateTime.TryParse(Console.ReadLine(), out DateTime dateCooking);

            Console.WriteLine("При необходиомсти, добавьте комментарий:");
            string comment = Console.ReadLine();

            var done = new DishDoneModel()
            {
                IdDishInfo = number,
                DateCooking = dateCooking,
                Comment = comment,
            };

            var dish = new DishCreateTask();
            dish.AddDishToDone(done);
        }

        public void GetDishInfo()
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