namespace AdventureBookApp.Model.Location;

public class Exit
{
    public int Id { get; }
    private string Description { get; }
    public Section DestinationSection { get; }
    public bool IsHidden { get; set; }

    public Exit(string description, Section destinationSection, bool isHidden)
    {
        Id = destinationSection.Id;
        DestinationSection = destinationSection;
        Description = description;
        IsHidden = isHidden;
    }

    public override string ToString()
    {
        return $"{Description} ({DestinationSection.Id})";
    }

    private bool Equals(Exit other)
    {
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Exit)obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }
}