using MongoDB.Driver;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.API.Services
{
    public static class DefaultOrders
    {
        public static List<DairySperm> DairyDefaultDescendingOrder(this List<DairySperm> dairySperms)
        {
            dairySperms = dairySperms.OrderByDescending(x => x.CustomOrder)
                .ThenBy(x => x.IsNew)
                .ThenByDescending(x => x.FM)
                .ThenByDescending(x => x.LNM)
                .ThenByDescending(x => x.MILK)
                .ThenByDescending(x => x.PL)
                .ThenByDescending(x => x.TPI).ToList();

            return dairySperms;
        }

        public static List<DairySperm> DairyDefaultOrder(this List<DairySperm> dairySperms)
        {
            dairySperms = dairySperms.OrderBy(x => x.CustomOrder)
                .ThenByDescending(x => x.IsNew)
                .ThenBy(x => x.FM)
                .ThenBy(x => x.LNM)
                .ThenBy(x => x.MILK)
                .ThenBy(x => x.PL)
                .ThenBy(x => x.TPI)
                .ToList();

            return dairySperms;
        }

        public static List<BeefSperm> BeefDefaultOrder(this List<BeefSperm> beefSperm)
        {
            beefSperm = beefSperm.OrderBy(x => x.CustomOrder)
                .ThenByDescending(x => x.IsNew)
                .ThenBy(x => x.SCE)
                .ToList();
            //.OrderByDescending(x => x.CustomOrder)
            //    .ThenBy(x => x.IsNew)
            //    .ThenByDescending(x => x.SCE)
            //    .ToList();
            return beefSperm;
        }

        public static List<BeefSperm> BeefDefaultDescendingOrder(this List<BeefSperm> beefSperms)
        {
            beefSperms = beefSperms.OrderByDescending(x => x.CustomOrder)
                .ThenBy(x => x.IsNew)
                .ThenByDescending(x => x.SCE)
                .ToList();

            return beefSperms;
        }

        
    }
}
