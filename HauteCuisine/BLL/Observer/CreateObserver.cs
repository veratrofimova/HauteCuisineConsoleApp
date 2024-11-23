namespace HauteCuisine.BLL.Observer
{
    public class CreateObserver
    {
        public void AddUser()
        {
            var dishCreateTask = new EventPublisher();
            var sender = new EventSubscriber();
            sender.SubcriberTo(dishCreateTask);

            //Подписка нескольких пользователей будет в реализации через телеграмм
        }
    }
}
