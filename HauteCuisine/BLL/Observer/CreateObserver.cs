using HauteCuisine.Infrastructure.DAL.Database;

namespace HauteCuisine.BLL.Observer
{
    public class CreateObserver
    {
        public void AddUser()
        {
            var dishCreateTask = new QueryOperation();
            var sender = new EventSubscriber();
            sender.SubcriberTo(dishCreateTask);

            //Подписка нескольких пользователей будет в реализации через телеграмм
        }
    }
}
