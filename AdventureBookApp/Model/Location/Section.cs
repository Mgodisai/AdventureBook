﻿using System.Text;
using AdventureBookApp.Model.Entity;

namespace AdventureBookApp.Model.Location;

public class Section : BaseEntity
{
    public int Index { get; set; }
    private readonly List<Item.Item> _items = new();
    private readonly List<Character> _characters = new();
    private readonly List<Exit> _exits = new();
    
    public IEnumerable<Character> GetCharacters() => _characters;
    public IEnumerable<Item.Item> GetItems() => _items;
    public IEnumerable<Exit> GetExits() => _exits;

    public Section()
    {
    }
    
    public Section(int index, string name, string description)
    : base(name, description)
    {
        Index = index;
    }

    public Monster? GetMonster(Predicate<Monster> predicate) {
        return _characters.OfType<Monster>().FirstOrDefault(m => predicate(m));
    }

    public string GetSectionContent()
    {
        var stringBuilder = new StringBuilder();
        if (_items.Count > 0)
        {
            stringBuilder.AppendLine($"Items: {string.Join(',', _items)}");
        }
            
        if (_characters.Count > 0)
        {
            stringBuilder.AppendLine($"Characters: \n{string.Join('\n', _characters)}");
        }
            
        if (_exits.Count > 0)
        {
            stringBuilder.Append($"Exits: {GetAvailableExitsAsString()}");
        }

        return stringBuilder.ToString();
    }

    private string GetAvailableExitsAsString()
    {
        var isAnyVisibleExit = _exits.Find(e => e.IsHidden == false) != null;
        if (_exits.Count == 0 || !isAnyVisibleExit) return "No visible exit!";
        
        var stringBuilder = new StringBuilder();
        foreach (var exit in _exits)
        {
            if (exit.IsHidden) continue;
            stringBuilder.AppendLine(exit.ToString());
        }

        return stringBuilder.ToString();
    }
    
    public void AddItem(Item.Item? item)
    {
        if (item != null) _items.Add(item);
    }
    
    public void AddItems(params Item.Item[] items)
    {
        foreach (var item in items)
        {
            AddItem(item);
        }
    }

    private void RemoveItem(Item.Item item)
    {
        _items.Remove(item);
    }

    public Item.Item? RemoveItem(string itemName)
    {
        var item = _items.Find(i => i.Name == itemName);
        if (item is not null)
        {
            RemoveItem(item);
        }

        return item;
    }
    
    public void AddCharacter(Character character)
    {
        _characters.Add(character);
    }

    public bool RemoveCharacter(Character character)
    {
        return _characters.Remove(character);
    }

    public void AddExit(Exit exit)
    {
        if (_exits.Contains(exit)) return;
        _exits.Add(exit);
    }

    public Exit? GetExitById(int id) =>_exits.Find(e => e.Id == id);
    
}