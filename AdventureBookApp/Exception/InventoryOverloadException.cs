namespace AdventureBookApp.Exception;

using System;

public class InventoryOverloadException : Exception
{
    // Default constructor
    public InventoryOverloadException()
        : base("Inventory is full")
    {
    }

    // Constructor with custom message
    public InventoryOverloadException(string message)
        : base(message)
    {
    }
}
