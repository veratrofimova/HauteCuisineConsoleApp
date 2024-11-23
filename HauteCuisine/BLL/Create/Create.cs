using HauteCuisine.Infrastructure.DAL.Database;

namespace HauteCuisine.BLL.Create
{
    public class Create : ICreatable
    {
        public async Task Creater(object dishInfo, CancellationToken token)
        {
            DB dB = new DB();
            dB.InsertData(dishInfo);

            await Task.Delay(0, token);
        }
    }
}
