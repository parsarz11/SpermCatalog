namespace SpermCatalog.API.Exceptions
{
    public class DairySpermInvalidException : SpermCatalogException
    {
        public DairySpermInvalidException() : base("Dairy sperm data is invalid")
        {
        }
    }
}
