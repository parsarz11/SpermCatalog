namespace SpermCatalog.API.Exceptions
{
    public class DairySpermNotFoundException : SpermCatalogException
    {
        public DairySpermNotFoundException() : base ("No dairy sperm found")
        {
        }

        public DairySpermNotFoundException(string id) : base($"dairy sperm with id : {id} not found")
        {
        }


    }
}
