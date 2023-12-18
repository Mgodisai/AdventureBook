using System.Text.Json;
using AdventureBookApp.Game.WinningCondition;
using AdventureBookApp.Loader.Mapper;
using AdventureBookApp.Model;
using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Item;
using AdventureBookApp.Model.Location;
using AutoMapper;

namespace AdventureBookApp.Loader;

public class GameDataLoader
{
    private readonly IMapper _mapper;
    public GameDataLoader()
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CharacterProfile>();
            cfg.AddProfile<ItemProfile>();
        });
        _mapper = mapperConfiguration.CreateMapper();
    }

    public Book LoadBook(string filePath)
    {
        var jsonData = File.ReadAllText(filePath);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var bookDto = JsonSerializer.Deserialize<BookDto>(jsonData, options);

        if (bookDto == null)
        {
            throw new InvalidOperationException("Failed to load book data.");
        }

        Book book;
        try
        {
            book = CreateBook(bookDto);
        }
        catch (System.Exception)
        {
            throw new InvalidOperationException("Failed to load book data.");
        }
        return book;
    }
    
    public bool IsValidBookJson(string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        try
        {
            var bookData = JsonSerializer.Deserialize<BookDto>(json, options);
            return bookData?.World != null;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    private Book CreateBook(BookDto bookDto)
    {
        if (bookDto.World is null)
        {
            throw new ArgumentNullException(nameof(bookDto.World));
        }
        var world = CreateWorld(bookDto.World);
        return new Book(bookDto.Title ?? string.Empty, bookDto.Authors ?? new List<string> { string.Empty }, bookDto.Summary ?? string.Empty, world);
    }

    private World CreateWorld(WorldDto worldDto)
    {
        var world = new World();

        var sections = new Dictionary<int, Section>();
        
        foreach (var sectionDto in worldDto.Sections)
        {
            var section = new Section(sectionDto.Index, sectionDto.Name, sectionDto.Description);
            sections[sectionDto.Index] = section;
        }

        foreach (var sectionDto in worldDto.Sections)
        {
            var section = sections[sectionDto.Index];
            foreach (var itemDto in sectionDto.Items)
            {
                var item = CreateItem(itemDto);
                if (itemDto.IsWinningCondition) world.AddWinningCondition(new GetItemCondition(item));
                section.AddItem(item);
            }

            foreach (var characterDto in sectionDto.Characters)
            {
                var character = CreateCharacter(characterDto);
                if (characterDto.IsWinningCondition) world.AddWinningCondition(new DefeatMonsterCondition((Monster)character));
                
                foreach (var itemDto in characterDto.Items)
                {
                    var item = CreateItem(itemDto);
                    character.Inventory.AddItem(item);
                    if (itemDto.IsWinningCondition) world.AddWinningCondition(new GetItemCondition(item));
                }
                
                section.AddCharacter(character);
            }

            foreach (var exitDto in sectionDto.Exits)
            {
                if (sections.TryGetValue(exitDto.TargetSectionIndex, out var targetSection))
                {
                    var exit = new Exit(exitDto.Description ?? string.Empty, targetSection, exitDto.IsHidden);
                    section.AddExit(exit);
                }
            }

            world.AddSection(section);
        }

        return world;
    }

    private Item CreateItem(ItemDto itemDto)
    {
        return _mapper.Map<Item>(itemDto);
    }

    private Character CreateCharacter(CharacterDto characterDto)
    {
        return _mapper.Map<Character>(characterDto);
    }
}
public record SectionDto(int Index, string Name, string Description, List<ItemDto> Items, List<CharacterDto> Characters, List<ExitDto> Exits);
public record ExitDto(string Description, int TargetSectionIndex, bool IsHidden);
public record AdjustmentDto(string AdjustmentType, string PropertyType, int Value);
public record ItemDto(string Type, string Name, string Description, double Weight, AdjustmentDto Adjustment, bool IsWinningCondition);
public record BookDto(string? Title, List<string>? Authors, string? Summary, WorldDto? World);
public record CharacterDto(string Type, string Name, string Description, int Health, int Skill, string CharacterType, string MonsterType, List<ItemDto> Items, bool IsWinningCondition);
public record WorldDto(List<SectionDto> Sections);