namespace HauteCuisine.BLL.Print
{
    public sealed class Print<T> : IPrintable<T>
    {
        public void PrintTable(List<(string, int)> data)
        {
                data.ForEach(x => { Console.WriteLine($"{x.Item1} - {x.Item2} раз(а)"); });
        }
    }
}
