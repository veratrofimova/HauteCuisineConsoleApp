using HauteCuisine.DAL.OM;
using HauteCuisine.Infrastructure.DAL.Database;

namespace HauteCuisine.BLL.Observer
{
    public class EventPublisher
    {
        public event Action<string> WhatsForDinner;

        protected void CookingStarted(string text)
        {
            WhatsForDinner?.Invoke(text);
        }

        public void UserSubscribe(DishDoneModel dishDone, string name, string title)
        {
            CookingStarted($"{name}, на ужин {dishDone.DateCooking.ToShortDateString()} ожидается {title}");
        }
    }
}
