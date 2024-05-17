using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpermCatalog.DataAccess.Entities
{
    public class BeefSperm
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string RegNo { get; set; }
        public string BREED { get; set; }
        public string NAME { get; set; }

        public double SCE { get; set; }
        public double CR { get; set; }
        public double DM { get; set; }
        public double PCAR { get; set; }
        public double RDT { get; set; }
        public double CONF { get; set; }
        public double COUL { get; set; }
        public double GRAS { get; set; }
        public double IAB { get; set; }
        public double ICRC { get; set; }

        public string SIRE { get; set; }
        public string MGS { get; set; }
        public string Gender { get; set; }
        public string QuantityStatus { get; set; }
        public int Price { get; set; }

        public bool IsNew { get; set; }
        public int CustomOrder { get; set; } = 999;
    }
}
