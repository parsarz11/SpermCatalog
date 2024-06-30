namespace SpermCatalog.API.models
{
    public class TimeSelectionModel
    {
        public enum TimeSelectionEnum
        {
            AllTime = 0,
            LastYear = 1,
            ThisYear = 2,
            LastMonth = 3,
            ThisMonth = 4,
            SevenDays = 5,
            Today = 6
        }
    }
}
