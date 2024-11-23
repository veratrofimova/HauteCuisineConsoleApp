using HauteCuisine.BLL.Create;
using HauteCuisine.DAL.OM;

namespace HauteCuisine.Controllers
{
    public class DBController
    {
        private readonly ICreatable _create;
        private readonly CancellationTokenSource _tokenSource;
        public DBController(ICreatable create, CancellationTokenSource cancellationTokenSource) 
        {
            _create = create;
            _tokenSource = cancellationTokenSource;
        }

        public async Task Handle<T>(T data)
        {
            try
            {
                await _create.Creater(data, _tokenSource.Token);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
