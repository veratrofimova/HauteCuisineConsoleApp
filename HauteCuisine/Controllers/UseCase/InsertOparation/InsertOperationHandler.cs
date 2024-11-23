using HauteCuisine.BLL.Create;

namespace HauteCuisine.Controllers.UseCase.InsertOparation
{
    public class InsertOperationHandler
    {
        private readonly ICreatable _create;
        private readonly CancellationToken _token;

        public InsertOperationHandler(Create create,CancellationToken cancellationToken)
        {
            _create = create;
            _token = cancellationToken;
        }

        public async Task Handle(object data)
        {
            try
            {
                await _create.Creater(data, _token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
