using AdventureBookApp.Enum;

namespace AdventureBookApp.Model.Item;

public class Adjustment
{
    public AdjustmentType AdjustmentType { get; }
    public PropertyType PropertyType { get; }
    public int Value { get; }

    public Adjustment(AdjustmentType adjustmentType, PropertyType propertyType, int value)
    {
        AdjustmentType = adjustmentType;
        PropertyType = propertyType;
        Value = value;
    }
}