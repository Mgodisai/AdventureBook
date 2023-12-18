using AdventureBookApp.Enum;
using AdventureBookApp.Game;
using AdventureBookApp.Game.Setting;
using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Storage;
using AutoMapper;

namespace AdventureBookApp.Loader.Mapper;

public class CharacterProfile : Profile
{
    public CharacterProfile()
    {
        CreateMap<string, CharacterType>().ConvertUsing(str => System.Enum.Parse<CharacterType>(str));
        CreateMap<string, MonsterType>().ConvertUsing(str => System.Enum.Parse<MonsterType>(str));
        CreateMap<CharacterDto, Character>().ConstructUsing(
            (dto,context) => dto.Type == "Monster"
                ? new Monster(
                    context.Mapper.Map<CharacterType>(dto.CharacterType),
                    context.Mapper.Map<MonsterType>(dto.MonsterType),
                    dto.Name,
                    dto.Description,
                    dto.Health,
                    dto.Skill,
                    new Inventory(GameRules.MonsterInventoryDefaultCapacity))
                : new Character(
                    context.Mapper.Map<CharacterType>(dto.CharacterType),
                    dto.Name,
                    dto.Description,
                    dto.Health,
                    dto.Skill,
                    new Inventory(GameRules.CharacterInventoryDefaultCapacity))
        );
    }
}