namespace Runtime.Item
{
    public interface IItemDatabase
    {
        void AddItem(ItemDefinition item);
        void RemoveItem(ItemDefinition item);
        bool TryGetItem(string uuid, out ItemDefinition itemType);
    }
}