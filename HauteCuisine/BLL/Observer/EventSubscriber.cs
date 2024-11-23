namespace HauteCuisine.BLL.Observer
{
    public class EventSubscriber
    {
        public void SubcriberTo(EventPublisher sender)
        {
            sender.WhatsForDinner += (text) =>
            {
                Console.WriteLine(text);
            };
        }
    }
}
