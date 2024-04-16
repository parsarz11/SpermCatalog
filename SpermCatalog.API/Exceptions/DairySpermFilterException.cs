namespace SpermCatalog.API.Exceptions
{
    public class DairySpermFilterException : SpermCatalogException
    {
        public DairySpermFilterException() : base("dairy sperm with Those filters doesn't Exist")
        {
        }
    }
}
