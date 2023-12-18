namespace AdventureBookApp.Exception;

using System;

public class InventoryOverloadException : Exception
{
    public InventoryOverloadException(string message)
        : base(message)
    {
    }
}
