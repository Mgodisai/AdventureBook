using AdventureBookApp.Model.Item;
using AutoMapper;

namespace AdventureBookApp.Loader.Mapper;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        CreateMap<AdjustmentDto, Adjustment>();
        CreateMap<ItemDto, Item>()
            .ConstructUsing((dto,context) => dto.Type switch
            {
                "Equipment" => new Equipment(
                    dto.Name,
                    dto.Description,
                    dto.Weight,
                    context.Mapper.Map<Adjustment>(dto.Adjustment)),
                "Consumable" => new Consumable(
                    dto.Name,
                    dto.Description,
                    dto.Weight,
                    context.Mapper.Map<Adjustment>(dto.Adjustment)),
                _ => throw new ArgumentOutOfRangeException()
            });
    }
}