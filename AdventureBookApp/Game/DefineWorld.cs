﻿using AdventureBookApp.Common;
using AdventureBookApp.Enum;
using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Item;
using AdventureBookApp.Model.Location;
using AdventureBookApp.Model.Storage;
using static AdventureBookApp.Game.GameRules;

namespace AdventureBookApp.Game;

public static class DefineWorld
{
    public static World InitializeWorld()
    {
        var world = new World();
        var section1 = new Section(1, "Village Square", "The central hub of a small village");
        var section2 = new Section(2, "Old Library", "A dusty library filled with ancient books.");
        var section3 = new Section(3, "Mystic Garden", "A serene garden with exotic plants.");
        var section4 = new Section(4, "Abandoned Hut", "A small hut, long abandoned.");
        var section5 = new Section(5, "Whispering Woods", "A dense forest with paths leading in various directions.");
        var section6 = new Section(6, "Crossing River", "A river with a narrow bridge crossing it.");
        var section7 = new Section(7, "Rocky Hills", "Hills with rocky terrain leading to the cave entrance.");
        var section8 = new Section(8, "Cave Entrance", "The entrance to the Caves of Mystery.");
        var section9 = new Section(9, "Inner Caves", "Dark and deep caves that echo with unknown sounds.");
        var section10 = new Section(10, "Amulet Chamber", "The final chamber where the Lost Amulet of Zanar is kept.");
        world.AddSections(section1, section2);

        // Section1 parameters
        Item shortSword =
            new Equipment(1, "short-sword", "steel sword", 1.0,
                new Adjustment(AdjustmentType.Modify, PropertyType.Skill, 2));
        Item apple =
            new Consumable(2, "apple", "it has very suspicious color", 7d,
                new Adjustment(AdjustmentType.Modify, PropertyType.Health, -10));

        Character dog = new Monster(CharacterType.Animal, MonsterType.Friendly, "Dog", "It is a calm pitbull",
            12, 7, new Inventory(1));
        Character hedgehog = new Monster(CharacterType.Animal, MonsterType.Neutral, "Hedgehog", "Nice hedgehog",
            8, 3, new Inventory(1));
        section1.AddItems(shortSword, apple);
        section1.AddExit(new Exit("Old Library", section2, false));
        section1.AddCharacter(dog);
        section1.AddCharacter(hedgehog);
        
        // Section2 parameters
        section2.AddExit(new Exit("To the Start", section1, false));
        section2.AddExit(new Exit("Go to Mystic Garden", section3, false));
        Item longSword =
            new Equipment(3, "longsword", "steel longsword", 5.0,
                new Adjustment(AdjustmentType.Modify, PropertyType.Skill, 4));
        Character helldog = new Monster(CharacterType.Animal, MonsterType.Enemy, "Helldog", "It is a very angry helldog",
            10, 8, new Inventory(1));
        section2.AddItem(longSword);
        section2.AddCharacter(helldog);
        
        section3.AddExit(new Exit("Abandoned Hut", section4, true));
        section4.AddExit(new Exit("Go into Whispering Woods", section5, false));
        section5.AddExit(new Exit("Go to Crossing River", section6, false));
        section6.AddExit(new Exit("Go to Rocky Hills", section7, false));
        section7.AddExit(new Exit("Go to cave", section8, false));
        section8.AddExit(new Exit("Go inner", section9, false));
        section9.AddExit(new Exit("Go into the chamber", section10, false));
        
        return world;
    }

    public static IPlayer InitializePlayer()
    {
        var playerName = ConsoleInputReader.ReadString("Enter your character's name: ");
        var description = ConsoleInputReader.ReadString("Describe yourself: ");
        int initSkill, initHealth, initLuck;
        var currentNumberOfRolls = 0;
        Console.WriteLine($"Roll for points (max {MaxNumberOfRolls} tries)");
        do
        {
            currentNumberOfRolls++;
            ConsoleExtensions.WriteTitle($"Roll {currentNumberOfRolls}");
            
            initSkill = DiceRoller(new Dice(DiceForSkill), "Roll for skill points: ")+SkillBase;
            ConsoleExtensions.WriteSuccess(initSkill.ToString());
            
            initHealth = DiceRoller(new Dice(DiceForHealth), "Roll for health points: ")+HealthBase;
            ConsoleExtensions.WriteSuccess(initHealth.ToString());
            
            initLuck = DiceRoller(new Dice(DiceForLuck), "Roll for luck points: ")+LuckBase;
            ConsoleExtensions.WriteSuccess(initLuck.ToString());
            if (currentNumberOfRolls == MaxNumberOfRolls)
            {
                break;
            }

        } while (ConsoleInputReader.ReadYesNo("Would you like to re-roll?"));

        Console.Write("Starting");
        for (var i = 0; i <= 5; i++)
        {
            Thread.Sleep(500);
            Console.Write(".");
            
        }
        return new Player(CharacterType.Human, playerName, description, initHealth, initSkill, initLuck, new Inventory(10));
    }

    public static int DiceRoller(IDice dice, string message)
    {
        var random = new Random();
        ConsoleExtensions.WriteInfo(message+" ");
        for (var i = 0; i < 20; i++)
        {
            Console.Write("\b" + random.Next(0,10));
            Thread.Sleep(50);
        }
        Console.Write("\b");
        return dice.Roll();
    }
}
