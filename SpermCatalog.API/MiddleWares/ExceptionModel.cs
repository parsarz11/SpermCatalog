using System.Collections;

namespace SpermCatalog.API.MiddleWares
{
    internal class ExceptionModel
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public IDictionary Datas { get; set; }

    }
}