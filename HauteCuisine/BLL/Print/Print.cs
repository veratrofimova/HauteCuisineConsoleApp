using System.Collections.Generic;
using System.Reflection;

namespace HauteCuisine.BLL.Print
{
    public sealed class Print<T> : IPrintable<T>
    {
        /*private const int _tableWidth = 150;

        public void PrintTable(IEnumerable<T> data)
        {
            try
            {
                PropertyInfo[] propertyInfos = typeof(T).GetProperties();
                var propertyName = propertyInfos.Select(x => x.Name).ToArray();

                GetSepSimble();
                PrintRow(propertyName);
                GetSepSimble();


                foreach (var row in data)
                {
                    var values = propertyInfos.Select(x => x.GetValue(row, null).ToString()).ToArray();
                    PrintRow(values);
                }

                GetSepSimble();
            }
            catch (Exception ex)
            {
                throw;
            }
        }*/

        public void PrintTable(List<(string, int)> data)
        {
                data.ForEach(x => { Console.WriteLine($"{x.Item1} - {x.Item2} раз(а)"); });
        }

        /*private void GetSepSimble()
        {
            Console.WriteLine(new String('-', _tableWidth));
        }

        public void PrintRow(params string[] columns)
        {
            int columnWidth = (_tableWidth - columns.Length) / columns.Length;
            const string columnSeperator = "|";

            string row = columns.Aggregate(columnSeperator, (seperator, columnText) => seperator + GetCenterAlignedText(columnText, columnWidth) + columnSeperator);

            Console.WriteLine(row);
        }

        private string GetCenterAlignedText(string text, int columnWidth)
        {
            text = text.Length > columnWidth ? text.Substring(0, columnWidth - 3) + "_" : text;

            return string.IsNullOrEmpty(text)
                ? new string(' ', columnWidth)
                : text.PadRight(columnWidth - ((columnWidth - text.Length) / 2)).PadLeft(columnWidth);
        }*/
    }
}
