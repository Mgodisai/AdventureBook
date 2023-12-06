﻿namespace AdventureBookApp.Model.Location;

public class World
{
    private readonly Dictionary<int, Section> _sections = new();

    public void AddSection(Section section) =>_sections[section.Id] = section;

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