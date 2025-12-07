namespace Runtime.Player.Choice
{
    public interface IChoice
    {
        string Id { get; }
        void SetInteractable(IChoiceInteractable interactable);
        bool CheckAvailability();
        void Select();
    }
}