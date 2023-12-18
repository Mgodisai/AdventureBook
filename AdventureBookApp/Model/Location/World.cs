using System.ComponentModel;
using System.Text;
using AdventureBookApp.Game;
using AdventureBookApp.Game.WinningCondition;

namespace AdventureBookApp.Model.Location;

public class World
{
    private readonly Dictionary<int, Section> _sections = new();
    private readonly List<IWinningCondition> _winningConditions = new List<IWinningCondition>();

    public void AddSection(Section section) =>_sections[section.Index] = section;
    public void AddWinningCondition(IWinningCondition winningCondition)
    {
        _winningConditions.Add(winningCondition);
    }
    public IEnumerable<IWinningCondition> WinningConditions => _winningConditions;

    public string GetWinningConditionsAsString()
    {
        StringBuilder sb = new();
        foreach (var winningCondition in WinningConditions)
        {
            sb.AppendLine($"- {winningCondition}");
        }

        return sb.ToString();
    }
    
    public void AddSections(params Section[] sections)
    {
        foreach (var section in sections)
        {
            AddSection(section);
        }
    }
    private Section? GetSection(int id) =>_sections.TryGetValue(id, out var section) ? section : null;
    
    public Section? this[int id] => GetSection(id);
}