using AdventureBookApp.Enum;
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
            var monster = GetMonsterToBattle(parameter, context.CurrentSection);
            if (monster is not null)
            {
                monster.MonsterType = MonsterType.Enemy;
                context.StartCombat((Player)context.Player, monster, context.CurrentSection);
            }
            else
            {
                ConsoleExtensions.WriteInfo(parameter+" is friendly, cannot attack it!");
            }
        }
    }
    
    private Monster? GetMonsterToBattle(string name, Section section)
    {
        Predicate<Monster> pred = (m) => m.MonsterType != MonsterType.Friendly && string.Equals(m.Name, name, StringComparison.CurrentCultureIgnoreCase);
        var monster = section.GetMonster(pred);
        return monster;
    }

    public string GetHelp()
    {
        return "Attack a character (neutral or enemy) in section. Usage: attack [characterName]";
    }
}