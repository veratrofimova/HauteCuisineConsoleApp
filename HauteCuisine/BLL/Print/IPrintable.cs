
namespace HauteCuisine.BLL.Print
{
    public interface IPrintable<T>
    {
        /*void PrintTable(IEnumerable<T> data);*/

        void PrintTable(List<(string, int)> data);
    }
}
