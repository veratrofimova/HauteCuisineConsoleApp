using HauteCuisine.Infrastructure.DAL.Database;

namespace HauteCuisine.BLL.Observer
{
    public class EventSubscriber
    {
        public void SubcriberTo(QueryOperation sender)
        {
            sender.WhatsForDinner += (text) =>
            {
                Console.WriteLine(text);
            };
        }
    }
}
