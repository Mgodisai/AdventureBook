﻿using AdventureBookApp.Enum;
using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;
using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Location;

namespace AdventureBookApp.Command;

public class AttackCommand : ICommand 
{
    public void Execute(GameContext context, string parameter)
    {
        if (context.CurrentSection != null)
        {
            var monster = GetMonsterFromSectionByName(parameter, context.CurrentSection);
            if (monster is not null)
            {
                if (monster.MonsterType == MonsterType.Friendly)
                {
                    ConsoleExtensions.WriteLineInfo(parameter+" is friendly, cannot attack it!");
                    return;
                } 
                monster.MonsterType = MonsterType.Enemy;
                context.StartCombat((Player)context.Player, monster, context.CurrentSection);
            }
            else
            {
                ConsoleExtensions.WriteLineError($"Monster with name {parameter} cannot be found in this section!");
            }
        }
    }
    
    private Monster? GetMonsterFromSectionByName(string name, Section section)
    {
        Predicate<Monster> pred = (m) => string.Equals(m.Name, name, StringComparison.CurrentCultureIgnoreCase);
        var monster = section.GetMonster(pred);
        return monster;
    }

    public string GetHelp()
    {
        return "Attack a character (neutral or enemy) in section. Usage: attack [characterName]";
    }
}