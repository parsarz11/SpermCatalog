namespace SpermCatalog.API.Exceptions
{
    public class BeefSpermNotFoundException : SpermCatalogException
    {
        public BeefSpermNotFoundException() : base("No beef sperm found")
        {
        }
        public BeefSpermNotFoundException(string id) : base($"beef sperm with id : {id} not found")
        {
        }
    }
}
