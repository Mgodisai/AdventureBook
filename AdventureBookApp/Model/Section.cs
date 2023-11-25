using System.Text;

namespace AdventureBookApp.Model;

public class Section
{
    private Guid Id { get; }
    private string Name { get; }
    private string Description { get; }
    private readonly List<Item> _items;
    private readonly List<Section> _exits;

    public Section(List<Item> items, List<Section> exits, Guid id, string name, string description)
    {
        _items = items;
        _exits = exits;
        Id = id;
        Name = name;
        Description = description;
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(Name);
        stringBuilder.AppendLine(Description);
        stringBuilder.AppendLine($"You see: {string.Join(',', _items)}");
        stringBuilder.AppendLine($"Available exits: {string.Join(',', _exits)}");
        return stringBuilder.ToString();
    }
}