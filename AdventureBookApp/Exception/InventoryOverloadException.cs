namespace AdventureBookApp.Exception;

using System;

public class InventoryOverloadException : Exception
{
    public InventoryOverloadException()
        : base("Inventory is full")
    {
    }
    
    public InventoryOverloadException(string message)
        : base(message)
    {
    }
}
