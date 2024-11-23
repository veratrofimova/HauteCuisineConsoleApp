using HauteCuisine.BLL.Observer;
using HauteCuisine.Controllers;
using HauteCuisine.DAL.OM;
using HauteCuisine.Infrastructure.DAL.Database;

namespace HauteCuisine.BLL
{
    public class DishCreateTask
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
            Task.Run(async () =>
            {
                await WaitForInsertDishDone(result, done, cts);
            });
        }

        public async Task WaitForInsertDishInfo(Task task, string title, CancellationTokenSource token)
        {
            await task;

            Console.WriteLine($"{title} успешно добавлено");
        }

        public async Task WaitForInsertDishDone(Task task, DishDoneModel dishDone, CancellationTokenSource token)
        {
            await task;

            if (dishDone.DateCooking >= DateTime.Today)
            {
                var eventPublisher = new EventPublisher();

                var insertOperation = new QueryOperation();
                insertOperation.GetUsersData().ForEach(x =>
                {
                    eventPublisher.UserSubscribe(dishDone, x);
                });
            }
        }
    }
}
