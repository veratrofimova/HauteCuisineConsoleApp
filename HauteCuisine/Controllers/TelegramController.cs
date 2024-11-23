using HauteCuisine.BLL.Create;
using HauteCuisine.Controllers.UseCase.InsertOparation;
using HauteCuisine.DAL.OM;

namespace HauteCuisine.Controllers
{
    public class TelegramController<T>
    {        
        private readonly CancellationToken _token;
        public TelegramController(CancellationToken cancellationToken)
        {
            _token = cancellationToken;
        }

        public Task InsertOperation(object data)
        {
            var insertOperationHandler = new InsertOperationHandler(new Create(), _token);
            Task task = insertOperationHandler.Handle(data);

            return task;
        }        
    }
}
