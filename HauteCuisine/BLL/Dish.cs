using HauteCuisine.Controllers;
using HauteCuisine.DAL.OM;

namespace HauteCuisine.BLL
{
    public class Dish
    {
        public void CreateNewDish(DishInfoModel dishInfo)
        {
            var cts = new CancellationTokenSource();
            var controller = new TelegramController<DishInfoModel>(cts.Token);

            var result = Task.Run(() => controller.InsertOperation(dishInfo));
            Task.Run(async () =>
            {
                await WaitForInsertDishInfo(result, dishInfo.Title, cts);
            });
        }

        public void AddDishToDone(DishDoneModel done)
        {

            var cts = new CancellationTokenSource();
            var controller = new TelegramController<DishDoneModel>(cts.Token);

            var result = Task.Run(() => controller.InsertOperation(done));
        }

        public async Task WaitForInsertDishInfo(Task task, string title, CancellationTokenSource token)
        {
            await task;

            Console.WriteLine($"{title} успешно добавлено");
        }

        public async Task WaitForInsertDishDone(Task task, DishDoneModel dishDone, CancellationTokenSource token)
        {
            await task;
        }
    }
}
