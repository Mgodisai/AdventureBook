using AdventureBookApp.Enum;
using AdventureBookApp.Model;
using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Item;
using AdventureBookApp.Model.Location;

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
            new Equipable(1, "short-sword", "steel sword", 1.0,
                new Adjustment(AdjustmentType.Restore, PropertyType.Skill, 2));
        Item apple =
            new Consumable(2, "apple", "it has very suspicious color", 5.0d,
                new Adjustment(AdjustmentType.Min, PropertyType.Health, 0));
        section1.AddItems(shortSword, apple);
        section1.AddExit(new Exit("Old Library", section2, false));

        // Section2 parameters
        section2.AddExit(new Exit("To the Start", section1, false));
        section2.AddExit(new Exit("Go to Mystic Garden", section3, false));
        Item longSword =
            new Equipable(3, "longsword", "steel longsword", 3.0,
                new Adjustment(AdjustmentType.Restore, PropertyType.Skill, 4));
        Character dog = new Monster(CharacterType.Animal, MonsterType.Enemy, "Helldog", "It is a very angry helldog",
            20, 8, new Inventory(8));
        section2.AddItem(longSword);
        section2.AddCharacter(dog);




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
        return new Player(CharacterType.Human, "Tamas", "Its me", 12, 10, 15, new Inventory(10));
    }
}
