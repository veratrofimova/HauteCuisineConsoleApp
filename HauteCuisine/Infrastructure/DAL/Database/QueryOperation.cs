using HauteCuisine.DAL.OM;
using System.Data;

namespace HauteCuisine.Infrastructure.DAL.Database
{
    public class QueryOperation
    {
        public event Action<string> WhatsForDinner;

        protected void CookingStarted(string text)
        {
            WhatsForDinner?.Invoke(text);
        }

        public string InsertSqlQuery(object data)
        {
            const string exceptionText = "Скрипт на добавление строки не создан";
            //CREATE SEQUENCE Сuisine.DishInfo_id_seq;
            try
            {
                if (data is DishInfoModel)
                {
                    var dishInfo = (DishInfoModel)data;

                    if (dishInfo == null)
                        throw new Exception(exceptionText);

                    var sql = string.Format(@"insert into ""Сuisine"".""DishInfo""(""Id"", ""Title"", ""Recipe"", ""Ingredients"", ""Comment"", ""FlagGarnish"", ""Calorie"", ""PrefDima"", ""PrefDanya"", ""PrefVera"", ""PrefEvgen"", ""TimeCooking"")
                values(nextval('Сuisine.DishInfo_id_seq'), '{0}', '{1}', '{2}', '{3}', {4}, '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')",
                        dishInfo.Title,
                        dishInfo.Recipe,
                        dishInfo.Ingredients,
                        dishInfo.Comment,
                        dishInfo.FlagGarnish,
                        dishInfo.Calorie,
                        dishInfo.PrefDima,
                        dishInfo.PrefDanya,
                        dishInfo.PrefVera,
                        dishInfo.PrefEvgen,
                        dishInfo.TimeCooking);

                    return sql;
                }
                else if (data is DishDoneModel)
                {
                    var dishDone = (DishDoneModel)data;

                    if (dishDone == null)
                        throw new Exception(exceptionText);

                    var sql = string.Format(@"insert into ""Сuisine"".""DishDone""(""Id"", ""IdDishInfo"", ""DateCooking"", ""Comment"")
                values(nextval('Сuisine.DishDone_id_seq'), '{0}', '{1}', '{2}')",
                        dishDone.IdDishInfo,
                        dishDone.DateCooking,
                        dishDone.Comment);

                    if (dishDone.DateCooking >= DateTime.Today)
                    {
                        string title = GetDishInfoDataById(dishDone.IdDishInfo).Title;
                        GetUsersData().ForEach(x =>
                        {
                            CookingStarted($"{x}, на ужин {dishDone.DateCooking.ToShortDateString()} ожидается {title}");
                        });
                    }
                    return sql;
                }
                else
                {
                   throw new Exception(exceptionText);      
                }
            }
            catch 
            {
                throw new Exception(exceptionText);                
            }
        }

        public List<(string, int)> GetRatingData()
        {
            DB dB = new DB();

            var query = from row in dB.SelectData("SelectDishDoneInfo").AsEnumerable()
                        group row by row.Field<string>("Title") into infoTable
                        select new { Title = infoTable.Key, CountT = infoTable.Count() };

            return query
                .Select(s => (s.Title, s.CountT))
                .OrderByDescending(x => x.CountT)
                .ToList();
        }

        public List<string> GetUsersData()
        {
            DB dB = new DB();
            
            var query = dB.SelectData("SelectUser")
                .AsEnumerable()
                .Where(x => x.Field<bool>("Admin") == false)
                .Select(x => x.Field<string>("Name"));

            return [.. query];
        }

        public DishInfoModel GetDishInfoDataById(int id)
        {
            var data = GetDishInfoData().Where(x => x.Id == id).FirstOrDefault();

            return data;
        }

        public List<DishInfoModel> GetDishInfoData()
        {
            var dishInfoModels = new List<DishInfoModel>();
            var dB = new DB();

            foreach (var item in dB.SelectData("SelectDishInfo").AsEnumerable())
            {
                dishInfoModels.Add(new DishInfoModel()
                {
                    Id = item.Field<int>("Id"),
                    Title = item.Field<string>("Title"),
                    Recipe = item.Field<string>("Recipe"),
                    Ingredients = item.Field<string>("Ingredients"),
                    Comment = item.Field<string>("Comment"),
                    FlagGarnish = item.Field<bool>("FlagGarnish"),
                    Calorie = item.Field<int>("Calorie"),
                    PrefDima = item.Field<int>("PrefDima"),
                    PrefVera = item.Field<int>("PrefVera"),
                    PrefDanya = item.Field<int>("PrefDanya"),
                    PrefEvgen = item.Field<int>("PrefEvgen"),
                    TimeCooking = item.Field<int>("TimeCooking"),
                });
            }

            return dishInfoModels;
        }
    }
}
