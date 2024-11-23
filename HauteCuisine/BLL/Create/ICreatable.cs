using HauteCuisine.DAL.OM;

namespace HauteCuisine.BLL.Create
{
    public interface ICreatable
    {
        Task Creater(object data, CancellationToken token);
    }
}
