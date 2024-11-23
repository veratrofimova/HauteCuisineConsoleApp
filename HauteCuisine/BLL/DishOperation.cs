using HauteCuisine.DAL.OM;
using HauteCuisine.Infrastructure.DAL.Database;
using System.Data;

namespace HauteCuisine.BLL
{
    public class DishOperation
    { 
        /*public List<RatingDishesModel> GetDishDoneData()
        {
            DB dB = new DB();

            List<RatingDishesModel> dishDoneData = dB
                .SelectData("SelectDishDoneInfo")
                .AsEnumerable()
                .Select(x =>
                    new RatingDishesModel
                    {
                        Title = x.Field<string>("Title")!,
                        Calorie = x.Field<double?>("Calorie"),
                        DateCooking = x.Field<DateTime>("DateCooking"),
                        PrefDima = x.Field<int>("PrefDima"),
                        PrefVera = x.Field<int>("PrefVera"),
                        PrefDanya = x.Field<int>("PrefDanya"),
                        PrefEvgen = x.Field<int>("PrefEvgen"),
                        TimeCooking = x.Field<int>("TimeCooking"),
                    })
                .ToList();

            return dishDoneData;
        }*/


    }
}
