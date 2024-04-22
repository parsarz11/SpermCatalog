using Mapster;
using SpermCatalog.API.models.DTOs.Filters;
using SpermCatalog.DataAccess.Entities;

namespace SpermCatalog.API.Extenssions.configs
{
    public class MapsterConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(RangeModel, RangeListModel), RangeFilter>()
                .Map
                (
                    destination => destination.Index,
                    source => source.Item1.Index
                )
                .Map
                (
                    destination => destination.MinValue,
                    source => source.Item1.MinValue,
                    sourceCondition => sourceCondition.Item1.MinValue != 0
                ).IgnoreIf((source, destination) => source.Item1.MinValue == 0,destination=>destination.MinValue)
                .Map
                (
                    destination => destination.MaxValue,
                    source => source.Item1.MaxValue
                    
                ).IgnoreIf((source, destination) => source.Item1.MaxValue == 0, destination => destination.MaxValue)

                .Map
                (
                    destination => destination.Category,
                    source => source.Item2.Category
                )
                .Map
                (
                    destination => destination.HerdId,
                    source => source.Item2.HerdId
                )
                .Map
                (
                    destination => destination.UserId,
                    source => source.Item2.UserId
                );


        }
    }
}
