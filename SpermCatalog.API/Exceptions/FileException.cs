namespace SpermCatalog.API.Exceptions
{
    public class FileException : SpermCatalogException
    {
        public FileException() : base("Can not read data from file")
        {
        }
    }
}
