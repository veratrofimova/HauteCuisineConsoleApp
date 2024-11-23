namespace HauteCuisine.DAL.OM
{
    public class DishInfoModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Recipe { get; set; }
        public string Ingredients { get; set; }
        public string Comment { get; set; }
        public bool FlagGarnish { get; set; }
        public double Calorie { get; set; }
        public int PrefDima { get; set; }
        public int PrefVera { get; set; }
        public int PrefDanya { get; set; }
        public int PrefEvgen { get; set; }
        public int TimeCooking { get; set; }
    }
}
