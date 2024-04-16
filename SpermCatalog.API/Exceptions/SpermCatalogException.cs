namespace SpermCatalog.API.Exceptions
{
    public abstract class SpermCatalogException : Exception
    {
        public SpermCatalogException(string? message) : base(message)
        {
        }
    }
}
